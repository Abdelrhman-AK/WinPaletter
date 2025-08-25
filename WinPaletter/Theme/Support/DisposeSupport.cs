using Serilog.Events;
using System;

namespace WinPaletter.Theme
{
    public partial class Manager : IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// Dispose WinPaletter theme to free up memory
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
            }
            disposedValue = true;
        }


        /// <summary>
        /// Dispose WinPaletter theme to free up memory
        /// </summary>
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
            Program.Log?.Write(LogEventLevel.Debug, "Disposing WinPaletter theme...");
        }
    }
}
