using System;

namespace WinPaletter.UI.Retro
{
    public class EditorEventArgs(string propertyName) : EventArgs
    {
        public string PropertyName { get; } = propertyName;
    }
}