using System.Reflection;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        private bool _ErrorHappened = false;
        private readonly BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

        /// <summary>
        /// Enumeration for WinPaletter themes sources
        /// </summary>
        public enum Source
        {
            /// <summary>
            /// EmptyError WinPaletter theme File
            /// </summary>
            Empty,
            /// <summary>
            /// Windows Registry
            /// </summary>
            Registry,
            /// <summary>
            /// WinPaletter theme File
            /// </summary>
            File,
        }
    }
}