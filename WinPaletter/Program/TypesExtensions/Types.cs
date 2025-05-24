using System;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extensions for types
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks if this type is a structure or not
        /// </summary>
        public static bool IsStructure(this Type type)
        {
            return type.IsValueType && !type.IsPrimitive && type.Namespace is not null && !type.Namespace.StartsWith("System.");
        }
    }
}
