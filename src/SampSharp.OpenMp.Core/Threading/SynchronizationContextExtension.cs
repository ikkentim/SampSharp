using SampSharp.OpenMp.Core.Api;
using SampSharp.OpenMp.Core.Std.Chrono;

namespace SampSharp.OpenMp.Core;

[Extension(0x05e87f7adbdc0b7d)]
internal class SynchronizationContextExtension : Extension, ICoreEventHandler
{
    private readonly ICore _core;
    private static SynchronizationContextExtension? _active;

    private readonly int _mainThreadId = Environment.CurrentManagedThreadId;
    private readonly SampSharpSynchronizationContext _context = new();

    public SynchronizationContextExtension(ICore core)
    {
        _core = core;
        _active = this;

        core.GetEventDispatcher().AddEventHandler(this);

        SynchronizationContext.SetSynchronizationContext(_context);
    }
    
    public static SynchronizationContextExtension Active => _active ?? throw new InvalidOperationException("No active synchronization context.");

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
}