using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
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
                /// Gets or sets the default profile settings.
                /// </summary>
                [JsonProperty("defaults")]
                public Profile Defaults { get; set; } = new() { ColorScheme = new() { Dark = "Campbell", Light = "Campbell" } };

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
                /// Returns string format of current color scheme
                /// </summary>
                /// <returns></returns>
                public override string ToString()
                {
                    return Light ?? Dark;

                    //// Check if dark and light are the same; if so, return a single value
                    //if (Dark == Light)
                    //{
                    //    return Dark;
                    //}

                    //// Otherwise, return the ColorScheme object as JSON
                    //return JsonConvert.SerializeObject(this);
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
                /// Write values in JSON format
                /// </summary>
                /// <param name="writer"></param>
                /// <param name="value"></param>
                /// <param name="serializer"></param>
                public override void WriteJson(JsonWriter writer, ColorScheme value, JsonSerializer serializer)
                {
                    // Use the custom ToString method for serialization
                    writer.WriteValue(value.ToString());
                }

                /// <summary>
                /// Read values in JSON format and convert them to <see cref="ColorScheme"/> objects
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
                        return new() { Dark = reader.Value.ToString(), Light = reader.Value.ToString() };
                    }
                    else if (reader.TokenType == JsonToken.StartObject)
                    {
                        // If it's an object, use the default deserialization for ColorScheme
                        return serializer.Deserialize<ColorScheme>(reader);
                    }
                    else
                    {
                        // If it's neither a string nor an object, create a ColorScheme with both dark and light set to null
                        return new ColorScheme { Dark = null, Light = null };
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

                    // Remove unsupported characters
                    hexColor = Regex.Replace(hexColor, "[，、]", ",");

                    // Replace spaces with commas if exist as ColorTranslator.FromHtml does not support spaces
                    if (hexColor.Contains(" ") && !hexColor.Contains(",")) hexColor = hexColor.Replace(" ", ", ");

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
            return token == null ||
                   (token.Type == JTokenType.String && string.IsNullOrWhiteSpace(token.Value<string>())) ||
                   (token.Type == JTokenType.Null);
        }

        /// <summary>
        /// Take ownership of a File for the current Windows user
        /// </summary>
        /// <param name="filepath"></param>
        private void TakeOwnership(string filepath)
        {
            Process proc = new();
            proc.StartInfo.FileName = "takeown.exe";
            proc.StartInfo.Arguments = $"/R /F \"{filepath}\"";
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
            proc.StartInfo.FileName = "icacls.exe";
            proc.StartInfo.Arguments = "\"{filepath}\" /grant *{GROUP_USERS_SID}:F /T";
            proc.Start();
            proc.WaitForExit();
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

        private readonly List<Scheme> DefaultSchemes =
        [
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
            BrightYellow = Color.FromArgb(228, 192, 122),
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

            // Reusable serializer (no per-call allocations)
            private static readonly JsonSerializer serializer = JsonSerializer.Create(cachedSettings);

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

                return serializer.Deserialize<T>(reader);
            }
        }

        /// <summary>
        /// Create an instance of a <see cref="WinTerminal"/> class that has all data from Windows Terminal settings.
        /// </summary>
        public WinTerminal() { }

        /// <summary>
        /// Create an instance of a <see cref="WinTerminal"/> class that has all data from Windows Terminal settings.
        /// </summary>
        /// <param name="File">File to be opened, either JSON or WinPaletter theme File</param>
        /// <param name="Mode">Either Windows Terminal JSON settings File or WinPaletter theme File</param>
        /// <param name="Version">Either Stable or Preview</param>
        public WinTerminal(string File, Mode Mode, Version Version = Version.Stable)
        {
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

                            if (!string.IsNullOrEmpty(JSON_String)) result = JsonHelper.DeserializeFast<WinTerminal>(JSON_String);
                            else Program.Log?.Write(LogEventLevel.Information, $"Couldn't load Windows Terminal {(Version == Version.Stable ? "Stable" : "Preview")} settings from JSON file `{File}`.");

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
        /// Save Windows Terminal settings data
        /// </summary>
        /// <param name="File">File into which data will be saved, either JSON or WinPaletter theme File</param>
        /// <param name="Mode">Either Windows Terminal JSON settings File or WinPaletter theme File</param>
        /// <param name="Version">Either Stable or Preview</param>
        /// <returns></returns>
        public string Save(string File, Mode Mode, Version Version = Version.Stable)
        {
            switch (Mode)
            {
                // Save Windows Terminal settings to JSON File
                case Mode.JSONFile:
                    {
                        string SettingsFile = string.Empty;

                        // Determine the path of the Windows Terminal settings JSON File based on the Version
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
                                    else if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                                    {
                                        SettingsFile = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
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
                        JObject newJson = JObject.FromObject(this);

                        // Merge properties from newJson to existingJson
                        Merge(existingJson, newJson);

                        // Get schemes from new JSON unmodified (solve the problem of schemes not being merged correctly)
                        existingJson["schemes"] = newJson["schemes"];

                        // Remove default properties from other profiles, so Windows Terminal will handle this and apply default values automatically (Like what it actually does)
                        {
                            Program.Log?.Write(LogEventLevel.Information, $"Removing default properties from other profiles, so Windows Terminal will handle this and apply default values automatically (Like what it actually does).");

                            // Retrieve the list of profiles and the defaults JObject
                            JArray profilesList = (JArray)existingJson["profiles"]["list"];
                            JObject defaults = existingJson["profiles"]["defaults"] as JObject;

                            for (int i = 0; i < profilesList.Count; i++)
                            {
                                // Retrieve each profile from the list
                                JObject profile = (JObject)profilesList[i];

                                // Remove default properties from the profile
                                profile = RemoveDefaultProperties(profile, defaults);

                                // Remove the profile if it is empty
                                if (!profile.Properties().Any())
                                {
                                    profilesList.RemoveAt(i);
                                    i--;
                                }
                                else
                                {
                                    // Put the modified profile back into the list
                                    profilesList[i] = profile;
                                }
                            }

                            // Update the profiles list in the existingJson JObject
                            existingJson["profiles"]["list"] = profilesList;
                        }

                        // Serialize the merged JObject to a JSON string
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
    }
}