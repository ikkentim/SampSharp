// SampSharp
// Copyright 2022 Tim Potze
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

namespace SampSharp.Entities;

internal class TimerInfo
{
    public TimerInfo(long intervalTicks, long nextTick, Action invoke, bool isActive)
    {
        IntervalTicks = intervalTicks;
        NextTick = nextTick;
        Invoke = invoke;
        IsActive = isActive;
    }

    public long IntervalTicks;
    public long NextTick;
    public Action Invoke;
    public bool IsActive;
    public TimerReference? Reference;
}