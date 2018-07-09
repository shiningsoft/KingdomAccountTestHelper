namespace Request文件生成工具
{
    partial class Form1
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
            this.btnPreProccess = new System.Windows.Forms.Button();
            this.tbRaw = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPreProccess
            // 
            this.btnPreProccess.Location = new System.Drawing.Point(12, 10);
            this.btnPreProccess.Name = "btnPreProccess";
            this.btnPreProccess.Size = new System.Drawing.Size(94, 23);
            this.btnPreProccess.TabIndex = 12;
            this.btnPreProccess.Text = "开始";
            this.btnPreProccess.UseVisualStyleBackColor = true;
            this.btnPreProccess.Click += new System.EventHandler(this.btnProccess_Click);
            // 
            // tbRaw
            // 
            this.tbRaw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRaw.Location = new System.Drawing.Point(12, 39);
            this.tbRaw.MaxLength = 999999;
            this.tbRaw.Multiline = true;
            this.tbRaw.Name = "tbRaw";
            this.tbRaw.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbRaw.Size = new System.Drawing.Size(1137, 657);
            this.tbRaw.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(172, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "检查重复节点";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 708);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnPreProccess);
            this.Controls.Add(this.tbRaw);
            this.Name = "Form1";
            this.Text = "Request文件生成工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPreProccess;
        private System.Windows.Forms.TextBox tbRaw;
        private System.Windows.Forms.Button button1;
    }
}

