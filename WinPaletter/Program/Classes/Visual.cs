using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace WinPaletter
{
    public class Visual
    {
        // ' FILENAME:        Visual.vb
        // ' NAMESPACE:       PI.Common
        // ' CREATED BY:      Luke Berg
        // ' CREATED:         10-02-06
        // ' DESCRIPTION:     A common module of visual functions.

        private static readonly Dictionary<string, BackgroundWorker> colorsFading = new Dictionary<string, BackgroundWorker>(); // Keeps track of any backgroundworkers already fading colors
        private static readonly Dictionary<BackgroundWorker, ColorFaderInformation> backgroundWorkers = new Dictionary<BackgroundWorker, ColorFaderInformation>(); // Associate each background worker with information it needs
        private static bool PreviousColorItemPauseColorsHistory;

        // The delegate of a method that will be called when the color finishes fading
        public delegate void DoneFading(object container, string colorProperty);

        /// <summary>
        /// Fades a color property from one color to another
        /// </summary>
        /// <param name="container">The object that contains the color property</param>
        /// <param name="colorProperty">The name of the color property to change</param>
        /// <param name="startColor">The color to start the fade with</param>
        /// <param name="endColor">The color to end the fade with</param>
        /// <param name="steps">The number of steps to take to fade from the start color to the end color</param>
        /// <param name="delay">The delay in milliseconds between each step in the fade.</param>
        /// <param name="callback">A function to be called when the fade completes</param>
        /// <remarks></remarks>
        public static void FadeColor(object container, string colorProperty, Color startColor, Color endColor, int steps, int delay, DoneFading callback = null)
        {
            var colorSteps = new ColorStep[1];
            colorSteps[0] = new ColorStep(endColor, steps);
            try
            {
                FadeColor(container, colorProperty, startColor, colorSteps, delay, callback);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Fades a color property from one color to another, and then to yet another
        /// </summary>
        /// <param name="container">The object that contains the color property</param>
        /// <param name="colorProperty">The name of the color property to change</param>
        /// <param name="startColor">The color to start the fade with</param>
        /// <param name="middleColor">The color to fade to first</param>
        /// <param name="middleSteps">The number of steps to take in fading to the first color</param>
        /// <param name="endcolor">The last color to fade to</param>
        /// <param name="endSteps">The number of steps to take in fading to the last color</param>
        /// <param name="delay">The delay between each step in the fade</param>
        /// <param name="callback">A function that will be called after the fading has completed</param>
        /// <remarks></remarks>
        public static void FadeColor(object container, string colorProperty, Color startColor, Color middleColor, int middleSteps, Color endcolor, int endSteps, int delay, DoneFading callback = null)
        {
            var colorSteps = new ColorStep[2];
            colorSteps[0] = new ColorStep(middleColor, middleSteps);
            colorSteps[1] = new ColorStep(endcolor, endSteps);
            try
            {
                FadeColor(container, colorProperty, startColor, colorSteps, delay, callback);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Fades a color property to various colors
        /// </summary>
        /// <param name="container">The object that contains the color property</param>
        /// <param name="colorProperty">The name of the color property to change</param>
        /// <param name="startColor">The color to start the fade with</param>
        /// <param name="colorSteps">A list of steps in fading the color - an enumerable list of colors and the steps to get to that color</param>
        /// <param name="delay">The delay between each step in fading the color</param>
        /// <param name="callBack">A method to call when the fading has completed</param>
        /// <remarks></remarks>

        public static void FadeColor(object container, string colorProperty, Color startColor, IEnumerable<ColorStep> colorSteps, int delay, DoneFading callBack = null)
        {

            var colorFader = new BackgroundWorker();

            // Stores all the parameter information into a class that the background worker will access
            var colorFaderInfo = new ColorFaderInformation(container, colorProperty, startColor, colorSteps, delay, callBack);

            // Checks if the color is already in the process of fading.
            if (colorsFading.TryGetValue(GenerateHashCode(container, colorProperty), out colorFader))
            {
                // Cancels the backgroundWorkers process and sets a flag indicating that it should restart itself with
                // the new information.
                colorFader.CancelAsync();
                colorFaderInfo.Rerun = true;
                backgroundWorkers[colorFader] = colorFaderInfo;
            }
            else
            {

                // Creates a new backgroundWorker and adds handlers to all its events
                colorFader = new BackgroundWorker();
                colorFader.DoWork += BackgroundWorker_DoWork;
                colorFader.ProgressChanged += BackgroundWorker_ProgressChanged;
                colorFader.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
                colorFader.WorkerReportsProgress = true;
                colorFader.WorkerSupportsCancellation = true;
                if (container is UI.Controllers.ColorItem)
                {
                    PreviousColorItemPauseColorsHistory = ((UI.Controllers.ColorItem)container).PauseColorsHistory;
                    ((UI.Controllers.ColorItem)container).PauseColorsHistory = true;
                }

                backgroundWorkers.Add(colorFader, colorFaderInfo);
                colorsFading.Add(GenerateHashCode(container, colorProperty), colorFader);

            }

            // Starts the backgroundWorker beginning the fade
            if (!colorFader.IsBusy)
            {
                colorFader.RunWorkerAsync(colorFaderInfo);
            }
        }

        /// <summary>
        /// The work that the background worker does in fading the color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private static void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ColorFaderInformation info = (ColorFaderInformation)e.Argument;
            // These are calculated with each iteration (step) and used to set the color
            // when the background worker reports its progress.

            double curA;
            double curR;
            double curG;
            double curB;

            var startStepColor = info.StartColor;
            Color endStepColor;

            foreach (ColorStep colorStep in info.Colors)
            {

                endStepColor = colorStep.Color;

                // Gets the amount to change each color part per step

                double aStep = (endStepColor.A - (double)startStepColor.A) / colorStep.Steps;
                double rStep = (endStepColor.R - (double)startStepColor.R) / colorStep.Steps;
                double gStep = (endStepColor.G - (double)startStepColor.G) / colorStep.Steps;
                double bStep = (endStepColor.B - (double)startStepColor.B) / colorStep.Steps;

                // the red, green and blue parts of the current color
                curA = startStepColor.A;
                curR = startStepColor.R;
                curG = startStepColor.G;
                curB = startStepColor.B;

                // loop through, and fade
                for (int i = 1, loopTo = colorStep.Steps; i <= loopTo; i++)
                {
                    curA += aStep;
                    curR += rStep;
                    curB += bStep;
                    curG += gStep;

                    try
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, Color.FromArgb((int)Math.Round(curA), (int)Math.Round(curR), (int)Math.Round(curG), (int)Math.Round(curB)));
                    }
                    catch
                    {
                    }

                    System.Threading.Thread.Sleep(info.Delay);

                    if (((BackgroundWorker)sender).CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                }

                startStepColor = endStepColor;

            }

        }

        /// <summary>
        /// Calls to this method are marshalled back to the original thread, so here is where we actually change the color.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private static void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ColorFaderInformation info = null;
            if (backgroundWorkers.TryGetValue((BackgroundWorker)sender, out info))
            {
                Color currentColor = (Color)e.UserState;
                try
                {
                    Interaction.CallByName(info.Container, info.ColorProperty, CallType.Let, currentColor);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// This is raised when the background method completes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private static void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            ColorFaderInformation info = null;

            if (backgroundWorkers.TryGetValue((BackgroundWorker)sender, out info))
            {

                if (!e.Cancelled)
                {
                    info.CallBack?.Invoke(info.Container, info.ColorProperty);
                    backgroundWorkers.Remove((BackgroundWorker)sender);
                    colorsFading.Remove(GenerateHashCode(info.Container, info.ColorProperty));

                    if (info.Container is UI.Controllers.ColorItem)
                    {
                        {
                            var temp = (UI.Controllers.ColorItem)info.Container;
                            temp.PauseColorsHistory = PreviousColorItemPauseColorsHistory;
                            if (!PreviousColorItemPauseColorsHistory)
                                temp.ColorsHistory.Add(temp.BackColor);
                        }

                    } ((BackgroundWorker)sender).DoWork -= BackgroundWorker_DoWork;
                    ((BackgroundWorker)sender).ProgressChanged -= BackgroundWorker_ProgressChanged;
                    ((BackgroundWorker)sender).RunWorkerCompleted -= BackgroundWorker_RunWorkerCompleted;
                }

                else if (info.Rerun)
                {
                    info.Rerun = false;
                    ((BackgroundWorker)sender).RunWorkerAsync(info);

                }

            }

        }

        /// <summary>
        /// Generates a hashcode for an object and its color that are in the process of fading
        /// </summary>
        /// <param name="container">The object whose color property needs to be faded</param>
        /// <param name="colorProperty">The string name of the property to fade</param>
        /// <returns>A unique string representing the object and it's color property</returns>
        /// <remarks></remarks>
        private static string GenerateHashCode(object container, string colorProperty)
        {
            return container.GetHashCode() + colorProperty;
        }

        /// <summary>
        /// A simple class for storing information a backgroundWorker needs to perform the fading.
        /// </summary>
        /// <remarks></remarks>
        private class ColorFaderInformation
        {

            public DoneFading CallBack;
            public object Container;
            public string ColorProperty;
            public Color StartColor;
            public IEnumerable<ColorStep> Colors;
            public int Delay;
            public bool Rerun;

            public ColorFaderInformation(object container, string colorProperty, Color startColor, IEnumerable<ColorStep> colorSteps, int delay, DoneFading callBack = null)
            {
                Container = container;
                ColorProperty = colorProperty;
                StartColor = startColor;
                Colors = colorSteps;
                Delay = delay;
                CallBack = callBack;
                Rerun = false;
            }

        }

        /// <summary>
        /// A simple class needed to represent a single step in the fading process
        /// </summary>
        /// <remarks></remarks>
        public struct ColorStep
        {

            public Color Color;
            public int Steps;

            public ColorStep(Color color, int steps)
            {
                Color = color;
                Steps = steps;
            }

        }

    }

}
