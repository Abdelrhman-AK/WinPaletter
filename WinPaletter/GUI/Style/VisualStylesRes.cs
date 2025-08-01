using Devcorp.Controls.VisualStyles;
using System;
using System.Drawing;

namespace WinPaletter
{
    /// <summary>
    /// This class is used to draw Windows WXP visual styles on <see cref="Graphics"/> .
    /// </summary>
    /// <param name="themeFile"></param>
    public class VisualStylesRes(string themeFile) : IDisposable
    {
        /// <summary>
        /// The visual style file.
        /// </summary>
        private readonly VisualStyleFile _VisualStyleFile = new(themeFile);

        /// <summary>
        /// The elements that can be drawn.
        /// </summary>
        public enum Element
        {
            /// <summary>
            /// Windows WXP titlebar.
            /// </summary>
            Titlebar,

            /// <summary>
            /// Windows WXP right edge.
            /// </summary>
            RightEdge,

            /// <summary>
            /// Windows WXP left edge.
            /// </summary>
            LeftEdge,

            /// <summary>
            /// Windows WXP bottom edge.
            /// </summary>
            BottomEdge,

            /// <summary>
            /// Windows WXP taskbar.
            /// </summary>
            Taskbar,

            /// <summary>
            /// Windows WXP close button.
            /// </summary>
            CloseButton
        }

        /// <summary>
        /// Draws a Windows WXP visual style element on the specified <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="G"></param>
        /// <param name="Rectangle"></param>
        /// <param name="element"></param>
        /// <param name="Active"></param>
        /// <param name="ToolWindow"></param>
        public void Draw(Graphics G, Rectangle Rectangle, Element element, bool Active, bool ToolWindow)
        {
            // The visual style element.
            VisualStyleElement el;

            // Draw the visual style element.
            switch (element)
            {
                case Element.Titlebar:
                    {
                        // Do some adjustments to the rectangle.
                        Rectangle.Offset(1, 1);
                        Rectangle.Inflate(0, 1);

                        // Get the visual style element for the titlebar and the caption state.
                        WindowCaptionState CS = Active ? WindowCaptionState.Active : WindowCaptionState.Inactive;

                        if (!ToolWindow)
                        {
                            el = VisualStyleElement.Window.Caption.GetElement(_VisualStyleFile, CS);
                        }
                        else
                        {
                            el = VisualStyleElement.Window.SmallCaption.GetElement(_VisualStyleFile, CS);
                        }

                        break;
                    }

                case Element.RightEdge:
                    {
                        // Get the visual style element for the right edge and the frame state.
                        WindowFrameState FS = Active ? WindowFrameState.Active : WindowFrameState.Inactive;

                        if (!ToolWindow)
                        {
                            el = VisualStyleElement.Window.FrameRight.GetElement(_VisualStyleFile, FS);
                        }
                        else
                        {
                            el = VisualStyleElement.Window.SmallFrameRight.GetElement(_VisualStyleFile, FS);
                        }

                        break;
                    }

                case Element.LeftEdge:
                    {
                        // Get the visual style element for the left edge and the frame state.
                        WindowFrameState FS = Active ? WindowFrameState.Active : WindowFrameState.Inactive;

                        if (!ToolWindow)
                        {
                            el = VisualStyleElement.Window.FrameLeft.GetElement(_VisualStyleFile, FS);
                        }
                        else
                        {
                            el = VisualStyleElement.Window.SmallFrameLeft.GetElement(_VisualStyleFile, FS);
                        }

                        break;
                    }

                case Element.BottomEdge:
                    {
                        // Get the visual style element for the bottom edge and the frame state.
                        WindowFrameState FS = Active ? WindowFrameState.Active : WindowFrameState.Inactive;

                        if (!ToolWindow)
                        {
                            el = VisualStyleElement.Window.FrameBottom.GetElement(_VisualStyleFile, FS);
                        }
                        else
                        {
                            el = VisualStyleElement.Window.SmallFrameBottom.GetElement(_VisualStyleFile, FS);
                        }

                        break;
                    }

                case Element.CloseButton:
                    {
                        // Get the visual style element for the close button and the button state.
                        WindowButtonState BS = Active ? WindowButtonState.Normal : WindowButtonState.Disabled;

                        if (!ToolWindow)
                        {
                            el = VisualStyleElement.Window.CloseButton.GetElement(_VisualStyleFile, BS);
                        }
                        else
                        {
                            el = VisualStyleElement.Window.SmallCloseButton.GetElement(_VisualStyleFile, BS);
                        }

                        break;
                    }

                case Element.Taskbar:
                    {
                        // Get the visual style element for the taskbar.
                        el = VisualStyleElement.TaskBar.BackgroundBottom.GetElement(_VisualStyleFile);
                        break;
                    }

                default:
                    {
                        // The element is not supported.
                        el = null;
                        break;
                    }
            }

            // Draw the visual style element.
            // There is a bug in Devcorp.Controls.VisualStyles.dll when a classic theme is running, it throws an exception.
            if (el is not null && !Program.ClassicThemeRunning)
            {
                try
                {
                    // Draw the visual style element.
                    VisualStyleRenderer renderer = new(el);
                    renderer.DrawBackground(G, Rectangle);
                }
                catch { } // Couldn't draw the visual style, may be a classic theme is enabled or the visual style is not supported.
            }
        }

        /// <summary>
        /// Disposes the visual style file.
        /// </summary>
        public void Dispose()
        {
            _VisualStyleFile?.Dispose();
        }
    }
}