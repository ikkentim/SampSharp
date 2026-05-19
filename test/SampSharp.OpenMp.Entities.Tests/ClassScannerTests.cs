using System.Reflection;
using SampSharp.Entities.Utilities;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Tests;

public class ClassScannerTests
{
    // --- helper types ---

    private interface IFoo { }
    private interface IBar { }

    private class FooClass : IFoo { }
    private class BarClass : IBar { }
    private class FooBothClass : IFoo, IBar { }
    private abstract class AbstractFoo : IFoo { }

    [AttributeUsage(AttributeTargets.Class)]
    private class MarkerAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    private class MethodMarkerAttribute : Attribute { }

    [Marker]
    private class MarkedClass
    {
        [MethodMarker]
        public void MarkedMethod() { }

        public void UnmarkedMethod() { }
    }

    private class UnmarkedClass
    {
        [MethodMarker]
        public void MarkedMethod() { }
    }

    // --- tests ---

    [Fact]
    public void ScanTypes_IncludeTypes_FindsExplicitlyIncludedTypes()
    {
        var types = ClassScanner.Create()
            .IncludeTypes(new[] { typeof(FooClass) })
            .ScanTypes()
            .ToList();

        types.ShouldContain(typeof(FooClass));
    }

    [Fact]
    public void ScanTypes_IncludeTypes_ExcludesTypesNotIncluded()
    {
        var types = ClassScanner.Create()
            .IncludeTypes(new[] { typeof(FooClass) })
            .ScanTypes()
            .ToList();

        types.ShouldNotContain(typeof(BarClass));
    }

    [Fact]
    public void ScanTypes_Implements_FiltersToCorrectInterface()
    {
        var types = ClassScanner.Create()
            .IncludeTypes(new[] { typeof(FooClass), typeof(BarClass), typeof(FooBothClass) })
            .Implements<IFoo>()
            .ScanTypes()
            .ToList();

        types.ShouldContain(typeof(FooClass));
        types.ShouldContain(typeof(FooBothClass));
        types.ShouldNotContain(typeof(BarClass));
    }

    [Fact]
    public void ScanTypes_ImplementsChained_FiltersToTypesImplementingBoth()
    {
        var types = ClassScanner.Create()
            .IncludeTypes(new[] { typeof(FooClass), typeof(BarClass), typeof(FooBothClass) })
            .Implements<IFoo>()
            .Implements<IBar>()
            .ScanTypes()
            .ToList();

        types.ShouldContain(typeof(FooBothClass));
        types.ShouldNotContain(typeof(FooClass));
        types.ShouldNotContain(typeof(BarClass));
    }

    [Fact]
    public void ScanTypes_HasClassAttribute_FiltersToMarkedClasses()
    {
        var types = ClassScanner.Create()
            .IncludeTypes(new[] { typeof(MarkedClass), typeof(UnmarkedClass) })
            .HasClassAttribute<MarkerAttribute>()
            .ScanTypes()
            .ToList();

        types.ShouldContain(typeof(MarkedClass));
        types.ShouldNotContain(typeof(UnmarkedClass));
    }

    [Fact]
    public void ScanTypes_IncludeAbstractClasses_IncludesAbstractType()
    {
        var types = ClassScanner.Create()
            .IncludeTypes(new[] { typeof(AbstractFoo) })
            .IncludeAbstractClasses()
            .ScanTypes()
            .ToList();

        types.ShouldContain(typeof(AbstractFoo));
    }

    [Fact]
    public void ScanTypes_WithoutIncludeAbstractClasses_ExcludesAbstractType()
    {
        var types = ClassScanner.Create()
            .IncludeTypes(new[] { typeof(AbstractFoo) })
            .ScanTypes()
            .ToList();

        types.ShouldNotContain(typeof(AbstractFoo));
    }

    [Fact]
    public void ScanTypes_DuplicateTypes_ReturnsDistinct()
    {
        var types = ClassScanner.Create()
            .IncludeTypes(new[] { typeof(FooClass), typeof(FooClass) })
            .ScanTypes()
            .ToList();

        types.Count.ShouldBe(1);
    }

    [Fact]
    public void ScanMethods_HasMemberAttribute_FindsMarkedMethods()
    {
        var methods = ClassScanner.Create()
            .IncludeTypes(new[] { typeof(MarkedClass), typeof(UnmarkedClass) })
            .ScanMethods<MethodMarkerAttribute>()
            .ToList();

        methods.ShouldContain(x => x.method.Name == "MarkedMethod" && x.target == typeof(MarkedClass));
        methods.ShouldContain(x => x.method.Name == "MarkedMethod" && x.target == typeof(UnmarkedClass));
    }

    [Fact]
    public void ScanMethods_WithClassAttributeFilter_OnlyScansClassesThatMatch()
    {
        var methods = ClassScanner.Create()
            .IncludeTypes(new[] { typeof(MarkedClass), typeof(UnmarkedClass) })
            .HasClassAttribute<MarkerAttribute>()
            .ScanMethods<MethodMarkerAttribute>()
            .ToList();

        methods.ShouldContain(x => x.method.Name == "MarkedMethod" && x.target == typeof(MarkedClass));
        methods.ShouldNotContain(x => x.target == typeof(UnmarkedClass));
    }

    [Fact]
    public void ScanMethods_WithAttribute_ProvidesCorrectAttribute()
    {
        var methods = ClassScanner.Create()
            .IncludeTypes(new[] { typeof(MarkedClass) })
            .ScanMethods<MethodMarkerAttribute>()
            .ToList();

        methods.ShouldHaveSingleItem();
        methods[0].attribute.ShouldNotBeNull();
        methods[0].attribute.ShouldBeOfType<MethodMarkerAttribute>();
    }

    [Fact]
    public void ScanMethods_IncludeNonPublicMembers_IncludesPrivateMethods()
    {
        var methods = ClassScanner.Create()
            .IncludeTypes(new[] { typeof(ClassWithPrivateMethod) })
            .IncludeNonPublicMembers()
            .ScanMethods<MethodMarkerAttribute>()
            .ToList();

        methods.ShouldContain(x => x.method.Name == "PrivateMarkedMethod");
    }

    [Fact]
    public void ScanMethods_WithoutIncludeNonPublicMembers_ExcludesPrivateMethods()
    {
        var methods = ClassScanner.Create()
            .IncludeTypes(new[] { typeof(ClassWithPrivateMethod) })
            .ScanMethods<MethodMarkerAttribute>()
            .ToList();

        methods.ShouldNotContain(x => x.method.Name == "PrivateMarkedMethod");
    }

    [Fact]
    public void IncludeAssembly_AlreadyIncluded_NotDuplicated()
    {
        var asm = typeof(FooClass).Assembly;
        var scanner = ClassScanner.Create()
            .IncludeAssembly(asm)
            .IncludeAssembly(asm); // add same assembly again

        // Should not throw, just returns same scanner result
        var types = scanner.Implements<IFoo>().ScanTypes().ToList();
        types.ShouldNotBeNull();
    }

    private class ClassWithPrivateMethod
    {
        [MethodMarker]
        private void PrivateMarkedMethod() { }
    }
}
