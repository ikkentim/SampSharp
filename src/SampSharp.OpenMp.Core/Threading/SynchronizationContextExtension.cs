using SampSharp.OpenMp.Core.Api;
using SampSharp.OpenMp.Core.Std.Chrono;

namespace SampSharp.OpenMp.Core;

[Extension(0x05e87f7adbdc0b7d)]
internal class SynchronizationContextExtension : Extension, ICoreEventHandler
{
    private static SynchronizationContextExtension? _active;
    private readonly SampSharpSynchronizationContext _context = new();
    private readonly ICore _core;

    private readonly int _mainThreadId = Environment.CurrentManagedThreadId;

    public SynchronizationContextExtension(ICore core)
    {
        _core = core;
        _active = this;

        core.GetEventDispatcher().AddEventHandler(this);

        SynchronizationContext.SetSynchronizationContext(_context);
    }

    public static SynchronizationContextExtension Active => _active ?? throw new InvalidOperationException("No active synchronization context.");

    public void OnTick(Microseconds elapsed, TimePoint now)
    {
        while (true)
        {
            var message = _context.GetMessage();

            if (message == null)
            {
                break;
            }

            try
            {
                message.Execute();
            }
            catch (Exception ex)
            {
                SampSharpExceptionHandler.HandleException("async", ex);
            }
        }
    }

    public bool IsMainThread()
    {
        return _mainThreadId == Environment.CurrentManagedThreadId;
    }

    public void Invoke(Action continuation)
    {
        _context.Send(_ => continuation(), null);
    }

    protected override void Cleanup()
    {
        _active = null;
        
        SynchronizationContext.SetSynchronizationContext(null);

        _core.GetEventDispatcher().RemoveEventHandler(this);
    }
}