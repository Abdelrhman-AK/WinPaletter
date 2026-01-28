using FluentTransitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.NativeMethods;
using WinPaletter.Properties;
using WinPaletter.UI.AdvancedControls;
using static WinPaletter.Settings.Structures.NerdStats;

namespace WinPaletter.UI.Controllers
{
    [DefaultEvent("Click")]
    public class ColorItem : Control
    {
        WP.ContextMenuStrip contextMenu = new() { ShowImageMargin = true, AllowTransparency = true };
        ToolStripMenuItem darken = new() { Image = ColorItemContextMenu.Darken };
        ToolStripMenuItem lighten = new() { Image = ColorItemContextMenu.Lighten };
        ToolStripMenuItem invert = new() { Image = ColorEffects.Invert_24 };
        ToolStripMenuItem copy = new() { Image = ColorItemContextMenu.Copy };
        ToolStripMenuItem paste = new();
        ToolStripMenuItem cut = new() { Image = ColorItemContextMenu.Cut };
        ToolStripMenuItem delete = new() { Image = ColorItemContextMenu.Delete };
        ToolStripMenuItem reset = new() { Image = ColorItemContextMenu.Default };
        ToolStripMenuItem blend = new();
        ToolStripMenuItem someEffects = new() { Image = ColorItemContextMenu.Effects };

        ToolStripMenuItem previousColor = new();
        ToolStripSeparator toolStripSeparator0 = new();
        ToolStripSeparator toolStripSeparator1 = new();
        ToolStripSeparator toolStripSeparator2 = new();
        ToolStripMenuItem copy_AsHex = new();
        ToolStripMenuItem copy_AsRGB = new();
        ToolStripMenuItem copy_AsHSL = new();
        ToolStripMenuItem copy_AsDecimal = new();
        ToolStripMenuItem copy_AsRGBPercent = new();
        ToolStripMenuItem copy_AsARGB = new();
        ToolStripMenuItem copy_AsHSLA = new();
        ToolStripMenuItem copy_AsHSV = new();
        ToolStripMenuItem copy_AsCMYK = new();
        ToolStripMenuItem copy_AsWin32 = new();
        ToolStripMenuItem copy_AsKnownName = new();
        ToolStripMenuItem copy_AsCSS = new();

        private static Font genericMonospacedFont = new(FontFamily.GenericMonospace.Name, 8.5f, FontStyle.Regular);

        public ColorItem()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            Rect = new(0, 0, Width - 1, Height - 1);
            RectInner = new(1, 1, Width - 3, Height - 3);
            Rect_DefColor = new(7, (Height - 7) / 2, 7, 7);
            Rect_DefColor_MouseHoverFixer = new(Rect_DefColor.X - 3, Rect_DefColor.Y - 3, Rect_DefColor.Width + 6, Rect_DefColor.Height + 6);
            Timer2 = new() { Enabled = false, Interval = 1 };

            alpha = 0;
            Timer2_factor = 0;

            Text = string.Empty;
            ColorsHistory.Clear();
            Timer2.Tick += Timer2_Tick;

            copy.DropDown = new UI.WP.ContextMenuStrip() { ShowImageMargin = false };
            copy.DropDownItems.Add(copy_AsHex);
            copy.DropDownItems.Add(copy_AsRGB);
            copy.DropDownItems.Add(copy_AsHSL);
            copy.DropDownItems.Add(copy_AsDecimal);
            copy.DropDownItems.Add(copy_AsRGBPercent);
            copy.DropDownItems.Add(copy_AsARGB);
            copy.DropDownItems.Add(copy_AsHSLA);
            copy.DropDownItems.Add(copy_AsHSV);
            copy.DropDownItems.Add(copy_AsCMYK);
            copy.DropDownItems.Add(copy_AsWin32);
            copy.DropDownItems.Add(copy_AsKnownName);
            copy.DropDownItems.Add(copy_AsCSS);

            contextMenu.Items.Add(cut);
            contextMenu.Items.Add(copy);
            contextMenu.Items.Add(paste);
            contextMenu.Items.Add(blend);
            contextMenu.Items.Add(toolStripSeparator0);
            contextMenu.Items.Add(delete);
            contextMenu.Items.Add(reset);
            contextMenu.Items.Add(previousColor);
            contextMenu.Items.Add(toolStripSeparator1);
            contextMenu.Items.Add(darken);
            contextMenu.Items.Add(lighten);
            contextMenu.Items.Add(toolStripSeparator2);
            contextMenu.Items.Add(invert);

            someEffects.DropDown = new WP.ContextMenuStrip();

            for (int i = 0; i < ColorEffect.RegisteredEffects.Count; i++)
            {
                ColorEffect effect = ColorEffect.RegisteredEffects[i].Clone();

                if (!effect.HasScrollbar)
                {
                    effect.Checked = true;

                    ToolStripMenuItem item = new() { Text = effect.Name, Image = effect.SmallImage.Resize(20, 20), Tag = effect };

                    item.Click += Item_Click;

                    someEffects.DropDownItems.Add(item);
                }
                else
                {
                    //ToolStripMenuItem item = new()
                    //{
                    //    Text = effect.Name,
                    //    Image = effect.SmallImage,
                    //    DropDown = new UI.WP.ContextMenuStrip() { ShowImageMargin = false },
                    //    Tag = effect
                    //};

                    //for (int i = (int)effect.ScrollbarMin; i <= effect.ScrollbarMax; i += (int)((effect.ScrollbarMax - effect.ScrollbarMin) / 10f))
                    //{
                    //    effect.Checked = true;
                    //    effect.ScrollbarValue = i;

                    //    ToolStripMenuItem subItem = new() { Text = i.ToString(), Tag = effect };
                    //    subItem.Click += Item_Click;
                    //    item.DropDownItems.Add(subItem);
                    //}

                    //someEffects.DropDownItems.Add(item);
                }
            }

            contextMenu.Items.Add(someEffects);

            copy.Click += Copy_Click;
            cut.Click += Cut_Click;
            paste.Click += Paste_Click;
            blend.Click += Blend_Click;
            delete.Click += Delete_Click;
            darken.Click += Darken_Click;
            lighten.Click += Lighten_Click;
            invert.Click += Invert_Click;
            reset.Click += Reset_Click;
            previousColor.Click += PreviousColor_Click;
            copy_AsHex.Click += Copy_AsHEX;
            copy_AsRGB.Click += Copy_AsRGB;
            copy_AsHSL.Click += Copy_AsHSL;
            copy_AsDecimal.Click += Copy_AsDec;
            copy_AsRGBPercent.Click += Copy_AsRGBPercent;
            copy_AsARGB.Click += Copy_AsARGB;
            copy_AsHSLA.Click += Copy_AsHSLA;
            copy_AsHSV.Click += Copy_AsHSV;
            copy_AsCMYK.Click += Copy_AsCMYK;
            copy_AsWin32.Click += Copy_AsWin32;
            copy_AsKnownName.Click += Copy_AsKnownName;
            copy_AsCSS.Click += Copy_AsCSS;
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        public bool ColorPickerOpened = false;
        public List<Color> ColorsHistory = [];
        private Color LineColor;
        public bool PauseColorsHistory = false;

        private Rectangle Rect;
        private Rectangle RectInner;
        private Rectangle Rect_DefColor;
        private Rectangle Rect_DefColor_MouseHoverFixer;


        public MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        #endregion

        #region Properties 
        /// <summary>
        /// Gets or sets the default background color.
        /// </summary>
        public new Color DefaultBackColor { get; set; } = Color.Black;

        /// <summary>
        /// Gets or sets a value indicating whether the menu display should be canceled.
        /// </summary>
        [DefaultValue(false)]
        public bool CancelShowingMenu { get; set; } = false;

        public bool DontShowInfo { get; set; } = false;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Bindable(true)]
        [DefaultValue("")]
        public override string Text { get; set; } = string.Empty;

        #endregion

        #region Drag and drop
        private bool SwapNotCopy = false;
        private bool InitializeDrag = false;
        private Point mousePosition_beforeDrag, mousePosition_afterDrag;
        private AfterDropEffects AfterDropEffect = AfterDropEffects.None;
        private bool DragDefaultColor = false;
        private Color DraggedColor;
        private bool DragDropMouseHovering = false;
        private bool MakeAfterDropEffect = false;
        private Color BeforeDropColor;
        private Point BeforeDropMousePosition;
        private bool HoverOverDefColorDot = false;

        public enum AfterDropEffects
        {
            None,
            Invert,
            Darker,
            Lighter,
            Mix
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            mousePosition_afterDrag = MousePosition;

            if (InitializeDrag && mousePosition_beforeDrag != mousePosition_afterDrag)
            {
                DragDefaultColor = CanRaiseEventsForDefColorDot();
                DoDragDrop(this, DragDropEffects.Copy | DragDropEffects.Move);
            }

            if (!DesignMode && Program.Settings.NerdStats.DotDefaultChangedIndicator)
            {
                HoverOverDefColorDot = CanRaiseEventsForDefColorDot();
                Invalidate();
            }

            base.OnMouseMove(e);
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            if (AllowDrop && Program.Settings.NerdStats.DragAndDrop)
            {
                BeforeDropColor = BackColor;
                BeforeDropMousePosition = PointToClient(MousePosition);
                Timer2_factor = 0;
                MakeAfterDropEffect = true;
                Timer2.Enabled = true;
                Timer2.Start();

                if (e.Data.GetData(typeof(ColorItem)) is not ColorItem draggedColorItem) return;

                if (!SwapNotCopy) // Copy
                {
                    BackColor = ProcessDraggedColorEffect(draggedColorItem.DragDefaultColor ? draggedColorItem.DefaultBackColor : draggedColorItem.BackColor);
                }
                else // Swap
                {
                    BackColor = draggedColorItem.BackColor;

                    draggedColorItem.BackColor = ProcessDraggedColorEffect(BeforeDropColor);

                    draggedColorItem.ContextMenuMadeColorChangeInvoker?.Invoke(draggedColorItem, new ContextMenuMadeColorChangeEventArgs(draggedColorItem, null));
                }

                ColorClipboard.CopiedColor = BackColor;

                base.OnDragDrop(e);
            }

            AfterDropEffect = AfterDropEffects.None;
            DragDropMouseHovering = false;
            Invalidate();
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            if (DesignMode) { base.OnDragEnter(e); return; }

            if (!AllowDrop || !Program.Settings.NerdStats.DragAndDrop)
            {
                DragDropMouseHovering = false;
                BeginInvoke((Action)Invalidate);
                e.Effect = DragDropEffects.None;
                base.OnDragEnter(e);
                return;
            }

            DragDropMouseHovering = true;
            UpdateDragState(e);   // evaluate keys on entry

            if (e.Data.GetData(typeof(ColorItem)) is ColorItem item)
            {
                DraggedColor = ProcessDraggedColorEffect(item.DragDefaultColor ? item.DefaultBackColor : item.BackColor);
            }

            base.OnDragEnter(e);
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (!AllowDrop || !Program.Settings.NerdStats.DragAndDrop)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            DragDropMouseHovering = true;

            if (e.Data.GetData(typeof(ColorItem)) is ColorItem item)
            {
                DraggedColor = ProcessDraggedColorEffect(item.DragDefaultColor ? item.DefaultBackColor : item.BackColor);
            }

            UpdateDragState(e);      // re-check modifiers every mouse-move

            // Quick repaint for visual feedback
            Invalidate();

            base.OnDragOver(e);
        }

        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);

            // Stop hover visuals and request a repaint
            DragDropMouseHovering = false;
            AfterDropEffect = AfterDropEffects.None;
            SwapNotCopy = false;
            Invalidate();   // schedules repaint; no delay needed
        }

        private void UpdateDragState(DragEventArgs e)
        {
            const int MK_RBUTTON = 2;
            const int MK_SHIFT = 4;
            const int MK_CTRL = 8;
            const int MK_ALT = 32;

            // Defaults
            SwapNotCopy = false;
            AfterDropEffect = AfterDropEffects.None;

            bool alt = (e.KeyState & MK_ALT) != 0;
            bool ctrl = (e.KeyState & MK_CTRL) != 0;
            bool shift = (e.KeyState & MK_SHIFT) != 0;
            bool rbtn = (e.KeyState & MK_RBUTTON) != 0;

            if (alt && ctrl) AfterDropEffect = AfterDropEffects.Mix;
            else if (alt) AfterDropEffect = AfterDropEffects.Invert;
            else if (ctrl) AfterDropEffect = AfterDropEffects.Darker;
            else if (shift) AfterDropEffect = AfterDropEffects.Lighter;

            if (rbtn) SwapNotCopy = true;

            // Set the drag-drop cursor feedback
            e.Effect = SwapNotCopy ? DragDropEffects.Move : DragDropEffects.Copy;
        }

        private Color ProcessDraggedColorEffect(Color c)
        {
            switch (AfterDropEffect)
            {
                case AfterDropEffects.Invert:
                    {
                        return c.Invert();
                    }

                case AfterDropEffects.Darker:
                    {
                        return c.Dark();
                    }

                case AfterDropEffects.Lighter:
                    {
                        return c.Light();
                    }

                case AfterDropEffects.Mix:
                    {
                        return c.Blend(BeforeDropColor, 0.5f);
                    }

                default:
                    {
                        return c;
                    }
            }
        }

        #endregion

        #region Methods

        public void UpdateColorsHistory()
        {
            if (!PauseColorsHistory)
            {
                if (ColorsHistory.Count > 0)
                {
                    if (ColorsHistory.Last() != BackColor) ColorsHistory.Add(BackColor);
                }
                else
                {
                    ColorsHistory.Add(BackColor);
                }
            }
        }

        public static Size GetMiniColorItemSize()
        {
            return new Size(Program.Settings.NerdStats.Enabled ? 80 : 30, 24);
        }

        public bool CanRaiseEventsForDefColorDot()
        {
            return Program.Settings.NerdStats.DotDefaultChangedIndicator && DefaultBackColor != Color.Empty && DefaultBackColor != Color.Transparent && Rect_DefColor_MouseHoverFixer.Contains(PointToClient(MousePosition)) && BackColor != DefaultBackColor;
        }

        #endregion

        #region Events/Overrides

        /// <summary>
        /// Represents the method that will handle an event when a context menu triggers a color change.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An object that contains the event data, including details about the color change.</param>
        public delegate void ContextMenuMadeColorChange(object sender, ContextMenuMadeColorChangeEventArgs e);

        /// <summary>
        /// Occurs when a color change is triggered by the context menu.
        /// </summary>
        /// <remarks>This event is raised to notify subscribers that a color change has been made through
        /// the context menu. Subscribers can handle this event to perform actions in response to the color
        /// change.</remarks>
        public event ContextMenuMadeColorChange ContextMenuMadeColorChangeInvoker;

        /// <summary>
        /// Provides data for the event that occurs when a context menu action results in a color change.
        /// </summary>
        /// <remarks>This event argument contains information about the selected color and the item that
        /// was clicked in the context menu. It is typically used in scenarios where a user selects a color from a
        /// context menu, and the application needs to respond to the selection.</remarks>
        /// <param name="colorItem"></param>
        /// <param name="clickedItem"></param>
        public class ContextMenuMadeColorChangeEventArgs(ColorItem colorItem, object clickedItem) : EventArgs
        {
            /// <summary>
            /// Gets or sets the item that was clicked.
            /// </summary>
            public object ClickedItem { get; set; } = clickedItem;

            /// <summary>
            /// Gets the color item associated with this instance.
            /// </summary>
            public ColorItem ColorItem { get; } = colorItem;

            /// <summary>
            /// Gets the background color associated with the current item.
            /// </summary>
            public Color Color => ColorItem?.BackColor ?? Color.Empty;
        }

        /// <summary>
        /// Processes Windows messages sent to the control, including handling custom behavior for the right mouse
        /// button release.
        /// </summary>
        /// <remarks>This method overrides the default message processing to display a custom context menu
        /// when the right mouse button is released. If the message is not handled, it is passed to the base
        /// implementation.</remarks>
        /// <param name="m">A <see cref="Message"/> object that represents the Windows message to process.</param>
        protected override void WndProc(ref Message m)
        {
            const int WM_RBUTTONUP = 0x0205;

            if (m.Msg == WM_RBUTTONUP && !CancelShowingMenu)
            {
                // Custom context menu implementation
                copy_AsHex.Text = Program.Localization.Strings.General.Copy_HEX;
                copy_AsRGB.Text = Program.Localization.Strings.General.Copy_RGB;
                copy_AsHSL.Text = Program.Localization.Strings.General.Copy_HSL;
                copy_AsDecimal.Text = Program.Localization.Strings.General.Copy_Decimal;
                copy_AsRGBPercent.Text = Program.Localization.Strings.General.Copy_RGBPercent;
                copy_AsARGB.Text = Program.Localization.Strings.General.Copy_ARGB;
                copy_AsHSLA.Text = Program.Localization.Strings.General.Copy_HSLA;
                copy_AsHSV.Text = Program.Localization.Strings.General.Copy_HSV;
                copy_AsCMYK.Text = Program.Localization.Strings.General.Copy_CMYK;
                copy_AsWin32.Text = Program.Localization.Strings.General.Copy_Win32;
                copy_AsKnownName.Text = Program.Localization.Strings.General.Copy_KnownName;
                copy_AsCSS.Text = Program.Localization.Strings.General.Copy_CSS;

                copy.Text = Program.Localization.Strings.General.Copy;
                cut.Text = Program.Localization.Strings.General.Cut;
                paste.Text = Program.Localization.Strings.General.Paste;
                blend.Text = Program.Localization.Strings.General.PasteByBlending;
                delete.Text = Program.Localization.Strings.General.Delete;
                reset.Text = Program.Localization.Strings.General.Default + " (" + OS.Name + ")";
                darken.Text = Program.Localization.Strings.General.Darken;
                lighten.Text = Program.Localization.Strings.General.Lighten;
                invert.Text = Program.Localization.Strings.General.Invert;
                previousColor.Text = Program.Localization.Strings.General.PreviousColor;
                someEffects.Text = Program.Localization.Strings.ColorEffects.SomeEffects;

                blend.Enabled = ColorClipboard.CopiedColor != Color.Empty;
                blend.Image = blend.Enabled
                    ? ColorEffects.Blend_24
                    : ColorEffects.Blend_24.Grayscale();

                string clipBoard = Clipboard.GetText();
                bool clipBoardHasColor = clipBoard?.IsHexColor() ?? false;
                ColorClipboard.CopiedColor = ColorClipboard.CopiedColor == Color.Empty && clipBoardHasColor ? clipBoard.ToColor() : ColorClipboard.CopiedColor;

                paste.Enabled = ColorClipboard.CopiedColor != Color.Empty;
                paste.Image = paste.Enabled
                    ? ColorItemContextMenu.Paste
                    : ColorItemContextMenu.Paste.Grayscale();

                previousColor.Enabled = ColorsHistory.Count > 2;
                previousColor.Image = previousColor.Enabled
                    ? ColorItemContextMenu.Reset
                    : ColorItemContextMenu.Reset.Grayscale();

                // Screen coordinates
                Point screenPoint = new((short)(m.LParam.ToInt32() & 0xFFFF), (short)(m.LParam.ToInt32() >> 16));

                // release mouse capture so the menu can take focus
                User32.ReleaseCapture();

                contextMenu.Show(this, screenPoint);

                return;
            }

            base.WndProc(ref m);
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            ColorClipboard.CopiedColor = BackColor;
            CopyColor(BackColor.ToString(Program.Settings.NerdStats.Type));
            contextMenu.Close();
        }

        private void Copy_AsHEX(object sender, EventArgs e)
        {
            CopyColor(BackColor.ToString(Formats.HEX));
        }

        private void Copy_AsRGB(object sender, EventArgs e)
        {
            CopyColor(BackColor.ToString(Formats.RGB));
        }

        private void Copy_AsHSL(object sender, EventArgs e)
        {
            CopyColor(BackColor.ToString(Formats.HSL));
        }

        private void Copy_AsDec(object sender, EventArgs e)
        {
            CopyColor(BackColor.ToString(Formats.Dec));
        }

        private void Copy_AsRGBPercent(object sender, EventArgs e)
        {
            CopyColor(BackColor.ToString(Formats.RGBPercent));
        }

        private void Copy_AsARGB(object sender, EventArgs e)
        {
            CopyColor(BackColor.ToString(Formats.ARGB));
        }

        private void Copy_AsHSLA(object sender, EventArgs e)
        {
            CopyColor(BackColor.ToString(Formats.HSLA));
        }

        private void Copy_AsHSV(object sender, EventArgs e)
        {
            CopyColor(BackColor.ToString(Formats.HSV));
        }

        private void Copy_AsCMYK(object sender, EventArgs e)
        {
            CopyColor(BackColor.ToString(Formats.CMYK));
        }

        private void Copy_AsWin32(object sender, EventArgs e)
        {
            CopyColor(BackColor.ToString(Formats.Win32));
        }

        private void Copy_AsKnownName(object sender, EventArgs e)
        {
            CopyColor(BackColor.ToString(Formats.KnownName));
        }

        private void Copy_AsCSS(object sender, EventArgs e)
        {
            CopyColor(BackColor.ToString(Formats.CSS));
        }

        private void CopyColor(string text)
        {
            Clipboard.SetText(text);
            ColorClipboard.CopiedColor = BackColor;
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            ColorClipboard.CopiedColor = BackColor;

            BeforeDropColor = BackColor;
            BeforeDropMousePosition = PointToClient(new(contextMenu.Left, contextMenu.Top));
            Timer2_factor = 0;
            MakeAfterDropEffect = true;
            Timer2.Enabled = true;
            Timer2.Start();
            BackColor = Color.Empty;

            if (BeforeDropColor != BackColor) ContextMenuMadeColorChangeInvoker?.Invoke(this, new ContextMenuMadeColorChangeEventArgs(this, sender));
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            if (ColorClipboard.CopiedColor != Color.Empty)
            {
                BeforeDropColor = BackColor;
                BeforeDropMousePosition = PointToClient(new(contextMenu.Left, contextMenu.Top));
                Timer2_factor = 0;
                MakeAfterDropEffect = true;
                Timer2.Enabled = true;
                Timer2.Start();
                BackColor = ColorClipboard.CopiedColor;

                if (BeforeDropColor != BackColor) ContextMenuMadeColorChangeInvoker?.Invoke(this, new ContextMenuMadeColorChangeEventArgs(this, sender));
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            BeforeDropColor = BackColor;
            BeforeDropMousePosition = PointToClient(new(contextMenu.Left, contextMenu.Top));
            Timer2_factor = 0;
            MakeAfterDropEffect = true;
            Timer2.Enabled = true;
            Timer2.Start();
            BackColor = Color.Empty;

            if (BeforeDropColor != BackColor) ContextMenuMadeColorChangeInvoker?.Invoke(this, new ContextMenuMadeColorChangeEventArgs(this, sender));
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            BeforeDropColor = BackColor;
            BeforeDropMousePosition = PointToClient(new(contextMenu.Left, contextMenu.Top));
            Timer2_factor = 0;
            MakeAfterDropEffect = true;
            Timer2.Enabled = true;
            Timer2.Start();
            BackColor = DefaultBackColor;

            if (BeforeDropColor != BackColor) ContextMenuMadeColorChangeInvoker?.Invoke(this, new ContextMenuMadeColorChangeEventArgs(this, sender));
        }

        private void PreviousColor_Click(object sender, EventArgs e)
        {
            if (ColorsHistory.Count > 1)
            {
                PauseColorsHistory = true;

                BeforeDropColor = BackColor;
                BeforeDropMousePosition = PointToClient(new(contextMenu.Left, contextMenu.Top));
                Timer2_factor = 0;
                MakeAfterDropEffect = true;
                Timer2.Enabled = true;
                Timer2.Start();
                BackColor = ColorsHistory[ColorsHistory.Count - 2];

                ColorsHistory.RemoveAt(ColorsHistory.Count - 1);
                PauseColorsHistory = false;
                if (BeforeDropColor != BackColor) ContextMenuMadeColorChangeInvoker?.Invoke(this, new ContextMenuMadeColorChangeEventArgs(this, sender));
            }
        }

        private void Darken_Click(object sender, EventArgs e)
        {
            BeforeDropColor = BackColor;
            BeforeDropMousePosition = PointToClient(new(contextMenu.Left, contextMenu.Top));
            Timer2_factor = 0;
            MakeAfterDropEffect = true;
            Timer2.Enabled = true;
            Timer2.Start();
            BackColor = BackColor.Dark();

            if (BeforeDropColor != BackColor) ContextMenuMadeColorChangeInvoker?.Invoke(this, new ContextMenuMadeColorChangeEventArgs(this, sender));
        }

        private void Lighten_Click(object sender, EventArgs e)
        {
            BeforeDropColor = BackColor;
            BeforeDropMousePosition = PointToClient(new(contextMenu.Left, contextMenu.Top));
            Timer2_factor = 0;
            MakeAfterDropEffect = true;
            Timer2.Enabled = true;
            Timer2.Start();
            BackColor = BackColor.Light();

            if (BeforeDropColor != BackColor) ContextMenuMadeColorChangeInvoker?.Invoke(this, new ContextMenuMadeColorChangeEventArgs(this, sender));
        }

        private void Invert_Click(object sender, EventArgs e)
        {
            BeforeDropColor = BackColor;
            BeforeDropMousePosition = PointToClient(new(contextMenu.Left, contextMenu.Top));
            Timer2_factor = 0;
            MakeAfterDropEffect = true;
            Timer2.Enabled = true;
            Timer2.Start();
            BackColor = BackColor.Invert();

            if (BeforeDropColor != BackColor) ContextMenuMadeColorChangeInvoker?.Invoke(this, new ContextMenuMadeColorChangeEventArgs(this, sender));
        }

        private void Blend_Click(object sender, EventArgs e)
        {
            BeforeDropColor = BackColor;
            BeforeDropMousePosition = PointToClient(new(contextMenu.Left, contextMenu.Top));
            Timer2_factor = 0;
            MakeAfterDropEffect = true;
            Timer2.Enabled = true;
            Timer2.Start();
            BackColor = BackColor.Blend(ColorClipboard.CopiedColor, 0.5f);

            if (BeforeDropColor != BackColor) ContextMenuMadeColorChangeInvoker?.Invoke(this, new ContextMenuMadeColorChangeEventArgs(this, sender));
        }

        private void Item_Click(object sender, EventArgs e)
        {
            BeforeDropColor = BackColor;
            BeforeDropMousePosition = PointToClient(new(contextMenu.Left, contextMenu.Top));
            Timer2_factor = 0;
            MakeAfterDropEffect = true;
            Timer2.Enabled = true;
            Timer2.Start();

            ColorEffect effect = (sender as ToolStripMenuItem).Tag as ColorEffect;
            BackColor = effect.Apply(BeforeDropColor);

            if (BeforeDropColor != BackColor) ContextMenuMadeColorChangeInvoker?.Invoke(this, new ContextMenuMadeColorChangeEventArgs(this, sender));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Rect = new(0, 0, Width - 1, Height - 1);
            RectInner = new(1, 1, Width - 3, Height - 3);
            Rect_DefColor = new(7, (Height - 7) / 2, 7, 7);
            Rect_DefColor_MouseHoverFixer = new(Rect_DefColor.X - 3, Rect_DefColor.Y - 3, Rect_DefColor.Width + 6, Rect_DefColor.Height + 6);

            base.OnSizeChanged(e);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            UpdateColorsHistory();

            base.OnBackColorChanged(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            mousePosition_beforeDrag = MousePosition;
            InitializeDrag = Program.Settings.NerdStats.DragAndDrop;
            State = MouseState.Down;

            if (CanAnimate) { Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            InitializeDrag = false;
            State = MouseState.Over;
            bool MouseOver = ClientRectangle.Contains(e.Location);
            if (CanAnimate) { Transition.With(this, nameof(alpha), ContainsFocus || MouseOver ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = ContainsFocus || MouseOver ? 255 : 0; }

            base.OnMouseUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            State = MouseState.Over;

            if (CanAnimate) { Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            InitializeDrag = false;
            HoverOverDefColorDot = false;
            State = MouseState.None;

            if (CanAnimate) { Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseLeave(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Timer2?.Dispose();
        }

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }

        #endregion

        #region Animator
        private int _alpha = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha
        {
            get => _alpha;
            set { _alpha = value; Invalidate(); }
        }

        private readonly Timer Timer2;
        private int Timer2_factor = 0;

        private async void Timer2_Tick(object sender, EventArgs e)
        {
            if (!DesignMode && MakeAfterDropEffect)
            {
                Timer2_factor = (int)(Timer2_factor + Math.Min(Width, Height) * 3.5f);
                await Task.Delay(1);
                Invalidate();
            }
            else
            {
                Timer2_factor = 0;
                MakeAfterDropEffect = false;
                await Task.Delay(1);
                Invalidate();
                Timer2.Enabled = false;
                Timer2.Stop();
            }
        }
        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            G.Clear(this.GetParentColor());

            if (Enabled)
            {
                byte a = State == MouseState.Down ? (byte)200 : State == MouseState.Over ? (byte)150 : (byte)100;
                a *= (byte)(BackColor.A / 255f);

                LineColor = Color.FromArgb(a, BackColor.IsDark() ? BackColor.Light() : BackColor.Dark());

                if (BackColor.A < 255)
                {
                    using (TextureBrush br = new(Resources.BackgroundOpacity)) { G.FillRoundedRect(br, RectInner); }
                    using (Bitmap b = Resources.BackgroundOpacity.Fade(alpha / 255f))
                    using (TextureBrush br = new(b))
                    {
                        G.FillRoundedRect(br, Rect);
                    }
                }

                if (!DesignMode && MakeAfterDropEffect && CanAnimate)
                {
                    // Make ripple effect on dropping a color
                    using (SolidBrush br = new(BeforeDropColor))
                    {
                        G.FillRoundedRect(br, RectInner);
                    }

                    using (GraphicsPath path = Program.Style.RoundedCorners ? RectInner.Round(Program.Style.Radius) : new GraphicsPath())
                    {
                        if (!Program.Style.RoundedCorners) { path.AddRectangle(RectInner); }
                        {
                            int i = Math.Max(Width, Height) + Timer2_factor;

                            using (Region reg = new(path))
                            using (GraphicsPath gp = new())
                            {
                                G.Clip = reg;
                                Point px = BeforeDropMousePosition;
                                RectangleF MouseCircle = new(px.X - 0.5f * i, px.Y - 0.5f * i, i, i);
                                gp.AddEllipse(MouseCircle);
                                using (PathGradientBrush pgb = new(gp)
                                {
                                    CenterPoint = px,
                                    CenterColor = BackColor,
                                    SurroundColors = [Color.Transparent]
                                })
                                { G.FillEllipse(pgb, MouseCircle); }
                                G.ResetClip();
                            }

                            if (i / 2d > Width * Height)
                            {
                                Timer2.Enabled = false;
                                Timer2.Stop();
                                Timer2_factor = 0;
                                MakeAfterDropEffect = false;
                                Invalidate();
                            }
                        }
                    }

                    using (Pen P = new(LineColor)) { G.DrawRoundedRectBeveled(P, RectInner); }
                }

                else if (!DesignMode && DragDropMouseHovering && CanAnimate)
                {
                    // Make circle hover effect on dragging over a color

                    using (SolidBrush br = new(BackColor)) { G.FillRoundedRect(br, Rect); }

                    using (GraphicsPath path = Rect.Round(Program.Style.Radius))
                    using (Region reg = new(path))
                    using (GraphicsPath gp = new())
                    {
                        G.Clip = reg;
                        int i = Math.Max(Width, Height);
                        Point px = PointToClient(MousePosition);
                        RectangleF MouseCircle = new(px.X - 0.5f * i, px.Y - 0.5f * i, i, i);
                        gp.AddEllipse(MouseCircle);
                        using (PathGradientBrush pgb = new(gp)
                        {
                            CenterPoint = px,
                            CenterColor = DraggedColor,
                            SurroundColors = [Color.Transparent]
                        })
                        {
                            G.FillEllipse(pgb, MouseCircle);
                        }
                        G.ResetClip();
                    }

                    using (Pen P = new(base.BackColor.IsDark() ? Color.White : Color.Black, 1.5f) { DashStyle = DashStyle.Dot }) { G.DrawRoundedRectBeveled(P, Rect); }
                }

                else
                {
                    // Normal appearance

                    using (SolidBrush br = new(BackColor)) { G.FillRoundedRect(br, RectInner); }

                    using (SolidBrush br = new(Color.FromArgb((int)(alpha / 255f * BackColor.A), BackColor))) { G.FillRoundedRect(br, Rect); }

                    using (Pen P = new(Color.FromArgb((int)((255f - alpha) / 255f * LineColor.A), LineColor))) { G.DrawRoundedRectBeveled(P, RectInner); }

                    using (Pen P = new(Color.FromArgb((int)(alpha / 255f * LineColor.A), LineColor))) { G.DrawRoundedRectBeveled(P, Rect); }
                }

                if (!DesignMode && Program.Settings.NerdStats.DotDefaultChangedIndicator)
                {
                    using (SolidBrush br = new(DefaultBackColor))
                    {
                        float L = 7f;
                        float Y = RectInner.Y + (RectInner.Height - L) / 2f;
                        RectangleF DefDotRect;

                        if (!HoverOverDefColorDot) { DefDotRect = new(L, Y, L, L); }
                        else { DefDotRect = new(L - 1, Y - 1, L + 2, L + 2); }

                        G.FillEllipse(br, DefDotRect);
                    }
                }
            }

            else
            {
                using (SolidBrush br = new(Program.Style.Schemes.Disabled.Colors.Back(parentLevel)))
                {
                    G.FillRoundedRect(br, RectInner);
                }
            }

            if (!DesignMode)
            {
                if (Program.Settings.NerdStats.Enabled & !DontShowInfo)
                {
                    G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

                    Color TargetColor = Enabled ? (!HoverOverDefColorDot | !Program.Settings.NerdStats.DotDefaultChangedIndicator ? BackColor : DefaultBackColor) : Program.Style.Schemes.Disabled.Colors.Line(parentLevel);
                    Color FC0 = TargetColor.IsDark() ? LineColor.LightLight() : LineColor.Dark(0.9f);
                    Color FC1 = TargetColor.IsDark() ? LineColor.LightLight() : LineColor.Dark(0.9f);

                    FC0 = Color.FromArgb(Program.Settings.NerdStats.MoreLabelTransparency ? 75 : 125, FC0);
                    FC1 = Color.FromArgb(alpha, FC1);

                    Rectangle RectX = Rect; RectX.Y += 1;

                    string S;
                    if (AfterDropEffect != AfterDropEffects.None)
                    {
                        S = AfterDropEffect == AfterDropEffects.Invert ? Program.Localization.Strings.General.Invert :
                            AfterDropEffect == AfterDropEffects.Darker ? Program.Localization.Strings.General.Darken :
                            AfterDropEffect == AfterDropEffects.Lighter ? Program.Localization.Strings.General.Lighten :
                            Program.Localization.Strings.General.Blend;
                    }
                    else
                    {
                        S = Enabled ? TargetColor.ToString(default, Program.Settings.NerdStats.ShowHexHash, true) : Program.Localization.Strings.General.Disabled;
                    }

                    Font F = Program.Settings.NerdStats.UseWindowsMonospacedFont ? genericMonospacedFont : Fonts.Console;

                    using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                    {
                        using (SolidBrush br = new(FC0)) { G.DrawString(S, F, br, RectX, sf); }
                        using (SolidBrush br = new(FC1)) { G.DrawString(S, F, br, RectX, sf); }
                    }

                    if (!DesignMode && DragDropMouseHovering && CanAnimate)
                    {
                        using (GraphicsPath path = Rect.Round(Program.Style.Radius))
                        using (Region reg = new(path))
                        using (GraphicsPath gp = new())
                        {
                            int i = Math.Max(Width, Height);
                            Point px = PointToClient(MousePosition);
                            RectangleF MouseCircle = new(px.X - 0.5f * i, px.Y - 0.5f * i, i, i);
                            gp.AddEllipse(MouseCircle);

                            G.SetClip(gp);

                            using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                            {
                                using (SolidBrush br = new(DraggedColor.IsDark() ? DraggedColor.Light() : DraggedColor.Dark())) { G.DrawString(S, F, br, RectX, sf); }
                            }

                            G.ResetClip();
                        }
                    }

                    using (StringFormat sf = ContentAlignment.MiddleRight.ToStringFormat())
                    {
                        using (SolidBrush br = new(FC0)) { G.DrawString(ColorPickerOpened ? "▼" : string.Empty, F, br, new Rectangle(RectX.X, RectX.Y, RectX.Width - 5, RectX.Height), sf); }
                        using (SolidBrush br = new(FC1)) { G.DrawString(ColorPickerOpened ? "▼" : string.Empty, F, br, new Rectangle(RectX.X, RectX.Y, RectX.Width - 5, RectX.Height), sf); }
                    }
                }
            }

            base.OnPaint(e);
        }
    }
}