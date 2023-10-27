using System.Reflection;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        private bool _ErrorHappened = false;
        private readonly BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
        private readonly Converter converter = new();

        /// <summary>
        /// Enumeration for WinPaletter themes sources
        /// </summary>
        public enum Source
        {
            /// <summary>
            /// Windows Registry
            /// </summary>
            Registry,
            /// <summary>
            /// WinPaletter theme file
            /// </summary>
            File,
            /// <summary>
            /// Empty WinPaletter theme file
            /// </summary>
            Empty
        }
    }
}