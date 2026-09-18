using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static WinPaletter.Theme.Structures.WinTerminal.Types;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Class that has data of Windows Terminal settings
    /// </summary>
    public class WinTerminal : ManagerBase<WinTerminal>
    {
        /// <summary>
        /// Enumeration for ways by which WinPaletter can get Windows Terminal settings data
        /// </summary>
        public enum Mode : int
        {
            /// <summary>EmptyError data that has nothing; no profiles, no themes, ...</summary>
            Empty,
            /// <summary>Default Windows Terminal settings</summary>
            Default,
            /// <summary>Windows Terminal JSON settings File</summary>
            JSONFile,
            /// <summary>WinPaletter theme  File</summary>
            WinPaletterFile,
        }

        /// <summary>
        /// Enumeration of Windows Terminal editions
        /// </summary>
        public enum Version : int
        {
            /// <summary></summary>
            Stable,
            /// <summary></summary>
            Preview
        }

        #region Properties

        [JsonIgnore]
        private string _signature;

        [JsonIgnore]
        private string signatureEnabled => $"{_signature}_Enabled";

        /// <summary>
        /// Controls if this feature is enabled or not
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// Gets or sets the default profile.
        /// </summary>
        [JsonProperty("defaultProfile")]
        public string DefaultProfile { get; set; }

        /// <summary>
        /// Gets or sets the profiles for the terminal.
        /// </summary>
        [JsonProperty("profiles")]
        public Profiles Profiles { get; set; } = new();

        /// <summary>
        /// Gets or sets the color schemes for the terminal.
        /// </summary>
        [JsonProperty("schemes")]
        public List<Scheme> Schemes { get; set; } = [];

        /// <summary>
        /// Gets or sets the default theme for the terminal.
        /// </summary>
        [JsonProperty("theme")]
        public string Theme { get; set; }

        /// <summary>
        /// Gets or sets the list of available themes for the terminal.
        /// </summary>
        [JsonProperty("themes")]
        public List<Types.Theme> Themes { get; set; } = [];

        /// <summary>
        /// Gets or sets whether to use acrylic in the tab row.
        /// </summary>
        [JsonProperty("useAcrylicInTabRow")]
        public bool UseAcrylicInTabRow { get; set; } = false;
        #endregion

        #region Construction and methods

        /// <summary>
        /// Types, convertors and enums related to and managing Windows Terminal JSON structure
        /// </summary>
        public class Types
        {
            /// <summary>
            /// Represents the profiles for the terminal.
            /// </summary>
            public class Profiles : ICloneable
            {
                /// <summary>
                /// Gets or sets the default profile settings. Initialised to the Windows Terminal
                /// built-in default scheme (Campbell), mirroring Windows Terminal's own runtime
                /// defaults, so serialisation always produces a real scheme name rather than null.
                /// </summary>
                [JsonProperty("defaults")]
                public Profile Defaults { get; set; } = new()
                {
                    ColorScheme = new ColorScheme { Dark = "Campbell", Light = "Campbell" }
                };

                /// <summary>
                /// Gets or sets the list of profiles.
                /// </summary>
                [JsonProperty("list")]
                public List<Profile> List { get; set; } = [];

                /// <summary>
                /// Clone current Windows Terminal profiles
                /// </summary>
                /// <returns></returns>
                public object Clone()
                {
                    return new Profiles
                    {
                        Defaults = Defaults.Clone() as Profile,
                        List = [.. List.Select(p => p.Clone() as Profile)]
                    };
                }
            }

            /// <summary>
            /// Represents a profile for the terminal.
            /// </summary>
            public class Profile : ICloneable
            {
                /// <summary>
                /// Gets or sets the name of the profile.
                /// </summary>
                [JsonProperty("name")]
                public string Name { get; set; }

                /// <summary>
                /// Gets or sets profile's GUID.
                /// </summary>
                [JsonProperty("guid")]
                public string Guid { get; set; }

                /// <summary>
                /// Gets or sets the tab color for the profile.
                /// </summary>
                [JsonProperty("tabColor", NullValueHandling = NullValueHandling.Ignore)]
                [JsonConverter(typeof(ColorConverter))]
                public Color TabColor { get; set; }

                /// <summary>
                /// Gets or sets the background image for the profile.
                /// </summary>
                [JsonProperty("backgroundImage")]
                public string BackgroundImage { get; set; } = string.Empty;

                /// <summary>
                /// Gets or sets the cursor shape for the profile.
                /// </summary>
                [JsonProperty("cursorShape")]
                [JsonConverter(typeof(CursorShapeConverter))]
                public CursorShape CursorShape { get; set; } = CursorShape.Bar;

                /// <summary>
                /// Gets or sets the color scheme for the profile.
                /// </summary>
                [JsonProperty("colorScheme")]
                [JsonConverter(typeof(ColorSchemeConverter))]
                public ColorScheme ColorScheme { get; set; }

                /// <summary>
                /// Gets or sets the cursor height for the profile.
                /// </summary>
                [JsonProperty("cursorHeight")]
                public int CursorHeight { get; set; } = 25;

                /// <summary>
                /// Gets or sets the opacity for the profile.
                /// </summary>
                [JsonProperty("opacity")]
                public int Opacity { get; set; } = 100;

                /// <summary>
                /// Gets or sets the background image opacity for the profile.
                /// </summary>
                [JsonProperty("backgroundImageOpacity")]
                [JsonConverter(typeof(BackgroundImageOpacityConverter))]
                public double BackgroundImageOpacity
                {
                    get => backgroundImageOpacity * 100;
                    set => backgroundImageOpacity = Math.Max(0, Math.Min(value / 100, 1));
                }
                private double backgroundImageOpacity;

                /// <summary>
                /// Gets or sets whether to use acrylic in the profile.
                /// </summary>
                [JsonProperty("useAcrylic")]
                public bool UseAcrylic { get; set; } = false;

                /// <summary>
                /// Gets or sets the font settings for the profile.
                /// </summary>
                [JsonProperty("font")]
                public FontSettings Font { get; set; } = new();

                /// <summary>
                /// Gets or sets the command line for the profile.
                /// </summary>
                [JsonProperty("commandline")]
                public string Commandline { get; set; } = string.Empty;

                /// <summary>
                /// Gets or sets the icon for the profile.
                /// </summary>
                [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
                public string Icon { get; set; } = null;

                /// <summary>
                /// Gets or sets the tab title for the profile.
                /// </summary>
                [JsonProperty("tabTitle", NullValueHandling = NullValueHandling.Ignore)]
                public string TabTitle { get; set; } = null;

                /// <summary>
                /// Clone Windows Terminal profile
                /// </summary>
                /// <returns></returns>
                public object Clone()
                {
                    return new Profile
                    {
                        Name = Name,
                        Guid = Guid,
                        TabColor = TabColor,
                        BackgroundImage = BackgroundImage,
                        CursorShape = CursorShape,
                        ColorScheme = ColorScheme,
                        CursorHeight = CursorHeight,
                        Opacity = Opacity,
                        BackgroundImageOpacity = BackgroundImageOpacity,
                        UseAcrylic = UseAcrylic,
                        Font = Font.Clone() as FontSettings,
                        Commandline = Commandline,
                        Icon = Icon,
                        TabTitle = TabTitle
                    };
                }
            }

            /// <summary>
            /// Represents the color scheme for the terminal.
            /// </summary>
            public class Scheme
            {
                /// <summary>
                /// Gets or sets the name of the scheme.
                /// </summary>
                [JsonProperty("name")]
                public string Name { get; set; } = string.Empty;

                /// <summary>
                /// Gets or sets the background color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("background")]
                public Color Background { get; set; } = Color.FromArgb(12, 12, 12);

                /// <summary>
                /// Gets or sets the black color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("black")]
                public Color Black { get; set; } = Color.FromArgb(12, 12, 12);

                /// <summary>
                /// Gets or sets the blue color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("blue")]
                public Color Blue { get; set; } = Color.FromArgb(0, 55, 218);

                /// <summary>
                /// Gets or sets the bright black color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightBlack")]
                public Color BrightBlack { get; set; } = Color.FromArgb(118, 118, 118);

                /// <summary>
                /// Gets or sets the bright blue color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightBlue")]
                public Color BrightBlue { get; set; } = Color.FromArgb(59, 120, 255);

                /// <summary>
                /// Gets or sets the bright cyan color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightCyan")]
                public Color BrightCyan { get; set; } = Color.FromArgb(97, 214, 214);

                /// <summary>
                /// Gets or sets the bright green color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightGreen")]
                public Color BrightGreen { get; set; } = Color.FromArgb(22, 198, 12);

                /// <summary>
                /// Gets or sets the bright purple color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightPurple")]
                public Color BrightPurple { get; set; } = Color.FromArgb(180, 0, 158);

                /// <summary>
                /// Gets or sets the bright red color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightRed")]
                public Color BrightRed { get; set; } = Color.FromArgb(231, 72, 86);

                /// <summary>
                /// Gets or sets the bright white color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightWhite")]
                public Color BrightWhite { get; set; } = Color.FromArgb(242, 242, 242);

                /// <summary>
                /// Gets or sets the bright yellow color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightYellow")]
                public Color BrightYellow { get; set; } = Color.FromArgb(249, 241, 165);

                /// <summary>
                /// Gets or sets the cursor color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("cursorColor")]
                public Color CursorColor { get; set; } = Color.FromArgb(255, 255, 255);

                /// <summary>
                /// Gets or sets the cyan color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("cyan")]
                public Color Cyan { get; set; } = Color.FromArgb(58, 150, 221);

                /// <summary>
                /// Gets or sets the foreground color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("foreground")]
                public Color Foreground { get; set; } = Color.FromArgb(204, 204, 204);

                /// <summary>
                /// Gets or sets the green color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("green")]
                public Color Green { get; set; } = Color.FromArgb(19, 161, 14);

                /// <summary>
                /// Gets or sets the purple color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("purple")]
                public Color Purple { get; set; } = Color.FromArgb(136, 23, 152);

                /// <summary>
                /// Gets or sets the red color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("red")]
                public Color Red { get; set; } = Color.FromArgb(197, 15, 31);

                /// <summary>
                /// Gets or sets the selection background color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("selectionBackground")]
                public Color SelectionBackground { get; set; } = Color.FromArgb(255, 255, 255);

                /// <summary>
                /// Gets or sets the white color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("white")]
                public Color White { get; set; } = Color.FromArgb(204, 204, 204);

                /// <summary>
                /// Gets or sets the yellow color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("yellow")]
                public Color Yellow { get; set; } = Color.FromArgb(196, 156, 0);
            }

            /// <summary>
            /// Represents a color scheme with specific colors for dark and light themes.
            /// </summary>
            public class ColorScheme
            {
                /// <summary>
                /// Dark scheme name
                /// </summary>
                [JsonProperty("dark")]
                public string Dark { get; set; }

                /// <summary>
                /// Light scheme name
                /// </summary>
                [JsonProperty("light")]
                public string Light { get; set; }

                /// <summary>
                /// Create new color scheme pair from one color scheme
                /// </summary>
                /// <param name="color"></param>
                public static implicit operator ColorScheme(string color)
                {
                    return new ColorScheme { Dark = color, Light = color };
                }

                /// <summary>
                /// Returns a single string that identifies this color scheme. Prefers the pair's
                /// shared value when Dark and Light agree; falls back to whichever is set, and
                /// finally to the Windows Terminal built-in default ("Campbell") so callers never
                /// receive null.
                /// </summary>
                /// <returns></returns>
                public override string ToString()
                {
                    if (!string.IsNullOrWhiteSpace(Dark) && !string.IsNullOrWhiteSpace(Light) && Dark == Light)
                    {
                        return Dark;
                    }

                    return Light ?? Dark ?? "Campbell";
                }
            }

            /// <summary>
            /// Represents font settings.
            /// </summary>
            public class FontSettings : ICloneable
            {
                /// <summary>
                /// Gets or sets the font size.
                /// </summary>
                [JsonProperty("size")]
                public float Size { get; set; } = 12;

                /// <summary>
                /// Gets or sets the font face.
                /// </summary>
                [JsonProperty("face")]
                public string Face { get; set; } = "Cascadia Mono";

                /// <summary>
                /// Gets or sets the font weight.
                /// </summary>
                [JsonProperty("weight")]
                [JsonConverter(typeof(FontWeightConverter))]
                public FontWeight Weight { get; set; } = FontWeight.Normal;

                /// <summary>
                /// Clone Windows Terminal font settings
                /// </summary>
                /// <returns></returns>
                public object Clone()
                {
                    return new FontSettings
                    {
                        Size = Size,
                        Face = Face,
                        Weight = Weight
                    };
                }
            }

            /// <summary>
            /// Represents a theme with specific settings for the tab, tab row, and window.
            /// </summary>
            public class Theme
            {
                /// <summary>
                /// Gets or sets the name of the theme.
                /// </summary>
                [JsonProperty("name")]
                public string Name { get; set; } = string.Empty;

                /// <summary>
                /// Gets or sets the settings for the tab in the theme.
                /// </summary>
                [JsonProperty("tab")]
                public TabSettings Tab { get; set; } = new();

                /// <summary>
                /// Gets or sets the settings for the tab row in the theme.
                /// </summary>
                [JsonProperty("tabRow")]
                public TabRowSettings TabRow { get; set; } = new();

                /// <summary>
                /// Gets or sets the settings for the window in the theme.
                /// </summary>
                [JsonProperty("window")]
                public WindowSettings Window { get; set; } = new();
            }

            /// <summary>
            /// Represents the settings for the "window" in a theme.
            /// </summary>
            public class WindowSettings
            {
                /// <summary>
                /// Gets or sets the application theme for the window.
                /// </summary>
                [JsonProperty("applicationTheme")]
                public string ApplicationTheme { get; set; }
            }

            /// <summary>
            /// Represents the settings for the tab in a theme.
            /// </summary>
            public class TabSettings
            {
                /// <summary>
                /// Gets or sets the background color for the tab.
                /// </summary>
                [JsonProperty("background")]
                [JsonConverter(typeof(ColorConverter))]
                public Color Background { get; set; } = Color.Empty;

                /// <summary>
                /// Gets or sets the unfocused background color for the tab.
                /// </summary>
                [JsonProperty("unfocusedBackground")]
                [JsonConverter(typeof(ColorConverter))]
                public Color UnfocusedBackground { get; set; } = Color.Empty;
            }

            /// <summary>
            /// Represents the settings for the tab row in a theme.
            /// </summary>
            public class TabRowSettings
            {
                /// <summary>
                /// Gets or sets the background color for the tab row.
                /// </summary>
                [JsonProperty("background")]
                [JsonConverter(typeof(ColorConverter))]
                public Color Background { get; set; } = Color.Empty;

                /// <summary>
                /// Gets or sets the unfocused background color for the tab row.
                /// </summary>
                [JsonProperty("unfocusedBackground")]
                [JsonConverter(typeof(ColorConverter))]
                public Color UnfocusedBackground { get; set; } = Color.Empty;
            }

            #region Converters

            /// <summary>
            /// JSON converter for serializing and deserializing <see cref="ColorScheme"/> objects.
            /// </summary>
            private class ColorSchemeConverter : JsonConverter<ColorScheme>
            {
                /// <summary>
                /// Write values in JSON format. Never writes null - falls back to "Campbell" so the
                /// key is always present and cannot be silently dropped by the merge step.
                /// </summary>
                /// <param name="writer"></param>
                /// <param name="value"></param>
                /// <param name="serializer"></param>
                public override void WriteJson(JsonWriter writer, ColorScheme value, JsonSerializer serializer)
                {
                    string serialized = value?.ToString();

                    writer.WriteValue(string.IsNullOrWhiteSpace(serialized) ? "Campbell" : serialized);
                }

                /// <summary>
                /// Read values in JSON format and convert them to <see cref="ColorScheme"/> objects.
                /// Missing values fall back to Campbell so neither slot is ever null.
                /// </summary>
                /// <param name="reader"></param>
                /// <param name="objectType"></param>
                /// <param name="existingValue"></param>
                /// <param name="hasExistingValue"></param>
                /// <param name="serializer"></param>
                /// <returns></returns>
                public override ColorScheme ReadJson(JsonReader reader, Type objectType, ColorScheme existingValue, bool hasExistingValue, JsonSerializer serializer)
                {
                    if (reader.TokenType == JsonToken.String)
                    {
                        // If it's a string, create a ColorScheme with both dark and light set to the string value
                        string s = reader.Value?.ToString();
                        if (string.IsNullOrWhiteSpace(s)) s = "Campbell";
                        return new() { Dark = s, Light = s };
                    }
                    else if (reader.TokenType == JsonToken.StartObject)
                    {
                        // If it's an object, use the default deserialization for ColorScheme
                        ColorScheme result = serializer.Deserialize<ColorScheme>(reader) ?? new ColorScheme();

                        // Neither slot may end up null - fall back to the other slot, then to Campbell.
                        if (string.IsNullOrWhiteSpace(result.Dark)) result.Dark = string.IsNullOrWhiteSpace(result.Light) ? "Campbell" : result.Light;
                        if (string.IsNullOrWhiteSpace(result.Light)) result.Light = result.Dark;

                        return result;
                    }
                    else
                    {
                        // Missing colorScheme: inherit the Windows Terminal built-in default.
                        return new ColorScheme { Dark = "Campbell", Light = "Campbell" };
                    }
                }
            }

            /// <summary>
            /// Read values in .NET color format and convert them to hex color format
            /// </summary>
            private class ColorConverter : JsonConverter
            {
                /// <summary>
                /// Write values in JSON format
                /// </summary>
                /// <param name="writer"></param>
                /// <param name="value"></param>
                /// <param name="serializer"></param>
                public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
                {
                    if (value == null)
                    {
                        writer.WriteNull();
                        return;
                    }

                    Color color = (Color)value;
                    string hexColor = ColorTranslator.ToHtml(color);
                    serializer.Serialize(writer, hexColor);
                }

                /// <summary>
                /// Read values in JSON format and convert them to .NET color format
                /// </summary>
                /// <param name="reader"></param>
                /// <param name="objectType"></param>
                /// <param name="existingValue"></param>
                /// <param name="serializer"></param>
                /// <returns></returns>
                public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
                {
                    if (reader.TokenType == JsonToken.Null) return Color.Empty;

                    string hexColor = reader.Value as string;

                    // Handle empty string
                    if (string.IsNullOrEmpty(hexColor)) return Color.Empty; // Return a default color.

                    // Windows Terminal theme keywords ("terminalBackground", "accent") are resolved to real hex colors by ResolveThemeBackgroundKeywords() BEFORE deserialization runs
                    // (see WinTerminal's JSONFile loading branch), using data from the same JSON document (schemes/profiles). This check is only a crash guard for paths that bypass that
                    // pre-pass (e.g. Mode.WinPaletterFile, which copies fields via reflection instead of re-parsing JSON).
                    if (hexColor.Equals("terminalBackground", StringComparison.OrdinalIgnoreCase) ||
                        hexColor.Equals("accent", StringComparison.OrdinalIgnoreCase))
                    {
                        return Color.Empty;
                    }

                    // Remove unsupported characters
                    hexColor = Regex.Replace(hexColor, "[，、]", ",");

                    // Replace spaces with commas if exist as ColorTranslator.FromHtml does not support spaces
                    if (hexColor.Contains(" ") && !hexColor.Contains(",")) hexColor = hexColor.Replace(" ", ", ");

                    try
                    {
                        // Check if the color is HEX or RGB/ARGB format
                        if (!hexColor.Contains(","))
                        {
                            return ColorTranslator.FromHtml(hexColor);
                        }
                        else
                        {
                            string[] colors = hexColor.Split([','], StringSplitOptions.RemoveEmptyEntries);
                            if (colors.Length == 3)
                            {
                                return Color.FromArgb(Math.Min(255, Math.Max(0, int.Parse(colors[0].Trim()))),
                                                      Math.Min(255, Math.Max(0, int.Parse(colors[1].Trim()))),
                                                      Math.Min(255, Math.Max(0, int.Parse(colors[2].Trim()))));
                            }
                            else if (colors.Length == 4)
                            {
                                return Color.FromArgb(Math.Min(255, Math.Max(0, int.Parse(colors[0].Trim()))),
                                                      Math.Min(255, Math.Max(0, int.Parse(colors[1].Trim()))),
                                                      Math.Min(255, Math.Max(0, int.Parse(colors[2].Trim()))),
                                                      Math.Min(255, Math.Max(0, int.Parse(colors[3].Trim()))));
                            }
                            else
                            {
                                return Color.Empty;
                            }
                        }
                    }
                    catch
                    {
                        // Any unrecognized keyword or malformed value (current or future Windows Terminal schema additions) should never crash theme loading - just skip the override.
                        return Color.Empty;
                    }
                }

                /// <summary>
                /// Check if the object type is <see cref="Color"/> for conversion.
                /// </summary>
                /// <param name="objectType"></param>
                /// <returns></returns>
                public override bool CanConvert(Type objectType)
                {
                    return objectType == typeof(Color);
                }
            }

            /// <summary>
            /// JSON converter for serializing and deserializing <see cref="FontWeight"/> enumeration values.
            /// </summary>
            /// <remarks>
            /// This converter is used to handle the conversion between JSON representations and the <see cref="FontWeight"/> enumeration.
            /// It ensures correct deserialization of font weights and provides a default value (Normal) if parsing fails.
            /// </remarks>
            private class FontWeightConverter : JsonConverter<FontWeight>
            {
                /// <summary>
                /// Read values in JSON format and convert them to <see cref="FontWeight"/> enumeration values.
                /// </summary>
                /// <param name="reader"></param>
                /// <param name="objectType"></param>
                /// <param name="existingValue"></param>
                /// <param name="hasExistingValue"></param>
                /// <param name="serializer"></param>
                /// <returns></returns>
                public override FontWeight ReadJson(JsonReader reader, Type objectType, FontWeight existingValue, bool hasExistingValue, JsonSerializer serializer)
                {
                    if (reader.TokenType == JsonToken.Integer)
                    {
                        int intValue = Convert.ToInt32(reader.Value);

                        // Check specific ranges and map them to suitable enum values
                        if (intValue >= 100 && intValue <= 200)
                        {
                            return FontWeight.Thin;
                        }
                        else if (intValue > 200 && intValue <= 300)
                        {
                            return FontWeight.ExtraLight;
                        }
                        else if (intValue > 300 && intValue <= 400)
                        {
                            return FontWeight.Light;
                        }
                        else if (intValue > 400 && intValue <= 500)
                        {
                            return FontWeight.SemiLight;
                        }
                        else if (intValue > 500 && intValue <= 600)
                        {
                            return FontWeight.Normal;
                        }
                        else if (intValue > 600 && intValue <= 700)
                        {
                            return FontWeight.Medium;
                        }
                        else if (intValue > 700 && intValue <= 800)
                        {
                            return FontWeight.SemiBold;
                        }
                        else if (intValue > 800 && intValue <= 900)
                        {
                            return FontWeight.Bold;
                        }
                        else if (intValue > 900 && intValue <= 950)
                        {
                            return FontWeight.ExtraBold;
                        }
                        else if (intValue > 950)
                        {
                            return FontWeight.Black;
                        }
                        // Add more custom range checks as needed

                        // If it's an integer and not within specific ranges, try to parse it to an enum
                        if (Enum.IsDefined(typeof(FontWeight), intValue))
                        {
                            return (FontWeight)intValue;
                        }
                    }
                    else if (reader.TokenType == JsonToken.String)
                    {
                        // If it's a string, parse it to an enum
                        if (Enum.TryParse(reader.Value?.ToString(), true, out FontWeight result))
                        {
                            return result;
                        }
                    }

                    return FontWeight.Normal; // Default value if parsing fails
                }

                /// <summary>
                /// Write values in JSON format
                /// </summary>
                /// <param name="writer"></param>
                /// <param name="value"></param>
                /// <param name="serializer"></param>
                public override void WriteJson(JsonWriter writer, FontWeight value, JsonSerializer serializer)
                {
                    // Use reflection to get the JsonProperty attribute value
                    Type enumType = typeof(FontWeight);
                    FieldInfo fieldInfo = enumType.GetField(value.ToString());
                    JsonPropertyAttribute jsonPropertyAttribute = (JsonPropertyAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(JsonPropertyAttribute));

                    serializer.Serialize(writer, jsonPropertyAttribute.PropertyName);
                }
            }

            /// <summary>
            /// JSON converter for serializing and deserializing <see cref="CursorShape"/> enumeration values.
            /// </summary>
            /// <remarks>
            /// This converter is used to handle the conversion between JSON representations and the <see cref="CursorShape"/> enumeration.
            /// It ensures correct deserialization of cursor shapes and provides a default value (Bar) if parsing fails.
            /// </remarks>
            private class CursorShapeConverter : JsonConverter<CursorShape>
            {
                /// <summary>
                /// Read values in JSON format and convert them to <see cref="CursorShape"/> enumeration values.
                /// </summary>
                /// <param name="reader"></param>
                /// <param name="objectType"></param>
                /// <param name="existingValue"></param>
                /// <param name="hasExistingValue"></param>
                /// <param name="serializer"></param>
                /// <returns></returns>
                public override CursorShape ReadJson(JsonReader reader, Type objectType, CursorShape existingValue, bool hasExistingValue, JsonSerializer serializer)
                {
                    if (Enum.TryParse(reader.Value?.ToString(), true, out CursorShape result))
                    {
                        return result;
                    }

                    return CursorShape.Bar; // Default value if parsing fails
                }

                /// <summary>
                /// Write values in JSON format
                /// </summary>
                /// <param name="writer"></param>
                /// <param name="value"></param>
                /// <param name="serializer"></param>
                public override void WriteJson(JsonWriter writer, CursorShape value, JsonSerializer serializer)
                {
                    // Use reflection to get the JsonProperty attribute value
                    Type enumType = typeof(CursorShape);
                    FieldInfo fieldInfo = enumType.GetField(value.ToString());
                    JsonPropertyAttribute jsonPropertyAttribute = (JsonPropertyAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(JsonPropertyAttribute));

                    serializer.Serialize(writer, jsonPropertyAttribute.PropertyName);
                }
            }

            /// <summary>
            /// JSON converter for serializing and deserializing <see cref="double"/> values representing background image opacity.
            /// </summary>
            /// <remarks>
            /// This converter is used to handle the conversion between JSON representations and the <see cref="double"/> type for background image opacity.
            /// It ensures correct deserialization of opacity values and clamps them between 0 and 1. The default value is 1 if parsing fails.
            /// </remarks>
            private class BackgroundImageOpacityConverter : JsonConverter<double>
            {
                /// <summary>
                /// Read values in JSON format and convert them to <see cref="double"/> values representing background image opacity.
                /// </summary>
                /// <param name="reader"></param>
                /// <param name="objectType"></param>
                /// <param name="existingValue"></param>
                /// <param name="hasExistingValue"></param>
                /// <param name="serializer"></param>
                /// <returns></returns>
                public override double ReadJson(JsonReader reader, Type objectType, double existingValue, bool hasExistingValue, JsonSerializer serializer)
                {
                    if (double.TryParse(reader.Value?.ToString(), out double result))
                    {
                        return Math.Max(0, Math.Min(result / 100, 1));
                    }

                    return 1; // Default value if parsing fails
                }

                /// <summary>
                /// Write values in JSON format
                /// </summary>
                /// <param name="writer"></param>
                /// <param name="value"></param>
                /// <param name="serializer"></param>
                public override void WriteJson(JsonWriter writer, double value, JsonSerializer serializer)
                {
                    double clampedValue = Math.Max(0, Math.Min(value / 100, 1));
                    serializer.Serialize(writer, clampedValue);
                }
            }

            #endregion

            #region Enums

            /// <summary>
            /// Enumeration representing different shapes for the cursor.
            /// </summary>
            /// <remarks>
            /// The cursor shape is a visual indicator of the position in a text document and can have various forms.
            /// This enumeration defines different shapes that can be used for representing the cursor in a terminal or text editor.
            /// </remarks>
            public enum CursorShape
            {
                /// <summary>
                /// Vertical bar cursor shape.
                /// </summary>
                [JsonProperty("bar")]
                Bar,

                /// <summary>
                /// Double underscore cursor shape.
                /// </summary>
                [JsonProperty("doubleUnderscore")]
                DoubleUnderscore,

                /// <summary>
                /// EmptyError box cursor shape.
                /// </summary>
                [JsonProperty("emptyBox")]
                EmptyBox,

                /// <summary>
                /// Filled box cursor shape.
                /// </summary>
                [JsonProperty("filledBox")]
                FilledBox,

                /// <summary>
                /// Underscore cursor shape.
                /// </summary>
                [JsonProperty("underscore")]
                Underscore,

                /// <summary>
                /// Vintage cursor shape.
                /// </summary>
                [JsonProperty("vintage")]
                Vintage
            }


            /// <summary>
            /// Enumeration representing different font weights.
            /// </summary>
            /// <remarks>
            /// Font weight is a typographic property indicating the thickness of the characters in a written font.
            /// This enumeration represents a range of font weights from thin to extra black.
            /// </remarks>
            public enum FontWeight
            {
                /// <summary>
                /// Thin font weight.
                /// </summary>
                [JsonProperty("thin")]
                Thin,

                /// <summary>
                /// Extra light font weight.
                /// </summary>
                [JsonProperty("extra-light")]
                ExtraLight,

                /// <summary>
                /// Light font weight.
                /// </summary>
                [JsonProperty("light")]
                Light,

                /// <summary>
                /// Semi-light font weight.
                /// </summary>
                [JsonProperty("semi-light")]
                SemiLight,

                /// <summary>
                /// Normal font weight.
                /// </summary>
                [JsonProperty("normal")]
                Normal,

                /// <summary>
                /// Medium font weight.
                /// </summary>
                [JsonProperty("medium")]
                Medium,

                /// <summary>
                /// Semi-bold font weight.
                /// </summary>
                [JsonProperty("semi-bold")]
                SemiBold,

                /// <summary>
                /// Bold font weight.
                /// </summary>
                [JsonProperty("bold")]
                Bold,

                /// <summary>
                /// Extra bold font weight.
                /// </summary>
                [JsonProperty("extra-bold")]
                ExtraBold,

                /// <summary>
                /// Black font weight.
                /// </summary>
                [JsonProperty("black")]
                Black,

                /// <summary>
                /// Extra black font weight.
                /// </summary>
                [JsonProperty("extra-black")]
                ExtraBlack
            }

            #endregion
        }

        /// <summary>
        /// Merging two JObjects with desired behavior.
        /// <br>The desired behavior is to merge two JSONs without including null values.</br>
        /// </summary>
        /// <param name="original"></param>
        /// <param name="newJson"></param>
        void Merge(JObject original, JObject newJson)
        {
            foreach (JProperty newProperty in newJson.Properties())
            {
                JProperty originalProperty = original.Property(newProperty.Name);

                if (originalProperty != null)
                {
                    if (newProperty.Value.Type == JTokenType.Object && originalProperty.Value.Type == JTokenType.Object)
                    {
                        // Recursively merge subproperties
                        Merge((JObject)originalProperty.Value, (JObject)newProperty.Value);
                    }
                    else if (newProperty.Value.Type == JTokenType.Array && originalProperty.Value.Type == JTokenType.Array)
                    {
                        // Merge arrays if both properties are arrays
                        MergeArrays((JArray)originalProperty.Value, (JArray)newProperty.Value);
                    }
                    else
                    {
                        // Update existing property with the one from newJson if it is not null or empty
                        if (!IsNullOrWhiteSpace(newProperty.Value))
                        {
                            originalProperty.Value = newProperty.Value;
                        }
                    }
                }
                else
                {
                    // Add new property if not found in original JSON and it is not null or empty
                    if (!IsNullOrWhiteSpace(newProperty.Value))
                    {
                        original.Add(newProperty);
                    }
                }
            }

            // Check for properties in originalJson that are not in newJson
            foreach (JProperty originalProperty in original.Properties().ToList())
            {
                if (!newJson.Properties().Any(p => p.Name == originalProperty.Name))
                {
                    // Add original property if it is not null or empty
                    if (!IsNullOrWhiteSpace(originalProperty.Value))
                    {
                        // Check if the property already exists in the original JSON
                        JProperty newProperty = newJson.Property(originalProperty.Name);
                        if (newProperty != null && !IsNullOrWhiteSpace(newProperty.Value))
                        {
                            // Update the existing property with the one from newJson
                            originalProperty.Value = newProperty.Value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Merge two JArrays without duplications
        /// </summary>
        /// <param name="originalArray"></param>
        /// <param name="newArray"></param>
        private void MergeArrays(JArray originalArray, JArray newArray)
        {
            List<JObject> originalItems = [.. originalArray.Children<JObject>()];

            foreach (JObject newItem in newArray.Children<JObject>())
            {
                JObject existingItem = originalItems.FirstOrDefault(
                    x => x.Property("guid")?.Value.ToString() == newItem.Property("guid")?.Value.ToString()
                );

                if (existingItem != null)
                {
                    // Update existing item with values from new item
                    Merge(existingItem, newItem);
                }
                else
                {
                    // Add new item to the original array
                    originalArray.Add(newItem);
                }
            }
        }

        /// <summary>
        /// Check if a JToken is null or empty string
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool IsNullOrWhiteSpace(JToken token)
        {
            return token == null || (token.Type == JTokenType.String && string.IsNullOrWhiteSpace(token.Value<string>())) || (token.Type == JTokenType.Null);
        }

        /// <summary>
        /// Remove default properties from a <see cref="JObject"></see> based on a default scheme.
        /// <br>Useful for making schemes that has values exist in default scheme being got from default scheme not from scheme itself (Same Windows Terminal behaviour).</br>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaults"></param>
        /// <returns></returns>
        public static JObject RemoveDefaultProperties(JObject value, JObject defaults)
        {
            // Create a list to store properties to remove
            List<string> propertiesToRemove = [];

            foreach (JProperty property in value.Properties())
            {
                JToken defaultValue = defaults[property.Name];
                JToken instanceValue = property.Value;

                // Exclude property if it matches the default value (including handling null values)
                if (AreValuesEqual(instanceValue, defaultValue))
                {
                    // Add property name to the list for later removal
                    propertiesToRemove.Add(property.Name);
                }
            }

            // Remove properties after the loop to avoid modifying the TabDataList during iteration
            foreach (string propertyName in propertiesToRemove)
            {
                value.Property(propertyName)?.Remove();
            }

            return value;
        }

        /// <summary>
        /// Check if two <see cref="JToken"/> values are equal
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool AreValuesEqual(JToken value1, JToken value2)
        {
            // Handle null values
            if (value1 == null && value2 == null)
            {
                return true;
            }

            // Handle case where one value is null and the other is not
            if (value1 == null || value2 == null)
            {
                return false;
            }

            // Compare values
            return JToken.DeepEquals(value1, value2);
        }

        private static readonly List<Scheme> DefaultSchemes =
        [
        new Scheme
        {
            Name = "Campbell",
            Background = Color.FromArgb(12, 12, 12),
            Black = Color.FromArgb(12, 12, 12),
            Blue = Color.FromArgb(0, 55, 218),
            BrightBlack = Color.FromArgb(118, 118, 118),
            BrightBlue = Color.FromArgb(59, 120, 255),
            BrightCyan = Color.FromArgb(97, 214, 214),
            BrightGreen = Color.FromArgb(22, 198, 12),
            BrightPurple = Color.FromArgb(180, 0, 158),
            BrightRed = Color.FromArgb(231, 72, 86),
            BrightWhite = Color.FromArgb(242, 242, 242),
            BrightYellow = Color.FromArgb(249, 241, 165),
            CursorColor = Color.FromArgb(255, 255, 255),
            Cyan = Color.FromArgb(58, 150, 221),
            Foreground = Color.FromArgb(204, 204, 204),
            Green = Color.FromArgb(19, 161, 14),
            Purple = Color.FromArgb(136, 23, 152),
            Red = Color.FromArgb(197, 15, 31),
            SelectionBackground = Color.FromArgb(255, 255, 255),
            White = Color.FromArgb(204, 204, 204),
            Yellow = Color.FromArgb(196, 156, 0)
        },
        new Scheme
        {
            Name = "Campbell Powershell",
            Background = Color.FromArgb(1, 36, 86),
            Black = Color.FromArgb(12, 12, 12),
            Blue = Color.FromArgb(0, 55, 218),
            BrightBlack = Color.FromArgb(118, 118, 118),
            BrightBlue = Color.FromArgb(59, 120, 255),
            BrightCyan = Color.FromArgb(97, 214, 214),
            BrightGreen = Color.FromArgb(22, 198, 12),
            BrightPurple = Color.FromArgb(180, 0, 158),
            BrightRed = Color.FromArgb(231, 72, 86),
            BrightWhite = Color.FromArgb(242, 242, 242),
            BrightYellow = Color.FromArgb(249, 241, 165),
            CursorColor = Color.FromArgb(255, 255, 255),
            Cyan = Color.FromArgb(58, 150, 221),
            Foreground = Color.FromArgb(204, 204, 204),
            Green = Color.FromArgb(19, 161, 14),
            Purple = Color.FromArgb(136, 23, 152),
            Red = Color.FromArgb(197, 15, 31),
            SelectionBackground = Color.FromArgb(255, 255, 255),
            White = Color.FromArgb(204, 204, 204),
            Yellow = Color.FromArgb(196, 156, 0)
        },
        new Scheme
        {
            Name = "CGA",
            Background = Color.FromArgb(0, 0, 0),
            Black = Color.FromArgb(0, 0, 0),
            Blue = Color.FromArgb(0, 0, 170),
            BrightBlack = Color.FromArgb(85, 85, 85),
            BrightBlue = Color.FromArgb(85, 85, 255),
            BrightCyan = Color.FromArgb(85, 255, 255),
            BrightGreen = Color.FromArgb(85, 255, 85),
            BrightPurple = Color.FromArgb(255, 85, 255),
            BrightRed = Color.FromArgb(255, 85, 85),
            BrightWhite = Color.FromArgb(255, 255, 255),
            BrightYellow = Color.FromArgb(255, 255, 85),
            CursorColor = Color.FromArgb(0, 170, 0),
            Cyan = Color.FromArgb(0, 170, 170),
            Foreground = Color.FromArgb(170, 170, 170),
            Green = Color.FromArgb(0, 170, 0),
            Purple = Color.FromArgb(170, 0, 170),
            Red = Color.FromArgb(170, 0, 0),
            SelectionBackground = Color.FromArgb(255, 255, 255),
            White = Color.FromArgb(170, 170, 170),
            Yellow = Color.FromArgb(170, 85, 0)
        },
        new Scheme
        {
            Name = "Dark+",
            Background = Color.FromArgb(30, 30, 30),
            Black = Color.FromArgb(0, 0, 0),
            Blue = Color.FromArgb(36, 114, 200),
            BrightBlack = Color.FromArgb(102, 102, 102),
            BrightBlue = Color.FromArgb(59, 142, 234),
            BrightCyan = Color.FromArgb(41, 184, 219),
            BrightGreen = Color.FromArgb(35, 209, 139),
            BrightPurple = Color.FromArgb(214, 112, 214),
            BrightRed = Color.FromArgb(241, 76, 76),
            BrightWhite = Color.FromArgb(229, 229, 229),
            BrightYellow = Color.FromArgb(245, 245, 67),
            CursorColor = Color.FromArgb(128, 128, 128),
            Cyan = Color.FromArgb(17, 168, 205),
            Foreground = Color.FromArgb(204, 204, 204),
            Green = Color.FromArgb(13, 188, 121),
            Purple = Color.FromArgb(188, 63, 188),
            Red = Color.FromArgb(205, 49, 49),
            SelectionBackground = Color.FromArgb(255, 255, 255),
            White = Color.FromArgb(229, 229, 229),
            Yellow = Color.FromArgb(229, 229, 16)
        },
        new Scheme
        {
            Name = "Dimidium",
            Background = Color.FromArgb(20, 20, 20),
            Black = Color.FromArgb(0, 0, 0),
            Blue = Color.FromArgb(5, 117, 216),
            BrightBlack = Color.FromArgb(129, 126, 126),
            BrightBlue = Color.FromArgb(104, 141, 253),
            BrightCyan = Color.FromArgb(50, 224, 251),
            BrightGreen = Color.FromArgb(55, 229, 123),
            BrightPurple = Color.FromArgb(237, 111, 233),
            BrightRed = Color.FromArgb(255, 100, 59),
            BrightWhite = Color.FromArgb(211, 216, 217),
            BrightYellow = Color.FromArgb(252, 205, 26),
            CursorColor = Color.FromArgb(55, 229, 123),
            Cyan = Color.FromArgb(29, 182, 187),
            Foreground = Color.FromArgb(186, 183, 182),
            Green = Color.FromArgb(96, 180, 66),
            Purple = Color.FromArgb(175, 94, 210),
            Red = Color.FromArgb(207, 73, 76),
            SelectionBackground = Color.FromArgb(255, 255, 255),
            White = Color.FromArgb(186, 183, 182),
            Yellow = Color.FromArgb(219, 156, 17)
        },
        new Scheme
        {
            Name = "IBM 5153",
            Background = Color.FromArgb(0, 0, 0),
            Black = Color.FromArgb(0, 0, 0),
            Blue = Color.FromArgb(0, 0, 170),
            BrightBlack = Color.FromArgb(85, 85, 85),
            BrightBlue = Color.FromArgb(85, 85, 255),
            BrightCyan = Color.FromArgb(85, 255, 255),
            BrightGreen = Color.FromArgb(85, 255, 85),
            BrightPurple = Color.FromArgb(255, 85, 255),
            BrightRed = Color.FromArgb(255, 85, 85),
            BrightWhite = Color.FromArgb(255, 255, 255),
            BrightYellow = Color.FromArgb(255, 255, 85),
            CursorColor = Color.FromArgb(0, 170, 0),
            Cyan = Color.FromArgb(0, 170, 170),
            Foreground = Color.FromArgb(170, 170, 170),
            Green = Color.FromArgb(0, 170, 0),
            Purple = Color.FromArgb(170, 0, 170),
            Red = Color.FromArgb(170, 0, 0),
            SelectionBackground = Color.FromArgb(255, 255, 255),
            White = Color.FromArgb(170, 170, 170),
            Yellow = Color.FromArgb(196, 126, 0)
        },
        new Scheme
        {
            Name = "One Half Dark",
            Background = Color.FromArgb(40, 44, 52),
            Black = Color.FromArgb(40, 44, 52),
            Blue = Color.FromArgb(97, 175, 239),
            BrightBlack = Color.FromArgb(90, 99, 116),
            BrightBlue = Color.FromArgb(97, 175, 239),
            BrightCyan = Color.FromArgb(86, 182, 194),
            BrightGreen = Color.FromArgb(152, 195, 121),
            BrightPurple = Color.FromArgb(198, 120, 221),
            BrightRed = Color.FromArgb(224, 108, 117),
            BrightWhite = Color.FromArgb(220, 223, 228),
            BrightYellow = Color.FromArgb(229, 192, 123),
            CursorColor = Color.FromArgb(255, 255, 255),
            Cyan = Color.FromArgb(86, 182, 194),
            Foreground = Color.FromArgb(220, 223, 228),
            Green = Color.FromArgb(152, 195, 121),
            Purple = Color.FromArgb(198, 120, 221),
            Red = Color.FromArgb(224, 108, 117),
            SelectionBackground = Color.FromArgb(255, 255, 255),
            White = Color.FromArgb(220, 223, 228),
            Yellow = Color.FromArgb(229, 192, 123)
        },
        new Scheme
        {
            Name = "One Half Light",
            Background = Color.FromArgb(250, 250, 250),
            Black = Color.FromArgb(56, 58, 66),
            Blue = Color.FromArgb(1, 132, 188),
            BrightBlack = Color.FromArgb(79, 82, 93),
            BrightBlue = Color.FromArgb(97, 175, 239),
            BrightCyan = Color.FromArgb(86, 181, 193),
            BrightGreen = Color.FromArgb(152, 195, 121),
            BrightPurple = Color.FromArgb(197, 119, 221),
            BrightRed = Color.FromArgb(223, 108, 117),
            BrightWhite = Color.FromArgb(255, 255, 255),
            BrightYellow = Color.FromArgb(228, 196, 122),
            CursorColor = Color.FromArgb(79, 82, 93),
            Cyan = Color.FromArgb(9, 151, 179),
            Foreground = Color.FromArgb(56, 58, 66),
            Green = Color.FromArgb(80, 161, 79),
            Purple = Color.FromArgb(166, 38, 164),
            Red = Color.FromArgb(228, 86, 73),
            SelectionBackground = Color.FromArgb(255, 255, 255),
            White = Color.FromArgb(250, 250, 250),
            Yellow = Color.FromArgb(193, 131, 1)
        },
                    new Scheme
        {
            Name = "Ottosson",
            Background = Color.FromArgb(0, 0, 0),
            Black = Color.FromArgb(0, 0, 0),
            Blue = Color.FromArgb(32, 77, 190),
            BrightBlack = Color.FromArgb(128, 128, 128),
            BrightBlue = Color.FromArgb(47, 106, 255),
            BrightCyan = Color.FromArgb(0, 225, 240),
            BrightGreen = Color.FromArgb(88, 234, 81),
            BrightPurple = Color.FromArgb(252, 116, 255),
            BrightRed = Color.FromArgb(255, 62, 48),
            BrightWhite = Color.FromArgb(255, 255, 255),
            BrightYellow = Color.FromArgb(255, 201, 68),
            CursorColor = Color.FromArgb(255, 255, 255),
            Cyan = Color.FromArgb(0, 167, 178),
            Foreground = Color.FromArgb(190, 190, 190),
            Green = Color.FromArgb(63, 174, 58),
            Purple = Color.FromArgb(187, 84, 190),
            Red = Color.FromArgb(190, 44, 33),
            SelectionBackground = Color.FromArgb(146, 164, 253),
            White = Color.FromArgb(190, 190, 190),
            Yellow = Color.FromArgb(190, 154, 74)
        },
        new Scheme
        {
            Name = "Solarized Dark",
            Background = Color.FromArgb(0, 43, 54),
            Black = Color.FromArgb(0, 43, 54),
            Blue = Color.FromArgb(38, 139, 210),
            BrightBlack = Color.FromArgb(7, 54, 66),
            BrightBlue = Color.FromArgb(131, 148, 150),
            BrightCyan = Color.FromArgb(147, 161, 161),
            BrightGreen = Color.FromArgb(88, 110, 117),
            BrightPurple = Color.FromArgb(108, 113, 196),
            BrightRed = Color.FromArgb(203, 75, 22),
            BrightWhite = Color.FromArgb(253, 246, 227),
            BrightYellow = Color.FromArgb(101, 123, 131),
            CursorColor = Color.FromArgb(255, 255, 255),
            Cyan = Color.FromArgb(42, 161, 152),
            Foreground = Color.FromArgb(131, 148, 150),
            Green = Color.FromArgb(133, 153, 0),
            Purple = Color.FromArgb(211, 54, 130),
            Red = Color.FromArgb(220, 50, 47),
            SelectionBackground = Color.FromArgb(255, 255, 255),
            White = Color.FromArgb(238, 232, 213),
            Yellow = Color.FromArgb(181, 137, 0)
        },
        new Scheme
        {
            Name = "Solarized Light",
            Background = Color.FromArgb(253, 246, 227),
            Black = Color.FromArgb(0, 43, 54),
            Blue = Color.FromArgb(38, 139, 210),
            BrightBlack = Color.FromArgb(7, 54, 66),
            BrightBlue = Color.FromArgb(131, 148, 150),
            BrightCyan = Color.FromArgb(147, 161, 161),
            BrightGreen = Color.FromArgb(88, 110, 117),
            BrightPurple = Color.FromArgb(108, 113, 196),
            BrightRed = Color.FromArgb(203, 75, 22),
            BrightWhite = Color.FromArgb(253, 246, 227),
            BrightYellow = Color.FromArgb(101, 123, 131),
            CursorColor = Color.FromArgb(0, 43, 54),
            Cyan = Color.FromArgb(42, 161, 152),
            Foreground = Color.FromArgb(101, 123, 131),
            Green = Color.FromArgb(133, 153, 0),
            Purple = Color.FromArgb(211, 54, 130),
            Red = Color.FromArgb(220, 50, 47),
            SelectionBackground = Color.FromArgb(255, 255, 255),
            White = Color.FromArgb(238, 232, 213),
            Yellow = Color.FromArgb(181, 137, 0)
        },
        new Scheme
        {
            Name = "Tango Dark",
            Background = Color.FromArgb(0, 0, 0),
            Black = Color.FromArgb(0, 0, 0),
            Blue = Color.FromArgb(52, 101, 164),
            BrightBlack = Color.FromArgb(85, 87, 83),
            BrightBlue = Color.FromArgb(114, 159, 207),
            BrightCyan = Color.FromArgb(52, 226, 226),
            BrightGreen = Color.FromArgb(138, 226, 52),
            BrightPurple = Color.FromArgb(173, 127, 168),
            BrightRed = Color.FromArgb(239, 41, 41),
            BrightWhite = Color.FromArgb(238, 238, 236),
            BrightYellow = Color.FromArgb(252, 233, 79),
            CursorColor = Color.FromArgb(255, 255, 255),
            Cyan = Color.FromArgb(6, 152, 154),
            Foreground = Color.FromArgb(211, 215, 207),
            Green = Color.FromArgb(78, 154, 6),
            Purple = Color.FromArgb(117, 80, 123),
            Red = Color.FromArgb(204, 0, 0),
            SelectionBackground = Color.FromArgb(255, 255, 255),
            White = Color.FromArgb(211, 215, 207),
            Yellow = Color.FromArgb(196, 160, 0)
        },
        new Scheme
        {
            Name = "Tango Light",
            Background = Color.FromArgb(255, 255, 255),
            Black = Color.FromArgb(0, 0, 0),
            Blue = Color.FromArgb(52, 101, 164),
            BrightBlack = Color.FromArgb(85, 87, 83),
            BrightBlue = Color.FromArgb(114, 159, 207),
            BrightCyan = Color.FromArgb(52, 226, 226),
            BrightGreen = Color.FromArgb(138, 226, 52),
            BrightPurple = Color.FromArgb(173, 127, 168),
            BrightRed = Color.FromArgb(239, 41, 41),
            BrightWhite = Color.FromArgb(238, 238, 236),
            BrightYellow = Color.FromArgb(252, 233, 79),
            CursorColor = Color.FromArgb(0, 0, 0),
            Cyan = Color.FromArgb(6, 152, 154),
            Foreground = Color.FromArgb(85, 87, 83),
            Green = Color.FromArgb(78, 154, 6),
            Purple = Color.FromArgb(117, 80, 123),
            Red = Color.FromArgb(204, 0, 0),
            SelectionBackground = Color.FromArgb(255, 255, 255),
            White = Color.FromArgb(211, 215, 207),
            Yellow = Color.FromArgb(196, 160, 0)
        },
        new Scheme
        {
            Name = "Vintage",
            Background = Color.FromArgb(0, 0, 0),
            Black = Color.FromArgb(0, 0, 0),
            Blue = Color.FromArgb(0, 0, 128),
            BrightBlack = Color.FromArgb(128, 128, 128),
            BrightBlue = Color.FromArgb(0, 0, 255),
            BrightCyan = Color.FromArgb(0, 255, 255),
            BrightGreen = Color.FromArgb(0, 255, 0),
            BrightPurple = Color.FromArgb(255, 0, 255),
            BrightRed = Color.FromArgb(255, 0, 0),
            BrightWhite = Color.FromArgb(255, 255, 255),
            BrightYellow = Color.FromArgb(255, 255, 0),
            CursorColor = Color.FromArgb(255, 255, 255),
            Cyan = Color.FromArgb(0, 128, 128),
            Foreground = Color.FromArgb(192, 192, 192),
            Green = Color.FromArgb(0, 128, 0),
            Purple = Color.FromArgb(128, 0, 128),
            Red = Color.FromArgb(128, 0, 0),
            SelectionBackground = Color.FromArgb(255, 255, 255),
            White = Color.FromArgb(192, 192, 192),
            Yellow = Color.FromArgb(128, 128, 0)
        }
];

        #endregion

        private static class JsonHelper
        {
            // Cached settings (no reflection rebuild per call)
            private static readonly JsonSerializerSettings cachedSettings = new()
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Formatting = Formatting.None,
                ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver()
            };

            // Reusable serializer (no per-call allocations). Made internal (was private) so DeserializeFast and the pre-parse keyword-resolution path can share the same instance.
            internal static readonly JsonSerializer Serializer = JsonSerializer.Create(cachedSettings);

            // The optimized deserializer method
            public static T DeserializeFast<T>(string json)
            {
                if (string.IsNullOrEmpty(json))
                    return default;

                using var stringReader = new StringReader(json);
                using var reader = new JsonTextReader(stringReader)
                {
                    CloseInput = true,
                    SupportMultipleContent = false
                };

                return Serializer.Deserialize<T>(reader);
            }

            /// <summary>
            /// Every JSON property name that WinTerminal's C# model deserializes as a <see cref="Color"/>
            /// via <c>ColorConverter</c>. Kept in one place so the keyword-resolution walk below stays in
            /// sync automatically if new Color-typed properties are added to the model later.
            /// </summary>
            private static readonly HashSet<string> ColorPropertyNames =
                GetColorPropertyNames();

            private static HashSet<string> GetColorPropertyNames()
            {
                var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // GetNestedTypes() is NOT recursive - it only returns types declared directly inside
                // the given type. All of Profile/Scheme/TabSettings/TabRowSettings/etc. are nested one
                // level deeper, inside WinTerminal.Types, not directly inside WinTerminal - so this must
                // walk the whole nested-type tree, not just the immediate children.
                foreach (Type type in GetAllNestedTypesRecursively(typeof(WinTerminal)))
                {
                    AddColorProperties(type, result);
                }

                // Also include WinTerminal itself, in case it ever gets a Color property directly.
                AddColorProperties(typeof(WinTerminal), result);

                return result;
            }

            private static IEnumerable<Type> GetAllNestedTypesRecursively(Type root)
            {
                foreach (Type nested in root.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic))
                {
                    yield return nested;

                    foreach (Type deeper in GetAllNestedTypesRecursively(nested))
                    {
                        yield return deeper;
                    }
                }
            }

            private static void AddColorProperties(
                Type type,
                HashSet<string> result)
            {
                foreach (PropertyInfo property in type.GetProperties(
                    BindingFlags.Instance |
                    BindingFlags.Static |
                    BindingFlags.Public |
                    BindingFlags.NonPublic))
                {
                    if (property.PropertyType != typeof(Color))
                        continue;

                    JsonPropertyAttribute attribute =
                        property.GetCustomAttribute<JsonPropertyAttribute>();

                    string jsonName = attribute?.PropertyName ?? property.Name;

                    result.Add(jsonName);
                }
            }

            /// <summary>
            /// Resolves Windows Terminal's special color keywords ("terminalBackground", "accent") into
            /// concrete hex colors, wherever a Color-typed property can hold one anywhere in the settings
            /// JSON. Must run on the raw JObject BEFORE deserialization into WinTerminal: resolving from
            /// "this" mid-deserialization is unreliable because Profiles/Schemes may not be populated yet,
            /// depending on property order in the source file.
            /// </summary>
            public static void ResolveThemeBackgroundKeywords(JObject root)
            {
                // Resolve profile defaults first.
                JObject defaults = root["profiles"]?["defaults"] as JObject;

                string defaultSchemeName = GetSchemeNameForProfile(defaults, "Campbell");

                // Resolve defaults themselves.
                if (defaults != null)
                {
                    Walk(defaults, defaultSchemeName, root);
                }

                // Resolve every profile using its own scheme,
                // falling back to profiles.defaults.
                if (root["profiles"]?["list"] is JArray profiles)
                {
                    foreach (JObject profile in profiles.OfType<JObject>())
                    {
                        string profileSchemeName =
                            GetSchemeNameForProfile(profile, defaultSchemeName);

                        Walk(profile, profileSchemeName, root);
                    }
                }

                // Resolve everything else.
                foreach (JProperty property in root.Properties().ToList())
                {
                    if (property.Name.Equals("schemes", StringComparison.OrdinalIgnoreCase) ||
                        property.Name.Equals("profiles", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    Walk(property.Value, defaultSchemeName, root);
                }
            }

            /// <summary>
            /// Layering pass that runs on the raw settings JSON before deserialization.
            /// <para>
            /// Windows Terminal's own runtime semantics are: a value in a profile overrides the same
            /// value in <c>profiles.defaults</c>; if the profile omits the key entirely, the value
            /// from <c>profiles.defaults</c> is used. After JSON has been deserialized into the C#
            /// model, "key omitted" and "key present with the CLR default" are indistinguishable for
            /// value-typed members (<c>bool</c>, <c>int</c>, <c>double</c>, enums, <c>Color</c>) -
            /// <c>useAcrylic</c> omitted becomes <c>false</c>, <c>opacity</c> omitted becomes <c>100</c>,
            /// and so on. This method restores the "omitted means inherit" semantics by copying the
            /// defaults' value into any profile that did not carry the key, so that deserialization
            /// then sees the inherited value as if the profile had set it.
            /// </para>
            /// <para>
            /// Reference-typed members (<c>colorScheme</c>, <c>icon</c>, <c>tabTitle</c>, <c>font</c>)
            /// are handled too, but the model-side <c>InheritNullValuesFromDefaults()</c> pass is what
            /// guarantees their inheritance in the WinPaletterFile path where no raw JSON exists.
            /// </para>
            /// </summary>
            public static void ApplyProfileDefaultsLayering(JObject root)
            {
                JObject defaults = root["profiles"]?["defaults"] as JObject;
                if (defaults == null) return;

                if (root["profiles"]?["list"] is not JArray profiles) return;

                foreach (JObject profile in profiles.OfType<JObject>())
                {
                    // Top-level scalar members.
                    InheritMissingKey(profile, defaults, "useAcrylic");
                    InheritMissingKey(profile, defaults, "opacity");
                    InheritMissingKey(profile, defaults, "cursorShape");
                    InheritMissingKey(profile, defaults, "cursorHeight");
                    InheritMissingKey(profile, defaults, "backgroundImageOpacity");
                    InheritMissingKey(profile, defaults, "tabColor");
                    InheritMissingKey(profile, defaults, "colorScheme");
                    InheritMissingKey(profile, defaults, "backgroundImage");
                    InheritMissingKey(profile, defaults, "commandline");
                    InheritMissingKey(profile, defaults, "icon");
                    InheritMissingKey(profile, defaults, "tabTitle");

                    // "font" is an object in JSON and has its own three sub-keys. Only layer the
                    // sub-keys the profile's font object actually omits; leave whatever the profile
                    // set alone.
                    if (profile["font"] is JObject profileFont)
                    {
                        if (defaults["font"] is JObject defaultFont)
                        {
                            InheritMissingKey(profileFont, defaultFont, "face");
                            InheritMissingKey(profileFont, defaultFont, "size");
                            InheritMissingKey(profileFont, defaultFont, "weight");
                        }
                    }
                    else
                    {
                        // Profile has no "font" at all - inherit the whole object.
                        JToken defaultFont = defaults["font"];
                        if (defaultFont != null)
                        {
                            profile["font"] = defaultFont.DeepClone();
                        }
                    }
                }
            }

            /// <summary>
            /// If <paramref name="source"/> does not contain the given property, copies it from
            /// <paramref name="donor"/>. Used to implement "omitted means inherit from defaults" on
            /// raw JSON, before the value-typed defaults of the C# model can mask the omission.
            /// </summary>
            private static void InheritMissingKey(JObject source, JObject donor, string propertyName)
            {
                if (source[propertyName] != null) return;

                JToken donorValue = donor[propertyName];
                if (donorValue == null) return;

                source[propertyName] = donorValue.DeepClone();
            }

            /// <summary>
            /// Recursively visits every node in the tree. "currentSchemeName" tracks which color scheme
            /// applies to "terminalBackground" lookups at this point - it's re-derived whenever we enter a
            /// profile object (each profile can reference its own scheme) and inherited by everything
            /// nested under it (e.g. a theme has no profile of its own, so it keeps using the ambient
            /// default-profile scheme passed in from the top level).
            /// </summary>
            private static void Walk(JToken node, string currentSchemeName, JObject root)
            {
                if (node is JObject obj)
                {
                    // If this object looks like a profile (carries its own colorScheme), colors nested
                    // directly in it should resolve against its scheme, not the ambient one.
                    if (obj["colorScheme"] != null)
                    {
                        currentSchemeName = GetSchemeNameForProfile(obj, currentSchemeName);
                    }

                    // Snapshot properties before iterating: ResolveColorKeyword can mutate/remove the property it's called on, which would invalidate a live enumerator over
                    // obj.Properties() and throw InvalidOperationException mid-loop.
                    foreach (JProperty property in obj.Properties().ToList())
                    {
                        if (ColorPropertyNames.Contains(property.Name) && obj[property.Name] is JValue { Type: JTokenType.String })
                        {
                            ResolveColorKeyword(obj, property.Name, root, currentSchemeName);
                        }
                        else
                        {
                            Walk(property.Value, currentSchemeName, root);
                        }
                    }
                }
                else if (node is JArray arr)
                {
                    foreach (JToken child in arr)
                    {
                        Walk(child, currentSchemeName, root);
                    }
                }
            }

            /// <summary>
            /// Resolves a single property, if a recognized keyword string, into a real hex color in place.
            /// "terminalBackground" resolves against schemeName's "background" entry; "accent" resolves
            /// against the current system accent color. Anything else (an actual hex/rgb string) is left
            /// untouched for ColorConverter to parse normally during deserialization.
            /// </summary>
            private static void ResolveColorKeyword(
                JObject container,
                string propertyName,
                JObject root,
                string schemeName)
            {
                if (container[propertyName] is not JValue value ||
                    value.Type != JTokenType.String)
                    return;

                string colorValue = value.Value<string>();

                if (string.IsNullOrWhiteSpace(colorValue))
                    return;

                if (colorValue.Equals(
                        "terminalBackground",
                        StringComparison.OrdinalIgnoreCase))
                {
                    string background = GetSchemeBackgroundHex(
                        schemeName,
                        root);

                    if (!string.IsNullOrWhiteSpace(background))
                    {
                        container[propertyName] = background;
                    }
                    else
                    {
                        // Keep the property rather than deleting it.
                        // The C# ColorConverter can then safely return Color.Empty.
                    }
                }
                else if (colorValue.Equals(
                             "accent",
                             StringComparison.OrdinalIgnoreCase))
                {
                    Color accent = GetSystemAccentColor();

                    container[propertyName] =
                        ColorTranslator.ToHtml(accent);
                }
            }

            /// <summary>
            /// Gets the color scheme name that applies to a given profile: its own "colorScheme" if set,
            /// else the fallback passed in (typically the "defaults" profile's resolved scheme), else
            /// Windows Terminal's built-in default.
            /// </summary>
            private static string GetSchemeNameForProfile(JObject profile, string fallbackSchemeName)
            {
                return ExtractSchemeName(profile?["colorScheme"]) ?? fallbackSchemeName ?? "Campbell";
            }

            /// <summary>
            /// "colorScheme" can be a plain string or a {"dark": "...", "light": "..."} object; either way
            /// we want the name to look up in "schemes" (dark takes precedence, matching WinTerminal's
            /// runtime default-theme behavior).
            /// </summary>
            private static string ExtractSchemeName(JToken colorSchemeToken)
            {
                return colorSchemeToken?.Type == JTokenType.Object
                    ? colorSchemeToken["dark"]?.Value<string>() ?? colorSchemeToken["light"]?.Value<string>()
                    : colorSchemeToken?.Value<string>();
            }

            /// <summary>
            /// Looks up a scheme by name and returns its background hex string. Checks the file's own
            /// "schemes" array first (by property name, from the SAME JObject being deserialized - that
            /// data is already fully present in the raw JSON regardless of where other properties sit).
            /// If the file doesn't define a matching scheme - common, since many settings.json files omit
            /// "schemes" entirely and rely on Windows Terminal's compiled-in defaults - falls back to this
            /// class's own DefaultSchemes list, which mirrors those built-ins. Returns null only if the
            /// scheme name matches nothing in either source.
            /// </summary>
            private static string GetSchemeBackgroundHex(string schemeName, JObject root)
            {
                if (string.IsNullOrWhiteSpace(schemeName))
                    return null;

                JObject scheme = (root["schemes"] as JArray)?
                    .Children<JObject>()
                    .FirstOrDefault(s =>
                        string.Equals(
                            s["name"]?.Value<string>(),
                            schemeName,
                            StringComparison.OrdinalIgnoreCase));

                string background = scheme?["background"]?.Value<string>();

                if (!string.IsNullOrWhiteSpace(background))
                {
                    // Do not recursively resolve terminalBackground here. A scheme referring to itself would otherwise recurse forever.
                    if (!background.Equals("terminalBackground", StringComparison.OrdinalIgnoreCase))
                    {
                        return background;
                    }
                }

                Scheme builtIn = DefaultSchemes.FirstOrDefault(
                    s => string.Equals(
                        s.Name,
                        schemeName,
                        StringComparison.OrdinalIgnoreCase));

                return builtIn != null
                    ? ColorTranslator.ToHtml(builtIn.Background)
                    : null;
            }

            /// <summary>
            /// Reads the current Windows accent color (DWM colorization color) from the registry.
            /// </summary>
            private static Color GetSystemAccentColor()
            {
                try
                {
                    using var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\DWM");
                    if (key?.GetValue("AccentColor") is int abgr)
                    {
                        // Stored as 0xAABBGGRR
                        byte a = (byte)((abgr >> 24) & 0xFF);
                        byte b = (byte)((abgr >> 16) & 0xFF);
                        byte g = (byte)((abgr >> 8) & 0xFF);
                        byte r = (byte)(abgr & 0xFF);
                        return Color.FromArgb(a, r, g, b);
                    }
                }
                catch { /* fall through to default */ }

                return Color.FromArgb(0, 120, 215); // Windows default accent as last resort
            }
        }

        /// <summary>
        /// Create an instance of a <see cref="WinTerminal"/> class that has all data from Windows Terminal settings.
        /// </summary>
        public WinTerminal() { }

        /// <summary>
        /// Create an instance of a <see cref="WinTerminal"/> class that has all data from Windows Terminal settings.
        /// </summary>
        public WinTerminal(string signature) { _signature = signature; }

        /// <summary>
        /// Create an instance of a <see cref="WinTerminal"/> class that has all data from Windows Terminal settings.
        /// </summary>
        /// <param name="File">File to be opened, either JSON or WinPaletter theme File</param>
        /// <param name="Mode">Either Windows Terminal JSON settings File or WinPaletter theme File</param>
        /// <param name="Version">Either Stable or Preview</param>
        public WinTerminal(string File, Mode Mode, Version Version = Version.Stable)
        {
            _signature = Version == Version.Stable ? "Terminal_Stable" : "Terminal_Preview";

            switch (Mode)
            {
                // Load Windows Terminal settings from JSON File
                case Mode.JSONFile:
                    {
                        WinTerminal result = new(string.Empty, Mode.Empty, Version);

                        if (System.IO.File.Exists(File))
                        {
                            string JSON_String;
                            using (StreamReader St = new(File))
                            {
                                JSON_String = St.ReadToEnd();
                                St.Close();
                            }

                            if (!string.IsNullOrEmpty(JSON_String))
                            {
                                // Parse to a JObject first so:
                                //  1. theme keyword resolution can look up "profiles"/"schemes" data
                                //     directly by name in the raw JSON, regardless of where "themes"
                                //     appears relative to them in the source file, and
                                //  2. profile->defaults layering can distinguish "key omitted" from
                                //     "key present with a value equal to the CLR default" for value-typed
                                //     members like useAcrylic (bool), opacity (int), etc.
                                JObject root = JObject.Parse(JSON_String);

                                JsonHelper.ResolveThemeBackgroundKeywords(root);
                                JsonHelper.ApplyProfileDefaultsLayering(root);

                                result = root.ToObject<WinTerminal>(JsonHelper.Serializer);
                            }
                            else
                            {
                                Program.Log?.Write(LogEventLevel.Information, $"Couldn't load Windows Terminal {(Version == Version.Stable ? "Stable" : "Preview")} settings from JSON file `{File}`.");
                            }

                            Program.Log?.Write(LogEventLevel.Information, $"Windows Terminal {(Version == Version.Stable ? "Stable" : "Preview")} settings have been loaded from JSON file `{File}`.");
                        }
                        else
                        {
                            Program.Log?.Write(LogEventLevel.Information, $"Couldn't load Windows Terminal {(Version == Version.Stable ? "Stable" : "Preview")} settings from JSON file `{File}`.");
                        }

                        Enabled = result.Enabled;
                        Theme = result.Theme;
                        DefaultProfile = result.DefaultProfile;
                        Profiles = result.Profiles;
                        Schemes = result.Schemes.Count == 0 ? DefaultSchemes : result.Schemes;
                        Themes = result.Themes;
                        UseAcrylicInTabRow = result.UseAcrylicInTabRow;

                        // Safety net for reference-typed members (colorScheme/icon/tabTitle/font) that
                        // could still be null for profiles whose defaults also lacked the key, or which
                        // reached this point through a code path that bypassed the raw-JSON layering.
                        InheritNullValuesFromDefaults();

                        break;
                    }

                // Load Windows Terminal settings from WinPaletter theme File
                case Mode.WinPaletterFile:
                    {
                        using (Manager TMx = new(Manager.Source.File, File))
                        {
                            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                            foreach (FieldInfo field in GetType().GetFields(bindingFlags))
                            {
                                if (Version == Version.Stable)
                                {
                                    field.SetValue(this, field.GetValue(TMx.Terminal));
                                }
                                else if (Version == Version.Preview)
                                {
                                    field.SetValue(this, field.GetValue(TMx.TerminalPreview));
                                }
                            }
                        }

                        // No raw JSON is available on this path, so the JSON pre-pass cannot run.
                        // Fall back to the reference-typed-only inheritance here; value-typed members
                        // are expected to have been baked into the .wpth file at save time.
                        InheritNullValuesFromDefaults();

                        Program.Log?.Write(LogEventLevel.Information, $"Windows Terminal {(Version == Version.Stable ? "Stable" : "Preview")} settings have been loaded from WinPaletter theme file `{File}`.");

                        break;
                    }

                case Mode.Empty:
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"An empty instance of Windows Terminal {(Version == Version.Stable ? "Stable" : "Preview")} settings has been created.");

                        break;
                    }
            }
        }

        /// <summary>
        /// For every profile in <see cref="Profiles.List"/>, fills any null/missing reference-typed
        /// field from <see cref="Types.Profiles.Defaults"/>. Mirrors Windows Terminal's runtime
        /// behaviour where a profile only overrides what it explicitly sets, and everything else is
        /// inherited from profiles.defaults.
        /// <para>
        /// This method can only see <c>null</c> for reference-typed members. Value-typed members
        /// (<c>bool</c>, <c>int</c>, <c>double</c>, enums, <c>Color</c>) cannot be <c>null</c> after
        /// deserialization, so their "omitted means inherit" semantics must be established on the raw
        /// JSON instead - see <see cref="JsonHelper.ApplyProfileDefaultsLayering"/>.
        /// </para>
        /// </summary>
        private void InheritNullValuesFromDefaults()
        {
            if (Profiles?.Defaults == null || Profiles.List == null) return;

            Profile defaults = Profiles.Defaults;

            foreach (Profile profile in Profiles.List)
            {
                if (profile == null || ReferenceEquals(profile, defaults)) continue;

                if (profile.ColorScheme == null)
                {
                    profile.ColorScheme = defaults.ColorScheme;
                }

                if (profile.Icon == null)
                {
                    profile.Icon = defaults.Icon;
                }

                if (profile.TabTitle == null)
                {
                    profile.TabTitle = defaults.TabTitle;
                }

                if (profile.Font == null)
                {
                    profile.Font = defaults.Font != null
                        ? (FontSettings)defaults.Font.Clone()
                        : new FontSettings();
                }
                else
                {
                    if (defaults.Font != null)
                    {
                        if (profile.Font.Face == null)
                        {
                            profile.Font.Face = defaults.Font.Face;
                        }

                        if (profile.Font.Size <= 0)
                        {
                            profile.Font.Size = defaults.Font.Size;
                        }
                    }
                }

                if (string.IsNullOrEmpty(profile.BackgroundImage))
                {
                    profile.BackgroundImage = defaults.BackgroundImage ?? string.Empty;
                }

                if (string.IsNullOrEmpty(profile.Commandline))
                {
                    profile.Commandline = defaults.Commandline ?? string.Empty;
                }
            }
        }

        /// <summary>
        /// Save Windows Terminal settings data
        /// </summary>
        /// <param name="File">File into which data will be saved, either JSON or WinPaletter theme File</param>
        /// <param name="Mode">Either Windows Terminal JSON settings File or WinPaletter theme File</param>
        /// <param name="Version">Either Stable or Preview</param>
        /// <returns></returns>
        public string Save(Mode Mode, Version Version = Version.Stable)
        {
            return Save(null, Mode, Version);
        }

        /// <summary>
        /// The Windows Terminal built-in defaults, expressed as a JObject, used as the baseline
        /// for the compaction pass during save. Anything that matches the corresponding entry here
        /// is omitted from the output, exactly like Windows Terminal itself does.
        /// </summary>
        private static JObject GetBuiltInDefaults()
        {
            // Build a JObject that mirrors what Windows Terminal writes when a profile/scheme/theme does not override anything.
            // These values match the WT schema defaults documented at https://learn.microsoft.com/windows/terminal/customize-settings/profile-general
            return new JObject
            {
                ["$help"] = "https://aka.ms/terminal-documentation",
                ["$schema"] = "https://aka.ms/terminal-profiles-schema",
                ["defaultProfile"] = null,
                ["profiles"] = new JObject
                {
                    ["defaults"] = new JObject(),
                    ["list"] = new JArray()
                },
                ["schemes"] = new JArray(),
                ["themes"] = new JArray(),
                ["actions"] = new JArray(),
                ["keybindings"] = new JArray(),
                ["useAcrylicInTabRow"] = false,
                ["theme"] = "system",
                ["showTabsInTitlebar"] = true,
                ["showTerminalTitleInTitlebar"] = true,
                ["initialCols"] = 120,
                ["initialRows"] = 30,
                ["launchMode"] = "default",
                ["confirmCloseAllTabs"] = true,
                ["startOnUserLogin"] = false,
                ["snapToGridOnResize"] = true,
                ["tabWidthMode"] = "equal",
                ["alwaysShowTabs"] = true,
                ["copyOnSelect"] = false,
                ["copyFormatting"] = false,
                ["focusFollowMouse"] = false,
                ["wordDelimiters"] = " /\\()\"'-:,.;<>~!@#$%^&*|+=[]{}~?\u2502",
                ["trimBlockSelection"] = true,
                ["debugFeaturesEnabled"] = false,
                ["startupActions"] = "nt",
                ["windowingBehavior"] = "useNew",
                ["useTabSwitcher"] = true,
                ["disableAnimations"] = false
            };
        }

        /// <summary>
        /// Windows Terminal's built-in Campbell scheme, used as the baseline for compacting user
        /// schemes. Any key in a user scheme that equals Campbell's corresponding key is omitted.
        /// </summary>
        private static JObject GetBuiltInScheme()
        {
            // Serialize the first DefaultSchemes entry (Campbell) through the same serializer used
            // elsewhere, so the JSON representation matches exactly what gets written for user schemes.
            return JObject.FromObject(DefaultSchemes[0], JsonHelper.Serializer);
        }

        /// <summary>
        /// Windows Terminal's built-in default theme(s), used as the baseline for compacting user
        /// themes. Windows Terminal does not ship named themes by default, so the baseline is empty
        /// apart from the implicit "applicationTheme" and the two tab/tabRow color slots.
        /// </summary>
        private static JObject GetBuiltInTheme()
        {
            return new JObject
            {
                ["window"] = new JObject
                {
                    ["applicationTheme"] = "system"
                },
                ["tab"] = new JObject(),
                ["tabRow"] = new JObject()
            };
        }

        /// <summary>
        /// Windows Terminal's built-in profile defaults, used as the baseline for compacting both
        /// <c>profiles.defaults</c> and individual profiles. Keys matching these values are omitted,
        /// exactly like Windows Terminal itself does.
        /// </summary>
        private static JObject GetBuiltInProfileDefaults()
        {
            return new JObject
            {
                ["colorScheme"] = "Campbell",
                ["cursorShape"] = "bar",
                ["cursorHeight"] = 25,
                ["useAcrylic"] = false,
                ["opacity"] = 100,
                ["backgroundImageOpacity"] = 1.0,
                ["font"] = new JObject
                {
                    ["face"] = "Cascadia Mono",
                    ["size"] = 12,
                    ["weight"] = "normal"
                }
            };
        }

        /// <summary>
        /// Keys that Windows Terminal's schema requires to be present, even when empty. These are
        /// re-added after every compaction pass, because Windows Terminal will refuse to load a
        /// settings.json that is missing any of them.
        /// <para>
        /// Path syntax: "/" separates JSON object properties; a leading "/" is the document root.
        /// Each entry also specifies the kind of container (object or array) that must exist at
        /// that path.
        /// </para>
        /// </summary>
        private static readonly (string Path, JTokenType Kind)[] RequiredKeys =
        [
            ("/profiles",            JTokenType.Object),
            ("/profiles/defaults",   JTokenType.Object),
            ("/profiles/list",       JTokenType.Array),
            ("/schemes",             JTokenType.Array),
            ("/themes",              JTokenType.Array)
        ];

        /// <summary>
        /// Ensures every key listed in <see cref="RequiredKeys"/> exists at its expected path with
        /// the expected container type. Missing keys are created empty.
        /// </summary>
        private static void EnsureRequiredKeys(JObject root)
        {
            foreach ((string path, JTokenType kind) in RequiredKeys)
            {
                EnsureRequiredKey(root, path, kind);
            }
        }

        private static void EnsureRequiredKey(JObject root, string path, JTokenType kind)
        {
            string[] segments = path.Split(['/'], StringSplitOptions.RemoveEmptyEntries);

            JObject current = root;

            for (int i = 0; i < segments.Length - 1; i++)
            {
                string segment = segments[i];

                if (current[segment] is not JObject next)
                {
                    // Missing or wrong type intermediate: replace with a fresh object.
                    next = new JObject();
                    current[segment] = next;
                }

                current = next;
            }

            string leaf = segments[segments.Length - 1];
            JToken existing = current[leaf];

            if (existing == null || existing.Type != kind)
            {
                current[leaf] = kind == JTokenType.Array ? new JArray() : new JObject();
            }
        }

        /// <summary>
        /// Removes from <paramref name="value"/> any property whose value is deep-equal to the
        /// corresponding property in <paramref name="reference"/>. Recurses into nested objects
        /// and arrays. After this runs, <paramref name="value"/> contains only the keys that
        /// actually differ from the reference - which is exactly the "compact" representation
        /// Windows Terminal itself writes.
        /// <para>
        /// Special cases:
        /// <list type="bullet">
        /// <item>For <c>profiles.list</c>, the compaction is per-profile (each profile has its own
        /// reference: <c>profiles.defaults</c> after its own compaction).</item>
        /// <item>For <c>schemes</c>, each scheme is compacted against the built-in Campbell scheme.</item>
        /// <item>For <c>themes</c>, each theme is compacted against the built-in theme defaults.</item>
        /// </list>
        /// </para>
        /// </summary>
        private static void CompactAgainst(JObject value, JObject reference)
        {
            foreach (JProperty property in value.Properties().ToList())
            {
                JToken refValue = reference[property.Name];

                if (refValue == null)
                {
                    // No matching reference key: keep it. But still recurse into it if it's an object,
                    // to strip empty nested objects.
                    if (property.Value is JObject childObj) RemoveEmptyContainers(childObj);
                    continue;
                }

                if (property.Value is JObject obj && refValue is JObject refObj)
                {
                    CompactAgainst(obj, refObj);

                    // If the object is now empty, drop it entirely.
                    if (!obj.Properties().Any())
                    {
                        property.Remove();
                    }
                    else
                    {
                        RemoveEmptyContainers(obj);
                    }
                }
                else if (property.Value is JArray arr && refValue is JArray refArr)
                {
                    // Arrays are not compacted element-by-element against a reference; they are
                    // simply stripped of empty entries and dropped entirely if empty.
                    StripEmptyEntries(arr);

                    if (!arr.Any())
                    {
                        property.Remove();
                    }
                }
                else if (JToken.DeepEquals(property.Value, refValue))
                {
                    property.Remove();
                }
            }
        }

        /// <summary>
        /// Removes any property whose value is null, an empty object, or an empty array - recursively.
        /// </summary>
        private static void RemoveEmptyContainers(JObject obj)
        {
            foreach (JProperty property in obj.Properties().ToList())
            {
                JToken value = property.Value;

                if (value.Type == JTokenType.Null)
                {
                    property.Remove();
                    continue;
                }

                if (value is JObject childObj)
                {
                    RemoveEmptyContainers(childObj);

                    if (!childObj.Properties().Any())
                    {
                        property.Remove();
                    }
                }
                else if (value is JArray childArr)
                {
                    StripEmptyEntries(childArr);

                    if (!childArr.Any())
                    {
                        property.Remove();
                    }
                }
                else if (value.Type == JTokenType.String && string.IsNullOrEmpty(value.Value<string>()))
                {
                    // Do not strip empty strings in general - the "name" or "backgroundImage" key may
                    // legitimately be an empty string. Only strip if the key is one whose empty value
                    // is equivalent to "unset". Windows Terminal itself keeps "" for backgroundImage
                    // when present, so leave empty strings alone.
                }
            }
        }

        /// <summary>
        /// Removes empty entries from an array. Array elements are not compared against anything.
        /// </summary>
        private static void StripEmptyEntries(JArray arr)
        {
            for (int i = arr.Count - 1; i >= 0; i--)
            {
                JToken item = arr[i];

                if (item.Type == JTokenType.Null)
                {
                    arr.RemoveAt(i);
                    continue;
                }

                if (item is JObject obj)
                {
                    RemoveEmptyContainers(obj);

                    if (!obj.Properties().Any())
                    {
                        arr.RemoveAt(i);
                    }
                }
                else if (item is JArray innerArr)
                {
                    StripEmptyEntries(innerArr);

                    if (!innerArr.Any())
                    {
                        arr.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// Save Windows Terminal settings data
        /// </summary>
        /// <param name="treeView">TreeView control for logging</param>
        /// <param name="Mode">Either Windows Terminal JSON settings File or WinPaletter theme File</param>
        /// <param name="Version">Either Stable or Preview</param>
        /// <returns></returns>
        public string Save(TreeView treeView, Mode Mode, Version Version = Version.Stable)
        {
            SaveToggleState(treeView);

            if (!Enabled) return string.Empty;

            switch (Mode)
            {
                // Save Windows Terminal settings to JSON File
                case Mode.JSONFile:
                    {
                        string SettingsFile = string.Empty;

                        // Determine the path of the Windows Terminal settings JSON File based on the _ver
                        switch (Version)
                        {
                            case Version.Stable:
                                {
                                    if (!Program.Settings.WindowsTerminals.Path_Deflection)
                                    {
                                        SettingsFile = SysPaths.TerminalJSON;
                                    }
                                    else if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                                    {
                                        SettingsFile = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                                    }
                                    else
                                    {
                                        SettingsFile = SysPaths.TerminalJSON;
                                    }

                                    Program.Log?.Write(LogEventLevel.Information, $"Saving Windows Terminal Stable settings into JSON file `{SettingsFile}`.");

                                    break;
                                }

                            case Version.Preview:
                                {
                                    if (!Program.Settings.WindowsTerminals.Path_Deflection)
                                    {
                                        SettingsFile = SysPaths.TerminalPreviewJSON;
                                    }
                                    else if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
                                    {
                                        SettingsFile = Program.Settings.WindowsTerminals.Terminal_Preview_Path;
                                    }
                                    else
                                    {
                                        SettingsFile = SysPaths.TerminalPreviewJSON;
                                    }

                                    Program.Log?.Write(LogEventLevel.Information, $"Saving Windows Terminal Preview settings into JSON file `{SettingsFile}`.");

                                    break;
                                }
                        }

                        // Load the original JSON from the File
                        JObject existingJson;

                        if (System.IO.File.Exists(SettingsFile))
                        {
                            using (FileStream fileStream = new(SettingsFile, FileMode.Open, FileAccess.Read))
                            using (StreamReader streamReader = new(fileStream))
                            using (JsonTextReader jsonReader = new(streamReader))
                            {
                                existingJson = JObject.Load(jsonReader);
                            }
                        }
                        else
                        {
                            existingJson = [];
                        }

                        // Create a new JObject from the current instance
                        JObject newJson = JObject.FromObject(this, JsonHelper.Serializer);

                        // Merge properties from newJson to existingJson
                        Merge(existingJson, newJson);

                        // Schemes from new JSON are taken unmodified first, then compacted against
                        // the built-in Campbell scheme, so user schemes only carry what differs.
                        existingJson["schemes"] = newJson["schemes"];

                        // Compact the whole document against the Windows Terminal built-in defaults.
                        // This removes:
                        //   - top-level keys whose value equals the built-in default,
                        //   - profiles.defaults keys whose value equals the built-in profile defaults,
                        //   - per-profile keys whose value equals the (possibly already-compacted) defaults,
                        //   - per-scheme keys whose value equals the built-in Campbell scheme,
                        //   - per-theme keys whose value equals the built-in theme defaults,
                        //   - empty objects and empty arrays everywhere.
                        JObject builtInDefaults = GetBuiltInDefaults();
                        CompactAgainst(existingJson, builtInDefaults);

                        // profiles.list: compact each profile against profiles.defaults.
                        if (existingJson["profiles"] is JObject profilesObj)
                        {
                            JObject defaultsObj = profilesObj["defaults"] as JObject;

                            if (profilesObj["list"] is JArray profilesList && defaultsObj != null)
                            {
                                foreach (JObject profile in profilesList.OfType<JObject>())
                                {
                                    CompactAgainst(profile, defaultsObj);
                                }

                                // Remove any profile that became empty (all its keys matched the defaults).
                                for (int i = profilesList.Count - 1; i >= 0; i--)
                                {
                                    if (profilesList[i] is JObject p && !p.Properties().Any())
                                    {
                                        profilesList.RemoveAt(i);
                                    }
                                }
                            }

                            // profiles.defaults itself: compact against the built-in profile defaults.
                            if (defaultsObj != null)
                            {
                                CompactAgainst(defaultsObj, GetBuiltInProfileDefaults());
                            }
                        }

                        // schemes: compact each against the built-in Campbell scheme.
                        if (existingJson["schemes"] is JArray schemesArr)
                        {
                            JObject builtInScheme = GetBuiltInScheme();

                            foreach (JObject scheme in schemesArr.OfType<JObject>())
                            {
                                CompactAgainst(scheme, builtInScheme);
                            }

                            // Remove empty schemes (a scheme whose only content was "name" equal to Campbell is meaningless; drop it).
                            for (int i = schemesArr.Count - 1; i >= 0; i--)
                            {
                                if (schemesArr[i] is JObject s && !s.Properties().Any())
                                {
                                    schemesArr.RemoveAt(i);
                                }
                            }
                        }

                        // themes: compact each against the built-in theme defaults.
                        if (existingJson["themes"] is JArray themesArr)
                        {
                            JObject builtInTheme = GetBuiltInTheme();

                            foreach (JObject theme in themesArr.OfType<JObject>())
                            {
                                CompactAgainst(theme, builtInTheme);
                            }

                            for (int i = themesArr.Count - 1; i >= 0; i--)
                            {
                                if (themesArr[i] is JObject t && !t.Properties().Any())
                                {
                                    themesArr.RemoveAt(i);
                                }
                            }
                        }

                        // Final pass: strip any remaining nulls / empty containers from the root.
                        RemoveEmptyContainers(existingJson);

                        // Re-add the keys that Windows Terminal's schema requires to be present, even
                        // when they are empty. RemoveEmptyContainers above would have stripped them;
                        // this guarantees schemes: [], themes: [], profiles: { defaults: {}, list: [] }
                        // are always written, matching what Windows Terminal itself produces.
                        EnsureRequiredKeys(existingJson);

                        // Serialize the merged JObject to a JSON string. Formatting.Indented matches
                        // what Windows Terminal itself writes.
                        string result = existingJson.ToString(Formatting.Indented);

                        // Take ownership of Windows Terminal settings JSON File
                        TakeOwnership(SettingsFile);

                        // Write the updated JSON to the File
                        using (FileStream fileStream = new(SettingsFile, FileMode.Create, FileAccess.Write))
                        using (StreamWriter streamWriter = new(fileStream))
                        {
                            streamWriter.Write(result);
                        }

                        Program.Log?.Write(LogEventLevel.Information, $"Saving `{SettingsFile}` has just been completed.");

                        return result;
                    }

                default:
                    {
                        return string.Empty;
                    }
            }
        }

        /// <summary>
        /// Saves the toggle state of this Windows Terminal instance.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="edition"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", signatureEnabled, Enabled);
        }
    }
}