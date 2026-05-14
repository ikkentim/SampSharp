using Microsoft.Extensions.DependencyInjection;

namespace SampSharp.Entities;

/// <summary>
/// Represents a middleware which adds a Dependency Injection scope to the <see cref="EventContext" /> of an event.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="EventScopeMiddleware" /> class.
/// </remarks>
/// <param name="next">The next middleware handler.</param>
public class EventScopeMiddleware(EventDelegate next)
{
    private readonly EventContextScoped _context = new();
    private readonly EventDelegate _next = next;

    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    public object? Invoke(EventContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        using var scope = context.EventServices.CreateScope();

        _context.BaseContext = context;
        _context.Scope = scope;

        var result = _next(_context);

        _context.BaseContext = null;
        _context.Scope = null;

        return result;
    }

    private sealed class EventContextScoped : EventContext
    {
        public EventContext? BaseContext { get; set; }
        public IServiceScope? Scope { get; set; }

        public override string Name => BaseContext!.Name;
        public override object[] Arguments => BaseContext!.Arguments;

        public override IServiceProvider EventServices => Scope!.ServiceProvider;
    }
}