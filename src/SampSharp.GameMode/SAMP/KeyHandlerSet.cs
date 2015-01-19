// SampSharp
// Copyright 2015 Tim Potze
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

using System;
using System.Dynamic;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Tools;

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

            Action = new PriorityKeyHandler();
            Crouch = new PriorityKeyHandler();
            Fire = new PriorityKeyHandler();
            Sprint = new PriorityKeyHandler();
            SecondaryAttack = new PriorityKeyHandler();
            Jump = new PriorityKeyHandler();
            LookRight = new PriorityKeyHandler();
            Handbrake = new PriorityKeyHandler();
            Aim = new PriorityKeyHandler();
            LookLeft = new PriorityKeyHandler();
            Submission = new PriorityKeyHandler();
            Walk = new PriorityKeyHandler();
            AnalogUp = new PriorityKeyHandler();
            AnalogDown = new PriorityKeyHandler();
            AnalogLeft = new PriorityKeyHandler();
            AnalogRight = new PriorityKeyHandler();
            Yes = new PriorityKeyHandler();
            No = new PriorityKeyHandler();
            CtrlBack = new PriorityKeyHandler();
        }

        /// <summary>
        ///     Occurs when the Keys.Action key has been pressed.
        /// </summary>
        public PriorityKeyHandler Action { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.Crouch key has been pressed.
        /// </summary>
        public PriorityKeyHandler Crouch { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.Fire key has been pressed.
        /// </summary>
        public PriorityKeyHandler Fire { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.Sprint key has been pressed.
        /// </summary>
        public PriorityKeyHandler Sprint { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.Attack key has been pressed.
        /// </summary>
        public PriorityKeyHandler SecondaryAttack { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.Jump key has been pressed.
        /// </summary>
        public PriorityKeyHandler Jump { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.Right key has been pressed.
        /// </summary>
        public PriorityKeyHandler LookRight { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.Handbrake key has been pressed.
        /// </summary>
        public PriorityKeyHandler Handbrake { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.Aim key has been pressed.
        /// </summary>
        public PriorityKeyHandler Aim { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.Left key has been pressed.
        /// </summary>
        public PriorityKeyHandler LookLeft { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.Submission or Keys.LookBehind key has been pressed.
        /// </summary>
        public PriorityKeyHandler Submission { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.LookBehind or Keys.Submission key has been pressed.
        /// </summary>
        public PriorityKeyHandler LookBehind
        {
            get { return Submission; }
        }

        /// <summary>
        ///     Occurs when the Keys.Walk key has been pressed.
        /// </summary>
        public PriorityKeyHandler Walk { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.Up key has been pressed.
        /// </summary>
        public PriorityKeyHandler AnalogUp { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.Down key has been pressed.
        /// </summary>
        public PriorityKeyHandler AnalogDown { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.Left key has been pressed.
        /// </summary>
        public PriorityKeyHandler AnalogLeft { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.Right key has been pressed.
        /// </summary>
        public PriorityKeyHandler AnalogRight { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.Yes key has been pressed.
        /// </summary>
        public PriorityKeyHandler Yes { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.No key has been pressed.
        /// </summary>
        public PriorityKeyHandler No { get; private set; }

        /// <summary>
        ///     Occurs when the Keys.CtrlBack key has been pressed.
        /// </summary>
        public PriorityKeyHandler CtrlBack { get; private set; }

        /// <summary>
        ///     Handles a change in PlayerKeyState.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Object containing information about the event.</param>
        public void Handle(object sender, PlayerKeyStateChangedEventArgs e)
        {
            if (Action != null && _check(e, Keys.Action)) Action.Handle(sender, e);
            if (Crouch != null && _check(e, Keys.Crouch)) Crouch.Handle(sender, e);
            if (Fire != null && _check(e, Keys.Fire)) Fire.Handle(sender, e);
            if (Sprint != null && _check(e, Keys.Sprint)) Sprint.Handle(sender, e);
            if (SecondaryAttack != null && _check(e, Keys.SecondaryAttack)) SecondaryAttack.Handle(sender, e);
            if (Jump != null && _check(e, Keys.Jump)) Jump.Handle(sender, e);
            if (LookRight != null && _check(e, Keys.LookRight)) LookRight.Handle(sender, e);
            if (Handbrake != null && _check(e, Keys.Handbrake)) Handbrake.Handle(sender, e);
            if (Aim != null && _check(e, Keys.Aim)) Aim.Handle(sender, e);
            if (LookLeft != null && _check(e, Keys.LookLeft)) LookLeft.Handle(sender, e);
            if (Submission != null && _check(e, Keys.Submission)) Submission.Handle(sender, e);
            if (Walk != null && _check(e, Keys.Walk)) Walk.Handle(sender, e);
            if (AnalogUp != null && _check(e, Keys.AnalogUp)) AnalogUp.Handle(sender, e);
            if (AnalogDown != null && _check(e, Keys.AnalogDown)) AnalogDown.Handle(sender, e);
            if (AnalogLeft != null && _check(e, Keys.AnalogLeft)) AnalogLeft.Handle(sender, e);
            if (AnalogRight != null && _check(e, Keys.AnalogRight)) AnalogRight.Handle(sender, e);
            if (Yes != null && _check(e, Keys.Yes)) Yes.Handle(sender, e);
            if (No != null && _check(e, Keys.No)) No.Handle(sender, e);
            if (CtrlBack != null && _check(e, Keys.CtrlBack)) CtrlBack.Handle(sender, e);
        }
    }

    public class PriorityKeyHandler
    {
        public event EventHandler<PlayerCancelableEventArgs> HighPriority;
        public event EventHandler<PlayerCancelableEventArgs> NormalPriority;
        public event EventHandler<PlayerCancelableEventArgs> LowPriority;

        public void Handle(object sender, PlayerKeyStateChangedEventArgs e)
        {
            var args = new PlayerCancelableEventArgs(e.PlayerId);

            OnHighPriority(args);

            if (!args.Canceled)
                OnNormalPriority(args);

            if (!args.Canceled)
                OnLowPriority(args);
        }

        protected virtual void OnHighPriority(PlayerCancelableEventArgs e)
        {
            if (HighPriority != null)
                HighPriority(this, e);
        }

        protected virtual void OnNormalPriority(PlayerCancelableEventArgs e)
        {
            if (NormalPriority != null)
                NormalPriority(this, e);
        }

        protected virtual void OnLowPriority(PlayerCancelableEventArgs e)
        {
            if (LowPriority != null)
                LowPriority(this, e);
        }
    }
}