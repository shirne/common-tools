using NativeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ConfigWindow
{
    public partial class SetWindow : Form
    {
        XmlConfig config;

        string[] args;

        Dictionary<String, long> windowStyles;
        public SetWindow(string[] args)
        {
            this.args = args;
            InitializeComponent();
            loadConfigs();
            foreach (string arg in args)
            {
                if ("--apply".Equals(arg))
                {
                    DoSetWindow();
                }
            }
        }
        private void loadConfigs()
        {
            config = Utils.loadConfig();
            showConfig();
        }

        private void showConfig()
        {
            configList.Nodes.Clear();
            int count = 0;
            foreach (ItemConfig configItem in config.items)
            {

                var treeNode = new TreeNode(configItem.processName + "-" + configItem.left + "," + configItem.top + "/" + configItem.width + "," + configItem.height);
                treeNode.Tag = count;
                addItemProps(treeNode, configItem);

                addDelNode(treeNode, count);
                configList.Nodes.Add(treeNode);
                count++;
            }
        }

        private void addItemProps(TreeNode treeNode, ItemConfig itemConfig)
        {
            treeNode.Nodes.Add(new TreeNode("ProcessName:" + itemConfig.processName));
            treeNode.Nodes.Add(new TreeNode("Left:" + itemConfig.left));
            treeNode.Nodes.Add(new TreeNode("Top:" + itemConfig.top));
            treeNode.Nodes.Add(new TreeNode("Width:" + itemConfig.width));
            treeNode.Nodes.Add(new TreeNode("Height:" + itemConfig.height));
        }

        private void addDelNode(TreeNode treeNode, int index)
        {
            var delNode = new TreeNode("删除");
            delNode.Tag = "del-" + index;
            treeNode.Nodes.Add(delNode);
        }


        private void SetWindowBtn_Click(object sender, EventArgs e)
        {
            DoSetWindow();
        }
        private void DoSetWindow()
        {
            var count = 0;
            foreach (ItemConfig item in config.items)
            {
                var wins = Utils.findWindows(item.processName);

                if (wins == null)
                {
                    configList.Nodes[count].Text = item.processName + "-" + "not found";
                }
                else
                {
                    if (wins.Count < 1)
                    {
                        configList.Nodes[count].Text = item.processName + "-" + "not found any windows";
                    }
                    else if (wins.Count > 1)
                    {
                        configList.Nodes[count].Text = item.processName + "-" + " found " + wins.Count + " windows";
                        foreach (IntPtr win in wins)
                        {
                            if (User32.MoveWindow(win, item.left, item.top, item.width, item.height, 1))
                            {
                                WINDOWINFO info = new WINDOWINFO();
                                info.cbSize = (uint)Marshal.SizeOf(info);
                                bool isInfoget = User32.GetWindowInfo(win, ref info);

                                //treeNode.Nodes.Add( User32.GetWindowTitle(win) + "-" + "positioned" + "/" + getRect(info.rcWindow));
                            }
                        }
                    }
                    else
                    {
                        if (User32.MoveWindow(wins[0], item.left, item.top, item.width, item.height, 1))
                        {
                            WINDOWINFO info = new WINDOWINFO();
                            info.cbSize = (uint)Marshal.SizeOf(info);
                            bool isInfoget = User32.GetWindowInfo(wins[0], ref info);

                            configList.Nodes[count].Text = item.processName + "-" + "positioned" + "/" + getRect(info.rcWindow);
                        }
                    }
                }

                count++;
            }
        }

        private string getRect(RECT rect)
        {
            return rect.Left + "," + rect.Top + "/" + (rect.Right - rect.Left) + "," + (rect.Bottom - rect.Top);
        }

        private TreeNode rectNode(RECT rect, String prefix)
        {
            var node = new TreeNode(prefix + ":" + getRect(rect));
            node.Nodes.Add(new TreeNode("Left:" + rect.Left));
            node.Nodes.Add(new TreeNode("Top:" + rect.Top));
            node.Nodes.Add(new TreeNode("Right:" + rect.Right));
            node.Nodes.Add(new TreeNode("Bottom:" + rect.Bottom));
            return node;
        }
        private TreeNode styleNode(uint style, String prefix)
        {
            var node = new TreeNode(prefix + ": 0x" + style.ToString("X"));
            if (windowStyles == null)
            {
                windowStyles = Utils.WindowStyles();
            }
            foreach (var keyvalue in windowStyles)
            {
                if ((style & keyvalue.Value) == keyvalue.Value)
                {
                    node.Nodes.Add(new TreeNode(keyvalue.Key));
                }
            }
            return node;
        }

        private TreeNode processNode(int processId)
        {
            var node = new TreeNode("Process: " + processId);
            node.Tag = processId;
            node.Nodes.Add("...");
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

            WINDOWINFO info = User32.GetWindowInfo(win);

            string title = User32.GetWindowTitle(win);

            uint processId = 0;
            User32.GetWindowThreadProcessId(win, out processId);

            StringBuilder className = new StringBuilder(100);
            User32.GetClassName(win, className, 100);

            // var process = Process.GetProcessById(Convert.ToInt32(processId));
            // "-" + process.ProcessName +
            var node = new TreeNode(info.atomWindowType.ToString() +  "-" + title);
            node.Tag = win;
            node.Nodes.Add("Status:" + info.dwWindowStatus.ToString());
            node.Nodes.Add(processNode(Convert.ToInt32(processId)));
            node.Nodes.Add("ClassName:" + className.ToString());
            node.Nodes.Add(styleNode(info.dwStyle, "Style"));
            node.Nodes.Add(rectNode(info.rcClient, "ClientRect"));
            node.Nodes.Add(rectNode(info.rcClient, "WindowRect"));
            node.Nodes.Add("Border:" + info.cxWindowBorders + "," + info.cyWindowBorders);

            /*var subWindows = User32.EnumWindows(process.Id);
            if(subWindows.Count>0)
            {
                var subNode = new TreeNode("SubWindows(" + subWindows.Count + ")");
                node.Nodes.Add(subNode);
                *//*foreach(var window in subWindows)
                {
                    subNode.Nodes.Add(windowNo)
                }*//*
            }*/

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
                var node = new TreeNode((screen.Primary ? "Primary - " : "") + screen.DeviceName);
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
            foreach (TreeNode node in displayList.Nodes)
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

        private void addConfigBtn_Click(object sender, EventArgs e)
        {
            var add = new Add();
            add.index = -1;
            add.left = 0;
            add.top = 0;
            add.width = 800;
            add.height = 600;
            add.ShowDialog(this);
        }

        public void AddConfig(Add add)
        {
            var item = new ItemConfig()
            {
                processName = add.processName,
                left = add.left,
                top = add.top,
                width = add.width,
                height = add.height,
            };
            if (add.index > -1 && add.index < config.items.Count)
            {
                config.items[add.index] = item;
            }
            else
            {
                config.items.Add(item);
            }
            Utils.saveConfig(config);
            showConfig();
        }

        private void saveConfigBtn_Click(object sender, EventArgs e)
        {
            Utils.saveConfig(config);
        }

        private void configList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var index = e.Node.Tag;
            if (index == null)
            {
                index = e.Node.Parent.Tag;
            }
            if (index != null)
            {
                var add = new Add();
                add.index = (int)index;
                var item = config.items[add.index];
                add.processName = item.processName;
                add.left = item.left;
                add.top = item.top;
                add.width = item.width;
                add.height = item.height;
                add.ShowDialog(this);
            }
        }

        private void configList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var action = e.Node.Tag;
            if (action != null)
            {
                string[] parts = action.ToString().Split('-');
                if (parts.Length > 1)
                {
                    int index = -1;
                    if (int.TryParse(parts[1], out index))
                    {
                        if (index < 0 || index > config.items.Count)
                        {
                            MessageBox.Show("序号错误", "提示", MessageBoxButtons.OK);
                            showConfig();
                            return;
                        }

                        if (parts[0] == "del")
                        {
                            var result = MessageBox.Show("确定删除 " + config.items[index].processName, "提示", MessageBoxButtons.OKCancel);
                            if (result == DialogResult.OK)
                            {
                                config.items.RemoveAt(index);
                                Utils.saveConfig(config);
                                showConfig();
                            }
                        }
                    }
                }
            }
        }

        private void windowList_AfterExpand(object sender, TreeViewEventArgs e)
        {
            var tag = e.Node.Tag;
            if (tag is int && e.Node.Nodes.Count == 1)
            {
                var process = Process.GetProcessById((int)tag);
                e.Node.Nodes.Clear();
                e.Node.Nodes.Add("ProcessId:" + tag.ToString());
                e.Node.Nodes.Add("ProcessName:" + process.ProcessName);
                e.Node.Nodes.Add("Priority:" + process.BasePriority);
                e.Node.Nodes.Add("MachineName:" + process.MachineName);
            }
        }

        private void windowList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node;
            var tag = node.Tag;
            while (node.Parent != null && !(tag is IntPtr))
            {
                node = node.Parent;
                tag = node.Tag;
            }
            if (tag == null) return;
            if (tag is IntPtr)
            {
                var processId = User32.GetWindowThreadProcessId((IntPtr)tag);
                if (processId > 0)
                {
                    var process = Process.GetProcessById(processId);
                    var add = new Add();
                    add.index = config.items.FindIndex((config) => config.processName == process.ProcessName);
                    var winInfo = User32.GetWindowInfo((IntPtr)tag);
                    add.processName = process.ProcessName;
                    add.left = winInfo.rcClient.Left;
                    add.top = winInfo.rcClient.Top;
                    add.width = winInfo.rcClient.Right - winInfo.rcClient.Left;
                    add.height = winInfo.rcClient.Bottom - winInfo.rcClient.Top;
                    add.ShowDialog(this);
                }
            }
        }
    }
}
