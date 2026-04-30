using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SampSharp.SourceGenerator.SyntaxFactories;

/// <summary>
/// Creates common statement syntax nodes.
/// </summary>
public static class StatementFactory
{
    public static LocalDeclarationStatementSyntax DeclareLocal(TypeSyntax type, string identifier)
    {
        return LocalDeclarationStatement(
            VariableDeclaration(type,
                SingletonSeparatedList(
                    VariableDeclarator(Identifier(identifier))
                        .WithInitializer(
                            EqualsValueClause(
                                LiteralExpression(SyntaxKind.DefaultLiteralExpression, Token(SyntaxKind.DefaultKeyword)))))));
    }


    public static LocalDeclarationStatementSyntax DeclareLocal(TypeSyntax type, string identifier, ExpressionSyntax value)
    {
        return LocalDeclarationStatement(
            VariableDeclaration(type)
                .WithVariables(
                    SingletonSeparatedList(
                        VariableDeclarator(
                                Identifier(identifier))
                            .WithInitializer(
                                EqualsValueClause(
                                    value)))));
    }

    public static ExpressionStatementSyntax Invoke(TypeSyntax target, string method)
    {
        return ExpressionStatement(
            ExpressionFactory.InvocationExpression(
                target,
                method));
    }

    public static ExpressionStatementSyntax Invoke(string target, string method)
    {
        return ExpressionStatement(
            ExpressionFactory.InvocationExpression(
                target,
                method));
    }

    public static ExpressionStatementSyntax Invoke(TypeSyntax target, string method, params ArgumentSyntax[] arguments)
    {
        return ExpressionStatement(
            ExpressionFactory.InvocationExpression(
                target,
                method,
                arguments));
    }
    public static ExpressionStatementSyntax Invoke(string target, string method, params ArgumentSyntax[] arguments)
    {
        return ExpressionStatement(
            ExpressionFactory.InvocationExpression(
                target,
                method,
                arguments));
    }

    public static ExpressionStatementSyntax Assign(string destination, ExpressionSyntax value)
    {
        return ExpressionStatement(
            AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                IdentifierName(destination),
                value));
    }
}

public static class ExpressionFactory
{
    public static InvocationExpressionSyntax InvocationExpression(string target, string method)
    {
        return InvocationExpression(IdentifierName(target), method);
    }

    public static InvocationExpressionSyntax InvocationExpression(TypeSyntax target, string method)
    {
        return SyntaxFactory.InvocationExpression(
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                target,
                IdentifierName(method)));
    }

    public static InvocationExpressionSyntax InvocationExpression(string target, string method, params ArgumentSyntax[] arguments)
    {
        return InvocationExpression(IdentifierName(target), method, arguments);
    }

    public static InvocationExpressionSyntax InvocationExpression(TypeSyntax target, string method, params ArgumentSyntax[] arguments)
    {
        return SyntaxFactory.InvocationExpression(
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                target,
                IdentifierName(method))).WithArgumentList(ArgumentList(SeparatedList(arguments)));
    }
}