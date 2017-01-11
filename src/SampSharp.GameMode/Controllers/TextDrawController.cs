// SampSharp
// Copyright 2017 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Tools;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all textdraw actions.
    /// </summary>
    [Controller]
    public class TextDrawController : Disposable, IEventListener, ITypeProvider
    {
        /// <summary>
        ///     Registers the events this TextDrawController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            gameMode.PlayerClickTextDraw += (sender, args) => args.TextDraw?.OnClick(args);
        }

        /// <summary>
        ///     Registers types this TextDrawController requires the system to use.
        /// </summary>
        public virtual void RegisterTypes()
        {
            TextDraw.Register<TextDraw>();
        }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var td in PlayerTextDraw.All)
                {
                    td.Dispose();
                }
            }
        }
    }
}