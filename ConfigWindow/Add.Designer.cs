
namespace ConfigWindow
{
    partial class Add
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.processNameInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.leftInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.topInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.widthInput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.heightInput = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.errorLabel = new System.Windows.Forms.Label();
            this.queryBtn = new System.Windows.Forms.Button();
            this.queryLabel = new System.Windows.Forms.Label();
            this.testBtn = new System.Windows.Forms.Button();
            this.loggerBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 68);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "进程名";
            // 
            // processNameInput
            // 
            this.processNameInput.Location = new System.Drawing.Point(218, 63);
            this.processNameInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.processNameInput.Name = "processNameInput";
            this.processNameInput.Size = new System.Drawing.Size(256, 28);
            this.processNameInput.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 160);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "左上X";
            // 
            // leftInput
            // 
            this.leftInput.Location = new System.Drawing.Point(218, 156);
            this.leftInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.leftInput.Name = "leftInput";
            this.leftInput.Size = new System.Drawing.Size(256, 28);
            this.leftInput.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(111, 226);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "左上Y";
            // 
            // topInput
            // 
            this.topInput.Location = new System.Drawing.Point(218, 222);
            this.topInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.topInput.Name = "topInput";
            this.topInput.Size = new System.Drawing.Size(256, 28);
            this.topInput.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 300);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "窗口宽度";
            // 
            // widthInput
            // 
            this.widthInput.Location = new System.Drawing.Point(218, 296);
            this.widthInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.widthInput.Name = "widthInput";
            this.widthInput.Size = new System.Drawing.Size(256, 28);
            this.widthInput.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(111, 364);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "窗口高度";
            // 
            // heightInput
            // 
            this.heightInput.Location = new System.Drawing.Point(218, 360);
            this.heightInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.heightInput.Name = "heightInput";
            this.heightInput.Size = new System.Drawing.Size(256, 28);
            this.heightInput.TabIndex = 1;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(237, 423);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(238, 34);
            this.saveBtn.TabIndex = 2;
            this.saveBtn.Text = "保存";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.errorLabel.Location = new System.Drawing.Point(64, 478);
            this.errorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(0, 18);
            this.errorLabel.TabIndex = 3;
            // 
            // queryBtn
            // 
            this.queryBtn.Location = new System.Drawing.Point(218, 105);
            this.queryBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.queryBtn.Name = "queryBtn";
            this.queryBtn.Size = new System.Drawing.Size(112, 34);
            this.queryBtn.TabIndex = 4;
            this.queryBtn.Text = "查询";
            this.queryBtn.UseVisualStyleBackColor = true;
            this.queryBtn.Click += new System.EventHandler(this.queryBtn_Click);
            // 
            // queryLabel
            // 
            this.queryLabel.AutoSize = true;
            this.queryLabel.Location = new System.Drawing.Point(339, 112);
            this.queryLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.queryLabel.Name = "queryLabel";
            this.queryLabel.Size = new System.Drawing.Size(0, 18);
            this.queryLabel.TabIndex = 5;
            // 
            // testBtn
            // 
            this.testBtn.Location = new System.Drawing.Point(114, 423);
            this.testBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(112, 34);
            this.testBtn.TabIndex = 6;
            this.testBtn.Text = "测试";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Click += new System.EventHandler(this.testBtn_Click);
            // 
            // loggerBox
            // 
            this.loggerBox.Location = new System.Drawing.Point(535, 63);
            this.loggerBox.Name = "loggerBox";
            this.loggerBox.ReadOnly = true;
            this.loggerBox.Size = new System.Drawing.Size(423, 456);
            this.loggerBox.TabIndex = 7;
            this.loggerBox.Text = "";
            // 
            // Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 596);
            this.Controls.Add(this.loggerBox);
            this.Controls.Add(this.testBtn);
            this.Controls.Add(this.queryLabel);
            this.Controls.Add(this.queryBtn);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.heightInput);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.widthInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.topInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.leftInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.processNameInput);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Add";
            this.Text = "Add";
            this.Shown += new System.EventHandler(this.Add_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox processNameInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox leftInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox topInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox widthInput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox heightInput;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Button queryBtn;
        private System.Windows.Forms.Label queryLabel;
        private System.Windows.Forms.Button testBtn;
        private System.Windows.Forms.RichTextBox loggerBox;
    }
}