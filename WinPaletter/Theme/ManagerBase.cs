using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Provides a base class for manager types, offering common functionality such as equality comparison, cloning, and
    /// resource disposal. This class is designed to be inherited by specific manager implementations.
    /// </summary>
    /// <remarks>The <see cref="ManagerBase{T}"/> class includes the following features: <list type="bullet">
    /// <item> <description>Equality comparison based on the serialized JSON representation of objects, ensuring deep
    /// structural equality.</description> </item> <item> <description>Support for cloning objects using deep copy
    /// semantics.</description> </item> <item> <description>Resource cleanup through the <see cref="IDisposable"/>
    /// interface.</description> </item> </list> Derived classes should implement additional functionality specific to
    /// their domain while leveraging the common features provided by this base class.</remarks>
    /// <typeparam name="T">The type of the derived manager class. This ensures that the base class can provide type-safe operations
    /// specific to the derived type.</typeparam>
    public abstract class ManagerBase<T> : ICloneable, IEquatable<T>, IDisposable where T : ManagerBase<T>
    {
        #region Equality

        /// <summary>
        /// Compares two objects for equality based on their serialized JSON representation.
        /// </summary>
        /// <remarks>This method performs a deep comparison of the two objects by serializing them into
        /// JSON and comparing the resulting JSON structures. It handles circular references and preserves object
        /// references during serialization. The comparison ignores JSON metadata such as `$id` and `$ref` to ensure
        /// consistent results.</remarks>
        /// <param name="obj1">The first object to compare. Can be <see langword="null"/>.</param>
        /// <param name="obj2">The second object to compare. Can be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the objects are considered equal based on their JSON representation; otherwise,
        /// <see langword="false"/>.</returns>
        private static bool Compare(object obj1, object obj2)
        {
            if (ReferenceEquals(obj1, obj2)) return true;

            if (obj1 is null || obj2 is null) return false;

            if (obj1.GetType() != obj2.GetType()) return false;

            JsonSerializerSettings settings = new()
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Formatting = Formatting.None
            };

            JsonSerializer serializer = JsonSerializer.Create(settings);

            JToken j1 = JToken.FromObject(obj1, serializer);
            JToken j2 = JToken.FromObject(obj2, serializer);

            // Remove $id and $ref to avoid circular reference issues
            RemoveIds(j1);
            RemoveIds(j2);

            // Compare the cleaned JTokens
            return JToken.DeepEquals(j1, j2);
        }

        /// <summary>
        /// Recursively removes the "$id" and "$ref" properties from a JSON token and its descendants.
        /// </summary>
        /// <remarks>This method traverses the JSON structure recursively. If the token is a JSON object,
        /// it removes the "$id"  and "$ref" properties from the object and processes its properties. If the token is a
        /// JSON array, it processes  each item in the array. Other token types are ignored.</remarks>
        /// <param name="token">The JSON token to process. This can be an object, array, or other JSON structure.</param>
        private static void RemoveIds(JToken token)
        {
            if (token is JObject obj)
            {
                obj.Remove("$id");
                obj.Remove("$ref");

                foreach (var property in obj.Properties().ToList()) RemoveIds(property.Value);
            }
            else if (token is JArray array)
            {
                foreach (var item in array) RemoveIds(item);
            }
        }

        /// <summary>
        /// Determines whether the current instance is equal to the specified object.
        /// </summary>
        /// <remarks>The equality comparison is performed using the <c>Compare</c> method. If <paramref
        /// name="other"/> is <see langword="null"/>, this method returns <see langword="false"/>.</remarks>
        /// <param name="other">The object to compare with the current instance. Can be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the current instance is equal to <paramref name="other"/>; otherwise, <see
        /// langword="false"/>.</returns>
        public bool Equals(T other)
        {
            if (other is null) return false;
            return Compare(this, other);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <remarks>This method performs a reference equality check first, followed by a type comparison,
        /// and then delegates to a comparison method  to determine equality. If the specified object is <see
        /// langword="null"/>, this method returns <see langword="false"/>.</remarks>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see
        /// langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null) return false;
            if (obj.GetType() != GetType()) return false;

            return Compare(this, obj);
        }

        /// <summary>
        /// Computes a hash code for the current object based on its JSON structural representation.
        /// </summary>
        /// <remarks>This method ensures that the hash code is consistent with the equality comparison
        /// defined by  the <see cref="Equals(object)"/> method. The hash code is derived from a JSON representation  of
        /// the object, which includes type information, reference handling, and serialization settings  to ensure deep
        /// consistency.</remarks>
        /// <returns>An integer that represents the hash code for the current object.</returns>
        public override int GetHashCode()
        {
            // Ensures that == and Equals remain consistent.
            // Uses JSON structural hash for deep consistency.
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Formatting = Formatting.None
            };

            var serializer = JsonSerializer.Create(settings);
            var jToken = JToken.FromObject(this, serializer);
            return jToken.ToString(Formatting.None).GetHashCode();
        }

        /// <summary>
        /// Determines whether two <see cref="ManagerBase{T}"/> instances are equal.
        /// </summary>
        /// <remarks>Two instances are considered equal if they are the same reference or if the <see
        /// cref="Equals(object?)"/> method  returns <see langword="true"/> for the comparison of the two objects. If
        /// both instances are <see langword="null"/>,  they are also considered equal.</remarks>
        /// <param name="left">The first <see cref="ManagerBase{T}"/> instance to compare, or <see langword="null"/>.</param>
        /// <param name="right">The second <see cref="ManagerBase{T}"/> instance to compare, or <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the two instances are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(ManagerBase<T> left, ManagerBase<T> right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two <see cref="ManagerBase{T}"/> instances are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="ManagerBase{T}"/> instance to compare, or <see langword="null"/>.</param>
        /// <param name="right">The second <see cref="ManagerBase{T}"/> instance to compare, or <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the two instances are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(ManagerBase<T>? left, ManagerBase<T>? right) => !(left == right);

        #endregion

        #region Clone and Dispose

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <remarks>This method provides a deep or shallow copy of the current instance, depending on the
        /// implementation. Callers should refer to the specific type's documentation to understand the cloning
        /// behavior.</remarks>
        /// <returns>A new object that is a copy of this instance.</returns>
        object ICloneable.Clone()
        {
            return Clone();
        }

        /// <summary>
        /// Creates a deep copy of the current object.
        /// </summary>
        /// <remarks>The cloned object is a new instance with the same state as the original object. 
        /// Changes made to the cloned object will not affect the original object, and vice versa.</remarks>
        /// <returns>A deep copy of the current object.</returns>
        public T Clone()
        {
            return FastCloner.FastCloner.DeepClone(this) as T;
        }

        private bool _disposed;

        /// <summary>
        /// Releases all resources used by the current instance of the class.
        /// </summary>
        /// <remarks>
        /// This method clears any cached data used by the instance and suppresses finalization
        /// to optimize garbage collection. Call this method when the instance is no longer needed
        /// to free up resources promptly.
        /// </remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing">True to release managed resources as well as unmanaged; false only for unmanaged.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                FastCloner.FastCloner.ClearCache();
            }

            // Free unmanaged resources (if any are implemented futurely)

            _disposed = true;
        }

        /// <summary>
        /// Finalizer (only if unmanaged resources exist).
        /// </summary>
        ~ManagerBase()
        {
            Dispose(false);
        }

        #endregion

        #region Others

        /// <summary>
        /// Returns a JSON string representation of the current object.
        /// </summary>
        /// <remarks>The JSON string is generated using the <see
        /// cref="JsonConvert.SerializeObject(object)"/> method. This representation includes all public properties of
        /// the object.</remarks>
        /// <returns>A JSON string that represents the current object.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion
    }
}