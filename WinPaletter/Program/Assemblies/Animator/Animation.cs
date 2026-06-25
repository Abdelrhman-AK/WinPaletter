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
        private static EasingType _defaultEasingType = EasingType.QuinticInOut;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Advanced), TypeConverter(typeof(PointFConverter))]
        public PointF SlideCoeff { get; set; } = PointF.Empty;
        public float TransparencyCoeff { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Advanced), TypeConverter(typeof(PointFConverter))]
        public PointF BlindCoeff { get; set; } = PointF.Empty;

        /// <summary>
        /// Zoom coefficient: 0 = no zoom, 1 = full zoom effect
        /// </summary>
        public float ZoomCoeff { get; set; } = 0f;

        /// <summary>
        /// Minimum scale for zoom animation (0.3f = 30% of original size)
        /// </summary>
        public float MinZoomScale { get; set; } = 0.7f;

        /// <summary>
        /// Maximum scale for zoom animation (1.3f = 130% of original size)
        /// </summary>
        public float MaxZoomScale { get; set; } = 1.3f;

        public float BlurRadius { get; set; } = 5f;

        public float TimeCoeff { get; set; }
        public float MinTime { get; set; } = 0f;
        public float MaxTime { get; set; } = 1f;
        public Padding Padding { get; set; } = new Padding(0);
        public bool AnimateOnlyDifferences { get; set; } = true;

        [DefaultValue(EasingType.QuinticInOut)]
        public EasingType EasingType { get; set; } = _defaultEasingType;

        /// <summary>
        /// Gets the easing function for this animation
        /// </summary>
        public Easing.EasingFunction EasingFunction => EasingType.ToEasingFunction();

        public bool IsNonLinearTransformNeeded
        {
            get
            {
                return !(BlindCoeff == PointF.Empty && TransparencyCoeff == 0f && BlurRadius == 0f && ZoomCoeff == 0f);
            }
        }

        public static readonly Animation HorizSlide = new() { SlideCoeff = new PointF(1, 0), EasingType = _defaultEasingType };
        public static readonly Animation VertSlide = new() { SlideCoeff = new PointF(0, 1), EasingType = _defaultEasingType };

        /// <summary>
        /// Fade animation without zoom (original behavior)
        /// </summary>
        public static readonly Animation Fade = new()
        {
            TransparencyCoeff = 1,
            ZoomCoeff = 0f,
            EasingType = _defaultEasingType
        };

        /// <summary>
        /// Fade animation with zoom effect
        /// Show: zoom in from 105% to 100% while fading in
        /// Hide: zoom out from 100% to 105% while fading out
        /// </summary>
        public static readonly Animation FadeZoom = new()
        {
            TransparencyCoeff = 1,
            ZoomCoeff = 1f,
            MaxZoomScale = 1.05f,
            EasingType = _defaultEasingType
        };

        public static readonly Animation VertBlind = new() { BlindCoeff = new PointF(0f, 1f), EasingType = _defaultEasingType };
        public static readonly Animation HorizBlind = new() { BlindCoeff = new PointF(1f, 0f), EasingType = _defaultEasingType };
    }

    public enum AnimationType
    {
        Custom = 0,
        HorizSlide,
        VertSlide,
        Fade,
        FadeZoom,
        VertBlind,
        HorizBlind
    }
}