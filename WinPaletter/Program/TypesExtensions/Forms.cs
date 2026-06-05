using Newtonsoft.Json.Linq;
using System;
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
        /// Center the form to the screen
        /// </summary>
        public static void CenterToScreen(this System.Windows.Forms.Form form)
        {
            form.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - form.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - form.Height) / 2);
        }

        /// <summary>
        /// Load the language of a form.
        /// </summary>
        public static void Localize(this System.Windows.Forms.Form Form, Localizer Localizer = null)
        {
            if (Localizer == null)
            {
                if (Program.Settings.Language.Enabled && File.Exists(Program.Settings.Language.File)) Program.Localization.LoadFromStrings(Form);
            }
            else
            {
                Localizer.LoadFromStrings(Form);
            }
        }

        /// <summary>
        /// Deserialize a form into a JSON object.
        /// </summary>
        public static JObject ToJSON(this System.Windows.Forms.Form form)
        {
            if (Forms.IExclude.Contains(form.GetType())) return null;

            JObject j_ctrl = [];
            JObject j_child = [];
            j_ctrl.Add("Text", form.Text);

            List<Control> allControls = form.GetAllControls().ToList();

            // Build a set of controls that live inside TablessControl tab pages.
            // Using a HashSet<int> (handle) avoids boxing and is faster to look up than HashSet<Control>.
            HashSet<IntPtr> tablessChildHandles = null;
            foreach (TablessControl tc in allControls.OfType<TablessControl>())
            {
                tablessChildHandles ??= [];

                foreach (TabPage tp in tc.TabPages)
                    foreach (Control c in tp.Controls)
                        tablessChildHandles.Add(c.Handle);
            }

            foreach (Control ctrl in allControls)
            {
                string ctrlTag = ctrl.Tag?.ToString();

                bool notTabPage = ctrl is not TabPage;
                bool isTablessChild = tablessChildHandles != null && tablessChildHandles.Contains(ctrl.Handle);

                string textKey = $"{ctrl.Name}.Text";
                string tagKey = $"{ctrl.Name}.Tag";

                if (notTabPage && !isTablessChild && !j_child.ContainsKey(textKey) && IsTranslatable(ctrl.Text, ctrl.Name))
                    j_child.Add(textKey, ctrl.Text);

                if (!j_child.ContainsKey(tagKey) && IsTranslatable(ctrlTag, ctrl.Name))
                    j_child.Add(tagKey, ctrlTag);
            }

            if (j_child.Count > 0)
                j_ctrl.Add("Controls", j_child);

            return j_ctrl;
        }

        // Pre-compiled regexes allocated once for the process lifetime.
        private static readonly Regex SingleLetterWithPunctuationRegex = new(@"^[\p{P}]*[A-Za-z][\p{P}]*$", RegexOptions.Compiled);
        private static readonly Regex VersionRegex = new(@"^\d+(\.\d+){1,3}$", RegexOptions.Compiled);

        private static bool IsTranslatable(string text, string ctrlName)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;
            if (text.Length < 2) return false;

            // Quick rejection: starts with "http" covers both http:// and https://
            if (text.StartsWith("http://", StringComparison.Ordinal) || text.StartsWith("https://", StringComparison.Ordinal))
                return false;

            // Reject strings identical to the control name
            if (text == ctrlName) return false;

            // Single-pass classification: count digit, letter, punctuation, symbol chars.
            // This replaces three separate .All() scans (digit, punctuation, symbol).
            int digitCount = 0;
            int letterCount = 0;
            int punctCount = 0;
            int symbolCount = 0;
            int len = text.Length;

            for (int i = 0; i < len; i++)
            {
                char c = text[i];
                if (char.IsDigit(c)) digitCount++;
                else if (char.IsLetter(c)) letterCount++;
                else if (char.IsPunctuation(c)) punctCount++;
                else if (char.IsSymbol(c)) symbolCount++;
            }

            // All digits
            if (digitCount == len) return false;

            // All punctuation or all symbols
            if (punctCount == len || symbolCount == len) return false;

            // Single letter with surrounding punctuation
            if (letterCount == 1 && (punctCount + letterCount) == len && SingleLetterWithPunctuationRegex.IsMatch(text))
                return false;

            // GUID check: GUIDs are exactly 36 chars with digits and hyphens only
            if (len == 36 && Guid.TryParse(text, out _)) return false;

            // Version string check: only attempt regex when the string contains only digits and dots.
            // This avoids the regex engine entirely for most UI strings.
            if (digitCount + punctCount == len && VersionRegex.IsMatch(text)) return false;

            // DateTime check: only attempt when the string has a plausible date-like character profile.
            // A date string must contain at least 3 digits and a separator (- / : space).
            // This pre-filter eliminates the expensive TryParse for the vast majority of UI labels.
            if (digitCount >= 3 && (text.IndexOf('-') >= 0 || text.IndexOf('/') >= 0 || text.IndexOf(':') >= 0))
            {
                if (DateTime.TryParse(text, out _)) return false;
            }

            return true;
        }
    }
}