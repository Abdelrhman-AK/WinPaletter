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

        /// <summary>Returns the icon of a file, or null if it can't be extracted. Caller disposes.</summary>
        private static Icon GetIcon(string filePath)
        {
            // 1) Normal path
            try
            {
                Icon icon = Icon.ExtractAssociatedIcon(filePath);
                if (icon != null) return icon;
            }
            catch { }

            // 2) Windows 9x fallback
            if (OS.W9x)
            {
                IntPtr[] small = new IntPtr[1];
                try
                {
                    if (NativeMethods.Shell32.ExtractIconEx(filePath, 0, null, small, 1) > 0 && small[0] != IntPtr.Zero)
                    {
                        // FromHandle doesn't own the handle, so clone it into an independent Icon and destroy the original handle in the finally block.
                        using Icon temp = Icon.FromHandle(small[0]);
                        return (Icon)temp.Clone();
                    }
                }
                catch { /* Use default icon */ }
                finally
                {
                    if (small[0] != IntPtr.Zero) NativeMethods.User32.DestroyIcon(small[0]);
                }
            }

            return null;
        }

        private void PopulateScreenSaverList()
        {
            if (listView1 == null || imageList1 == null) return;

            listView1.BeginUpdate();
            try
            {
                listView1.Items.Clear();

                // Set these before adding images: changing either one recreates the native image list and discards its contents.
                // If you get black backgrounds on 9x, try Depth24Bit instead.
                imageList1.ColorDepth = ColorDepth.Depth32Bit;
                imageList1.ImageSize = new Size(24, 24);
                imageList1.TransparentColor = Color.Transparent;

                imageList1.Images.Clear();
                listView1.SmallImageList = imageList1;

                const string fallbackKey = "__default";
                imageList1.Images.Add(fallbackKey, SystemIcons.Application); // shared icon: never dispose

                foreach (string filePath in Directory.GetFiles(SysPaths.System32, "*.scr"))
                {
                    string name = Path.GetFileNameWithoutExtension(filePath);
                    ListViewItem item = new(name);
                    item.SubItems.Add(filePath);
                    item.ImageKey = fallbackKey;

                    using (Icon icon = GetIcon(filePath))
                    {
                        if (icon != null)
                        {
                            imageList1.Images.Add(name, icon); // ImageList copies it
                            item.ImageKey = name;
                        }
                    }

                    listView1.Items.Add(item);
                }

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            finally
            {
                listView1.EndUpdate();
            }
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
