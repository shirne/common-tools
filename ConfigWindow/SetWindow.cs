using NativeLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
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
            configList.Nodes.Clear();
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

                var treeNode = new TreeNode(configItem.processName + "-" + configItem.left + "," + configItem.top + "/" + configItem.width + "," + configItem.height);
                addItemProps(treeNode, configItem);
                configList.Nodes.Add(treeNode);
            }


        }

        private void addItemProps(TreeNode treeNode, ItemConfig itemConfig)
        {
            treeNode.Nodes.Add(new TreeNode("Left:" + itemConfig.left));
            treeNode.Nodes.Add(new TreeNode("Top:" + itemConfig.top));
            treeNode.Nodes.Add(new TreeNode("Width:" + itemConfig.width));
            treeNode.Nodes.Add(new TreeNode("Height:" + itemConfig.height));
        }


        private void SetWindowBtn_Click(object sender, EventArgs e)
        {
            configList.Nodes.Clear();
            foreach (ItemConfig item in config.items)
            {
                Process[] processes = Process.GetProcessesByName(item.processName);
                bool seted = false;
                var treeNode = new TreeNode(item.processName + "-" + item.left + "," + item.top + "/" + item.width + "," + item.height);
                foreach (Process proc in processes)
                {
                    if (User32.MoveWindow(proc.MainWindowHandle, item.left, item.top, item.width, item.height, 1))
                    {
                        //User32.SetForegroundWindow(proc.MainWindowHandle);

                        WINDOWINFO info = new WINDOWINFO();
                        info.cbSize = (uint)Marshal.SizeOf(info);
                        bool isInfoget = User32.GetWindowInfo(proc.MainWindowHandle, ref info);

                        treeNode.Text = item.processName + "-" + "positioned" + "/" + getRect(info.rcWindow);
                        
                        seted = true;
                    }

                }
                if (!seted)
                {
                    treeNode.Text = item.processName + "-" + "not found";
                }

                addItemProps(treeNode, item);
                configList.Nodes.Add(treeNode);
            }
        }

        private string getRect(RECT rect)
        {
            return rect.Left + "," + rect.Top + "/" + (rect.Right-rect.Left) + "," + (rect.Bottom-rect.Top);
        }

        private TreeNode rectNode(RECT rect, String prefix)
        {
            var node = new TreeNode(prefix+":"+getRect(rect));
            node.Nodes.Add(new TreeNode("Left:" + rect.Left));
            node.Nodes.Add(new TreeNode("Top:" + rect.Top));
            node.Nodes.Add(new TreeNode("Right:" + rect.Right));
            node.Nodes.Add(new TreeNode("Bottom:" + rect.Bottom));
            return node;
        }


        private void windowListBtn_Click(object sender, EventArgs e)
        {
            windowList.Nodes.Clear();
            if (inVisibleWindows != null)
            {
                inVisibleWindows.Nodes.Clear();
            }
            inVisibleWindows = new TreeNode("InVisible Windows");
            //LabelStatus.Text = GetWindows().Count.ToString();
            EnumWindowsProc ewp = new EnumWindowsProc(EnumWindows);
            User32.EnumWindows(ewp, 0);
            windowList.Nodes.Add(inVisibleWindows);
        }

        TreeNode inVisibleWindows;
        private bool EnumWindows(IntPtr win, int lParam)
        {
           
            WINDOWINFO info = new WINDOWINFO();
            info.cbSize = (uint)Marshal.SizeOf(info);
            bool isInfoget = User32.GetWindowInfo(win, ref info);
            //string title = "00000000000000";
            StringBuilder title = new StringBuilder(User32.GetWindowTextLength
                (win) + 1);
            User32.GetWindowText(win, title, 20);
            //LabelStatus.Text = LabelStatus.Text+"/" + info.rcWindow.Left.ToString()+","+ info.rcWindow.Top.ToString();
            uint processId = 0;
            User32.GetWindowThreadProcessId(win, out processId);

            StringBuilder className = new StringBuilder(100);
            User32.GetClassName(win, className, 100);

            var node = new TreeNode(Convert.ToInt64(info.atomWindowType).ToString() + "-" + title.ToString());
            node.Nodes.Add("Status:" + info.dwWindowStatus.ToString());
            node.Nodes.Add("ProcessId:" + processId.ToString());
            node.Nodes.Add("ClassName:" + className.ToString());
            node.Nodes.Add(rectNode(info.rcClient,"ClientRect" ));
            node.Nodes.Add(rectNode(info.rcClient, "WindowRect"));

            if (!User32.IsWindowVisible(win))
            {
                inVisibleWindows.Nodes.Add(node);
                return true;
            }
            // node.Expand();

            windowList.Nodes.Add(node);
            return true;
        }

        private void displayListBtn_Click(object sender, EventArgs e)
        {
            displayList.Nodes.Clear();
            foreach (Screen screen in Screen.AllScreens)
            {
                var node = new TreeNode((screen.Primary?"Primary - ":"")  + screen.DeviceName);
                node.Nodes.Add("X:" + screen.Bounds.X);
                node.Nodes.Add("Y:" + screen.Bounds.Y);
                node.Nodes.Add("Width:" + screen.Bounds.Width);
                node.Nodes.Add("Height:" + screen.Bounds.Height);
                node.Expand();
                displayList.Nodes.Add(node);
            }
        }

        private void reloadBtn_Click(object sender, EventArgs e)
        {
            loadConfigs();
        }

        private void displayListExpand_Click(object sender, EventArgs e)
        {
            bool isExpand = displayListExpand.Text.Contains("展开");
            foreach(TreeNode node in displayList.Nodes)
            {
                if (isExpand)
                {
                    node.Expand();
                }
                else
                {
                    node.Collapse(false);
                }
            }
            displayListExpand.Text = isExpand ? "折叠全部" : "展开全部";
        }

        private void windowListExpand_Click(object sender, EventArgs e)
        {
            bool isExpand = windowListExpand.Text.Contains("展开");
            foreach (TreeNode node in windowList.Nodes)
            {

                if (isExpand)
                {
                    node.Expand();
                }
                else
                {
                    node.Collapse(true);
                }
            }
            windowListExpand.Text = isExpand ? "折叠全部" : "展开全部";
        }

        private void configListExpand_Click(object sender, EventArgs e)
        {
            bool isExpand = configListExpand.Text.Contains("展开");
            foreach (TreeNode node in configList.Nodes)
            {

                if (isExpand)
                {
                    node.Expand();
                }
                else
                {
                    node.Collapse(true);
                }
            }
            configListExpand.Text = isExpand ? "折叠全部" : "展开全部";
        }
    }
}
