namespace 金证统一账户测试账户生成器
{
    partial class frmSettings
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
            this.components = new System.ComponentModel.Container();
            this.cancel = new System.Windows.Forms.Button();
            this.accept = new System.Windows.Forms.Button();
            this.tbxOperatorId = new System.Windows.Forms.TextBox();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.tbxChannel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxZdTimeout = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxBranchNo = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpNormal = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.tbxMaxConnections = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.rbWin = new System.Windows.Forms.RadioButton();
            this.rbU = new System.Windows.Forms.RadioButton();
            this.tpServers = new System.Windows.Forms.TabPage();
            this.label17 = new System.Windows.Forms.Label();
            this.dgvWebServices = new System.Windows.Forms.DataGridView();
            this.webservice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbxSurveySN = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnResetRiskSettings = new System.Windows.Forms.Button();
            this.tbxCellsE = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbxCellsD = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbxCellsC = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbxCellsB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbxCellsA = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxCols = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tpNormal.SuspendLayout();
            this.tpServers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWebServices)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.Location = new System.Drawing.Point(580, 282);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 0;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.Location = new System.Drawing.Point(499, 282);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 1;
            this.accept.Text = "确定";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // tbxOperatorId
            // 
            this.tbxOperatorId.Location = new System.Drawing.Point(126, 78);
            this.tbxOperatorId.Name = "tbxOperatorId";
            this.tbxOperatorId.Size = new System.Drawing.Size(141, 21);
            this.tbxOperatorId.TabIndex = 2;
            this.toolTip.SetToolTip(this.tbxOperatorId, "统一账户系统里有WebService接口权限的操作员账号");
            // 
            // tbxPassword
            // 
            this.tbxPassword.Location = new System.Drawing.Point(126, 105);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.PasswordChar = '*';
            this.tbxPassword.Size = new System.Drawing.Size(141, 21);
            this.tbxPassword.TabIndex = 3;
            this.toolTip.SetToolTip(this.tbxPassword, "统一账户系统操作员的密码");
            // 
            // tbxChannel
            // 
            this.tbxChannel.Location = new System.Drawing.Point(126, 132);
            this.tbxChannel.Name = "tbxChannel";
            this.tbxChannel.Size = new System.Drawing.Size(141, 21);
            this.tbxChannel.TabIndex = 4;
            this.toolTip.SetToolTip(this.tbxChannel, "WebService操作员使用的操作渠道，操作员一定要有这个渠道的访问权限");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "操作员编号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "操作员密码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "操作渠道：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(313, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "中登超时时间：";
            // 
            // tbxZdTimeout
            // 
            this.tbxZdTimeout.Location = new System.Drawing.Point(408, 51);
            this.tbxZdTimeout.Name = "tbxZdTimeout";
            this.tbxZdTimeout.Size = new System.Drawing.Size(141, 21);
            this.tbxZdTimeout.TabIndex = 5;
            this.toolTip.SetToolTip(this.tbxZdTimeout, "发起中登业务时的超时时间，默认30秒，视情况而定");
            this.tbxZdTimeout.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxZdTimeout_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(555, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "秒";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "开户营业部：";
            // 
            // tbxBranchNo
            // 
            this.tbxBranchNo.Location = new System.Drawing.Point(126, 51);
            this.tbxBranchNo.Name = "tbxBranchNo";
            this.tbxBranchNo.Size = new System.Drawing.Size(141, 21);
            this.tbxBranchNo.TabIndex = 1;
            this.toolTip.SetToolTip(this.tbxBranchNo, "新开账户时默认开到哪个营业部，注意要用营业部代码而不是名称");
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpServers);
            this.tabControl1.Controls.Add(this.tpNormal);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(642, 263);
            this.tabControl1.TabIndex = 15;
            // 
            // tpNormal
            // 
            this.tpNormal.Controls.Add(this.label16);
            this.tpNormal.Controls.Add(this.tbxMaxConnections);
            this.tpNormal.Controls.Add(this.label15);
            this.tpNormal.Controls.Add(this.rbWin);
            this.tpNormal.Controls.Add(this.rbU);
            this.tpNormal.Controls.Add(this.label7);
            this.tpNormal.Controls.Add(this.tbxOperatorId);
            this.tpNormal.Controls.Add(this.tbxBranchNo);
            this.tpNormal.Controls.Add(this.tbxPassword);
            this.tpNormal.Controls.Add(this.label6);
            this.tpNormal.Controls.Add(this.tbxChannel);
            this.tpNormal.Controls.Add(this.label5);
            this.tpNormal.Controls.Add(this.tbxZdTimeout);
            this.tpNormal.Controls.Add(this.label2);
            this.tpNormal.Controls.Add(this.label4);
            this.tpNormal.Controls.Add(this.label3);
            this.tpNormal.Location = new System.Drawing.Point(4, 22);
            this.tpNormal.Name = "tpNormal";
            this.tpNormal.Padding = new System.Windows.Forms.Padding(3);
            this.tpNormal.Size = new System.Drawing.Size(634, 237);
            this.tpNormal.TabIndex = 0;
            this.tpNormal.Text = "系统参数";
            this.tpNormal.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(325, 104);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 12);
            this.label16.TabIndex = 19;
            this.label16.Text = "最大并发数：";
            // 
            // tbxMaxConnections
            // 
            this.tbxMaxConnections.Location = new System.Drawing.Point(408, 101);
            this.tbxMaxConnections.Name = "tbxMaxConnections";
            this.tbxMaxConnections.Size = new System.Drawing.Size(141, 21);
            this.tbxMaxConnections.TabIndex = 18;
            this.toolTip.SetToolTip(this.tbxMaxConnections, "发起WebService请求的最大并发数，最小为1。适当提高能够大大加快查询速度");
            this.tbxMaxConnections.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxMaxConnections_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(313, 81);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 12);
            this.label15.TabIndex = 17;
            this.label15.Text = "统一账户版本：";
            // 
            // rbWin
            // 
            this.rbWin.AutoSize = true;
            this.rbWin.Location = new System.Drawing.Point(455, 79);
            this.rbWin.Name = "rbWin";
            this.rbWin.Size = new System.Drawing.Size(53, 16);
            this.rbWin.TabIndex = 16;
            this.rbWin.Text = "Win版";
            this.toolTip.SetToolTip(this.rbWin, "Win版统一账户系统请选这个！");
            this.rbWin.UseVisualStyleBackColor = true;
            // 
            // rbU
            // 
            this.rbU.AutoSize = true;
            this.rbU.Checked = true;
            this.rbU.Location = new System.Drawing.Point(408, 79);
            this.rbU.Name = "rbU";
            this.rbU.Size = new System.Drawing.Size(41, 16);
            this.rbU.TabIndex = 15;
            this.rbU.TabStop = true;
            this.rbU.Text = "U版";
            this.toolTip.SetToolTip(this.rbU, "U版统一账户系统请选这个！");
            this.rbU.UseVisualStyleBackColor = true;
            // 
            // tpServers
            // 
            this.tpServers.Controls.Add(this.label17);
            this.tpServers.Controls.Add(this.dgvWebServices);
            this.tpServers.Location = new System.Drawing.Point(4, 22);
            this.tpServers.Name = "tpServers";
            this.tpServers.Padding = new System.Windows.Forms.Padding(3);
            this.tpServers.Size = new System.Drawing.Size(634, 237);
            this.tpServers.TabIndex = 2;
            this.tpServers.Text = "WebService列表";
            this.tpServers.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 12);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(281, 12);
            this.label17.TabIndex = 1;
            this.label17.Text = "允许设置多个WebService，以便快速切换不同环境。";
            // 
            // dgvWebServices
            // 
            this.dgvWebServices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWebServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWebServices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.webservice});
            this.dgvWebServices.Location = new System.Drawing.Point(7, 30);
            this.dgvWebServices.Name = "dgvWebServices";
            this.dgvWebServices.RowTemplate.Height = 23;
            this.dgvWebServices.Size = new System.Drawing.Size(621, 201);
            this.dgvWebServices.TabIndex = 0;
            // 
            // webservice
            // 
            this.webservice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.webservice.DataPropertyName = "webservice";
            this.webservice.HeaderText = "WebService地址";
            this.webservice.MinimumWidth = 400;
            this.webservice.Name = "webservice";
            this.webservice.Width = 400;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbxSurveySN);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.btnResetRiskSettings);
            this.tabPage2.Controls.Add(this.tbxCellsE);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.tbxCellsD);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.tbxCellsC);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.tbxCellsB);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.tbxCellsA);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.tbxCols);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(634, 237);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "风险测评";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbxSurveySN
            // 
            this.tbxSurveySN.Location = new System.Drawing.Point(137, 13);
            this.tbxSurveySN.Name = "tbxSurveySN";
            this.tbxSurveySN.Size = new System.Drawing.Size(458, 21);
            this.tbxSurveySN.TabIndex = 0;
            this.tbxSurveySN.Text = "1";
            this.toolTip.SetToolTip(this.tbxSurveySN, "个人投资者风险测评问卷序号");
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(30, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(101, 12);
            this.label14.TabIndex = 21;
            this.label14.Text = "问卷序号（SN）：";
            // 
            // btnResetRiskSettings
            // 
            this.btnResetRiskSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetRiskSettings.Location = new System.Drawing.Point(520, 202);
            this.btnResetRiskSettings.Name = "btnResetRiskSettings";
            this.btnResetRiskSettings.Size = new System.Drawing.Size(75, 23);
            this.btnResetRiskSettings.TabIndex = 7;
            this.btnResetRiskSettings.Text = "恢复默认值";
            this.btnResetRiskSettings.UseVisualStyleBackColor = true;
            this.btnResetRiskSettings.Click += new System.EventHandler(this.btnResetRiskSettings_Click);
            // 
            // tbxCellsE
            // 
            this.tbxCellsE.Location = new System.Drawing.Point(137, 175);
            this.tbxCellsE.Name = "tbxCellsE";
            this.tbxCellsE.Size = new System.Drawing.Size(458, 21);
            this.tbxCellsE.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 178);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 12);
            this.label13.TabIndex = 18;
            this.label13.Text = "激进型（Cells）：";
            // 
            // tbxCellsD
            // 
            this.tbxCellsD.Location = new System.Drawing.Point(137, 148);
            this.tbxCellsD.Name = "tbxCellsD";
            this.tbxCellsD.Size = new System.Drawing.Size(458, 21);
            this.tbxCellsD.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 151);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(107, 12);
            this.label12.TabIndex = 16;
            this.label12.Text = "积极型（Cells）：";
            // 
            // tbxCellsC
            // 
            this.tbxCellsC.Location = new System.Drawing.Point(137, 121);
            this.tbxCellsC.Name = "tbxCellsC";
            this.tbxCellsC.Size = new System.Drawing.Size(458, 21);
            this.tbxCellsC.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 124);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 12);
            this.label11.TabIndex = 14;
            this.label11.Text = "稳健型（Cells）：";
            // 
            // tbxCellsB
            // 
            this.tbxCellsB.Location = new System.Drawing.Point(137, 94);
            this.tbxCellsB.Name = "tbxCellsB";
            this.tbxCellsB.Size = new System.Drawing.Size(458, 21);
            this.tbxCellsB.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "谨慎型（Cells）：";
            // 
            // tbxCellsA
            // 
            this.tbxCellsA.Location = new System.Drawing.Point(137, 67);
            this.tbxCellsA.Name = "tbxCellsA";
            this.tbxCellsA.Size = new System.Drawing.Size(458, 21);
            this.tbxCellsA.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 12);
            this.label9.TabIndex = 10;
            this.label9.Text = "保守型（Cells）：";
            // 
            // tbxCols
            // 
            this.tbxCols.Location = new System.Drawing.Point(137, 40);
            this.tbxCols.Name = "tbxCols";
            this.tbxCols.Size = new System.Drawing.Size(458, 21);
            this.tbxCols.TabIndex = 1;
            this.toolTip.SetToolTip(this.tbxCols, "用于快速提交风险测评的题目序列");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "题目序列（Cols）：";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 317);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.cancel);
            this.Name = "frmSettings";
            this.ShowIcon = false;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpNormal.ResumeLayout(false);
            this.tpNormal.PerformLayout();
            this.tpServers.ResumeLayout(false);
            this.tpServers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWebServices)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.TextBox tbxOperatorId;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.TextBox tbxChannel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxZdTimeout;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxBranchNo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpNormal;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbxCellsE;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbxCellsD;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxCellsC;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbxCellsB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbxCellsA;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxCols;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnResetRiskSettings;
        private System.Windows.Forms.TextBox tbxSurveySN;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton rbWin;
        private System.Windows.Forms.RadioButton rbU;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbxMaxConnections;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabPage tpServers;
        private System.Windows.Forms.DataGridView dgvWebServices;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DataGridViewTextBoxColumn webservice;
    }
}