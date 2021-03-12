using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace Hack.Extensions
{
    public static class ProcessExtensions
    {
        public static Rectangle GetClientRectangle(this Process process, IntPtr handle)
        {
            return User32.ClientToScreen(handle, out var point) && User32.GetClientRect(handle, out var rect)
                ? new Rectangle(point.X, point.Y, rect.Right - rect.Left, rect.Bottom - rect.Top)
                : default;
        }
        
        public static ProcessModule GetModule(this Process process, string moduleName)
            => process?.Modules.OfType<ProcessModule>().
                FirstOrDefault(t => string.Equals(t.ModuleName.ToLower(), moduleName.ToLower()));

        public static bool IsRunning(this Process process)
        {
            try
            {
                Process.GetProcessById(process.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }
    }
}