namespace SampSharp.Entities;

internal class EventContextImpl : EventContext
{
    private object[]? _arguments;

    public EventContextImpl(string name, IServiceProvider eventServices)
    {
        Name = name;
        EventServices = eventServices;
    }

    public override string Name { get; }
    public override IServiceProvider EventServices { get; }
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