using System;
using System.Runtime.InteropServices;

namespace WinPaletter.Interfaces
{
    /// <summary>
    /// Represents the CTaskbarList class for managing taskbar-related operations.
    /// </summary>
    [ComImport]
    [ClassInterface(ClassInterfaceType.None)]
    [TypeLibType(TypeLibTypeFlags.FCanCreate)]
    [Guid("56FDF344-FD6D-11d0-958A-006097C9A090")]
    public class CTaskbarList
    {
    }
}
