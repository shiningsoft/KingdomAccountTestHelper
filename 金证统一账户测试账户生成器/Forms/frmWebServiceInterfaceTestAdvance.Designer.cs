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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnRefreshMethonList = new System.Windows.Forms.Button();
            this.cbxMethonList = new System.Windows.Forms.ComboBox();
            this.tbxResponse = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnLoadRequestXml = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvParams = new System.Windows.Forms.DataGridView();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbQueryDictStatus = new System.Windows.Forms.Label();
            this.dgvDict = new System.Windows.Forms.DataGridView();
            this.DD_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DD_ITEM_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DD_ITEM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INT_ORG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbInterfaceTitle = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpResponseDgv = new System.Windows.Forms.TabPage();
            this.cbxAutoTranslate = new System.Windows.Forms.CheckBox();
            this.lbResult = new System.Windows.Forms.Label();
            this.dgvResponse = new System.Windows.Forms.DataGridView();
            this.tpResponseText = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDict)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tpResponseDgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResponse)).BeginInit();
            this.tpResponseText.SuspendLayout();
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
            this.tbxResponse.Location = new System.Drawing.Point(6, 6);
            this.tbxResponse.Multiline = true;
            this.tbxResponse.Name = "tbxResponse";
            this.tbxResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxResponse.Size = new System.Drawing.Size(497, 516);
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
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
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
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1018, 560);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.TabIndex = 28;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(3, 35);
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
            this.splitContainer2.Panel2.Controls.Add(this.dgvDict);
            this.splitContainer2.Size = new System.Drawing.Size(494, 522);
            this.splitContainer2.SplitterDistance = 372;
            this.splitContainer2.TabIndex = 26;
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
            this.dgvParams.Size = new System.Drawing.Size(488, 366);
            this.dgvParams.TabIndex = 0;
            this.dgvParams.CurrentCellChanged += new System.EventHandler(this.dgvParams_CurrentCellChanged);
            // 
            // ColumnName
            // 
            this.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnName.DataPropertyName = "字段名";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnName.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnName.FillWeight = 152.2843F;
            this.ColumnName.HeaderText = "字段名";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnName.Width = 120;
            // 
            // ColumnValue
            // 
            this.ColumnValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnValue.DataPropertyName = "字段值";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnValue.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnValue.FillWeight = 140.1015F;
            this.ColumnValue.HeaderText = "字段值";
            this.ColumnValue.Name = "ColumnValue";
            this.ColumnValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnValue.Width = 120;
            // 
            // Comment
            // 
            this.Comment.DataPropertyName = "备注";
            this.Comment.FillWeight = 7.614212F;
            this.Comment.HeaderText = "备注";
            this.Comment.Name = "Comment";
            this.Comment.ReadOnly = true;
            this.Comment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lbQueryDictStatus
            // 
            this.lbQueryDictStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbQueryDictStatus.AutoSize = true;
            this.lbQueryDictStatus.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbQueryDictStatus.Location = new System.Drawing.Point(175, 45);
            this.lbQueryDictStatus.Name = "lbQueryDictStatus";
            this.lbQueryDictStatus.Size = new System.Drawing.Size(88, 16);
            this.lbQueryDictStatus.TabIndex = 18;
            this.lbQueryDictStatus.Text = "正在查询：";
            this.lbQueryDictStatus.Visible = false;
            // 
            // dgvDict
            // 
            this.dgvDict.AllowUserToAddRows = false;
            this.dgvDict.AllowUserToDeleteRows = false;
            this.dgvDict.AllowUserToResizeRows = false;
            this.dgvDict.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDict.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDict.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvDict.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDict.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDict.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DD_ID,
            this.DD_ITEM_NAME,
            this.DD_ITEM,
            this.INT_ORG});
            this.dgvDict.Location = new System.Drawing.Point(3, 3);
            this.dgvDict.Name = "dgvDict";
            this.dgvDict.ReadOnly = true;
            this.dgvDict.RowHeadersVisible = false;
            this.dgvDict.RowTemplate.Height = 23;
            this.dgvDict.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDict.Size = new System.Drawing.Size(488, 143);
            this.dgvDict.TabIndex = 17;
            this.dgvDict.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDict_CellDoubleClick);
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
            this.DD_ITEM.MinimumWidth = 60;
            this.DD_ITEM.Name = "DD_ITEM";
            this.DD_ITEM.ReadOnly = true;
            // 
            // INT_ORG
            // 
            this.INT_ORG.DataPropertyName = "INT_ORG";
            this.INT_ORG.HeaderText = "机构代码";
            this.INT_ORG.MinimumWidth = 80;
            this.INT_ORG.Name = "INT_ORG";
            this.INT_ORG.ReadOnly = true;
            // 
            // lbInterfaceTitle
            // 
            this.lbInterfaceTitle.AutoSize = true;
            this.lbInterfaceTitle.Location = new System.Drawing.Point(8, 14);
            this.lbInterfaceTitle.Name = "lbInterfaceTitle";
            this.lbInterfaceTitle.Size = new System.Drawing.Size(65, 12);
            this.lbInterfaceTitle.TabIndex = 1;
            this.lbInterfaceTitle.Text = "接口功能：";
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpResponseDgv);
            this.tabControl1.Controls.Add(this.tpResponseText);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(508, 554);
            this.tabControl1.TabIndex = 26;
            // 
            // tpResponseDgv
            // 
            this.tpResponseDgv.Controls.Add(this.cbxAutoTranslate);
            this.tpResponseDgv.Controls.Add(this.lbResult);
            this.tpResponseDgv.Controls.Add(this.dgvResponse);
            this.tpResponseDgv.Location = new System.Drawing.Point(4, 4);
            this.tpResponseDgv.Name = "tpResponseDgv";
            this.tpResponseDgv.Padding = new System.Windows.Forms.Padding(3);
            this.tpResponseDgv.Size = new System.Drawing.Size(500, 528);
            this.tpResponseDgv.TabIndex = 0;
            this.tpResponseDgv.Text = "图形版";
            this.tpResponseDgv.UseVisualStyleBackColor = true;
            // 
            // cbxAutoTranslate
            // 
            this.cbxAutoTranslate.AutoSize = true;
            this.cbxAutoTranslate.Checked = true;
            this.cbxAutoTranslate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxAutoTranslate.Location = new System.Drawing.Point(6, 6);
            this.cbxAutoTranslate.Name = "cbxAutoTranslate";
            this.cbxAutoTranslate.Size = new System.Drawing.Size(72, 16);
            this.cbxAutoTranslate.TabIndex = 2;
            this.cbxAutoTranslate.Text = "自动翻译";
            this.cbxAutoTranslate.UseVisualStyleBackColor = true;
            this.cbxAutoTranslate.CheckedChanged += new System.EventHandler(this.cbxAutoTranslate_CheckedChanged);
            // 
            // lbResult
            // 
            this.lbResult.AutoSize = true;
            this.lbResult.Location = new System.Drawing.Point(107, 7);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(41, 12);
            this.lbResult.TabIndex = 1;
            this.lbResult.Text = "状态：";
            // 
            // dgvResponse
            // 
            this.dgvResponse.AllowUserToAddRows = false;
            this.dgvResponse.AllowUserToDeleteRows = false;
            this.dgvResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResponse.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvResponse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResponse.Location = new System.Drawing.Point(6, 28);
            this.dgvResponse.Name = "dgvResponse";
            this.dgvResponse.ReadOnly = true;
            this.dgvResponse.RowHeadersVisible = false;
            this.dgvResponse.RowTemplate.Height = 23;
            this.dgvResponse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResponse.Size = new System.Drawing.Size(488, 494);
            this.dgvResponse.TabIndex = 0;
            // 
            // tpResponseText
            // 
            this.tpResponseText.Controls.Add(this.tbxResponse);
            this.tpResponseText.Location = new System.Drawing.Point(4, 4);
            this.tpResponseText.Name = "tpResponseText";
            this.tpResponseText.Padding = new System.Windows.Forms.Padding(3);
            this.tpResponseText.Size = new System.Drawing.Size(509, 528);
            this.tpResponseText.TabIndex = 1;
            this.tpResponseText.Text = "文字版";
            this.tpResponseText.UseVisualStyleBackColor = true;
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDict)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tpResponseDgv.ResumeLayout(false);
            this.tpResponseDgv.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResponse)).EndInit();
            this.tpResponseText.ResumeLayout(false);
            this.tpResponseText.PerformLayout();
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
        private System.Windows.Forms.DataGridView dgvDict;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lbQueryDictStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn DD_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DD_ITEM_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn DD_ITEM;
        private System.Windows.Forms.DataGridViewTextBoxColumn INT_ORG;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpResponseDgv;
        private System.Windows.Forms.TabPage tpResponseText;
        private System.Windows.Forms.DataGridView dgvResponse;
        private System.Windows.Forms.Label lbResult;
        private System.Windows.Forms.CheckBox cbxAutoTranslate;
    }
}