using NativeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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

        public void fromItem(ItemConfig item, int index = -1)
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
            User32.SetLastError(0);
            var name = processNameInput.Text.Trim();
            var wins = Utils.findWindows(name);
            if (wins == null)
            {
                loggerBox.AppendText("未查询到进程\n");
                return;
            }

            if (wins.Count < 1)
            {
                loggerBox.AppendText("未查询到窗口\n");
            }
            else
            {
                if (wins.Count > 1)
                {
                    loggerBox.AppendText("查询到多个窗口\n");
                    foreach (IntPtr win in wins)
                    {
                        string title = User32.GetWindowTitle(win);

                        loggerBox.AppendText(title + "[" + (User32.IsWindowVisible(win) ? "已显示" : "未显示") + "]\n");
                    }
                }
                else
                {
                    string title = User32.GetWindowTitle(wins[0]);
                    loggerBox.AppendText("查询到窗口" + title + "[" + (User32.IsWindowVisible(wins[0]) ? "已显示" : "未显示") + "]\n");
                }
            }
            var error = Marshal.GetLastWin32Error();
            if (error != 0)
            {
                string errorMessage = new Win32Exception(error).Message;
                loggerBox.AppendText("发生错误：" + error.ToString() + ":" + errorMessage + "\n");
            }
        }

        private void testBtn_Click(object sender, EventArgs e)
        {
            if (GetInput())
            {
                User32.SetLastError(0);
                var wins = Utils.findWindows(processName);
                if (wins == null)
                {
                    loggerBox.AppendText("未查询到进程\n");
                    return;
                }
                if (wins.Count < 1)
                {
                    loggerBox.AppendText("未查询到窗口\n");
                }
                else
                {
                    if (wins.Count > 1)
                    {
                        loggerBox.AppendText("查询到多个窗口\n");
                        foreach (IntPtr win in wins)
                        {
                            string stitle = User32.GetWindowTitle(win);

                            var isVisible = User32.IsWindowVisible(win);

                            var moved = User32.MoveWindow(win, left, top, width, height, 1);

                            loggerBox.AppendText(stitle + "[" + (isVisible ? "已显示" : "未显示") + "][" + (moved ? "已设置" : "未设置") + "]\n");
                        }
                    }
                    else
                    {
                        string title = User32.GetWindowTitle(wins[0]);
                        var isVisible = User32.IsWindowVisible(wins[0]);
                        var wininfo = User32.GetWindowInfo(wins[0]);
                        var oldPosition = wininfo.rcClient.Top + "," + wininfo.rcClient.Left + "," + (wininfo.rcClient.Right - wininfo.rcClient.Left) + "," + (wininfo.rcClient.Bottom - wininfo.rcClient.Top);
                        var moved = User32.MoveWindow(wins[0], left, top, width, height, 1);

                        loggerBox.AppendText("查询到窗口" + title + "(" + oldPosition + ")[" + (isVisible ? "已显示" : "未显示") + "][" + (moved ? "已设置" : "未设置") + "]\n");
                    }
                    var error = Marshal.GetLastWin32Error();
                    if (error != 0)
                    {
                        string errorMessage = new Win32Exception(error).Message;
                        loggerBox.AppendText("发生错误：" + error.ToString() + ":" + errorMessage + "\n");
                    }

                }
            }
        }
    }
}
