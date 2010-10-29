using System;

namespace AutoConfig
{
    /// <summary>
    /// Validates method call arguments.
    /// </summary>
    static class ArgumentValidator
    {
        /// <summary>
        /// Trows an ArgumentNullException if the argument is null.
        /// </summary>
        /// <typeparam name="TArgument">Argument type.</typeparam>
        /// <param name="name">Argument name.</param>
        /// <param name="value">Argument value.</param>
        public static void IsNotNull<TArgument>(string name, TArgument value) where TArgument: class
        {
            if (value == null)
                throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Throws an ArgumentException if the argument is empty.
        /// </summary>
        /// <param name="name">Argument name.</param>
        /// <param name="value">Argument value.</param>
        private static void IsNotEmpty(string name, string value)
        {
            if (value.Length == 0)
                throw new ArgumentException(Resources.ArgumentIsNotEmptyMessage, name);
        }

        /// <summary>
        /// Throws an ArgumentNullException or an ArgumentException if the argument is null or empty.
        /// </summary>
        /// <param name="name">Argument name.</param>
        /// <param name="value">Argument value.</param>
        public static void IsNotNullAndNotEmpty(string name, string value)
        {
            IsNotNull(name, value);
            IsNotEmpty(name, value);
        }
    }
}
