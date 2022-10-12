using NativeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSet
{
    class Program
    {
        static XmlConfig config;
        static void Main(string[] args)
        {
            config = Utils.loadConfig(false);
            SetWindow();
        }

        static void SetWindow()
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
                        }
                    }
                    else
                    {
                        if (User32.MoveWindow(wins[0], item.left, item.top, item.width, item.height, 1))
                        {
                            Console.WriteLine(item.processName + "-" + "positioned");
                        }
                    }
                }
            }
        }
    }
}
