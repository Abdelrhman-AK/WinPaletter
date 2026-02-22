using FluentTransitions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    /// <summary>
    /// Search filter form for the store.
    /// </summary>
    public partial class Store_SearchFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Store_SearchFilter"/> class.
        /// </summary>
        public Store_SearchFilter()
        {
            InitializeComponent();
        }

        Size targetSize;

        private void Store_SearchFilter_Load(object sender, EventArgs e)
        {
            CheckBox1.Checked = Program.Settings.Store.Search_ThemeNames;
            CheckBox2.Checked = Program.Settings.Store.Search_AuthorsNames;
            CheckBox3.Checked = Program.Settings.Store.Search_Descriptions;

             // Animate the form.
            Transition.With(this, nameof(Height), targetSize.Height).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration * 0.6));
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Program.Settings.Store.Search_ThemeNames = CheckBox1.Checked;
            Program.Settings.Store.Search_AuthorsNames = CheckBox2.Checked;
            Program.Settings.Store.Search_Descriptions = CheckBox3.Checked;
            Program.Settings.Store.Save();
            Close();
        }

        public DialogResult ShowDialog(Size parentButtonSize, Point parentButtonLocation)
        {
            targetSize = Size;

            Size = new(Width, 1);
            Location = parentButtonLocation;
            Left -= Width - parentButtonSize.Width;

            return this.ShowDialog();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}