using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace 金证统一账户测试账户生成器
{
    partial class frmNewAccount
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
            if (kess != null)
            {
                kess.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewAccount));
            this.tbxBankAcctCode = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.cbxCubsbScOpenAcctOpType = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.tbCuacct_cls = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.tbChannels = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.cbxOccupation = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cbxShortIdNo = new System.Windows.Forms.CheckBox();
            this.btnBindSHAcct = new System.Windows.Forms.Button();
            this.cbxLongTerm = new System.Windows.Forms.CheckBox();
            this.btnCreateIDCardImgBackSide = new System.Windows.Forms.Button();
            this.btnCreateIDCardImgFaceSide = new System.Windows.Forms.Button();
            this.tbxCybSignDate = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.dtpCybSignDate = new System.Windows.Forms.DateTimePicker();
            this.cbxOpenType = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.btnRegisterSZAStkAcct = new System.Windows.Forms.Button();
            this.btnOpenSZAStkAcct = new System.Windows.Forms.Button();
            this.btnQueryCYB = new System.Windows.Forms.Button();
            this.bank_code = new System.Windows.Forms.ComboBox();
            this.btnValidateId = new System.Windows.Forms.Button();
            this.sex = new System.Windows.Forms.ComboBox();
            this.citizenship = new System.Windows.Forms.ComboBox();
            this.education = new System.Windows.Forms.ComboBox();
            this.occu_type = new System.Windows.Forms.ComboBox();
            this.nationality = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.zip_code = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.btnOpenYMT = new System.Windows.Forms.Button();
            this.btnOpenCYB = new System.Windows.Forms.Button();
            this.tbxYMTCode = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.btnOpenAccountByOneClick = new System.Windows.Forms.Button();
            this.tbxSZAcct = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbxSHAcct = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tbxCuacct = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnRegisterSHAStkAcct = new System.Windows.Forms.Button();
            this.btnOpenSHAStkAcct = new System.Windows.Forms.Button();
            this.btnQueryStockAccount = new System.Windows.Forms.Button();
            this.btnBankSign = new System.Windows.Forms.Button();
            this.btnSubmitRiskTest = new System.Windows.Forms.Button();
            this.btnSetPassword = new System.Windows.Forms.Button();
            this.btnOpenCuacct = new System.Windows.Forms.Button();
            this.risk_level = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.btnRecreateUserinfo = new System.Windows.Forms.Button();
            this.id_exp_date = new System.Windows.Forms.DateTimePicker();
            this.id_beg_date = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.mobile_tel = new System.Windows.Forms.TextBox();
            this.id_addr = new System.Windows.Forms.TextBox();
            this.id_iss_agcy = new System.Windows.Forms.TextBox();
            this.id_code = new System.Windows.Forms.TextBox();
            this.user_name = new System.Windows.Forms.TextBox();
            this.tbxCustCode = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenUserCode = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxBankAcctCode
            // 
            this.tbxBankAcctCode.Enabled = false;
            this.tbxBankAcctCode.Location = new System.Drawing.Point(637, 208);
            this.tbxBankAcctCode.Name = "tbxBankAcctCode";
            this.tbxBankAcctCode.Size = new System.Drawing.Size(157, 21);
            this.tbxBankAcctCode.TabIndex = 91;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(567, 211);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(65, 12);
            this.label33.TabIndex = 90;
            this.label33.Text = "银行卡号：";
            // 
            // cbxCubsbScOpenAcctOpType
            // 
            this.cbxCubsbScOpenAcctOpType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCubsbScOpenAcctOpType.FormattingEnabled = true;
            this.cbxCubsbScOpenAcctOpType.Items.AddRange(new object[] {
            "预指定",
            "一步式"});
            this.cbxCubsbScOpenAcctOpType.Location = new System.Drawing.Point(460, 181);
            this.cbxCubsbScOpenAcctOpType.Name = "cbxCubsbScOpenAcctOpType";
            this.cbxCubsbScOpenAcctOpType.Size = new System.Drawing.Size(100, 20);
            this.cbxCubsbScOpenAcctOpType.TabIndex = 88;
            this.cbxCubsbScOpenAcctOpType.SelectedIndexChanged += new System.EventHandler(this.cbxCubsbScOpenAcctOpType_SelectedIndexChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(389, 184);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(65, 12);
            this.label32.TabIndex = 89;
            this.label32.Text = "存管类型：";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbCuacct_cls
            // 
            this.tbCuacct_cls.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::金证统一账户测试账户生成器.Properties.Settings.Default, "默认开通的资产账户类别", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbCuacct_cls.Location = new System.Drawing.Point(694, 155);
            this.tbCuacct_cls.Name = "tbCuacct_cls";
            this.tbCuacct_cls.Size = new System.Drawing.Size(100, 21);
            this.tbCuacct_cls.TabIndex = 87;
            this.tbCuacct_cls.Text = global::金证统一账户测试账户生成器.Properties.Settings.Default.默认开通的资产账户类别;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(599, 158);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(89, 12);
            this.label31.TabIndex = 86;
            this.label31.Text = "资产账户类别：";
            // 
            // tbChannels
            // 
            this.tbChannels.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::金证统一账户测试账户生成器.Properties.Settings.Default, "默认开通的操作渠道", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbChannels.Location = new System.Drawing.Point(694, 181);
            this.tbChannels.Name = "tbChannels";
            this.tbChannels.Size = new System.Drawing.Size(100, 21);
            this.tbChannels.TabIndex = 85;
            this.tbChannels.Text = global::金证统一账户测试账户生成器.Properties.Settings.Default.默认开通的操作渠道;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(623, 184);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(65, 12);
            this.label30.TabIndex = 84;
            this.label30.Text = "操作渠道：";
            // 
            // cbxOccupation
            // 
            this.cbxOccupation.FormattingEnabled = true;
            this.cbxOccupation.Items.AddRange(new object[] {
            "专业技术人员",
            "一般工商业、服务业人员",
            "农、林、牧、渔、水利业生产人员",
            "生产、运输设备操作人员及有关人员",
            "自由职业者",
            "艺术品收藏、拍卖等从业人员",
            "娱乐场所、博彩、影视等从业人员"});
            this.cbxOccupation.Location = new System.Drawing.Point(658, 104);
            this.cbxOccupation.Name = "cbxOccupation";
            this.cbxOccupation.Size = new System.Drawing.Size(136, 20);
            this.cbxOccupation.TabIndex = 83;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(587, 106);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(65, 12);
            this.label29.TabIndex = 82;
            this.label29.Text = "手输职业：";
            // 
            // cbxShortIdNo
            // 
            this.cbxShortIdNo.AutoSize = true;
            this.cbxShortIdNo.Location = new System.Drawing.Point(313, 49);
            this.cbxShortIdNo.Name = "cbxShortIdNo";
            this.cbxShortIdNo.Size = new System.Drawing.Size(48, 16);
            this.cbxShortIdNo.TabIndex = 2;
            this.cbxShortIdNo.Text = "15位";
            this.cbxShortIdNo.UseVisualStyleBackColor = true;
            this.cbxShortIdNo.CheckedChanged += new System.EventHandler(this.cbxShortIdNo_CheckedChanged);
            // 
            // btnBindSHAcct
            // 
            this.btnBindSHAcct.Location = new System.Drawing.Point(400, 101);
            this.btnBindSHAcct.Name = "btnBindSHAcct";
            this.btnBindSHAcct.Size = new System.Drawing.Size(100, 23);
            this.btnBindSHAcct.TabIndex = 32;
            this.btnBindSHAcct.Text = "指定交易";
            this.btnBindSHAcct.UseVisualStyleBackColor = true;
            this.btnBindSHAcct.Click += new System.EventHandler(this.btnBindSHAcct_Click);
            // 
            // cbxLongTerm
            // 
            this.cbxLongTerm.AutoSize = true;
            this.cbxLongTerm.Location = new System.Drawing.Point(314, 130);
            this.cbxLongTerm.Name = "cbxLongTerm";
            this.cbxLongTerm.Size = new System.Drawing.Size(48, 16);
            this.cbxLongTerm.TabIndex = 6;
            this.cbxLongTerm.Text = "长期";
            this.cbxLongTerm.UseVisualStyleBackColor = true;
            this.cbxLongTerm.CheckedChanged += new System.EventHandler(this.cbxLongTerm_CheckedChanged);
            // 
            // btnCreateIDCardImgBackSide
            // 
            this.btnCreateIDCardImgBackSide.Location = new System.Drawing.Point(815, 129);
            this.btnCreateIDCardImgBackSide.Name = "btnCreateIDCardImgBackSide";
            this.btnCreateIDCardImgBackSide.Size = new System.Drawing.Size(100, 23);
            this.btnCreateIDCardImgBackSide.TabIndex = 51;
            this.btnCreateIDCardImgBackSide.Text = "生成身份证背面";
            this.btnCreateIDCardImgBackSide.UseVisualStyleBackColor = true;
            this.btnCreateIDCardImgBackSide.Click += new System.EventHandler(this.btnCreateIDCardImgBackSide_Click);
            // 
            // btnCreateIDCardImgFaceSide
            // 
            this.btnCreateIDCardImgFaceSide.Location = new System.Drawing.Point(815, 102);
            this.btnCreateIDCardImgFaceSide.Name = "btnCreateIDCardImgFaceSide";
            this.btnCreateIDCardImgFaceSide.Size = new System.Drawing.Size(100, 23);
            this.btnCreateIDCardImgFaceSide.TabIndex = 50;
            this.btnCreateIDCardImgFaceSide.Text = "生成身份证正面";
            this.btnCreateIDCardImgFaceSide.UseVisualStyleBackColor = true;
            this.btnCreateIDCardImgFaceSide.Click += new System.EventHandler(this.btnCreateIDCardImg_Click);
            // 
            // tbxCybSignDate
            // 
            this.tbxCybSignDate.Location = new System.Drawing.Point(400, 157);
            this.tbxCybSignDate.Name = "tbxCybSignDate";
            this.tbxCybSignDate.ReadOnly = true;
            this.tbxCybSignDate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxCybSignDate.Size = new System.Drawing.Size(206, 21);
            this.tbxCybSignDate.TabIndex = 43;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(317, 160);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(77, 12);
            this.label25.TabIndex = 80;
            this.label25.Text = "创业板信息：";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(587, 77);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(101, 12);
            this.label24.TabIndex = 79;
            this.label24.Text = "创业板签约类型：";
            // 
            // dtpCybSignDate
            // 
            this.dtpCybSignDate.CustomFormat = "yyyyMMdd";
            this.dtpCybSignDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCybSignDate.Location = new System.Drawing.Point(694, 47);
            this.dtpCybSignDate.Name = "dtpCybSignDate";
            this.dtpCybSignDate.Size = new System.Drawing.Size(100, 21);
            this.dtpCybSignDate.TabIndex = 18;
            this.dtpCybSignDate.Value = new System.DateTime(2017, 8, 1, 0, 0, 0, 0);
            // 
            // cbxOpenType
            // 
            this.cbxOpenType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOpenType.FormattingEnabled = true;
            this.cbxOpenType.Location = new System.Drawing.Point(694, 74);
            this.cbxOpenType.Name = "cbxOpenType";
            this.cbxOpenType.Size = new System.Drawing.Size(100, 20);
            this.cbxOpenType.TabIndex = 19;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(587, 50);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(101, 12);
            this.label23.TabIndex = 76;
            this.label23.Text = "创业板签约日期：";
            // 
            // btnRegisterSZAStkAcct
            // 
            this.btnRegisterSZAStkAcct.Location = new System.Drawing.Point(294, 128);
            this.btnRegisterSZAStkAcct.Name = "btnRegisterSZAStkAcct";
            this.btnRegisterSZAStkAcct.Size = new System.Drawing.Size(100, 23);
            this.btnRegisterSZAStkAcct.TabIndex = 35;
            this.btnRegisterSZAStkAcct.Text = "加挂";
            this.btnRegisterSZAStkAcct.UseVisualStyleBackColor = true;
            this.btnRegisterSZAStkAcct.Click += new System.EventHandler(this.btnRegisterSZAStkAcct_Click);
            // 
            // btnOpenSZAStkAcct
            // 
            this.btnOpenSZAStkAcct.Location = new System.Drawing.Point(188, 128);
            this.btnOpenSZAStkAcct.Name = "btnOpenSZAStkAcct";
            this.btnOpenSZAStkAcct.Size = new System.Drawing.Size(100, 23);
            this.btnOpenSZAStkAcct.TabIndex = 34;
            this.btnOpenSZAStkAcct.Text = "新开";
            this.btnOpenSZAStkAcct.UseVisualStyleBackColor = true;
            this.btnOpenSZAStkAcct.Click += new System.EventHandler(this.btnOpenSZAStkAcct_Click);
            // 
            // btnQueryCYB
            // 
            this.btnQueryCYB.Location = new System.Drawing.Point(506, 128);
            this.btnQueryCYB.Name = "btnQueryCYB";
            this.btnQueryCYB.Size = new System.Drawing.Size(100, 23);
            this.btnQueryCYB.TabIndex = 37;
            this.btnQueryCYB.Text = "创业板信息查询";
            this.btnQueryCYB.UseVisualStyleBackColor = true;
            this.btnQueryCYB.Click += new System.EventHandler(this.btnQueryCYB_Click);
            // 
            // bank_code
            // 
            this.bank_code.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bank_code.FormattingEnabled = true;
            this.bank_code.Location = new System.Drawing.Point(461, 208);
            this.bank_code.Name = "bank_code";
            this.bank_code.Size = new System.Drawing.Size(100, 20);
            this.bank_code.TabIndex = 11;
            // 
            // btnValidateId
            // 
            this.btnValidateId.Location = new System.Drawing.Point(815, 46);
            this.btnValidateId.Name = "btnValidateId";
            this.btnValidateId.Size = new System.Drawing.Size(100, 23);
            this.btnValidateId.TabIndex = 46;
            this.btnValidateId.Text = "中登公安校验";
            this.btnValidateId.UseVisualStyleBackColor = true;
            this.btnValidateId.Click += new System.EventHandler(this.btnValidateId_Click);
            // 
            // sex
            // 
            this.sex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sex.FormattingEnabled = true;
            this.sex.Location = new System.Drawing.Point(694, 20);
            this.sex.Name = "sex";
            this.sex.Size = new System.Drawing.Size(100, 20);
            this.sex.TabIndex = 17;
            // 
            // citizenship
            // 
            this.citizenship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.citizenship.FormattingEnabled = true;
            this.citizenship.Location = new System.Drawing.Point(460, 155);
            this.citizenship.Name = "citizenship";
            this.citizenship.Size = new System.Drawing.Size(100, 20);
            this.citizenship.TabIndex = 16;
            // 
            // education
            // 
            this.education.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.education.FormattingEnabled = true;
            this.education.Location = new System.Drawing.Point(460, 127);
            this.education.Name = "education";
            this.education.Size = new System.Drawing.Size(100, 20);
            this.education.TabIndex = 15;
            // 
            // occu_type
            // 
            this.occu_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.occu_type.FormattingEnabled = true;
            this.occu_type.Location = new System.Drawing.Point(460, 101);
            this.occu_type.Name = "occu_type";
            this.occu_type.Size = new System.Drawing.Size(100, 20);
            this.occu_type.TabIndex = 14;
            this.occu_type.SelectedIndexChanged += new System.EventHandler(this.occu_type_SelectedIndexChanged);
            // 
            // nationality
            // 
            this.nationality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nationality.FormattingEnabled = true;
            this.nationality.Location = new System.Drawing.Point(460, 74);
            this.nationality.Name = "nationality";
            this.nationality.Size = new System.Drawing.Size(100, 20);
            this.nationality.TabIndex = 13;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(647, 23);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 12);
            this.label22.TabIndex = 62;
            this.label22.Text = "性别：";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // zip_code
            // 
            this.zip_code.Location = new System.Drawing.Point(268, 181);
            this.zip_code.Name = "zip_code";
            this.zip_code.Size = new System.Drawing.Size(100, 21);
            this.zip_code.TabIndex = 9;
            this.zip_code.Text = "230000";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(221, 184);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(41, 12);
            this.label21.TabIndex = 59;
            this.label21.Text = "邮编：";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOpenYMT
            // 
            this.btnOpenYMT.Location = new System.Drawing.Point(188, 74);
            this.btnOpenYMT.Name = "btnOpenYMT";
            this.btnOpenYMT.Size = new System.Drawing.Size(100, 23);
            this.btnOpenYMT.TabIndex = 28;
            this.btnOpenYMT.Text = "开一码通";
            this.btnOpenYMT.UseVisualStyleBackColor = true;
            this.btnOpenYMT.Click += new System.EventHandler(this.btnOpenYMT_Click);
            // 
            // btnOpenCYB
            // 
            this.btnOpenCYB.Location = new System.Drawing.Point(400, 128);
            this.btnOpenCYB.Name = "btnOpenCYB";
            this.btnOpenCYB.Size = new System.Drawing.Size(100, 23);
            this.btnOpenCYB.TabIndex = 36;
            this.btnOpenCYB.Text = "开通创业板";
            this.btnOpenCYB.UseVisualStyleBackColor = true;
            this.btnOpenCYB.Click += new System.EventHandler(this.btnOpenCYB_Click);
            // 
            // tbxYMTCode
            // 
            this.tbxYMTCode.Location = new System.Drawing.Point(82, 76);
            this.tbxYMTCode.Name = "tbxYMTCode";
            this.tbxYMTCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxYMTCode.Size = new System.Drawing.Size(100, 21);
            this.tbxYMTCode.TabIndex = 27;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(23, 79);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 12);
            this.label20.TabIndex = 55;
            this.label20.Text = "一码通：";
            // 
            // btnOpenAccountByOneClick
            // 
            this.btnOpenAccountByOneClick.Location = new System.Drawing.Point(82, 157);
            this.btnOpenAccountByOneClick.Name = "btnOpenAccountByOneClick";
            this.btnOpenAccountByOneClick.Size = new System.Drawing.Size(100, 23);
            this.btnOpenAccountByOneClick.TabIndex = 38;
            this.btnOpenAccountByOneClick.Text = "一键开户";
            this.btnOpenAccountByOneClick.UseVisualStyleBackColor = true;
            this.btnOpenAccountByOneClick.Click += new System.EventHandler(this.btnOpenAccountByOneClick_Click);
            // 
            // tbxSZAcct
            // 
            this.tbxSZAcct.Location = new System.Drawing.Point(82, 130);
            this.tbxSZAcct.Name = "tbxSZAcct";
            this.tbxSZAcct.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxSZAcct.Size = new System.Drawing.Size(100, 21);
            this.tbxSZAcct.TabIndex = 33;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(17, 133);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 12);
            this.label19.TabIndex = 52;
            this.label19.Text = "深圳A股：";
            // 
            // tbxSHAcct
            // 
            this.tbxSHAcct.Location = new System.Drawing.Point(82, 103);
            this.tbxSHAcct.Name = "tbxSHAcct";
            this.tbxSHAcct.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxSHAcct.Size = new System.Drawing.Size(100, 21);
            this.tbxSHAcct.TabIndex = 29;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(17, 106);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 12);
            this.label18.TabIndex = 50;
            this.label18.Text = "上海A股：";
            // 
            // tbxCuacct
            // 
            this.tbxCuacct.Location = new System.Drawing.Point(82, 49);
            this.tbxCuacct.Name = "tbxCuacct";
            this.tbxCuacct.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxCuacct.Size = new System.Drawing.Size(100, 21);
            this.tbxCuacct.TabIndex = 22;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(11, 52);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 48;
            this.label17.Text = "资金账号：";
            // 
            // btnRegisterSHAStkAcct
            // 
            this.btnRegisterSHAStkAcct.Location = new System.Drawing.Point(294, 101);
            this.btnRegisterSHAStkAcct.Name = "btnRegisterSHAStkAcct";
            this.btnRegisterSHAStkAcct.Size = new System.Drawing.Size(100, 23);
            this.btnRegisterSHAStkAcct.TabIndex = 31;
            this.btnRegisterSHAStkAcct.Text = "加挂";
            this.btnRegisterSHAStkAcct.UseVisualStyleBackColor = true;
            this.btnRegisterSHAStkAcct.Click += new System.EventHandler(this.btnRegisterStockAccount_Click);
            // 
            // btnOpenSHAStkAcct
            // 
            this.btnOpenSHAStkAcct.Location = new System.Drawing.Point(188, 101);
            this.btnOpenSHAStkAcct.Name = "btnOpenSHAStkAcct";
            this.btnOpenSHAStkAcct.Size = new System.Drawing.Size(100, 23);
            this.btnOpenSHAStkAcct.TabIndex = 30;
            this.btnOpenSHAStkAcct.Text = "新开";
            this.btnOpenSHAStkAcct.UseVisualStyleBackColor = true;
            this.btnOpenSHAStkAcct.Click += new System.EventHandler(this.btnOpenSHAStkAcct_Click);
            // 
            // btnQueryStockAccount
            // 
            this.btnQueryStockAccount.Location = new System.Drawing.Point(815, 72);
            this.btnQueryStockAccount.Name = "btnQueryStockAccount";
            this.btnQueryStockAccount.Size = new System.Drawing.Size(100, 23);
            this.btnQueryStockAccount.TabIndex = 47;
            this.btnQueryStockAccount.Text = "中登账户查询";
            this.btnQueryStockAccount.UseVisualStyleBackColor = true;
            this.btnQueryStockAccount.Click += new System.EventHandler(this.btnQueryStockAccount_Click);
            // 
            // btnBankSign
            // 
            this.btnBankSign.Location = new System.Drawing.Point(506, 47);
            this.btnBankSign.Name = "btnBankSign";
            this.btnBankSign.Size = new System.Drawing.Size(100, 23);
            this.btnBankSign.TabIndex = 26;
            this.btnBankSign.Text = "签约三方存管";
            this.btnBankSign.UseVisualStyleBackColor = true;
            this.btnBankSign.Click += new System.EventHandler(this.btnBankSign_Click);
            // 
            // btnSubmitRiskTest
            // 
            this.btnSubmitRiskTest.Location = new System.Drawing.Point(400, 47);
            this.btnSubmitRiskTest.Name = "btnSubmitRiskTest";
            this.btnSubmitRiskTest.Size = new System.Drawing.Size(100, 23);
            this.btnSubmitRiskTest.TabIndex = 25;
            this.btnSubmitRiskTest.Text = "提交风险测评";
            this.btnSubmitRiskTest.UseVisualStyleBackColor = true;
            this.btnSubmitRiskTest.Click += new System.EventHandler(this.btnSubmitRiskTest_Click);
            // 
            // btnSetPassword
            // 
            this.btnSetPassword.Location = new System.Drawing.Point(294, 47);
            this.btnSetPassword.Name = "btnSetPassword";
            this.btnSetPassword.Size = new System.Drawing.Size(100, 23);
            this.btnSetPassword.TabIndex = 24;
            this.btnSetPassword.Text = "设置密码";
            this.btnSetPassword.UseVisualStyleBackColor = true;
            this.btnSetPassword.Click += new System.EventHandler(this.btnSetPassword_Click);
            // 
            // btnOpenCuacct
            // 
            this.btnOpenCuacct.Location = new System.Drawing.Point(188, 47);
            this.btnOpenCuacct.Name = "btnOpenCuacct";
            this.btnOpenCuacct.Size = new System.Drawing.Size(100, 23);
            this.btnOpenCuacct.TabIndex = 23;
            this.btnOpenCuacct.Text = "开资金账号";
            this.btnOpenCuacct.UseVisualStyleBackColor = true;
            this.btnOpenCuacct.Click += new System.EventHandler(this.btnOpenCuacct_Click);
            // 
            // risk_level
            // 
            this.risk_level.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.risk_level.FormattingEnabled = true;
            this.risk_level.Location = new System.Drawing.Point(460, 48);
            this.risk_level.Name = "risk_level";
            this.risk_level.Size = new System.Drawing.Size(100, 20);
            this.risk_level.TabIndex = 12;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(389, 51);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 38;
            this.label16.Text = "风测级别：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(390, 211);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 36;
            this.label15.Text = "存管银行：";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(389, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 34;
            this.label1.Text = "交易密码：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(460, 20);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(100, 21);
            this.password.TabIndex = 10;
            this.password.Text = "111111";
            // 
            // btnRecreateUserinfo
            // 
            this.btnRecreateUserinfo.Location = new System.Drawing.Point(815, 18);
            this.btnRecreateUserinfo.Name = "btnRecreateUserinfo";
            this.btnRecreateUserinfo.Size = new System.Drawing.Size(100, 23);
            this.btnRecreateUserinfo.TabIndex = 44;
            this.btnRecreateUserinfo.Text = "重新生成客户";
            this.btnRecreateUserinfo.UseVisualStyleBackColor = true;
            this.btnRecreateUserinfo.Click += new System.EventHandler(this.btnRecreateUserinfo_Click);
            // 
            // id_exp_date
            // 
            this.id_exp_date.CustomFormat = "yyyyMMdd";
            this.id_exp_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.id_exp_date.Location = new System.Drawing.Point(107, 128);
            this.id_exp_date.Name = "id_exp_date";
            this.id_exp_date.Size = new System.Drawing.Size(200, 21);
            this.id_exp_date.TabIndex = 5;
            this.id_exp_date.Value = new System.DateTime(2027, 1, 1, 0, 0, 0, 0);
            // 
            // id_beg_date
            // 
            this.id_beg_date.CustomFormat = "yyyyMMdd";
            this.id_beg_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.id_beg_date.Location = new System.Drawing.Point(107, 101);
            this.id_beg_date.Name = "id_beg_date";
            this.id_beg_date.Size = new System.Drawing.Size(200, 21);
            this.id_beg_date.TabIndex = 4;
            this.id_beg_date.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 104);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 12);
            this.label13.TabIndex = 29;
            this.label13.Text = "证件开始日期：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(36, 184);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 27;
            this.label12.Text = "移动电话：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mobile_tel
            // 
            this.mobile_tel.Location = new System.Drawing.Point(107, 181);
            this.mobile_tel.Name = "mobile_tel";
            this.mobile_tel.Size = new System.Drawing.Size(100, 21);
            this.mobile_tel.TabIndex = 8;
            this.mobile_tel.Text = "18655958868";
            // 
            // id_addr
            // 
            this.id_addr.Location = new System.Drawing.Point(107, 155);
            this.id_addr.Name = "id_addr";
            this.id_addr.Size = new System.Drawing.Size(261, 21);
            this.id_addr.TabIndex = 7;
            this.id_addr.Text = "安徽合肥蜀山区梅山路18号国际金融中心A座";
            // 
            // id_iss_agcy
            // 
            this.id_iss_agcy.Location = new System.Drawing.Point(107, 74);
            this.id_iss_agcy.Name = "id_iss_agcy";
            this.id_iss_agcy.Size = new System.Drawing.Size(261, 21);
            this.id_iss_agcy.TabIndex = 3;
            this.id_iss_agcy.Text = "合肥市公安局蜀山分局";
            // 
            // id_code
            // 
            this.id_code.Location = new System.Drawing.Point(107, 47);
            this.id_code.Name = "id_code";
            this.id_code.Size = new System.Drawing.Size(200, 21);
            this.id_code.TabIndex = 1;
            // 
            // user_name
            // 
            this.user_name.Location = new System.Drawing.Point(107, 20);
            this.user_name.Name = "user_name";
            this.user_name.Size = new System.Drawing.Size(261, 21);
            this.user_name.TabIndex = 0;
            // 
            // tbxCustCode
            // 
            this.tbxCustCode.Location = new System.Drawing.Point(82, 22);
            this.tbxCustCode.Name = "tbxCustCode";
            this.tbxCustCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxCustCode.Size = new System.Drawing.Size(100, 21);
            this.tbxCustCode.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(36, 158);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 25;
            this.label11.Text = "证件地址：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(413, 131);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "学历：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(413, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "职业：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(413, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "民族：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(413, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "国籍：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "证件有效日期：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "发证机关：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "身份证号码：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "客户名称：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOpenUserCode
            // 
            this.btnOpenUserCode.Location = new System.Drawing.Point(188, 20);
            this.btnOpenUserCode.Name = "btnOpenUserCode";
            this.btnOpenUserCode.Size = new System.Drawing.Size(100, 23);
            this.btnOpenUserCode.TabIndex = 21;
            this.btnOpenUserCode.Text = "开客户号";
            this.btnOpenUserCode.UseVisualStyleBackColor = true;
            this.btnOpenUserCode.Click += new System.EventHandler(this.btnOpenUserCode_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "客户号：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.user_name);
            this.groupBox1.Controls.Add(this.tbxBankAcctCode);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Controls.Add(this.zip_code);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.cbxCubsbScOpenAcctOpType);
            this.groupBox1.Controls.Add(this.btnQueryStockAccount);
            this.groupBox1.Controls.Add(this.nationality);
            this.groupBox1.Controls.Add(this.label32);
            this.groupBox1.Controls.Add(this.occu_type);
            this.groupBox1.Controls.Add(this.education);
            this.groupBox1.Controls.Add(this.tbCuacct_cls);
            this.groupBox1.Controls.Add(this.citizenship);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.sex);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.risk_level);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnValidateId);
            this.groupBox1.Controls.Add(this.tbChannels);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.bank_code);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxOccupation);
            this.groupBox1.Controls.Add(this.password);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnRecreateUserinfo);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.id_exp_date);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.id_beg_date);
            this.groupBox1.Controls.Add(this.cbxShortIdNo);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cbxOpenType);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.cbxLongTerm);
            this.groupBox1.Controls.Add(this.dtpCybSignDate);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.mobile_tel);
            this.groupBox1.Controls.Add(this.btnCreateIDCardImgBackSide);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.id_addr);
            this.groupBox1.Controls.Add(this.btnCreateIDCardImgFaceSide);
            this.groupBox1.Controls.Add(this.id_iss_agcy);
            this.groupBox1.Controls.Add(this.id_code);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(938, 243);
            this.groupBox1.TabIndex = 92;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置客户资料";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOpenUserCode);
            this.groupBox2.Controls.Add(this.btnOpenAccountByOneClick);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbxSZAcct);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.btnBindSHAcct);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.tbxCustCode);
            this.groupBox2.Controls.Add(this.tbxYMTCode);
            this.groupBox2.Controls.Add(this.tbxCybSignDate);
            this.groupBox2.Controls.Add(this.tbxSHAcct);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.btnOpenCYB);
            this.groupBox2.Controls.Add(this.btnRegisterSZAStkAcct);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.btnOpenSZAStkAcct);
            this.groupBox2.Controls.Add(this.btnOpenYMT);
            this.groupBox2.Controls.Add(this.btnQueryCYB);
            this.groupBox2.Controls.Add(this.tbxCuacct);
            this.groupBox2.Controls.Add(this.btnOpenCuacct);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.btnSetPassword);
            this.groupBox2.Controls.Add(this.btnRegisterSHAStkAcct);
            this.groupBox2.Controls.Add(this.btnSubmitRiskTest);
            this.groupBox2.Controls.Add(this.btnOpenSHAStkAcct);
            this.groupBox2.Controls.Add(this.btnBankSign);
            this.groupBox2.Location = new System.Drawing.Point(12, 261);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(937, 195);
            this.groupBox2.TabIndex = 93;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "开通账户";
            // 
            // frmNewAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 480);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmNewAccount";
            this.ShowIcon = false;
            this.Text = "新开账户";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnRecreateUserinfo;
        private System.Windows.Forms.DateTimePicker id_exp_date;
        private System.Windows.Forms.DateTimePicker id_beg_date;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox mobile_tel;
        private System.Windows.Forms.TextBox id_addr;
        private System.Windows.Forms.TextBox id_iss_agcy;
        private System.Windows.Forms.TextBox id_code;
        private System.Windows.Forms.TextBox user_name;
        private System.Windows.Forms.TextBox tbxCustCode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenUserCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox risk_level;
        private System.Windows.Forms.Button btnOpenSHAStkAcct;
        private System.Windows.Forms.Button btnQueryStockAccount;
        private System.Windows.Forms.Button btnBankSign;
        private System.Windows.Forms.Button btnSubmitRiskTest;
        private System.Windows.Forms.Button btnSetPassword;
        private System.Windows.Forms.Button btnOpenCuacct;
        private System.Windows.Forms.Button btnRegisterSHAStkAcct;
        private System.Windows.Forms.TextBox tbxCuacct;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbxSZAcct;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbxSHAcct;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnOpenAccountByOneClick;
        private System.Windows.Forms.TextBox tbxYMTCode;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnOpenCYB;
        private System.Windows.Forms.Button btnOpenYMT;
        private System.Windows.Forms.TextBox zip_code;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox citizenship;
        private System.Windows.Forms.ComboBox education;
        private System.Windows.Forms.ComboBox occu_type;
        private System.Windows.Forms.ComboBox nationality;
        private System.Windows.Forms.ComboBox sex;
        private System.Windows.Forms.Button btnValidateId;
        private System.Windows.Forms.ComboBox bank_code;
        private System.Windows.Forms.Button btnQueryCYB;
        private System.Windows.Forms.Button btnRegisterSZAStkAcct;
        private System.Windows.Forms.Button btnOpenSZAStkAcct;
        private System.Windows.Forms.ComboBox cbxOpenType;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.DateTimePicker dtpCybSignDate;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox tbxCybSignDate;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCreateIDCardImgBackSide;
        private System.Windows.Forms.Button btnCreateIDCardImgFaceSide;
        private System.Windows.Forms.CheckBox cbxLongTerm;
        private System.Windows.Forms.Button btnBindSHAcct;
        private System.Windows.Forms.CheckBox cbxShortIdNo;
        private System.Windows.Forms.ComboBox cbxOccupation;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox tbChannels;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox tbCuacct_cls;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ComboBox cbxCubsbScOpenAcctOpType;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox tbxBankAcctCode;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

