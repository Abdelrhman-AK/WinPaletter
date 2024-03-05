using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.TypesExtensions
{
    public static class ComboBoxExtensions
    {
        /// <summary>
        /// Add classic themes names to a ComboBox
        /// </summary>
        public static void PopulateClassicColors(this ComboBox ComboBox)
        {
            ComboBox.Items.Clear();
            ComboBox.Items.AddRange(Properties.Resources.ClassicColorsDB.CList().Select(f => f.Split('|')[0]).ToArray());
        }

        /// <summary>
        /// Add classic themes names to a ComboBox
        /// </summary>
        public static void PopulateMetricsFonts(this ComboBox ComboBox)
        {
            ComboBox.Items.Clear();
            ComboBox.Items.AddRange(Properties.Resources.MetricsFontsDB.CList().Select(f => f.Split('|')[0]).ToArray());
        }
    }
}
