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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnRefreshMethonList = new System.Windows.Forms.Button();
            this.cbxMethonList = new System.Windows.Forms.ComboBox();
            this.tbxResponse = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnLoadRequestXml = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DD_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DD_ITEM_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DD_ITEM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INT_ORG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbInterfaceTitle = new System.Windows.Forms.Label();
            this.dgvParams = new System.Windows.Forms.DataGridView();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lbQueryDictStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.tbxResponse.Location = new System.Drawing.Point(3, 22);
            this.tbxResponse.Multiline = true;
            this.tbxResponse.Name = "tbxResponse";
            this.tbxResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxResponse.Size = new System.Drawing.Size(517, 535);
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Controls.Add(this.lbInterfaceTitle);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbxResponse);
            this.splitContainer1.Size = new System.Drawing.Size(1018, 560);
            this.splitContainer1.SplitterDistance = 491;
            this.splitContainer1.TabIndex = 28;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DD_ID,
            this.DD_ITEM_NAME,
            this.DD_ITEM,
            this.INT_ORG});
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(479, 147);
            this.dataGridView1.TabIndex = 17;
            // 
            // DD_ID
            // 
            this.DD_ID.DataPropertyName = "DD_ID";
            this.DD_ID.HeaderText = "字典项";
            this.DD_ID.Name = "DD_ID";
            this.DD_ID.ReadOnly = true;
            // 
            // DD_ITEM_NAME
            // 
            this.DD_ITEM_NAME.DataPropertyName = "DD_ITEM_NAME";
            this.DD_ITEM_NAME.HeaderText = "名称";
            this.DD_ITEM_NAME.Name = "DD_ITEM_NAME";
            this.DD_ITEM_NAME.ReadOnly = true;
            // 
            // DD_ITEM
            // 
            this.DD_ITEM.DataPropertyName = "DD_ITEM";
            this.DD_ITEM.HeaderText = "值";
            this.DD_ITEM.Name = "DD_ITEM";
            this.DD_ITEM.ReadOnly = true;
            // 
            // INT_ORG
            // 
            this.INT_ORG.DataPropertyName = "INT_ORG";
            this.INT_ORG.HeaderText = "INT_ORG";
            this.INT_ORG.Name = "INT_ORG";
            this.INT_ORG.ReadOnly = true;
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
            // dgvParams
            // 
            this.dgvParams.AllowUserToAddRows = false;
            this.dgvParams.AllowUserToDeleteRows = false;
            this.dgvParams.AllowUserToResizeRows = false;
            this.dgvParams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvParams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName,
            this.ColumnValue,
            this.Comment});
            this.dgvParams.Location = new System.Drawing.Point(3, 3);
            this.dgvParams.MultiSelect = false;
            this.dgvParams.Name = "dgvParams";
            this.dgvParams.RowHeadersVisible = false;
            this.dgvParams.RowHeadersWidth = 100;
            this.dgvParams.RowTemplate.Height = 23;
            this.dgvParams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvParams.Size = new System.Drawing.Size(479, 378);
            this.dgvParams.TabIndex = 0;
            this.dgvParams.CurrentCellChanged += new System.EventHandler(this.dgvParams_CurrentCellChanged);
            // 
            // ColumnName
            // 
            this.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnName.DataPropertyName = "字段名";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnName.DefaultCellStyle = dataGridViewCellStyle7;
            this.ColumnName.FillWeight = 152.2843F;
            this.ColumnName.HeaderText = "字段名";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnName.Width = 120;
            // 
            // ColumnValue
            // 
            this.ColumnValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnValue.DataPropertyName = "字段值";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnValue.DefaultCellStyle = dataGridViewCellStyle8;
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
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(3, 19);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvParams);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lbQueryDictStatus);
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer2.Size = new System.Drawing.Size(485, 538);
            this.splitContainer2.SplitterDistance = 384;
            this.splitContainer2.TabIndex = 26;
            // 
            // lbQueryDictStatus
            // 
            this.lbQueryDictStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbQueryDictStatus.AutoSize = true;
            this.lbQueryDictStatus.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbQueryDictStatus.Location = new System.Drawing.Point(170, 47);
            this.lbQueryDictStatus.Name = "lbQueryDictStatus";
            this.lbQueryDictStatus.Size = new System.Drawing.Size(88, 16);
            this.lbQueryDictStatus.TabIndex = 18;
            this.lbQueryDictStatus.Text = "正在查询：";
            this.lbQueryDictStatus.Visible = false;
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
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
        private System.Windows.Forms.Label lbInterfaceTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DD_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DD_ITEM_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn DD_ITEM;
        private System.Windows.Forms.DataGridViewTextBoxColumn INT_ORG;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lbQueryDictStatus;
    }
}