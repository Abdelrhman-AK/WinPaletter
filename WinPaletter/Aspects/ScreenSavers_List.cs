using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class ScreenSavers_List : UI.WP.Form
    {
        public ScreenSavers_List()
        {
            InitializeComponent();
        }

        public string GetScreenSaver(Size parentButtonSize, Point parentButtonLocation)
        {
            if (ShowDialog(parentButtonSize, parentButtonLocation) == DialogResult.OK)
            {
                return listView1.SelectedItems[0].SubItems[1].Text;
            }
            else
            {
                return string.Empty;
            }
        }

        private void ScreenSavers_List_Load(object sender, EventArgs e)
        {
            // Set up columns for the ListView
            listView1.View = View.Details;
            listView1.Columns.Clear();
            listView1.Columns.Add(Program.Localization.Strings.General.Name);
            listView1.Columns.Add(Program.Localization.Strings.General.FilePath);

            PopulateScreenSaverList();
        }

        public DialogResult ShowDialog(Size parentButtonSize, Point parentButtonLocation)
        {
            Location = parentButtonLocation + new Size(parentButtonSize.Width - Width, parentButtonSize.Height);

            return this.ShowDialog();
        }

        private void PopulateScreenSaverList()
        {
            listView1.Items.Clear();

            // Set the ImageList for the ListView
            listView1.SmallImageList = imageList1;

            // Get all .scr files in the screensaver folder
            string[] screensavers = Directory.GetFiles(SysPaths.System32, "*.scr");

            // SetFormValues the ListView
            foreach (string screensaver in screensavers)
            {
                string name = Path.GetFileNameWithoutExtension(screensaver);
                string filePath = Path.Combine(SysPaths.System32, screensaver);

                // Get the icon for the screensaver
                using (Icon icon = Icon.ExtractAssociatedIcon(filePath))
                {
                    // Add the screensaver details to the ListView
                    ListViewItem item = new(name);
                    item.SubItems.Add(filePath);
                    item.ImageKey = name;

                    // Add the icon to the ImageList
                    imageList1.Images.Add(name, icon.ToBitmap());

                    listView1.Items.Add(item);
                }

                listView1.Items[listView1.Items.Count - 1].ImageIndex = imageList1.Images.Count - 1;
            }

            // Resize the columns
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
