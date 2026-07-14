using System;
using System.Drawing;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.UI;

namespace WinPaletter
{
    /// <summary>
    /// A form with glass effect using DWM API.
    /// </summary>
    public class GlassWindow : UI.WP.Form
    {
        private bool shownOverAParent = false;
        private bool blurActive = false;

        public GlassWindow()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.Opaque, true);
            BackColor = Color.Black;
            ControlBox = false;
            Font = new("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            //WindowState = FormWindowState.Maximized;
            PreventFocusSteal = true;
            BorderThickness = -1;
        }

        public new void Show()
        {
            shownOverAParent = false;
            WindowState = FormWindowState.Maximized;
            base.Show();
        }

        public void Show(Form parent)
        {
            if (parent is null)
            {
                shownOverAParent = false;
                WindowState = FormWindowState.Maximized;
                base.Show();
                return;
            }

            shownOverAParent = true;

            int fixer_Width = OS.W10 || OS.W11 || OS.W12 ? 15 : 0;
            int fixer_Height = OS.W10 || OS.W11 || OS.W12 ? 7 : 0;

            WindowState = FormWindowState.Normal;
            Location = parent.Location + new Size(fixer_Width / 2, 0);
            Size = parent.Size - new Size(fixer_Width, fixer_Height);

            Owner = parent;

            base.Show();

            User32.SetWindowPos(Handle, parent.Handle, 0, 0, 0, 0, User32.SetWindowsPosition.NoMove | User32.SetWindowsPosition.NoSize | User32.SetWindowsPosition.NoActivate);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BackColor = Color.Black;
            ApplyGlassEffect();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            //base.OnHandleCreated(e);

            Win32Control.AppendExtendedStyle(Handle, Win32Control.ControlExtendedStyles.Transparent | Win32Control.ControlExtendedStyles.NoActivate);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (!DesignMode && IsHandleCreated) ApplyGlassEffect();
        }

        private void ApplyGlassEffect()
        {
            if (DesignMode || !IsHandleCreated) return;

            if (!Program.ClassicThemeRunning)
            {
                if ((OS.W7 || OS.WVista) && DWMAPI.IsCompositionEnabled())
                {
                    blurActive = true;
                    RemoveLayeredAlpha();

                    DWMAPI.MARGINS margins = new() { leftWidth = -1, rightWidth = -1, topHeight = -1, bottomHeight = -1 };
                    DWMAPI.DwmExtendFrameIntoClientArea(Handle, ref margins);

                    IntPtr hRgn = GDI32.CreateRectRgn(0, 0, Math.Max(1, Width), Math.Max(1, Height));

                    DWMAPI.DWM_BLURBEHIND blurBehind = new()
                    {
                        dwFlags = DWMAPI.DWM_BB_ENABLE | DWMAPI.DWM_BB_BLURREGION,
                        fEnable = true,
                        hRgnBlur = hRgn,
                        fTransitionOnMaximized = false
                    };

                    DWMAPI.DwmEnableBlurBehindWindow(Handle, blurBehind);

                    if (hRgn != IntPtr.Zero) GDI32.DeleteObject(hRgn);

                    Invalidate();
                }
                else if (!OS.WXP && !OS.W8x)
                {
                    blurActive = false;

                    this.DropEffect(Padding.Empty, shownOverAParent, DWM.DWMStyles.Acrylic, true);

                    if (shownOverAParent && (OS.W12 || OS.W11))
                    {
                        bool useRoundedCorners = Program.Settings.Appearance.ManagedByTheme && Program.Settings.Appearance.CustomColors && !OS.WXP && !OS.WVista && !OS.W7 && !OS.W8x && !OS.W10;

                        int argpvAttribute = (int)DWMAPI.FormCornersType.Round;
                        DWMAPI.DwmSetWindowAttribute(Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));

                        if (useRoundedCorners && !Program.Settings.Appearance.RoundedCorners)
                        {
                            int argpvAttribute1 = (int)DWMAPI.FormCornersType.Rectangular;
                            DWMAPI.DwmSetWindowAttribute(Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute1, Marshal.SizeOf(typeof(int)));
                        }
                    }

                    ApplyLayeredAlpha();
                }
                else
                {
                    blurActive = false;
                    this.DropEffect(Padding.Empty, shownOverAParent, DWM.DWMStyles.None);
                    ApplyLayeredAlpha();
                }
            }
            else
            {
                blurActive = false;
                this.DropEffect(Padding.Empty, shownOverAParent, DWM.DWMStyles.None);
                ApplyLayeredAlpha();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (blurActive)
            {
                using (SolidBrush brush = new(Color.Black))
                {
                    e.Graphics.FillRectangle(brush, ClientRectangle);
                }
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        private void ApplyLayeredAlpha()
        {
            Win32Control.AppendExtendedStyle(Handle, Win32Control.ControlExtendedStyles.Layered);
            User32.SetLayeredWindowAttributes(Handle, 0, 128, NativeMethods.User32.LWA_ALPHA);
        }

        private void RemoveLayeredAlpha()
        {
            Win32Control.RemoveExtendedStyle(Handle, Win32Control.ControlExtendedStyles.Layered);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GlassWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.BorderThickness = -1;
            this.ClientSize = new System.Drawing.Size(484, 351);
            this.Name = "GlassWindow";
            this.ResumeLayout(false);

        }
    }
}