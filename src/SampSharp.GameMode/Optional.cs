using System;

namespace SampSharp.GameMode
{
    
    /// <summary>
    ///     
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Optional<T> where T : class
    {
        private readonly T _value;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Optional(T value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
            HasValue = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nullableInstance"></param>
        /// <returns></returns>
        public static Optional<T> From(T nullableInstance)
        {
            return new Optional<T>(nullableInstance);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasValue { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public T Value
        {
            get
            {
                if(!HasValue){throw new InvalidOperationException();}
                return _value;
            }
        }
    }
}