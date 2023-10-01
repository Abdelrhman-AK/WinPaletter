using System;
using System.Text;
using static WinPaletter.NativeMethods.Kernel32;

namespace WinPaletter
{

    public class INI : IDisposable
    {

        public string path;
        private bool disposedValue;

        public INI(string INIPath)
        {
            path = INIPath;
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        public string IniReadValue(string Section, string Key, string DefaultValue = null)
        {
            var temp = new StringBuilder(65535);
            int i = GetPrivateProfileString(Section, Key, DefaultValue, temp, temp.Capacity, path);
            return temp.ToString();
        }

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
    }
}