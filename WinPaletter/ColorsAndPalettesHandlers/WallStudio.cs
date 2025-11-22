using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.NativeMethods;
using WinPaletter.Theme;
using WinPaletter.Theme.Structures;
using WinPaletter.UI.AdvancedControls;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.Simulation;
using WinPaletter.UI.WP;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.TypesExtensions.BitmapExtensions;

namespace WinPaletter
{
    public partial class WallStudio : Form
    {
        List<Color> Palette = null;
        Manager TM;
        private readonly List<CursorControl> AnimateList = [];
        private float Angle = 180f;
        private float Increment = 5f;
        private int Cycles = 0;
        ColorEffectControl[] colorEffectControls = null;
        float previewWidthFactor, previewHeightFactor;
        CancellationTokenSource cts;
        public List<ControlProperty<Color>> ColorProperties;
        bool canCloseWithoutMsg = false;

        public WallStudio()
        {
            InitializeComponent();
        }

        private void WallStudio_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);

            NativeMethods.Helpers.RemoveFormTitlebarTextAndIcon(Handle);
            Icon = FormsExtensions.Icon<MainForm>();
            tablessControl1.SelectedIndex = 0;
            progressBar1.Value = 0;
            canCloseWithoutMsg = false;
            next_btn.Enabled = true;
            cts = new();

            progressBar1.Maximum = (tablessControl1.TabCount - 1) * 100;
            previewWidthFactor = tabs_preview_1.Width / 1920f;
            previewHeightFactor = tabs_preview_1.Height / 1080f;

            tabs_preview_1.SelectedIndex = Program.WindowStyle == PreviewHelpers.WindowStyle.W10 ? 1 : 0;
            pictureBox7.Image = LogonUIRes.Win7;

            labelAlt1.Text = Text;
            next_btn.Text = Program.Lang.Strings.General.Next;
            textBox1.Text = Program.TM.Wallpaper.ImageFile;
            label7.Text = DateTime.Now.ToString("h:mm");
            label5.Text = DateTime.Now.ToString("dddd, MMMM d");
            label9.Text = DateTime.Now.ToString("h:mm");
            label10.Text = DateTime.Now.ToString("dddd, MMMM d");

            label16.Font = Fonts.ConsoleMedium;
            if (!Fonts.Exists("Segoe UI Variable Small Semibol"))
            {
                label7.Font = new("Segoe UI", label1.Font.Size, label1.Font.Style);
                label5.Font = new("Segoe UI", label2.Font.Size, label2.Font.Style);
            }

            CheckedListBox1.ForeColor = Program.Style.DarkMode ? Color.White : Color.Black;

            // Make them all black after ApplyStyle(this);
            for (int i = 0; i <= tabs_preview_1.TabCount - 1; i++) { tabs_preview_1.TabPages[i].BackColor = Color.Black; }

            label7.ForeColor = Color.White;
            label5.ForeColor = Color.White;
            label9.ForeColor = Color.White;
            label10.ForeColor = Color.White;

            foreach (CursorControl i in Cursors_Container.Controls.OfType<CursorControl>().Where(i => i.Prop_Cursor == Paths.CursorType.AppLoading | i.Prop_Cursor == Paths.CursorType.Busy))
            {
                AnimateList.Add(i);
            }

            if (colorEffectControls == null)
            {
                colorEffectControls = new ColorEffectControl[ColorEffect.RegisteredEffects.Count];

                for (int i = 0; i < ColorEffect.RegisteredEffects.Count; i++)
                {
                    ColorEffect effect = ColorEffect.RegisteredEffects[i].Clone();
                    ColorEffectControl colorEffectControl = new() { ColorEffect = effect, Dock = DockStyle.Top };
                    colorEffectControls[i] = colorEffectControl;
                    colorEffectControl.ProcessColorEffect += ColorEffectControl_ProcessColorEffect;
                }

                smoothPanel1.Controls.AddRange(colorEffectControls);

                label16.Text = EffectsSummary();
            }

            Forms.GlassWindow.Show();
        }

        private void toggle1_CheckedChanged(object sender, EventArgs e)
        {
            panel14.Enabled = (sender as Toggle).Checked;
            ColorEffectControl_ProcessColorEffect(sender, e);
        }

        private void ColorEffectControl_ProcessColorEffect(object sender, EventArgs e)
        {
            label16.Text = EffectsSummary();
            ApplyEffects();
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            // If last tab, process and close
            if (tablessControl1.SelectedIndex == tablessControl1.TabCount - 1)
            {
                AdjustTM(checkBox3.Checked, false);
                AdjustTMToggles();

                if (radioImage3.Checked || radioImage4.Checked) Program.TM = TM;
                if (radioImage3.Checked) Forms.ThemeLog.Apply_Theme(TM);
                if (radioImage5.Checked)
                {
                    using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = $"WallStudioTheme_{Path.GetFileNameWithoutExtension(textBox1.Text)}.wpth", Title = Program.Lang.Strings.Extensions.SaveWinPaletterTheme })
                    {
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            TM.Save(Manager.Source.File, dlg.FileNames[0]);
                        }
                    }
                }

                canCloseWithoutMsg = true;
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            // Tab 1: Generate palette
            if (tablessControl1.SelectedIndex == 1)
            {
                Cursor = System.Windows.Forms.Cursors.WaitCursor;

                // Clear previous controls
                foreach (Control c in ImgPaletteContainer.Controls)
                {
                    c?.Dispose();
                }
                ImgPaletteContainer.Controls.Clear();

                // Progress reporting
                var progress = new Progress<float>(p => progressBar2.Value = (int)(p * 100));
                var status = new Progress<string>(s => label4.Text = s);

                label4.Text = string.Empty;
                progressBar2.Value = 0;
                label4.Visible = true;
                progressBar2.Visible = true;
                next_btn.Enabled = false;

                try
                {
                    using Bitmap bmp_src = BitmapMgr.Load(textBox1.Text);
                    using Bitmap bmp = bmp_src.Resize(Program.PreviewSize);

                    Palette?.Clear();

                    // Prepare palette generation settings
                    PaletteGeneratorSettings settings = new()
                    {
                        Progress = progress,
                        Status = status,
                        CancellationToken = cts.Token,
                        AccentColor = checkBox2.Checked ? colorItem1.BackColor : null
                    };

                    // Generate palette asynchronously (non-blocking)
                    List<Color> palette = await bmp.ToPalette(settings);

                    // Sort in parallel (still non-blocking)
                    Palette = palette.AsParallel().OrderBy(c => c, new RGBColorComparer()).ToList();

                    if (checkBox1.Checked && File.Exists(textBox2.Text) && textBox2.Text.ToLower().Trim() != textBox1.Text.ToLower().Trim())
                    {
                        using Bitmap bmp_lock_src = BitmapMgr.Load(textBox2.Text);
                        using Bitmap bmp_lock = bmp_lock_src.Resize(Program.PreviewSize);
                        {
                            // Generate palette asynchronously
                            List<Color> palette_lockScreen = await bmp_lock.ToPalette(settings);

                            Palette.AddRange(palette_lockScreen);

                            // Sort in parallel (still non-blocking)
                            Palette = Palette.AsParallel().OrderBy(c => c, new RGBColorComparer()).ToList();
                        }
                    }

                    // Update UI safely
                    ImgPaletteContainer.Controls.Clear();
                    List<ColorItem> colorItems = [];
                    foreach (Color c in Palette)
                    {
                        colorItems.Add(new ColorItem
                        {
                            Size = ColorItem.GetMiniColorItemSize(),
                            AllowDrop = false,
                            PauseColorsHistory = true,
                            BackColor = Color.FromArgb(255, c),
                            DefaultBackColor = Color.FromArgb(255, c)
                        });
                    }
                    ImgPaletteContainer.Controls.AddRange([.. colorItems]);

                    label4.Text = string.Empty;
                    progressBar2.Value = 0;
                    label4.Visible = false;
                    progressBar2.Visible = false;
                    next_btn.Enabled = true;
                    Cursor = System.Windows.Forms.Cursors.Default;
                }
                catch (OperationCanceledException)
                {
                    label4.Text = "Palette generation canceled.";
                    Cursor = System.Windows.Forms.Cursors.Default;
                }
                catch (Exception ex)
                {
                    label4.Text = $"Error: {ex.Message}";
                    Cursor = System.Windows.Forms.Cursors.Default;
                }
            }

            else if (tablessControl1.SelectedIndex == 2)
            {
                // Start Magic
                bool altNearing = checkBox3.Checked;
                toggle1.Checked = false;

                AdjustTM(altNearing);
                Adjust_Preview(TM);
                LoadCursorsFromTM(TM);
                ApplyCMDPreview(CMD1, TM.CommandPrompt, false);
                ApplyCMDPreview(CMD2, TM.PowerShellx86, true);
                ApplyCMDPreview(CMD3, TM.PowerShellx64, true);

                ColorProperties = [];

                foreach (Control ctrl in FlowLayoutPanel1.Controls)
                {
                    // Skip FlowLayoutPanel itself
                    if (ctrl is FlowLayoutPanel) continue;

                    // Get all writable Color properties except BackColor
                    var props = ctrl.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color) && p.CanWrite/* && p.Name != "BackColor"*/);

                    foreach (PropertyInfo prop in props)
                    {
                        ColorProperties.Add(new ControlProperty<Color>(ctrl, prop));
                    }
                }
            }
            else if (tablessControl1.SelectedIndex == 3)
            {
                CheckedListBox1.Items.Clear();
                CheckedListBox1.Items.AddRange([.. Store_CPToggles.EnabledAspects(TM, true)]);

                for (int i = 0, loopTo = CheckedListBox1.Items.Count - 1; i <= loopTo; i++) CheckedListBox1.SetItemChecked(i, true);
            }

            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = tablessControl1.SelectedIndex + 1 > tablessControl1.TabCount - 1 ? tablessControl1.TabCount - 1 : tablessControl1.SelectedIndex + 1;
            Program.Animator.ShowSync(tablessControl1);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            next_btn.Enabled = true;
            next_btn.Text = tablessControl1.SelectedIndex >= tablessControl1.TabCount - 2 ? Program.Lang.Strings.General.Finish : Program.Lang.Strings.General.Next;

            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = tablessControl1.SelectedIndex - 1 < 0 ? 0 : tablessControl1.SelectedIndex - 1;
            Program.Animator.ShowSync(tablessControl1);
        }

        private void tablessControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            back_btn.Enabled = tablessControl1.SelectedIndex > 0;
            next_btn.Text = tablessControl1.SelectedIndex >= tablessControl1.TabCount - 1 ? Program.Lang.Strings.General.Finish : Program.Lang.Strings.General.Next;

            progressBar1.Value = tablessControl1.SelectedIndex * 100;
        }

        /// <summary>
        /// Void to handle the form closing event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = false;

            if (!canCloseWithoutMsg)
            {
                if (MsgBox(Program.Lang.Strings.Messages.CloseWizard, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true;
            }

            // Continue with the closing event if the user has not cancelled it.
            if (!e.Cancel)
            {
                cts?.Cancel();
                Forms.GlassWindow.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void WallStudio_FormClosed(object sender, FormClosedEventArgs e)
        {
            tabs_preview_1.TabPages[0].BackgroundImage?.Dispose();
            tabs_preview_1.TabPages[1].BackgroundImage?.Dispose();
            tabs_preview_1.TabPages[2].BackgroundImage?.Dispose();
            Forms.GlassWindow.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Images, FileName = textBox1.Text, Title = Program.Lang.Strings.Extensions.OpenImages })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = dlg.FileName;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Program.WindowStyle == WindowStyle.WXP)
            {
                textBox1.Text = $@"{SysPaths.Windows}\Web\Wallpaper\Bliss.bmp";
            }
            else
            {
                textBox1.Text = $@"{SysPaths.Windows}\Web\Wallpaper\Windows\img0.jpg";
            }
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = ReadReg("HKEY_CURRENT_USER\\Control Panel\\Desktop", "Wallpaper", string.Empty);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Images, FileName = textBox2.Text, Title = Program.Lang.Strings.Extensions.OpenImages })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = dlg.FileName;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Program.WindowStyle == WindowStyle.WXP)
            {
                textBox2.Text = $@"{SysPaths.Windows}\Web\Wallpaper\Bliss.bmp";
            }
            else
            {
                textBox2.Text = $@"{SysPaths.Windows}\Web\Wallpaper\Windows\img0.jpg";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string defaultLockScreen = ReadReg("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\Personalization", "LockScreenImage", $"{SysPaths.Windows}\\Web\\Screen\\img100.jpg");
            textBox2.Text = defaultLockScreen;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = ReadReg("HKEY_CURRENT_USER\\Control Panel\\Desktop", "Wallpaper", string.Empty);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            next_btn.Enabled = File.Exists(textBox1.Text);

            if (File.Exists(textBox1.Text))
            {
                using (Bitmap bmp_src = BitmapMgr.Load(textBox1.Text))
                using (Bitmap bmp = bmp_src.Resize(Program.PreviewSize))
                {
                    colorItem1.BackColor = bmp.AverageColor();
                }
            }
        }

        private void cur_anim_btn_Click(object sender, EventArgs e)
        {
            Angle = 180f;
            Cycles = 0;
            Cursor_Timer.Enabled = true;
            Cursor_Timer.Start();
        }

        private void Cursor_Timer_Tick(object sender, EventArgs e)
        {
            foreach (CursorControl i in AnimateList)
            {
                Increment = i.Prop_LoadingCircleHot_AnimationSpeed / 2f;

                i.Angle = Angle;
                i.Refresh();

                if (Angle + Increment >= 360f)
                    Angle = 0f;
                Angle += Increment;

                if (Angle == 180f & Cycles >= numericUpDown1.Value - 1)
                {
                    i.Angle = 180f;
                    Cursor_Timer.Enabled = false;
                    Cursor_Timer.Stop();
                }
                else if (Angle == 180f)
                {
                    Cycles += 1;
                }
            }
        }

        private void colorItem1_Click(object sender, EventArgs e)
        {
            colorItem1.BackColor = Forms.ColorPickerDlg.Pick(colorItem1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0, loopTo = CheckedListBox1.Items.Count - 1; i <= loopTo; i++) CheckedListBox1.SetItemChecked(i, false);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            for (int i = 0, loopTo = CheckedListBox1.Items.Count - 1; i <= loopTo; i++) CheckedListBox1.SetItemChecked(i, true);
        }

        #region Helpers

        void ApplyEffects()
        {
            foreach (ControlProperty<Color> prop in ColorProperties)
            {
                prop.Value = Color.FromArgb(255, ApplyEffect(prop.InitialValue));
            }
        }

        Color ApplyEffect(Color c, bool skip = false)
        {
            if (skip || !toggle1.Checked) return Color.FromArgb(255, c);

            // Apply all enabled effects in order
            foreach (ColorEffectControl ctrl in smoothPanel1.Controls.OfType<ColorEffectControl>())
            {
                if (ctrl.ColorEffect.Checked)
                {
                    c = Color.FromArgb(255, ctrl.ColorEffect.Apply(c));
                }
            }

            return Color.FromArgb(255, c);
        }

        private string EffectsSummary()
        {
            int checkedCount = toggle1.Checked ? smoothPanel1.Controls.OfType<ColorEffectControl>().Count(c => c.ColorEffect.Checked) : 0;

            return $"{Program.Lang.Strings.ColorEffects.TotalEffects}: " + smoothPanel1.Controls.OfType<ColorEffectControl>().Count().ToString() + ", " +
                   $"{(checkedCount > 0 ? ($"{Program.Lang.Strings.ColorEffects.Applied}: {checkedCount}") : Program.Lang.Strings.ColorEffects.NoEffects)}.";
        }

        void AdjustTMToggles()
        {
            for (int i = 0, loopTo = CheckedListBox1.Items.Count - 1; i <= loopTo; i++)
            {
                if (CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W12).ToLower())
                    TM.Windows12.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W11).ToLower())
                    TM.Windows11.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W10).ToLower())
                    TM.Windows10.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W81).ToLower())
                    TM.Windows81.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W8).ToLower())
                    TM.Windows8.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W7).ToLower())
                    TM.Windows7.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.WVista).ToLower())
                    TM.WindowsVista.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString().ToLower() == string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.WXP).ToLower())
                    TM.WindowsXP.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.ClassicColors)
                    TM.Win32.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.Accessibility)
                    TM.Accessibility.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.AppTheme)
                    TM.AppTheme.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.LogonUI)
                {
                    if (OS.W7)
                    {
                        TM.LogonUI7.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                    else if (OS.WXP)
                    {
                        TM.LogonUIXP.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                }

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.LockScreen)
                {
                    if (OS.W8x)
                    {
                        TM.LogonUI81.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                    else if (OS.W10)
                    {
                        TM.LogonUI10.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                    else if (OS.W11)
                    {
                        TM.LogonUI11.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                    else if (OS.W12)
                    {
                        TM.LogonUI12.Enabled = CheckedListBox1.GetItemChecked(i);
                    }
                }

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.Cursors)
                    TM.Cursors.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.CommandPrompt)
                    TM.CommandPrompt.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.PowerShellx86)
                    TM.PowerShellx86.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.PowerShellx64)
                    TM.PowerShellx64.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.TerminalStable)
                    TM.Terminal.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.TerminalPreview)
                    TM.TerminalPreview.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.MetricsFonts)
                    TM.MetricsFonts.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.Wallpaper)
                    TM.Wallpaper.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.WinEffects)
                    TM.WindowsEffects.Enabled = CheckedListBox1.GetItemChecked(i);
                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.AltTab)
                    TM.AltTab.Enabled = CheckedListBox1.GetItemChecked(i);

                if (CheckedListBox1.Items[i].ToString() == Program.Lang.Strings.Aspects.Icons)
                    TM.Icons.Enabled = CheckedListBox1.GetItemChecked(i);
            }
        }

        void AdjustTM(bool altNearing = false, bool skipEffects = true)
        {
            TM = radioImage1.Checked ? Default.Get(Program.WindowStyle) : Program.TM.Clone();

            TM.LogonUI12.Enabled = File.Exists(textBox2.Text);
            TM.LogonUI11.Enabled = File.Exists(textBox2.Text);
            TM.LogonUI10.Enabled = File.Exists(textBox2.Text);
            TM.LogonUI7.Enabled = File.Exists(textBox2.Text);
            TM.CommandPrompt.Enabled = true;
            TM.PowerShellx86.Enabled = true;
            TM.PowerShellx64.Enabled = true;
            TM.Cursors.Enabled = true;
            TM.AppTheme.Enabled = true;

            TM.Info.ThemeName = Text;
            TM.Info.Author = Application.CompanyName;
            TM.Info.ExportResThemePack = true;

            TM.Wallpaper.WallpaperType = Wallpaper.WallpaperTypes.Picture;
            TM.Wallpaper.ImageFile = textBox1.Text;

            TM.Windows12.ApplyAccentOnTaskbar = Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;
            TM.Windows12.ApplyAccentOnTitlebars = true;
            TM.Windows12.WinMode_Light = Program.TM.Windows12.WinMode_Light;
            TM.Windows12.AppMode_Light = Program.TM.Windows12.AppMode_Light;

            TM.Windows11.ApplyAccentOnTaskbar = Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;
            TM.Windows11.ApplyAccentOnTitlebars = true;
            TM.Windows11.WinMode_Light = Program.TM.Windows11.WinMode_Light;
            TM.Windows11.AppMode_Light = Program.TM.Windows11.AppMode_Light;

            TM.Windows10.ApplyAccentOnTaskbar = Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;
            TM.Windows10.ApplyAccentOnTitlebars = true;
            TM.Windows10.WinMode_Light = Program.TM.Windows10.WinMode_Light;
            TM.Windows10.AppMode_Light = Program.TM.Windows10.AppMode_Light;

            TM.Windows7.ColorizationColorBalance = 50;
            TM.Windows7.ColorizationBlurBalance = 50;
            TM.Windows7.ColorizationAfterglowBalance = 50;

            TM.LogonUI12.ImageFile = File.Exists(textBox2.Text) ? textBox2.Text : string.Empty;
            TM.LogonUI11.ImageFile = File.Exists(textBox2.Text) ? textBox2.Text : string.Empty;
            TM.LogonUI10.ImageFile = File.Exists(textBox2.Text) ? textBox2.Text : string.Empty;

            TM.LogonUI7.Mode = File.Exists(textBox2.Text) ? Theme.Structures.LogonUI7.Sources.CustomImage : Theme.Structures.LogonUI7.Sources.Default;
            TM.LogonUI7.ImagePath = File.Exists(textBox2.Text) ? textBox2.Text : string.Empty;

            TM.Cursors.Cursor_Arrow.UseFromFile = false;
            TM.Cursors.Cursor_Help.UseFromFile = false;
            TM.Cursors.Cursor_AppLoading.UseFromFile = false;
            TM.Cursors.Cursor_Busy.UseFromFile = false;
            TM.Cursors.Cursor_Move.UseFromFile = false;
            TM.Cursors.Cursor_NS.UseFromFile = false;
            TM.Cursors.Cursor_EW.UseFromFile = false;
            TM.Cursors.Cursor_NESW.UseFromFile = false;
            TM.Cursors.Cursor_NWSE.UseFromFile = false;
            TM.Cursors.Cursor_Up.UseFromFile = false;
            TM.Cursors.Cursor_Pen.UseFromFile = false;
            TM.Cursors.Cursor_None.UseFromFile = false;
            TM.Cursors.Cursor_Link.UseFromFile = false;
            TM.Cursors.Cursor_Pin.UseFromFile = false;
            TM.Cursors.Cursor_Person.UseFromFile = false;
            TM.Cursors.Cursor_IBeam.UseFromFile = false;
            TM.Cursors.Cursor_Cross.UseFromFile = false;

            foreach (PropertyInfo prop in typeof(Windows10x).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
            {
                prop.SetValue(TM.Windows12, ApplyEffect(((Color)prop.GetValue(TM.Windows12)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Windows11, ApplyEffect(((Color)prop.GetValue(TM.Windows11)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Windows10, ApplyEffect(((Color)prop.GetValue(TM.Windows10)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
            }

            foreach (PropertyInfo prop in typeof(Windows81).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
            {
                prop.SetValue(TM.Windows81, ApplyEffect(((Color)prop.GetValue(TM.Windows81)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
            }

            foreach (PropertyInfo prop in typeof(Windows8).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
            {
                prop.SetValue(TM.Windows8, ApplyEffect(((Color)prop.GetValue(TM.Windows8)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
            }

            foreach (PropertyInfo prop in typeof(Windows7).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
            {
                prop.SetValue(TM.Windows7, ApplyEffect(((Color)prop.GetValue(TM.Windows7)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
            }

            foreach (PropertyInfo prop in typeof(WindowsVista).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
            {
                prop.SetValue(TM.WindowsVista, ApplyEffect(((Color)prop.GetValue(TM.WindowsVista)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
            }

            foreach (PropertyInfo prop in typeof(Theme.Structures.Win32UI).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
            {
                prop.SetValue(TM.Win32, ApplyEffect(((Color)prop.GetValue(TM.Win32)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
            }

            foreach (PropertyInfo prop in typeof(Theme.Structures.Console).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
            {
                prop.SetValue(TM.CommandPrompt, ApplyEffect(((Color)prop.GetValue(TM.CommandPrompt)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.PowerShellx86, ApplyEffect(((Color)prop.GetValue(TM.PowerShellx86)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.PowerShellx64, ApplyEffect(((Color)prop.GetValue(TM.PowerShellx64)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
            }

            foreach (PropertyInfo prop in typeof(Info).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
            {
                prop.SetValue(TM.Info, ApplyEffect(((Color)prop.GetValue(TM.Info)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
            }

            foreach (PropertyInfo prop in typeof(Theme.Structures.Cursor).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
            {
                prop.SetValue(TM.Cursors.Cursor_Arrow, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_Arrow)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_Help, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_Help)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_AppLoading, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_AppLoading)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_Busy, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_Busy)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_Move, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_Move)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_NS, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_NS)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_EW, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_EW)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_NESW, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_NESW)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_NWSE, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_NWSE)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_Up, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_Up)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_Pen, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_Pen)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_None, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_None)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_Link, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_Link)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_Pin, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_Pin)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_Person, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_Person)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_IBeam, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_IBeam)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.Cursors.Cursor_Cross, ApplyEffect(((Color)prop.GetValue(TM.Cursors.Cursor_Cross)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
            }

            foreach (Theme.Structures.WinTerminal.Types.Scheme scheme in TM.Terminal.Schemes)
            {
                foreach (PropertyInfo prop in typeof(Theme.Structures.WinTerminal.Types.Scheme).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
                {
                    prop.SetValue(scheme, ApplyEffect(((Color)prop.GetValue(scheme)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                }
            }

            foreach (Theme.Structures.WinTerminal.Types.Scheme scheme in TM.TerminalPreview.Schemes)
            {
                foreach (PropertyInfo prop in typeof(Theme.Structures.WinTerminal.Types.Scheme).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
                {
                    prop.SetValue(scheme, ApplyEffect(((Color)prop.GetValue(scheme)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                }
            }

            foreach (Theme.Structures.WinTerminal.Types.Theme theme in TM.Terminal.Themes)
            {
                foreach (PropertyInfo prop in typeof(Theme.Structures.WinTerminal.Types.Theme).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
                {
                    prop.SetValue(theme, ApplyEffect(((Color)prop.GetValue(theme)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                }
            }

            foreach (Theme.Structures.WinTerminal.Types.Theme theme in TM.TerminalPreview.Themes)
            {
                foreach (PropertyInfo prop in typeof(Theme.Structures.WinTerminal.Types.Theme).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
                {
                    prop.SetValue(theme, ApplyEffect(((Color)prop.GetValue(theme)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                }
            }

            foreach (Theme.Structures.WinTerminal.Types.Profile profile in TM.Terminal.Profiles.List)
            {
                foreach (PropertyInfo prop in typeof(Theme.Structures.WinTerminal.Types.Theme).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
                {
                    prop.SetValue(profile, ApplyEffect(((Color)prop.GetValue(profile)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                }
            }

            foreach (Theme.Structures.WinTerminal.Types.Profile profile in TM.TerminalPreview.Profiles.List)
            {
                foreach (PropertyInfo prop in typeof(Theme.Structures.WinTerminal.Types.Theme).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
                {
                    prop.SetValue(profile, ApplyEffect(((Color)prop.GetValue(profile)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                }
            }

            foreach (PropertyInfo prop in typeof(Theme.Structures.WinTerminal.Types.Profile).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
            {
                prop.SetValue(TM.Terminal.Profiles.Defaults, ApplyEffect(((Color)prop.GetValue(TM.Terminal.Profiles.Defaults)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
                prop.SetValue(TM.TerminalPreview.Profiles.Defaults, ApplyEffect(((Color)prop.GetValue(TM.TerminalPreview.Profiles.Defaults)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
            }

            foreach (PropertyInfo prop in typeof(AppTheme).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(Color)))
            {
                prop.SetValue(TM.AppTheme, ApplyEffect(((Color)prop.GetValue(TM.AppTheme)).GetNearestColorFromPalette(Palette, altNearing), skipEffects));
            }
        }

        #endregion

        #region Preview Methods

        /// <summary>
        /// Adjusts the preview of the store form with the specified theme manager.
        /// </summary>
        /// <param name="TM"></param>
        public void Adjust_Preview(Manager TM)
        {
            windowsDesktop1.WindowStyle = Program.WindowStyle;
            windowsDesktop1.BackgroundImage = Program.FetchSuitableWallpaper(TM, Program.WindowStyle);
            windowsDesktop1.LoadFromTM(TM);
            windowsDesktop1.LoadClassicColors(TM.Win32);
            retroDesktopColors1.LoadColors(TM);
            retroDesktopColors1.LoadMetrics(TM);

            panel2.Visible = System.IO.File.Exists(textBox2.Text) && (Program.WindowStyle == WindowStyle.W12 || Program.WindowStyle == WindowStyle.W11 || Program.WindowStyle == WindowStyle.W10 || Program.WindowStyle == WindowStyle.W7);
            tabs_preview_1.SelectedIndex = Program.WindowStyle == WindowStyle.W11 ? 0 : Program.WindowStyle == WindowStyle.W10 ? 1 : Program.WindowStyle == WindowStyle.W7 ? 2 : 0;

            tabs_preview_1.TabPages[0].BackgroundImage?.Dispose();
            tabs_preview_1.TabPages[1].BackgroundImage?.Dispose();
            tabs_preview_1.TabPages[2].BackgroundImage?.Dispose();

            tabs_preview_1.TabPages[0].BackgroundImage = System.IO.File.Exists(TM.LogonUI11.ImageFile) ? BitmapMgr.Load(TM.LogonUI11.ImageFile) : CaptureLockScreen() ?? null;
            tabs_preview_1.TabPages[1].BackgroundImage = System.IO.File.Exists(TM.LogonUI10.ImageFile) ? BitmapMgr.Load(TM.LogonUI10.ImageFile) : CaptureLockScreen() ?? null;
            tabs_preview_1.TabPages[2].BackgroundImage = ReturnBK() ?? null;
        }

        public Bitmap CaptureLockScreen()
        {
            if (File.Exists(textBox2.Text))
            {
                return BitmapMgr.Load(textBox2.Text).Resize(Program.PreviewSize);
            }
            else
            {
                string mostRecentFile = null;

                string defaultLockScreen = ReadReg("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\Personalization", "LockScreenImage", $"{SysPaths.Windows}\\Web\\Screen\\img100.jpg");

                // Get the path to the current user's lock screen image
                string lockScreenPath = Path.Combine(SysPaths.LocalAppData, "Packages\\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\\LocalState\\Assets");

                if (Directory.Exists(lockScreenPath))
                {
                    // Get the list of files in the lock screen folder
                    string[] files = Directory.GetFiles(lockScreenPath);

                    if (files.Count() > 0)
                    {
                        // Find the most recently accessed File (assuming it's the lock screen image)
                        mostRecentFile = files.OrderByDescending(File.GetLastAccessTime).FirstOrDefault();
                    }
                }

                if (mostRecentFile != null && File.Exists(mostRecentFile))
                {
                    using (Bitmap b = BitmapMgr.Load(mostRecentFile))
                    using (Bitmap b0 = b.Resize((int)(b.Width * previewWidthFactor), (int)(b.Height * previewHeightFactor)))
                    {
                        return b0.FillInSize(tabs_preview_1.Size);
                    }
                }
                else if (File.Exists(defaultLockScreen))
                {
                    using (Bitmap b = BitmapMgr.Load(defaultLockScreen))
                    using (Bitmap b0 = b.Resize((int)(b.Width * previewWidthFactor), (int)(b.Height * previewHeightFactor)))
                    {
                        return b0.FillInSize(tabs_preview_1.Size);
                    }
                }
                else
                {
                    return Program.Wallpaper;
                }
            }
        }

        public object ApplyEffects(Bitmap bmp)
        {
            Bitmap _bmp = bmp;

            if (TM.LogonUI7.Grayscale) _bmp = _bmp.Grayscale();

            if (TM.LogonUI7.Blur) _bmp = _bmp.Blur(TM.LogonUI7.Blur_Intensity);

            if (TM.LogonUI7.Noise)
            {
                switch (TM.LogonUI7.Noise_Mode)
                {
                    case BitmapExtensions.NoiseMode.Acrylic:
                        {
                            _bmp = _bmp.Noise(BitmapExtensions.NoiseMode.Acrylic, TM.LogonUI7.Noise_Intensity / 100f);
                            break;
                        }
                    case BitmapExtensions.NoiseMode.Aero:
                        {
                            _bmp = _bmp.Noise(BitmapExtensions.NoiseMode.Aero, TM.LogonUI7.Noise_Intensity / 100f);
                            break;
                        }
                }
            }

            return _bmp;
        }

        public Bitmap ReturnBK()
        {
            Bitmap bmpX = null;

            if (TM.LogonUI7.Mode == Theme.Structures.LogonUI7.Sources.Default && (OS.W7 || OS.WVista))
            {
                bmpX = PE.GetPNG(SysPaths.imageres, 5038);
            }
            else if (TM.LogonUI7.Mode == Theme.Structures.LogonUI7.Sources.Wallpaper)
            {
                using (Bitmap b = new(Program.GetWallpaperFromRegistry()))
                {
                    bmpX = (Bitmap)b.Clone();
                }
            }
            else if (TM.LogonUI7.Mode == Theme.Structures.LogonUI7.Sources.SolidColor)
            {
                bmpX = TM.Win32.Background.ToBitmap(Screen.PrimaryScreen.Bounds.Size);
            }
            else if (TM.LogonUI7.Mode == Theme.Structures.LogonUI7.Sources.CustomImage & File.Exists(textBox2.Text))
            {
                bmpX = BitmapMgr.Load(textBox2.Text);
            }

            else
            {
                bmpX = Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size);
            }

            if (bmpX is not null)
            {
                return (Bitmap)ApplyEffects(bmpX.Resize(Program.PreviewSize));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Apply Command Prompt preview with the specified console settings.
        /// </summary>
        /// <param name="CMD"></param>
        /// <param name="Console"></param>
        /// <param name="PS"></param>
        public void ApplyCMDPreview(WinCMD CMD, Theme.Structures.Console Console, bool PS)
        {
            CMD.CMD_ColorTable00 = Console.ColorTable00;
            CMD.CMD_ColorTable01 = Console.ColorTable01;
            CMD.CMD_ColorTable02 = Console.ColorTable02;
            CMD.CMD_ColorTable03 = Console.ColorTable03;
            CMD.CMD_ColorTable04 = Console.ColorTable04;
            CMD.CMD_ColorTable05 = Console.ColorTable05;
            CMD.CMD_ColorTable06 = Console.ColorTable06;
            CMD.CMD_ColorTable07 = Console.ColorTable07;
            CMD.CMD_ColorTable08 = Console.ColorTable08;
            CMD.CMD_ColorTable09 = Console.ColorTable09;
            CMD.CMD_ColorTable10 = Console.ColorTable10;
            CMD.CMD_ColorTable11 = Console.ColorTable11;
            CMD.CMD_ColorTable12 = Console.ColorTable12;
            CMD.CMD_ColorTable13 = Console.ColorTable13;
            CMD.CMD_ColorTable14 = Console.ColorTable14;
            CMD.CMD_ColorTable15 = Console.ColorTable15;
            CMD.CMD_PopupForeground = Console.PopupForeground;
            CMD.CMD_PopupBackground = Console.PopupBackground;
            CMD.CMD_ScreenColorsForeground = Console.ScreenColorsForeground;
            CMD.CMD_ScreenColorsBackground = Console.ScreenColorsBackground;

            if (!Console.FontRaster)
            {
                GDI32.LogFont logFont = new()
                {
                    lfFaceName = Console.FaceName,
                    lfHeight = -Console.PixelHeight,
                    lfWidth = Console.PixelWidth,
                    lfWeight = Console.FontWeight
                };

                try
                {
                    CMD.Font = Font.FromLogFont(logFont);
                }
                catch
                {

                }
            }

            CMD.PowerShell = PS;
            CMD.Raster = Console.FontRaster;

            string key = $"{Console.PixelWidth}x{Console.PixelHeight}";
            if (key == "4x6") CMD.RasterSize = WinCMD.Raster_Sizes._4x6;
            else if (key == "6x8") CMD.RasterSize = WinCMD.Raster_Sizes._6x8;
            else if (key == "6x9") CMD.RasterSize = WinCMD.Raster_Sizes._6x8;
            else if (key == "8x8") CMD.RasterSize = WinCMD.Raster_Sizes._8x8;
            else if (key == "8x9") CMD.RasterSize = WinCMD.Raster_Sizes._8x8;
            else if (key == "16x8") CMD.RasterSize = WinCMD.Raster_Sizes._16x8;
            else if (key == "5x12") CMD.RasterSize = WinCMD.Raster_Sizes._5x12;
            else if (key == "7x12") CMD.RasterSize = WinCMD.Raster_Sizes._7x12;
            else if (key == "8x12") CMD.RasterSize = WinCMD.Raster_Sizes._8x12;
            else if (key == "16x12") CMD.RasterSize = WinCMD.Raster_Sizes._16x12;
            else if (key == "12x16") CMD.RasterSize = WinCMD.Raster_Sizes._12x16;
            else if (key == "10x18") CMD.RasterSize = WinCMD.Raster_Sizes._10x18;
            else CMD.RasterSize = WinCMD.Raster_Sizes._8x12; // default

            CMD.Refresh();
        }

        /// <summary>
        /// Load cursors preview from the specified theme manager.
        /// </summary>
        /// <param name="TM"></param>
        public void LoadCursorsFromTM(Manager TM)
        {
            CursorTM_to_Cursor(Arrow, TM.Cursors.Cursor_Arrow);
            CursorTM_to_Cursor(Help, TM.Cursors.Cursor_Help);
            CursorTM_to_Cursor(AppLoading, TM.Cursors.Cursor_AppLoading);
            CursorTM_to_Cursor(Busy, TM.Cursors.Cursor_Busy);
            CursorTM_to_Cursor(Move_Cur, TM.Cursors.Cursor_Move);
            CursorTM_to_Cursor(NS, TM.Cursors.Cursor_NS);
            CursorTM_to_Cursor(EW, TM.Cursors.Cursor_EW);
            CursorTM_to_Cursor(NESW, TM.Cursors.Cursor_NESW);
            CursorTM_to_Cursor(NWSE, TM.Cursors.Cursor_NWSE);
            CursorTM_to_Cursor(Up, TM.Cursors.Cursor_Up);
            CursorTM_to_Cursor(Pen, TM.Cursors.Cursor_Pen);
            CursorTM_to_Cursor(None, TM.Cursors.Cursor_None);
            CursorTM_to_Cursor(Link, TM.Cursors.Cursor_Link);
            CursorTM_to_Cursor(Pin, TM.Cursors.Cursor_Pin);
            CursorTM_to_Cursor(Person, TM.Cursors.Cursor_Person);
            CursorTM_to_Cursor(IBeam, TM.Cursors.Cursor_IBeam);
            CursorTM_to_Cursor(Cross, TM.Cursors.Cursor_Cross);

            foreach (CursorControl i in Cursors_Container.Controls)
            {
                i.Prop_Scale = TM.Cursors.Size / 32f;
                i.Width = (int)(32f * i.Prop_Scale + 32f);
                i.Height = i.Width;
                i.Refresh();
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as UI.WP.CheckBox).Checked)
            {
                Program.Animator.ShowSync(groupBox2);
                Program.Animator.ShowSync(groupBox3);
                Program.Animator.ShowSync(groupBox4);
            }
            else
            {
                Program.Animator.HideSync(groupBox4);
                Program.Animator.HideSync(groupBox3);
                Program.Animator.HideSync(groupBox2);
            }
        }

        /// <summary>
        /// Converts the specified theme manager cursor to the specified cursor control.
        /// </summary>
        /// <param name="CursorControl"></param>
        /// <param name="Cursor"></param>
        public void CursorTM_to_Cursor(CursorControl CursorControl, Theme.Structures.Cursor Cursor)
        {
            CursorControl.Prop_ArrowStyle = Cursor.ArrowStyle;
            CursorControl.Prop_CircleStyle = Cursor.CircleStyle;
            CursorControl.Prop_PrimaryColor1 = Cursor.PrimaryColor1;
            CursorControl.Prop_PrimaryColor2 = Cursor.PrimaryColor2;
            CursorControl.Prop_PrimaryColorGradient = Cursor.PrimaryColorGradient;
            CursorControl.Prop_PrimaryColorGradientMode = Cursor.PrimaryColorGradientMode;
            CursorControl.Prop_PrimaryNoise = Cursor.PrimaryColorNoise;
            CursorControl.Prop_PrimaryNoiseOpacity = Cursor.PrimaryColorNoiseOpacity;
            CursorControl.Prop_SecondaryColor1 = Cursor.SecondaryColor1;
            CursorControl.Prop_SecondaryColor2 = Cursor.SecondaryColor2;
            CursorControl.Prop_SecondaryColorGradient = Cursor.SecondaryColorGradient;
            CursorControl.Prop_SecondaryColorGradientMode = Cursor.SecondaryColorGradientMode;
            CursorControl.Prop_SecondaryNoise = Cursor.SecondaryColorNoise;
            CursorControl.Prop_SecondaryNoiseOpacity = Cursor.SecondaryColorNoiseOpacity;
            CursorControl.Prop_LoadingCircleBack1 = Cursor.LoadingCircleBack1;
            CursorControl.Prop_LoadingCircleBack2 = Cursor.LoadingCircleBack2;
            CursorControl.Prop_LoadingCircleBackGradient = Cursor.LoadingCircleBackGradient;
            CursorControl.Prop_LoadingCircleBackGradientMode = Cursor.LoadingCircleBackGradientMode;
            CursorControl.Prop_LoadingCircleBackNoise = Cursor.LoadingCircleBackNoise;
            CursorControl.Prop_LoadingCircleBackNoiseOpacity = Cursor.LoadingCircleBackNoiseOpacity;
            CursorControl.Prop_LoadingCircleHot1 = Cursor.LoadingCircleHot1;
            CursorControl.Prop_LoadingCircleHot2 = Cursor.LoadingCircleHot2;
            CursorControl.Prop_LoadingCircleHotGradient = Cursor.LoadingCircleHotGradient;
            CursorControl.Prop_LoadingCircleHotGradientMode = Cursor.LoadingCircleHotGradientMode;
            CursorControl.Prop_LoadingCircleHotNoise = Cursor.LoadingCircleHotNoise;
            CursorControl.Prop_LoadingCircleHotNoiseOpacity = Cursor.LoadingCircleHotNoiseOpacity;
            CursorControl.Prop_Shadow_Enabled = Cursor.Shadow_Enabled;
            CursorControl.Prop_Shadow_Color = Cursor.Shadow_Color;
            CursorControl.Prop_Shadow_Blur = Cursor.Shadow_Blur;
            CursorControl.Prop_Shadow_Opacity = Cursor.Shadow_Opacity;
            CursorControl.Prop_Shadow_OffsetX = Cursor.Shadow_OffsetX;
            CursorControl.Prop_Shadow_OffsetY = Cursor.Shadow_OffsetY;
        }

        #endregion
    }
}
