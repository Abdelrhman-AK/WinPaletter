using System.Windows.Forms;

namespace WinPaletter.TypesExtensions
{
    public static class FormsExtensions
    {
        /// <summary>
        /// Center the form to the screen (It's a extension method that makes CenterToScreen() accessible as it is a private void in forms)
        /// </summary>
        /// <param name="form">Form to be centered to screen</param>
        public static void CenterToScreen(this Form form) => form.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - form.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - form.Height) / 2);
    }
}