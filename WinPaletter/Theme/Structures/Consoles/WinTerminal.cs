using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Documents;
using static WinPaletter.WinTerminal.Types;

namespace WinPaletter
{
    /// <summary>
    /// Class that has data of Windows Terminal settings
    /// </summary>
    public class WinTerminal : ICloneable, IEquatable<WinTerminal>
    {
        /// <summary>
        /// Enumeration for ways by which WinPaletter can get Windows Terminal settings data
        /// </summary>
        public enum Mode : int
        {
            /// <summary>Default Windows Terminal settings</summary>
            Default,
            /// <summary>Windows Terminal JSON settings file</summary>
            JSONFile,
            /// <summary>WinPaletter theme  file</summary>
            WinPaletterFile,
            /// <summary>Empty data that has nothing; no profiles, no themes, ...</summary>
            Empty
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
        [JsonIgnore]
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
        public Types.Profiles Profiles { get; set; } = new();

        /// <summary>
        /// Gets or sets the color schemes for the terminal.
        /// </summary>
        [JsonProperty("schemes")]
        public List<Types.Scheme> Schemes { get; set; } = new();

        /// <summary>
        /// Gets or sets the default theme for the terminal.
        /// </summary>
        [JsonProperty("theme")]
        public string Theme { get; set; }

        /// <summary>
        /// Gets or sets the list of available themes for the terminal.
        /// </summary>
        [JsonProperty("themes")]
        public List<Types.Theme> Themes { get; set; } = new();

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
                public Profile Defaults { get; set; } = new() { ColorScheme = "Campbell" };

                /// <summary>
                /// Gets or sets the list of profiles.
                /// </summary>
                [JsonProperty("list")]
                public List<Profile> List { get; set; } = new();

                /// <summary>
                /// Clone current Windows Terminal profiles
                /// </summary>
                /// <returns></returns>
                public object Clone()
                {
                    return new Profiles
                    {
                        Defaults = Defaults.Clone() as Profile,
                        List = new List<Profile>(List.Select(p => p.Clone() as Profile))
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
                public string ColorScheme { get; set; }

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
                public Color Background { get; set; }

                /// <summary>
                /// Gets or sets the black color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("black")]
                public Color Black { get; set; }

                /// <summary>
                /// Gets or sets the blue color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("blue")]
                public Color Blue { get; set; }

                /// <summary>
                /// Gets or sets the bright black color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightBlack")]
                public Color BrightBlack { get; set; }

                /// <summary>
                /// Gets or sets the bright blue color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightBlue")]
                public Color BrightBlue { get; set; }

                /// <summary>
                /// Gets or sets the bright cyan color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightCyan")]
                public Color BrightCyan { get; set; }

                /// <summary>
                /// Gets or sets the bright green color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightGreen")]
                public Color BrightGreen { get; set; }

                /// <summary>
                /// Gets or sets the bright purple color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightPurple")]
                public Color BrightPurple { get; set; }

                /// <summary>
                /// Gets or sets the bright red color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightRed")]
                public Color BrightRed { get; set; }

                /// <summary>
                /// Gets or sets the bright white color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightWhite")]
                public Color BrightWhite { get; set; }

                /// <summary>
                /// Gets or sets the bright yellow color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("brightYellow")]
                public Color BrightYellow { get; set; }

                /// <summary>
                /// Gets or sets the cursor color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("cursorColor")]
                public Color CursorColor { get; set; }

                /// <summary>
                /// Gets or sets the cyan color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("cyan")]
                public Color Cyan { get; set; }

                /// <summary>
                /// Gets or sets the foreground color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("foreground")]
                public Color Foreground { get; set; }

                /// <summary>
                /// Gets or sets the green color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("green")]
                public Color Green { get; set; }

                /// <summary>
                /// Gets or sets the purple color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("purple")]
                public Color Purple { get; set; }

                /// <summary>
                /// Gets or sets the red color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("red")]
                public Color Red { get; set; }

                /// <summary>
                /// Gets or sets the selection background color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("selectionBackground")]
                public Color SelectionBackground { get; set; }

                /// <summary>
                /// Gets or sets the white color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("white")]
                public Color White { get; set; }

                /// <summary>
                /// Gets or sets the yellow color for the scheme.
                /// </summary>
                [JsonConverter(typeof(ColorConverter))]
                [JsonProperty("yellow")]
                public Color Yellow { get; set; }
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
            /// Read values in .NET color format and convert them to hex color format
            /// </summary>
            private class ColorConverter : JsonConverter
            {
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

                public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
                {
                    if (reader.TokenType == JsonToken.Null)
                    {
                        return null;
                    }

                    string hexColor = (string)reader.Value;

                    // Handle empty string
                    if (string.IsNullOrEmpty(hexColor))
                    {
                        // Return a default color or null, depending on your needs
                        return Color.Empty; // or return null;
                    }

                    return ColorTranslator.FromHtml(hexColor);
                }

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
            public class FontWeightConverter : JsonConverter<FontWeight>
            {
                /// <inheritdoc/>
                public override FontWeight ReadJson(JsonReader reader, Type objectType, FontWeight existingValue, bool hasExistingValue, JsonSerializer serializer)
                {
                    if (Enum.TryParse(reader.Value?.ToString(), true, out FontWeight result))
                    {
                        return result;
                    }

                    return FontWeight.Normal; // Default value if parsing fails
                }

                /// <inheritdoc/>
                public override void WriteJson(JsonWriter writer, FontWeight value, JsonSerializer serializer)
                {
                    // Use reflection to get the JsonProperty attribute value
                    var enumType = typeof(FontWeight);
                    var fieldInfo = enumType.GetField(value.ToString());
                    var jsonPropertyAttribute = (JsonPropertyAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(JsonPropertyAttribute));

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
            public class CursorShapeConverter : JsonConverter<CursorShape>
            {
                /// <inheritdoc/>
                public override CursorShape ReadJson(JsonReader reader, Type objectType, CursorShape existingValue, bool hasExistingValue, JsonSerializer serializer)
                {
                    if (Enum.TryParse(reader.Value?.ToString(), true, out CursorShape result))
                    {
                        return result;
                    }

                    return CursorShape.Bar; // Default value if parsing fails
                }

                /// <inheritdoc/>
                public override void WriteJson(JsonWriter writer, CursorShape value, JsonSerializer serializer)
                {
                    // Use reflection to get the JsonProperty attribute value
                    var enumType = typeof(CursorShape);
                    var fieldInfo = enumType.GetField(value.ToString());
                    var jsonPropertyAttribute = (JsonPropertyAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(JsonPropertyAttribute));

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
            public class BackgroundImageOpacityConverter : JsonConverter<double>
            {
                /// <inheritdoc/>
                public override double ReadJson(JsonReader reader, Type objectType, double existingValue, bool hasExistingValue, JsonSerializer serializer)
                {
                    if (double.TryParse(reader.Value?.ToString(), out double result))
                    {
                        return Math.Max(0, Math.Min(result / 100, 1));
                    }

                    return 1; // Default value if parsing fails
                }

                /// <inheritdoc/>
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
                /// Empty box cursor shape.
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
        /// Merging two JObjects with desired behavior
        /// <br>Desired behavior is to merge two JSONs without including null values</br>
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
        void MergeArrays(JArray originalArray, JArray newArray)
        {
            List<JObject> originalItems = originalArray.Children<JObject>().ToList();

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
        /// Take ownership of a file for the current Windows user
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
        /// Remove default properties from a JObject
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaults"></param>
        /// <returns></returns>
        public static JObject RemoveDefaultProperties(JObject value, JObject defaults)
        {
            // Create a list to store properties to remove
            List<string> propertiesToRemove = new List<string>();

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

            // Remove properties after the loop to avoid modifying the collection during iteration
            foreach (string propertyName in propertiesToRemove)
            {
                value.Property(propertyName)?.Remove();
            }

            return value;
        }

        /// <summary>
        /// Check if two JToken values are equal
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

        #endregion

        /// <summary>
        /// Create an instance of class has all data of Windows Terminal settings
        /// </summary>
        /// <param name="File">File to be opened, either JSON or WinPaletter theme file</param>
        /// <param name="Mode">Either Windows Terminal JSON settings file or WinPaletter theme file</param>
        /// <param name="Version">Either Stable or Preview</param>
        public WinTerminal(string File, Mode Mode, Version Version = Version.Stable)
        {
            switch (Mode)
            {
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

                            if (!string.IsNullOrEmpty(JSON_String)) result = JsonConvert.DeserializeObject<WinTerminal>(JSON_String);
                        }

                        Theme = result.Theme;
                        DefaultProfile = result.DefaultProfile;
                        Profiles = result.Profiles;
                        Schemes = result.Schemes;
                        Themes = result.Themes;
                        UseAcrylicInTabRow = result.UseAcrylicInTabRow;

                        break;
                    }

                case Mode.WinPaletterFile:
                    {
                        using (Theme.Manager TMx = new(WinPaletter.Theme.Manager.Source.File, File))
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

                        break;
                    }

                case Mode.Empty:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// ApplyToTM Windows Terminal settings data
        /// </summary>
        /// <param name="File">File into which data will be saved, either JSON or WinPaletter theme file</param>
        /// <param name="Mode">Either Windows Terminal JSON settings file or WinPaletter theme file</param>
        /// <param name="Version">Either Stable or Preview</param>
        /// <returns></returns>
        public string Save(string File, Mode Mode, Version Version = Version.Stable)
        {
            switch (Mode)
            {
                case Mode.JSONFile:
                    {
                        string SettingsFile = string.Empty;

                        switch (Version)
                        {
                            case Version.Stable:
                                {
                                    if (!Program.Settings.WindowsTerminals.Path_Deflection)
                                    {
                                        SettingsFile = PathsExt.TerminalJSON;
                                    }
                                    else if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                                    {
                                        SettingsFile = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                                    }
                                    else
                                    {
                                        SettingsFile = PathsExt.TerminalJSON;
                                    }

                                    break;
                                }

                            case Version.Preview:
                                {
                                    if (!Program.Settings.WindowsTerminals.Path_Deflection)
                                    {
                                        SettingsFile = PathsExt.TerminalPreviewJSON;
                                    }
                                    else if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                                    {
                                        SettingsFile = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                                    }
                                    else
                                    {
                                        SettingsFile = PathsExt.TerminalPreviewJSON;
                                    }

                                    break;
                                }
                        }

                        // Load the original JSON from the file
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
                            existingJson = new JObject();
                        }

                        // Create a new JObject from the current instance
                        JObject newJson = JObject.FromObject(this);

                        // Merge properties from newJson to existingJson
                        Merge(existingJson, newJson);

                        // Get schemes from new JSON unmodified (solve the problem of schemes not being merged correctly)
                        existingJson["schemes"] = newJson["schemes"];

                        // Remove default properties from the profiles, so Windows Terminal will handle this and apply default values automatically
                        {
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

                        // Take ownership of Windows Terminal settings JSON file
                        TakeOwnership(SettingsFile);

                        // Write the updated JSON to the file
                        using (FileStream fileStream = new(SettingsFile, FileMode.Create, FileAccess.Write))
                        using (StreamWriter streamWriter = new(fileStream))
                        {
                            streamWriter.Write(result);
                        }

                        return result;
                    }

                default:
                    {
                        return string.Empty;
                    }
            }
        }

        /// <summary>
        /// Clone current Windows Terminal settings
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            // Deep copy the WinTerminal instance
            return new WinTerminal(string.Empty, Mode.Empty, Version.Stable)
            {
                Enabled = Enabled,
                DefaultProfile = DefaultProfile,
                Profiles = Profiles.Clone() as Profiles,
                Schemes = new List<Types.Scheme>(Schemes),
                Theme = Theme,
                Themes = new List<Types.Theme>(Themes),
                UseAcrylicInTabRow = UseAcrylicInTabRow
            };
        }

        /// <summary>
        /// Checks if two Windows Terminals settings are equal or not
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as WinTerminal);
        }

        /// <summary>
        /// Checks if two Windows Terminals settings are equal or not
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(WinTerminal other)
        {
            return other != null &&
                   Enabled == other.Enabled &&
                   DefaultProfile == other.DefaultProfile &&
                   EqualityComparer<Types.Profiles>.Default.Equals(Profiles, other.Profiles) &&
                   EqualityComparer<List<Types.Scheme>>.Default.Equals(Schemes, other.Schemes) &&
                   Theme == other.Theme &&
                   EqualityComparer<List<Types.Theme>>.Default.Equals(Themes, other.Themes) &&
                   UseAcrylicInTabRow == other.UseAcrylicInTabRow;
        }

        /// <summary>
        /// Get hash code of Windows Terminal settings
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = (hash * 23) + Enabled.GetHashCode();
                hash = (hash * 23) + (DefaultProfile?.GetHashCode() ?? 0);
                hash = (hash * 23) + (Profiles?.GetHashCode() ?? 0);
                hash = (hash * 23) + (Schemes?.GetHashCode() ?? 0);
                hash = (hash * 23) + (Theme?.GetHashCode() ?? 0);
                hash = (hash * 23) + (Themes?.GetHashCode() ?? 0);
                hash = (hash * 23) + UseAcrylicInTabRow.GetHashCode();

                return hash;
            }
        }

        /// <summary>
        /// Checks if two Windows Terminals settings are equal
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(WinTerminal left, WinTerminal right)
        {
            return EqualityComparer<WinTerminal>.Default.Equals(left, right);
        }

        /// <summary>
        /// Checks if two Windows Terminals settings are not equal
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(WinTerminal left, WinTerminal right)
        {
            return !(left == right);
        }
    }
}