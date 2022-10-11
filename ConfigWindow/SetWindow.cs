using NativeLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace ConfigWindow
{
    public partial class SetWindow : Form
    {
        XmlConfig config;
        string configFile = "config.xml";
        public SetWindow()
        {
            InitializeComponent();
            configFile = Directory.GetCurrentDirectory() + "/" + configFile;
            loadConfigs();
        }
        private void loadConfigs()
        {
            var doc = new XmlDocument();
            if (!File.Exists(configFile))
            {
                doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><configs><item><processName>QQ</processName><left>0</left><top>0</top><width>800</width><height>600</height></item></configs>");
                using (XmlWriter writer = XmlWriter.Create(configFile))
                {
                    doc.WriteTo(writer);
                }
            }
            else
            {
                doc.Load(configFile);
            }
            config = new XmlConfig();
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
                configList.Nodes.Add(configItem.processName + "-" + configItem.left + "," + configItem.top + "/" + configItem.width + "," + configItem.height);
            }


        }


        private void SetWindowBtn_Click(object sender, EventArgs e)
        {
            configList.Nodes.Clear();
            foreach (ItemConfig item in config.items)
            {
                Process[] processes = Process.GetProcessesByName(item.processName);
                bool seted = false;
                foreach (Process proc in processes)
                {
                    if (User32.MoveWindow(proc.MainWindowHandle, item.left, item.top, item.width, item.height, 1))
                    {
                        //User32.SetForegroundWindow(proc.MainWindowHandle);

                        WINDOWINFO info = new WINDOWINFO();
                        info.cbSize = (uint)Marshal.SizeOf(info);
                        bool isInfoget = User32.GetWindowInfo(proc.MainWindowHandle, ref info);

                        configList.Nodes.Add(proc.ProcessName + "-" + "positioned" + "/" + getRect(info.rcWindow));
                        seted = true;
                    }

                }
                if (!seted)
                {
                    configList.Nodes.Add(item.processName + "-" + "not found");
                }
            }
        }

        private string getRect(RECT rect)
        {
            return rect.Left + "," + rect.Top + "/" + rect.Right + "," + rect.Bottom;
        }
    }
}
