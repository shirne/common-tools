
namespace ConfigWindow
{
    partial class SetWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.configList = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.SetWindowBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // configList
            // 
            this.configList.Location = new System.Drawing.Point(28, 48);
            this.configList.Name = "configList";
            this.configList.Size = new System.Drawing.Size(282, 376);
            this.configList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "配置列表";
            // 
            // SetWindowBtn
            // 
            this.SetWindowBtn.Location = new System.Drawing.Point(235, 19);
            this.SetWindowBtn.Name = "SetWindowBtn";
            this.SetWindowBtn.Size = new System.Drawing.Size(75, 23);
            this.SetWindowBtn.TabIndex = 2;
            this.SetWindowBtn.Text = "设置窗口";
            this.SetWindowBtn.UseVisualStyleBackColor = true;
            this.SetWindowBtn.Click += new System.EventHandler(this.SetWindowBtn_Click);
            // 
            // SetWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SetWindowBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.configList);
            this.Name = "SetWindow";
            this.Text = "SetWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView configList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SetWindowBtn;
    }
}

