using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Windows.Forms;

public class SingleInstanceApplication : WindowsFormsApplicationBase
{
    private SingleInstanceApplication()
    {
        base.IsSingleInstance = true;
    }

    public static void Run(Form form, StartupNextInstanceEventHandler startupHandler)
    {
        SingleInstanceApplication app = new();
        app.MainForm = form;
        app.StartupNextInstance += startupHandler;
        app.Run(Environment.GetCommandLineArgs());
    }
}