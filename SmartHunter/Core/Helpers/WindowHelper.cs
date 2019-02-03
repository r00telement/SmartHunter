using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace SmartHunter.Core.Helpers
{
    public static class WindowHelper
    {
        static uint TopMostWindowSizePositions
        {
            get
            {
                return (uint)WindowsApi.WindowSizePositionFlag.SWP_NOMOVE
                    | (uint)WindowsApi.WindowSizePositionFlag.SWP_NOSIZE
                    | (uint)WindowsApi.WindowSizePositionFlag.SWP_SHOWWINDOW
                    | (uint)WindowsApi.WindowSizePositionFlag.SWP_NOACTIVATE;
            }
        }

        static uint TopMostSelectableWindowStyleFlags
        {
            get
            {
                return (uint)WindowsApi.WindowStyleFlag.WS_EX_LAYERED
                    | (uint)WindowsApi.WindowStyleFlag.WS_EX_TOPMOST;
            }
        }

        static uint TopMostTransparentWindowStyleFlags
        {
            get
            {
                return (uint)WindowsApi.WindowStyleFlag.WS_EX_LAYERED
                    | (uint)WindowsApi.WindowStyleFlag.WS_EX_TRANSPARENT
                    | (uint)WindowsApi.WindowStyleFlag.WS_EX_TOPMOST;
            }
        }

        public static void SetTopMostSelectable(Window window)
        {
            var handle = new WindowInteropHelper(window).EnsureHandle();
            WindowsApi.SetWindowLong(handle, (int)WindowsApi.WindowLongGroup.GWL_EXSTYLE, TopMostSelectableWindowStyleFlags);
            WindowsApi.SetWindowPos(handle, -1, 0, 0, 0, 0, TopMostWindowSizePositions);
        }

        public static void SetTopMostTransparent(Window window)
        {
            var handle = new WindowInteropHelper(window).EnsureHandle();
            WindowsApi.SetWindowLong(handle, (int)WindowsApi.WindowLongGroup.GWL_EXSTYLE, TopMostTransparentWindowStyleFlags);
            WindowsApi.SetWindowPos(handle, -1, 0, 0, 0, 0, TopMostWindowSizePositions);
        }

        // Experimental function to set the window background to use Windows 10 acrylic - this part of the win api is poorly documented
        public static void EnableAcrylic(Window window)
        {
            var handle = new WindowInteropHelper(window).EnsureHandle();

            var accent = new WindowsApi.AccentPolicy();
            accent.GradientColor = 0x01000000;

            accent.AccentState = WindowsApi.AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND;

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowsApi.WindowCompositionAttributeData();
            data.Attribute = WindowsApi.WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            WindowsApi.SetWindowCompositionAttribute(handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }
    }
}