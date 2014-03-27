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

namespace GameMode.Definitions
{
    [Flags]
    public enum Keys
    {
        Action = 1,
        Crouch = 2,
        Fire = 4,
        Sprint = 8,
        SecondaryAttack = 16,
        Jump = 32,
        LookRight = 64,
        Handbrake = 128,
        LookLeft = 256,
        Submission = 512,
        LookBehind = 512,
        Walk = 1024,
        AnalogUp = 2048,
        AnalogDown = 4096,
        AnalogLeft = 8192,
        AnalogRight = 16384,
        Yes = 65536,
        No = 131072,
        CtrlBack = 262144,
        Up = -128,
        Down = 128,
        Left = -128,
        Right = 128
    }
}