namespace SampSharp.Entities;

internal sealed class EventContextImpl(string name, IServiceProvider eventServices) : EventContext
{
    private object[]? _arguments;

    public override string Name { get; } = name;
    public override IServiceProvider EventServices { get; } = eventServices;
    public override object[] Arguments => _arguments!;

    public void SetArguments(ReadOnlySpan<object> arguments)
    {
        if (_arguments == null || _arguments.Length != arguments.Length)
        {
            _arguments = new object[arguments.Length];
        }

        arguments.CopyTo(_arguments);
    }
}