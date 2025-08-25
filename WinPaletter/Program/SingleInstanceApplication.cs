using Microsoft.VisualBasic.ApplicationServices;
using Serilog.Events;
using System;
using System.Windows.Forms;
using WinPaletter;

/// <summary>
/// A class that ensures that only one instance of the application is running at a time.
/// </summary>
public class SingleInstanceApplication : WindowsFormsApplicationBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SingleInstanceApplication"/> class.
    /// </summary>
    private SingleInstanceApplication()
    {
        base.IsSingleInstance = true;
    }

    /// <summary>
    /// Runs the single-instance application.
    /// </summary>
    /// <param name="form"></param>
    /// <param name="startupHandler"></param>
    public static void Run(Form form, StartupNextInstanceEventHandler startupHandler)
    {
        Program.Log?.Write(LogEventLevel.Information, "Starting WinPaletter in single-instance mode");

        SingleInstanceApplication app = new()
        {
            MainForm = form
        };
        app.StartupNextInstance += startupHandler;
        app.Run(Environment.GetCommandLineArgs());
    }
}