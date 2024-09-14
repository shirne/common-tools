using NativeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NativeLib
{
    public class Utils
    {

        const string configFile = "config.xml";
        const string emptyXml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><configs></configs>";

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

                var subWindows = User32.EnumWindows(proc.Id);
              
                windows.AddRange(subWindows);
                
            }
            return windows;
        }

        public static Dictionary<string, long> WindowStyles()
        {
            var map = new Dictionary<string,long >();

            map.Add("WS_BORDER", 0x00800000L);
            map.Add("WS_CAPTION", 0x00C00000L);
            map.Add("WS_CHILD", 0x40000000L);
            map.Add("WS_CHILDWINDOW", 0x40000000L);
            map.Add("WS_CLIPCHILDREN", 0x02000000L);
            map.Add("WS_CLIPSIBLINGS", 0x04000000L);
            map.Add("WS_DISABLED", 0x08000000L);
            map.Add("WS_DLGFRAME", 0x00400000L);
            map.Add("WS_GROUP", 0x00020000L);
            map.Add("WS_HSCROLL", 0x00100000L);
            map.Add("WS_ICONIC", 0x20000000L);
            map.Add("WS_MAXIMIZE", 0x01000000L);
            map.Add("WS_MAXIMIZEBOX", 0x00010000L);
            map.Add("WS_MINIMIZE", 0x20000000L);
            map.Add("WS_MINIMIZEBOX", 0x00020000L);
            map.Add("WS_OVERLAPPED", 0x00000000L);
            map.Add("WS_OVERLAPPEDWINDOW", 0x00000000L | 0x00C00000L | 0x00080000L | 0x00040000L | 0x00020000L | 0x00010000L);
            map.Add("WS_POPUP", 0x80000000L);
            map.Add("WS_POPUPWINDOW", 0x80000000L | 0x00800000L | 0x00080000L);
            map.Add("WS_SIZEBOX", 0x00040000L);
            map.Add("WS_SYSMENU", 0x00080000L);
            map.Add("WS_TABSTOP", 0x00010000L);
            map.Add("WS_THICKFRAME", 0x00040000L);
            map.Add("WS_TILED", 0x00000000L);
            map.Add("WS_TILEDWINDOW", 0x00000000L | 0x00C00000L | 0x00080000L | 0x00040000L | 0x00020000L | 0x00010000L);
            map.Add("WS_VISIBLE", 0x10000000L);
            map.Add("WS_VSCROLL", 0x00200000L);

            return map;
        }

        public static XmlConfig loadConfig(bool create=true)
        {
            string file = Directory.GetCurrentDirectory() + "/" + configFile;
            XmlConfig config = new XmlConfig();
            var doc = new XmlDocument();
            config = new XmlConfig();
            if (!File.Exists(configFile))
            {
                if (!create)
                {
                    return config;
                }
                doc.LoadXml(emptyXml);
                config.items.Add(new ItemConfig()
                {
                    processName = "QQ",
                });
                config.items.Add(new ItemConfig()
                {
                    processName = "TIM",
                });
                config.items.Add(new ItemConfig()
                {
                    processName = "WeChat",
                });
                saveConfig(config);
            }
            else
            {
                doc.Load(configFile);
                var nodes = doc.SelectNodes("configs/item");
                foreach (XmlNode node in nodes)
                {
                    var configItem = new ItemConfig();
                    foreach (XmlNode item in node.ChildNodes)
                    {
                        int value = 0;
                        int.TryParse(item.InnerText, out value);
                        switch (item.Name)
                        {
                            case "processName":
                                configItem.processName = item.InnerText;
                                break;
                            case "left":
                                configItem.left = value;
                                break;
                            case "top":
                                configItem.top = value;
                                break;
                            case "width":
                                if (value > 0) configItem.width = value;
                                break;
                            case "height":
                                if (value > 0) configItem.height = value;
                                break;
                        }

                    }
                    config.items.Add(configItem);
                }

            }
            return config;
        }

        public static void saveConfig(XmlConfig config)
        {
            var doc = new XmlDocument();
            doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><configs></configs>");
            var configNode = doc.SelectSingleNode("configs");
            foreach (ItemConfig item in config.items)
            {
                var child = doc.CreateElement("item");
                var processNameNode = doc.CreateElement("processName");
                processNameNode.InnerText = item.processName;
                child.AppendChild(processNameNode);

                var leftNode = doc.CreateElement("left");
                leftNode.InnerText = item.left.ToString();
                child.AppendChild(leftNode);

                var topNode = doc.CreateElement("top");
                topNode.InnerText = item.top.ToString();
                child.AppendChild(topNode);

                var widthNode = doc.CreateElement("width");
                widthNode.InnerText = item.width.ToString();
                child.AppendChild(widthNode);

                var heightNode = doc.CreateElement("height");
                heightNode.InnerText = item.height.ToString();
                child.AppendChild(heightNode);

                configNode.AppendChild(child);
            }

            using (XmlTextWriter writer = new XmlTextWriter(configFile, Encoding.UTF8))
            {
                writer.Indentation = 4;
                writer.Formatting = Formatting.Indented;
                doc.WriteTo(writer);
                writer.Flush();
            }
        }

    }
}
