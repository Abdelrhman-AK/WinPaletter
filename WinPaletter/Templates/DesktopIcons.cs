using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.UI.Retro;
using WinPaletter.UI.Simulation;

namespace WinPaletter.Templates
{
    /// <summary>
    /// Desktop Icons template (Used to simulate the desktop icons preview)
    /// </summary>
    public partial class DesktopIcons : UserControl
    {
        /// <summary>
        /// Desktop icons font
        /// </summary>
        public new Font Font
        {
            get => _font;
            set
            {
                if (_font != value)
                {
                    _font = value;
                    FakeIcon1.Font = value;
                    FakeIcon2.Font = value;
                    FakeIcon3.Font = value;
                }
            }
        }
        private Font _font = new("Segoe UI", 9f);

        /// <summary>
        /// Desktop icons size
        /// </summary>
        public int IconSize
        {
            get => _iconSize;
            set
            {
                if (value < 16) value = 16;
                if (value > 256) value = 256;

                if (_iconSize != value)
                {
                    _iconSize = value;

                    FakeIcon1.IconSize = value;
                    FakeIcon2.IconSize = value;
                    FakeIcon3.IconSize = value;

                    FakeIcon1.Size = new Size(value + 15 + _iconSpacingH / 16, value + 35);
                    FakeIcon2.Size = new Size(value + 15 + _iconSpacingH / 16, value + 35);
                    FakeIcon3.Size = new Size(value + 15 + _iconSpacingH / 16, value + 35);

                    FakeIcon2.Top = FakeIcon1.Bottom + (_iconSpacingV - 45);
                    FakeIcon3.Left = FakeIcon1.Right + 2;
                }
            }
        }
        private int _iconSize = 32;

        /// <summary>
        /// Vertical spacing between icons
        /// </summary>
        public int IconSpacing_Vertical
        {
            get => _iconSpacingV;
            set
            {
                if (value < 30) value = 30;
                if (value > 100) value = 100;

                if (_iconSpacingV != value)
                {
                    _iconSpacingV = value;

                    FakeIcon2.Top = FakeIcon1.Bottom + (value - 45);

                    FakeIcon2.SendToBack();
                    FakeIcon1.BringToFront();
                }
            }
        }
        private int _iconSpacingV = 75;

        /// <summary>
        /// Horizontal spacing between icons
        /// </summary>
        public int IconSpacing_Horizontal
        {
            get => _iconSpacingH;
            set
            {
                if (value < 30) value = 30;
                if (value > 100) value = 100;

                if (_iconSpacingH != value)
                {
                    _iconSpacingH = value;

                    FakeIcon1.Width = _iconSize + 15 + _iconSpacingH / 16;
                    FakeIcon2.Width = _iconSize + 15 + _iconSpacingH / 16;
                    FakeIcon3.Width = _iconSize + 15 + _iconSpacingH / 16;
                    FakeIcon3.Left = FakeIcon1.Right + 2;

                    FakeIcon3.SendToBack();
                    FakeIcon1.BringToFront();
                }
            }
        }
        private int _iconSpacingH = 75;

        /// <summary>
        /// Icon font
        /// </summary>
        public Font IconFont
        {
            get { return _iconFont; }
            set
            {
                _iconFont = value;
                foreach (WinIcon icon in Controls.OfType<WinIcon>()) { icon.Font = value; }
            }
        }
        private Font _iconFont = new("Segoe UI", 9f);

        /// <summary>
        /// Create a new instance of <see cref="DesktopIcons"/>
        /// </summary>
        public DesktopIcons()
        {
            DoubleBuffered = true;

            InitializeComponent();
        }

        /// <summary>
        /// Event handler for editing an item on the preview (<see cref="EditorInvoker"/>) event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Occurs when an item on the preview is being edited
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        private void DesktopIcons_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                FakeIcon3.hockIcon = FakeIcon1;
                FakeIcon2.hockIcon = FakeIcon1;
                FakeIcon3.EnableEditingSpacingH = true;
                FakeIcon2.EnableEditingSpacingV = true;

                FakeIcon1.Icon = FormsExtensions.Icon<MainForm>();                    // Properties.Resources.fileextension 'Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.RECYCLER, Shell32.SHGSI.ICON)
                FakeIcon2.Icon = Properties.Resources.fileextension;                  // Properties.Resources.settingsfile 'Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.FOLDER, Shell32.SHGSI.ICON)
                FakeIcon3.Icon = Properties.Resources.ThemesResIcon;                  // Properties.Resources.icons8_command_line 'Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.APPLICATION, Shell32.SHGSI.ICON)

                foreach (WinIcon icon in Controls.OfType<WinIcon>()) { icon.EnableEditingMetrics = true; }

                //LoadMetrics(Program.TM);
            }
        }

        /// <summary>
        /// Load metrics from the theme manager
        /// </summary>
        /// <param name="TM"></param>
        public void LoadMetrics(Theme.Manager TM)
        {
            Font = TM.MetricsFonts.IconFont;

            if (TM.WindowsEffects.IconsShadow)
            {
                FakeIcon1.ColorGlow = Color.FromArgb(75, 0, 0, 0);
            }
            else
            {
                FakeIcon1.ColorGlow = Color.FromArgb(0, 0, 0, 0);
            }
            FakeIcon2.ColorGlow = FakeIcon1.ColorGlow;
            FakeIcon3.ColorGlow = FakeIcon1.ColorGlow;
        }

        private void FakeIconX_EditorInvoker(object sender, EditorEventArgs e)
        {
            if (e.PropertyName == nameof(IconSize))
            {
                IconSize = ((WinIcon)sender).IconSize;
                EditorInvoker?.Invoke(sender, e);
            }
            else if (e.PropertyName == nameof(IconSpacing_Vertical))
            {
                IconSpacing_Vertical = ((WinIcon)sender).Top - ((WinIcon)sender).hockIcon.Bottom + 45;
                EditorInvoker?.Invoke(sender, e);
            }
            else if (e.PropertyName == nameof(IconSpacing_Horizontal))
            {
                IconSpacing_Horizontal = 16 * (((WinIcon)sender).Width - (_iconSize + 15));
                EditorInvoker?.Invoke(sender, e);
            }
            else if (e.PropertyName == nameof(IconFont))
            {
                IconFont = ((WinIcon)sender).Font;
                EditorInvoker?.Invoke(sender, e);
            }
        }
    }
}
