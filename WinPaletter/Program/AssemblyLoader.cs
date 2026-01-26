using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using WinPaletter.Properties;

namespace WinPaletter
{
    internal partial class Program
    {
        private static ConcurrentDictionary<string, Assembly> _assemblyCache = new(StringComparer.OrdinalIgnoreCase);
        private static object _syncLock = new();

        /// <summary>
        /// Resolves assembly loading from embedded compressed resources
        /// </summary>
        private static Assembly DomainCheck(object sender, ResolveEventArgs e)
        {
            try
            {
                return GetAssemblyFromZIP(e.Name);
            }
            catch (Exception ex)
            {
                // Consider logging this exception
                Debug.WriteLine($"Failed to resolve assembly {e.Name}: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Get the assembly from the ZIP archive from the embedded resources
        /// </summary>
        /// <param name="assemblyName">The name of the assembly to load</param>
        /// <returns>The loaded assembly or null if not found</returns>
        public static Assembly GetAssemblyFromZIP(string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName)) return null;

            try
            {
                var name = new AssemblyName(assemblyName).Name;

                // Early return for resource assemblies
                if (name.StartsWith("WinPaletter.resources", StringComparison.OrdinalIgnoreCase)) return null;

                _assemblyCache ??= new();
                _syncLock ??= new();

                // Check cache first
                if (_assemblyCache.TryGetValue(name, out var cachedAssembly)) return cachedAssembly;

                // Check already loaded assemblies
                var loadedAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => string.Equals(a.GetName().Name, name, StringComparison.OrdinalIgnoreCase));

                if (loadedAssembly != null)
                {
                    _assemblyCache.TryAdd(name, loadedAssembly);
                    return loadedAssembly;
                }

                // Load from embedded resources
                lock (_syncLock)
                {
                    // Double-check cache after acquiring lock
                    if (_assemblyCache.TryGetValue(name, out cachedAssembly)) return cachedAssembly;

                    using var ms = new MemoryStream(Resources.Assemblies);
                    using var zip = new ZipArchive(ms, ZipArchiveMode.Read, false);

                    var entry = zip.Entries.FirstOrDefault(e => e.Name.EndsWith($"{name}.dll", StringComparison.OrdinalIgnoreCase));

                    if (entry == null) return null;

                    using var entryStream = entry.Open();
                    using var assemblyStream = new MemoryStream();

                    entryStream.CopyTo(assemblyStream);
                    var assemblyBytes = assemblyStream.ToArray();

                    if (assemblyBytes.Length == 0) return null;

                    var assembly = Assembly.Load(assemblyBytes);

                    // Cache the loaded assembly
                    _assemblyCache.TryAdd(name, assembly);

                    return assembly;
                }
            }
            catch (BadImageFormatException)
            {
                // This occurs when trying to load a non-.NET assembly
                return null;
            }
            catch (IOException ex)
            {
                // Log this exception if you have logging infrastructure
                Debug.WriteLine($"IO Error loading assembly {assemblyName}: {ex.Message}");
                return null;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidDataException || ex is NotSupportedException)
            {
                // Handle specific ZIP-related exceptions
                Debug.WriteLine($"Error loading assembly {assemblyName}: {ex.Message}");
                return null;
            }
        }
    }
}