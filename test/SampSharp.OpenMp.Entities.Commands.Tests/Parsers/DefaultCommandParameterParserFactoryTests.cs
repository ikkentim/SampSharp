using System;
using System.Collections.Generic;
using System.Reflection;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Parsers;

/// <summary>
/// Tests for DefaultCommandParameterParserFactory, which maps parameter types to parsers.
/// </summary>
public class DefaultCommandParameterParserFactoryTests
{
    private static ParameterInfo[] CreateParams(params Type[] types)
    {
        // Use reflection to build a synthetic parameter list via a helper method
        return DummyHelper.MakeParameters(types);
    }

    private readonly DefaultCommandParameterParserFactory _factory = new();

    [Fact]
    public void CreateParser_IntParameter_ReturnsIntParser()
    {
        var parameters = CreateParams(typeof(int));

        var parser = _factory.CreateParser(parameters, 0);

        parser.ShouldBeOfType<IntParser>();
    }

    [Fact]
    public void CreateParser_FloatParameter_ReturnsFloatParser()
    {
        var parameters = CreateParams(typeof(float));

        var parser = _factory.CreateParser(parameters, 0);

        parser.ShouldBeOfType<FloatParser>();
    }

    [Fact]
    public void CreateParser_DoubleParameter_ReturnsDoubleParser()
    {
        var parameters = CreateParams(typeof(double));

        var parser = _factory.CreateParser(parameters, 0);

        parser.ShouldBeOfType<DoubleParser>();
    }

    [Fact]
    public void CreateParser_BoolParameter_ReturnsBooleanParser()
    {
        var parameters = CreateParams(typeof(bool));

        var parser = _factory.CreateParser(parameters, 0);

        parser.ShouldBeOfType<BooleanParser>();
    }

    [Fact]
    public void CreateParser_StringParameterLast_ReturnsStringParser()
    {
        // When string is the last parameter, use StringParser (greedy)
        var parameters = CreateParams(typeof(int), typeof(string));

        var parser = _factory.CreateParser(parameters, 1);

        parser.ShouldBeOfType<StringParser>();
    }

    [Fact]
    public void CreateParser_StringParameterNotLast_ReturnsWordParser()
    {
        // When string is not last, use WordParser (single word)
        var parameters = CreateParams(typeof(string), typeof(int));

        var parser = _factory.CreateParser(parameters, 0);

        parser.ShouldBeOfType<WordParser>();
    }

    [Fact]
    public void CreateParser_StringOnlyParam_ReturnsStringParser()
    {
        var parameters = CreateParams(typeof(string));

        var parser = _factory.CreateParser(parameters, 0);

        parser.ShouldBeOfType<StringParser>();
    }

    [Fact]
    public void CreateParser_PlayerParameter_ReturnsPlayerParser()
    {
        var parameters = CreateParams(typeof(Player));

        var parser = _factory.CreateParser(parameters, 0);

        parser.ShouldBeOfType<PlayerParser>();
    }

    [Fact]
    public void CreateParser_EnumParameter_ReturnsEnumParser()
    {
        var parameters = CreateParams(typeof(DayOfWeek));

        var parser = _factory.CreateParser(parameters, 0);

        parser.ShouldBeOfType<EnumParser>();
    }

    [Fact]
    public void CreateParser_UnknownType_ReturnsNull()
    {
        var parameters = CreateParams(typeof(List<string>));

        var parser = _factory.CreateParser(parameters, 0);

        parser.ShouldBeNull();
    }
}

/// <summary>
/// Helper to build ParameterInfo arrays for testing via reflection on a template method.
/// </summary>
internal static class DummyHelper
{
    public static ParameterInfo[] MakeParameters(Type[] types)
    {
        // We use reflection on a compiled method to get real ParameterInfo objects.
        // The simplest approach: look up a pre-defined method by signature.
        return ParameterInfoFactory.Build(types);
    }
}

/// <summary>
/// Builds ParameterInfo arrays from method signatures using reflection.
/// </summary>
internal static class ParameterInfoFactory
{
    // Template methods for each supported combination - we use one real method per type signature.
    private static readonly MethodInfo SingleInt = typeof(ParameterInfoFactory).GetMethod(nameof(T_Int), BindingFlags.Static | BindingFlags.NonPublic)!;
    private static readonly MethodInfo SingleFloat = typeof(ParameterInfoFactory).GetMethod(nameof(T_Float), BindingFlags.Static | BindingFlags.NonPublic)!;
    private static readonly MethodInfo SingleDouble = typeof(ParameterInfoFactory).GetMethod(nameof(T_Double), BindingFlags.Static | BindingFlags.NonPublic)!;
    private static readonly MethodInfo SingleBool = typeof(ParameterInfoFactory).GetMethod(nameof(T_Bool), BindingFlags.Static | BindingFlags.NonPublic)!;
    private static readonly MethodInfo SingleString = typeof(ParameterInfoFactory).GetMethod(nameof(T_String), BindingFlags.Static | BindingFlags.NonPublic)!;
    private static readonly MethodInfo SinglePlayer = typeof(ParameterInfoFactory).GetMethod(nameof(T_Player), BindingFlags.Static | BindingFlags.NonPublic)!;
    private static readonly MethodInfo SingleEnum = typeof(ParameterInfoFactory).GetMethod(nameof(T_Enum), BindingFlags.Static | BindingFlags.NonPublic)!;
    private static readonly MethodInfo SingleList = typeof(ParameterInfoFactory).GetMethod(nameof(T_List), BindingFlags.Static | BindingFlags.NonPublic)!;
    private static readonly MethodInfo IntString = typeof(ParameterInfoFactory).GetMethod(nameof(T_IntString), BindingFlags.Static | BindingFlags.NonPublic)!;
    private static readonly MethodInfo StringInt = typeof(ParameterInfoFactory).GetMethod(nameof(T_StringInt), BindingFlags.Static | BindingFlags.NonPublic)!;

    private static readonly Dictionary<string, MethodInfo> _lookup = new()
    {
        [Key(typeof(int))] = SingleInt,
        [Key(typeof(float))] = SingleFloat,
        [Key(typeof(double))] = SingleDouble,
        [Key(typeof(bool))] = SingleBool,
        [Key(typeof(string))] = SingleString,
        [Key(typeof(Player))] = SinglePlayer,
        [Key(typeof(DayOfWeek))] = SingleEnum,
        [Key(typeof(List<string>))] = SingleList,
        [Key(typeof(int), typeof(string))] = IntString,
        [Key(typeof(string), typeof(int))] = StringInt,
    };

    private static string Key(params Type[] types) => string.Join(",", (IEnumerable<Type>)types);

    public static ParameterInfo[] Build(Type[] types)
    {
        var key = Key(types);
        if (_lookup.TryGetValue(key, out var method))
        {
            return method.GetParameters();
        }

        throw new NotSupportedException($"No template method for type signature: {key}");
    }

#pragma warning disable IDE0060
    private static void T_Int(int x) { }
    private static void T_Float(float x) { }
    private static void T_Double(double x) { }
    private static void T_Bool(bool x) { }
    private static void T_String(string x) { }
    private static void T_Player(Player x) { }
    private static void T_Enum(DayOfWeek x) { }
    private static void T_List(List<string> x) { }
    private static void T_IntString(int a, string b) { }
    private static void T_StringInt(string a, int b) { }
#pragma warning restore IDE0060
}
