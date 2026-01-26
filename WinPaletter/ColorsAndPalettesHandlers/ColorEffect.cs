using Cyotek.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using WinPaletter.UI.Controllers;

namespace WinPaletter
{
    /// <summary>
    /// Represents a configurable color effect that can be applied to colors, with support for intensity adjustment,
    /// preview images, and event handling.
    /// </summary>
    /// <remarks>A <see cref="ColorEffect"/> defines a transformation on colors using a delegate function,
    /// with optional intensity control via a scrollbar.  It includes properties for UI integration, such as display
    /// name, description, and preview images. The effect can be toggled on or off  using the <see cref="Checked"/>
    /// property, and changes to this property raise the <see cref="OnCheckedChanged"/> event.</remarks>
    public class ColorEffect
    {
        /// <summary>
        /// Gets the list of currently registered color effects.
        /// </summary>
        public static List<ColorEffect> RegisteredEffects => _registeredEffects;

        /// <summary>
        /// A list of all registered color effects.
        /// </summary>
        private static readonly List<ColorEffect> _registeredEffects =
        [
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Reverse,
                Description = Program.Localization.Strings.ColorEffects.Reverse_Description,
                Image = Assets.ColorEffects.Reverse_48,
                SmallImage = Assets.ColorEffects.Reverse_24,
                Effect = (c, v, c_sec) => ColorsExtensions.Reverse(c)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Brightness,
                Description = Program.Localization.Strings.ColorEffects.Brightness_Description,
                Image = Assets.ColorEffects.Brightness_48,
                SmallImage = Assets.ColorEffects.Brightness_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 200f,
                ScrollbarValue = 100,
                DefaultValue = 100,
                Effect = (c, v, c_sec) => ColorsExtensions.CB(c, (v - 100f) / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Darken,
                Description = Program.Localization.Strings.ColorEffects.Darken_Description,
                Image = Assets.ColorEffects.Dark_48,
                SmallImage = Assets.ColorEffects.Dark_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 30f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Dark(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Lighten,
                Description = Program.Localization.Strings.ColorEffects.Lighten_Description,
                Image = Assets.ColorEffects.Light_48,
                SmallImage = Assets.ColorEffects.Light_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 30f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Light(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Invert,
                Description = Program.Localization.Strings.ColorEffects.Invert_Description,
                Image = Assets.ColorEffects.Invert_48,
                SmallImage = Assets.ColorEffects.Invert_24,
                Effect = (c, v, c_sec) => ColorsExtensions.Invert(c)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Grayscale,
                Description =Program.Localization.Strings.ColorEffects.Grayscale_Description,
                Image = Assets.ColorEffects.Grayscale_48,
                SmallImage = Assets.ColorEffects.Grayscale_24,
                Effect = (c, v, c_sec) => ColorsExtensions.Grayscale(c)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Sepia,
                Description = Program.Localization.Strings.ColorEffects.Sepia_Description,
                Image = Assets.ColorEffects.Sepia_48,
                SmallImage = Assets.ColorEffects.Sepia_24,
                Effect = (c, v, c_sec) => ColorsExtensions.Sepia(c)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.HueRotation,
                Description = Program.Localization.Strings.ColorEffects.HueRotation_Description,
                Image = Assets.ColorEffects.Hue_48,
                SmallImage = Assets.ColorEffects.Hue_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 360f,
                ScrollbarValue = 30f,
                DefaultValue = 0,
                Effect = (c, v, c_sec) => ColorsExtensions.RotateHue(c, v)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Saturate,
                Description = Program.Localization.Strings.ColorEffects.Saturate_Description,
                Image = Assets.ColorEffects.Saturation_48,
                SmallImage = Assets.ColorEffects.Saturation_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 300f,
                ScrollbarValue = 120f,
                DefaultValue = 1f,
                Effect = (c, v, c_sec) => ColorsExtensions.Saturate(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Cool,
                Description = Program.Localization.Strings.ColorEffects.Cool_Description,
                Image = Assets.ColorEffects.Cold_48,
                SmallImage = Assets.ColorEffects.Cold_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 50f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Cool(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Hot,
                Description = Program.Localization.Strings.ColorEffects.Hot_Description,
                Image = Assets.ColorEffects.Hot_48,
                SmallImage = Assets.ColorEffects.Hot_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 30f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Hot(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Sunset,
                Description = Program.Localization.Strings.ColorEffects.Sunset_Description,
                Image = Assets.ColorEffects.Sunset_48,
                SmallImage = Assets.ColorEffects.Sunset_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 50f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Sunset(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Night,
                Description = Program.Localization.Strings.ColorEffects.Night_Description,
                Image = Assets.ColorEffects.Night_48,
                SmallImage = Assets.ColorEffects.Night_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 60f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Night(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Filmic,
                Description = Program.Localization.Strings.ColorEffects.Filmic_Description,
                Image = Assets.ColorEffects.Filmic_48,
                SmallImage = Assets.ColorEffects.Filmic_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 60f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Filmic(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Vintage,
                Description = Program.Localization.Strings.ColorEffects.Vintage_Description,
                Image = Assets.ColorEffects.Vintage_48,
                SmallImage = Assets.ColorEffects.Vintage_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 60f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Vintage(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Noir,
                Description = Program.Localization.Strings.ColorEffects.Noir_Description,
                Image = Assets.ColorEffects.Noir_48,
                SmallImage = Assets.ColorEffects.Noir_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 90f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Noir(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Pastel,
                Description = Program.Localization.Strings.ColorEffects.Pastel_Description,
                Image = Assets.ColorEffects.Pastel_48,
                SmallImage = Assets.ColorEffects.Pastel_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 60f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Pastel(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.PopArt,
                Description = Program.Localization.Strings.ColorEffects.PopArt_Description,
                Image = Assets.ColorEffects.PopArt_48,
                SmallImage = Assets.ColorEffects.PopArt_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 90f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.PopArt(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Faded,
                Description = Program.Localization.Strings.ColorEffects.Faded_Description,
                Image = Assets.ColorEffects.Faded_48,
                SmallImage = Assets.ColorEffects.Faded_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 50f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Faded(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Matte,
                Description = Program.Localization.Strings.ColorEffects.Matte_Description,
                Image = Assets.ColorEffects.Matte_48,
                SmallImage = Assets.ColorEffects.Matte_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 45f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Matte(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Fog,
                Description = Program.Localization.Strings.ColorEffects.Fog_Description,
                Image = Assets.ColorEffects.Fog_48,
                SmallImage = Assets.ColorEffects.Fog_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 35f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Fog(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Underwater,
                Description = Program.Localization.Strings.ColorEffects.Underwater_Description,
                Image = Assets.ColorEffects.Underwater_48,
                SmallImage = Assets.ColorEffects.Underwater_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 70f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Underwater(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.ThermalVision,
                Description = Program.Localization.Strings.ColorEffects.ThermalVision_Description,
                Image = Assets.ColorEffects.ThermalVision_48,
                SmallImage = Assets.ColorEffects.ThermalVision_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 100f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.ThermalVision(c, (100f - v) / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Infrared,
                Description = Program.Localization.Strings.ColorEffects.Infrared_Description,
                Image = Assets.ColorEffects.IR_48,
                SmallImage = Assets.ColorEffects.IR_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 100f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Infrared(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.XRay,
                Description = Program.Localization.Strings.ColorEffects.XRay_Description,
                Image = Assets.ColorEffects.XRay_48,
                SmallImage = Assets.ColorEffects.XRay_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 100f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.XRay(c, v / 100f)
            },
            new()
            {
                Name =Program.Localization.Strings.ColorEffects.GlowBoost,
                Description = Program.Localization.Strings.ColorEffects.GlowBoost_Description,
                Image = Assets.ColorEffects.Glow_48,
                SmallImage = Assets.ColorEffects.Glow_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 60f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.GlowBoost(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Blend,
                Description = Program.Localization.Strings.ColorEffects.Blend_Description,
                Image = Assets.ColorEffects.Blend_48,
                SmallImage = Assets.ColorEffects.Blend_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 50f,
                SecondaryColor = Color.Red,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Blend(c, (Color)c_sec, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.ShadowTint,
                Description = Program.Localization.Strings.ColorEffects.ShadowTint_Description,
                Image = Assets.ColorEffects.Shadow_48,
                SmallImage = Assets.ColorEffects.Shadow_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 35f,
                SecondaryColor = Color.Red,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.ShadowTint(c, (Color)c_sec, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.HighlightTint,
                Description = Program.Localization.Strings.ColorEffects.HighlightTint_Description,
                Image = Assets.ColorEffects.Highlight_48,
                SmallImage = Assets.ColorEffects.Highlight_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 35f,
                SecondaryColor = Color.Red,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.HighlightTint(c, (Color)c_sec, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Vibrance,
                Description = Program.Localization.Strings.ColorEffects.Vibrance_Description,
                Image = Assets.ColorEffects.Vibrance_48,
                SmallImage = Assets.ColorEffects.Vibrance_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 300f,
                ScrollbarValue = 120f,
                DefaultValue = 100f,
                Effect = (c, v, c_sec) => ColorsExtensions.Vibrance(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.SoftLight,
                Description = Program.Localization.Strings.ColorEffects.SoftLight_Description,
                Image = Assets.ColorEffects.SoftLight_48,
                SmallImage = Assets.ColorEffects.SoftLight_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 50f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.SoftLight(c, (Color)c_sec, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.HardLight,
                Description = Program.Localization.Strings.ColorEffects.HardLight_Description,
                Image = Assets.ColorEffects.HardLight_48,
                SmallImage = Assets.ColorEffects.HardLight_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 50f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.HardLight(c, (Color)c_sec, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Posterize,
                Description = Program.Localization.Strings.ColorEffects.Posterize_Description,
                Image = Assets.ColorEffects.Posterize_48,
                SmallImage = Assets.ColorEffects.Posterize_24,
                HasScrollbar = true,
                ScrollbarMin = 2f,
                ScrollbarMax = 64f,
                ScrollbarValue = 8f,
                DefaultValue = 2f,
                Effect = (c, v, c_sec) => ColorsExtensions.Posterize(c, (int)v)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Monochrome,
                Description = Program.Localization.Strings.ColorEffects.Monochrome_Description,
                Image = Assets.ColorEffects.Monochrome_48,
                SmallImage = Assets.ColorEffects.Monochrome_24,
                HasScrollbar = true,
                ScrollbarMin = 0f,
                ScrollbarMax = 100f,
                ScrollbarValue = 50f,
                DefaultValue = 50f,
                Effect = (c, v, c_sec) => ColorsExtensions.Monochrome(c, v / 100f)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.WebSafe,
                Description = Program.Localization.Strings.ColorEffects.WebSafe_Description,
                Image = Assets.ColorEffects.IE_48,
                SmallImage = Assets.ColorEffects.IE_24,
                Effect = (c, v, c_sec) => ColorsExtensions.ToWebSafe(c)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Colors256,
                Description = Program.Localization.Strings.ColorEffects.Colors256_Description,
                Image = Assets.ColorEffects.Win9x_48,
                SmallImage = Assets.ColorEffects.Win9x_24,
                Effect = (c, v, c_sec) => ColorsExtensions.To256Color(c)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.FrutigerAero,
                Description = Program.Localization.Strings.ColorEffects.FrutigerAero_Description,
                Image = Assets.ColorEffects.FrutigerAero_48,
                SmallImage = Assets.ColorEffects.FrutigerAero_24,
                Effect = (c, v, c_sec) => ColorsExtensions.ToFrutigerAero(c)
            },
            new()
            {
                Name = "2016",
                Description = Program.Localization.Strings.ColorEffects.Photo2016_Description,
                Image = Assets.ColorEffects._2016_48,
                SmallImage = Assets.ColorEffects._2016_24,
                Effect = (c, v, c_sec) => ColorsExtensions.Photo2016(c)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.Metro,
                Description = Program.Localization.Strings.ColorEffects.Metro_Description,
                Image = Assets.ColorEffects.Metro_48,
                SmallImage = Assets.ColorEffects.Metro_24,
                Effect = (c, v, c_sec) => ColorsExtensions.ToMetro(c)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.MaterialDesign,
                Description = Program.Localization.Strings.ColorEffects.MaterialDesign_Description,
                Image = Assets.ColorEffects.Android_48,
                SmallImage = Assets.ColorEffects.Android_24,
                Effect = (c, v, c_sec) => ColorsExtensions.ToMaterial(c)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.MaterialDesignExpressive3,
                Description = Program.Localization.Strings.ColorEffects.MaterialDesignExpressive3_Description,
                Image = Assets.ColorEffects.AndroidME3_48,
                SmallImage = Assets.ColorEffects.AndroidME3_24,
                Effect = (c, v, c_sec) => ColorsExtensions.ToMaterialExpressive3(c)
            },
            new()
            {
                Name = Program.Localization.Strings.ColorEffects.MacOSSemantic,
                Description = Program.Localization.Strings.ColorEffects.MacOSSemantic_Description,
                Image = Assets.ColorEffects.Mac_48,
                SmallImage = Assets.ColorEffects.Mac_24,
                Effect = (c, v, c_sec) => ColorsExtensions.ToMacSemantic(c)
            }
        ];

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorEffect"/> class.
        /// </summary>
        public ColorEffect()
        {

        }

        /// <summary>
        /// Retrieves a color effect by its name.
        /// </summary>
        public static ColorEffect Get(string name)
        {
            return RegisteredEffects.Find(e => string.Equals(e.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets or sets the name of the color effect.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A short description for UI tooltips or lists.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the default value used when no specific value is provided.
        /// </summary>
        public float DefaultValue { get; set; }

        /// <summary>
        /// Large preview image (e.g. in gallery view).
        /// </summary>
        public Bitmap Image { get; set; }

        /// <summary>
        /// Small icon image (e.g. in toolbar or menu).
        /// </summary>
        public Bitmap SmallImage { get; set; }

        /// <summary>
        /// Whether this effect is currently enabled.
        /// </summary>
        public bool Checked
        {
            get => _checked;
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    OnCheckedChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        private bool _checked;

        /// <summary>
        /// Fired when the Checked property changes.
        /// </summary>
        public event EventHandler OnCheckedChanged;

        /// <summary>
        /// Whether this effect uses a scrollbar (intensity slider).
        /// </summary>
        public bool HasScrollbar { get; set; }

        /// <summary>
        /// Current scrollbar value (intensity).
        /// Changing this value raises the <see cref="OnScrollbarValueChanged"/> event.
        /// </summary>
        public float ScrollbarValue
        {
            get => _scrollbarValue;
            set
            {
                float clamped = Math.Min(Math.Max(value, ScrollbarMin), ScrollbarMax);
                if (Math.Abs(_scrollbarValue - clamped) > float.Epsilon)
                {
                    _scrollbarValue = clamped;
                    OnScrollbarValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        private float _scrollbarValue;

        /// <summary>
        /// Fired when the <see cref="ScrollbarValue"/> property changes.
        /// </summary>
        public event EventHandler OnScrollbarValueChanged;

        /// <summary>
        /// Color to be processed by effect
        /// </summary>
        public Color? InputColor { get; set; }

        /// <summary>
        /// A second color to be processed by effect
        /// </summary>
        public Color SecondaryColor { get; set; } = Color.Empty;

        /// <summary>
        /// Minimum scrollbar value.
        /// </summary>
        public float ScrollbarMin { get; set; } = 0f;

        /// <summary>
        /// Maximum scrollbar value.
        /// </summary>
        public float ScrollbarMax { get; set; } = 1f;

        /// <summary>
        /// Gets or sets the function that applies an effect to a color based on the specified parameters.
        /// </summary>
        /// <remarks>The function takes the following parameters: <list type="bullet">
        /// <item><description>The base color to which the effect is applied.</description></item> <item><description>A
        /// float value that may influence the effect's intensity or behavior.</description></item>
        /// <item><description>An optional secondary color that may be used in the effect
        /// calculation.</description></item> </list> The returned color represents the result of the effect. Ensure the
        /// function is thread-safe if accessed concurrently.</remarks>
        public Func<Color, float, Color?, Color> Effect { get; set; }

        public Color OutputColor => Apply(InputColor ?? Color.Empty);

        /// <summary>
        /// Applies the configured effect to the specified color if the effect is enabled and checked.
        /// </summary>
        /// <param name="color">The input <see cref="Color"/> to which the effect will be applied.</param>
        /// <returns>The resulting <see cref="Color"/> after applying the effect.  If no effect is configured or the effect is
        /// unchecked, the original <paramref name="color"/> is returned.</returns>
        public Color Apply(Color color)
        {
            if (Effect == null) return color;

            // If effect is unchecked, just return the original color
            if (!Checked) return color;

            return Effect.Invoke(color, ScrollbarValue, SecondaryColor);
        }

        /// <summary>
        /// Applies the effect to the specified color using the given value.
        /// </summary>
        /// <remarks>The effect is applied only if the <c>Effect</c> property is set and the
        /// <c>Checked</c> property is <see langword="true"/>.</remarks>
        /// <param name="color">The <see cref="Color"/> to which the effect will be applied.</param>
        /// <param name="value">A floating-point value that influences the effect's behavior.</param>
        /// <returns>The resulting <see cref="Color"/> after applying the effect.  If the effect is not set or is unchecked, the
        /// original <paramref name="color"/> is returned.</returns>
        public Color Apply(Color color, float value)
        {
            if (Effect == null) return color;

            // If effect is unchecked, just return the original color
            if (!Checked) return color;

            ScrollbarValue = value;

            return Effect.Invoke(color, ScrollbarValue, SecondaryColor);
        }

        /// <summary>
        /// Applies the specified effect to the given color based on the provided parameters.
        /// </summary>
        /// <remarks>The behavior of the method depends on the <c>Effect</c> delegate and the
        /// <c>Checked</c> property: <list type="bullet"> <item>If <c>Effect</c> is <see langword="null"/>, the method
        /// returns the original <paramref name="color"/>.</item> <item>If <c>Checked</c> is <see langword="false"/>,
        /// the method returns the original <paramref name="color"/> without invoking the effect.</item> <item>If both
        /// <c>Effect</c> is set and <c>Checked</c> is <see langword="true"/>, the effect is invoked with the provided
        /// parameters.</item> </list></remarks>
        /// <param name="color">The primary color to which the effect will be applied.</param>
        /// <param name="color_second">An additional color that may be used by the effect, depending on its implementation.</param>
        /// <param name="value">A numeric value that influences the effect's behavior. The interpretation of this value depends on the
        /// effect.</param>
        /// <returns>The resulting <see cref="Color"/> after applying the effect. If the effect is not set or is unchecked, the
        /// original <paramref name="color"/> is returned.</returns>
        public Color Apply(Color color, Color color_second, float value)
        {
            if (Effect == null) return color;

            // If effect is unchecked, just return the original color
            if (!Checked) return color;

            return Effect.Invoke(color, value, color_second);
        }

        /// <summary>
        /// Creates a deep copy of the current <see cref="ColorEffect"/> instance.
        /// </summary>
        /// <returns>A new <see cref="ColorEffect"/> object that is a deep copy of this instance.</returns>
        public ColorEffect Clone()
        {
            return FastCloner.FastCloner.DeepClone(this);
        }
    }
}
