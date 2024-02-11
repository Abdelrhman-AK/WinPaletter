using Devcorp.Controls.VisualStyles;
using System;
using System.Drawing;

namespace WinPaletter
{
    public class VisualStylesRes : IDisposable
    {
        public VisualStylesRes(string themeFile)
        {
            _VisualStyleFile = new(themeFile);
            try
            {
                Colors = _VisualStyleFile.Metrics.Colors;
            }
            catch { }

            try
            {
                Metrics = _VisualStyleFile.Metrics.Sizes;
            }
            catch { }
        }

        private VisualStyleFile _VisualStyleFile;
        public VisualStyleMetricColors Colors { get; set; } = new();
        public VisualStyleMetricSizes Metrics { get; set; } = new();

        public enum Element
        {
            Titlebar,
            RightEdge,
            LeftEdge,
            BottomEdge,
            Taskbar,
            CloseButton
        }

        public void Draw(Graphics G, Rectangle Rectangle, Element element, bool Active, bool ToolWindow)
        {
            VisualStyleElement el;

            switch (element)
            {
                case Element.Titlebar:
                    {
                        Rectangle.Offset(1, 1);
                        Rectangle.Inflate(0, 1);

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
                        el = VisualStyleElement.TaskBar.BackgroundBottom.GetElement(_VisualStyleFile);
                        break;
                    }

                default:
                    {
                        el = null;
                        break;
                    }

            }

            try
            {
                VisualStyleRenderer renderer = new(el);
                renderer.DrawBackground(G, Rectangle);
            }
            catch { }

        }

        public void Dispose()
        {
            _VisualStyleFile?.Dispose();
        }
    }
}