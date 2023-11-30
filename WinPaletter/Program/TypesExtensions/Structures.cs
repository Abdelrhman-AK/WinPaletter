using System;
using System.Reflection;

namespace WinPaletter.TypesExtensions
{
    public static class StructuresExtensions
    {
        public static bool StructureEquals<T>(this T obj1, T obj2)
        {
            Type type = typeof(T);

            // Get all fields and properties
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                var value1 = field.GetValue(obj1);
                var value2 = field.GetValue(obj2);

                if (!object.Equals(value1, value2))
                {
                    return false;
                }
            }

            foreach (var property in properties)
            {
                var value1 = property.GetValue(obj1);
                var value2 = property.GetValue(obj2);

                if (!object.Equals(value1, value2))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
