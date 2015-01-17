// SampSharp
// Copyright (C) 2015 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System;
using System.Reflection;
using SampSharp.GameMode.Controllers;

namespace SampSharp.GameMode
{
    /// <summary>
    ///     Represents a SA-MP gamemode.
    /// </summary>
    public abstract partial class BaseMode : IDisposable
    {
        private readonly ControllerCollection _controllers = new ControllerCollection();

        #region Constructor

        /// <summary>
        ///     Initalizes a new instance of the BaseMode class.
        /// </summary>
        protected BaseMode()
        {
            Console.SetOut(new LogWriter());

            Type type = Type.GetType("Mono.Runtime");
            if (type != null)
            {
                MethodInfo displayName = type.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);
                if (displayName != null)
                    Console.WriteLine("[SampSharp] Detected mono version: {0}", displayName.Invoke(null, null));
            }

            RegisterControllers();
        }

        #endregion

        /// <summary>
        ///     Gets the collection of controllers loaded.
        /// </summary>
        protected virtual ControllerCollection Controllers
        {
            get { return _controllers; }
        }

        #region Methods

        private void RegisterControllers()
        {
            LoadControllers(_controllers);

            foreach (IController controller in _controllers)
            {
                var typeProvider = controller as ITypeProvider;
                var eventListener = controller as IEventListener;

                if (typeProvider != null)
                    typeProvider.RegisterTypes();

                if (eventListener != null)
                    eventListener.RegisterEvents(this);
            }
        }

        /// <summary>
        ///     Loads all default controllers into the given ControllerCollection.
        /// </summary>
        /// <param name="controllers">The collection to load the default controllers into.</param>
        protected virtual void LoadControllers(ControllerCollection controllers)
        {
            controllers.Add(new CommandController());
            controllers.Add(new DialogController());
            controllers.Add(new GlobalObjectController());
            controllers.Add(new MenuController());
            controllers.Add(new GtaPlayerController());
            controllers.Add(new PlayerObjectController());
            controllers.Add(new PlayerTextDrawController());
            controllers.Add(new TextDrawController());
            controllers.Add(new TimerController());
            controllers.Add(new DelayController());
            controllers.Add(new GtaVehicleController());
            controllers.Add(new SyncController());
            controllers.Add(new PickupController());
        }

        #endregion

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            _controllers.Dispose();
        }
    }
}