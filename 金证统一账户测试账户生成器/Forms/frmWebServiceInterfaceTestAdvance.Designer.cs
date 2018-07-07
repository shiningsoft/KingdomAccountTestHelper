namespace 金证统一账户测试账户生成器
{
    partial class frmWebServiceInterfaceTestAdvance
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
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnLoadRequestXml = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvParams = new System.Windows.Forms.DataGridView();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbInterfaceTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.lbInterfaceTitle);
            this.splitContainer1.Panel1.Controls.Add(this.dgvParams);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbxResponse);
            this.splitContainer1.Size = new System.Drawing.Size(1018, 560);
            this.splitContainer1.SplitterDistance = 390;
            this.splitContainer1.TabIndex = 28;
            // 
            // dgvParams
            // 
            this.dgvParams.AllowUserToAddRows = false;
            this.dgvParams.AllowUserToDeleteRows = false;
            this.dgvParams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvParams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName,
            this.ColumnValue,
            this.Comment});
            this.dgvParams.Location = new System.Drawing.Point(3, 19);
            this.dgvParams.MultiSelect = false;
            this.dgvParams.Name = "dgvParams";
            this.dgvParams.RowHeadersVisible = false;
            this.dgvParams.RowHeadersWidth = 100;
            this.dgvParams.RowTemplate.Height = 23;
            this.dgvParams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvParams.Size = new System.Drawing.Size(384, 538);
            this.dgvParams.TabIndex = 0;
            // 
            // ColumnName
            // 
            this.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnName.DataPropertyName = "字段名";
            this.ColumnName.FillWeight = 152.2843F;
            this.ColumnName.HeaderText = "字段名";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.Width = 120;
            // 
            // ColumnValue
            // 
            this.ColumnValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnValue.DataPropertyName = "字段值";
            this.ColumnValue.FillWeight = 140.1015F;
            this.ColumnValue.HeaderText = "字段值";
            this.ColumnValue.Name = "ColumnValue";
            this.ColumnValue.Width = 120;
            // 
            // Comment
            // 
            this.Comment.DataPropertyName = "备注";
            this.Comment.FillWeight = 7.614212F;
            this.Comment.HeaderText = "备注";
            this.Comment.Name = "Comment";
            this.Comment.ReadOnly = true;
            // 
            // lbInterfaceTitle
            // 
            this.lbInterfaceTitle.AutoSize = true;
            this.lbInterfaceTitle.Location = new System.Drawing.Point(4, 4);
            this.lbInterfaceTitle.Name = "lbInterfaceTitle";
            this.lbInterfaceTitle.Size = new System.Drawing.Size(65, 12);
            this.lbInterfaceTitle.TabIndex = 1;
            this.lbInterfaceTitle.Text = "接口名称：";
            // 
            // frmWebServiceInterfaceTestAdvance
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
            this.Name = "frmWebServiceInterfaceTestAdvance";
            this.Text = "接口测试工具";
            this.Load += new System.EventHandler(this.frmWebServiceInterfaceTest_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefreshMethonList;
        private System.Windows.Forms.ComboBox cbxMethonList;
        private System.Windows.Forms.TextBox tbxResponse;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnLoadRequestXml;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.Label lbInterfaceTitle;
    }
}