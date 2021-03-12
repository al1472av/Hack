using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using Hack.Extensions;

namespace Hack
{
    public static class Utilities
    {
        public static T Read<T>(this ProcessWrapper process, IntPtr lpBaseAddress) where T : unmanaged
            => Read<T>(process.GameProcess.Handle, lpBaseAddress);


        public static T Read<T>(this ModuleWrapper module, int offset) where T : unmanaged
            => Read<T>(module.Process.Handle, module.ProcessModule.BaseAddress + offset);


        public static T Read<T>(IntPtr hProcess, IntPtr lpBaseAddress) where T : unmanaged
        {
            var size = Marshal.SizeOf<T>();
            var buffer = (object) default(T);
            Kernel32Calls.ReadProcessMemory(hProcess, lpBaseAddress, buffer, size, out var lpNumberOfBytesRead);
            return lpNumberOfBytesRead == size ? (T) buffer : default;
        }
    }
}