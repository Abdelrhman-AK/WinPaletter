using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace WinPaletter.UI.WP
{
    /// <summary>
    /// Represents a dialog that can be used to report progress to the user.
    /// </summary>
    [DefaultEvent("DoWork")]
    [DefaultProperty("Text")]
    [Description("Represents a dialog that can be used to report progress to the user.")]
    public partial class ProgressDialog : Component, IProgress<int>, IProgress<string>
    {
        #region Nested Types

        private class ProgressChangedData
        {
            public string Text { get; set; }
            public string Description { get; set; }
            public object UserState { get; set; }
        }

        #endregion

        #region Private Fields

        private string _windowTitle = string.Empty;
        private string _text = string.Empty;
        private string _description = string.Empty;
        private Interop.IProgressDialog _dialog;
        private string _cancellationText = string.Empty;
        private bool _useCompactPathsForText;
        private bool _useCompactPathsForDescription;
        private SafeModuleHandle _currentAnimationModuleHandle;
        private bool _cancellationPending;
        private int _percentProgress;
        private IntPtr _ownerHandle;
        private IntPtr _dialogHandle;
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();
        private IntPtr _originalWndProc = IntPtr.Zero;
        private NativeMethods.User32.WndProcDelegate _hookedWndProcDelegate;
        private IntPtr _darkBackgroundBrush = IntPtr.Zero;

        #endregion

        #region Events

        public event DoWorkEventHandler DoWork;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        public event ProgressChangedEventHandler ProgressChanged;
        public event EventHandler<IntPtr> DialogHandleAvailable;

        #endregion

        #region Constructor

        public ProgressDialog() : this(null) { }

        public ProgressDialog(IContainer container)
        {
            if (container != null)
                container.Add(this);

            InitializeComponent();

            ProgressBarStyle = ProgressBarStyle.ProgressBar;
            ShowCancelButton = true;
            MinimizeBox = true;

            // Windows XP has no native marquee support on this dialog and looks bare without
            // an animation, so default to the shell's flying-papers animation there, exactly
            // like the underlying shell IProgressDialog behaves when hosted from old apps.
            if (OS.WXP)
            {
                Animation = AnimationResource.GetShellAnimation(ShellAnimation.FlyingPapers);
            }

            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
            _backgroundWorker.ProgressChanged += _backgroundWorker_ProgressChanged;
        }

        #endregion

        #region Public Properties

        [Browsable(false)]
        public IntPtr DialogHandle
        {
            get
            {
                if (_dialogHandle == IntPtr.Zero && _dialog != null)
                {
                    _dialogHandle = GetDialogHandle();
                }
                return _dialogHandle;
            }
        }

        [Localizable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        public string WindowTitle
        {
            get { return _windowTitle ?? string.Empty; }
            set { _windowTitle = value; }
        }

        [Localizable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        public string Text
        {
            get { return _text ?? string.Empty; }
            set
            {
                _text = value;
                if (_dialog != null) _dialog.SetLine(1, _text, UseCompactPathsForText, IntPtr.Zero);
            }
        }

        [Localizable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        public string Description
        {
            get { return _description ?? string.Empty; }
            set
            {
                _description = value;
                if (_dialog != null) _dialog.SetLine(2, _description, UseCompactPathsForDescription, IntPtr.Zero);
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool UseCompactPathsForText
        {
            get { return _useCompactPathsForText; }
            set
            {
                _useCompactPathsForText = value;
                if (_dialog != null) _dialog.SetLine(1, Text, _useCompactPathsForText, IntPtr.Zero);
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool UseCompactPathsForDescription
        {
            get { return _useCompactPathsForDescription; }
            set
            {
                _useCompactPathsForDescription = value;
                if (_dialog != null) _dialog.SetLine(2, Description, _useCompactPathsForDescription, IntPtr.Zero);
            }
        }

        [Localizable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        public string CancellationText
        {
            get { return _cancellationText ?? string.Empty; }
            set { _cancellationText = value; }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool ShowTimeRemaining { get; set; }

        [Category("Appearance")]
        [DefaultValue(true)]
        public bool ShowCancelButton { get; set; }

        [Category("Window Style")]
        [DefaultValue(true)]
        public bool MinimizeBox { get; set; }

        [Browsable(false)]
        public bool CancellationPending
        {
            get
            {
                if (_dialog != null) _cancellationPending = _dialog.HasUserCancelled();
                return _cancellationPending;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AnimationResource Animation { get; set; }

        [Category("Appearance")]
        [DefaultValue(ProgressBarStyle.ProgressBar)]
        public ProgressBarStyle ProgressBarStyle { get; set; }

        [Browsable(false)]
        public bool IsBusy => _backgroundWorker.IsBusy;

        #endregion

        #region Public Methods

        public void Show() => Show(null);

        public void Show(object argument) => RunProgressDialog(IntPtr.Zero, argument);

        public void ShowDialog() => ShowDialog(null, null);

        public void ShowDialog(IWin32Window owner) => ShowDialog(owner, null);

        public void ShowDialog(IntPtr owner) => ShowDialog(owner, null);

        public void ShowDialog(IWin32Window owner, object argument) => RunProgressDialog(owner == null ? NativeMethods.User32.GetActiveWindow() : owner.Handle, argument);

        public void ShowDialog(IntPtr owner, object argument) => RunProgressDialog(owner == IntPtr.Zero ? NativeMethods.User32.GetActiveWindow() : owner, argument);

        void IProgress<int>.Report(int value) => ReportProgress(value, null, null, null);

        void IProgress<string>.Report(string value) => ReportProgress(_percentProgress, value, null, null);

        public void ReportProgress(int percentProgress) => ReportProgress(percentProgress, null, null, null);

        public void ReportProgress(int percentProgress, string text, string description) =>
            ReportProgress(percentProgress, text, description, null);

        public void ReportProgress(int percentProgress, string text, string description, object userState)
        {
            if (percentProgress < 0 || percentProgress > 100) throw new ArgumentOutOfRangeException(nameof(percentProgress));
            if (_dialog == null) return;

            _percentProgress = percentProgress;
            _backgroundWorker.ReportProgress(percentProgress, new ProgressChangedData
            {
                Text = text,
                Description = description,
                UserState = userState
            });
        }

        public void CancelAsync() => _backgroundWorker.CancelAsync();

        #endregion

        #region Protected Methods

        protected virtual void OnDoWork(DoWorkEventArgs e) => DoWork?.Invoke(this, e);

        protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e) => RunWorkerCompleted?.Invoke(this, e);

        protected virtual void OnProgressChanged(ProgressChangedEventArgs e) => ProgressChanged?.Invoke(this, e);

        #endregion

        #region Private Methods

        private void SubclassDialog(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero || !Program.Style.DarkMode) return;

            // Create a dark brush matching your app's theme background color
            _darkBackgroundBrush = NativeMethods.GDI32.CreateSolidBrush(0x202020);

            // Keep the delegate alive in a field so garbage collection doesn't kill it
            _hookedWndProcDelegate = new NativeMethods.User32.WndProcDelegate(ProgressDialogWndProc);

            // Subclass the main dialog window
            long lpfnWndProc = Marshal.GetFunctionPointerForDelegate(_hookedWndProcDelegate).ToInt64();
            _originalWndProc = (IntPtr)NativeMethods.User32.SetWindowLongPtr(hwnd, -4, (IntPtr)lpfnWndProc);

            // CRITICAL: Strip the modern UX themes from the static text child controls.
            // This forces Windows to fall back to classic drawing, which honors WM_CTLCOLORSTATIC.
            NativeMethods.User32.EnumChildWindows(hwnd, (childHwnd, lParam) =>
            {
                System.Text.StringBuilder className = new System.Text.StringBuilder(256);
                NativeMethods.User32.GetClassName(childHwnd, className, className.Capacity);

                if (className.ToString().Equals("Static", StringComparison.OrdinalIgnoreCase))
                {
                    // Passing empty strings strips the UxTheme engine controls off this element
                    NativeMethods.UxTheme.SetWindowTheme(childHwnd, "", "");
                }
                return true;
            }, IntPtr.Zero);

            // Force the window and its children to redraw immediately with the new configurations
            NativeMethods.User32.RedrawWindow(hwnd, IntPtr.Zero, IntPtr.Zero, 0x0101 /* RDW_INVALIDATE | RDW_ERASE */);
        }

        private IntPtr ProgressDialogWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            // WM_CTLCOLORSTATIC (0x0138) handles the text and background color for static elements
            if (msg == NativeMethods.User32.WM_CTLCOLORSTATIC)
            {
                // Text color: White (0x00FFFFFF)
                NativeMethods.GDI32.SetTextColor(wParam, 0xFFFFFF);
                // Remove the default background bounding box behind the font pixels
                NativeMethods.GDI32.SetBkMode(wParam, NativeMethods.GDI32.TRANSPARENT);

                // Return the brush to paint the control's bounding box background dark
                return _darkBackgroundBrush;
            }

            if (msg == NativeMethods.User32.WM_DESTROY)
            {
                if (_darkBackgroundBrush != IntPtr.Zero)
                {
                    NativeMethods.GDI32.DeleteObject(_darkBackgroundBrush);
                    _darkBackgroundBrush = IntPtr.Zero;
                }
            }

            return NativeMethods.User32.CallWindowProc(_originalWndProc, hWnd, msg, wParam, lParam);
        }

        private void RunProgressDialog(IntPtr owner, object argument)
        {
            if (_backgroundWorker.IsBusy)
                throw new InvalidOperationException("The progress dialog is already running.");

            if (Animation != null)
            {
                try
                {
                    _currentAnimationModuleHandle = Animation.LoadLibrary();
                }
                catch
                {
                    _currentAnimationModuleHandle = null;
                }
            }

            _cancellationPending = false;
            _ownerHandle = owner;
            _dialogHandle = IntPtr.Zero;

            try
            {
                // "new Interop.IProgressDialog()" is not a typo - IProgressDialog carries a
                // [CoClass] attribute, and the C# compiler specifically recognizes "new" on a
                // CoClass-attributed interface, translating it into CoCreateInstance(CLSID) then
                // QueryInterface(IID). This only works now because IProgressDialog's [Guid] was
                // corrected to the real IID_IProgressDialog - see the interface's doc comment.
                _dialog = new Interop.IProgressDialog();

                _dialog.SetTitle(WindowTitle);

                if (Animation != null && _currentAnimationModuleHandle != null && !_currentAnimationModuleHandle.IsInvalid)
                {
                    _dialog.SetAnimation(_currentAnimationModuleHandle.DangerousGetHandle(), (ushort)Animation.ResourceId);
                }

                if (!string.IsNullOrEmpty(CancellationText)) _dialog.SetCancelMsg(CancellationText, null);

                _dialog.SetLine(1, Text, UseCompactPathsForText, IntPtr.Zero);
                _dialog.SetLine(2, Description, UseCompactPathsForDescription, IntPtr.Zero);

                Interop.ProgressDialogFlags flags = Interop.ProgressDialogFlags.Normal;
                if (owner != IntPtr.Zero) flags |= Interop.ProgressDialogFlags.Modal;

                switch (ProgressBarStyle)
                {
                    case ProgressBarStyle.None:
                        flags |= Interop.ProgressDialogFlags.NoProgressBar;
                        break;
                    case ProgressBarStyle.MarqueeProgressBar:
                        // Marquee style requires Vista+; older shells simply don't support it,
                        // so fall back to hiding the progress bar entirely on XP.
                        if (!OS.WXP) flags |= Interop.ProgressDialogFlags.MarqueeProgress;
                        else flags |= Interop.ProgressDialogFlags.NoProgressBar;
                        break;
                }

                if (ShowTimeRemaining) flags |= Interop.ProgressDialogFlags.AutoTime;
                if (!ShowCancelButton) flags |= Interop.ProgressDialogFlags.NoCancel;
                if (!MinimizeBox) flags |= Interop.ProgressDialogFlags.NoMinimize;

                _dialog.StartProgressDialog(owner, null, flags, IntPtr.Zero);

                _dialogHandle = GetDialogHandle();
                DialogHandleAvailable?.Invoke(this, _dialogHandle);

                // 1. Set the main window title/frame to dark mode
                ApplyDarkModeToWindow(_dialogHandle);

                // 2. Subclass the window to intercept the text painting messages
                SubclassDialog(_dialogHandle);

                _backgroundWorker.RunWorkerAsync(argument);
            }
            catch
            {
                if (_dialog != null)
                {
                    Marshal.FinalReleaseComObject(_dialog);
                    _dialog = null;
                }
                throw;
            }
        }

        private IntPtr GetDialogHandle()
        {
            IntPtr hwnd = NativeMethods.User32.FindWindow("ProgressDialog", null);
            if (hwnd != IntPtr.Zero)
            {
                NativeMethods.User32.GetWindowThreadProcessId(hwnd, out uint processId);
                if (processId == (uint)System.Diagnostics.Process.GetCurrentProcess().Id) return hwnd;
            }

            if (!string.IsNullOrEmpty(WindowTitle))
            {
                hwnd = NativeMethods.User32.FindWindow(null, WindowTitle);
                if (hwnd != IntPtr.Zero) return hwnd;
            }

            return IntPtr.Zero;
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e) => OnDoWork(e);

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_dialog != null)
            {
                _dialog.StopProgressDialog();
                Marshal.FinalReleaseComObject(_dialog);
                _dialog = null;
            }

            if (_currentAnimationModuleHandle != null)
            {
                _currentAnimationModuleHandle.Dispose();
                _currentAnimationModuleHandle = null;
            }

            if (_ownerHandle != IntPtr.Zero) NativeMethods.User32.EnableWindow(_ownerHandle, true);

            _dialogHandle = IntPtr.Zero;
            OnRunWorkerCompleted(new RunWorkerCompletedEventArgs((!e.Cancelled && e.Error == null) ? e.Result : null, e.Error, e.Cancelled));
        }

        private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (_dialog != null)
            {
                _cancellationPending = _dialog.HasUserCancelled();

                if (e.ProgressPercentage >= 0 && e.ProgressPercentage <= 100)
                {
                    _dialog.SetProgress((uint)e.ProgressPercentage, 100);

                    if (e.UserState is ProgressChangedData data)
                    {
                        if (data.Text != null) _text = data.Text;
                        if (data.Description != null) _description = data.Description;

                        _dialog.SetLine(1, _text, UseCompactPathsForText, IntPtr.Zero);
                        _dialog.SetLine(2, _description, UseCompactPathsForDescription, IntPtr.Zero);

                        OnProgressChanged(new ProgressChangedEventArgs(e.ProgressPercentage, data.UserState));
                    }
                }
            }
        }

        private void InitializeComponent() { }

        private static void ApplyDarkModeToWindow(IntPtr hwnd)
        {
            if (!Program.Style.DarkMode) return;
            NativeMethods.Helpers.SetHWNDDarkMode(hwnd, Program.Style.DarkMode);
            DarkTaskDialog.DarkenProgressDialog(hwnd);
        }

        #endregion
    }

    #region Native COM Interface Mapping

    namespace Interop
    {
        /// <summary>
        /// Managed projection of the native shell IProgressDialog interface.
        /// </summary>
        [ComImport]
        [Guid("EBBC7C04-315E-11d2-B62F-006097DF5BD4")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [CoClass(typeof(ProgressDialogCoClass))]
        internal interface IProgressDialog
        {
            void StartProgressDialog([In] IntPtr hwndParent, [In, MarshalAs(UnmanagedType.IUnknown)] object enableModeless, [In] ProgressDialogFlags dwFlags, [In] IntPtr pvReserved);
            void StopProgressDialog();
            void SetTitle([In, MarshalAs(UnmanagedType.LPWStr)] string pwzTitle);
            void SetAnimation([In] IntPtr hInstAnimation, [In] ushort idAnimation);
            [PreserveSig]
            [return: MarshalAs(UnmanagedType.Bool)]
            bool HasUserCancelled();
            void SetProgress([In] uint dwCompleted, [In] uint dwTotal);
            void SetProgress64([In] ulong ullCompleted, [In] ulong ullTotal);
            void SetLine([In] uint dwLineNum, [In, MarshalAs(UnmanagedType.LPWStr)] string pwzString, [In, MarshalAs(UnmanagedType.VariantBool)] bool fCompactPath, [In] IntPtr pvReserved);
            void SetCancelMsg([In, MarshalAs(UnmanagedType.LPWStr)] string pwzCancelMsg, [In] object pvReserved);
            void Timer([In] uint dwTimerAction, [In] object pvReserved);
        }

        /// <summary>
        /// Bare CoClass target for CLSID_ProgressDialog (F8383852-FCD3-11d1-A6B9-006097DF5BD4).
        /// Deliberately empty - it exists only so [CoClass] on <see cref="IProgressDialog"/> has
        /// a concrete type to CoCreateInstance. Never instantiate this type directly;
        /// use "new IProgressDialog()" instead.
        /// </summary>
        [ComImport]
        [Guid("F8383852-FCD3-11d1-A6B9-006097DF5BD4")]
        internal class ProgressDialogCoClass
        {
        }

        [Flags]
        internal enum ProgressDialogFlags : uint
        {
            Normal = 0x00000000,
            Modal = 0x00000001,
            AutoTime = 0x00000002,
            NoTime = 0x00000004,
            NoMinimize = 0x00000008,
            NoProgressBar = 0x00000010,
            MarqueeProgress = 0x00000020,
            NoCancel = 0x00000040
        }
    }

    #endregion

    #region Animation Support

    public class AnimationResource
    {
        private readonly string _moduleName;
        private readonly int _resourceId;
        private static string _shell32 = "shell32.dll";

        private AnimationResource(string moduleName, int resourceId)
        {
            _moduleName = moduleName;
            _resourceId = resourceId;
        }

        public int ResourceId => _resourceId;

        public SafeModuleHandle LoadLibrary() => SafeModuleHandle.LoadLibrary(_moduleName);

        public static AnimationResource GetShellAnimation(ShellAnimation animation)
        {
            switch (animation)
            {
                case ShellAnimation.FlyingPapers: return new AnimationResource(_shell32, 160);
                case ShellAnimation.Searching: return new AnimationResource(_shell32, 165);
                case ShellAnimation.Deleting: return new AnimationResource(_shell32, 150);
                case ShellAnimation.Copying: return new AnimationResource(_shell32, 151);
                case ShellAnimation.EmptyRecycle: return new AnimationResource(_shell32, 152);
                case ShellAnimation.Saving: return new AnimationResource(_shell32, 153);
                default: return null;
            }
        }
    }

    public enum ShellAnimation
    {
        FlyingPapers,
        Searching,
        Deleting,
        Copying,
        EmptyRecycle,
        Saving
    }

    public class SafeModuleHandle : SafeHandle
    {
        private SafeModuleHandle() : base(IntPtr.Zero, true) { }

        public override bool IsInvalid => handle == IntPtr.Zero || handle == new IntPtr(-1);

        protected override bool ReleaseHandle() => NativeMethods.Kernel32.FreeLibrary(handle);

        public static SafeModuleHandle LoadLibrary(string fileName)
        {
            IntPtr module = NativeMethods.Kernel32.LoadLibraryEx(fileName, IntPtr.Zero, 0);
            if (module == IntPtr.Zero) throw new Win32Exception();

            SafeModuleHandle handle = new();
            handle.SetHandle(module);
            return handle;
        }
    }

    public enum ProgressBarStyle
    {
        ProgressBar,
        MarqueeProgressBar,
        None
    }

    #endregion
}