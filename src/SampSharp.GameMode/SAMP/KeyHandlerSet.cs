// SampSharp
// Copyright (C) 2014 Tim Potze
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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Contains a set of KeyHandlers
    /// </summary>
    public class KeyHandlerSet
    {
        private readonly Func<PlayerKeyStateChangedEventArgs, Keys, bool> _check;

        /// <summary>
        ///     Initializes a new instance of the KeyHandlerSet class.
        /// </summary>
        /// <param name="check">The check to run in Handle before calling an EventHandler.</param>
        public KeyHandlerSet(Func<PlayerKeyStateChangedEventArgs, Keys, bool> check)
        {
            _check = check;
        }

        /// <summary>
        ///     Occurs when the Keys.Action key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> Action;

        /// <summary>
        ///     Occurs when the Keys.Crouch key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> Crouch;

        /// <summary>
        ///     Occurs when the Keys.Fire key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> Fire;

        /// <summary>
        ///     Occurs when the Keys.Sprint key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> Sprint;

        /// <summary>
        ///     Occurs when the Keys.Attack key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> SecondaryAttack;

        /// <summary>
        ///     Occurs when the Keys.Jump key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> Jump;

        /// <summary>
        ///     Occurs when the Keys.Right key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> LookRight;

        /// <summary>
        ///     Occurs when the Keys.Handbrake key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> Handbrake;

        /// <summary>
        ///     Occurs when the Keys.Aim key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> Aim;

        /// <summary>
        ///     Occurs when the Keys.Left key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> LookLeft;

        /// <summary>
        ///     Occurs when the Keys.Submission or Keys.LookBehind key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> Submission;

        /// <summary>
        ///     Occurs when the Keys.LookBehind or Keys.Submission key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> LookBehind
        {
            add { Submission += value; }
            remove { Submission -= value; }
        }

        /// <summary>
        ///     Occurs when the Keys.Walk key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> Walk;

        /// <summary>
        ///     Occurs when the Keys.Up key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> AnalogUp;

        /// <summary>
        ///     Occurs when the Keys.Down key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> AnalogDown;

        /// <summary>
        ///     Occurs when the Keys.Left key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> AnalogLeft;

        /// <summary>
        ///     Occurs when the Keys.Right key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> AnalogRight;

        /// <summary>
        ///     Occurs when the Keys.Yes key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> Yes;

        /// <summary>
        ///     Occurs when the Keys.No key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> No;

        /// <summary>
        ///     Occurs when the Keys.CtrlBack key has been pressed.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> CtrlBack;

        /// <summary>
        ///     Handles a change in PlayerKeyState.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Object containing information about the event.</param>
        public void Handle(object sender, PlayerKeyStateChangedEventArgs e)
        {
            if (Action != null && _check(e, Keys.Action)) Action(sender, e);
            if (Crouch != null && _check(e, Keys.Crouch)) Crouch(sender, e);
            if (Fire != null && _check(e, Keys.Fire)) Fire(sender, e);
            if (Sprint != null && _check(e, Keys.Sprint)) Sprint(sender, e);
            if (SecondaryAttack != null && _check(e, Keys.SecondaryAttack)) SecondaryAttack(sender, e);
            if (Jump != null && _check(e, Keys.Jump)) Jump(sender, e);
            if (LookRight != null && _check(e, Keys.LookRight)) LookRight(sender, e);
            if (Handbrake != null && _check(e, Keys.Handbrake)) Handbrake(sender, e);
            if (Aim != null && _check(e, Keys.Aim)) Aim(sender, e);
            if (LookLeft != null && _check(e, Keys.LookLeft)) LookLeft(sender, e);
            if (Submission != null && _check(e, Keys.Submission)) Submission(sender, e);
            if (Walk != null && _check(e, Keys.Walk)) Walk(sender, e);
            if (AnalogUp != null && _check(e, Keys.AnalogUp)) AnalogUp(sender, e);
            if (AnalogDown != null && _check(e, Keys.AnalogDown)) AnalogDown(sender, e);
            if (AnalogLeft != null && _check(e, Keys.AnalogLeft)) AnalogLeft(sender, e);
            if (AnalogRight != null && _check(e, Keys.AnalogRight)) AnalogRight(sender, e);
            if (Yes != null && _check(e, Keys.Yes)) Yes(sender, e);
            if (No != null && _check(e, Keys.No)) No(sender, e);
            if (CtrlBack != null && _check(e, Keys.CtrlBack)) CtrlBack(sender, e);
        }
    }
}