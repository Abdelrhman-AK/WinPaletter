using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using WinPaletter.Properties;

namespace WinPaletter
{
    internal partial class Program
    {
        /// <summary>
        /// Load the assembly from the embedded compressed resources
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private static Assembly DomainCheck(object sender, ResolveEventArgs e)
        {
            return GetAssemblyFromZIP(e.Name);
        }

        /// <summary>
        /// Get the assembly from the ZIP archive from the embedded resources
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static Assembly GetAssemblyFromZIP(string Name)
        {
            Name = new AssemblyName(Name).Name;

            if (Name.StartsWith("WinPaletter.resources", StringComparison.OrdinalIgnoreCase)) return null;

            // Check if already loaded
            Assembly loadedAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => string.Equals(a.GetName().Name, Name, StringComparison.OrdinalIgnoreCase));

            if (loadedAssembly != null) { return loadedAssembly; }

            byte[] b = null;

            using (MemoryStream ms = new(Resources.Assemblies))
            using (ZipArchive zip = new(ms))
            {
                if (zip.Entries.Any(entry => entry.Name.EndsWith($"{Name}.dll", StringComparison.OrdinalIgnoreCase)))
                {
                    using (MemoryStream _as = new())
                    {
                        zip.GetEntry($"{Name}.dll").Open().CopyTo(_as);
                        _as.Seek(0L, SeekOrigin.Begin);
                        b = _as.ToArray();
                    }
                }
            }

            return b is not null ? Assembly.Load(b) : null;
        }

    }
}