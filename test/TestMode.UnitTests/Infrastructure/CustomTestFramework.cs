using System.Reflection;
using Xunit.Internal;
using Xunit.v3;

namespace TestMode.UnitTests;

public class CustomTestFramework : XunitTestFramework
{
    protected override ITestFrameworkExecutor CreateExecutor(Assembly assembly) =>
        new CustomTestExecutor(new XunitTestAssembly(Guard.ArgumentNotNull(assembly), null, assembly.GetName().Version));
}