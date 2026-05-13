namespace SampSharp.Entities.SAMP.Commands;

internal class ConsoleCommandProcessingMiddleware
{
    private readonly EventDelegate _next;

    public ConsoleCommandProcessingMiddleware(EventDelegate next)
    {
        _next = next;
    }

    /// <summary>Invokes the middleware.</summary>
    public object? Invoke(EventContext context, IConsoleCommandService commandService)
    {
        var result = _next(context);

        // Successful response → done. We treat anything truthy as "handled" (matches EventDispatcher semantics).
        if (IsHandled(result))
        {
            return result;
        }

        if (context.Arguments is [string command, string args, ConsoleCommandSender sender])
        {
            // Build input text from command and args
            var inputText = string.IsNullOrEmpty(args) ? command : $"{command} {args}";

            // Create a dispatch context with message handler to send responses back
            var cmdContext = new ConsoleCommandDispatchContext(sender.Player, msg => Console.WriteLine(msg)); // TODO: ConsoleCommandSender is missing a messageSender

            return commandService.Invoke(context.EventServices, cmdContext, inputText);
        }

        return result;
    }

    private static bool IsHandled(object? result)
    {
        return result switch
        {
            null => false,
            bool b => b,
            MethodResult mr => mr.Value,
            _ => true
        };
    }
}