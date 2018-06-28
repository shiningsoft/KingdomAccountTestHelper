namespace 金证统一账户测试账户生成器
{
    partial class frmDictQuery
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnQueryDict = new System.Windows.Forms.Button();
            this.dictName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 36);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(951, 467);
            this.dataGridView1.TabIndex = 16;
            // 
            // btnQueryDict
            // 
            this.btnQueryDict.Location = new System.Drawing.Point(351, 7);
            this.btnQueryDict.Name = "btnQueryDict";
            this.btnQueryDict.Size = new System.Drawing.Size(100, 23);
            this.btnQueryDict.TabIndex = 15;
            this.btnQueryDict.Text = "查询";
            this.btnQueryDict.UseVisualStyleBackColor = true;
            this.btnQueryDict.Click += new System.EventHandler(this.btnQueryDict_Click);
            // 
            // dictName
            // 
            this.dictName.Location = new System.Drawing.Point(80, 9);
            this.dictName.Name = "dictName";
            this.dictName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dictName.Size = new System.Drawing.Size(265, 21);
            this.dictName.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 12);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 13;
            this.label14.Text = "字典项：";
            // 
            // frmDictQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 515);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnQueryDict);
            this.Controls.Add(this.dictName);
            this.Controls.Add(this.label14);
            this.Name = "frmDictQuery";
            this.Text = "数据字典查询";
            this.Load += new System.EventHandler(this.frmDictQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnQueryDict;
        private System.Windows.Forms.TextBox dictName;
        private System.Windows.Forms.Label label14;
    }
}