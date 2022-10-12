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

        string[] args;
        public SetWindow(string[] args)
        {
            this.args = args;
            InitializeComponent();
            loadConfigs();
            foreach(string arg in args)
            {
                if("--apply".Equals(arg))
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


        private void SetWindowBtn_Click(object sender, EventArgs e)
        {
            DoSetWindow();
        }
        private void DoSetWindow()
        {
            configList.Nodes.Clear();
            foreach (ItemConfig item in config.items)
            {
                var wins = Utils.findWindows(item.processName);

                var treeNode = new TreeNode(item.processName + "-" + item.left + "," + item.top + "/" + item.width + "," + item.height);

                addItemProps(treeNode, item);
                if (wins == null)
                {
                    treeNode.Text = item.processName + "-" + "not found";
                }
                else
                {
                    if (wins.Count < 1)
                    {
                        treeNode.Text = item.processName + "-" + "not found any windows";
                    }
                    else if (wins.Count > 1)
                    {
                        treeNode.Text = item.processName + "-" + " found "+ wins.Count + " windows";
                        foreach(IntPtr win in wins)
                        {
                            if (User32.MoveWindow(win, item.left, item.top, item.width, item.height, 1))
                            {
                                WINDOWINFO info = new WINDOWINFO();
                                info.cbSize = (uint)Marshal.SizeOf(info);
                                bool isInfoget = User32.GetWindowInfo(win, ref info);

                                treeNode.Nodes.Add( User32.GetWindowTitle(win) + "-" + "positioned" + "/" + getRect(info.rcWindow));
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

                            treeNode.Text = item.processName + "-" + "positioned" + "/" + getRect(info.rcWindow);
                        }
                    }
                }

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
            
            string title = User32.GetWindowTitle(win);

            uint processId = 0;
            User32.GetWindowThreadProcessId(win, out processId);

            StringBuilder className = new StringBuilder(100);
            User32.GetClassName(win, className, 100);

            var node = new TreeNode(Convert.ToInt64(info.atomWindowType).ToString() + "-" + title);
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
            if(index != null)
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
    }
}
