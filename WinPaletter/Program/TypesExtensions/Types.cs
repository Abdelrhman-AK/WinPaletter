using System;
using System.Reflection;

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

        /// <summary>
        /// Copies all field values from the specified source object to the target object.
        /// </summary>
        /// <remarks>This method performs a shallow copy of all fields, including private and non-public
        /// fields, from the source object to the target object. Both objects must be of the same type.</remarks>
        /// <typeparam name="T">The type of the objects involved in the operation.</typeparam>
        /// <param name="target">The object to which field values will be copied. Cannot be <see langword="null"/>.</param>
        /// <param name="source">The object from which field values will be copied. Cannot be <see langword="null"/>.</param>
        public static void CopyFrom<T>(this T target, T source)
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
                field.SetValue(target, field.GetValue(source));
        }
    }
}
