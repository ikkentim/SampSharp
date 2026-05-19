namespace SampSharp.Entities.SAMP.Commands;

internal class ConsoleCommandProcessingMiddleware(EventDelegate next)
{
    /// <summary>Invokes the middleware.</summary>
    public object? Invoke(EventContext context, IConsoleCommandService commandService)
    {
        var result = next(context);

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
            var cmdContext = new ConsoleCommandDispatchContext(sender.Player, msg =>
            {
                if (sender.HandleConsoleMessage is not null)
                {
                    sender.HandleConsoleMessage(msg);
                }
                else if (sender.Player is not null)
                {
                    sender.Player.SendClientMessage(msg);
                }
                else
                {
                    Console.WriteLine(msg);
                }
            });

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