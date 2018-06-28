namespace Xml格式化工具
{
    partial class Main
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
            this.btnSaveXmlStr = new System.Windows.Forms.Button();
            this.btnPreProccess = new System.Windows.Forms.Button();
            this.convert2memo = new System.Windows.Forms.Button();
            this.convert2setAttr = new System.Windows.Forms.Button();
            this.convert2params = new System.Windows.Forms.Button();
            this.tbXmlStr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSaveXmlStr
            // 
            this.btnSaveXmlStr.Location = new System.Drawing.Point(112, 12);
            this.btnSaveXmlStr.Name = "btnSaveXmlStr";
            this.btnSaveXmlStr.Size = new System.Drawing.Size(94, 23);
            this.btnSaveXmlStr.TabIndex = 11;
            this.btnSaveXmlStr.Text = "保存";
            this.btnSaveXmlStr.UseVisualStyleBackColor = true;
            this.btnSaveXmlStr.Click += new System.EventHandler(this.btnSaveXmlStr_Click);
            // 
            // btnPreProccess
            // 
            this.btnPreProccess.Location = new System.Drawing.Point(12, 12);
            this.btnPreProccess.Name = "btnPreProccess";
            this.btnPreProccess.Size = new System.Drawing.Size(94, 23);
            this.btnPreProccess.TabIndex = 10;
            this.btnPreProccess.Text = "预处理";
            this.btnPreProccess.UseVisualStyleBackColor = true;
            this.btnPreProccess.Click += new System.EventHandler(this.btnPreProccess_Click);
            // 
            // convert2memo
            // 
            this.convert2memo.Location = new System.Drawing.Point(451, 12);
            this.convert2memo.Name = "convert2memo";
            this.convert2memo.Size = new System.Drawing.Size(94, 23);
            this.convert2memo.TabIndex = 9;
            this.convert2memo.Text = "转成注释";
            this.convert2memo.UseVisualStyleBackColor = true;
            this.convert2memo.Click += new System.EventHandler(this.convert2memo_Click);
            // 
            // convert2setAttr
            // 
            this.convert2setAttr.Location = new System.Drawing.Point(351, 12);
            this.convert2setAttr.Name = "convert2setAttr";
            this.convert2setAttr.Size = new System.Drawing.Size(94, 23);
            this.convert2setAttr.TabIndex = 8;
            this.convert2setAttr.Text = "转成setAttr";
            this.convert2setAttr.UseVisualStyleBackColor = true;
            this.convert2setAttr.Click += new System.EventHandler(this.convert2setAttr_Click);
            // 
            // convert2params
            // 
            this.convert2params.Location = new System.Drawing.Point(251, 12);
            this.convert2params.Name = "convert2params";
            this.convert2params.Size = new System.Drawing.Size(94, 23);
            this.convert2params.TabIndex = 7;
            this.convert2params.Text = "转成入参";
            this.convert2params.UseVisualStyleBackColor = true;
            this.convert2params.Click += new System.EventHandler(this.convert2params_Click);
            // 
            // tbXmlStr
            // 
            this.tbXmlStr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbXmlStr.Location = new System.Drawing.Point(12, 41);
            this.tbXmlStr.Multiline = true;
            this.tbXmlStr.Name = "tbXmlStr";
            this.tbXmlStr.Size = new System.Drawing.Size(960, 475);
            this.tbXmlStr.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 529);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "使用说明：复制PDF文档接口入参中的data部分";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 553);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSaveXmlStr);
            this.Controls.Add(this.btnPreProccess);
            this.Controls.Add(this.convert2memo);
            this.Controls.Add(this.convert2setAttr);
            this.Controls.Add(this.convert2params);
            this.Controls.Add(this.tbXmlStr);
            this.Name = "Main";
            this.Text = "XML格式化工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveXmlStr;
        private System.Windows.Forms.Button btnPreProccess;
        private System.Windows.Forms.Button convert2memo;
        private System.Windows.Forms.Button convert2setAttr;
        private System.Windows.Forms.Button convert2params;
        private System.Windows.Forms.TextBox tbXmlStr;
        private System.Windows.Forms.Label label1;
    }
}

