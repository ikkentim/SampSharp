namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Middleware on <c>OnPlayerCommandText</c> that, if the [Event]-handlers leave
/// the input unhandled (return <see langword="false"/>/<see langword="null"/>),
/// dispatches it through <see cref="IPlayerCommandService"/>.
/// </summary>
public class PlayerCommandProcessingMiddleware
{
    private readonly EventDelegate _next;

    /// <summary>Initializes a new instance.</summary>
    public PlayerCommandProcessingMiddleware(EventDelegate next)
    {
        _next = next;
    }

    /// <summary>Invokes the middleware.</summary>
    public object? Invoke(EventContext context, IPlayerCommandService commandService)
    {
        var result = _next(context);

        // Successful response → done. We treat anything truthy as "handled" (matches EventDispatcher semantics).
        if (IsHandled(result))
            return result;

        if (context.Arguments is [EntityId player, string text, ..])
            return commandService.Invoke(context.EventServices, player, text);

        return result;
    }

    private static bool IsHandled(object? result) => result switch
    {
        null => false,
        bool b => b,
        int i => i != 0,
        MethodResult mr => mr.Value,
        _ => true,
    };
}
