using System;
using System.Text;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    /// <summary>
    /// INI file reader and writer using native Windows API
    /// </summary>
    /// <remarks>
    /// Create a new instance of <see cref="INI"/>
    /// </remarks>
    /// <param name="File"></param>
    public class INI(string File) : ICloneable, IDisposable
    {
        /// <summary>
        /// Path to the INI file
        /// </summary>
        public string path = File;
        private bool disposedValue;

        /// <summary>
        /// Write a value to the INI file
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void Write(string Section, string Key, string Value)
        {
            if (!System.IO.File.Exists(path))
                System.IO.File.WriteAllText(path, string.Empty);

            Kernel32.WritePrivateProfileString(Section, Key, Value, path);
        }

        /// <summary>
        /// Read a value from the INI file
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public string Read(string Section, string Key, string DefaultValue = null)
        {
            StringBuilder SB = new(65535);
            int i = Kernel32.GetPrivateProfileString(Section, Key, DefaultValue, SB, SB.Capacity, path);
            return SB.ToString();
        }

        /// <summary>
        /// Delete a section from the INI file
        /// </summary>
        /// <param name="Section"></param>
        public void DeleteSection(string Section)
        {
            Kernel32.WritePrivateProfileString(Section, null, null, path);
        }

        /// <summary>
        /// Delete a key from the INI file
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        public void DeleteKey(string Section, string Key)
        {
            Kernel32.WritePrivateProfileString(Section, Key, null, path);
        }

        /// <summary>
        /// Clone the current instance of the INI file
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        #region IDisposable Support

        /// <summary>
        /// Dispose the INI instance
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~INI()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        /// <summary>
        /// Dispose the INI instance
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}