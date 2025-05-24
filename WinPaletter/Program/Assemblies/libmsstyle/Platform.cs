namespace libmsstyle
{
    public enum Platform
    {
        Vista,
        Win7,
        Win8,
        Win81,
        Win10,
        Win11
    }

    public static class PlatformExtensions
    {
        public static string ToDisplayString(this Platform p)
        {
            return p switch
            {
                Platform.Vista => "Windows Vista",
                Platform.Win7 => "Windows 7",
                Platform.Win8 => "Windows 8",
                Platform.Win81 => "Windows 8.1",
                Platform.Win10 => "Windows 10",
                Platform.Win11 => "Windows 11",
                _ => "Unknown",
            };
        }
    }
}