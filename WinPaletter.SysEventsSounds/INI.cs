using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WinPaletter
{

    public class INI : ICloneable, IDisposable

    {
        public string path;
        private bool disposedValue;

        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public INI(string File)
        {
            path = File;
        }

        public void Write(string Section, string Key, string Value)
        {
            if (!System.IO.File.Exists(path)) { return; }
            WritePrivateProfileString(Section, Key, Value, path);
        }

        public string Read(string Section, string Key, string DefaultValue = "")
        {
            if (!System.IO.File.Exists(path)) { return DefaultValue; }
            StringBuilder SB = new(65535);
            GetPrivateProfileString(Section, Key, DefaultValue, SB, SB.Capacity, path);
            return SB.ToString();
        }

        public void DeleteSection(string Section)
        {
            WritePrivateProfileString(Section, null, null, path);
        }

        public void DeleteKey(string Section, string Key)
        {
            WritePrivateProfileString(Section, Key, null, path);
        }

        #region Clone support
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

        #region IDisposable Support
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

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}