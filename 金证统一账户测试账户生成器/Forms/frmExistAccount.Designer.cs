using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace 金证统一账户测试账户生成器
{
    partial class frmExistAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExistAccount));
            this.cbxCubsbScOpenAcctOpType = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.tbCuacct_cls = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.tbChannels = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.cbxOccupation = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.btnBindSHAcct = new System.Windows.Forms.Button();
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
            this.btnQueryByUserCode = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.tbxBankAcctCode = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.address = new System.Windows.Forms.TextBox();
            this.id_beg_date = new System.Windows.Forms.TextBox();
            this.id_exp_date = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxCuacctCondition = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbxFislCuacct = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.tc用户信息 = new System.Windows.Forms.TabControl();
            this.tp基本资料 = new System.Windows.Forms.TabPage();
            this.tp诚信记录 = new System.Windows.Forms.TabPage();
            this.dgv诚信记录 = new System.Windows.Forms.DataGridView();
            this.tp受益人 = new System.Windows.Forms.TabPage();
            this.dgv受益人 = new System.Windows.Forms.DataGridView();
            this.tp控制人 = new System.Windows.Forms.TabPage();
            this.dgv控制人 = new System.Windows.Forms.DataGridView();
            this.tp已签署协议 = new System.Windows.Forms.TabPage();
            this.dgv已签署协议 = new System.Windows.Forms.DataGridView();
            this.CUST_AGMT_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REMOTE_SYS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EFT_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXP_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPD_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUACCT_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STKBD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRDACCT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUST_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tc用户信息.SuspendLayout();
            this.tp基本资料.SuspendLayout();
            this.tp诚信记录.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv诚信记录)).BeginInit();
            this.tp受益人.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv受益人)).BeginInit();
            this.tp控制人.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv控制人)).BeginInit();
            this.tp已签署协议.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv已签署协议)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxCubsbScOpenAcctOpType
            // 
            this.cbxCubsbScOpenAcctOpType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCubsbScOpenAcctOpType.FormattingEnabled = true;
            this.cbxCubsbScOpenAcctOpType.Items.AddRange(new object[] {
            "预指定",
            "一步式"});
            this.cbxCubsbScOpenAcctOpType.Location = new System.Drawing.Point(460, 174);
            this.cbxCubsbScOpenAcctOpType.Name = "cbxCubsbScOpenAcctOpType";
            this.cbxCubsbScOpenAcctOpType.Size = new System.Drawing.Size(100, 20);
            this.cbxCubsbScOpenAcctOpType.TabIndex = 15;
            this.cbxCubsbScOpenAcctOpType.SelectedIndexChanged += new System.EventHandler(this.cbxCubsbScOpenAcctOpType_SelectedIndexChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(389, 177);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(65, 12);
            this.label32.TabIndex = 89;
            this.label32.Text = "存管类型：";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbCuacct_cls
            // 
            this.tbCuacct_cls.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::金证统一账户测试账户生成器.Properties.Settings.Default, "默认开通的资产账户类别", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbCuacct_cls.Location = new System.Drawing.Point(817, 22);
            this.tbCuacct_cls.Name = "tbCuacct_cls";
            this.tbCuacct_cls.Size = new System.Drawing.Size(100, 21);
            this.tbCuacct_cls.TabIndex = 21;
            this.tbCuacct_cls.Text = global::金证统一账户测试账户生成器.Properties.Settings.Default.默认开通的资产账户类别;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(722, 25);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(89, 12);
            this.label31.TabIndex = 86;
            this.label31.Text = "资产账户类别：";
            // 
            // tbChannels
            // 
            this.tbChannels.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::金证统一账户测试账户生成器.Properties.Settings.Default, "默认开通的操作渠道", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbChannels.Location = new System.Drawing.Point(817, 48);
            this.tbChannels.Name = "tbChannels";
            this.tbChannels.Size = new System.Drawing.Size(100, 21);
            this.tbChannels.TabIndex = 22;
            this.tbChannels.Text = global::金证统一账户测试账户生成器.Properties.Settings.Default.默认开通的操作渠道;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(746, 51);
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
            this.cbxOccupation.Location = new System.Drawing.Point(658, 97);
            this.cbxOccupation.Name = "cbxOccupation";
            this.cbxOccupation.Size = new System.Drawing.Size(136, 20);
            this.cbxOccupation.TabIndex = 20;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(587, 99);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(65, 12);
            this.label29.TabIndex = 82;
            this.label29.Text = "手输职业：";
            // 
            // btnBindSHAcct
            // 
            this.btnBindSHAcct.Location = new System.Drawing.Point(429, 74);
            this.btnBindSHAcct.Name = "btnBindSHAcct";
            this.btnBindSHAcct.Size = new System.Drawing.Size(100, 23);
            this.btnBindSHAcct.TabIndex = 10;
            this.btnBindSHAcct.Text = "指定交易";
            this.btnBindSHAcct.UseVisualStyleBackColor = true;
            this.btnBindSHAcct.Click += new System.EventHandler(this.btnBindSHAcct_Click);
            // 
            // btnCreateIDCardImgBackSide
            // 
            this.btnCreateIDCardImgBackSide.Location = new System.Drawing.Point(814, 66);
            this.btnCreateIDCardImgBackSide.Name = "btnCreateIDCardImgBackSide";
            this.btnCreateIDCardImgBackSide.Size = new System.Drawing.Size(100, 23);
            this.btnCreateIDCardImgBackSide.TabIndex = 26;
            this.btnCreateIDCardImgBackSide.Text = "生成身份证背面";
            this.btnCreateIDCardImgBackSide.UseVisualStyleBackColor = true;
            this.btnCreateIDCardImgBackSide.Click += new System.EventHandler(this.btnCreateIDCardImgBackSide_Click);
            // 
            // btnCreateIDCardImgFaceSide
            // 
            this.btnCreateIDCardImgFaceSide.Location = new System.Drawing.Point(814, 39);
            this.btnCreateIDCardImgFaceSide.Name = "btnCreateIDCardImgFaceSide";
            this.btnCreateIDCardImgFaceSide.Size = new System.Drawing.Size(100, 23);
            this.btnCreateIDCardImgFaceSide.TabIndex = 25;
            this.btnCreateIDCardImgFaceSide.Text = "生成身份证正面";
            this.btnCreateIDCardImgFaceSide.UseVisualStyleBackColor = true;
            this.btnCreateIDCardImgFaceSide.Click += new System.EventHandler(this.btnCreateIDCardImg_Click);
            // 
            // tbxCybSignDate
            // 
            this.tbxCybSignDate.Location = new System.Drawing.Point(729, 103);
            this.tbxCybSignDate.Name = "tbxCybSignDate";
            this.tbxCybSignDate.ReadOnly = true;
            this.tbxCybSignDate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxCybSignDate.Size = new System.Drawing.Size(188, 21);
            this.tbxCybSignDate.TabIndex = 16;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(646, 106);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(77, 12);
            this.label25.TabIndex = 80;
            this.label25.Text = "创业板信息：";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(587, 70);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(101, 12);
            this.label24.TabIndex = 79;
            this.label24.Text = "创业板签约类型：";
            // 
            // dtpCybSignDate
            // 
            this.dtpCybSignDate.CustomFormat = "yyyyMMdd";
            this.dtpCybSignDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCybSignDate.Location = new System.Drawing.Point(694, 40);
            this.dtpCybSignDate.Name = "dtpCybSignDate";
            this.dtpCybSignDate.Size = new System.Drawing.Size(100, 21);
            this.dtpCybSignDate.TabIndex = 18;
            this.dtpCybSignDate.Value = new System.DateTime(2017, 8, 1, 0, 0, 0, 0);
            // 
            // cbxOpenType
            // 
            this.cbxOpenType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOpenType.FormattingEnabled = true;
            this.cbxOpenType.Location = new System.Drawing.Point(694, 67);
            this.cbxOpenType.Name = "cbxOpenType";
            this.cbxOpenType.Size = new System.Drawing.Size(100, 20);
            this.cbxOpenType.TabIndex = 19;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(587, 43);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(101, 12);
            this.label23.TabIndex = 76;
            this.label23.Text = "创业板签约日期：";
            // 
            // btnRegisterSZAStkAcct
            // 
            this.btnRegisterSZAStkAcct.Location = new System.Drawing.Point(323, 101);
            this.btnRegisterSZAStkAcct.Name = "btnRegisterSZAStkAcct";
            this.btnRegisterSZAStkAcct.Size = new System.Drawing.Size(100, 23);
            this.btnRegisterSZAStkAcct.TabIndex = 13;
            this.btnRegisterSZAStkAcct.Text = "加挂";
            this.btnRegisterSZAStkAcct.UseVisualStyleBackColor = true;
            this.btnRegisterSZAStkAcct.Click += new System.EventHandler(this.btnRegisterSZAStkAcct_Click);
            // 
            // btnOpenSZAStkAcct
            // 
            this.btnOpenSZAStkAcct.Location = new System.Drawing.Point(217, 101);
            this.btnOpenSZAStkAcct.Name = "btnOpenSZAStkAcct";
            this.btnOpenSZAStkAcct.Size = new System.Drawing.Size(100, 23);
            this.btnOpenSZAStkAcct.TabIndex = 12;
            this.btnOpenSZAStkAcct.Text = "新开";
            this.btnOpenSZAStkAcct.UseVisualStyleBackColor = true;
            this.btnOpenSZAStkAcct.Click += new System.EventHandler(this.btnOpenSZAStkAcct_Click);
            // 
            // btnQueryCYB
            // 
            this.btnQueryCYB.Location = new System.Drawing.Point(535, 101);
            this.btnQueryCYB.Name = "btnQueryCYB";
            this.btnQueryCYB.Size = new System.Drawing.Size(100, 23);
            this.btnQueryCYB.TabIndex = 15;
            this.btnQueryCYB.Text = "创业板信息查询";
            this.btnQueryCYB.UseVisualStyleBackColor = true;
            this.btnQueryCYB.Click += new System.EventHandler(this.btnQueryCYB_Click);
            // 
            // bank_code
            // 
            this.bank_code.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bank_code.FormattingEnabled = true;
            this.bank_code.Location = new System.Drawing.Point(461, 201);
            this.bank_code.Name = "bank_code";
            this.bank_code.Size = new System.Drawing.Size(100, 20);
            this.bank_code.TabIndex = 16;
            // 
            // sex
            // 
            this.sex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sex.FormattingEnabled = true;
            this.sex.Location = new System.Drawing.Point(694, 13);
            this.sex.Name = "sex";
            this.sex.Size = new System.Drawing.Size(100, 20);
            this.sex.TabIndex = 17;
            // 
            // citizenship
            // 
            this.citizenship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.citizenship.FormattingEnabled = true;
            this.citizenship.Location = new System.Drawing.Point(460, 148);
            this.citizenship.Name = "citizenship";
            this.citizenship.Size = new System.Drawing.Size(100, 20);
            this.citizenship.TabIndex = 14;
            // 
            // education
            // 
            this.education.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.education.FormattingEnabled = true;
            this.education.Location = new System.Drawing.Point(460, 120);
            this.education.Name = "education";
            this.education.Size = new System.Drawing.Size(100, 20);
            this.education.TabIndex = 13;
            // 
            // occu_type
            // 
            this.occu_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.occu_type.FormattingEnabled = true;
            this.occu_type.Location = new System.Drawing.Point(460, 94);
            this.occu_type.Name = "occu_type";
            this.occu_type.Size = new System.Drawing.Size(100, 20);
            this.occu_type.TabIndex = 12;
            this.occu_type.SelectedIndexChanged += new System.EventHandler(this.occu_type_SelectedIndexChanged);
            // 
            // nationality
            // 
            this.nationality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nationality.FormattingEnabled = true;
            this.nationality.Location = new System.Drawing.Point(460, 67);
            this.nationality.Name = "nationality";
            this.nationality.Size = new System.Drawing.Size(100, 20);
            this.nationality.TabIndex = 11;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(647, 16);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 12);
            this.label22.TabIndex = 62;
            this.label22.Text = "性别：";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // zip_code
            // 
            this.zip_code.Location = new System.Drawing.Point(268, 174);
            this.zip_code.Name = "zip_code";
            this.zip_code.Size = new System.Drawing.Size(100, 21);
            this.zip_code.TabIndex = 7;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(221, 177);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(41, 12);
            this.label21.TabIndex = 59;
            this.label21.Text = "邮编：";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOpenYMT
            // 
            this.btnOpenYMT.Location = new System.Drawing.Point(217, 47);
            this.btnOpenYMT.Name = "btnOpenYMT";
            this.btnOpenYMT.Size = new System.Drawing.Size(100, 23);
            this.btnOpenYMT.TabIndex = 6;
            this.btnOpenYMT.Text = "开一码通";
            this.btnOpenYMT.UseVisualStyleBackColor = true;
            this.btnOpenYMT.Click += new System.EventHandler(this.btnOpenYMT_Click);
            // 
            // btnOpenCYB
            // 
            this.btnOpenCYB.Location = new System.Drawing.Point(429, 101);
            this.btnOpenCYB.Name = "btnOpenCYB";
            this.btnOpenCYB.Size = new System.Drawing.Size(100, 23);
            this.btnOpenCYB.TabIndex = 14;
            this.btnOpenCYB.Text = "开通创业板";
            this.btnOpenCYB.UseVisualStyleBackColor = true;
            this.btnOpenCYB.Click += new System.EventHandler(this.btnOpenCYB_Click);
            // 
            // tbxYMTCode
            // 
            this.tbxYMTCode.Location = new System.Drawing.Point(111, 49);
            this.tbxYMTCode.Name = "tbxYMTCode";
            this.tbxYMTCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxYMTCode.Size = new System.Drawing.Size(100, 21);
            this.tbxYMTCode.TabIndex = 5;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(52, 52);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 12);
            this.label20.TabIndex = 55;
            this.label20.Text = "一码通：";
            // 
            // tbxSZAcct
            // 
            this.tbxSZAcct.Location = new System.Drawing.Point(111, 103);
            this.tbxSZAcct.Name = "tbxSZAcct";
            this.tbxSZAcct.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxSZAcct.Size = new System.Drawing.Size(100, 21);
            this.tbxSZAcct.TabIndex = 11;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(46, 106);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 12);
            this.label19.TabIndex = 52;
            this.label19.Text = "深圳A股：";
            // 
            // tbxSHAcct
            // 
            this.tbxSHAcct.Location = new System.Drawing.Point(111, 76);
            this.tbxSHAcct.Name = "tbxSHAcct";
            this.tbxSHAcct.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxSHAcct.Size = new System.Drawing.Size(100, 21);
            this.tbxSHAcct.TabIndex = 7;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(46, 79);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 12);
            this.label18.TabIndex = 50;
            this.label18.Text = "上海A股：";
            // 
            // tbxCuacct
            // 
            this.tbxCuacct.Location = new System.Drawing.Point(111, 22);
            this.tbxCuacct.Name = "tbxCuacct";
            this.tbxCuacct.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxCuacct.Size = new System.Drawing.Size(100, 21);
            this.tbxCuacct.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(40, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 48;
            this.label17.Text = "资金账号：";
            // 
            // btnRegisterSHAStkAcct
            // 
            this.btnRegisterSHAStkAcct.Location = new System.Drawing.Point(323, 74);
            this.btnRegisterSHAStkAcct.Name = "btnRegisterSHAStkAcct";
            this.btnRegisterSHAStkAcct.Size = new System.Drawing.Size(100, 23);
            this.btnRegisterSHAStkAcct.TabIndex = 9;
            this.btnRegisterSHAStkAcct.Text = "加挂";
            this.btnRegisterSHAStkAcct.UseVisualStyleBackColor = true;
            this.btnRegisterSHAStkAcct.Click += new System.EventHandler(this.btnRegisterStockAccount_Click);
            // 
            // btnOpenSHAStkAcct
            // 
            this.btnOpenSHAStkAcct.Location = new System.Drawing.Point(217, 74);
            this.btnOpenSHAStkAcct.Name = "btnOpenSHAStkAcct";
            this.btnOpenSHAStkAcct.Size = new System.Drawing.Size(100, 23);
            this.btnOpenSHAStkAcct.TabIndex = 8;
            this.btnOpenSHAStkAcct.Text = "新开";
            this.btnOpenSHAStkAcct.UseVisualStyleBackColor = true;
            this.btnOpenSHAStkAcct.Click += new System.EventHandler(this.btnOpenSHAStkAcct_Click);
            // 
            // btnQueryStockAccount
            // 
            this.btnQueryStockAccount.Location = new System.Drawing.Point(814, 11);
            this.btnQueryStockAccount.Name = "btnQueryStockAccount";
            this.btnQueryStockAccount.Size = new System.Drawing.Size(100, 23);
            this.btnQueryStockAccount.TabIndex = 24;
            this.btnQueryStockAccount.Text = "证券账户查询";
            this.btnQueryStockAccount.UseVisualStyleBackColor = true;
            this.btnQueryStockAccount.Click += new System.EventHandler(this.btnQueryStockAccount_Click);
            // 
            // btnBankSign
            // 
            this.btnBankSign.Location = new System.Drawing.Point(535, 20);
            this.btnBankSign.Name = "btnBankSign";
            this.btnBankSign.Size = new System.Drawing.Size(100, 23);
            this.btnBankSign.TabIndex = 4;
            this.btnBankSign.Text = "三方存管预指定";
            this.btnBankSign.UseVisualStyleBackColor = true;
            this.btnBankSign.Click += new System.EventHandler(this.btnBankSign_Click);
            // 
            // btnSubmitRiskTest
            // 
            this.btnSubmitRiskTest.Location = new System.Drawing.Point(429, 20);
            this.btnSubmitRiskTest.Name = "btnSubmitRiskTest";
            this.btnSubmitRiskTest.Size = new System.Drawing.Size(100, 23);
            this.btnSubmitRiskTest.TabIndex = 3;
            this.btnSubmitRiskTest.Text = "提交风险测评";
            this.btnSubmitRiskTest.UseVisualStyleBackColor = true;
            this.btnSubmitRiskTest.Click += new System.EventHandler(this.btnSubmitRiskTest_Click);
            // 
            // btnSetPassword
            // 
            this.btnSetPassword.Location = new System.Drawing.Point(323, 20);
            this.btnSetPassword.Name = "btnSetPassword";
            this.btnSetPassword.Size = new System.Drawing.Size(100, 23);
            this.btnSetPassword.TabIndex = 2;
            this.btnSetPassword.Text = "重置密码";
            this.btnSetPassword.UseVisualStyleBackColor = true;
            this.btnSetPassword.Click += new System.EventHandler(this.btnSetPassword_Click);
            // 
            // btnOpenCuacct
            // 
            this.btnOpenCuacct.Location = new System.Drawing.Point(217, 20);
            this.btnOpenCuacct.Name = "btnOpenCuacct";
            this.btnOpenCuacct.Size = new System.Drawing.Size(100, 23);
            this.btnOpenCuacct.TabIndex = 1;
            this.btnOpenCuacct.Text = "开资金账号";
            this.btnOpenCuacct.UseVisualStyleBackColor = true;
            this.btnOpenCuacct.Click += new System.EventHandler(this.btnOpenCuacct_Click);
            // 
            // risk_level
            // 
            this.risk_level.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.risk_level.FormattingEnabled = true;
            this.risk_level.Location = new System.Drawing.Point(460, 41);
            this.risk_level.Name = "risk_level";
            this.risk_level.Size = new System.Drawing.Size(100, 20);
            this.risk_level.TabIndex = 10;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(389, 44);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 38;
            this.label16.Text = "风测级别：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(390, 204);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 36;
            this.label15.Text = "存管银行：";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(389, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 34;
            this.label1.Text = "交易密码：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(460, 13);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(100, 21);
            this.password.TabIndex = 9;
            this.password.Text = "111111";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 12);
            this.label13.TabIndex = 29;
            this.label13.Text = "证件开始日期：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(36, 177);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 27;
            this.label12.Text = "移动电话：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mobile_tel
            // 
            this.mobile_tel.Location = new System.Drawing.Point(107, 174);
            this.mobile_tel.Name = "mobile_tel";
            this.mobile_tel.Size = new System.Drawing.Size(100, 21);
            this.mobile_tel.TabIndex = 6;
            // 
            // id_addr
            // 
            this.id_addr.Location = new System.Drawing.Point(107, 148);
            this.id_addr.Name = "id_addr";
            this.id_addr.Size = new System.Drawing.Size(261, 21);
            this.id_addr.TabIndex = 5;
            // 
            // id_iss_agcy
            // 
            this.id_iss_agcy.Location = new System.Drawing.Point(107, 67);
            this.id_iss_agcy.Name = "id_iss_agcy";
            this.id_iss_agcy.Size = new System.Drawing.Size(261, 21);
            this.id_iss_agcy.TabIndex = 2;
            // 
            // id_code
            // 
            this.id_code.Location = new System.Drawing.Point(107, 40);
            this.id_code.Name = "id_code";
            this.id_code.Size = new System.Drawing.Size(261, 21);
            this.id_code.TabIndex = 1;
            // 
            // user_name
            // 
            this.user_name.Location = new System.Drawing.Point(107, 13);
            this.user_name.Name = "user_name";
            this.user_name.Size = new System.Drawing.Size(261, 21);
            this.user_name.TabIndex = 0;
            // 
            // tbxCustCode
            // 
            this.tbxCustCode.Location = new System.Drawing.Point(68, 21);
            this.tbxCustCode.Name = "tbxCustCode";
            this.tbxCustCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxCustCode.Size = new System.Drawing.Size(100, 21);
            this.tbxCustCode.TabIndex = 0;
            this.tbxCustCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxCustCode_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(36, 151);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 25;
            this.label11.Text = "证件地址：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(413, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "学历：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(413, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "职业：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(413, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "民族：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(413, 151);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "国籍：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "证件有效日期：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "发证机关：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "证件号码：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "客户名称：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnQueryByUserCode
            // 
            this.btnQueryByUserCode.Location = new System.Drawing.Point(357, 19);
            this.btnQueryByUserCode.Name = "btnQueryByUserCode";
            this.btnQueryByUserCode.Size = new System.Drawing.Size(100, 23);
            this.btnQueryByUserCode.TabIndex = 2;
            this.btnQueryByUserCode.Text = "查询";
            this.btnQueryByUserCode.UseVisualStyleBackColor = true;
            this.btnQueryByUserCode.Click += new System.EventHandler(this.btnQueryByUserCode_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 24);
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
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(567, 204);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(65, 12);
            this.label33.TabIndex = 90;
            this.label33.Text = "银行卡号：";
            // 
            // tbxBankAcctCode
            // 
            this.tbxBankAcctCode.Enabled = false;
            this.tbxBankAcctCode.Location = new System.Drawing.Point(637, 201);
            this.tbxBankAcctCode.Name = "tbxBankAcctCode";
            this.tbxBankAcctCode.Size = new System.Drawing.Size(157, 21);
            this.tbxBankAcctCode.TabIndex = 23;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(37, 204);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 93;
            this.label14.Text = "联系地址：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // address
            // 
            this.address.Location = new System.Drawing.Point(108, 201);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(261, 21);
            this.address.TabIndex = 8;
            // 
            // id_beg_date
            // 
            this.id_beg_date.Location = new System.Drawing.Point(108, 94);
            this.id_beg_date.Name = "id_beg_date";
            this.id_beg_date.Size = new System.Drawing.Size(261, 21);
            this.id_beg_date.TabIndex = 3;
            // 
            // id_exp_date
            // 
            this.id_exp_date.Location = new System.Drawing.Point(107, 120);
            this.id_exp_date.Name = "id_exp_date";
            this.id_exp_date.Size = new System.Drawing.Size(261, 21);
            this.id_exp_date.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxCuacctCondition);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.tbxCustCode);
            this.groupBox1.Controls.Add(this.btnQueryByUserCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(943, 51);
            this.groupBox1.TabIndex = 96;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // tbxCuacctCondition
            // 
            this.tbxCuacctCondition.Location = new System.Drawing.Point(251, 21);
            this.tbxCuacctCondition.Name = "tbxCuacctCondition";
            this.tbxCuacctCondition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxCuacctCondition.Size = new System.Drawing.Size(100, 21);
            this.tbxCuacctCondition.TabIndex = 1;
            this.tbxCuacctCondition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxCuacctCondition_KeyDown);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(180, 24);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(65, 12);
            this.label26.TabIndex = 22;
            this.label26.Text = "资金账号：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnOpenCuacct);
            this.groupBox3.Controls.Add(this.tbxSZAcct);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.btnBindSHAcct);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.tbxCybSignDate);
            this.groupBox3.Controls.Add(this.tbxYMTCode);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.tbxSHAcct);
            this.groupBox3.Controls.Add(this.btnRegisterSZAStkAcct);
            this.groupBox3.Controls.Add(this.btnOpenCYB);
            this.groupBox3.Controls.Add(this.btnOpenSZAStkAcct);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.btnQueryCYB);
            this.groupBox3.Controls.Add(this.btnOpenYMT);
            this.groupBox3.Controls.Add(this.tbxCuacct);
            this.groupBox3.Controls.Add(this.btnSetPassword);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.btnSubmitRiskTest);
            this.groupBox3.Controls.Add(this.btnRegisterSHAStkAcct);
            this.groupBox3.Controls.Add(this.btnBankSign);
            this.groupBox3.Controls.Add(this.btnOpenSHAStkAcct);
            this.groupBox3.Controls.Add(this.tbCuacct_cls);
            this.groupBox3.Controls.Add(this.label31);
            this.groupBox3.Controls.Add(this.tbChannels);
            this.groupBox3.Controls.Add(this.label30);
            this.groupBox3.Location = new System.Drawing.Point(12, 340);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(943, 140);
            this.groupBox3.TabIndex = 98;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "普通账户";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbxFislCuacct);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Location = new System.Drawing.Point(12, 487);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(943, 57);
            this.groupBox4.TabIndex = 99;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "信用账户";
            this.groupBox4.Visible = false;
            // 
            // tbxFislCuacct
            // 
            this.tbxFislCuacct.Location = new System.Drawing.Point(111, 20);
            this.tbxFislCuacct.Name = "tbxFislCuacct";
            this.tbxFislCuacct.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxFislCuacct.Size = new System.Drawing.Size(100, 21);
            this.tbxFislCuacct.TabIndex = 49;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(40, 23);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(65, 12);
            this.label27.TabIndex = 50;
            this.label27.Text = "资金账号：";
            // 
            // tc用户信息
            // 
            this.tc用户信息.Controls.Add(this.tp基本资料);
            this.tc用户信息.Controls.Add(this.tp诚信记录);
            this.tc用户信息.Controls.Add(this.tp受益人);
            this.tc用户信息.Controls.Add(this.tp控制人);
            this.tc用户信息.Controls.Add(this.tp已签署协议);
            this.tc用户信息.Location = new System.Drawing.Point(12, 69);
            this.tc用户信息.Name = "tc用户信息";
            this.tc用户信息.SelectedIndex = 0;
            this.tc用户信息.Size = new System.Drawing.Size(943, 265);
            this.tc用户信息.TabIndex = 97;
            // 
            // tp基本资料
            // 
            this.tp基本资料.Controls.Add(this.user_name);
            this.tp基本资料.Controls.Add(this.btnCreateIDCardImgBackSide);
            this.tp基本资料.Controls.Add(this.btnCreateIDCardImgFaceSide);
            this.tp基本资料.Controls.Add(this.label11);
            this.tp基本资料.Controls.Add(this.label21);
            this.tp基本资料.Controls.Add(this.id_code);
            this.tp基本资料.Controls.Add(this.id_exp_date);
            this.tp基本资料.Controls.Add(this.label10);
            this.tp基本资料.Controls.Add(this.zip_code);
            this.tp基本资料.Controls.Add(this.id_iss_agcy);
            this.tp基本资料.Controls.Add(this.id_beg_date);
            this.tp基本资料.Controls.Add(this.id_addr);
            this.tp基本资料.Controls.Add(this.label22);
            this.tp基本资料.Controls.Add(this.label9);
            this.tp基本资料.Controls.Add(this.label14);
            this.tp基本资料.Controls.Add(this.label24);
            this.tp基本资料.Controls.Add(this.btnQueryStockAccount);
            this.tp基本资料.Controls.Add(this.label8);
            this.tp基本资料.Controls.Add(this.address);
            this.tp基本资料.Controls.Add(this.mobile_tel);
            this.tp基本资料.Controls.Add(this.nationality);
            this.tp基本资料.Controls.Add(this.label29);
            this.tp基本资料.Controls.Add(this.tbxBankAcctCode);
            this.tp基本资料.Controls.Add(this.dtpCybSignDate);
            this.tp基本资料.Controls.Add(this.occu_type);
            this.tp基本资料.Controls.Add(this.label7);
            this.tp基本资料.Controls.Add(this.label33);
            this.tp基本资料.Controls.Add(this.label12);
            this.tp基本资料.Controls.Add(this.education);
            this.tp基本资料.Controls.Add(this.cbxOccupation);
            this.tp基本资料.Controls.Add(this.citizenship);
            this.tp基本资料.Controls.Add(this.cbxOpenType);
            this.tp基本资料.Controls.Add(this.cbxCubsbScOpenAcctOpType);
            this.tp基本资料.Controls.Add(this.label6);
            this.tp基本资料.Controls.Add(this.sex);
            this.tp基本资料.Controls.Add(this.label13);
            this.tp基本资料.Controls.Add(this.label32);
            this.tp基本资料.Controls.Add(this.risk_level);
            this.tp基本资料.Controls.Add(this.label23);
            this.tp基本资料.Controls.Add(this.label5);
            this.tp基本资料.Controls.Add(this.label16);
            this.tp基本资料.Controls.Add(this.password);
            this.tp基本资料.Controls.Add(this.label2);
            this.tp基本资料.Controls.Add(this.bank_code);
            this.tp基本资料.Controls.Add(this.label1);
            this.tp基本资料.Controls.Add(this.label4);
            this.tp基本资料.Controls.Add(this.label15);
            this.tp基本资料.Location = new System.Drawing.Point(4, 22);
            this.tp基本资料.Name = "tp基本资料";
            this.tp基本资料.Padding = new System.Windows.Forms.Padding(3);
            this.tp基本资料.Size = new System.Drawing.Size(935, 239);
            this.tp基本资料.TabIndex = 0;
            this.tp基本资料.Text = "基本资料";
            this.tp基本资料.UseVisualStyleBackColor = true;
            // 
            // tp诚信记录
            // 
            this.tp诚信记录.Controls.Add(this.dgv诚信记录);
            this.tp诚信记录.Location = new System.Drawing.Point(4, 22);
            this.tp诚信记录.Name = "tp诚信记录";
            this.tp诚信记录.Padding = new System.Windows.Forms.Padding(3);
            this.tp诚信记录.Size = new System.Drawing.Size(935, 239);
            this.tp诚信记录.TabIndex = 3;
            this.tp诚信记录.Text = "诚信记录";
            this.tp诚信记录.UseVisualStyleBackColor = true;
            // 
            // dgv诚信记录
            // 
            this.dgv诚信记录.AllowUserToAddRows = false;
            this.dgv诚信记录.AllowUserToDeleteRows = false;
            this.dgv诚信记录.AllowUserToResizeRows = false;
            this.dgv诚信记录.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv诚信记录.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv诚信记录.Location = new System.Drawing.Point(6, 6);
            this.dgv诚信记录.Name = "dgv诚信记录";
            this.dgv诚信记录.ReadOnly = true;
            this.dgv诚信记录.RowHeadersVisible = false;
            this.dgv诚信记录.RowTemplate.Height = 23;
            this.dgv诚信记录.Size = new System.Drawing.Size(923, 227);
            this.dgv诚信记录.TabIndex = 0;
            // 
            // tp受益人
            // 
            this.tp受益人.Controls.Add(this.dgv受益人);
            this.tp受益人.Location = new System.Drawing.Point(4, 22);
            this.tp受益人.Name = "tp受益人";
            this.tp受益人.Padding = new System.Windows.Forms.Padding(3);
            this.tp受益人.Size = new System.Drawing.Size(935, 239);
            this.tp受益人.TabIndex = 1;
            this.tp受益人.Text = "受益人";
            this.tp受益人.UseVisualStyleBackColor = true;
            // 
            // dgv受益人
            // 
            this.dgv受益人.AllowUserToAddRows = false;
            this.dgv受益人.AllowUserToDeleteRows = false;
            this.dgv受益人.AllowUserToResizeRows = false;
            this.dgv受益人.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv受益人.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv受益人.Location = new System.Drawing.Point(6, 6);
            this.dgv受益人.Name = "dgv受益人";
            this.dgv受益人.ReadOnly = true;
            this.dgv受益人.RowHeadersVisible = false;
            this.dgv受益人.RowTemplate.Height = 23;
            this.dgv受益人.Size = new System.Drawing.Size(923, 227);
            this.dgv受益人.TabIndex = 1;
            // 
            // tp控制人
            // 
            this.tp控制人.Controls.Add(this.dgv控制人);
            this.tp控制人.Location = new System.Drawing.Point(4, 22);
            this.tp控制人.Name = "tp控制人";
            this.tp控制人.Padding = new System.Windows.Forms.Padding(3);
            this.tp控制人.Size = new System.Drawing.Size(935, 239);
            this.tp控制人.TabIndex = 2;
            this.tp控制人.Text = "控制人";
            this.tp控制人.UseVisualStyleBackColor = true;
            // 
            // dgv控制人
            // 
            this.dgv控制人.AllowUserToAddRows = false;
            this.dgv控制人.AllowUserToDeleteRows = false;
            this.dgv控制人.AllowUserToResizeRows = false;
            this.dgv控制人.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv控制人.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv控制人.Location = new System.Drawing.Point(6, 6);
            this.dgv控制人.Name = "dgv控制人";
            this.dgv控制人.ReadOnly = true;
            this.dgv控制人.RowHeadersVisible = false;
            this.dgv控制人.RowTemplate.Height = 23;
            this.dgv控制人.Size = new System.Drawing.Size(923, 227);
            this.dgv控制人.TabIndex = 1;
            // 
            // tp已签署协议
            // 
            this.tp已签署协议.Controls.Add(this.dgv已签署协议);
            this.tp已签署协议.Location = new System.Drawing.Point(4, 22);
            this.tp已签署协议.Name = "tp已签署协议";
            this.tp已签署协议.Padding = new System.Windows.Forms.Padding(3);
            this.tp已签署协议.Size = new System.Drawing.Size(935, 239);
            this.tp已签署协议.TabIndex = 4;
            this.tp已签署协议.Text = "已签署协议";
            this.tp已签署协议.UseVisualStyleBackColor = true;
            // 
            // dgv已签署协议
            // 
            this.dgv已签署协议.AllowUserToAddRows = false;
            this.dgv已签署协议.AllowUserToDeleteRows = false;
            this.dgv已签署协议.AllowUserToResizeRows = false;
            this.dgv已签署协议.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv已签署协议.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv已签署协议.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CUST_AGMT_TYPE,
            this.REMOTE_SYS,
            this.EFT_DATE,
            this.EXP_DATE,
            this.UPD_DATE,
            this.CUACCT_CODE,
            this.STKBD,
            this.TRDACCT,
            this.CUST_CODE});
            this.dgv已签署协议.Location = new System.Drawing.Point(6, 6);
            this.dgv已签署协议.Name = "dgv已签署协议";
            this.dgv已签署协议.ReadOnly = true;
            this.dgv已签署协议.RowHeadersVisible = false;
            this.dgv已签署协议.RowTemplate.Height = 23;
            this.dgv已签署协议.Size = new System.Drawing.Size(923, 227);
            this.dgv已签署协议.TabIndex = 2;
            // 
            // CUST_AGMT_TYPE
            // 
            this.CUST_AGMT_TYPE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CUST_AGMT_TYPE.DataPropertyName = "CUST_AGMT_TYPE";
            this.CUST_AGMT_TYPE.HeaderText = "客户协议类型";
            this.CUST_AGMT_TYPE.MinimumWidth = 100;
            this.CUST_AGMT_TYPE.Name = "CUST_AGMT_TYPE";
            this.CUST_AGMT_TYPE.ReadOnly = true;
            // 
            // REMOTE_SYS
            // 
            this.REMOTE_SYS.DataPropertyName = "REMOTE_SYS";
            this.REMOTE_SYS.HeaderText = "对接远程系统";
            this.REMOTE_SYS.Name = "REMOTE_SYS";
            this.REMOTE_SYS.ReadOnly = true;
            // 
            // EFT_DATE
            // 
            this.EFT_DATE.DataPropertyName = "EFT_DATE";
            this.EFT_DATE.HeaderText = "生效日期";
            this.EFT_DATE.Name = "EFT_DATE";
            this.EFT_DATE.ReadOnly = true;
            // 
            // EXP_DATE
            // 
            this.EXP_DATE.DataPropertyName = "EXP_DATE";
            this.EXP_DATE.HeaderText = "生效截止日期";
            this.EXP_DATE.Name = "EXP_DATE";
            this.EXP_DATE.ReadOnly = true;
            // 
            // UPD_DATE
            // 
            this.UPD_DATE.DataPropertyName = "UPD_DATE";
            this.UPD_DATE.HeaderText = "更新日期";
            this.UPD_DATE.Name = "UPD_DATE";
            this.UPD_DATE.ReadOnly = true;
            // 
            // CUACCT_CODE
            // 
            this.CUACCT_CODE.DataPropertyName = "CUACCT_CODE";
            this.CUACCT_CODE.HeaderText = "资产账户";
            this.CUACCT_CODE.Name = "CUACCT_CODE";
            this.CUACCT_CODE.ReadOnly = true;
            // 
            // STKBD
            // 
            this.STKBD.DataPropertyName = "STKBD";
            this.STKBD.HeaderText = "交易板块";
            this.STKBD.Name = "STKBD";
            this.STKBD.ReadOnly = true;
            // 
            // TRDACCT
            // 
            this.TRDACCT.DataPropertyName = "TRDACCT";
            this.TRDACCT.HeaderText = "交易账户";
            this.TRDACCT.Name = "TRDACCT";
            this.TRDACCT.ReadOnly = true;
            // 
            // CUST_CODE
            // 
            this.CUST_CODE.DataPropertyName = "CUST_CODE";
            this.CUST_CODE.HeaderText = "客户代码";
            this.CUST_CODE.Name = "CUST_CODE";
            this.CUST_CODE.ReadOnly = true;
            this.CUST_CODE.Visible = false;
            // 
            // frmExistAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(984, 734);
            this.Controls.Add(this.tc用户信息);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmExistAccount";
            this.ShowIcon = false;
            this.Text = "存量账户";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.frmExistAccount_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tc用户信息.ResumeLayout(false);
            this.tp基本资料.ResumeLayout(false);
            this.tp基本资料.PerformLayout();
            this.tp诚信记录.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv诚信记录)).EndInit();
            this.tp受益人.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv受益人)).EndInit();
            this.tp控制人.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv控制人)).EndInit();
            this.tp已签署协议.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv已签署协议)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.Button btnQueryByUserCode;
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
        private System.Windows.Forms.Button btnBindSHAcct;
        private System.Windows.Forms.ComboBox cbxOccupation;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox tbChannels;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox tbCuacct_cls;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ComboBox cbxCubsbScOpenAcctOpType;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox tbxBankAcctCode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox address;
        private System.Windows.Forms.TextBox id_beg_date;
        private System.Windows.Forms.TextBox id_exp_date;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbxCuacctCondition;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbxFislCuacct;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TabControl tc用户信息;
        private System.Windows.Forms.TabPage tp基本资料;
        private System.Windows.Forms.TabPage tp受益人;
        private System.Windows.Forms.TabPage tp诚信记录;
        private System.Windows.Forms.TabPage tp控制人;
        private System.Windows.Forms.DataGridView dgv诚信记录;
        private System.Windows.Forms.DataGridView dgv受益人;
        private System.Windows.Forms.DataGridView dgv控制人;
        private System.Windows.Forms.TabPage tp已签署协议;
        private System.Windows.Forms.DataGridView dgv已签署协议;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUST_AGMT_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn REMOTE_SYS;
        private System.Windows.Forms.DataGridViewTextBoxColumn EFT_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn EXP_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPD_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUACCT_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STKBD;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRDACCT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUST_CODE;
    }
}

