using SampSharp.OpenMp.Core;
using Xunit.Sdk;
using Xunit.v3;

namespace TestMode.UnitTests;

public class CustomTestExecutor : XunitTestFrameworkExecutor
{
    public CustomTestExecutor(IXunitTestAssembly testAssembly) : base(testAssembly)
    {
    }

    public override async ValueTask RunTestCases(IReadOnlyCollection<IXunitTestCase> testCases, IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions, CancellationToken cancellationToken)
    {
        await TaskHelper.SwitchToMainThread();
        await base.RunTestCases(testCases, executionMessageSink, executionOptions, cancellationToken);
    }
}