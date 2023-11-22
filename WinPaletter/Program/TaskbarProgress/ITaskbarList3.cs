using System;
using System.Runtime.InteropServices;

namespace WinPaletter.Interfaces
{
    /// <summary>
    /// Represents the ITaskbarList3 interface for managing taskbar-related operations.
    /// </summary>
    [ComImport]
    [Guid("EA1AFB91-9E28-4B86-90E9-9E9F8A5EEFAF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITaskbarList3
    {
        // ITaskbarList

        /// <summary>
        /// Initializes the taskbar list object.
        /// </summary>
        [PreserveSig]
        void HrInit();

        /// <summary>
        /// Adds a new tab to the taskbar.
        /// </summary>
        /// <param name="hwnd">The handle to the window whose tab is to be added.</param>
        [PreserveSig]
        void AddTab(IntPtr hwnd);

        /// <summary>
        /// Deletes a tab from the taskbar.
        /// </summary>
        /// <param name="hwnd">The handle to the window whose tab is to be deleted.</param>
        [PreserveSig]
        void DeleteTab(IntPtr hwnd);

        /// <summary>
        /// Activates the tab associated with the specified window handle.
        /// </summary>
        /// <param name="hwnd">The handle to the window whose tab is to be activated.</param>
        [PreserveSig]
        void ActivateTab(IntPtr hwnd);

        /// <summary>
        /// Sets the specified tab as the active tab.
        /// </summary>
        /// <param name="hwnd">The handle to the window whose tab is to be set as active.</param>
        [PreserveSig]
        void SetActiveAlt(IntPtr hwnd);

        // ITaskbarList2

        /// <summary>
        /// Marks or unmarks a window as a full-screen window.
        /// </summary>
        /// <param name="hwnd">The handle to the window to be marked or unmarked.</param>
        /// <param name="fFullscreen">True to mark the window as full-screen, false to unmark it.</param>
        [PreserveSig]
        void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

        // ITaskbarList3

        /// <summary>
        /// Sets the progress value of the taskbar button.
        /// </summary>
        /// <param name="hwnd">The handle to the window whose progress is to be set.</param>
        /// <param name="ullCompleted">The completed progress value.</param>
        /// <param name="ullTotal">The total progress value.</param>
        void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);

        /// <summary>
        /// Sets the progress state of the taskbar button.
        /// </summary>
        /// <param name="hwnd">The handle to the window whose progress state is to be set.</param>
        /// <param name="tbpFlags">The progress state flags.</param>
        void SetProgressState(IntPtr hwnd, WinPaletter.UI.WP.ProgressBar.TaskbarProgressBarState tbpFlags);
    }


}
