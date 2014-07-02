using System;

namespace SampSharp.GameMode
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ParameterLengthAttribute : Attribute
    {
        public ParameterLengthAttribute(int index)
        {
            Index = index;
        }

        public int Index { get; private set; }
    }
}
