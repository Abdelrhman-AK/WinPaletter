using System.Reflection;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        private bool _ErrorHappened = false;
        private readonly BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
        private readonly Converter _Converter = new Converter();
        public enum Source
        {
            Registry,
            File,
            Empty
        }
    }
}