using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AnimatorNS
{
    /// <summary>
    /// Settings of animation
    /// </summary>
    public class Animation
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Advanced), TypeConverter(typeof(PointFConverter))]
        public PointF SlideCoeff { get; set; } = PointF.Empty;
        public float TransparencyCoeff { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Advanced), TypeConverter(typeof(PointFConverter))]
        public PointF BlindCoeff { get; set; } = PointF.Empty;

        public float BlurRadius { get; set; } // Add this property

        public float TimeCoeff { get; set; }
        public float MinTime { get; set; } = 0f;
        public float MaxTime { get; set; } = 1f;
        public Padding Padding { get; set; } = new Padding(0);
        public bool AnimateOnlyDifferences { get; set; } = true;

        public bool IsNonLinearTransformNeeded
        {
            get
            {
                return !(BlindCoeff == PointF.Empty && TransparencyCoeff == 0f && BlurRadius == 0f);
            }
        }

        public Animation Clone()
        {
            return (Animation)MemberwiseClone();
        }

        public static readonly Animation HorizSlide = new Animation { SlideCoeff = new PointF(1, 0) };
        public static readonly Animation VertSlide = new Animation { SlideCoeff = new PointF(0, 1) };
        public static readonly Animation Transparent = new Animation { TransparencyCoeff = 1 };
        public static readonly Animation VertBlind = new Animation { BlindCoeff = new PointF(0f, 1f) };
        public static readonly Animation HorizBlind = new Animation { BlindCoeff = new PointF(1f, 0f) };
        public static readonly Animation Blur = new Animation { BlurRadius = 5f };
    }

    public enum AnimationType
    {
        Custom = 0,
        HorizSlide,
        VertSlide,
        Transparent,
        VertBlind,
        HorizBlind,
        Blur
    }
}