using NativeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigWindow
{
    public class Utils
    {
        public static List<IntPtr> findWindows(string name)
        {
            Process[] processes = Process.GetProcessesByName(name);
            if (processes.Length < 1)
            {
                return null;
            }
            var windows = new List<IntPtr>();
            foreach (Process proc in processes)
            {
                if (User32.IsWindow(proc.MainWindowHandle))
                {
                    if (!windows.Contains(proc.MainWindowHandle))
                    {
                        windows.Add(proc.MainWindowHandle);
                    }
                }
            }
            return windows;
        }
    }
}
