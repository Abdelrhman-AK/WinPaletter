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

            if (Name.StartsWith("WinPaletter.resources", (StringComparison)5))
                return null;

            byte[] b = null;

            using (var ms = new System.IO.MemoryStream(Properties.Resources.Assemblies))
            {
                using (var zip = new ZipArchive(ms))
                {
                    if (zip.Entries.Any(entry => entry.Name.EndsWith(Name + ".dll", (StringComparison)5)))
                    {
                        using (var _as = new System.IO.MemoryStream())
                        {
                            zip.GetEntry(Name + ".dll").Open().CopyTo(_as);
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