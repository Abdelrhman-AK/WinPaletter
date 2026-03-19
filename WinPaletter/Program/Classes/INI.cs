using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    /// <summary>
    /// INI file reader and writer using native Windows API.
    /// Uses the \\?\ path prefix to bypass the Windows INI file cache, which silently
    /// ignores writes to system-owned files even when the process is elevated.
    /// </summary>
    public class INI(string File) : ICloneable, IDisposable
    {
        /// <summary>
        /// Path to the INI file
        /// </summary>
        public string path = File;

        private bool disposedValue;

        // \\?\ prefix forces WritePrivateProfileString to bypass the Windows INI cache
        // and write directly to disk regardless of file ownership.
        private string UncPath => path.StartsWith(@"\\?\", StringComparison.Ordinal) ? path : @"\\?\" + path;

        /// <summary>
        /// Write a value to the INI file
        /// </summary>
        public void Write(string Section, string Key, string Value)
        {
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.WriteAllText(path, string.Empty);
                Program.Log?.Write(LogEventLevel.Information, $"INI file created as `{path}`");
            }

            Kernel32.WritePrivateProfileString(Section, Key, Value, UncPath);
            Program.Log?.Write(LogEventLevel.Information, $"INI write at `{path}` — [{Section}] {Key}={Value}");
        }

        /// <summary>
        /// Read a value from the INI file
        /// </summary>
        public string Read(string Section, string Key, string DefaultValue = null)
        {
            StringBuilder SB = new(65535);
            int i = Kernel32.GetPrivateProfileString(Section, Key, DefaultValue, SB, SB.Capacity, UncPath);

            if (i == 0)
            {
                Program.Log?.Write(LogEventLevel.Warning, $"INI read failed at `{path}` — [{Section}] {Key} not found, returning default.");
                return DefaultValue;
            }

            Program.Log?.Write(LogEventLevel.Information, $"INI read at `{path}` — [{Section}] {Key}={SB}");
            return SB.ToString();
        }

        /// <summary>
        /// Returns all section names in the INI file
        /// </summary>
        public string[] GetSections()
        {
            StringBuilder SB = new(65535);
            Kernel32.GetPrivateProfileString(null, null, null, SB, SB.Capacity, UncPath);
            return ParseDoubleNullTerminated(SB.ToString());
        }

        /// <summary>
        /// Returns all key names within a section
        /// </summary>
        public string[] GetKeys(string Section)
        {
            StringBuilder SB = new(65535);
            Kernel32.GetPrivateProfileString(Section, null, null, SB, SB.Capacity, UncPath);
            return ParseDoubleNullTerminated(SB.ToString());
        }

        /// <summary>
        /// Returns true if the specified section exists in the INI file
        /// </summary>
        public bool SectionExists(string Section)
        {
            return Array.IndexOf(GetSections(), Section) >= 0;
        }

        /// <summary>
        /// Returns true if the specified key exists within a section.
        /// Uses two distinct sentinel values to distinguish a missing key from a key whose value equals the sentinel.
        /// </summary>
        public bool KeyExists(string Section, string Key)
        {
            StringBuilder SB = new(65535);
            Kernel32.GetPrivateProfileString(Section, Key, "\x01", SB, SB.Capacity, UncPath);
            if (SB.ToString() != "\x01")
                return true;

            Kernel32.GetPrivateProfileString(Section, Key, "\x02", SB, SB.Capacity, UncPath);
            return SB.ToString() != "\x02";
        }

        /// <summary>
        /// Reads all key-value pairs in a section as a dictionary
        /// </summary>
        public Dictionary<string, string> ReadSection(string Section)
        {
            Dictionary<string, string> result = new(StringComparer.OrdinalIgnoreCase);

            foreach (string key in GetKeys(Section))
                result[key] = Read(Section, key, string.Empty);

            return result;
        }

        /// <summary>
        /// Copies all key-value pairs from one section to another within the same file
        /// </summary>
        public void CopySection(string SourceSection, string DestSection)
        {
            foreach (KeyValuePair<string, string> kvp in ReadSection(SourceSection))
                Write(DestSection, kvp.Key, kvp.Value);
        }

        /// <summary>
        /// Renames a section by copying all its keys to a new section name and deleting the original
        /// </summary>
        public void RenameSection(string OldSection, string NewSection)
        {
            CopySection(OldSection, NewSection);
            DeleteSection(OldSection);
        }

        /// <summary>
        /// Renames a key within a section
        /// </summary>
        public void RenameKey(string Section, string OldKey, string NewKey)
        {
            string value = Read(Section, OldKey);
            if (value is null)
                return;

            Write(Section, NewKey, value);
            DeleteKey(Section, OldKey);
        }

        /// <summary>
        /// Delete a section from the INI file
        /// </summary>
        public void DeleteSection(string Section)
        {
            Kernel32.WritePrivateProfileString(Section, null, null, UncPath);
            Program.Log?.Write(LogEventLevel.Information, $"INI section deleted at `{path}` — [{Section}]");
        }

        /// <summary>
        /// Delete a key from the INI file
        /// </summary>
        public void DeleteKey(string Section, string Key)
        {
            Kernel32.WritePrivateProfileString(Section, Key, null, UncPath);
            Program.Log?.Write(LogEventLevel.Information, $"INI key deleted at `{path}` — [{Section}] {Key}");
        }

        // GetPrivateProfileString with a null key returns all key names in a section,
        // or all section names when section is also null, as a double-null-terminated
        // buffer: "entry1\0entry2\0\0". StringBuilder collapses embedded nulls so we
        // split on \0 and drop empty entries produced by the trailing double-null.
        private static string[] ParseDoubleNullTerminated(string raw)
        {
            string[] parts = raw.Split('\0');
            List<string> result = new(parts.Length);

            foreach (string part in parts)
            {
                if (part.Length > 0)
                    result.Add(part);
            }

            return result.ToArray();
        }

        /// <summary>
        /// Clone the current instance of the INI file
        /// </summary>
        public object Clone() => MemberwiseClone();

        #region IDisposable Support

        /// <summary>
        /// Dispose the INI instance
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) { }
                disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose the INI instance
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}