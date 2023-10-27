using System;

/// <summary>
/// Class contains booleans, represent if the current OS is the same as variable name or not.
/// </summary>
public static class OS
{
    /// <summary>
    /// A boolean that represents if OS is Windows XP
    /// </summary>
    public readonly static bool WXP = Environment.OSVersion.Version.Major == 5;

    /// <summary>
    /// A boolean that represents if OS is Windows Vista
    /// </summary>
    public readonly static bool WVista = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 0;

    /// <summary>
    /// A boolean that represents if OS is Windows 7 or not
    /// </summary>
    public readonly static bool W7 = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1;

    /// <summary>
    /// A boolean that represents if OS is Windows 8 or not
    /// </summary>
    public readonly static bool W8 = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 2;

    /// <summary>
    /// A boolean that represents if OS is Windows 8.1 or not
    /// </summary>
    public readonly static bool W81 = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 3;

    /// <summary>
    /// A boolean that represents if OS is Windows 10 or not
    /// </summary>
    public readonly static bool W10 = Environment.OSVersion.Version.Major == 10 && Environment.OSVersion.Version.Minor == 0 && Environment.OSVersion.Version.Build < 22000;

    /// <summary>
    /// A boolean that represents if OS is Windows 11 or not
    /// </summary>
    public readonly static bool W11 = Environment.OSVersion.Version.Major == 10 && Environment.OSVersion.Version.Minor == 0 && Environment.OSVersion.Version.Build >= 22000;

    /// <summary>
    /// A boolean that represents if OS is Windows 12 or not (For near future! :))
    /// </summary>
    public readonly static bool W12 = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Contains("12") || Environment.OSVersion.Version.Major >= 10;
}