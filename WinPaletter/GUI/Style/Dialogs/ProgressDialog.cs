using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static WinPaletter.NativeMethods.User32;

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
        private readonly BackgroundWorker _backgroundWorker = new();
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
            if (hwnd == IntPtr.Zero || !Program.Style.DarkMode || !NativeMethods.User32.IsWindow(hwnd)) return;

            // Clean up any previous subclass first
            RemoveSubclass();

            // Small delay to ensure window is fully created (CRITICAL FOR FORCED DARK MODE APPLICATION)
            Thread.Sleep(10);

            // Create a dark brush matching your app's theme background color
            if (_darkBackgroundBrush == IntPtr.Zero)
            {
                _darkBackgroundBrush = NativeMethods.GDI32.CreateSolidBrush(0x202020);
                if (_darkBackgroundBrush == IntPtr.Zero)
                    return;
            }

            // Keep the delegate alive in a field so garbage collection doesn't kill it
            if (_hookedWndProcDelegate == null)
            {
                _hookedWndProcDelegate = new NativeMethods.User32.WndProcDelegate(ProgressDialogWndProc);
            }

            IntPtr lpfnWndProc = Marshal.GetFunctionPointerForDelegate(_hookedWndProcDelegate);
            if (lpfnWndProc == IntPtr.Zero) return;

            // Subclass the main dialog window
            IntPtr result = NativeMethods.User32.SetWindowLongPtr(hwnd, -4, lpfnWndProc);
            if (result == IntPtr.Zero || result == new IntPtr(-1))
            {
                if (_darkBackgroundBrush != IntPtr.Zero)
                {
                    NativeMethods.GDI32.DeleteObject(_darkBackgroundBrush);
                    _darkBackgroundBrush = IntPtr.Zero;
                }
                _hookedWndProcDelegate = null;
                return;
            }

            _originalWndProc = result;
            _dialogHandle = hwnd;

            // Strip the modern UX themes from the static text child controls.
            NativeMethods.User32.EnumChildWindows(hwnd, (childHwnd, lParam) =>
            {
                StringBuilder className = new(256);
                NativeMethods.User32.GetClassName(childHwnd, className, className.Capacity);

                if (className.ToString().Equals("Static", StringComparison.OrdinalIgnoreCase))
                {
                    NativeMethods.UxTheme.SetWindowTheme(childHwnd, "", "");
                }
                return true;
            }, IntPtr.Zero);

            // Force the window and its children to redraw
            NativeMethods.User32.RedrawWindow(hwnd, IntPtr.Zero, IntPtr.Zero, 0x0101);
        }

        private void RemoveSubclass()
        {
            if (_dialogHandle != IntPtr.Zero && _originalWndProc != IntPtr.Zero)
            {
                if (NativeMethods.User32.IsWindow(_dialogHandle))
                {
                    try
                    {
                        NativeMethods.User32.SetWindowLongPtr(_dialogHandle, (int)NativeMethods.User32.WindowsLongs.WndProc, _originalWndProc);
                    }
                    catch { }
                }
                _originalWndProc = IntPtr.Zero;
                _dialogHandle = IntPtr.Zero;
            }

            if (_darkBackgroundBrush != IntPtr.Zero)
            {
                try
                {
                    NativeMethods.GDI32.DeleteObject(_darkBackgroundBrush);
                }
                catch { }
                finally
                {
                    _darkBackgroundBrush = IntPtr.Zero;
                }
            }
        }

        private IntPtr GetDialogHandle()
        {
            // First try to find by title (the most reliable way)
            if (!string.IsNullOrEmpty(WindowTitle))
            {
                IntPtr hwnd = NativeMethods.User32.FindWindow(null, WindowTitle);
                if (hwnd != IntPtr.Zero)
                {
                    NativeMethods.User32.GetWindowThreadProcessId(hwnd, out uint processId);
                    if (processId == (uint)System.Diagnostics.Process.GetCurrentProcess().Id && NativeMethods.User32.IsWindow(hwnd))
                        return hwnd;
                }
            }

            // If still not found, wait a bit and try again
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(50); // (CRITICAL FOR FORCED DARK MODE APPLICATION)
                if (!string.IsNullOrEmpty(WindowTitle))
                {
                    IntPtr hwnd = NativeMethods.User32.FindWindow(null, WindowTitle);
                    if (hwnd != IntPtr.Zero)
                    {
                        NativeMethods.User32.GetWindowThreadProcessId(hwnd, out uint processId);
                        if (processId == (uint)System.Diagnostics.Process.GetCurrentProcess().Id && NativeMethods.User32.IsWindow(hwnd))
                            return hwnd;
                    }
                }
            }

            return IntPtr.Zero;
        }

        private void CleanupDialog()
        {
            RemoveSubclass();

            if (_dialog != null)
            {
                try
                {
                    _dialog.StopProgressDialog();
                }
                catch (Exception ex)
                {
                    Program.Log?.Debug($"CleanupDialog: Exception stopping dialog", ex);
                }
                finally
                {
                    Marshal.FinalReleaseComObject(_dialog);
                    _dialog = null;
                }
            }

            if (_currentAnimationModuleHandle != null)
            {
                _currentAnimationModuleHandle.Dispose();
                _currentAnimationModuleHandle = null;
            }

            if (_ownerHandle != IntPtr.Zero)
            {
                try
                {
                    NativeMethods.User32.EnableWindow(_ownerHandle, true);
                }
                catch (Exception ex)
                {
                    Program.Log?.Debug($"CleanupDialog: Exception enabling owner", ex);
                }
            }

            // Don't reset _dialogHandle and _originalWndProc here - they're reset in RemoveSubclass
            Program.Log?.Debug("CleanupDialog: Reset handles and delegates");
        }

        private IntPtr ProgressDialogWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            // WM_CTLCOLORSTATIC handles the text and background color for static elements
            if (msg == (uint)WindowsMessage.CtlColorStatic)
            {
                // Text color: White (0x00FFFFFF)
                NativeMethods.GDI32.SetTextColor(wParam, 0xFFFFFF);
                // Remove the default background bounding box behind the font pixels
                NativeMethods.GDI32.SetBkMode(wParam, NativeMethods.GDI32.TRANSPARENT);

                // Return the brush to paint the control's bounding box background dark
                return _darkBackgroundBrush;
            }

            if (msg == (uint)WindowsMessage.Destroy)
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
            {
                Program.Log?.Debug("RunProgressDialog: Dialog is already running");
                throw new InvalidOperationException("The progress dialog is already running.");
            }

            if (Animation != null)
            {
                try
                {
                    _currentAnimationModuleHandle = Animation.LoadLibrary();
                }
                catch (Exception ex)
                {
                    Program.Log?.Debug($"RunProgressDialog: Failed to load animation", ex);
                    _currentAnimationModuleHandle = null;
                }
            }

            _cancellationPending = false;
            _ownerHandle = owner;
            _dialogHandle = IntPtr.Zero;
            _originalWndProc = IntPtr.Zero;
            _hookedWndProcDelegate = null;

            try
            {
                _dialog = new Interop.IProgressDialog();

                _dialog.SetTitle(WindowTitle);

                if (Animation != null && _currentAnimationModuleHandle != null && !_currentAnimationModuleHandle.IsInvalid)
                {
                    _dialog.SetAnimation(_currentAnimationModuleHandle.DangerousGetHandle(), (ushort)Animation.ResourceId);
                }

                if (!string.IsNullOrEmpty(CancellationText))
                {
                    _dialog.SetCancelMsg(CancellationText, null);
                }

                _dialog.SetLine(1, Text, UseCompactPathsForText, IntPtr.Zero);
                _dialog.SetLine(2, Description, UseCompactPathsForDescription, IntPtr.Zero);

                Interop.ProgressDialogFlags flags = Interop.ProgressDialogFlags.Normal;
                if (owner != IntPtr.Zero)
                {
                    flags |= Interop.ProgressDialogFlags.Modal;
                }

                switch (ProgressBarStyle)
                {
                    case ProgressBarStyle.None:
                        flags |= Interop.ProgressDialogFlags.NoProgressBar;
                        break;
                    case ProgressBarStyle.MarqueeProgressBar:
                        if (!OS.WXP)
                        {
                            flags |= Interop.ProgressDialogFlags.MarqueeProgress;
                        }
                        else
                        {
                            flags |= Interop.ProgressDialogFlags.NoProgressBar;
                        }
                        break;
                }

                if (ShowTimeRemaining)
                {
                    flags |= Interop.ProgressDialogFlags.AutoTime;
                }
                if (!ShowCancelButton)
                {
                    flags |= Interop.ProgressDialogFlags.NoCancel;
                }
                if (!MinimizeBox)
                {
                    flags |= Interop.ProgressDialogFlags.NoMinimize;
                }

                _dialog.StartProgressDialog(owner, null, flags, IntPtr.Zero);
                Thread.Sleep(50); // CRITICAL FOR FORCED DARK MODE APPLICATION

                // Wait for the dialog to be created and get its handle
                _dialogHandle = GetDialogHandle();
                Program.Log?.Debug($"RunProgressDialog: Got dialog handle=0x{_dialogHandle.ToInt64():X}");

                if (_dialogHandle == IntPtr.Zero)
                {
                    Program.Log?.Debug("RunProgressDialog: Failed to get dialog handle!");
                }
                else
                {
                    ApplyDarkModeToWindow(_dialogHandle);
                    SubclassDialog(_dialogHandle);
                    DialogHandleAvailable?.Invoke(this, _dialogHandle);
                }

                Program.Log?.Debug("RunProgressDialog: Starting BackgroundWorker");
                _backgroundWorker.RunWorkerAsync(argument);

                while (_backgroundWorker.IsBusy)
                {
                    Application.DoEvents();
                    Thread.Sleep(10);
                }

                Program.Log?.Debug("RunProgressDialog: BackgroundWorker completed");
            }
            catch (Exception ex)
            {
                Program.Log?.Debug($"RunProgressDialog: Exception: {ex.Message}\n{ex.StackTrace}");
                // Clean up on exception
                RemoveSubclass();
                if (_dialog != null)
                {
                    try { _dialog.StopProgressDialog(); } catch { }
                    Marshal.FinalReleaseComObject(_dialog);
                    _dialog = null;
                }
                throw;
            }
            finally
            {
                // Ensure cleanup happens even if the dialog is closed unexpectedly
                if (!_backgroundWorker.IsBusy)
                {
                    Program.Log?.Debug("RunProgressDialog: Calling CleanupDialog from finally");
                    CleanupDialog();
                }
            }
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CleanupDialog();
            OnRunWorkerCompleted(new RunWorkerCompletedEventArgs((!e.Cancelled && e.Error == null) ? e.Result : null, e.Error, e.Cancelled));
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e) => OnDoWork(e);

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
            Dark.DarkDirectUI.DarkenProgressDialog(hwnd);
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