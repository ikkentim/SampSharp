using System;

namespace SampSharp.GameMode.Natives
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NativeAttribute : Attribute
    {
        public NativeAttribute(string name) : this(name, null)
        {
        }

        public NativeAttribute(string name, params int[] sizes)
        {
            if (name == null) throw new ArgumentNullException("name");
            Name = name;
            Sizes = sizes;
        }

        public string Name { get; private set; }
        public int[] Sizes { get; private set; }
    }
}