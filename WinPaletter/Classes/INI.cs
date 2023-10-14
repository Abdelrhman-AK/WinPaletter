using System;
using System.Text;
using WinPaletter.NativeMethods;

namespace WinPaletter
{

    public class INI : IDisposable, ICloneable
    {
        public string path;
        private bool disposedValue;

        public INI(string File)
        {
            path = File;
        }

        public void Write(string Section, string Key, string Value)
        {
            Kernel32.WritePrivateProfileString(Section, Key, Value, path);
        }

        public string Read(string Section, string Key, string DefaultValue = null)
        {
            StringBuilder SB = new(65535);
            int i = Kernel32.GetPrivateProfileString(Section, Key, DefaultValue, SB, SB.Capacity, path);
            return SB.ToString();
        }

        public void DeleteSection(string Section)
        {
            Kernel32.WritePrivateProfileString(Section, null, null, path);
        }

        public void DeleteKey(string Section, string Key)
        {
            Kernel32.WritePrivateProfileString(Section, Key, null, path);
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

        // ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
        // Protected Overrides Sub Finalize()
        // ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        // Dispose(disposing:=False)
        // MyBase.Finalize()
        // End Sub

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}