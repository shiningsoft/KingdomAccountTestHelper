namespace 金证统一账户测试账户生成器
{
    partial class frmWebServiceInterfaceTest
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
            this.btnRefreshMethonList = new System.Windows.Forms.Button();
            this.cbxMethonList = new System.Windows.Forms.ComboBox();
            this.tbxResponse = new System.Windows.Forms.TextBox();
            this.tbxRequest = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnLoadRequestXml = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRefreshMethonList
            // 
            this.btnRefreshMethonList.Location = new System.Drawing.Point(562, 10);
            this.btnRefreshMethonList.Name = "btnRefreshMethonList";
            this.btnRefreshMethonList.Size = new System.Drawing.Size(100, 23);
            this.btnRefreshMethonList.TabIndex = 27;
            this.btnRefreshMethonList.Text = "重新加载列表";
            this.btnRefreshMethonList.UseVisualStyleBackColor = true;
            this.btnRefreshMethonList.Click += new System.EventHandler(this.btnRefreshMethonList_Click);
            // 
            // cbxMethonList
            // 
            this.cbxMethonList.FormattingEnabled = true;
            this.cbxMethonList.Location = new System.Drawing.Point(79, 12);
            this.cbxMethonList.Name = "cbxMethonList";
            this.cbxMethonList.Size = new System.Drawing.Size(265, 20);
            this.cbxMethonList.TabIndex = 26;
            this.cbxMethonList.SelectedIndexChanged += new System.EventHandler(this.cbxMethonList_SelectedIndexChanged);
            // 
            // tbxResponse
            // 
            this.tbxResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxResponse.Location = new System.Drawing.Point(3, 3);
            this.tbxResponse.Multiline = true;
            this.tbxResponse.Name = "tbxResponse";
            this.tbxResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxResponse.Size = new System.Drawing.Size(618, 554);
            this.tbxResponse.TabIndex = 25;
            // 
            // tbxRequest
            // 
            this.tbxRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxRequest.Location = new System.Drawing.Point(3, 3);
            this.tbxRequest.Multiline = true;
            this.tbxRequest.Name = "tbxRequest";
            this.tbxRequest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxRequest.Size = new System.Drawing.Size(384, 554);
            this.tbxRequest.TabIndex = 24;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(350, 10);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(100, 23);
            this.btnExecute.TabIndex = 23;
            this.btnExecute.Text = "执行请求";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnLoadRequestXml
            // 
            this.btnLoadRequestXml.Location = new System.Drawing.Point(456, 10);
            this.btnLoadRequestXml.Name = "btnLoadRequestXml";
            this.btnLoadRequestXml.Size = new System.Drawing.Size(100, 23);
            this.btnLoadRequestXml.TabIndex = 21;
            this.btnLoadRequestXml.Text = "打开接口目录";
            this.btnLoadRequestXml.UseVisualStyleBackColor = true;
            this.btnLoadRequestXml.Click += new System.EventHandler(this.btnLoadRequestXml_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(20, 15);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(53, 12);
            this.label26.TabIndex = 22;
            this.label26.Text = "接口名：";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 39);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbxRequest);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbxResponse);
            this.splitContainer1.Size = new System.Drawing.Size(1018, 560);
            this.splitContainer1.SplitterDistance = 390;
            this.splitContainer1.TabIndex = 28;
            // 
            // frmWebServiceInterfaceTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 611);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnRefreshMethonList);
            this.Controls.Add(this.cbxMethonList);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnLoadRequestXml);
            this.Controls.Add(this.label26);
            this.Name = "frmWebServiceInterfaceTest";
            this.Text = "接口测试工具";
            this.Load += new System.EventHandler(this.frmWebServiceInterfaceTest_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefreshMethonList;
        private System.Windows.Forms.ComboBox cbxMethonList;
        private System.Windows.Forms.TextBox tbxResponse;
        private System.Windows.Forms.TextBox tbxRequest;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnLoadRequestXml;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}