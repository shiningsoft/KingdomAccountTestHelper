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
            this.label31 = new System.Windows.Forms.Label();
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbCuacct_cls = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbxFislCuacct = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.tc用户信息 = new System.Windows.Forms.TabControl();
            this.tp基本资料 = new System.Windows.Forms.TabPage();
            this.btnMdfChannels = new System.Windows.Forms.Button();
            this.btnMdfCustBasicInfo = new System.Windows.Forms.Button();
            this.tbChannels = new System.Windows.Forms.TextBox();
            this.tpRiskSurveyResult = new System.Windows.Forms.TabPage();
            this.btnQueryRiskSurveyResult = new System.Windows.Forms.Button();
            this.label34 = new System.Windows.Forms.Label();
            this.dtpEND_DATE = new System.Windows.Forms.DateTimePicker();
            this.label28 = new System.Windows.Forms.Label();
            this.dtpBGN_DATE = new System.Windows.Forms.DateTimePicker();
            this.dgvRiskSurvey = new System.Windows.Forms.DataGridView();
            this.SURVEY_SN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.风险测评USER_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.USER_ROLE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SURVEY_CLS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SURVEY_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RATING_LVL_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NEXT_RATING_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SURVEY_SCORE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RATING_LVL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RATING_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.风险测评EXP_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VERSION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SURVEY_SYN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDINAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SURVEY_SCOPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SURVEY_CELLS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SURVEY_COLS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tp受益人 = new System.Windows.Forms.TabPage();
            this.btnAddBeneficirayInfo = new System.Windows.Forms.Button();
            this.dgv受益人 = new System.Windows.Forms.DataGridView();
            this.受益人USER_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BENEFICIARY_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BENEFICIARY_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BENEFICIARY_ID_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BENEFICIARY_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BENEFICIARY_EXP_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BENEFICIARY_TEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BENEFICIARY_ADDR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BENEFICIARY_RELA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tp控制人 = new System.Windows.Forms.TabPage();
            this.btnAddControllerInfo = new System.Windows.Forms.Button();
            this.dgv控制人 = new System.Windows.Forms.DataGridView();
            this.控制人CUST_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONTROLER_NUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONTROLER_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONTROLER_ID_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONTROLER_ID_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONTROLER_ID_EXP_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONTROLER_TEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONTROLER_EMAIL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONTROLER_RELATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REMARK = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.tp诚信记录 = new System.Windows.Forms.TabPage();
            this.dgv诚信记录 = new System.Windows.Forms.DataGridView();
            this.诚信记录CUST_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RECORD_NUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RECORD_SOURCE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RECORD_TXT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RECORD_SCORE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RECORD_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tp非居民涉税信息 = new System.Windows.Forms.TabPage();
            this.dgvCustNraTaxInfo = new System.Windows.Forms.DataGridView();
            this.非金融涉税信息CUST_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.非金融涉税信息CUST_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TAX_RESIDENT_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbLastRiskSurveyDate = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tc用户信息.SuspendLayout();
            this.tp基本资料.SuspendLayout();
            this.tpRiskSurveyResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiskSurvey)).BeginInit();
            this.tp受益人.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv受益人)).BeginInit();
            this.tp控制人.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv控制人)).BeginInit();
            this.tp已签署协议.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv已签署协议)).BeginInit();
            this.tp诚信记录.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv诚信记录)).BeginInit();
            this.tp非居民涉税信息.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustNraTaxInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxCubsbScOpenAcctOpType
            // 
            this.cbxCubsbScOpenAcctOpType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCubsbScOpenAcctOpType.FormattingEnabled = true;
            this.cbxCubsbScOpenAcctOpType.Items.AddRange(new object[] {
            "预指定",
            "一步式"});
            this.cbxCubsbScOpenAcctOpType.Location = new System.Drawing.Point(627, 20);
            this.cbxCubsbScOpenAcctOpType.Name = "cbxCubsbScOpenAcctOpType";
            this.cbxCubsbScOpenAcctOpType.Size = new System.Drawing.Size(100, 20);
            this.cbxCubsbScOpenAcctOpType.TabIndex = 15;
            this.cbxCubsbScOpenAcctOpType.SelectedIndexChanged += new System.EventHandler(this.cbxCubsbScOpenAcctOpType_SelectedIndexChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(555, 23);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(65, 12);
            this.label32.TabIndex = 89;
            this.label32.Text = "存管类型：";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(17, 23);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(89, 12);
            this.label31.TabIndex = 86;
            this.label31.Text = "资产账户类别：";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(637, 124);
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
            this.cbxOccupation.Location = new System.Drawing.Point(459, 147);
            this.cbxOccupation.Name = "cbxOccupation";
            this.cbxOccupation.Size = new System.Drawing.Size(136, 20);
            this.cbxOccupation.TabIndex = 20;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(388, 149);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(65, 12);
            this.label29.TabIndex = 82;
            this.label29.Text = "手输职业：";
            // 
            // btnBindSHAcct
            // 
            this.btnBindSHAcct.Location = new System.Drawing.Point(430, 116);
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
            this.tbxCybSignDate.Location = new System.Drawing.Point(245, 24);
            this.tbxCybSignDate.Name = "tbxCybSignDate";
            this.tbxCybSignDate.ReadOnly = true;
            this.tbxCybSignDate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxCybSignDate.Size = new System.Drawing.Size(277, 21);
            this.tbxCybSignDate.TabIndex = 16;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(162, 27);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(77, 12);
            this.label25.TabIndex = 80;
            this.label25.Text = "创业板信息：";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(350, 54);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(65, 12);
            this.label24.TabIndex = 79;
            this.label24.Text = "签约类型：";
            // 
            // dtpCybSignDate
            // 
            this.dtpCybSignDate.CustomFormat = "yyyyMMdd";
            this.dtpCybSignDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCybSignDate.Location = new System.Drawing.Point(245, 51);
            this.dtpCybSignDate.Name = "dtpCybSignDate";
            this.dtpCybSignDate.Size = new System.Drawing.Size(100, 21);
            this.dtpCybSignDate.TabIndex = 18;
            this.dtpCybSignDate.Value = new System.DateTime(2017, 8, 1, 0, 0, 0, 0);
            // 
            // cbxOpenType
            // 
            this.cbxOpenType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOpenType.FormattingEnabled = true;
            this.cbxOpenType.Location = new System.Drawing.Point(421, 51);
            this.cbxOpenType.Name = "cbxOpenType";
            this.cbxOpenType.Size = new System.Drawing.Size(100, 20);
            this.cbxOpenType.TabIndex = 19;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(138, 54);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(101, 12);
            this.label23.TabIndex = 76;
            this.label23.Text = "创业板签约日期：";
            // 
            // btnRegisterSZAStkAcct
            // 
            this.btnRegisterSZAStkAcct.Location = new System.Drawing.Point(324, 143);
            this.btnRegisterSZAStkAcct.Name = "btnRegisterSZAStkAcct";
            this.btnRegisterSZAStkAcct.Size = new System.Drawing.Size(100, 23);
            this.btnRegisterSZAStkAcct.TabIndex = 13;
            this.btnRegisterSZAStkAcct.Text = "加挂";
            this.btnRegisterSZAStkAcct.UseVisualStyleBackColor = true;
            this.btnRegisterSZAStkAcct.Click += new System.EventHandler(this.btnRegisterSZAStkAcct_Click);
            // 
            // btnOpenSZAStkAcct
            // 
            this.btnOpenSZAStkAcct.Location = new System.Drawing.Point(218, 143);
            this.btnOpenSZAStkAcct.Name = "btnOpenSZAStkAcct";
            this.btnOpenSZAStkAcct.Size = new System.Drawing.Size(100, 23);
            this.btnOpenSZAStkAcct.TabIndex = 12;
            this.btnOpenSZAStkAcct.Text = "新开";
            this.btnOpenSZAStkAcct.UseVisualStyleBackColor = true;
            this.btnOpenSZAStkAcct.Click += new System.EventHandler(this.btnOpenSZAStkAcct_Click);
            // 
            // btnQueryCYB
            // 
            this.btnQueryCYB.Location = new System.Drawing.Point(26, 22);
            this.btnQueryCYB.Name = "btnQueryCYB";
            this.btnQueryCYB.Size = new System.Drawing.Size(100, 23);
            this.btnQueryCYB.TabIndex = 15;
            this.btnQueryCYB.Text = "中登信息查询";
            this.btnQueryCYB.UseVisualStyleBackColor = true;
            this.btnQueryCYB.Click += new System.EventHandler(this.btnQueryCYB_Click);
            // 
            // bank_code
            // 
            this.bank_code.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bank_code.FormattingEnabled = true;
            this.bank_code.Location = new System.Drawing.Point(818, 20);
            this.bank_code.Name = "bank_code";
            this.bank_code.Size = new System.Drawing.Size(100, 20);
            this.bank_code.TabIndex = 16;
            this.bank_code.SelectedIndexChanged += new System.EventHandler(this.bank_code_SelectedIndexChanged);
            // 
            // sex
            // 
            this.sex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sex.FormattingEnabled = true;
            this.sex.Location = new System.Drawing.Point(459, 94);
            this.sex.Name = "sex";
            this.sex.Size = new System.Drawing.Size(100, 20);
            this.sex.TabIndex = 17;
            // 
            // citizenship
            // 
            this.citizenship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.citizenship.FormattingEnabled = true;
            this.citizenship.Location = new System.Drawing.Point(459, 67);
            this.citizenship.Name = "citizenship";
            this.citizenship.Size = new System.Drawing.Size(100, 20);
            this.citizenship.TabIndex = 14;
            // 
            // education
            // 
            this.education.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.education.FormattingEnabled = true;
            this.education.Location = new System.Drawing.Point(459, 40);
            this.education.Name = "education";
            this.education.Size = new System.Drawing.Size(100, 20);
            this.education.TabIndex = 13;
            // 
            // occu_type
            // 
            this.occu_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.occu_type.FormattingEnabled = true;
            this.occu_type.Location = new System.Drawing.Point(459, 120);
            this.occu_type.Name = "occu_type";
            this.occu_type.Size = new System.Drawing.Size(100, 20);
            this.occu_type.TabIndex = 12;
            this.occu_type.SelectedIndexChanged += new System.EventHandler(this.occu_type_SelectedIndexChanged);
            // 
            // nationality
            // 
            this.nationality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nationality.FormattingEnabled = true;
            this.nationality.Location = new System.Drawing.Point(459, 13);
            this.nationality.Name = "nationality";
            this.nationality.Size = new System.Drawing.Size(100, 20);
            this.nationality.TabIndex = 11;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(412, 97);
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
            this.btnOpenYMT.Location = new System.Drawing.Point(218, 89);
            this.btnOpenYMT.Name = "btnOpenYMT";
            this.btnOpenYMT.Size = new System.Drawing.Size(100, 23);
            this.btnOpenYMT.TabIndex = 6;
            this.btnOpenYMT.Text = "开一码通";
            this.btnOpenYMT.UseVisualStyleBackColor = true;
            this.btnOpenYMT.Click += new System.EventHandler(this.btnOpenYMT_Click);
            // 
            // btnOpenCYB
            // 
            this.btnOpenCYB.Location = new System.Drawing.Point(26, 49);
            this.btnOpenCYB.Name = "btnOpenCYB";
            this.btnOpenCYB.Size = new System.Drawing.Size(100, 23);
            this.btnOpenCYB.TabIndex = 14;
            this.btnOpenCYB.Text = "开通创业板";
            this.btnOpenCYB.UseVisualStyleBackColor = true;
            this.btnOpenCYB.Click += new System.EventHandler(this.btnOpenCYB_Click);
            // 
            // tbxYMTCode
            // 
            this.tbxYMTCode.Location = new System.Drawing.Point(112, 91);
            this.tbxYMTCode.Name = "tbxYMTCode";
            this.tbxYMTCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxYMTCode.Size = new System.Drawing.Size(100, 21);
            this.tbxYMTCode.TabIndex = 5;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(53, 94);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 12);
            this.label20.TabIndex = 55;
            this.label20.Text = "一码通：";
            // 
            // tbxSZAcct
            // 
            this.tbxSZAcct.Location = new System.Drawing.Point(112, 145);
            this.tbxSZAcct.Name = "tbxSZAcct";
            this.tbxSZAcct.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxSZAcct.Size = new System.Drawing.Size(100, 21);
            this.tbxSZAcct.TabIndex = 11;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(47, 148);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 12);
            this.label19.TabIndex = 52;
            this.label19.Text = "深圳A股：";
            // 
            // tbxSHAcct
            // 
            this.tbxSHAcct.Location = new System.Drawing.Point(112, 118);
            this.tbxSHAcct.Name = "tbxSHAcct";
            this.tbxSHAcct.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxSHAcct.Size = new System.Drawing.Size(100, 21);
            this.tbxSHAcct.TabIndex = 7;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(47, 121);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 12);
            this.label18.TabIndex = 50;
            this.label18.Text = "上海A股：";
            // 
            // tbxCuacct
            // 
            this.tbxCuacct.Location = new System.Drawing.Point(112, 49);
            this.tbxCuacct.Name = "tbxCuacct";
            this.tbxCuacct.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxCuacct.Size = new System.Drawing.Size(100, 21);
            this.tbxCuacct.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(41, 52);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 48;
            this.label17.Text = "资金账号：";
            // 
            // btnRegisterSHAStkAcct
            // 
            this.btnRegisterSHAStkAcct.Location = new System.Drawing.Point(324, 116);
            this.btnRegisterSHAStkAcct.Name = "btnRegisterSHAStkAcct";
            this.btnRegisterSHAStkAcct.Size = new System.Drawing.Size(100, 23);
            this.btnRegisterSHAStkAcct.TabIndex = 9;
            this.btnRegisterSHAStkAcct.Text = "加挂";
            this.btnRegisterSHAStkAcct.UseVisualStyleBackColor = true;
            this.btnRegisterSHAStkAcct.Click += new System.EventHandler(this.btnRegisterStockAccount_Click);
            // 
            // btnOpenSHAStkAcct
            // 
            this.btnOpenSHAStkAcct.Location = new System.Drawing.Point(218, 116);
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
            this.btnQueryStockAccount.Text = "中登账户查询";
            this.btnQueryStockAccount.UseVisualStyleBackColor = true;
            this.btnQueryStockAccount.Click += new System.EventHandler(this.btnQueryStockAccount_Click);
            // 
            // btnBankSign
            // 
            this.btnBankSign.Location = new System.Drawing.Point(818, 47);
            this.btnBankSign.Name = "btnBankSign";
            this.btnBankSign.Size = new System.Drawing.Size(100, 23);
            this.btnBankSign.TabIndex = 4;
            this.btnBankSign.Text = "签约三方存管";
            this.btnBankSign.UseVisualStyleBackColor = true;
            this.btnBankSign.Click += new System.EventHandler(this.btnBankSign_Click);
            // 
            // btnSubmitRiskTest
            // 
            this.btnSubmitRiskTest.Location = new System.Drawing.Point(829, 210);
            this.btnSubmitRiskTest.Name = "btnSubmitRiskTest";
            this.btnSubmitRiskTest.Size = new System.Drawing.Size(100, 23);
            this.btnSubmitRiskTest.TabIndex = 3;
            this.btnSubmitRiskTest.Text = "提交风险测评";
            this.btnSubmitRiskTest.UseVisualStyleBackColor = true;
            this.btnSubmitRiskTest.Click += new System.EventHandler(this.btnSubmitRiskTest_Click);
            // 
            // btnSetPassword
            // 
            this.btnSetPassword.Location = new System.Drawing.Point(814, 93);
            this.btnSetPassword.Name = "btnSetPassword";
            this.btnSetPassword.Size = new System.Drawing.Size(100, 23);
            this.btnSetPassword.TabIndex = 2;
            this.btnSetPassword.Text = "重置密码";
            this.btnSetPassword.UseVisualStyleBackColor = true;
            this.btnSetPassword.Click += new System.EventHandler(this.btnSetPassword_Click);
            // 
            // btnOpenCuacct
            // 
            this.btnOpenCuacct.Location = new System.Drawing.Point(218, 47);
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
            this.risk_level.Location = new System.Drawing.Point(723, 213);
            this.risk_level.Name = "risk_level";
            this.risk_level.Size = new System.Drawing.Size(100, 20);
            this.risk_level.TabIndex = 10;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(652, 216);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 38;
            this.label16.Text = "风测级别：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(747, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 36;
            this.label15.Text = "存管银行：";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(637, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 34;
            this.label1.Text = "交易密码：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(708, 95);
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
            this.label10.Location = new System.Drawing.Point(412, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "学历：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(412, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "职业：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(412, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "民族：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(412, 70);
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
            this.label33.Location = new System.Drawing.Point(557, 52);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(65, 12);
            this.label33.TabIndex = 90;
            this.label33.Text = "银行卡号：";
            // 
            // tbxBankAcctCode
            // 
            this.tbxBankAcctCode.Enabled = false;
            this.tbxBankAcctCode.Location = new System.Drawing.Point(627, 49);
            this.tbxBankAcctCode.Name = "tbxBankAcctCode";
            this.tbxBankAcctCode.Size = new System.Drawing.Size(185, 21);
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
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.btnOpenCuacct);
            this.groupBox3.Controls.Add(this.tbxSZAcct);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.btnBindSHAcct);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.tbxYMTCode);
            this.groupBox3.Controls.Add(this.tbxSHAcct);
            this.groupBox3.Controls.Add(this.btnRegisterSZAStkAcct);
            this.groupBox3.Controls.Add(this.btnOpenSZAStkAcct);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.btnOpenYMT);
            this.groupBox3.Controls.Add(this.tbxCuacct);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.btnRegisterSHAStkAcct);
            this.groupBox3.Controls.Add(this.btnBankSign);
            this.groupBox3.Controls.Add(this.btnOpenSHAStkAcct);
            this.groupBox3.Controls.Add(this.tbCuacct_cls);
            this.groupBox3.Controls.Add(this.tbxBankAcctCode);
            this.groupBox3.Controls.Add(this.label33);
            this.groupBox3.Controls.Add(this.cbxCubsbScOpenAcctOpType);
            this.groupBox3.Controls.Add(this.label31);
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.bank_code);
            this.groupBox3.Location = new System.Drawing.Point(12, 340);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(943, 279);
            this.groupBox3.TabIndex = 98;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "普通账户";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnQueryCYB);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.cbxOpenType);
            this.groupBox2.Controls.Add(this.dtpCybSignDate);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.btnOpenCYB);
            this.groupBox2.Controls.Add(this.tbxCybSignDate);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Location = new System.Drawing.Point(42, 172);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(876, 84);
            this.groupBox2.TabIndex = 91;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "创业板";
            // 
            // tbCuacct_cls
            // 
            this.tbCuacct_cls.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::金证统一账户测试账户生成器.Properties.Settings.Default, "默认开通的资产账户类别", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbCuacct_cls.Location = new System.Drawing.Point(112, 20);
            this.tbCuacct_cls.Name = "tbCuacct_cls";
            this.tbCuacct_cls.Size = new System.Drawing.Size(100, 21);
            this.tbCuacct_cls.TabIndex = 21;
            this.tbCuacct_cls.Text = global::金证统一账户测试账户生成器.Properties.Settings.Default.默认开通的资产账户类别;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbxFislCuacct);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Location = new System.Drawing.Point(12, 625);
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
            this.tc用户信息.Controls.Add(this.tpRiskSurveyResult);
            this.tc用户信息.Controls.Add(this.tp受益人);
            this.tc用户信息.Controls.Add(this.tp控制人);
            this.tc用户信息.Controls.Add(this.tp已签署协议);
            this.tc用户信息.Controls.Add(this.tp诚信记录);
            this.tc用户信息.Controls.Add(this.tp非居民涉税信息);
            this.tc用户信息.Location = new System.Drawing.Point(12, 69);
            this.tc用户信息.Name = "tc用户信息";
            this.tc用户信息.SelectedIndex = 0;
            this.tc用户信息.Size = new System.Drawing.Size(943, 265);
            this.tc用户信息.TabIndex = 97;
            // 
            // tp基本资料
            // 
            this.tp基本资料.Controls.Add(this.btnMdfChannels);
            this.tp基本资料.Controls.Add(this.btnMdfCustBasicInfo);
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
            this.tp基本资料.Controls.Add(this.btnSetPassword);
            this.tp基本资料.Controls.Add(this.id_addr);
            this.tp基本资料.Controls.Add(this.label22);
            this.tp基本资料.Controls.Add(this.label9);
            this.tp基本资料.Controls.Add(this.label14);
            this.tp基本资料.Controls.Add(this.btnQueryStockAccount);
            this.tp基本资料.Controls.Add(this.label8);
            this.tp基本资料.Controls.Add(this.address);
            this.tp基本资料.Controls.Add(this.mobile_tel);
            this.tp基本资料.Controls.Add(this.nationality);
            this.tp基本资料.Controls.Add(this.tbChannels);
            this.tp基本资料.Controls.Add(this.label30);
            this.tp基本资料.Controls.Add(this.label29);
            this.tp基本资料.Controls.Add(this.occu_type);
            this.tp基本资料.Controls.Add(this.label7);
            this.tp基本资料.Controls.Add(this.label12);
            this.tp基本资料.Controls.Add(this.education);
            this.tp基本资料.Controls.Add(this.cbxOccupation);
            this.tp基本资料.Controls.Add(this.citizenship);
            this.tp基本资料.Controls.Add(this.label6);
            this.tp基本资料.Controls.Add(this.sex);
            this.tp基本资料.Controls.Add(this.label13);
            this.tp基本资料.Controls.Add(this.label5);
            this.tp基本资料.Controls.Add(this.password);
            this.tp基本资料.Controls.Add(this.label2);
            this.tp基本资料.Controls.Add(this.label1);
            this.tp基本资料.Controls.Add(this.label4);
            this.tp基本资料.Location = new System.Drawing.Point(4, 22);
            this.tp基本资料.Name = "tp基本资料";
            this.tp基本资料.Padding = new System.Windows.Forms.Padding(3);
            this.tp基本资料.Size = new System.Drawing.Size(935, 239);
            this.tp基本资料.TabIndex = 0;
            this.tp基本资料.Text = "基本资料";
            this.tp基本资料.UseVisualStyleBackColor = true;
            // 
            // btnMdfChannels
            // 
            this.btnMdfChannels.Location = new System.Drawing.Point(814, 119);
            this.btnMdfChannels.Name = "btnMdfChannels";
            this.btnMdfChannels.Size = new System.Drawing.Size(100, 23);
            this.btnMdfChannels.TabIndex = 95;
            this.btnMdfChannels.Text = "设置操作渠道";
            this.btnMdfChannels.UseVisualStyleBackColor = true;
            this.btnMdfChannels.Click += new System.EventHandler(this.btnMdfChannels_Click);
            // 
            // btnMdfCustBasicInfo
            // 
            this.btnMdfCustBasicInfo.Location = new System.Drawing.Point(459, 199);
            this.btnMdfCustBasicInfo.Name = "btnMdfCustBasicInfo";
            this.btnMdfCustBasicInfo.Size = new System.Drawing.Size(100, 23);
            this.btnMdfCustBasicInfo.TabIndex = 94;
            this.btnMdfCustBasicInfo.Text = "修改客户资料";
            this.btnMdfCustBasicInfo.UseVisualStyleBackColor = true;
            this.btnMdfCustBasicInfo.Click += new System.EventHandler(this.btnMdfCustBasicInfo_Click);
            // 
            // tbChannels
            // 
            this.tbChannels.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::金证统一账户测试账户生成器.Properties.Settings.Default, "默认开通的操作渠道", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbChannels.Location = new System.Drawing.Point(708, 121);
            this.tbChannels.Name = "tbChannels";
            this.tbChannels.Size = new System.Drawing.Size(100, 21);
            this.tbChannels.TabIndex = 22;
            this.tbChannels.Text = global::金证统一账户测试账户生成器.Properties.Settings.Default.默认开通的操作渠道;
            // 
            // tpRiskSurveyResult
            // 
            this.tpRiskSurveyResult.Controls.Add(this.lbLastRiskSurveyDate);
            this.tpRiskSurveyResult.Controls.Add(this.btnQueryRiskSurveyResult);
            this.tpRiskSurveyResult.Controls.Add(this.label34);
            this.tpRiskSurveyResult.Controls.Add(this.dtpEND_DATE);
            this.tpRiskSurveyResult.Controls.Add(this.label28);
            this.tpRiskSurveyResult.Controls.Add(this.dtpBGN_DATE);
            this.tpRiskSurveyResult.Controls.Add(this.btnSubmitRiskTest);
            this.tpRiskSurveyResult.Controls.Add(this.label16);
            this.tpRiskSurveyResult.Controls.Add(this.dgvRiskSurvey);
            this.tpRiskSurveyResult.Controls.Add(this.risk_level);
            this.tpRiskSurveyResult.Location = new System.Drawing.Point(4, 22);
            this.tpRiskSurveyResult.Name = "tpRiskSurveyResult";
            this.tpRiskSurveyResult.Padding = new System.Windows.Forms.Padding(3);
            this.tpRiskSurveyResult.Size = new System.Drawing.Size(935, 239);
            this.tpRiskSurveyResult.TabIndex = 5;
            this.tpRiskSurveyResult.Text = "风险测评";
            this.tpRiskSurveyResult.UseVisualStyleBackColor = true;
            // 
            // btnQueryRiskSurveyResult
            // 
            this.btnQueryRiskSurveyResult.Location = new System.Drawing.Point(363, 210);
            this.btnQueryRiskSurveyResult.Name = "btnQueryRiskSurveyResult";
            this.btnQueryRiskSurveyResult.Size = new System.Drawing.Size(77, 23);
            this.btnQueryRiskSurveyResult.TabIndex = 52;
            this.btnQueryRiskSurveyResult.Text = "刷新";
            this.btnQueryRiskSurveyResult.UseVisualStyleBackColor = true;
            this.btnQueryRiskSurveyResult.Click += new System.EventHandler(this.btnQueryRiskSurveyResult_Click);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(185, 216);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(65, 12);
            this.label34.TabIndex = 51;
            this.label34.Text = "结束日期：";
            // 
            // dtpEND_DATE
            // 
            this.dtpEND_DATE.CustomFormat = "yyyyMMdd";
            this.dtpEND_DATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEND_DATE.Location = new System.Drawing.Point(256, 210);
            this.dtpEND_DATE.Name = "dtpEND_DATE";
            this.dtpEND_DATE.Size = new System.Drawing.Size(101, 21);
            this.dtpEND_DATE.TabIndex = 50;
            this.dtpEND_DATE.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(7, 216);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(65, 12);
            this.label28.TabIndex = 49;
            this.label28.Text = "开始日期：";
            // 
            // dtpBGN_DATE
            // 
            this.dtpBGN_DATE.CustomFormat = "yyyyMMdd";
            this.dtpBGN_DATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBGN_DATE.Location = new System.Drawing.Point(78, 210);
            this.dtpBGN_DATE.Name = "dtpBGN_DATE";
            this.dtpBGN_DATE.Size = new System.Drawing.Size(101, 21);
            this.dtpBGN_DATE.TabIndex = 5;
            this.dtpBGN_DATE.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            // 
            // dgvRiskSurvey
            // 
            this.dgvRiskSurvey.AllowUserToAddRows = false;
            this.dgvRiskSurvey.AllowUserToDeleteRows = false;
            this.dgvRiskSurvey.AllowUserToResizeRows = false;
            this.dgvRiskSurvey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRiskSurvey.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRiskSurvey.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SURVEY_SN,
            this.风险测评USER_CODE,
            this.USER_ROLE,
            this.SURVEY_CLS,
            this.SURVEY_NAME,
            this.RATING_LVL_NAME,
            this.NEXT_RATING_DATE,
            this.SURVEY_SCORE,
            this.RATING_LVL,
            this.RATING_DATE,
            this.风险测评EXP_DATE,
            this.VERSION,
            this.SURVEY_SYN,
            this.ORDINAL,
            this.SURVEY_SCOPE,
            this.SURVEY_CELLS,
            this.SURVEY_COLS});
            this.dgvRiskSurvey.Location = new System.Drawing.Point(6, 6);
            this.dgvRiskSurvey.Name = "dgvRiskSurvey";
            this.dgvRiskSurvey.ReadOnly = true;
            this.dgvRiskSurvey.RowHeadersVisible = false;
            this.dgvRiskSurvey.RowTemplate.Height = 23;
            this.dgvRiskSurvey.Size = new System.Drawing.Size(923, 198);
            this.dgvRiskSurvey.TabIndex = 3;
            // 
            // SURVEY_SN
            // 
            this.SURVEY_SN.DataPropertyName = "SURVEY_SN";
            this.SURVEY_SN.HeaderText = "调查表编码";
            this.SURVEY_SN.Name = "SURVEY_SN";
            this.SURVEY_SN.ReadOnly = true;
            this.SURVEY_SN.Visible = false;
            // 
            // 风险测评USER_CODE
            // 
            this.风险测评USER_CODE.DataPropertyName = "USER_CODE";
            this.风险测评USER_CODE.HeaderText = "用户代码";
            this.风险测评USER_CODE.Name = "风险测评USER_CODE";
            this.风险测评USER_CODE.ReadOnly = true;
            this.风险测评USER_CODE.Visible = false;
            // 
            // USER_ROLE
            // 
            this.USER_ROLE.DataPropertyName = "USER_ROLE";
            this.USER_ROLE.HeaderText = "用户角色";
            this.USER_ROLE.Name = "USER_ROLE";
            this.USER_ROLE.ReadOnly = true;
            this.USER_ROLE.Visible = false;
            // 
            // SURVEY_CLS
            // 
            this.SURVEY_CLS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SURVEY_CLS.DataPropertyName = "SURVEY_CLS";
            this.SURVEY_CLS.HeaderText = "调查表类别";
            this.SURVEY_CLS.MinimumWidth = 100;
            this.SURVEY_CLS.Name = "SURVEY_CLS";
            this.SURVEY_CLS.ReadOnly = true;
            // 
            // SURVEY_NAME
            // 
            this.SURVEY_NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SURVEY_NAME.DataPropertyName = "SURVEY_NAME";
            this.SURVEY_NAME.HeaderText = "调查表名称";
            this.SURVEY_NAME.MinimumWidth = 100;
            this.SURVEY_NAME.Name = "SURVEY_NAME";
            this.SURVEY_NAME.ReadOnly = true;
            // 
            // RATING_LVL_NAME
            // 
            this.RATING_LVL_NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RATING_LVL_NAME.DataPropertyName = "RATING_LVL_NAME";
            this.RATING_LVL_NAME.HeaderText = "评级级别名称";
            this.RATING_LVL_NAME.MinimumWidth = 120;
            this.RATING_LVL_NAME.Name = "RATING_LVL_NAME";
            this.RATING_LVL_NAME.ReadOnly = true;
            this.RATING_LVL_NAME.Width = 120;
            // 
            // NEXT_RATING_DATE
            // 
            this.NEXT_RATING_DATE.DataPropertyName = "NEXT_RATING_DATE";
            this.NEXT_RATING_DATE.HeaderText = "下次可测评日期";
            this.NEXT_RATING_DATE.MinimumWidth = 140;
            this.NEXT_RATING_DATE.Name = "NEXT_RATING_DATE";
            this.NEXT_RATING_DATE.ReadOnly = true;
            this.NEXT_RATING_DATE.Width = 140;
            // 
            // SURVEY_SCORE
            // 
            this.SURVEY_SCORE.DataPropertyName = "SURVEY_SCORE";
            this.SURVEY_SCORE.HeaderText = "调查表分值";
            this.SURVEY_SCORE.Name = "SURVEY_SCORE";
            this.SURVEY_SCORE.ReadOnly = true;
            // 
            // RATING_LVL
            // 
            this.RATING_LVL.DataPropertyName = "RATING_LVL";
            this.RATING_LVL.HeaderText = "评级级别";
            this.RATING_LVL.Name = "RATING_LVL";
            this.RATING_LVL.ReadOnly = true;
            // 
            // RATING_DATE
            // 
            this.RATING_DATE.DataPropertyName = "RATING_DATE";
            this.RATING_DATE.HeaderText = "评级日期";
            this.RATING_DATE.Name = "RATING_DATE";
            this.RATING_DATE.ReadOnly = true;
            // 
            // 风险测评EXP_DATE
            // 
            this.风险测评EXP_DATE.DataPropertyName = "EXP_DATE";
            this.风险测评EXP_DATE.HeaderText = "有效截止日期";
            this.风险测评EXP_DATE.MinimumWidth = 120;
            this.风险测评EXP_DATE.Name = "风险测评EXP_DATE";
            this.风险测评EXP_DATE.ReadOnly = true;
            this.风险测评EXP_DATE.Width = 120;
            // 
            // VERSION
            // 
            this.VERSION.DataPropertyName = "VERSION";
            this.VERSION.HeaderText = "版本";
            this.VERSION.Name = "VERSION";
            this.VERSION.ReadOnly = true;
            // 
            // SURVEY_SYN
            // 
            this.SURVEY_SYN.DataPropertyName = "SURVEY_SYN";
            this.SURVEY_SYN.HeaderText = "远程同步系统";
            this.SURVEY_SYN.MinimumWidth = 120;
            this.SURVEY_SYN.Name = "SURVEY_SYN";
            this.SURVEY_SYN.ReadOnly = true;
            this.SURVEY_SYN.Width = 120;
            // 
            // ORDINAL
            // 
            this.ORDINAL.DataPropertyName = "ORDINAL";
            this.ORDINAL.HeaderText = "次序";
            this.ORDINAL.Name = "ORDINAL";
            this.ORDINAL.ReadOnly = true;
            // 
            // SURVEY_SCOPE
            // 
            this.SURVEY_SCOPE.DataPropertyName = "SURVEY_SCOPE";
            this.SURVEY_SCOPE.HeaderText = "SURVEY_SCOPE";
            this.SURVEY_SCOPE.Name = "SURVEY_SCOPE";
            this.SURVEY_SCOPE.ReadOnly = true;
            this.SURVEY_SCOPE.Visible = false;
            // 
            // SURVEY_CELLS
            // 
            this.SURVEY_CELLS.DataPropertyName = "SURVEY_CELLS";
            this.SURVEY_CELLS.HeaderText = "答案串";
            this.SURVEY_CELLS.Name = "SURVEY_CELLS";
            this.SURVEY_CELLS.ReadOnly = true;
            this.SURVEY_CELLS.Visible = false;
            // 
            // SURVEY_COLS
            // 
            this.SURVEY_COLS.DataPropertyName = "SURVEY_COLS";
            this.SURVEY_COLS.HeaderText = "试题串";
            this.SURVEY_COLS.Name = "SURVEY_COLS";
            this.SURVEY_COLS.ReadOnly = true;
            this.SURVEY_COLS.Visible = false;
            // 
            // tp受益人
            // 
            this.tp受益人.Controls.Add(this.btnAddBeneficirayInfo);
            this.tp受益人.Controls.Add(this.dgv受益人);
            this.tp受益人.Location = new System.Drawing.Point(4, 22);
            this.tp受益人.Name = "tp受益人";
            this.tp受益人.Padding = new System.Windows.Forms.Padding(3);
            this.tp受益人.Size = new System.Drawing.Size(935, 239);
            this.tp受益人.TabIndex = 1;
            this.tp受益人.Text = "受益人";
            this.tp受益人.UseVisualStyleBackColor = true;
            // 
            // btnAddBeneficirayInfo
            // 
            this.btnAddBeneficirayInfo.Location = new System.Drawing.Point(6, 210);
            this.btnAddBeneficirayInfo.Name = "btnAddBeneficirayInfo";
            this.btnAddBeneficirayInfo.Size = new System.Drawing.Size(110, 23);
            this.btnAddBeneficirayInfo.TabIndex = 3;
            this.btnAddBeneficirayInfo.Text = "增加受益人";
            this.btnAddBeneficirayInfo.UseVisualStyleBackColor = true;
            this.btnAddBeneficirayInfo.Click += new System.EventHandler(this.btnAddBeneficirayInfo_Click);
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
            this.dgv受益人.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.受益人USER_CODE,
            this.BENEFICIARY_NO,
            this.BENEFICIARY_NAME,
            this.BENEFICIARY_ID_TYPE,
            this.BENEFICIARY_ID,
            this.BENEFICIARY_EXP_DATE,
            this.BENEFICIARY_TEL,
            this.BENEFICIARY_ADDR,
            this.BENEFICIARY_RELA});
            this.dgv受益人.Location = new System.Drawing.Point(6, 6);
            this.dgv受益人.Name = "dgv受益人";
            this.dgv受益人.ReadOnly = true;
            this.dgv受益人.RowHeadersVisible = false;
            this.dgv受益人.RowTemplate.Height = 23;
            this.dgv受益人.Size = new System.Drawing.Size(923, 198);
            this.dgv受益人.TabIndex = 1;
            // 
            // 受益人USER_CODE
            // 
            this.受益人USER_CODE.DataPropertyName = "USER_CODE";
            this.受益人USER_CODE.HeaderText = "用户代码";
            this.受益人USER_CODE.Name = "受益人USER_CODE";
            this.受益人USER_CODE.ReadOnly = true;
            this.受益人USER_CODE.Visible = false;
            // 
            // BENEFICIARY_NO
            // 
            this.BENEFICIARY_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BENEFICIARY_NO.DataPropertyName = "BENEFICIARY_NO";
            this.BENEFICIARY_NO.HeaderText = "受益人编号";
            this.BENEFICIARY_NO.MinimumWidth = 100;
            this.BENEFICIARY_NO.Name = "BENEFICIARY_NO";
            this.BENEFICIARY_NO.ReadOnly = true;
            // 
            // BENEFICIARY_NAME
            // 
            this.BENEFICIARY_NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BENEFICIARY_NAME.DataPropertyName = "BENEFICIARY_NAME";
            this.BENEFICIARY_NAME.HeaderText = "受益人名称";
            this.BENEFICIARY_NAME.MinimumWidth = 100;
            this.BENEFICIARY_NAME.Name = "BENEFICIARY_NAME";
            this.BENEFICIARY_NAME.ReadOnly = true;
            // 
            // BENEFICIARY_ID_TYPE
            // 
            this.BENEFICIARY_ID_TYPE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BENEFICIARY_ID_TYPE.DataPropertyName = "BENEFICIARY_ID_TYPE";
            this.BENEFICIARY_ID_TYPE.HeaderText = "受益人证件类型";
            this.BENEFICIARY_ID_TYPE.MinimumWidth = 120;
            this.BENEFICIARY_ID_TYPE.Name = "BENEFICIARY_ID_TYPE";
            this.BENEFICIARY_ID_TYPE.ReadOnly = true;
            this.BENEFICIARY_ID_TYPE.Width = 120;
            // 
            // BENEFICIARY_ID
            // 
            this.BENEFICIARY_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BENEFICIARY_ID.DataPropertyName = "BENEFICIARY_ID";
            this.BENEFICIARY_ID.HeaderText = "受益人证件号码";
            this.BENEFICIARY_ID.MinimumWidth = 120;
            this.BENEFICIARY_ID.Name = "BENEFICIARY_ID";
            this.BENEFICIARY_ID.ReadOnly = true;
            this.BENEFICIARY_ID.Width = 120;
            // 
            // BENEFICIARY_EXP_DATE
            // 
            this.BENEFICIARY_EXP_DATE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BENEFICIARY_EXP_DATE.DataPropertyName = "BENEFICIARY_EXP_DATE";
            this.BENEFICIARY_EXP_DATE.HeaderText = "受益人证件有效期";
            this.BENEFICIARY_EXP_DATE.MinimumWidth = 140;
            this.BENEFICIARY_EXP_DATE.Name = "BENEFICIARY_EXP_DATE";
            this.BENEFICIARY_EXP_DATE.ReadOnly = true;
            this.BENEFICIARY_EXP_DATE.Width = 140;
            // 
            // BENEFICIARY_TEL
            // 
            this.BENEFICIARY_TEL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BENEFICIARY_TEL.DataPropertyName = "BENEFICIARY_TEL";
            this.BENEFICIARY_TEL.HeaderText = "受益人电话号码";
            this.BENEFICIARY_TEL.MinimumWidth = 120;
            this.BENEFICIARY_TEL.Name = "BENEFICIARY_TEL";
            this.BENEFICIARY_TEL.ReadOnly = true;
            this.BENEFICIARY_TEL.Width = 120;
            // 
            // BENEFICIARY_ADDR
            // 
            this.BENEFICIARY_ADDR.DataPropertyName = "BENEFICIARY_ADDR";
            this.BENEFICIARY_ADDR.HeaderText = "受益人地址";
            this.BENEFICIARY_ADDR.Name = "BENEFICIARY_ADDR";
            this.BENEFICIARY_ADDR.ReadOnly = true;
            // 
            // BENEFICIARY_RELA
            // 
            this.BENEFICIARY_RELA.DataPropertyName = "BENEFICIARY_RELA";
            this.BENEFICIARY_RELA.HeaderText = "与股东关系";
            this.BENEFICIARY_RELA.Name = "BENEFICIARY_RELA";
            this.BENEFICIARY_RELA.ReadOnly = true;
            // 
            // tp控制人
            // 
            this.tp控制人.Controls.Add(this.btnAddControllerInfo);
            this.tp控制人.Controls.Add(this.dgv控制人);
            this.tp控制人.Location = new System.Drawing.Point(4, 22);
            this.tp控制人.Name = "tp控制人";
            this.tp控制人.Padding = new System.Windows.Forms.Padding(3);
            this.tp控制人.Size = new System.Drawing.Size(935, 239);
            this.tp控制人.TabIndex = 2;
            this.tp控制人.Text = "控制人";
            this.tp控制人.UseVisualStyleBackColor = true;
            // 
            // btnAddControllerInfo
            // 
            this.btnAddControllerInfo.Location = new System.Drawing.Point(6, 210);
            this.btnAddControllerInfo.Name = "btnAddControllerInfo";
            this.btnAddControllerInfo.Size = new System.Drawing.Size(110, 23);
            this.btnAddControllerInfo.TabIndex = 4;
            this.btnAddControllerInfo.Text = "增加控制人";
            this.btnAddControllerInfo.UseVisualStyleBackColor = true;
            this.btnAddControllerInfo.Click += new System.EventHandler(this.btnAddControllerInfo_Click);
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
            this.dgv控制人.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.控制人CUST_CODE,
            this.CONTROLER_NUM,
            this.CONTROLER_NAME,
            this.CONTROLER_ID_TYPE,
            this.CONTROLER_ID_NO,
            this.CONTROLER_ID_EXP_DATE,
            this.CONTROLER_TEL,
            this.CONTROLER_EMAIL,
            this.CONTROLER_RELATION,
            this.REMARK});
            this.dgv控制人.Location = new System.Drawing.Point(6, 6);
            this.dgv控制人.Name = "dgv控制人";
            this.dgv控制人.ReadOnly = true;
            this.dgv控制人.RowHeadersVisible = false;
            this.dgv控制人.RowTemplate.Height = 23;
            this.dgv控制人.Size = new System.Drawing.Size(923, 198);
            this.dgv控制人.TabIndex = 1;
            // 
            // 控制人CUST_CODE
            // 
            this.控制人CUST_CODE.DataPropertyName = "CUST_CODE";
            this.控制人CUST_CODE.HeaderText = "客户代码";
            this.控制人CUST_CODE.Name = "控制人CUST_CODE";
            this.控制人CUST_CODE.ReadOnly = true;
            this.控制人CUST_CODE.Visible = false;
            // 
            // CONTROLER_NUM
            // 
            this.CONTROLER_NUM.DataPropertyName = "CONTROLER_NUM";
            this.CONTROLER_NUM.HeaderText = "控制人编号";
            this.CONTROLER_NUM.Name = "CONTROLER_NUM";
            this.CONTROLER_NUM.ReadOnly = true;
            // 
            // CONTROLER_NAME
            // 
            this.CONTROLER_NAME.DataPropertyName = "CONTROLER_NAME";
            this.CONTROLER_NAME.HeaderText = "控制人姓名";
            this.CONTROLER_NAME.Name = "CONTROLER_NAME";
            this.CONTROLER_NAME.ReadOnly = true;
            // 
            // CONTROLER_ID_TYPE
            // 
            this.CONTROLER_ID_TYPE.DataPropertyName = "CONTROLER_ID_TYPE";
            this.CONTROLER_ID_TYPE.HeaderText = "控制人证件类型";
            this.CONTROLER_ID_TYPE.Name = "CONTROLER_ID_TYPE";
            this.CONTROLER_ID_TYPE.ReadOnly = true;
            this.CONTROLER_ID_TYPE.Width = 120;
            // 
            // CONTROLER_ID_NO
            // 
            this.CONTROLER_ID_NO.DataPropertyName = "CONTROLER_ID_NO";
            this.CONTROLER_ID_NO.HeaderText = "控制人证件号码";
            this.CONTROLER_ID_NO.Name = "CONTROLER_ID_NO";
            this.CONTROLER_ID_NO.ReadOnly = true;
            this.CONTROLER_ID_NO.Width = 120;
            // 
            // CONTROLER_ID_EXP_DATE
            // 
            this.CONTROLER_ID_EXP_DATE.DataPropertyName = "CONTROLER_ID_EXP_DATE";
            this.CONTROLER_ID_EXP_DATE.HeaderText = "控制人证件有效期";
            this.CONTROLER_ID_EXP_DATE.Name = "CONTROLER_ID_EXP_DATE";
            this.CONTROLER_ID_EXP_DATE.ReadOnly = true;
            this.CONTROLER_ID_EXP_DATE.Width = 140;
            // 
            // CONTROLER_TEL
            // 
            this.CONTROLER_TEL.DataPropertyName = "CONTROLER_TEL";
            this.CONTROLER_TEL.HeaderText = "控制人电话";
            this.CONTROLER_TEL.Name = "CONTROLER_TEL";
            this.CONTROLER_TEL.ReadOnly = true;
            // 
            // CONTROLER_EMAIL
            // 
            this.CONTROLER_EMAIL.DataPropertyName = "CONTROLER_EMAIL";
            this.CONTROLER_EMAIL.HeaderText = "控制人邮箱";
            this.CONTROLER_EMAIL.Name = "CONTROLER_EMAIL";
            this.CONTROLER_EMAIL.ReadOnly = true;
            // 
            // CONTROLER_RELATION
            // 
            this.CONTROLER_RELATION.DataPropertyName = "CONTROLER_RELATION";
            this.CONTROLER_RELATION.HeaderText = "控制人与本人关系";
            this.CONTROLER_RELATION.Name = "CONTROLER_RELATION";
            this.CONTROLER_RELATION.ReadOnly = true;
            this.CONTROLER_RELATION.Width = 140;
            // 
            // REMARK
            // 
            this.REMARK.DataPropertyName = "REMARK";
            this.REMARK.HeaderText = "备注";
            this.REMARK.Name = "REMARK";
            this.REMARK.ReadOnly = true;
            // 
            // tp已签署协议
            // 
            this.tp已签署协议.Controls.Add(this.dgv已签署协议);
            this.tp已签署协议.Location = new System.Drawing.Point(4, 22);
            this.tp已签署协议.Name = "tp已签署协议";
            this.tp已签署协议.Padding = new System.Windows.Forms.Padding(3);
            this.tp已签署协议.Size = new System.Drawing.Size(935, 239);
            this.tp已签署协议.TabIndex = 4;
            this.tp已签署协议.Text = "签署协议";
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
            this.dgv诚信记录.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.诚信记录CUST_CODE,
            this.RECORD_NUM,
            this.RECORD_SOURCE,
            this.RECORD_TXT,
            this.RECORD_SCORE,
            this.RECORD_DATE});
            this.dgv诚信记录.Location = new System.Drawing.Point(6, 6);
            this.dgv诚信记录.Name = "dgv诚信记录";
            this.dgv诚信记录.ReadOnly = true;
            this.dgv诚信记录.RowHeadersVisible = false;
            this.dgv诚信记录.RowTemplate.Height = 23;
            this.dgv诚信记录.Size = new System.Drawing.Size(923, 227);
            this.dgv诚信记录.TabIndex = 0;
            // 
            // 诚信记录CUST_CODE
            // 
            this.诚信记录CUST_CODE.DataPropertyName = "CUST_CODE";
            this.诚信记录CUST_CODE.HeaderText = "客户代码";
            this.诚信记录CUST_CODE.Name = "诚信记录CUST_CODE";
            this.诚信记录CUST_CODE.ReadOnly = true;
            this.诚信记录CUST_CODE.Visible = false;
            // 
            // RECORD_NUM
            // 
            this.RECORD_NUM.DataPropertyName = "RECORD_NUM";
            this.RECORD_NUM.HeaderText = "诚信记录编号";
            this.RECORD_NUM.Name = "RECORD_NUM";
            this.RECORD_NUM.ReadOnly = true;
            // 
            // RECORD_SOURCE
            // 
            this.RECORD_SOURCE.DataPropertyName = "RECORD_SOURCE";
            this.RECORD_SOURCE.HeaderText = "诚信记录来源";
            this.RECORD_SOURCE.Name = "RECORD_SOURCE";
            this.RECORD_SOURCE.ReadOnly = true;
            // 
            // RECORD_TXT
            // 
            this.RECORD_TXT.DataPropertyName = "RECORD_TXT";
            this.RECORD_TXT.HeaderText = "诚信记录内容";
            this.RECORD_TXT.Name = "RECORD_TXT";
            this.RECORD_TXT.ReadOnly = true;
            // 
            // RECORD_SCORE
            // 
            this.RECORD_SCORE.DataPropertyName = "RECORD_SCORE";
            this.RECORD_SCORE.HeaderText = "加扣分";
            this.RECORD_SCORE.Name = "RECORD_SCORE";
            this.RECORD_SCORE.ReadOnly = true;
            // 
            // RECORD_DATE
            // 
            this.RECORD_DATE.DataPropertyName = "RECORD_DATE";
            this.RECORD_DATE.HeaderText = "录入日期";
            this.RECORD_DATE.Name = "RECORD_DATE";
            this.RECORD_DATE.ReadOnly = true;
            // 
            // tp非居民涉税信息
            // 
            this.tp非居民涉税信息.Controls.Add(this.dgvCustNraTaxInfo);
            this.tp非居民涉税信息.Location = new System.Drawing.Point(4, 22);
            this.tp非居民涉税信息.Name = "tp非居民涉税信息";
            this.tp非居民涉税信息.Padding = new System.Windows.Forms.Padding(3);
            this.tp非居民涉税信息.Size = new System.Drawing.Size(935, 239);
            this.tp非居民涉税信息.TabIndex = 6;
            this.tp非居民涉税信息.Text = "非居民涉税信息";
            this.tp非居民涉税信息.UseVisualStyleBackColor = true;
            // 
            // dgvCustNraTaxInfo
            // 
            this.dgvCustNraTaxInfo.AllowUserToAddRows = false;
            this.dgvCustNraTaxInfo.AllowUserToDeleteRows = false;
            this.dgvCustNraTaxInfo.AllowUserToResizeRows = false;
            this.dgvCustNraTaxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCustNraTaxInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustNraTaxInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.非金融涉税信息CUST_CODE,
            this.非金融涉税信息CUST_NAME,
            this.TAX_RESIDENT_TYPE});
            this.dgvCustNraTaxInfo.Location = new System.Drawing.Point(6, 6);
            this.dgvCustNraTaxInfo.Name = "dgvCustNraTaxInfo";
            this.dgvCustNraTaxInfo.ReadOnly = true;
            this.dgvCustNraTaxInfo.RowHeadersVisible = false;
            this.dgvCustNraTaxInfo.RowTemplate.Height = 23;
            this.dgvCustNraTaxInfo.Size = new System.Drawing.Size(923, 227);
            this.dgvCustNraTaxInfo.TabIndex = 1;
            // 
            // 非金融涉税信息CUST_CODE
            // 
            this.非金融涉税信息CUST_CODE.DataPropertyName = "CUST_CODE";
            this.非金融涉税信息CUST_CODE.HeaderText = "客户代码";
            this.非金融涉税信息CUST_CODE.Name = "非金融涉税信息CUST_CODE";
            this.非金融涉税信息CUST_CODE.ReadOnly = true;
            this.非金融涉税信息CUST_CODE.Visible = false;
            // 
            // 非金融涉税信息CUST_NAME
            // 
            this.非金融涉税信息CUST_NAME.DataPropertyName = "CUST_NAME";
            this.非金融涉税信息CUST_NAME.HeaderText = "客户名称";
            this.非金融涉税信息CUST_NAME.Name = "非金融涉税信息CUST_NAME";
            this.非金融涉税信息CUST_NAME.ReadOnly = true;
            // 
            // TAX_RESIDENT_TYPE
            // 
            this.TAX_RESIDENT_TYPE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TAX_RESIDENT_TYPE.DataPropertyName = "TAX_RESIDENT_TYPE";
            this.TAX_RESIDENT_TYPE.HeaderText = "税收居民身份";
            this.TAX_RESIDENT_TYPE.MinimumWidth = 120;
            this.TAX_RESIDENT_TYPE.Name = "TAX_RESIDENT_TYPE";
            this.TAX_RESIDENT_TYPE.ReadOnly = true;
            this.TAX_RESIDENT_TYPE.Width = 120;
            // 
            // lbLastRiskSurveyDate
            // 
            this.lbLastRiskSurveyDate.AutoSize = true;
            this.lbLastRiskSurveyDate.Location = new System.Drawing.Point(446, 216);
            this.lbLastRiskSurveyDate.Name = "lbLastRiskSurveyDate";
            this.lbLastRiskSurveyDate.Size = new System.Drawing.Size(89, 12);
            this.lbLastRiskSurveyDate.TabIndex = 53;
            this.lbLastRiskSurveyDate.Text = "最后测评日期：";
            // 
            // frmExistAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1018, 634);
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tc用户信息.ResumeLayout(false);
            this.tp基本资料.ResumeLayout(false);
            this.tp基本资料.PerformLayout();
            this.tpRiskSurveyResult.ResumeLayout(false);
            this.tpRiskSurveyResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiskSurvey)).EndInit();
            this.tp受益人.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv受益人)).EndInit();
            this.tp控制人.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv控制人)).EndInit();
            this.tp已签署协议.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv已签署协议)).EndInit();
            this.tp诚信记录.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv诚信记录)).EndInit();
            this.tp非居民涉税信息.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustNraTaxInfo)).EndInit();
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
        private System.Windows.Forms.Button btnMdfCustBasicInfo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnMdfChannels;
        private System.Windows.Forms.DataGridViewTextBoxColumn 诚信记录CUST_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECORD_NUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECORD_SOURCE;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECORD_TXT;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECORD_SCORE;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECORD_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn 控制人CUST_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONTROLER_NUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONTROLER_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONTROLER_ID_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONTROLER_ID_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONTROLER_ID_EXP_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONTROLER_TEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONTROLER_EMAIL;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONTROLER_RELATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn REMARK;
        private System.Windows.Forms.Button btnAddBeneficirayInfo;
        private System.Windows.Forms.Button btnAddControllerInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn 受益人USER_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn BENEFICIARY_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn BENEFICIARY_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn BENEFICIARY_ID_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn BENEFICIARY_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BENEFICIARY_EXP_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn BENEFICIARY_TEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn BENEFICIARY_ADDR;
        private System.Windows.Forms.DataGridViewTextBoxColumn BENEFICIARY_RELA;
        private System.Windows.Forms.TabPage tpRiskSurveyResult;
        private System.Windows.Forms.DataGridView dgvRiskSurvey;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.DateTimePicker dtpBGN_DATE;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.DateTimePicker dtpEND_DATE;
        private System.Windows.Forms.Button btnQueryRiskSurveyResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn SURVEY_SN;
        private System.Windows.Forms.DataGridViewTextBoxColumn 风险测评USER_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn USER_ROLE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SURVEY_CLS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SURVEY_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn RATING_LVL_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn NEXT_RATING_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SURVEY_SCORE;
        private System.Windows.Forms.DataGridViewTextBoxColumn RATING_LVL;
        private System.Windows.Forms.DataGridViewTextBoxColumn RATING_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn 风险测评EXP_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VERSION;
        private System.Windows.Forms.DataGridViewTextBoxColumn SURVEY_SYN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDINAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn SURVEY_SCOPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SURVEY_CELLS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SURVEY_COLS;
        private System.Windows.Forms.TabPage tp非居民涉税信息;
        private System.Windows.Forms.DataGridView dgvCustNraTaxInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn 非金融涉税信息CUST_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn 非金融涉税信息CUST_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn TAX_RESIDENT_TYPE;
        private System.Windows.Forms.Label lbLastRiskSurveyDate;
    }
}

