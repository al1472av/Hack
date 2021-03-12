using System;
using System.Drawing;
using System.Threading;
using Hack.Extensions;
using Hack.Math;

namespace Hack
{
    public class GameHacker
    {
        public static Rectangle WindowRectangleClient;

        public static bool IsWindowActive => WindowHwnd == User32.GetForegroundWindow() &&
                                             WindowRectangleClient.Width > 0 && WindowRectangleClient.Height >= 0;

        private static IntPtr WindowHwnd;
        private static ProcessWrapper _processWrapper;

        public GameHacker()
        {
            //EnsureProcess();
        }

        public static void Initialize()
        {
            new Thread(EnsureData).Start();
        }

        private static void EnsureData()
        {
            EnsureProcess();
            while (true)
            {
                Thread.Sleep(1);
                EnsureWindow();
                Console.WriteLine(WindowRectangleClient.Size);
            }
           
        }

        private static bool EnsureProcess()
        {
            _processWrapper = new ProcessWrapper(Config.PROCESS_NAME, Config.ENGINE_DLL, Config.CLIENT_DLL);
            return _processWrapper != null && _processWrapper.IsModulesValid;
        }


        private static bool EnsureWindow()
        {
            WindowHwnd = User32.FindWindow(null, Config.WINDOW_NAME);
            if (WindowHwnd == IntPtr.Zero)
                return false;

            WindowRectangleClient = User32.ClientToScreen(WindowHwnd, out var point) &&
                                    User32.GetClientRect(WindowHwnd, out var rect)
                ? new Rectangle(point.X, point.Y, rect.Right - rect.Left, rect.Bottom - rect.Top)
                : default;

            return true;
        }
    }
}