using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using static WinPaletter.UI.Style.Config;
using static WinPaletter.UI.Style.Config.Colors_Collection;

namespace WinPaletter.UI.Style
{
    /// <summary>
    /// A form for editing the color scheme of the application (Engineering mode)
    /// </summary>
    public class SchemeEditor : UI.WP.Form
    {
        /// <summary>
        /// The collection of colors instance that is being edited
        /// </summary>
        private readonly Colors_Collection colorCollection;

        /// <summary>
        /// A ColorDialog instance for selecting colors
        /// </summary>
        readonly ColorDialog colorDialog = new();

        /// <summary>
        /// The panel for selecting the accent color
        /// </summary>
        private Panel accentPanel;

        /// <summary>
        /// The panel for selecting the back color
        /// </summary>
        private Panel backPanel;

        /// <summary>
        /// TestControl instances for button none state
        /// </summary>
        private WP.TestControl buttonNoneControl;

        /// <summary>
        /// TestControl instances for button over state
        /// </summary>
        private WP.TestControl buttonOverControl;

        /// <summary>
        /// TestControl instances for button down state
        /// </summary>
        private WP.TestControl buttonDownControl;

        /// <summary>
        /// TestControl instances for non checked state
        /// </summary>
        private WP.TestControl noneControl;

        /// <summary>
        /// TestControl instances for hover state
        /// </summary>
        private WP.TestControl hoverControl;

        /// <summary>
        /// TestControl instances for checked hover state
        /// </summary>
        private WP.TestControl checkedHoverControl;

        /// <summary>
        /// TestControl instances for checked state
        /// </summary>
        private WP.TestControl checkedControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemeEditor"/> class.
        /// </summary>
        public SchemeEditor()
        {
            // Initialize with default colors
            colorCollection = Program.Style.Schemes.Main.Colors.Clone() as Colors_Collection;

            // Create controls for editing the color scheme
            CreateColorControls();

            // Create TestControls for different states
            CreateTestControls();

            // Update color panels based on the current Colors_Collection
            UpdateColorPanels();

            // Initialize the programmatic output TextBox for setting colors different values
            InitializeProgrammaticOutputTextBox();
            WindowState = FormWindowState.Maximized;

            // Attach event handlers for updating the color panels when the form is shown
            Shown += (s, e) => UpdateColorPanels();
        }

        private void InitializeProgrammaticOutputTextBox()
        {
            // Create a TextBox for displaying the programmatic output
            TextBox programmaticOutputTextBox = new()
            {
                // Set TextBox properties
                Multiline = true,
                BackColor = Color.FromArgb(30, 30, 30),
                Font = new Font("Cascadia Mono", 10),
                Dock = DockStyle.Right,
                ForeColor = Color.White,
                ScrollBars = ScrollBars.Vertical,
                Width = 700
            };

            // Add TextBox to the form
            Controls.Add(programmaticOutputTextBox);

            // Attach event handlers for control changes
            foreach (ComboBox comboBox in Controls.OfType<ComboBox>())
            {
                comboBox.SelectedIndexChanged += (sender, e) => UpdateProgrammaticOutput(programmaticOutputTextBox);
            }

            // Attach event handlers for control changes
            foreach (TrackBar trackBar in Controls.OfType<TrackBar>())
            {
                trackBar.ValueChanged += (sender, e) => UpdateProgrammaticOutput(programmaticOutputTextBox);
            }

            // Attach event handlers for control changes
            accentPanel.BackColorChanged += (sender, e) => UpdateProgrammaticOutput(programmaticOutputTextBox);
            backPanel.BackColorChanged += (sender, e) => UpdateProgrammaticOutput(programmaticOutputTextBox);

            // Call the initial update
            UpdateProgrammaticOutput(programmaticOutputTextBox);
        }

        /// <summary>
        /// Creates controls instances for test with different states
        /// </summary>
        private void CreateTestControls()
        {
            int offsetY = 200;

            // Create TestControl instances for different states
            noneControl = CreateTestControl(WP.TestControl.States.None, offsetY);
            hoverControl = CreateTestControl(WP.TestControl.States.Hover, offsetY + 100);
            checkedControl = CreateTestControl(WP.TestControl.States.Checked, offsetY + 200);
            checkedHoverControl = CreateTestControl(WP.TestControl.States.CheckedHover, offsetY + 250);

            buttonNoneControl = CreateTestControl(WP.TestControl.States.ButtonNone, offsetY + 400);
            buttonOverControl = CreateTestControl(WP.TestControl.States.ButtonOver, offsetY + 450);
            buttonDownControl = CreateTestControl(WP.TestControl.States.ButtonDown, offsetY + 500);

            // Add TestControls to the form
            Controls.Add(noneControl);
            Controls.Add(hoverControl);
            Controls.Add(checkedHoverControl);
            Controls.Add(checkedControl);
            Controls.Add(buttonNoneControl);
            Controls.Add(buttonOverControl);
            Controls.Add(buttonDownControl);
        }

        /// <summary>
        /// Creates a control instance for test with the specified state
        /// </summary>
        /// <param name="state"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        private WP.TestControl CreateTestControl(WP.TestControl.States state, int offsetY)
        {
            Label label = new()
            {
                Text = state.ToString(),
                AutoSize = true,
                Location = new Point(700, offsetY - 20)
            };

            WP.TestControl testControl = new()
            {
                Location = new Point(700, offsetY),
                Size = new Size(150, 30),
                State = state
            };

            // Add label and TestControl to the form
            Controls.Add(label);
            Controls.Add(testControl);

            return testControl;
        }

        /// <summary>
        /// Creates controls for editing the color scheme
        /// </summary>
        private void CreateColorControls()
        {
            // Create controls dynamically based on the properties of Colors_Collection
            int offsetY = 5;

            foreach (PropertyInfo property in typeof(Colors_Collection).GetProperties())
            {
                if (property.PropertyType == typeof(Color))
                {
                    CreateColorControl(property.Name, offsetY);
                    offsetY += 40; // Adjust the offset for the next control
                }
            }

            // Create two panels for accent and back colors
            accentPanel = CreateColorPanel("Accent Color", DefaultColors.PrimaryColor_Dark, new Point(10, offsetY + 50));
            backPanel = CreateColorPanel("Back Color", Program.Style.DarkMode ? DefaultColors.BackColor_Dark : DefaultColors.BackColor_Light, new Point(10, offsetY + 100));

            // Hook up event handlers
            backPanel.Click += BackPanel_Click;
            accentPanel.Click += AccentPanel_Click;

            // Set border style
            accentPanel.BorderStyle = BorderStyle.FixedSingle;
            backPanel.BorderStyle = BorderStyle.FixedSingle;

            // Add panels to the form
            Controls.Add(accentPanel);
            Controls.Add(backPanel);
        }

        /// <summary>
        /// Creates a panel for selecting a color
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="color"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        private Panel CreateColorPanel(string propertyName, Color color, Point location)
        {
            // Create a Panel for selecting a color
            Panel panel = new()
            {
                Name = $"panel_{propertyName.Replace(" ", string.Empty)}",
                BackColor = color,
                Size = new Size(50, 20),
                Location = location,
                Margin = new Padding(5)
            };

            // Create a Label for the color property
            Label label = new()
            {
                Text = propertyName,
                AutoSize = true,
                Location = new Point(panel.Right + 5, panel.Top)
            };

            // Add label to the form
            Controls.Add(label);

            return panel;
        }

        /// <summary>
        /// Creates a control for editing a color property
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="offsetY"></param>
        private void CreateColorControl(string propertyName, int offsetY)
        {
            // Create a Label for the color property
            Label label = new()
            {
                Name = $"label_{propertyName}",
                Text = propertyName,
                AutoSize = true,
                Location = new Point(10, offsetY)
            };

            // Create a ComboBox for selecting the method
            ComboBox methodComboBox = new()
            {
                Name = $"comboBox_{propertyName}_Method",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(150, offsetY),
                Size = new Size(70, 21),
            };

            // Add the three methods to the ComboBox
            methodComboBox.Items.AddRange(Enum.GetNames(typeof(Method)));
            methodComboBox.SelectedIndex = (int)GetInitialMethodFromStyleSchemes(propertyName); // Set the default method

            // Create a TrackBar for adjusting the value
            TrackBar trackBar = new()
            {
                Name = $"trackbar_{propertyName}",
                Minimum = -200,
                Maximum = 200,
                SmallChange = 1,
                LargeChange = 10,
                Value = (int)(GetInitialValueFromStyleSchemes(propertyName) * 100),
                TickFrequency = 20,
                TickStyle = TickStyle.BottomRight,
                Location = new Point(250, offsetY),
            };

            // Create a TextBox for displaying and inputting the value
            TextBox textBox = new()
            {
                Name = $"textBox_{propertyName}",
                Text = ((int)(GetInitialValueFromStyleSchemes(propertyName) * 100)).ToString(),
                Location = new Point(400, offsetY),
                Size = new Size(50, 20),
                TextAlign = HorizontalAlignment.Center
            };

            // Create a Panel for displaying the color
            Panel colorPanel = new()
            {
                Name = $"panel_{propertyName}",
                BackColor = colorCollection.Accent, // Initial color
                Size = new Size(50, 20),
                Location = new Point(550, offsetY),
                Margin = new Padding(5)
            };

            // Hook up event handlers
            methodComboBox.SelectedIndexChanged += (sender, e) => { UpdateColorScheme(); };
            trackBar.Scroll += (sender, e) => { UpdateColorScheme(); UpdateTextBoxValue(trackBar, textBox); };
            textBox.TextChanged += (sender, e) => { UpdateColorScheme(); UpdateTrackBarValue(textBox, trackBar); };

            // Add controls to the form
            Controls.Add(label);
            Controls.Add(methodComboBox);
            Controls.Add(trackBar);
            Controls.Add(textBox);
            Controls.Add(colorPanel);
        }

        private void UpdateTextBoxValue(TrackBar trackBar, TextBox textBox)
        {
            // Update TextBox value based on TrackBar value
            textBox.Text = ((float)trackBar.Value).ToString();
        }

        private void UpdateTrackBarValue(TextBox textBox, TrackBar trackBar)
        {
            // Update TrackBar value based on TextBox value
            if (int.TryParse(textBox.Text, out int value))
            {
                trackBar.Value = Math.Min(Math.Max(value, trackBar.Minimum), trackBar.Maximum);
            }
        }

        private void BackPanel_Click(object sender, EventArgs e)
        {
            // Handle back panel click to open color dialog
            colorDialog.Color = backPanel.BackColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                backPanel.BackColor = colorDialog.Color;
                UpdateColorScheme();
            }
        }
        private void AccentPanel_Click(object sender, EventArgs e)
        {
            // Handle accent panel click to open color dialog
            colorDialog.Color = accentPanel.BackColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                accentPanel.BackColor = colorDialog.Color;
                UpdateColorScheme();
            }
        }

        private void UpdateProgrammaticOutput(TextBox programmaticOutputTextBox)
        {
            // Generate the programmatic output based on the control values
            string output = GenerateProgrammaticOutput();

            // Update the TextBox with the generated output
            programmaticOutputTextBox.Text = output;
        }

        private string GenerateProgrammaticOutput()
        {
            // Use StringBuilder for efficient string concatenation
            StringBuilder output = new();

            output.AppendLine($"AccentColor = Color.FromArgb({accentPanel.BackColor.R}, {accentPanel.BackColor.G}, {accentPanel.BackColor.B});");
            output.AppendLine($"BackColor = Color.FromArgb({backPanel.BackColor.R}, {backPanel.BackColor.G}, {backPanel.BackColor.B});");
            output.AppendLine();

            foreach (TrackBar trackBar in Controls.OfType<TrackBar>())
            {
                string propertyName = trackBar.Name.Replace("trackbar_", string.Empty).Trim();
                ComboBox methodComboBox = Controls.OfType<ComboBox>().FirstOrDefault(c => c.Name.ToLower() == $"comboBox_{propertyName}_Method".ToLower());

                output.AppendLine($"{propertyName}_Method = Method.{methodComboBox.SelectedItem};");
                output.AppendLine($"{propertyName}_Factor = {trackBar.Value * 0.01f}f;");
            }

            return output.ToString();
        }

        /// <summary>
        /// Updates the color panels based on the current Colors_Collection
        /// </summary>
        private void UpdateColorPanels()
        {
            // Update color panels based on the current Colors_Collection
            Color accentColor = accentPanel.BackColor;
            Color backColor = backPanel.BackColor;

            // Update color panels based on the current Colors_Collection
            foreach (TrackBar trackBar in Controls.OfType<TrackBar>())
            {
                string propertyName = trackBar.Name.Replace("trackbar_", string.Empty);
                ComboBox methodComboBox = Controls.OfType<ComboBox>().FirstOrDefault(c => c.Name.ToLower() == $"comboBox_{propertyName}_Method".ToLower());

                if (methodComboBox != null)
                {
                    PropertyInfo property = typeof(Colors_Collection).GetProperty(propertyName);

                    bool isBackProperty = propertyName.ToLower().Contains("back") || propertyName.ToLower().Contains("line") || propertyName.ToLower().Contains("button");
                    bool isCheckProperty = propertyName.ToLower().Contains("check") || propertyName.ToLower().Contains("accent");

                    // Determine the color based on the property name (normal control vs a check control)
                    Color color = isCheckProperty ? accentColor : backColor;

                    // Get the method for manipulating colors from the ComboBox
                    Method selectedMethod = (Method)Enum.Parse(typeof(Method), methodComboBox.SelectedItem.ToString());

                    if (selectedMethod == Method.CB)
                    {
                        // Change color brightness based on the trackbar value
                        color = color.CB(trackBar.Value / 100.0f);
                    }
                    else if (selectedMethod == Method.Light)
                    {
                        // Lighten the color based on the trackbar value
                        color = color.Light(trackBar.Value / 100.0f);
                    }
                    else if (selectedMethod == Method.Dark)
                    {
                        // Darken the color based on the trackbar value
                        color = color.Dark(trackBar.Value / 100.0f);
                    }
                    else
                    {
                        // Default: Change color brightness based on the trackbar value
                        color = color.CB(trackBar.Value / 100.0f);
                    }

                    // Update color panels
                    Panel panel = Controls.OfType<Panel>().FirstOrDefault(p => p.Name == $"panel_{propertyName}");

                    if (panel != null)
                    {
                        panel.BackColor = color;
                    }
                }
            }

            // Update TestControls based on the current Colors_Collection
            Scheme scheme = new()
            {
                Colors = colorCollection
            };

            noneControl.Scheme = scheme;
            hoverControl.Scheme = scheme;
            checkedHoverControl.Scheme = scheme;
            checkedControl.Scheme = scheme;
            buttonNoneControl.Scheme = scheme;
            buttonOverControl.Scheme = scheme;
            buttonDownControl.Scheme = scheme;

            // Update form's back color
            BackColor = backColor;
            ForeColor = backColor.IsDark() ? Color.White : Color.Black;
        }

        /// <summary>
        /// Get the initial value for a property from the Style.Schemes.Main instance
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private float GetInitialValueFromStyleSchemes(string propertyName)
        {
            // Assume Program.Style.Schemes.Main is an instance of Colors_Collection or similar
            FieldInfo field = typeof(Colors_Collection).GetField($"{propertyName}_Factor", BindingFlags.Instance | BindingFlags.NonPublic);

            if (field != null)
            {
                // Get the value of the corresponding private field
                object value = field.GetValue(colorCollection);

                // Convert the value to a float
                if (value is float floatValue)
                {
                    return floatValue;
                }
            }

            // Return a default value if something goes wrong
            return 0.0f;
        }

        /// <summary>
        /// Get the initial method for a property from the Style.Schemes.Main instance
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private Method GetInitialMethodFromStyleSchemes(string propertyName)
        {
            // Assume Program.Style.Schemes.Main is an instance of Colors_Collection or similar
            FieldInfo field = typeof(Colors_Collection).GetField($"{propertyName}_Method", BindingFlags.Instance | BindingFlags.NonPublic);

            if (field != null)
            {
                // Get the value of the corresponding private field
                object value = field.GetValue(colorCollection);

                // Convert the value to a float
                if (value is Method method)
                {
                    return method;
                }
            }

            // Return a default value if something goes wrong
            return 0.0f;
        }

        /// <summary>
        /// Updates the color scheme based on the current control values
        /// </summary>
        private void UpdateColorScheme()
        {
            // Update Colors_Collection with the specified colors
            Color accentColor = accentPanel.BackColor;
            Color backColor = backPanel.BackColor;

            foreach (ComboBox methodComboBox in Controls.OfType<ComboBox>())
            {
                TrackBar trackBar = Controls.OfType<TrackBar>().Where(t => t.Name == $"trackbar_{methodComboBox.Name.Replace("comboBox_", string.Empty).Replace("_Method", string.Empty).Trim()}").FirstOrDefault();
                string propertyName = methodComboBox.Name.Replace("comboBox_", string.Empty).Replace("_Method", string.Empty).Trim();
                PropertyInfo property = typeof(Colors_Collection).GetProperty(propertyName);

                // Determine the color based on the property name (normal control vs a check control)
                Color color = propertyName.ToLower().Contains("check") || propertyName.ToLower().Contains("accent") ? accentColor : backColor;

                // Get the method for manipulating colors from the ComboBox
                Method selectedMethod = (Method)Enum.Parse(typeof(Method), methodComboBox.SelectedItem.ToString());

                if (selectedMethod == Method.CB)
                {
                    // Change color brightness based on the trackbar value
                    property.SetValue(colorCollection, color.CB(trackBar.Value / 100.0f), null);
                }
                else if (selectedMethod == Method.Light)
                {
                    // Lighten the color based on the trackbar value
                    property.SetValue(colorCollection, color.Light(trackBar.Value / 100.0f), null);
                }
                else if (selectedMethod == Method.Dark)
                {
                    // Darken the color based on the trackbar value
                    property.SetValue(colorCollection, color.Dark(trackBar.Value / 100.0f), null);
                }
                else
                {
                    // Default: Change color brightness based on the trackbar value
                    property.SetValue(colorCollection, color.CB(trackBar.Value / 100.0f), null);
                }
            }

            // Update color panels based on the new Colors_Collection
            UpdateColorPanels();
        }
    }
}