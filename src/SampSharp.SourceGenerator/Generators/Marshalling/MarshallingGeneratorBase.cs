using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SampSharp.SourceGenerator.Marshalling;
using SampSharp.SourceGenerator.Models;
using SampSharp.SourceGenerator.SyntaxFactories;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static SampSharp.SourceGenerator.SyntaxFactories.StatementFactory;

namespace SampSharp.SourceGenerator.Generators.Marshalling;

public abstract class MarshallingGeneratorBase(MarshalDirection direction)
{
    private const string LocalInvokeSucceeded =  "__invokeSucceeded";

    /// <summary>
    /// Returns a block with the invocation of the native method including marshalling of parameters and return value.
    /// </summary>
    protected virtual BlockSyntax GetMarshallingBlock(MarshallingStubGenerationContext ctx)
    {
        return ctx.RequiresMarshalling 
            ? GenerateInvocationWithMarshalling(ctx)
            : GenerateInvocationWithoutMarshalling(ctx);
    }

    protected abstract ExpressionSyntax GetInvocation(MarshallingStubGenerationContext ctx);

    private BlockSyntax GenerateInvocationWithoutMarshalling(MarshallingStubGenerationContext ctx)
    {
        var invoke = GetInvocation(ctx);

        if (ctx.Symbol.ReturnsVoid)
        {
            return Block(ExpressionStatement(invoke));
        }
            
        if (ctx.Symbol.ReturnsByRef || ctx.Symbol.ReturnsByRefReadonly)
        {
            invoke = RefExpression(invoke);
        }

        return Block(ReturnStatement(invoke));
    }

    protected static IEnumerable<ArgumentSyntax> GetInvocationArguments(MarshallingStubGenerationContext ctx)
    {
        return ctx.Parameters.Select(GetArgumentForPInvokeParameter);
    }

    private BlockSyntax GenerateInvocationWithMarshalling(MarshallingStubGenerationContext ctx)
    {
        return direction == MarshalDirection.ManagedToUnmanaged 
            ? GenerateInvocationWithMarshallingManagedToUnmanaged(ctx) 
            : GenerateInvocationWithMarshallingUnmanagedToManaged(ctx);
    }
    
    private BlockSyntax GenerateInvocationWithMarshallingManagedToUnmanaged(MarshallingStubGenerationContext ctx)
    {
        // The generated method consists of the following content:
        //
        // LocalsInit
        // Setup
        // try
        // {
        //   Marshal
        //   {
        //     PinnedMarshal
        //     Invoke 
        //   }
        //   [[__invokeSucceeded = true;]]
        //   NotifyForSuccessfulInvoke
        //   UnmarshalCapture
        //   Unmarshal
        // }
        // finally
        // {
        //   if (__invokeSucceeded)
        //   {
        //      GuaranteedUnmarshal
        //      CleanupCalleeAllocated
        //   }
        //   CleanupCallerAllocated
        // }
        //
        // return: retVal

        var steps = CollectPhases(ctx);

        var initLocals = List(GenerateInitLocals(ctx));

        // if callee cleanup or guaranteed unmarshalling is required, we need to keep track of invocation success
        var guaranteedStatements = steps.GuaranteedUnmarshal.AddRange(steps.CleanupCallee);
        var notify = steps.Notify;

        GenerateGuaranteedBlock(ref guaranteedStatements, ref initLocals, ref notify);

        // chain all pin statements
        var invoke = AddReturnValueAssignmentToInvoke(ctx, GetInvocation(ctx));
        var pinnedBlock = ChainPins(steps, invoke);

        // wire up steps
        var statements = initLocals
            .AddRange(steps.Setup)
            .AddRange(
                TryCatch(
                    tryBlock: steps.Marshal
                        .Add(pinnedBlock)
                        .AddRange(notify)
                        .AddRange(steps.UnmarshalCapture)
                        .AddRange(steps.Unmarshal),
                    finallyBlock: guaranteedStatements
                        .AddRange(steps.CleanupCaller)));

        // add return statement if the method returns a value
        if (!ctx.Symbol.ReturnsVoid)
        {
            ExpressionSyntax returnExpression = IdentifierName(MarshallerConstants.LocalReturnValue);

            if (ctx.ReturnsByRef)
            {
                returnExpression = RefExpression(
                    PrefixUnaryExpression(
                        SyntaxKind.PointerIndirectionExpression,
                        returnExpression));
            }
            else if (ctx.Symbol.ReturnType is { IsReferenceType: true, NullableAnnotation: NullableAnnotation.NotAnnotated })
            {
                returnExpression = PostfixUnaryExpression(SyntaxKind.SuppressNullableWarningExpression, returnExpression);
            }

            statements = statements.Add(ReturnStatement(returnExpression));
        }

        return Block(statements);
    }

    private BlockSyntax GenerateInvocationWithMarshallingUnmanagedToManaged(MarshallingStubGenerationContext ctx)
    {
        // NOTE: We support only unmarshalling options for unmanaged to managed marshalling. 
        //
        // LocalsInit
        // Setup
        // try
        // {
        //   GuaranteedUnmarshal
        //   UnmarshalCapture
        //   Unmarshal
        //   {
        //     p/invoke 
        //   }
        //   [[__invokeSucceeded = true;]]
        //   NotifyForSuccessfulInvoke
        //   Marshal
        //   PinnedMarshal
        // }
        // finally
        // {
        //   CleanupCallerAllocated
        // }
        //
        // return: retVal

        var steps = CollectPhases(ctx);

        if (steps.CleanupCallee.Count > 0)
        {
            throw new InvalidOperationException("cannot cleanup callee allocated");
        }

        var initLocals = List(GenerateInitLocals(ctx));

        // if callee cleanup or guaranteed unmarshalling is required, we need to keep track of invocation success
        var notify = steps.Notify;

        var invoke = ExpressionStatement(AddReturnValueAssignmentToInvoke(ctx, GetInvocation(ctx)));

        // wire up steps
        var statements = initLocals
            .AddRange(steps.Setup)
            .AddRange(
                TryCatch(
                    tryBlock: steps.UnmarshalCapture
                        .AddRange(steps.Unmarshal)
                        .AddRange(steps.GuaranteedUnmarshal)
                        .Add(invoke)
                        .AddRange(notify),
                    finallyBlock: steps.CleanupCaller));

        // add return statement if the method returns a value
        if (!ctx.Symbol.ReturnsVoid)
        {
            var returnIdentifier = ctx.ReturnValue.Generator.UsesNativeIdentifier
                ? ctx.ReturnValue.GetNativeId()
                : MarshallerConstants.LocalReturnValue;

            ExpressionSyntax returnExpression = IdentifierName(returnIdentifier);

            if (ctx.ReturnsByRef)
            {
                returnExpression = RefExpression(returnExpression);
            }
            else if (ctx.Symbol.ReturnType is { IsReferenceType: true, NullableAnnotation: NullableAnnotation.NotAnnotated })
            {
                returnExpression = PostfixUnaryExpression(SyntaxKind.SuppressNullableWarningExpression, returnExpression);
            }

            statements = statements.Add(ReturnStatement(returnExpression));
        }

        return Block(statements);
    }

    private static void GenerateGuaranteedBlock(
        ref SyntaxList<StatementSyntax> guarannteedStatements, 
        ref SyntaxList<StatementSyntax> initLocals, 
        ref SyntaxList<StatementSyntax> notify)
    {
        if (guarannteedStatements.Count > 0)
        {
            // var __invokeSucceeded = false; to the initializer block
            initLocals = initLocals.Add(GenerateInvokeSucceededLocal());

            // insert __invokeSucceeded = true; at the beginning of the notify step
            notify = notify.Insert(0, GenerateSetInvokeSucceeded());
        
            guarannteedStatements = SingletonList<StatementSyntax>(
                IfStatement(
                    IdentifierName(LocalInvokeSucceeded), 
                    Block(guarannteedStatements)));
        }
    }

    private static IEnumerable<StatementSyntax> GenerateInitLocals(MarshallingStubGenerationContext ctx)
    {
        foreach(var p in ctx.Parameters)
        {
            if (!p.Generator.UsesNativeIdentifier)
            {
                continue;
            }

            if (p.Direction == MarshalDirection.ManagedToUnmanaged)
            {
                yield return DeclareLocal(p.Generator.GetNativeType(p), p.GetNativeId());
            }
            else
            {
                // UnmanagedToManaged
                yield return DeclareLocal(p.ManagedType.TypeName,  p.GetManagedId());
            }
        }
        
        if (!ctx.Symbol.ReturnsVoid)
        {
            var managedReturnType = ctx.ReturnValue.ManagedType.TypeName;
            if (ctx.ReturnsByRef)
            {
                // when marshalling ref-return types are marshalled as pointers
                managedReturnType = PointerType(managedReturnType);
            }
            else if (ctx.Symbol.ReturnType is { IsReferenceType: true, NullableAnnotation: NullableAnnotation.NotAnnotated })
            {
                managedReturnType = NullableType(managedReturnType);
            }

            yield return
                DeclareLocal(
                    managedReturnType,
                    ctx.ReturnValue.GetManagedId());
            
            if (ctx.ReturnValue.Generator.UsesNativeIdentifier)
            {
                yield return
                    DeclareLocal(
                        ctx.ReturnValue.Generator.GetNativeType(ctx.ReturnValue),
                        ctx.ReturnValue.GetNativeId());
            }
        }
    }

    private static LocalDeclarationStatementSyntax GenerateInvokeSucceededLocal()
    {
        return DeclareLocal(PredefinedType(Token(SyntaxKind.BoolKeyword)), LocalInvokeSucceeded);
    }

    private static ExpressionStatementSyntax GenerateSetInvokeSucceeded()
    {
        return ExpressionStatement(
            AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression, 
                IdentifierName(LocalInvokeSucceeded), 
                LiteralExpression(SyntaxKind.TrueLiteralExpression)));
    }

    private static StatementSyntax ChainPins(MarshallingPhases phases, ExpressionSyntax invoke)
    {
        StatementSyntax pinnedBlock = Block(phases.PinnedMarshal.Add(ExpressionStatement(invoke)));
        for (var i = phases.Pin.Count - 1; i >= 0; i--)
        {
            if (phases.Pin[i] is not FixedStatementSyntax fixedStatement)
            {
                throw new InvalidOperationException("Pinned statement is not fixed statement");
            }

            pinnedBlock = fixedStatement.WithStatement(pinnedBlock);
        }

        return pinnedBlock;
    }

    private static SyntaxList<StatementSyntax> TryCatch(SyntaxList<StatementSyntax> tryBlock, SyntaxList<StatementSyntax> finallyBlock)
    {
        if (finallyBlock.Count == 0)
        {
            return tryBlock;
        }

        return SingletonList<StatementSyntax>(
            TryStatement()
                .WithBlock(Block(tryBlock))
                .WithFinally(
                    FinallyClause(
                        Block(finallyBlock))));
    }

    private static ExpressionSyntax AddReturnValueAssignmentToInvoke(MarshallingStubGenerationContext ctx, ExpressionSyntax invoke)
    {
        if (!ctx.Symbol.ReturnsVoid)
        {
            string assignedLocal;
            if (ctx.ReturnValue.Direction == MarshalDirection.ManagedToUnmanaged)
            {
                assignedLocal = ctx.ReturnValue.Generator.UsesNativeIdentifier ? ctx.ReturnValue.GetNativeId() : ctx.ReturnValue.GetManagedId();
            }
            else
            {
                // UnmanagedToManaged
                assignedLocal = ctx.ReturnValue.Generator.UsesNativeIdentifier ? ctx.ReturnValue.GetManagedId() : ctx.ReturnValue.GetNativeId();
            }

            invoke = 
                AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression, 
                    IdentifierName(assignedLocal), 
                    invoke);
        }

        return invoke;
    }
    
    private static MarshallingPhases CollectPhases(MarshallingStubGenerationContext ctx)
    {
        var setup = Phase(ctx, MarshalPhase.Setup, MarshallingCodeGenDocumentation.COMMENT_SETUP);
        var marshal = Phase(ctx, MarshalPhase.Marshal, MarshallingCodeGenDocumentation.COMMENT_MARSHAL);
        var pinnedMarshal = Phase(ctx, MarshalPhase.PinnedMarshal, MarshallingCodeGenDocumentation.COMMENT_PINNED_MARSHAL);
        var pin = Phase(ctx, MarshalPhase.Pin, MarshallingCodeGenDocumentation.COMMENT_PIN);
        var notify = Phase(ctx, MarshalPhase.NotifyForSuccessfulInvoke, MarshallingCodeGenDocumentation.COMMENT_NOTIFY);
        var unmarshalCapture = Phase(ctx, MarshalPhase.UnmarshalCapture, MarshallingCodeGenDocumentation.COMMENT_UNMARSHAL_CAPTURE);
        var unmarshal = Phase(ctx, MarshalPhase.Unmarshal, MarshallingCodeGenDocumentation.COMMENT_UNMARSHAL);
        var guaranteedUnmarshal = Phase(ctx, MarshalPhase.GuaranteedUnmarshal, MarshallingCodeGenDocumentation.COMMENT_GUARANTEED_UNMARSHAL);
        var cleanupCallee = Phase(ctx, MarshalPhase.CleanupCalleeAllocated, MarshallingCodeGenDocumentation.COMMENT_CLEANUP_CALLEE);
        var cleanupCaller = Phase(ctx, MarshalPhase.CleanupCallerAllocated, MarshallingCodeGenDocumentation.COMMENT_CLEANUP_CALLER);

        return new MarshallingPhases(setup, marshal, pinnedMarshal, pin, notify, unmarshalCapture, unmarshal, guaranteedUnmarshal, cleanupCallee, cleanupCaller);
    }

    private static ArgumentSyntax GetArgumentForPInvokeParameter(IdentifierStubContext ctx)
    {
        var arg = ctx.GetManagedId();
        if (ctx.Generator.UsesNativeIdentifier)
        {
            arg = ctx.GetNativeId();
        }
        
        ExpressionSyntax expr = IdentifierName(arg);

        
        if (ctx.RefKind is RefKind.In or RefKind.RefReadOnlyParameter)
        {
            expr = PrefixUnaryExpression(SyntaxKind.AddressOfExpression, expr);
        }

        return HelperSyntaxFactory.WithPInvokeParameterRefToken(Argument(expr), ctx.RefKind);
    }
    
    private static SyntaxList<StatementSyntax> Phase(
        MarshallingStubGenerationContext ctx,
        MarshalPhase phase,
        string? comment)
    {
        var elements = ctx.Parameters
            .SelectMany(x => x.Generator.Generate(phase, x))
            .Concat(ctx.ReturnValue.Generator.Generate(phase, ctx.ReturnValue));

        var result = List(elements);

        if (comment != null && result.Count > 0)
        {
            result = result.Replace(result[0],
                result[0]
                    .WithLeadingTrivia(Comment(comment)));
        }

        return result;
    }

    private record MarshallingPhases(
        SyntaxList<StatementSyntax> Setup,
        SyntaxList<StatementSyntax> Marshal,
        SyntaxList<StatementSyntax> PinnedMarshal,
        SyntaxList<StatementSyntax> Pin,
        SyntaxList<StatementSyntax> Notify,
        SyntaxList<StatementSyntax> UnmarshalCapture,
        SyntaxList<StatementSyntax> Unmarshal,
        SyntaxList<StatementSyntax> GuaranteedUnmarshal,
        SyntaxList<StatementSyntax> CleanupCallee,
        SyntaxList<StatementSyntax> CleanupCaller);

}