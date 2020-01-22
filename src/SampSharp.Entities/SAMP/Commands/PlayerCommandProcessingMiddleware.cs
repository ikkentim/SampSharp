using SampSharp.Core.Logging;

namespace SampSharp.Entities.SAMP.Commands
{
    /// <summary>
    /// Represents a middleware which lets unhandled OnPlayerCommandText events be processed by the <see cref="IPlayerCommandService"/>.
    /// </summary>
    public class PlayerCommandProcessingMiddleware
    {
        private readonly EventDelegate _next;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerCommandProcessingMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next middleware handler.</param>
        public PlayerCommandProcessingMiddleware(EventDelegate next)
        {
            _next = next;
        }
        
        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        public object Invoke(EventContext context, IPlayerCommandService commandService)
        {
            var result = _next(context);

            if (EventHelper.IsSuccessResponse(result))
                return result;

            if (context.Arguments[0] is EntityId player &&
                context.Arguments[1] is string text)
                return commandService.Invoke(context.EventServices, player, text);

            CoreLog.Log(CoreLogLevel.Error, "Invalid command middleware input argument types!");
            return null;

        }
    }
}