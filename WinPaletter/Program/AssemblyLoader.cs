using System;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

namespace WinPaletter
{
    internal partial class Program
    {
        private static Assembly DomainCheck(object sender, ResolveEventArgs e)
        {
            return GetAssemblyFromZIP(e.Name);
        }

        public static Assembly GetAssemblyFromZIP(string Name)
        {
            Name = new AssemblyName(Name).Name;

            if (Name.StartsWith("WinPaletter.resources", StringComparison.OrdinalIgnoreCase))
                return null;

            byte[] b = null;

            using (System.IO.MemoryStream ms = new(Properties.Resources.Assemblies))
            {
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
            }

            if (b is not null)
            {
                return Assembly.Load(b);
            }
            else
            {
                return null;
            }
        }
    }
}