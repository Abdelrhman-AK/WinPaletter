using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static WinPaletter.NativeMethods.User32;
using static WinPaletter.NativeMethods.UxTheme;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Raw-COM UI Automation client. Talks directly to uiautomationcore.dll through
    /// manually defined COM interfaces with verified IIDs and vtable layouts.
    /// </summary>
    internal static class ComUIAutomation
    {
        /// <summary>
        /// Toggle state for UIA toggle-pattern elements (VerificationCheckBox).
        /// </summary>
        internal enum ToggleState
        {
            Off = 0,
            On = 1,
            Indeterminate = 2
        }

        // Property IDs (UIAutomationClient.h) -- plain documented integers
        public const int UIA_BoundingRectanglePropertyId = 30001;
        public const int UIA_ControlTypePropertyId = 30003;
        public const int UIA_NamePropertyId = 30005;
        public const int UIA_AutomationIdPropertyId = 30011;
        public const int UIA_ClassNamePropertyId = 30012;
        public const int UIA_NativeWindowHandlePropertyId = 30020;

        public const int UIA_TogglePatternId = 10015;

        // Control type IDs
        public const int UIA_ButtonControlTypeId = 50000;
        public const int UIA_HyperlinkControlTypeId = 50005;
        public const int UIA_ProgressBarControlTypeId = 50012;
        public const int UIA_RadioButtonControlTypeId = 50013;
        public const int UIA_ScrollBarControlTypeId = 50014;
        public const int UIA_PaneControlTypeId = 50033;

        // ToggleState enum values from UIAutomationClient.h
        private const int ToggleState_Off = 0;
        private const int ToggleState_On = 1;
        private const int ToggleState_Indeterminate = 2;

        // GUIDs from uiautomationcore.h
        private static readonly Guid CLSID_CUIAutomation = new Guid("ff48dba4-60ef-4201-aa87-54103eef594e");

        private static IUIAutomation s_automation;

        private static IUIAutomation Automation
        {
            get
            {
                if (s_automation == null)
                {
                    object obj = Activator.CreateInstance(
                        Type.GetTypeFromCLSID(CLSID_CUIAutomation));
                    s_automation = (IUIAutomation)obj;
                }
                return s_automation;
            }
        }

        /// <summary>
        /// Wraps a native window handle in a UIA element. Returns null instead of
        /// throwing if the window is gone or has no UIA representation.
        /// </summary>
        public static IUIAutomationElement FromHandle(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero) return null;
            try
            {
                Automation.ElementFromHandle(hwnd, out IUIAutomationElement element);
                return element;
            }
            catch (COMException)
            {
                return null;
            }
        }

        /// <summary>
        /// Enumerates the direct content-view children of <paramref name="parent"/> --
        /// one level only. This matches the flat structure of a TaskDialog DirectUI
        /// page, where every part we care about is a direct child of the TaskPage root,
        /// and matches what TreeWalker.ContentViewWalker + GetFirstChild/GetNextSibling
        /// did previously.
        /// </summary>
        public static IEnumerable<IUIAutomationElement> GetContentChildren(IUIAutomationElement parent)
        {
            if (parent == null) yield break;

            IUIAutomationTreeWalker walker;
            try
            {
                Automation.GetContentViewWalker(out walker);
            }
            catch (COMException)
            {
                yield break;
            }

            IUIAutomationElement child;
            try
            {
                walker.GetFirstChildElement(parent, out child);
            }
            catch (COMException)
            {
                yield break;
            }

            while (child != null)
            {
                yield return child;

                try
                {
                    IUIAutomationElement next;
                    walker.GetNextSiblingElement(child, out next);
                    child = next;
                }
                catch (COMException)
                {
                    yield break;
                }
            }
        }

        public static object GetPropertyValue(IUIAutomationElement element, int propertyId)
        {
            if (element == null) return null;
            try
            {
                element.GetCurrentPropertyValue(propertyId, out object value);
                return value;
            }
            catch (COMException)
            {
                return null;
            }
        }

        public static string GetPropertyValueString(IUIAutomationElement element, int propertyId)
        {
            return GetPropertyValue(element, propertyId) as string ?? string.Empty;
        }

        public static int GetPropertyValueInt(IUIAutomationElement element, int propertyId)
        {
            object value = GetPropertyValue(element, propertyId);
            if (value is int i) return i;
            if (value is double d) return (int)d;
            return 0;
        }

        public static IntPtr GetNativeWindowHandle(IUIAutomationElement element)
        {
            object value = GetPropertyValue(element, UIA_NativeWindowHandlePropertyId);
            if (value is int i) return new IntPtr(i);
            if (value is IntPtr p) return p;
            return IntPtr.Zero;
        }

        public static RECT GetBoundingRectangleScreen(IUIAutomationElement element)
        {
            object value = GetPropertyValue(element, UIA_BoundingRectanglePropertyId);
            if (value is double[] arr && arr.Length == 4)
            {
                return new RECT
                {
                    left = (int)arr[0],
                    top = (int)arr[1],
                    right = (int)(arr[0] + arr[2]),
                    bottom = (int)(arr[1] + arr[3])
                };
            }
            return new RECT();
        }

        public static ToggleState GetToggleState(IUIAutomationElement element)
        {
            if (element == null) return ToggleState.Indeterminate;
            try
            {
                element.GetCurrentPattern(UIA_TogglePatternId, out object patternObj);
                if (patternObj is IUIAutomationTogglePattern toggle)
                {
                    toggle.get_CurrentToggleState(out int state);
                    if (state == ToggleState_Off) return ToggleState.Off;
                    if (state == ToggleState_On) return ToggleState.On;
                    return ToggleState.Indeterminate;
                }
            }
            catch (COMException) { }
            catch (InvalidCastException) { }
            return ToggleState.Indeterminate;
        }

        /// <summary>
        /// Builds a <see cref="UIAElementInfo"/> from a raw-COM UIA element, converting
        /// its bounding rectangle from screen to <paramref name="hwndForClientCoords"/>'s
        /// client coordinates. Direct substitute for the AutomationElement-based
        /// conversion that used to live in DarkTaskDialog.QueryElements.
        /// </summary>
        public static UIAElementInfo ToElementInfo(IUIAutomationElement element, IntPtr hwndForClientCoords)
        {
            if (element == null) return null;

            UIAElementInfo info = new UIAElementInfo
            {
                automationId = GetPropertyValueString(element, UIA_AutomationIdPropertyId),
                name = GetPropertyValueString(element, UIA_NamePropertyId)
            };

            RECT screenRect = GetBoundingRectangleScreen(element);

            POINT topLeft = new POINT { X = screenRect.left, Y = screenRect.top };
            POINT bottomRight = new POINT { X = screenRect.right, Y = screenRect.bottom };
            ScreenToClient(hwndForClientCoords, ref topLeft);
            ScreenToClient(hwndForClientCoords, ref bottomRight);

            info.rect.left = topLeft.X;
            info.rect.top = topLeft.Y;
            info.rect.right = bottomRight.X;
            info.rect.bottom = bottomRight.Y;

            if (info.automationId == "VerificationCheckBox")
            {
                info.toggleState = GetToggleState(element);
            }

            return info;
        }

        /// <summary>
        /// Populates <paramref name="outList"/> with the whitelisted TaskDialog parts
        /// found among the content-view children of the window at <paramref name="hwnd"/>.
        /// Direct substitute for the old System.Windows.Automation-based QueryElements
        /// in DarkTaskDialog.
        /// </summary>
        public static bool QueryElements(IntPtr hwnd, List<UIAElementInfo> outList)
        {
            outList.Clear();

            IUIAutomationElement root = FromHandle(hwnd);
            if (root == null) return false;

            foreach (IUIAutomationElement child in GetContentChildren(root))
            {
                UIAElementInfo info = ToElementInfo(child, hwnd);
                if (info == null || info.IsRectEmpty() || string.IsNullOrEmpty(info.automationId)) continue;

                string id = info.automationId;
                if (id == "MainIcon" || id == "MainInstruction" ||
                    id == "ContentText" || id == "ExpandedInformationText" ||
                    id == "ExpandedFooterText" ||
                    id == "ExpandoButton" || id == "VerificationCheckBox" ||
                    id == "FootnoteText" || id == "FootnoteIcon" ||
                    id.StartsWith("RadioButton_") ||
                    id.StartsWith("CommandLink_") ||
                    id.StartsWith("CommandButton_"))
                {
                    outList.Add(info);
                }
            }

            return outList.Count > 0;
        }
    }

    #region COM Interface Definitions

    /// <summary>
    /// IUIAutomation interface (IID: 30cbe57d-d9d0-452a-ab13-7ac5ac4825ee)
    /// </summary>
    [ComImport]
    [Guid("30cbe57d-d9d0-452a-ab13-7ac5ac4825ee")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IUIAutomation
    {
        // Core methods we need - vtable order must match uiautomationcore.h

        [PreserveSig]
        int CompareElements(
            [MarshalAs(UnmanagedType.Interface)] IUIAutomationElement element1,
            [MarshalAs(UnmanagedType.Interface)] IUIAutomationElement element2,
            [MarshalAs(UnmanagedType.Bool)] out bool result);

        [PreserveSig]
        int CompareRuntimeIds(
            IntPtr runtimeId1,
            IntPtr runtimeId2,
            [MarshalAs(UnmanagedType.Bool)] out bool result);

        [PreserveSig]
        int GetRootElement(
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement root);

        [PreserveSig]
        int ElementFromHandle(
            IntPtr hwnd,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement element);

        [PreserveSig]
        int ElementFromPoint(
            POINT pt,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement element);

        [PreserveSig]
        int GetFocusedElement(
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement element);

        [PreserveSig]
        int GetRootElementBuildCache(
            IntPtr cacheRequest,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement root);

        [PreserveSig]
        int ElementFromHandleBuildCache(
            IntPtr hwnd,
            IntPtr cacheRequest,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement element);

        [PreserveSig]
        int ElementFromPointBuildCache(
            POINT pt,
            IntPtr cacheRequest,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement element);

        [PreserveSig]
        int GetFocusedElementBuildCache(
            IntPtr cacheRequest,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement element);

        [PreserveSig]
        int CreateTreeWalker(
            IntPtr condition,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationTreeWalker walker);

        [PreserveSig]
        int GetControlViewWalker(
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationTreeWalker walker);

        [PreserveSig]
        int GetContentViewWalker(
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationTreeWalker walker);

        [PreserveSig]
        int GetRawViewWalker(
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationTreeWalker walker);

        // Additional methods omitted for brevity - not needed for our use case
    }

    /// <summary>
    /// IUIAutomationElement interface (IID: d22108aa-8ac5-49a5-837b-37bbb3d7591e)
    /// </summary>
    [ComImport]
    [Guid("d22108aa-8ac5-49a5-837b-37bbb3d7591e")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IUIAutomationElement
    {
        // Core methods we need - vtable order must match uiautomationcore.h

        [PreserveSig]
        int SetFocus();

        [PreserveSig]
        int GetRuntimeId(out IntPtr runtimeId);

        [PreserveSig]
        int FindFirst(
            TreeScope scope,
            IntPtr condition,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement found);

        [PreserveSig]
        int FindAll(
            TreeScope scope,
            IntPtr condition,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElementArray found);

        [PreserveSig]
        int FindFirstBuildCache(
            TreeScope scope,
            IntPtr condition,
            IntPtr cacheRequest,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement found);

        [PreserveSig]
        int FindAllBuildCache(
            TreeScope scope,
            IntPtr condition,
            IntPtr cacheRequest,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElementArray found);

        [PreserveSig]
        int BuildUpdatedCache(
            IntPtr cacheRequest,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement updatedElement);

        [PreserveSig]
        int GetCurrentPropertyValue(
            int propertyId,
            out object value);

        [PreserveSig]
        int GetCurrentPropertyValueEx(
            int propertyId,
            [MarshalAs(UnmanagedType.Bool)] bool ignoreDefault,
            out object value);

        [PreserveSig]
        int GetCachedPropertyValue(
            int propertyId,
            out object value);

        [PreserveSig]
        int GetCachedPropertyValueEx(
            int propertyId,
            [MarshalAs(UnmanagedType.Bool)] bool ignoreDefault,
            out object value);

        [PreserveSig]
        int GetCurrentPattern(
            int patternId,
            [MarshalAs(UnmanagedType.IUnknown)] out object patternObject);

        [PreserveSig]
        int GetCachedPattern(
            int patternId,
            [MarshalAs(UnmanagedType.IUnknown)] out object patternObject);

        [PreserveSig]
        int GetCachedParent(
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement parent);

        [PreserveSig]
        int GetCachedChildren(
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElementArray children);

        [PreserveSig]
        int GetCurrentParent(
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement parent);

        [PreserveSig]
        int GetCurrentChildren(
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElementArray children);

        // Additional methods omitted for brevity
    }

    /// <summary>
    /// IUIAutomationTreeWalker interface (IID: 4042c624-389c-4afc-a630-9df854a541fc)
    /// </summary>
    [ComImport]
    [Guid("4042c624-389c-4afc-a630-9df854a541fc")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IUIAutomationTreeWalker
    {
        // Core methods we need - vtable order must match uiautomationcore.h

        [PreserveSig]
        int GetParentElement(
            [MarshalAs(UnmanagedType.Interface)] IUIAutomationElement element,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement parent);

        [PreserveSig]
        int GetFirstChildElement(
            [MarshalAs(UnmanagedType.Interface)] IUIAutomationElement element,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement first);

        [PreserveSig]
        int GetLastChildElement(
            [MarshalAs(UnmanagedType.Interface)] IUIAutomationElement element,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement last);

        [PreserveSig]
        int GetNextSiblingElement(
            [MarshalAs(UnmanagedType.Interface)] IUIAutomationElement element,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement next);

        [PreserveSig]
        int GetPreviousSiblingElement(
            [MarshalAs(UnmanagedType.Interface)] IUIAutomationElement element,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement previous);

        [PreserveSig]
        int GetParentElementBuildCache(
            [MarshalAs(UnmanagedType.Interface)] IUIAutomationElement element,
            IntPtr cacheRequest,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement parent);

        [PreserveSig]
        int GetFirstChildElementBuildCache(
            [MarshalAs(UnmanagedType.Interface)] IUIAutomationElement element,
            IntPtr cacheRequest,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement first);

        [PreserveSig]
        int GetLastChildElementBuildCache(
            [MarshalAs(UnmanagedType.Interface)] IUIAutomationElement element,
            IntPtr cacheRequest,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement last);

        [PreserveSig]
        int GetNextSiblingElementBuildCache(
            [MarshalAs(UnmanagedType.Interface)] IUIAutomationElement element,
            IntPtr cacheRequest,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement next);

        [PreserveSig]
        int GetPreviousSiblingElementBuildCache(
            [MarshalAs(UnmanagedType.Interface)] IUIAutomationElement element,
            IntPtr cacheRequest,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement previous);

        [PreserveSig]
        int GetCondition(
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationCondition condition);
    }

    /// <summary>
    /// IUIAutomationTogglePattern interface (IID: 94cf1458-0f6e-4496-9f73-92a66fe02d9e)
    /// </summary>
    [ComImport]
    [Guid("94cf1458-0f6e-4496-9f73-92a66fe02d9e")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IUIAutomationTogglePattern
    {
        // Methods must be in vtable order

        [PreserveSig]
        int Toggle();

        [PreserveSig]
        int get_CurrentToggleState(out int state);

        [PreserveSig]
        int get_CachedToggleState(out int state);

        [PreserveSig]
        int get_CurrentToggleStateProgrammaticName(
            [MarshalAs(UnmanagedType.BStr)] out string programmaticName);

        [PreserveSig]
        int get_CachedToggleStateProgrammaticName(
            [MarshalAs(UnmanagedType.BStr)] out string programmaticName);
    }

    /// <summary>
    /// IUIAutomationElementArray interface (IID: 14314595-b4bc-4055-95f2-58f2e42c9855)
    /// </summary>
    [ComImport]
    [Guid("14314595-b4bc-4055-95f2-58f2e42c9855")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IUIAutomationElementArray
    {
        [PreserveSig]
        int get_Length(out int length);

        [PreserveSig]
        int GetElement(
            int index,
            [MarshalAs(UnmanagedType.Interface)] out IUIAutomationElement element);
    }

    /// <summary>
    /// IUIAutomationCondition interface (IID: e3522c9b-e012-4abe-9901-47b60d6af7b1)
    /// </summary>
    [ComImport]
    [Guid("e3522c9b-e012-4abe-9901-47b60d6af7b1")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IUIAutomationCondition
    {
        // No methods needed for our use case
    }

    /// <summary>
    /// TreeScope enum from UIAutomationClient.h
    /// </summary>
    internal enum TreeScope
    {
        Element = 1,
        Children = 2,
        Descendants = 4,
        Subtree = 7
    }

    #endregion

    #region COM Class Factory

    /// <summary>
    /// CUIAutomation coclass (CLSID: ff48dba4-60ef-4201-aa87-54103eef594e)
    /// </summary>
    [ComImport]
    [Guid("ff48dba4-60ef-4201-aa87-54103eef594e")]
    [ClassInterface(ClassInterfaceType.None)]
    internal class CUIAutomation
    {
    }

    #endregion
}