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
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (FieldInfo field in fields)
            {
                object value1 = field.GetValue(obj1);
                object value2 = field.GetValue(obj2);

                if (!object.Equals(value1, value2))
                {
                    return false;
                }
            }

            foreach (PropertyInfo property in properties)
            {
                object value1 = property.GetValue(obj1);
                object value2 = property.GetValue(obj2);

                if (!object.Equals(value1, value2))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
