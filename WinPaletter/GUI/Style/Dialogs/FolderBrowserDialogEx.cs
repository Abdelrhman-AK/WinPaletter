using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    /// <summary>
    /// Prompts the user to select a folder using the native Windows Vista-style dialog when available.
    /// </summary>
    [DefaultEvent("HelpRequest")]
    [DefaultProperty("SelectedPath")]
    [Description("Prompts the user to select a folder.")]
    public sealed class FolderBrowserDialogEx : CommonDialog
    {
        private FolderBrowserDialog _downlevelDialog;
        private string _description = string.Empty;
        private bool _useDescriptionForTitle;
        private string _selectedPath = string.Empty;
        private Environment.SpecialFolder _rootFolder = Environment.SpecialFolder.Desktop;
        private bool _showNewFolderButton = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderBrowserDialogEx"/> class.
        /// </summary>
        public FolderBrowserDialogEx()
        {
            if (OS.WXP) _downlevelDialog = new FolderBrowserDialog();
            else Reset();
        }

        #region Public Properties

        /// <summary>
        /// Gets or sets the descriptive text displayed above the tree view control in the dialog box, 
        /// or below the list view control in the Vista style dialog.
        /// </summary>
        [Category("Folder Browsing")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The descriptive text displayed above the tree view control in the dialog box, or below the list view control in the Vista style dialog.")]
        public string Description
        {
            get { return _downlevelDialog?.Description ?? _description; }
            set
            {
                if (_downlevelDialog != null) _downlevelDialog.Description = value;
                else _description = value ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the root folder where the browsing starts from. This property has no effect if the Vista style dialog is used.
        /// </summary>
        [Localizable(false)]
        [Description("The root folder where the browsing starts from. This property has no effect if the Vista style dialog is used.")]
        [Category("Folder Browsing")]
        [DefaultValue(typeof(Environment.SpecialFolder), "Desktop")]
        public Environment.SpecialFolder RootFolder
        {
            get { return _downlevelDialog?.RootFolder ?? _rootFolder; }
            set
            {
                if (_downlevelDialog != null) _downlevelDialog.RootFolder = value;
                else _rootFolder = value;
            }
        }

        /// <summary>
        /// Gets or sets the path selected by the user.
        /// </summary>
        [Browsable(true)]
        [Description("The path selected by the user.")]
        [DefaultValue("")]
        [Localizable(true)]
        [Category("Folder Browsing")]
        public string SelectedPath
        {
            get { return _downlevelDialog?.SelectedPath ?? _selectedPath; }
            set
            {
                if (_downlevelDialog != null) _downlevelDialog.SelectedPath = value;
                else _selectedPath = value ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the New Folder button appears in the folder browser dialog box. 
        /// This property has no effect if the Vista style dialog is used; in that case, the New Folder button is always shown.
        /// </summary>
        [Browsable(true)]
        [Localizable(false)]
        [Description("A value indicating whether the New Folder button appears in the folder browser dialog box. This property has no effect if the Vista style dialog is used; in that case, the New Folder button is always shown.")]
        [DefaultValue(true)]
        [Category("Folder Browsing")]
        public bool ShowNewFolderButton
        {
            get { return _downlevelDialog?.ShowNewFolderButton ?? _showNewFolderButton; }
            set
            {
                if (_downlevelDialog != null) _downlevelDialog.ShowNewFolderButton = value;
                else _showNewFolderButton = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether to use the value of the <see cref="Description"/> property
        /// as the dialog title for Vista style dialogs. This property has no effect on old style dialogs.
        /// </summary>
        [Category("Folder Browsing")]
        [DefaultValue(false)]
        [Description("A value that indicates whether to use the value of the Description property as the dialog title for Vista style dialogs. This property has no effect on old style dialogs.")]
        public bool UseDescriptionForTitle
        {
            get { return _useDescriptionForTitle; }
            set { _useDescriptionForTitle = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Resets all properties to their default values.
        /// </summary>
        public override void Reset()
        {
            _description = string.Empty;
            _useDescriptionForTitle = false;
            _selectedPath = string.Empty;
            _rootFolder = Environment.SpecialFolder.Desktop;
            _showNewFolderButton = true;
        }

        /// <summary>
        /// Displays the folder browser dialog with the specified owner window handle.
        /// </summary>
        /// <param name="owner">The Win32 handle that is the owner of this dialog.</param>
        /// <returns><see langword="true"/> if the user clicks OK; otherwise, <see langword="false"/>.</returns>
        public bool? ShowDialog(IntPtr owner)
        {
            IntPtr ownerHandle = owner == IntPtr.Zero ? NativeMethods.User32.GetActiveWindow() : owner;
            return new bool?(RunDialog(ownerHandle));
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Runs the common dialog box.
        /// </summary>
        /// <param name="hwndOwner">The window handle of the owner window for the common dialog box.</param>
        /// <returns><see langword="true"/> if the user clicks OK; otherwise, <see langword="false"/>.</returns>
        protected override bool RunDialog(IntPtr hwndOwner)
        {
            if (_downlevelDialog != null)
            {
                return _downlevelDialog.ShowDialog(new WindowWrapper(hwndOwner)) == DialogResult.OK;
            }

            IFileDialog dialog = null;
            try
            {
                dialog = (IFileDialog)new FileOpenDialog();
                SetDialogProperties(dialog);

                // Show returns HRESULT
                int hr = dialog.Show(hwndOwner);
                if (hr < 0)
                {
                    if ((uint)hr == 0x800704C7) // ERROR_CANCELLED
                        return false;
                    else throw Marshal.GetExceptionForHR(hr);
                }

                GetResult(dialog, out _selectedPath);
                return true;
            }
            finally
            {
                if (dialog != null) Marshal.FinalReleaseComObject(dialog);
            }
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="FolderBrowserDialogEx"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && _downlevelDialog != null) _downlevelDialog.Dispose();
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion

        #region Private Methods

        private void SetDialogProperties(IFileDialog dialog)
        {
            // Set description or title
            if (!string.IsNullOrEmpty(_description))
            {
                if (_useDescriptionForTitle)
                {
                    dialog.SetTitle(_description);
                }
                else
                {
                    var customize = (IFileDialogCustomize)dialog;
                    customize.AddText(0, _description);
                }
            }

            // Set options for folder picking
            uint options = FOS.FOS_PICKFOLDERS | FOS.FOS_FORCEFILESYSTEM | FOS.FOS_FILEMUSTEXIST;
            dialog.SetOptions(options);

            // Set initial folder and selection
            if (!string.IsNullOrEmpty(_selectedPath))
            {
                string parent = Path.GetDirectoryName(_selectedPath);
                if (parent == null || !Directory.Exists(parent))
                {
                    dialog.SetFileName(_selectedPath);
                }
                else
                {
                    string folder = Path.GetFileName(_selectedPath);
                    NativeMethods.User32.IShellItem folderItem = NativeMethods.User32.CreateItemFromParsingName(parent);
                    dialog.SetFolder(folderItem);
                    dialog.SetFileName(folder);
                }
            }
        }

        private void GetResult(IFileDialog dialog, out string selectedPath)
        {
            NativeMethods.User32.IShellItem item;
            dialog.GetResult(out item);
            item.GetDisplayName(NativeMethods.User32.SIGDN.SIGDN_FILESYSPATH, out selectedPath);
        }

        #endregion

        #region Helper Classes and Interfaces

        /// <summary>
        /// Wraps an HWND for use with the classic FolderBrowserDialog.
        /// </summary>
        private class WindowWrapper : IWin32Window
        {
            private readonly IntPtr _handle;
            public WindowWrapper(IntPtr handle) { _handle = handle; }
            public IntPtr Handle => _handle;
        }

        #endregion
    }

    #region Native COM Interfaces and Structures

    [ComImport]
    [Guid("42f85136-db7e-439c-85f1-e4075d135fc8")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFileDialog
    {
        [PreserveSig]
        int Show([In] IntPtr parent);

        void SetFileTypes([In] uint cFileTypes, [In] IntPtr rgFilterSpec);
        void SetFileTypeIndex([In] uint iFileType);
        uint GetFileTypeIndex();

        [PreserveSig]
        uint Advise([In] IntPtr pfde, out uint pdwCookie);

        void Unadvise([In] uint dwCookie);
        void SetOptions([In] uint fos);
        uint GetOptions();
        void SetDefaultFolder([In] NativeMethods.User32.IShellItem psi);
        void SetFolder([In] NativeMethods.User32.IShellItem psi);
        NativeMethods.User32.IShellItem GetFolder();
        NativeMethods.User32.IShellItem GetCurrentSelection();
        void SetFileName([In, MarshalAs(UnmanagedType.LPWStr)] string pszName);

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetFileName();

        void SetTitle([In, MarshalAs(UnmanagedType.LPWStr)] string pszTitle);
        void SetOkButtonLabel([In, MarshalAs(UnmanagedType.LPWStr)] string pszText);
        void SetFileNameLabel([In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
        void GetResult(out NativeMethods.User32.IShellItem ppsi);
        void AddPlace([In] NativeMethods.User32.IShellItem psi, uint alignment);
        void SetDefaultExtension([In, MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);
        void Close([In, MarshalAs(UnmanagedType.Error)] int hr);
        void SetClientGuid([In] ref Guid guid);
        void ClearClientData();
        void SetFilter([In] IntPtr pFilter);
    }

    [ComImport]
    [Guid("ecc8691b-c1db-4dc0-855e-65f6c551af49")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFileDialogCustomize
    {
        void EnableOpenDropDown([In] uint dwIDCtl);
        void AddMenu([In] uint dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
        void AddPushButton([In] uint dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
        void AddComboBox([In] uint dwIDCtl);
        void AddRadioButtonList([In] uint dwIDCtl);
        void AddCheckButton([In] uint dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel, [In] bool bChecked);
        void AddEditBox([In] uint dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszText);
        void AddSeparator([In] uint dwIDCtl);
        void AddText([In] uint dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszText);
        void SetControlLabel([In] uint dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
        void GetControlState([In] uint dwIDCtl, out uint pdwState);
        void SetControlState([In] uint dwIDCtl, [In] uint dwState);
        void GetEditBoxText([In] uint dwIDCtl, [MarshalAs(UnmanagedType.LPWStr)] out string ppszText);
        void SetEditBoxText([In] uint dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszText);
        void GetCheckButtonState([In] uint dwIDCtl, out bool pbChecked);
        void SetCheckButtonState([In] uint dwIDCtl, [In] bool bChecked);
        void AddControlItem([In] uint dwIDCtl, [In] uint dwIDItem, [In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
        void RemoveControlItem([In] uint dwIDCtl, [In] uint dwIDItem);
        void RemoveAllControlItems([In] uint dwIDCtl);
        void GetControlItemState([In] uint dwIDCtl, [In] uint dwIDItem, out uint pdwState);
        void SetControlItemState([In] uint dwIDCtl, [In] uint dwIDItem, [In] uint dwState);
        void GetSelectedControlItem([In] uint dwIDCtl, out uint pdwIDItem);
        void SetSelectedControlItem([In] uint dwIDCtl, [In] uint dwIDItem);
        void StartVisualGroup([In] uint dwIDCtl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
        void EndVisualGroup();
        void MakeProminent([In] uint dwIDCtl);
        void SetControlItemText([In] uint dwIDCtl, [In] uint dwIDItem, [In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
    }

    internal static class FOS
    {
        public const uint FOS_OVERWRITEPROMPT = 0x00000002;
        public const uint FOS_STRICTFILETYPES = 0x00000004;
        public const uint FOS_NOCHANGEDIR = 0x00000008;
        public const uint FOS_PICKFOLDERS = 0x00000020;
        public const uint FOS_FORCEFILESYSTEM = 0x00000040;
        public const uint FOS_ALLNONSTORAGEITEMS = 0x00000080;
        public const uint FOS_NOVALIDATE = 0x00000100;
        public const uint FOS_ALLOWMULTISELECT = 0x00000200;
        public const uint FOS_PATHMUSTEXIST = 0x00000800;
        public const uint FOS_FILEMUSTEXIST = 0x00001000;
        public const uint FOS_CREATEPROMPT = 0x00002000;
        public const uint FOS_SHAREAWARE = 0x00004000;
        public const uint FOS_NOREADONLYRETURN = 0x00008000;
        public const uint FOS_NOTESTFILECREATE = 0x00010000;
        public const uint FOS_HIDEMRUPLACES = 0x00020000;
        public const uint FOS_HIDEPINNEDPLACES = 0x00040000;
        public const uint FOS_NODEREFERENCELINKS = 0x00100000;
        public const uint FOS_OKBUTTONNEEDSINTERACTION = 0x00200000;
        public const uint FOS_DONTADDTORECENT = 0x02000000;
        public const uint FOS_FORCESHOWHIDDEN = 0x10000000;
        public const uint FOS_DEFAULTNOMINIMODE = 0x20000000;
        public const uint FOS_FORCEPREVIEWPANEON = 0x40000000;
        public const uint FOS_SUPPORTSTREAMABLEITEMS = 0x80000000;
    }

    [ComImport]
    [Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")]
    [ClassInterface(ClassInterfaceType.None)]
    internal class FileOpenDialog { }

    #endregion
}