using System;
using System.Linq;

namespace WinPaletter.UI.Retro
{
    public class EditorEventArgs : EventArgs
    {
        public string PropertyName { get; }

        public EditorEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}