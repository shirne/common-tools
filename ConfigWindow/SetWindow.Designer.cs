
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetWindow));
            this.configList = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.SetWindowBtn = new System.Windows.Forms.Button();
            this.displayList = new System.Windows.Forms.TreeView();
            this.displayListBtn = new System.Windows.Forms.Button();
            this.windowList = new System.Windows.Forms.TreeView();
            this.windowListBtn = new System.Windows.Forms.Button();
            this.reloadBtn = new System.Windows.Forms.Button();
            this.displayListExpand = new System.Windows.Forms.Button();
            this.windowListExpand = new System.Windows.Forms.Button();
            this.configListExpand = new System.Windows.Forms.Button();
            this.addConfigBtn = new System.Windows.Forms.Button();
            this.saveConfigBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // configList
            // 
            this.configList.Location = new System.Drawing.Point(42, 72);
            this.configList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.configList.Name = "configList";
            this.configList.Size = new System.Drawing.Size(356, 540);
            this.configList.TabIndex = 0;
            this.configList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.configList_NodeMouseClick);
            this.configList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.configList_NodeMouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "配置列表";
            // 
            // SetWindowBtn
            // 
            this.SetWindowBtn.Location = new System.Drawing.Point(42, 622);
            this.SetWindowBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SetWindowBtn.Name = "SetWindowBtn";
            this.SetWindowBtn.Size = new System.Drawing.Size(112, 34);
            this.SetWindowBtn.TabIndex = 2;
            this.SetWindowBtn.Text = "应用设置";
            this.SetWindowBtn.UseVisualStyleBackColor = true;
            this.SetWindowBtn.Click += new System.EventHandler(this.SetWindowBtn_Click);
            // 
            // displayList
            // 
            this.displayList.Location = new System.Drawing.Point(410, 72);
            this.displayList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.displayList.Name = "displayList";
            this.displayList.Size = new System.Drawing.Size(360, 540);
            this.displayList.TabIndex = 3;
            // 
            // displayListBtn
            // 
            this.displayListBtn.Location = new System.Drawing.Point(410, 28);
            this.displayListBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.displayListBtn.Name = "displayListBtn";
            this.displayListBtn.Size = new System.Drawing.Size(152, 34);
            this.displayListBtn.TabIndex = 4;
            this.displayListBtn.Text = "显示器";
            this.displayListBtn.UseVisualStyleBackColor = true;
            this.displayListBtn.Click += new System.EventHandler(this.displayListBtn_Click);
            // 
            // windowList
            // 
            this.windowList.Location = new System.Drawing.Point(780, 72);
            this.windowList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.windowList.Name = "windowList";
            this.windowList.Size = new System.Drawing.Size(380, 540);
            this.windowList.TabIndex = 5;
            this.windowList.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.windowList_AfterExpand);
            this.windowList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.windowList_NodeMouseDoubleClick);
            // 
            // windowListBtn
            // 
            this.windowListBtn.Location = new System.Drawing.Point(780, 28);
            this.windowListBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.windowListBtn.Name = "windowListBtn";
            this.windowListBtn.Size = new System.Drawing.Size(112, 34);
            this.windowListBtn.TabIndex = 6;
            this.windowListBtn.Text = "窗口列表";
            this.windowListBtn.UseVisualStyleBackColor = true;
            this.windowListBtn.Click += new System.EventHandler(this.windowListBtn_Click);
            // 
            // reloadBtn
            // 
            this.reloadBtn.Location = new System.Drawing.Point(128, 28);
            this.reloadBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.reloadBtn.Name = "reloadBtn";
            this.reloadBtn.Size = new System.Drawing.Size(112, 34);
            this.reloadBtn.TabIndex = 7;
            this.reloadBtn.Text = "重新加载配置";
            this.reloadBtn.UseVisualStyleBackColor = true;
            this.reloadBtn.Click += new System.EventHandler(this.reloadBtn_Click);
            // 
            // displayListExpand
            // 
            this.displayListExpand.Location = new System.Drawing.Point(658, 28);
            this.displayListExpand.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.displayListExpand.Name = "displayListExpand";
            this.displayListExpand.Size = new System.Drawing.Size(112, 34);
            this.displayListExpand.TabIndex = 8;
            this.displayListExpand.Text = "折叠全部";
            this.displayListExpand.UseVisualStyleBackColor = true;
            this.displayListExpand.Click += new System.EventHandler(this.displayListExpand_Click);
            // 
            // windowListExpand
            // 
            this.windowListExpand.Location = new System.Drawing.Point(1050, 28);
            this.windowListExpand.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.windowListExpand.Name = "windowListExpand";
            this.windowListExpand.Size = new System.Drawing.Size(112, 34);
            this.windowListExpand.TabIndex = 9;
            this.windowListExpand.Text = "展开全部";
            this.windowListExpand.UseVisualStyleBackColor = true;
            this.windowListExpand.Click += new System.EventHandler(this.windowListExpand_Click);
            // 
            // configListExpand
            // 
            this.configListExpand.Location = new System.Drawing.Point(288, 28);
            this.configListExpand.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.configListExpand.Name = "configListExpand";
            this.configListExpand.Size = new System.Drawing.Size(112, 34);
            this.configListExpand.TabIndex = 10;
            this.configListExpand.Text = "展开全部";
            this.configListExpand.UseVisualStyleBackColor = true;
            this.configListExpand.Click += new System.EventHandler(this.configListExpand_Click);
            // 
            // addConfigBtn
            // 
            this.addConfigBtn.Location = new System.Drawing.Point(164, 622);
            this.addConfigBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addConfigBtn.Name = "addConfigBtn";
            this.addConfigBtn.Size = new System.Drawing.Size(112, 34);
            this.addConfigBtn.TabIndex = 11;
            this.addConfigBtn.Text = "添加配置";
            this.addConfigBtn.UseVisualStyleBackColor = true;
            this.addConfigBtn.Click += new System.EventHandler(this.addConfigBtn_Click);
            // 
            // saveConfigBtn
            // 
            this.saveConfigBtn.Location = new System.Drawing.Point(285, 622);
            this.saveConfigBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveConfigBtn.Name = "saveConfigBtn";
            this.saveConfigBtn.Size = new System.Drawing.Size(112, 34);
            this.saveConfigBtn.TabIndex = 12;
            this.saveConfigBtn.Text = "保存配置";
            this.saveConfigBtn.UseVisualStyleBackColor = true;
            this.saveConfigBtn.Click += new System.EventHandler(this.saveConfigBtn_Click);
            // 
            // SetWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.saveConfigBtn);
            this.Controls.Add(this.addConfigBtn);
            this.Controls.Add(this.configListExpand);
            this.Controls.Add(this.windowListExpand);
            this.Controls.Add(this.displayListExpand);
            this.Controls.Add(this.reloadBtn);
            this.Controls.Add(this.windowListBtn);
            this.Controls.Add(this.windowList);
            this.Controls.Add(this.displayListBtn);
            this.Controls.Add(this.displayList);
            this.Controls.Add(this.SetWindowBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.configList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SetWindow";
            this.Text = "SetWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView configList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SetWindowBtn;
        private System.Windows.Forms.TreeView displayList;
        private System.Windows.Forms.Button displayListBtn;
        private System.Windows.Forms.TreeView windowList;
        private System.Windows.Forms.Button windowListBtn;
        private System.Windows.Forms.Button reloadBtn;
        private System.Windows.Forms.Button displayListExpand;
        private System.Windows.Forms.Button windowListExpand;
        private System.Windows.Forms.Button configListExpand;
        private System.Windows.Forms.Button addConfigBtn;
        private System.Windows.Forms.Button saveConfigBtn;
    }
}

