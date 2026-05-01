using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities;

/// <summary>
/// Provides extension methods for safely adding event handlers to components within a <see cref="SampSharpEnvironment" />.
/// </summary>
public static class SafeEventHandlerSampSharpEnvironmentExtensions
{
    extension(SampSharpEnvironment environment)
    {
        /// <summary>
        /// Attempts to add an event handler to the specified component.
        /// </summary>
        /// <typeparam name="TComponent">The type of the component to which the event handler will be added.</typeparam>
        /// <typeparam name="TEventHandler">The type of the event handler.</typeparam>
        /// <param name="dispatcherProvider">A function that provides the event dispatcher for the specified component.</param>
        /// <param name="handler">The event handler instance to add to the component.</param>
        /// <param name="priority">The priority with which the event handler should be registered. The default is <see cref="EventPriority.Default" />.</param>
        /// <returns>An <see cref="IDisposable" /> representing the event handler registration, or <see langword="null" /> if the registration failed.</returns>
        public IDisposable? TryAddEventHandler<TComponent, TEventHandler>(Func<TComponent, IEventDispatcher<TEventHandler>> dispatcherProvider, TEventHandler handler, EventPriority priority = EventPriority.Default)
            where TComponent : unmanaged, IComponent.IManagedInterface
            where TEventHandler : class, IEventHandler<TEventHandler>
        {
            var component = environment.Components.QueryComponent<TComponent>();

            if (!component.HasValue)
            {
                return null;
            }

            var dispatcher = dispatcherProvider(component);

            if (!dispatcher.HasValue)
            {
                return null;
            }

            if (!dispatcher.AddEventHandler(handler, priority))
            {
                return null;
            }

            return new SafeEventHandlerRegistration<TComponent, TEventHandler>(environment, handler, dispatcherProvider);
        }

        /// <summary>
        /// Adds an event handler to the specified component.
        /// </summary>
        /// <typeparam name="TComponent">The type of the component to which the event handler will be added.</typeparam>
        /// <typeparam name="TEventHandler">The type of the event handler.</typeparam>
        /// <param name="dispatcherProvider">A function that provides the event dispatcher from the specified <paramref name="TComponent" />.</param>
        /// <param name="handler">The event handler instance to add to the component.</param>
        /// <param name="priority">The priority with which the event handler should be registered. The default is <see cref="EventPriority.Default" />.</param>
        /// <returns>An <see cref="IDisposable" /> representing the event handler registration.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the component does not exist, the dispatcher cannot be retrieved, or the event handler could not be added.</exception>
        public IDisposable AddEventHandler<TComponent, TEventHandler>(Func<TComponent, IEventDispatcher<TEventHandler>> dispatcherProvider, TEventHandler handler, EventPriority priority = EventPriority.Default)
            where TComponent : unmanaged, IComponent.IManagedInterface
            where TEventHandler : class, IEventHandler<TEventHandler>
        {
            var registration = TryAddEventHandler(environment, dispatcherProvider, handler, priority);
            if (registration is null)
            {
                throw new InvalidOperationException("Failed to add event handler.");
            }

            return registration;
        }

        /// <summary>
        /// Attempts to add an event handler to the core.
        /// </summary>
        /// <typeparam name="TEventHandler">The type of the event handler.</typeparam>
        /// <param name="dispatcherProvider">A function that provides the event dispatcher from the <see cref="ICore" />.</param>
        /// <param name="handler">The event handler instance to add to the core.</param>
        /// <param name="priority">The priority with which the event handler should be registered. The default is <see cref="EventPriority.Default" />.</param>
        /// <returns>An <see cref="IDisposable" /> representing the event handler registration, or <see langword="null" /> if the registration failed.</returns>
        public IDisposable? TryAddEventHandler<TEventHandler>(Func<ICore, IEventDispatcher<TEventHandler>> dispatcherProvider, TEventHandler handler, EventPriority priority = EventPriority.Default)
            where TEventHandler : class, IEventHandler<TEventHandler>
        {
            if (!environment.Core.HasValue)
            {
                return null;
            }

            var dispatcher = dispatcherProvider(environment.Core);

            if (!dispatcher.HasValue)
            {
                return null;
            }

            if (!dispatcher.AddEventHandler(handler, priority))
            {
                return null;
            }

            return new SafeEventHandlerRegistration<TEventHandler>(environment, handler, dispatcherProvider);
        }

        /// <summary>
        /// Adds an event handler to the core.
        /// </summary>
        /// <typeparam name="TEventHandler">The type of the event handler.</typeparam>
        /// <param name="dispatcherProvider">A function that provides the event dispatcher from the <see cref="ICore" />.</param>
        /// <param name="handler">The event handler instance to add to the core.</param>
        /// <param name="priority">The priority with which the event handler should be registered. The default is <see cref="EventPriority.Default" />.</param>
        /// <returns>An <see cref="IDisposable" /> representing the event handler registration.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the core does not exist, the dispatcher cannot be retrieved, or the event handler could not be added.</exception>
        public IDisposable AddEventHandler<TEventHandler>(Func<ICore, IEventDispatcher<TEventHandler>> dispatcherProvider, TEventHandler handler, EventPriority priority = EventPriority.Default)
            where TEventHandler : class, IEventHandler<TEventHandler>
        {
            var registration = TryAddEventHandler(environment, dispatcherProvider, handler, priority);
            if (registration is null)
            {
                throw new InvalidOperationException("Failed to add event handler.");
            }

            return registration;
        }
    }
}