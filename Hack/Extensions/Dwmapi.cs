using System;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace Hack.Extensions
{
    public static class Dwmapi
    {
        [DllImport("dwmapi.dll", SetLastError = true)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMargins);
    }
}