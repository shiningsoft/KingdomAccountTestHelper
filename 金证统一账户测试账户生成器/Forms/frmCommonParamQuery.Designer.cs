namespace 金证统一账户测试账户生成器
{
    partial class frmCommonParamQuery
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
            this.tbxCommonParamValue = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.btnQueryCommonParams = new System.Windows.Forms.Button();
            this.tbxCommonParamKey = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbxCommonParamValue
            // 
            this.tbxCommonParamValue.Location = new System.Drawing.Point(88, 52);
            this.tbxCommonParamValue.Name = "tbxCommonParamValue";
            this.tbxCommonParamValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxCommonParamValue.Size = new System.Drawing.Size(265, 21);
            this.tbxCommonParamValue.TabIndex = 17;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(29, 55);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(53, 12);
            this.label28.TabIndex = 15;
            this.label28.Text = "参数值：";
            // 
            // btnQueryCommonParams
            // 
            this.btnQueryCommonParams.Location = new System.Drawing.Point(359, 23);
            this.btnQueryCommonParams.Name = "btnQueryCommonParams";
            this.btnQueryCommonParams.Size = new System.Drawing.Size(100, 23);
            this.btnQueryCommonParams.TabIndex = 16;
            this.btnQueryCommonParams.Text = "查询";
            this.btnQueryCommonParams.UseVisualStyleBackColor = true;
            this.btnQueryCommonParams.Click += new System.EventHandler(this.btnQueryCommonParams_Click);
            // 
            // tbxCommonParamKey
            // 
            this.tbxCommonParamKey.Location = new System.Drawing.Point(88, 25);
            this.tbxCommonParamKey.Name = "tbxCommonParamKey";
            this.tbxCommonParamKey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxCommonParamKey.Size = new System.Drawing.Size(265, 21);
            this.tbxCommonParamKey.TabIndex = 14;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(29, 28);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 12);
            this.label27.TabIndex = 13;
            this.label27.Text = "参数名：";
            // 
            // frmCommonParamQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 315);
            this.Controls.Add(this.tbxCommonParamValue);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.btnQueryCommonParams);
            this.Controls.Add(this.tbxCommonParamKey);
            this.Controls.Add(this.label27);
            this.Name = "frmCommonParamQuery";
            this.Text = "公共参数查询";
            this.Load += new System.EventHandler(this.frmCommonParamQuery_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxCommonParamValue;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button btnQueryCommonParams;
        private System.Windows.Forms.TextBox tbxCommonParamKey;
        private System.Windows.Forms.Label label27;
    }
}