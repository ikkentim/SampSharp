namespace SampSharp.Entities.SAMP.Commands;

internal class ConsoleCommandListMiddleware
{
    private readonly EventDelegate _next;

    public ConsoleCommandListMiddleware(EventDelegate next)
    {
        _next = next;
    }

    /// <summary>Invokes the middleware.</summary>
    public object? Invoke(EventContext context, IConsoleCommandService commandService)
    {
        _next(context);

        if (context.Arguments is [ConsoleCommandCollection commands])
        {
            foreach (var command in commandService.Registry.GetAll())
            {
                commands.Add(command.Name);

                // Also register group-qualified names if they have a group
                if (command.Group.HasValue)
                {
                    commands.Add(command.Group.Value.FullName + " " + command.Name);
                }
            }
        }

        return null;
    }
}