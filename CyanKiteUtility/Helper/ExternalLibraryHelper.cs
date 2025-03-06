using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CyanKiteUtility
{
    public class ExternalLibraryHelper
    {
        [DllImport("kernel32", SetLastError = true)]
        private static extern bool SetDllDirectory(string lpPathName);

        public static void AttachDllDirectory()
        {
            try
            {
                var attachDllDirectory = Path.Combine(Directory.GetCurrentDirectory(), "ExternalLibrary", Environment.Is64BitOperatingSystem ? "x64" : "x86");

                if (Directory.Exists(attachDllDirectory))
                {
                    SetDllDirectory(attachDllDirectory);
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
