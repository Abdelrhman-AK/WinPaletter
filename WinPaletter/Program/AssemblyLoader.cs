using System;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

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

        static bool IsLoggerAssemblyLoaded =>
            AppDomain.CurrentDomain.GetAssemblies()
            .Any(a =>
                string.Equals(a.GetName().Name, "Serilog.Sinks.File", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(a.GetName().Name, "Serilog", StringComparison.OrdinalIgnoreCase));

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

            using (System.IO.MemoryStream ms = new(Properties.Resources.Assemblies))
            using (ZipArchive zip = new(ms))
            {
                if (zip.Entries.Any(entry => entry.Name.EndsWith($"{Name}.dll", StringComparison.OrdinalIgnoreCase)))
                {
                    using (System.IO.MemoryStream _as = new())
                    {
                        zip.GetEntry($"{Name}.dll").Open().CopyTo(_as);
                        _as.Seek(0L, System.IO.SeekOrigin.Begin);
                        b = _as.ToArray();
                    }
                }
            }

            return b is not null ? Assembly.Load(b) : null;
        }

    }
}