using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extensions for <see cref="Form"/> class
    /// </summary>
    public static class FormsExtensions
    {
        /// <summary>
        /// Center the form to the screen (It's a extension method that makes CenterToScreen() accessible as it is a private void in forms)
        /// </summary>
        /// <param name="form">Form to be centered to screen</param>
        public static void CenterToScreen(this Form form) => form.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - form.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - form.Height) / 2);

        /// <summary>
        /// Get the icon of a form type without creating an instance of the form and without form memory allocation.
        /// </summary>
        /// <typeparam name="TForm">The type of the form.</typeparam>
        /// <returns>The icon of the form type, or null if not found.</returns>
        public static Icon Icon<TForm>() where TForm : Form, new()
        {
            // Create an instance of the form, but prevent it from triggering the Load event
            using (TForm form = Activator.CreateInstance(typeof(TForm), true) as TForm)
            {
                return form.Icon;
            }
        }

        /// <summary>
        /// Load the language of a form.
        /// </summary>
        /// <param name="Form"></param>
        /// <param name="Localizer"></param>
        public static void LoadLanguage(this Form Form, Localizer Localizer = null)
        {
            if (Localizer is null)
            {
                if (Program.Settings.Language.Enabled && System.IO.File.Exists(Program.Settings.Language.File))
                    Program.Lang.LoadFromStrings(Form);
            }
            else
            {
                Localizer.LoadFromStrings(Form);
            }
        }

        /// <summary>
        /// Deserialize a form into a JSON object.
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static JObject ToJSON(this Form form)
        {
            if (Forms.IExclude.Contains(form.GetType())) return null;

            JObject j_ctrl = [], j_child = [];
            j_ctrl.RemoveAll();
            j_child.RemoveAll();
            j_ctrl.Add("Text", form.Text);

            foreach (Control ctrl in form.GetAllControls())
            {
                if (!string.IsNullOrWhiteSpace(ctrl.Text) && !ctrl.Text.All(char.IsDigit) && !(ctrl.Text.Count() == 1) && !((ctrl.Text ?? string.Empty) == (ctrl.Name ?? string.Empty)))
                {
                    try
                    {
                        if (!form.Controls.OfType<UI.WP.TablessControl>().ElementAt(0).TabPages.Cast<TabPage>().SelectMany(tp => tp.Controls.OfType<Control>()).Contains(ctrl) & ctrl is not TabPage
                            && !j_child.ContainsKey($"{ctrl.Name}.Text"))
                        {
                            j_child.Add($"{ctrl.Name}.Text", ctrl.Text);
                        }
                    }
                    catch { if (!j_child.ContainsKey($"{ctrl.Name}.Text")) j_child.Add($"{ctrl.Name}.Text", ctrl.Text); }
                }
                if (ctrl is UI.WP.Card card)
                {
                    if (card.Tag is not null && !string.IsNullOrWhiteSpace(card.Tag) && !card.Tag.All(char.IsDigit) && !j_child.ContainsKey($"{card.Name}.Tag"))
                    {
                        j_child.Add($"{card.Name}.Tag", card.Tag);
                    }
                }
                else
                {
                    if (ctrl.Tag is not null && !string.IsNullOrWhiteSpace(ctrl.Tag.ToString()) && !ctrl.Tag.ToString().All(char.IsDigit) && !j_child.ContainsKey($"{ctrl.Name}.Tag"))
                    {
                        j_child.Add($"{ctrl.Name}.Tag", ctrl.Tag.ToString());
                    }
                }
            }

            if (j_ctrl.Count != 0) j_ctrl.Add("Controls", j_child);

            return j_ctrl;
        }
    }
}