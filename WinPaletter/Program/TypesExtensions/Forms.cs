using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WinPaletter.UI.WP;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extensions for <see cref="System.Windows.Forms.Form"/> class
    /// </summary>
    public static class FormsExtensions
    {
        /// <summary>
        /// Center the form to the screen (It's a extension method that makes CenterToScreen() accessible as it is a private void in forms)
        /// </summary>
        /// <param name="form">Form to be centered to screen</param>
        public static void CenterToScreen(this System.Windows.Forms.Form form)
        {
            form.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - form.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - form.Height) / 2);
        }

        /// <summary>
        /// Load the language of a form.
        /// </summary>
        /// <param name="Form"></param>
        /// <param name="Localizer"></param>
        public static void Localize(this System.Windows.Forms.Form Form, Localizer Localizer = null)
        {
            if (Localizer is null)
            {
                if (Program.Settings.Language.Enabled && File.Exists(Program.Settings.Language.File))
                    Program.Localization.LoadFromStrings(Form);
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
        public static JObject ToJSON(this System.Windows.Forms.Form form)
        {
            if (Forms.IExclude.Contains(form.GetType())) return null;

            JObject j_ctrl = [], j_child = [];
            j_ctrl.Add("Text", form.Text);

            List<Control> allControls = form.GetAllControls().ToList();
            HashSet<Control> tablessChildren = [.. allControls.OfType<TablessControl>().SelectMany(tc => tc.TabPages.Cast<TabPage>().SelectMany(tp => tp.Controls.OfType<Control>()))];

            foreach (Control ctrl in allControls)
            {
                string ctrlTag = ctrl.Tag?.ToString();

                bool notTabPage = ctrl is not TabPage;
                bool textExists = j_child.ContainsKey($"{ctrl.Name}.Text");
                bool tagExists = j_child.ContainsKey($"{ctrl.Name}.Tag");

                if (notTabPage && !textExists && !tablessChildren.Contains(ctrl) && IsTranslatable(ctrl.Text, ctrl.Name))
                {
                    j_child.Add($"{ctrl.Name}.Text", ctrl.Text);
                }

                if (!tagExists && IsTranslatable(ctrlTag, ctrl.Name))
                {
                    j_child.Add($"{ctrl.Name}.Tag", ctrlTag);
                }
            }

            if (j_ctrl.Count != 0) j_ctrl.Add("Controls", j_child);

            return j_ctrl;
        }

        private static readonly Regex SingleLetterWithPunctuationRegex = new(@"^[\p{P}]*[A-Za-z][\p{P}]*$", RegexOptions.Compiled);

        private static bool IsTranslatable(string text, string ctrlName)
        {
            // Reject empty or whitespace-only strings
            if (string.IsNullOrWhiteSpace(text)) return false;

            // Reject strings shorter than 2 characters or strings that are purely numeric
            if (text.Length < 2 || text.All(char.IsDigit)) return false;

            // Reject single letters with surrounding punctuation, e.g., "H:", "(A)"
            if (SingleLetterWithPunctuationRegex.IsMatch(text) && text.Count(char.IsLetter) == 1) return false;

            // Reject strings composed entirely of punctuation or symbols, e.g., "---", "©"
            if (text.All(char.IsPunctuation) || text.All(char.IsSymbol)) return false;

            // Reject GUIDs, e.g., "3F2504E0-4F89-11D3-9A0C-0305E82C3301"
            if (Guid.TryParse(text, out _)) return false;

            // Reject dates and times, e.g., "2025-10-08", "10:45 AM"
            if (DateTime.TryParse(text, out _)) return false;

            // Reject version-like strings, e.g., "1.0", "3.2.1.15"
            if (Regex.IsMatch(text, @"^\d+(\.\d+){1,3}$")) return false;

            // Reject URLs, e.g., "http://example.com", "https://example.com"
            if (text.StartsWith("http://") || text.StartsWith("https://")) return false;

            // Reject strings that are identical to the control name
            if (text == ctrlName) return false;

            // If none of the above conditions matched, the string is likely translatable
            return true;
        }
    }
}