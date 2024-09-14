using NativeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigWindow
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool isQuit = false;
            foreach (string arg in args)
            {
                if ("--apply-quit".Equals(arg))
                {
                    DoSetWindow(Utils.loadConfig(false));
                    isQuit = true;
                }
            }
            if (!isQuit)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new SetWindow(args));
            }
        }
        static void DoSetWindow(XmlConfig config)
        {
            foreach (ItemConfig item in config.items)
            {

                var wins = Utils.findWindows(item.processName);
                if (wins == null)
                {
                    Console.WriteLine(item.processName + ": not found");
                }
                else
                {
                    Console.WriteLine(item.processName + ": found " + wins.Count + " windows");
                    if (wins.Count < 1)
                    {
                        Console.WriteLine(item.processName + ": not found any window");
                    }
                    else if (wins.Count > 1)
                    {
                        foreach (IntPtr win in wins)
                        {
                            if (User32.MoveWindow(win, item.left, item.top, item.width, item.height, 1))
                            {
                                Console.WriteLine(item.processName + "-" + "positioned");
                            }
                            else
                            {
                                Console.WriteLine(item.processName + "-" + "positioned failed");
                            }
                        }
                    }
                    else
                    {
                        if (User32.MoveWindow(wins[0], item.left, item.top, item.width, item.height, 1))
                        {
                            Console.WriteLine(item.processName + "-" + "positioned");
                        }
                        else
                        {
                            Console.WriteLine(item.processName + "-" + "positioned failed");
                        }
                    }
                }
            }
        }
    }
}
