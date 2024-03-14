using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.UI.Controllers;

namespace WinPaletter
{
    public partial class AspectsTemplate : Form
    {
        public AspectsTemplate()
        {
            InitializeComponent();
        }

        DesignerData _data;

        public delegate void AspectEnabledChangedEventHandler(object sender, EventArgs e);
        public event AspectEnabledChangedEventHandler AspectEnabledChanged;

        private bool _shown = false;
        public bool IsShown
        {
            get => _shown;
            set
            {
                if (value != _shown)
                {
                    _shown = value;
                }
            }
        }

        private void AspectsTemplate_Load(object sender, EventArgs e)
        {
            IsShown = false;
            CheckForIllegalCrossThreadCalls = false;

            this.LoadLanguage();
            ApplyStyle(this);
        }

        public void LoadData(DesignerData data)
        {
            _data = data;

            FormClosedEventHandler FormClosed = null;

            CanDragOver = _data.CanDragOver;
            AspectName = _data.AspectName;

            IEnumerable<Control> controls = _data.Form.GetAllControls();
            IEnumerable<UI.WP.Button> buttons = controls.OfType<UI.WP.Button>();
            IEnumerable<UI.WP.RadioImage> modes = controls.OfType<UI.WP.RadioImage>();

            UI.WP.Button button_import = buttons.Where(b => b.Name.StartsWith("btn_import")).FirstOrDefault() ?? null;
            UI.WP.Button button_palette_generate = buttons.Where(b => b.Name.StartsWith("btn_palette_generate")).FirstOrDefault() ?? null;
            UI.WP.Button button_saveas_MSTheme = buttons.Where(b => b.Name.StartsWith("btn_saveas_MSTheme")).FirstOrDefault() ?? null;

            UI.WP.Button button_load = buttons.Where(b => b.Name.StartsWith("btn_load_into_theme")).FirstOrDefault() ?? null; ;
            UI.WP.Button button_apply = buttons.Where(b => b.Name.StartsWith("btn_apply")).FirstOrDefault() ?? null; ;
            UI.WP.Button button_cancel = buttons.Where(b => b.Name.StartsWith("btn_cancel")).FirstOrDefault() ?? null; ;

            UI.WP.RadioImage mode_advanced = modes.Where(b => b.Name.StartsWith("checker_mode_advanced")).FirstOrDefault() ?? null;
            UI.WP.RadioImage mode_simple = modes.Where(b => b.Name.StartsWith("checker_mode_simple")).FirstOrDefault() ?? null;

            UI.WP.Toggle toggle = controls.OfType<UI.WP.Toggle>().Where(t => t.Name.StartsWith("checker")).FirstOrDefault() ?? null;

            UI.WP.ComboBox schemes = controls.OfType<UI.WP.ComboBox>().Where(c => c.Name.StartsWith("schemes_list")).FirstOrDefault() ?? null;

            button_palette_generate.Visible = _data.GeneratePalette;
            button_saveas_MSTheme.Visible = _data.GenerateMSTheme;

            if (toggle != null) toggle.Checked = _data.Enabled;

            mode_advanced.Visible = _data.CanSwitchMode;
            mode_simple.Visible = _data.CanSwitchMode;

            //schemes?.Visible = _data.ShowSchemesList;
            //titlebarExtender1.Height = !_data.ShowSchemesList ? schemes.Top + 3 : titlebarExtender1.Height;

            #region Text _data
            button_import.Text = Program.Lang.Designer_import_wpth;
            button_palette_generate.Text = Program.Lang.Designer_generatePalette_image;
            button_saveas_MSTheme.Text = Program.Lang.Designer_saveas_MSTheme;
            mode_advanced.Text = Program.Lang.Designer_mode_advanced;
            mode_simple.Text = Program.Lang.Designer_mode_simple;
            button_load.Text = Program.Lang.Designer_load_into_current_theme;
            button_apply.Text = Program.Lang.Designer_apply;
            button_cancel.Text = Program.Lang.Cancel;
            #endregion

            #region Menu _data
            button_import.Menu.Items.Clear();
            button_palette_generate.Menu.Items.Clear();

            ToolStripMenuItem import_current = new();
            ToolStripMenuItem import_defaultWindows = new();
            ToolStripMenuItem import_theme = new();
            ToolStripMenuItem import_msstyles = new();
            ToolStripMenuItem import_JSON = new();
            ToolStripMenuItem import_scheme = new();
            //ToolStripMenuItem import_scheme_12 = new();
            ToolStripMenuItem import_scheme_11 = new();
            ToolStripMenuItem import_scheme_10 = new();
            ToolStripMenuItem import_scheme_81 = new();
            ToolStripMenuItem import_scheme_7 = new();
            ToolStripMenuItem import_scheme_Vista = new();
            ToolStripMenuItem import_scheme_XP = new();
            ToolStripMenuItem create_palette_fromColor = new();
            ToolStripMenuItem saveTheme_oneAspect = new();

            import_current.Text = Program.Lang.Designer_import_current;
            import_defaultWindows.Text = Program.Lang.Designer_import_defaultWindows;
            import_theme.Text = Program.Lang.Designer_import_classictheme;
            import_msstyles.Text = Program.Lang.Designer_import_msstyles;
            import_scheme.Text = Program.Lang.Designer_import_preset;
            import_scheme_11.Text = Program.Lang.OS_Win11;
            import_scheme_10.Text = Program.Lang.OS_Win10;
            import_scheme_81.Text = Program.Lang.OS_Win81;
            import_scheme_7.Text = Program.Lang.OS_Win7;
            import_scheme_Vista.Text = Program.Lang.OS_WinVista;
            import_scheme_XP.Text = Program.Lang.OS_WinXP;
            //import_scheme_12.Text = Program.Lang.OS_Win12;

            import_JSON.Text = Program.Lang.Designer_import_JSON;

            create_palette_fromColor.Text = Program.Lang.Designer_generatePalette_color;

            import_current.Image = AspectsResources.CurrentApplied;

            if (Program.WindowStyle == PreviewHelpers.WindowStyle.W12)
            {
                import_defaultWindows.Image = WinLogos.Add_Win12_20px;
            }
            else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W11)
            {
                import_defaultWindows.Image = WinLogos.Add_Win11_20px;
            }
            else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W10)
            {
                import_defaultWindows.Image = WinLogos.Add_Win10_20px;
            }
            else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W81)
            {
                import_defaultWindows.Image = WinLogos.Add_Win81_20px;
            }
            else if (Program.WindowStyle == PreviewHelpers.WindowStyle.W7)
            {
                import_defaultWindows.Image = WinLogos.Add_Win7_20px;
            }
            else if (Program.WindowStyle == PreviewHelpers.WindowStyle.WVista)
            {
                import_defaultWindows.Image = WinLogos.Add_WinVista_20px;
            }
            else if (Program.WindowStyle == PreviewHelpers.WindowStyle.WXP)
            {
                import_defaultWindows.Image = WinLogos.Add_WinXP_20px;
            }

            import_theme.Image = AspectsResources.theme;
            import_msstyles.Image = AspectsResources.msstyles;

            import_scheme.Image = AspectsResources.Scheme;
            //import_scheme_12.Image = WinLogos.add_win12_20px;
            import_scheme_11.Image = WinLogos.Add_Win11_20px;
            import_scheme_10.Image = WinLogos.Add_Win10_20px;
            import_scheme_81.Image = WinLogos.Add_Win81_20px;
            import_scheme_7.Image = WinLogos.Add_Win7_20px;
            import_scheme_Vista.Image = WinLogos.Add_WinVista_20px;
            import_scheme_XP.Image = WinLogos.Add_WinXP_20px;

            import_JSON.Image = AspectsResources.JSON;

            create_palette_fromColor.Image = button_palette_generate.Image;

            import_scheme.DropDown.Renderer = button_import.Menu.Renderer;
            import_scheme.DropDown.Items.Add(import_scheme_11);
            import_scheme.DropDown.Items.Add(import_scheme_10);
            import_scheme.DropDown.Items.Add(import_scheme_81);
            import_scheme.DropDown.Items.Add(import_scheme_7);
            import_scheme.DropDown.Items.Add(import_scheme_Vista);
            import_scheme.DropDown.Items.Add(import_scheme_XP);

            button_import.Menu.Items.Add(import_current);
            button_import.Menu.Items.Add(import_defaultWindows);
            button_palette_generate.Menu.Items.Add(create_palette_fromColor);

            saveTheme_oneAspect.Text = Program.Lang.Designer_saveas_MSTheme_OneAspect;
            saveTheme_oneAspect.Image = button_saveas_MSTheme.Image;
            button_saveas_MSTheme.Menu.Items.Add(saveTheme_oneAspect);

            if (_data.Import_preset) button_import.Menu.Items.Add(import_scheme);

            if (_data.Import_theme || _data.Import_msstyles || _data.Import_JSON)
            {

                ToolStripSeparator separator = new();
                button_import.Menu.Items.Add(separator);
            }

            if (_data.Import_theme) button_import.Menu.Items.Add(import_theme);
            if (_data.Import_msstyles) button_import.Menu.Items.Add(import_msstyles);
            if (_data.Import_JSON) button_import.Menu.Items.Add(import_JSON);

            //schemes?.Items.Clear();
            //schemes?.Items.AddRange(_data.SchemeItems);

            #endregion

            #region Events/Overrides

            if (button_load != null)
                button_load.Click += _data.OnLoadIntoCurrentTheme ?? null;

            if (button_apply != null)
                button_apply.Click += _data.OnApply ?? null;

            if (button_cancel != null)
                button_cancel.Click += _data.OnCancel ?? null;

            if (mode_advanced != null)
                mode_advanced.CheckedChanged += _data.OnModeAdvanced ?? null; ;

            if (mode_simple != null)
                mode_simple.CheckedChanged += _data.OnModeSimple ?? null;

            if (toggle != null)
                toggle.CheckedChanged += _data.OnToggleCheckedChanged ?? null;

            if (button_import != null)
                button_import.Click += _data.OnImportFromWPTH ?? null;

            if (button_palette_generate != null)
                button_palette_generate.Click += _data.OnGeneratePaletteFromImage ?? null;

            if (button_saveas_MSTheme != null)
                button_saveas_MSTheme.Click += _data.OnSaveAsMSTheme ?? null;

            if (import_current != null)
                import_current.Click += _data.OnImportFromCurrentApplied ?? null;

            if (import_defaultWindows != null)
                import_defaultWindows.Click += _data.OnImportFromDefault ?? null;

            if (import_scheme_11 != null)
                import_scheme_11.Click += _data.OnImportFromScheme_11;

            if (import_scheme_10 != null)
                import_scheme_10.Click += _data.OnImportFromScheme_10;

            if (import_scheme_81 != null)
                import_scheme_81.Click += _data.OnImportFromScheme_81;

            if (import_scheme_7 != null)
                import_scheme_7.Click += _data.OnImportFromScheme_7;

            if (import_scheme_Vista != null)
                import_scheme_Vista.Click += _data.OnImportFromScheme_Vista;

            if (import_scheme_XP != null)
                import_scheme_XP.Click += _data.OnImportFromScheme_XP;

            if (import_theme != null)
                import_theme.Click += _data.OnImportFromTHEME ?? null;

            if (import_msstyles != null)
                import_msstyles.Click += _data.OnImportFromMSSTYLES ?? null;

            if (import_JSON != null)
                import_JSON.Click += _data.OnImportFromJSON ?? null;

            if (import_scheme != null)
                import_scheme.Click += _data.OnImportFromScheme ?? null;

            if (create_palette_fromColor != null)
                create_palette_fromColor.Click += _data.OnGeneratePaletteFromColor ?? null;

            if (schemes != null)
                schemes.SelectedIndexChanged += _data.OnSchemeIndexChanged ?? null;

            if (saveTheme_oneAspect != null)
                saveTheme_oneAspect.Click += _data.OnSaveAsMSTheme_OneAspect ?? null;

            FormClosed = (sender, args) =>
            {
                if (button_load != null)
                    button_load.Click -= _data.OnLoadIntoCurrentTheme ?? null;

                if (button_apply != null)
                    button_apply.Click -= _data.OnApply ?? null;

                if (button_cancel != null)
                    button_cancel.Click -= _data.OnCancel ?? null;

                if (mode_advanced != null)
                    mode_advanced.CheckedChanged -= _data.OnModeAdvanced ?? null; ;

                if (mode_simple != null)
                    mode_simple.CheckedChanged -= _data.OnModeSimple ?? null;

                if (toggle != null)
                    toggle.CheckedChanged -= _data.OnToggleCheckedChanged ?? null;

                if (button_import != null)
                    button_import.Click -= _data.OnImportFromWPTH ?? null;

                if (button_palette_generate != null)
                    button_palette_generate.Click -= _data.OnGeneratePaletteFromImage ?? null;

                if (button_saveas_MSTheme != null)
                    button_saveas_MSTheme.Click -= _data.OnSaveAsMSTheme ?? null;

                if (import_current != null)
                    import_current.Click -= _data.OnImportFromCurrentApplied ?? null;

                if (import_defaultWindows != null)
                    import_defaultWindows.Click -= _data.OnImportFromDefault ?? null;

                if (import_scheme_11 != null)
                    import_scheme_11.Click -= _data.OnImportFromScheme_11;

                if (import_scheme_10 != null)
                    import_scheme_10.Click -= _data.OnImportFromScheme_10;

                if (import_scheme_81 != null)
                    import_scheme_81.Click -= _data.OnImportFromScheme_81;

                if (import_scheme_7 != null)
                    import_scheme_7.Click -= _data.OnImportFromScheme_7;

                if (import_scheme_Vista != null)
                    import_scheme_Vista.Click -= _data.OnImportFromScheme_Vista;

                if (import_scheme_XP != null)
                    import_scheme_XP.Click -= _data.OnImportFromScheme_XP;

                if (import_theme != null)
                    import_theme.Click -= _data.OnImportFromTHEME ?? null;

                if (import_msstyles != null)
                    import_msstyles.Click -= _data.OnImportFromMSSTYLES ?? null;

                if (import_JSON != null)
                    import_JSON.Click -= _data.OnImportFromJSON ?? null;

                if (import_scheme != null)
                    import_scheme.Click -= _data.OnImportFromScheme ?? null;

                if (create_palette_fromColor != null)
                    create_palette_fromColor.Click -= _data.OnGeneratePaletteFromColor ?? null;

                if (schemes != null)
                    schemes.SelectedIndexChanged -= _data.OnSchemeIndexChanged ?? null;

                if (saveTheme_oneAspect != null)
                    saveTheme_oneAspect.Click -= _data.OnSaveAsMSTheme_OneAspect ?? null;

                _data.Form.FormClosed -= FormClosed;
            };

            _data.Form.FormClosed += FormClosed;

            #endregion
        }

        #region Properties

        public bool AspectEnabled
        {
            get => !DesignMode && _data != null ? _data.Enabled : false;
            set
            {
                if (!DesignMode)
                {
                    if (_data != null && _data.Enabled != value)
                    {
                        _data.Enabled = value;
                        UI.WP.Toggle toggle = this.GetAllControls().OfType<UI.WP.Toggle>().Where(t => t.Name.StartsWith("checker")).FirstOrDefault() ?? null;
                        if (toggle is not null) toggle.Checked = value;
                    }
                }
            }
        }

        public bool AdvancedMode
        {
            get => checker_mode_advanced.Checked;
            set
            {
                checker_mode_advanced.Checked = value;
                checker_mode_simple.Checked = !value;
            }
        }

        public bool SimpleMode => checker_mode_simple.Checked;

        public string AspectName { get; set; } = string.Empty;

        public bool CanDragOver = false;

        #endregion

        #region Private Methods

        private void checker_CheckedChanged(object sender, EventArgs e)
        {
            _data.Enabled = ((UI.WP.Toggle)sender).Checked;

            checker_img.Image = ((UI.WP.Toggle)sender).Checked ? Properties.Resources.checker_enabled : Properties.Resources.checker_disabled;

            AspectEnabledChanged?.Invoke(sender, EventArgs.Empty);

            if (IsShown)
            {
                Program.ToolTip.ToolTipText = ((UI.WP.Toggle)sender).Checked ? Program.Lang.AspectEnabledTip : Program.Lang.AspectDisabledTip;
                Program.ToolTip.ToolTipTitle = ((UI.WP.Toggle)sender).Checked ? string.Format(Program.Lang.AspectEnabled, AspectName) : string.Format(Program.Lang.AspectDisabled, AspectName);
                Program.ToolTip.Image = checker_img.Image;

                Point location = new(-Program.ToolTip.Size.Width + checker_img.Width, (checker_img.Height - Program.ToolTip.Size.Height) / 2 - 1);

                Program.ToolTip.Show(checker_img, Program.ToolTip.ToolTipTitle, Program.ToolTip.ToolTipText, Program.ToolTip.Image, location, 5000);
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (CanDragOver)
            {
                if (e.Data.GetData(typeof(ColorItem).FullName) is ColorItem)
                {
                    Focus();
                    BringToFront();
                }
            }

            base.OnDragOver(e);
        }

        #endregion

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AspectsTemplate_Shown(object sender, EventArgs e)
        {
            IsShown = true;
        }

        private void AspectsTemplate_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.ToolTip.Hide(checker_img);
        }

        private void AspectsTemplate_ParentChanged(object sender, EventArgs e)
        {
            if (this.Parent != null && Parent is TabPage)
            {
                pin_button.Visible = false;
            }
            else
            {
                pin_button.Visible = true;
            }
        }

        private void pin_button_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(this);
        }
    }

    /// <summary>
    /// Class that has data for WinPaletter aspets editor form
    /// </summary>
    public class DesignerData
    {
        /// <summary>
        /// Name of Windows aspect being modified by specified form
        /// </summary>
        public string AspectName { get; set; } = string.Empty;

        /// <summary>
        /// Controls if aspect is enabled
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Parent form
        /// </summary>
        public Form Form;

        /// <summary>
        /// EventHandler associated with clicking on 'Load into current theme' button
        /// </summary>
        public System.EventHandler OnLoadIntoCurrentTheme { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Quick apply' button
        /// </summary>
        public System.EventHandler OnApply { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Cancel' button
        /// </summary>
        public System.EventHandler OnCancel { get; set; }

        /// <summary>
        /// EventHandler associated with checked changed for 'Advanced mode' radio image
        /// </summary>
        public System.EventHandler OnModeAdvanced { get; set; }

        /// <summary>
        /// EventHandler associated with checked changed for 'Simple mode' radio image
        /// </summary>
        public System.EventHandler OnModeSimple { get; set; }

        /// <summary>
        /// EventHandler associated with checked changed for 'Toggle'
        /// </summary>
        public System.EventHandler OnToggleCheckedChanged { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Import from a WinPaletter theme' button
        /// </summary>
        public System.EventHandler OnImportFromWPTH { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Import from current applied theme' button
        /// </summary>
        public System.EventHandler OnImportFromCurrentApplied { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Import from default Windows presets' button
        /// </summary>
        public System.EventHandler OnImportFromDefault { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Import from a classic Windows theme file' button
        /// </summary>
        public System.EventHandler OnImportFromTHEME { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Import from a Windows visual styles file' button
        /// </summary>
        public System.EventHandler OnImportFromMSSTYLES { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Import from a classic Windows theme file' button
        /// </summary>
        public System.EventHandler OnImportFromJSON { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Import from a preset' button
        /// </summary>
        public System.EventHandler OnImportFromScheme { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Generate a palette from an image' button
        /// </summary>
        public System.EventHandler OnGeneratePaletteFromImage { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Generate a palette from a color' button
        /// </summary>
        public System.EventHandler OnGeneratePaletteFromColor { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Save as *.theme file' button
        /// </summary>
        public System.EventHandler OnSaveAsMSTheme { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Save this aspect only as *.theme file' button
        /// </summary>
        public System.EventHandler OnSaveAsMSTheme_OneAspect { get; set; }

        /// <summary>
        /// EventHandler associated with changing selected index of 'Schemes list' combobox
        /// </summary>
        public System.EventHandler OnSchemeIndexChanged { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Import from scheme > Windows 11' button
        /// </summary>
        public System.EventHandler OnImportFromScheme_11 { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Import from scheme > Windows 10' button
        /// </summary>
        public System.EventHandler OnImportFromScheme_10 { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Import from scheme > Windows 8.1' button
        /// </summary>
        public System.EventHandler OnImportFromScheme_81 { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Import from scheme > Windows 7' button
        /// </summary>
        public System.EventHandler OnImportFromScheme_7 { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Import from scheme > Windows Vista' button
        /// </summary>
        public System.EventHandler OnImportFromScheme_Vista { get; set; }

        /// <summary>
        /// EventHandler associated with clicking on 'Import from scheme > Windows XP' button
        /// </summary>
        public System.EventHandler OnImportFromScheme_XP { get; set; }

        /// <summary>
        /// Controls if user can switch between advanced and simple modes
        /// </summary>
        public bool CanSwitchMode { get; set; } = true;

        /// <summary>
        /// Controls if 'Import from a classic Windows theme file' button is visible
        /// </summary>
        public bool Import_theme { get; set; } = false;

        /// <summary>
        /// Controls if 'Import from a Windows visual styles file' button is visible
        /// </summary>
        public bool Import_msstyles { get; set; } = false;

        /// <summary>
        /// Controls if 'Import from a preset' button is visible
        /// </summary>
        public bool Import_preset { get; set; } = true;

        /// <summary>
        /// Controls if 'Import from a JSON file' button is visible
        /// </summary>
        public bool Import_JSON { get; set; } = false;

        /// <summary>
        /// Controls if 'Generate a palette from an image' button is visible
        /// </summary>
        public bool GeneratePalette { get; set; } = true;

        /// <summary>
        /// Controls if 'ApplyToTM as *.theme file' button is visible
        /// </summary>
        public bool GenerateMSTheme { get; set; } = false;

        /// <summary>
        /// Controls if a form can be dragged over by a ColorItem
        /// </summary>
        public bool CanDragOver { get; set; } = true;

        ///// <summary>
        ///// Shows or hides 'Schemes list' combobox
        ///// </summary>
        //public bool ShowSchemesList { get; set; } = false;

        ///// <summary>
        ///// Items for 'Schemes list' combobox
        ///// </summary>
        //public object[] SchemeItems { get; set; } = new object[] { };

        /// <summary>
        /// Creates new data instance for managing WinPaletter aspects editor
        /// </summary>
        /// <param name="Form"></param>
        public DesignerData(Form Form)
        {
            this.Form = Form;
        }
    }
}