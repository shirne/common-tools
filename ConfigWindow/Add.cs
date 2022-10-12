using NativeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigWindow
{
    public partial class Add : Form
    {
        public int index = -1;
        public string processName;
        public int left;
        public int top;
        public int width;
        public int height;

        public Add()
        {
            InitializeComponent();
        }

        public void fromItem(ItemConfig item, int index=-1)
        {
            this.index = index;
            this.processName = item.processName;
            this.left = item.left;
            this.top = item.top;
            this.width = item.width;
            this.height = item.height;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {

            if (GetInput())
            {
                var setWindow = (SetWindow)this.Owner;
                setWindow.AddConfig(this);
                this.Close();
            }
        }

        private bool GetInput()
        {
            errorLabel.Text = "";
            bool isFocused = false;
            processName = processNameInput.Text.Trim();
            if (string.IsNullOrEmpty(processName))
            {
                errorLabel.Text = "请填写进程名";
                processNameInput.Focus();
                isFocused = true;
            }
            if (!int.TryParse(leftInput.Text, out left))
            {
                if (!isFocused)
                {
                    errorLabel.Text = "左上X请输入数字";
                    leftInput.Focus();
                    isFocused = true;
                }
            }
            if (!int.TryParse(topInput.Text, out top))
            {
                if (!isFocused)
                {
                    errorLabel.Text = "左上Y请输入数字";
                    topInput.Focus();
                    isFocused = true;
                }
            }
            if (!int.TryParse(widthInput.Text, out width))
            {
                if (!isFocused)
                {
                    errorLabel.Text = "宽度请输入数字";
                    widthInput.Focus();
                    isFocused = true;
                }
            }
            if (!int.TryParse(heightInput.Text, out height))
            {
                if (!isFocused)
                {
                    errorLabel.Text = "高度请输入数字";
                    heightInput.Focus();
                    isFocused = true;
                }
            }
            return !isFocused;
        }

        private void Add_Shown(object sender, EventArgs e)
        {
            processNameInput.Text = processName;
            leftInput.Text = left.ToString();
            topInput.Text = top.ToString();
            widthInput.Text = width.ToString();
            heightInput.Text = height.ToString();
            processNameInput.Focus();
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            var name = processNameInput.Text.Trim();
            Process[] processes = Process.GetProcessesByName(name);
            if (processes.Length < 1)
            {
                queryLabel.Text = "未查询到进程";
            }
            else
            {
                foreach (Process proc in processes)
                {
                    if (User32.IsWindow(proc.MainWindowHandle))
                    {
                        StringBuilder title = new StringBuilder(User32.GetWindowTextLength
            (proc.MainWindowHandle) + 1);
                        User32.GetWindowText(proc.MainWindowHandle, title, 20);
                        queryLabel.Text = "查询到窗口\"" + title.ToString() + "\"" + (User32.IsWindowVisible(proc.MainWindowHandle) ? "已显示" : "未显示");
                    }
                    else
                    {
                        queryLabel.Text = "未查询到窗口";
                    }
                }
            }
        }

        private void testBtn_Click(object sender, EventArgs e)
        {
            if (GetInput())
            {
                Process[] processes = Process.GetProcessesByName(processName);
                if (processes.Length < 1)
                {
                    errorLabel.Text = "未查询到进程";
                }
                else
                {
                    foreach (Process proc in processes)
                    {
                        if (User32.IsWindow(proc.MainWindowHandle))
                        {
                            StringBuilder title = new StringBuilder(User32.GetWindowTextLength
                (proc.MainWindowHandle) + 1);
                            User32.GetWindowText(proc.MainWindowHandle, title, 20);
                            var isVisible = User32.IsWindowVisible(proc.MainWindowHandle);

                            var moved = User32.MoveWindow(proc.MainWindowHandle, left, top, width, height, 1);
                            errorLabel.Text = "查询到窗口\"" + title.ToString() + "\"" + (isVisible ? "已显示" : "未显示") + (moved ? "已设置" : "未设置");
                        }
                        else
                        {
                            errorLabel.Text = "未查询到窗口";
                        }
                    }
                }
            }
        }
    }
}
