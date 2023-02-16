using System;

namespace hrolgarUllr.ExtensionMethods
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the opposite value of a two-option enum.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <param name="value">The value to get the opposite of.</param>
        /// <returns>The opposite value of the enum.</returns>
        public static TEnum GetOpposite<TEnum>(this TEnum value) where TEnum : struct, Enum
        {
            if(!typeof(TEnum).IsEnum)
                throw new ArgumentException($"{typeof(TEnum)} is not an enum type.");
            
            var values = Enum.GetValues(typeof(TEnum));
            if(values.Length != 2)
                throw new ArgumentException($"{typeof(TEnum)} does not have exactly 2 values.");
            
            var first = values.GetValue(0);
            var second = values.GetValue(1);
            return value.Equals(first) ? (TEnum)second : (TEnum)first;
        }
        
        /// <summary>
        /// Returns a random value from an enum.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <returns>A random value from the enum.</returns>
        public static TEnum GetRandomValue<TEnum>(this TEnum value) where TEnum : struct, Enum
        {
            if(!typeof(TEnum).IsEnum)
                throw new ArgumentException($"{typeof(TEnum)} is not an enum type.");

            var values = Enum.GetValues(typeof(TEnum));
            return (TEnum)values.GetValue(UnityEngine.Random.Range(0, values.Length));
        }
    }
}
