using System.Drawing;
using System.Drawing.Text;

namespace WinPaletter.UI.Style
{
    public partial class Config
    {
        public Config(Color Accent, Color Secondary, Color Tertiary, Color Disabled, Color BackColor, Color DisabledBackColor, bool Dark, bool Rounded, bool EnableAnimations)
        {
            DarkMode = Dark;
            RoundedCorners = Rounded;
            Radius = 4;
            Animations = EnableAnimations;

            Schemes.Main = new(Accent, BackColor, Dark);
            Schemes.Secondary = new(Secondary, BackColor, Dark);
            Schemes.Tertiary = new(Tertiary, BackColor, Dark);
            Schemes.Disabled = new(Disabled, DisabledBackColor, Dark);

            if (Program.Animator != null)
            {
                if (Animations)
                {
                    Program.Animator.AnimationType = AnimatorNS.AnimationType.Transparent;
                    Program.Animator.Interval = 1;
                    Program.Animator.TimeStep = 0.07f;
                }
                else
                {
                    Program.Animator.AnimationType = AnimatorNS.AnimationType.Custom;
                    Program.Animator.Interval = 2;
                    Program.Animator.TimeStep = 1f;
                }
            }
        }

        /// <summary>
        /// Used to make custom controls follow Manager's font smoothing
        /// </summary>
        public TextRenderingHint RenderingHint { get; set; } = TextRenderingHint.SystemDefault;

        public bool DarkMode { get; set; } = true;

        public bool RoundedCorners { get; set; } = true;

        public readonly int Radius = 4;

        public Schemes_Collection Schemes = new();

        public bool Animations = false;
    }
}