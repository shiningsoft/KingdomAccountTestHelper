using NLog;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yushen.Util;
using Yushen.WebService.KessClient;
using 金证统一账户测试账户生成器.Properties;
using Dict = Yushen.WebService.KessClient.Dict;

namespace 金证统一账户测试账户生成器
{
    public partial class frmExistAccount : Form
    {
        frmFramework frmFramework;

        frmResultForm resultForm {
            get
            {
                return frmFramework.resultForm;
            }
        }

        Kess kess
        {
            get
            {
                return frmFramework.kess;
            }
        }

        static Logger logger = LogManager.GetCurrentClassLogger();

        User user;

        public frmExistAccount(frmFramework form)
        {
            frmFramework = form;
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                // 初始化风险评级选项
                Dict.RiskTestLevel levelList = new Dict.RiskTestLevel();
                risk_level.DisplayMember = "name";
                risk_level.ValueMember = "value";
                risk_level.DataSource = levelList.DataTable;

                Dict.NATIONALITY nationalityList = new Dict.NATIONALITY();
                nationalityList.selectable = true;
                nationality.DisplayMember = "name";
                nationality.ValueMember = "value";
                nationality.DataSource = nationalityList.DataTable;

                Dict.SEX sexList = new Dict.SEX();
                sexList.selectable = true;
                sex.DisplayMember = "name";
                sex.ValueMember = "value";
                sex.DataSource = sexList.DataTable;

                Dict.OCCU_EXTYPE occuList = new Dict.OCCU_EXTYPE();
                occuList.selectable = true;
                occu_type.DisplayMember = "name";
                occu_type.ValueMember = "value";
                occu_type.DataSource = occuList.DataTable;

                Dict.EDUCATION eduList = new Dict.EDUCATION();
                eduList.selectable = true;
                education.DisplayMember = "name";
                education.ValueMember = "value";
                education.DataSource = eduList.DataTable;

                Dict.CITIZENSHIP citizenshipList = new Dict.CITIZENSHIP();
                citizenshipList.selectable = true;
                citizenship.DisplayMember = "name";
                citizenship.ValueMember = "value";
                citizenship.DataSource = citizenshipList.DataTable;

                Dict.CubsbScOpenAcctOpType cubsbScOpenAcctOpTypeList = new Dict.CubsbScOpenAcctOpType();
                cbxCubsbScOpenAcctOpType.DisplayMember = "name";
                cbxCubsbScOpenAcctOpType.ValueMember = "value";
                cbxCubsbScOpenAcctOpType.DataSource = cubsbScOpenAcctOpTypeList.DataTable;

                Dict.CustomDict bankCodeList = new Dict.CustomDict("BANK_CODE");
                bankCodeList.selectable = true;
                bank_code.DisplayMember = "name";
                bank_code.ValueMember = "value";
                bank_code.DataSource = bankCodeList.DataTable;

                Dict.OPEN_TYPE openTypeList = new Dict.OPEN_TYPE();
                cbxOpenType.DisplayMember = "name";
                cbxOpenType.ValueMember = "value";
                cbxOpenType.DataSource = openTypeList.DataTable;

                dtpBGN_DATE.Value = DateTime.Now.AddYears(-1).Date;
                dtpEND_DATE.Value = DateTime.Now.Date;

                if (occu_type.SelectedValue.ToString() != Dict.OCCU_EXTYPE.其他)
                {
                    cbxOccupation.Enabled = false;
                    cbxOccupation.Text = "";
                }
                else
                {
                    cbxOccupation.Enabled = true;
                }

                dtpCybSignDate.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                resultForm.Append("载入程序时发生异常：" + ex.Message);
            }
        }
        
        /// <summary>
        /// 保存当前用户信息到User对象
        /// </summary>
        private bool saveUserInfo()
        {
            try
            {
                if (citizenship.Text == "")
                {
                    throw new Exception("没有选择国籍");
                }
                if (nationality.Text == "")
                {
                    throw new Exception("没有选择民族");
                }
                if (occu_type.Text == "")
                {
                    throw new Exception("没有选择职业");
                }
                if (education.Text == "")
                {
                    throw new Exception("没有选择学历");
                }
                if (bank_code.Text == "")
                {
                    // throw new Exception("没有选择银行类别");
                }
                if (sex.Text == "")
                {
                    throw new Exception("没有选择性别");
                }
                user = new User();
                user.cust_code = tbxCustCode.Text.Trim();
                user.user_type = Dict.USER_TYPE.个人;
                user.user_name = user_name.Text.Trim();
                user.user_fname = user_name.Text.Trim();
                user.id_type = Dict.ID_TYPE.身份证;
                user.id_code = id_code.Text.Trim();
                user.id_addr = id_addr.Text.Trim();
                user.id_iss_agcy = id_iss_agcy.Text.Trim();
                user.id_beg_date = id_beg_date.Text.Trim();
                user.id_exp_date = id_exp_date.Text.Trim();
                user.linktel_order = Dict.LINKTEL_ORDER.手机;
                user.linkaddr_order = Dict.LINKADDR_ORDER.家庭地址;
                user.address = address.Text.Trim();
                user.citizenship = citizenship.SelectedValue.ToString();
                user.nationality = nationality.SelectedValue.ToString();
                user.password = password.Text.Trim();
                user.mobile_tel = mobile_tel.Text.Trim();
                user.occu_type = occu_type.SelectedValue.ToString();
                if (occu_type.SelectedValue.ToString() == Dict.OCCU_EXTYPE.其他 && cbxOccupation.Text == "")
                {
                    throw new Exception("当职业为其他时，手输职业不能为空！");
                }
                user.occupation = cbxOccupation.Text;
                user.education = education.SelectedValue.ToString();
                user.bank_code = bank_code.SelectedValue.ToString();
                user.zip_code = zip_code.Text.Trim();
                user.sex = sex.SelectedValue.ToString();
                user.int_org = Settings.Default.开户营业部;
                user.cust_cls = Dict.CUST_CLS.标准客户;
                user.cust_type = Dict.CUST_TYPE.普通;
                user.channels = tbChannels.Text.Trim();

                user.cust_code = tbxCustCode.Text.Trim();
                user.cuacct_code = tbxCuacct.Text.Trim();
                user.ymt_code = tbxYMTCode.Text.Trim();
                user.shacct = tbxSHAcct.Text.Trim();
                user.szacct = tbxSZAcct.Text.Trim();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("保存用户信息失败：" + ex.Message);
            }
        }

        private async void btnBankSign_Click(object sender, EventArgs e)
        {
            btnBankSign.Enabled = false;
            try
            {
                saveUserInfo();
                await signBank();
            }
            catch (Exception ex)
            {
                resultForm.Append("三方存管签约失败：" + ex.Message);
            }
            btnBankSign.Enabled = true;
        }

        private async void btnSubmitRiskTest_Click(object sender, EventArgs e)
        {
            btnSubmitRiskTest.Enabled = false;

            try
            {
                await syncSurveyAns2Kbss();
                await queryRiskSurveyResult();
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }

            btnSubmitRiskTest.Enabled = true;
        }

        private async void btnSetPassword_Click(object sender, EventArgs e)
        {
            btnSetPassword.Enabled = false;

            try
            {
                await queryCustCode();

                await mdfUserPassword();
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }

            btnSetPassword.Enabled = true;
        }

        /// <summary>
        /// 执行开立资金账户操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnOpenCuacct_Click(object sender, EventArgs e)
        {
            btnOpenCuacct.Enabled = false;

            try
            {
                saveUserInfo();
                await openCuacctCode();
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }

            btnOpenCuacct.Enabled = true;
        }

        private async void btnOpenYMT_Click(object sender, EventArgs e)
        {
            btnOpenYMT.Enabled = false;

            try
            {
                saveUserInfo();
                await openYMTCode();
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }

            btnOpenYMT.Enabled = true;
        }

        private async void btnOpenSHAStkAcct_Click(object sender, EventArgs e)
        {
            btnOpenSHAStkAcct.Enabled = false;
            try
            {
                saveUserInfo();
                await openSHACode();
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
            btnOpenSHAStkAcct.Enabled = true;
        }

        private async void btnQueryStockAccount_Click(object sender, EventArgs e)
        {
            btnQueryStockAccount.Enabled = false;
            try
            {
                Response response = await kess.queryStkAcct(Dict.USER_TYPE.个人, user_name.Text.Trim(), Dict.ID_TYPE.身份证, id_code.Text.Trim(), Settings.Default.开户营业部);
                resultForm.Append("该客户有" + response.length.ToString() + "个证券账户");

                if (response.length > 0)
                {
                    // 显示所有股东账户的信息，包括卡号、市场、状态等
                    Dict.ACCT_TYPE acctTypeList = new Dict.ACCT_TYPE();
                    Dict.ACCT_STATUS acctStatusList = new Dict.ACCT_STATUS();
                    foreach (DataRow ds in response.DataSet.Tables["row"].Rows)
                    {
                        string acctType = acctTypeList.getNameByValue(ds["ACCT_TYPE"].ToString());
                        // string status = acctStatusList.getNameByValue(ds["ACCT_STATUS"].ToString());

                        resultForm.Append(
                            acctType + "：" + ds["TRDACCT"].ToString() + 
                            "，状态：" + ds["ACCT_STATUS"].ToString() + 
                            "，一码通号：" + ds["YMT_CODE"].ToString()
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
            btnQueryStockAccount.Enabled = true;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (await kess.operatorLogin())
                {
                    resultForm.Show();
                    resultForm.Append("操作员登录成功");
                }
            }
            catch (Exception ex)
            {
                resultForm.Append("操作员登录失败：" + ex.Message);
            }
        }

        private async void btnRegisterStockAccount_Click(object sender, EventArgs e)
        {
            resultForm.Show();
            try
            {
                saveUserInfo();
                await registerSHACode();
            }
            catch (Exception ex)
            {
                resultForm.Append("股东账号加挂失败：" + ex.Message);
            }
        }

        private async void btnOpenCYB_Click(object sender, EventArgs e)
        {
            btnOpenCYB.Enabled = false;
            try
            {
                saveUserInfo();
                await kess.openCyb2ZD(user, cbxOpenType.SelectedValue.ToString(), dtpCybSignDate.Text, timeout: Settings.Default.中登超时时间);
                resultForm.Append("中登创业板开通成功");
                await kess.openCyb2KBSS(user, Dict.OPEN_TYPE.T加2, dtpCybSignDate.Text, dtpCybSignDate.Text);
                resultForm.Append("系统内创业板协议签署成功");
            }
            catch (Exception ex)
            {
                resultForm.Append("创业板开通失败：" + ex.Message);
            }
            btnOpenCYB.Enabled = true;
        }
        
        private async void btnOpenSZAStkAcct_Click(object sender, EventArgs e)
        {
            btnOpenSZAStkAcct.Enabled = false;

            try
            {
                saveUserInfo();
                await openSZACode();
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }

            btnOpenSZAStkAcct.Enabled = true;
        }

        private async void btnRegisterSZAStkAcct_Click(object sender, EventArgs e)
        {
            btnRegisterSZAStkAcct.Enabled = false;
            try
            {
                saveUserInfo();
                await registerSZACode();
            }
            catch (Exception ex)
            {
                resultForm.Append("股东账号加挂失败：" + ex.Message);
            }
            btnRegisterSZAStkAcct.Enabled = true;
        }

        /// <summary>
        /// 开客户号
        /// </summary>
        private async Task openCustCode()
        {
            // 开客户号
            try
            {
                user.cust_code = await kess.createCustomerCode(user);
                user.user_code = user.cust_code;
                resultForm.Append("客户号开立成功：" + user.cust_code);
                tbxCustCode.Text = user.cust_code;

                // 更新职业
                try
                {
                    await kess.mdfUserExtInfo(CUST_CODE: user.cust_code, OPERATION_TYPE: "0", OCCU_TYPE: user.occu_type, OCCUPATION: user.occupation);
                }
                catch (Exception ex)
                {
                    resultForm.Append("用户扩展信息更新失败！" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                resultForm.Append("客户号开立失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 开资金号
        /// </summary>
        private async Task openCuacctCode()
        {
            // 开资金号
            user.cuacct_cls = tbCuacct_cls.Text.Trim();
            user.cuacct_code = await kess.openCuacctCode(user);
            resultForm.Append("资金账号开立成功：" + user.cuacct_code);
            tbxCuacct.Text = user.cuacct_code;
        }

        /// <summary>
        /// 设置交易和资金密码
        /// </summary>
        private async Task mdfUserPassword()
        {
            // 重置交易密码
            bool result = await kess.mdfUserPassword(Dict.OPERATION_TYPE.重置密码, tbxCustCode.Text.Trim(),password.Text,USE_SCOPE: Dict.USE_SCOPE.登录和交易);
            if (result)
            {
                resultForm.Append("重置交易密码成功，新密码：" + password.Text);
            }

            // 重置资金密码
            result = await kess.mdfUserPassword(Dict.OPERATION_TYPE.重置密码, tbxCustCode.Text.Trim(), password.Text, USE_SCOPE: Dict.USE_SCOPE.资金业务);
            if (result)
            {
                resultForm.Append("重置资金密码成功，新密码：" + password.Text);
            }
        }

        /// <summary>
        /// 预指定三方存管
        /// </summary>
        private async Task signBank()
        {
            bool result = false;

            if (cbxCubsbScOpenAcctOpType.SelectedValue.ToString() == Dict.CubsbScOpenAcctOpType.预指定)
            {
                result = await kess.cubsbScOpenAcct("1", tbxCuacct.Text.Trim(), bank_code.SelectedValue.ToString());

                if (result)
                {
                    resultForm.Append("三方存管预指定成功");
                }
            }
            else if (cbxCubsbScOpenAcctOpType.SelectedValue.ToString() == Dict.CubsbScOpenAcctOpType.一步式)
            {
                result = await kess.cubsbScOpenAcct("0", tbxCuacct.Text.Trim(), bank_code.SelectedValue.ToString(), tbxCustCode.Text.Trim(), tbxBankAcctCode.Text.Trim());

                if (result)
                {
                    resultForm.Append("三方存管一步式签约成功");
                }
            }
        }

        /// <summary>
        /// 提交风险测评
        /// </summary>
        private async Task syncSurveyAns2Kbss()
        {
            // 提交风险测评
            string cols = Settings.Default.Cols;
            string cells = "";
            switch (risk_level.SelectedValue.ToString())
            {
                case "A":
                    cells = Settings.Default.保守型;
                    break;
                case "B":
                    cells = Settings.Default.谨慎型;
                    break;
                case "C":
                    cells = Settings.Default.稳健型;
                    break;
                case "D":
                    cells = Settings.Default.积极型;
                    break;
                case "E":
                    cells = Settings.Default.激进型;
                    break;

                default:
                    string message = "风险等级" + risk_level.SelectedValue.ToString() + "不存在";
                    logger.Error(message);
                    throw new Exception(message);
            }

            bool result = await kess.syncSurveyAns2Kbss(tbxCustCode.Text.Trim(), Settings.Default.SURVEY_SN, cols, cells);
            if (result)
            {
                Action action = () =>
                {
                    resultForm.Append("提交风险测评成功");
                };
                this.Invoke(action);
            }
        }

        /// <summary>
        /// 开一码通
        /// </summary>
        private async Task openYMTCode()
        {
            try
            {
                // 开一码通
                Response response = await kess.openYMTAcct(user.user_type, user.user_fname, user.id_type, user.id_code, user.int_org, user.cust_code, user.birthday, user.id_beg_date, user.id_exp_date, user.citizenship, user.id_addr, user.address, user.zip_code, user.occu_type, user.nationality, user.education, user.tel, user.mobile_tel, user.sex);
                if (response.length > 2)
                {
                    throw new Exception("该客户有" + response.length.ToString() + "个一码通账号");
                }
                else if (response.length == 0)
                {
                    throw new Exception("没有返回一码通账号列表");
                }
                string ymtCode = response.getValue("YMT_CODE");
                resultForm.Append("一码通账号开立成功：" + response.getValue("YMT_CODE"));
                user.ymt_code = ymtCode;
                tbxYMTCode.Text = ymtCode;
            }
            catch (Exception ex)
            {
                throw new Exception("一码通账号开立失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 新开沪A账户
        /// </summary>
        async private Task openSHACode()
        {
            // 新开沪A账户
            Response response = await kess.openStkAcct(user, Dict.ACCT_TYPE.沪市A股账户);
            user.shacct = response.getValue("TRDACCT");
            resultForm.Append("沪A股东账号开立成功：" + user.shacct);
            tbxSHAcct.Text = user.shacct;
        }

        /// <summary>
        /// 加挂沪A账户
        /// </summary>
        private async Task registerSHACode()
        {
            // 加挂沪A账户
            if (await kess.registerSHAStkTrdAcct(user))
            {
                resultForm.Append("沪A股东账号加挂成功");
            }
        }

        /// <summary>
        /// 上海账户指定交易
        /// </summary>
        private async Task bindSHAcct()
        {
            Response response = await kess.listStkPbuOrg(Dict.STKBD.上海A股, Settings.Default.开户营业部);
            await kess.stkTrdacctBind(
                tbxCustCode.Text.Trim(),
                response.getValue("STKPBU"),
                Dict.STKBD.上海A股,
                tbxSHAcct.Text.Trim(),
                Dict.TREG_STATUS.首日指定
            );
            resultForm.Append("上海证券账户" + tbxSHAcct.Text + "指定交易成功" + "，交易单元为：" + response.getValue("STKPBU"));
        }

        /// <summary>
        /// 新开深A账户
        /// </summary>
        private async Task openSZACode()
        {
            // 新开深A账户
            Response response = await kess.openStkAcct(user, Dict.ACCT_TYPE.深市A股账户);
            tbxSHAcct.Text = response.getValue("TRDACCT");
            resultForm.Append("深A股东账号开立成功：" + tbxSHAcct.Text);
            tbxSZAcct.Text = tbxSHAcct.Text;
        }

        /// <summary>
        /// 加挂深A账户
        /// </summary>
        private async Task registerSZACode()
        {
            // 加挂深A账户
            if (await kess.registerSZAStkTrdAcct(user))
            {
                resultForm.Append("深A股东账号加挂成功");
            }
        }

        /// <summary>
        /// 查询深A创业板信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnQueryCYB_Click(object sender, EventArgs e)
        {
            btnQueryCYB.Enabled = false;
            try
            {
                // 清空显示
                tbxCybSignDate.Text = "";

                // 根据股东账号查询创业板信息
                Response response = await kess.queryCYB(tbxSZAcct.Text.Trim(), Settings.Default.中登超时时间);

                tbxCybSignDate.Text = response.getValue("SIGN_DATE");

                Dict.SIGN_CLS signClsList = new Dict.SIGN_CLS();
                tbxCybSignDate.Text += "；" + signClsList.getNameByValue(response.getValue("SIGN_CLS"));
            }
            catch (Exception ex)
            {
                resultForm.Append("创业板查询失败：" + ex.Message);
            }
            btnQueryCYB.Enabled = true;
        }

        /// <summary>
        /// 打开当前日志文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenLogFile_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.CurrentDirectory + @"\logs\" + DateTime.Now.ToString("yyyy-MM-dd") + @".log";
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        /// <summary>
        /// 打开日志文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenLogFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.CurrentDirectory + @"\logs\";
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }
        
        /// <summary>
        /// 生成身份证正面图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateIDCardImg_Click(object sender, EventArgs e)
        {
            try
            {
                if (id_code.Text.Length != 18)
                {
                    throw new Exception("只支持生成18位身份证的正面照。");
                }
                saveUserInfo();
                createIdCardImgFaceSide(user.user_name, user.sex, user.nationality, user.birthday, user.id_addr, user.id_code);
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        /// <summary>
        /// 生成身份证背面图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateIDCardImgBackSide_Click(object sender, EventArgs e)
        {
            try
            {
                saveUserInfo();
                createIdCardImgBackSide(user.id_iss_agcy, user.id_beg_date, user.id_exp_date);
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        /// <summary>
        /// 生成身份证正面照图片
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="nationality">民族</param>
        /// <param name="birthday">生日</param>
        /// <param name="addr">证件地址</param>
        /// <param name="idno">证件号码</param>
        private void createIdCardImgFaceSide(string name, string sex, string nationality, string birthday, string addr, string idno)
        {
            Dict.SEX dicSex = new Dict.SEX();
            sex = dicSex.getNameByValue(sex).Replace("性", "");

            Dict.NATIONALITY dicNatinality = new Dict.NATIONALITY();
            nationality = dicNatinality.getNameByValue(nationality).Replace("族","");

            birthday = birthday.Substring(0, 4) + "      " + int.Parse(birthday.Substring(4, 2)).ToString() + "      " + int.Parse(birthday.Substring(6, 2)).ToString();

            // 超长地址自动换行
            string newAddr = Formatter.lineWarp(addr, 13);

            Image image;// 具体这张图是从文件读取还是从picturebox什么的获取你来指定
            image = Resources.样本身份证正面;
            using (Graphics g = Graphics.FromImage(image))
            {
                g.DrawString(name, new Font("黑体", 13), Brushes.Black, new PointF(400, 220));    // 姓名
                g.DrawString(sex, new Font("黑体", 13), Brushes.Black, new PointF(400, 360));    // 性别
                g.DrawString(nationality, new Font("黑体", 13), Brushes.Black, new PointF(800, 360));    // 民族
                g.DrawString(birthday, new Font("黑体", 13), Brushes.Black, new PointF(400, 500));  // 出生年月日
                g.DrawString(newAddr, new Font("黑体", 13), Brushes.Black, new PointF(400, 630));    // 住址
                g.DrawString(idno, new Font("黑体", 20), Brushes.Black, new PointF(750, 960));    // 身份证号码
                g.Flush();
            }
            image.Save(Environment.CurrentDirectory + @"\身份证正面.jpg");
            System.Diagnostics.Process.Start(Environment.CurrentDirectory + @"\身份证正面.jpg");
        }

        /// <summary>
        /// 生成身份证照片反面
        /// </summary>
        /// <param name="ID_ISS_AGCY">发证机关</param>
        /// <param name="ID_BEG_DATE">开始日期，YYYYMMDD格式</param>
        /// <param name="ID_EXP_DATE">结束日期，YYYYMMDD格式，30001231表示长期</param>
        private void createIdCardImgBackSide(string ID_ISS_AGCY, string ID_BEG_DATE, string ID_EXP_DATE)
        {
            string iddate = "";
            ID_BEG_DATE = ID_BEG_DATE.Substring(0, 4) + "." + ID_BEG_DATE.Substring(4, 2) + "." + ID_BEG_DATE.Substring(6, 2);

            if (ID_EXP_DATE == "30001231")
            {
                iddate = ID_BEG_DATE + "-" + "长期";
            }
            else
            {
                iddate = ID_BEG_DATE + "-" + ID_EXP_DATE.Substring(0, 4) + "." + ID_EXP_DATE.Substring(4, 2) + "." + ID_EXP_DATE.Substring(6, 2);
            }
            

            Image image = Resources.样本身份证反面;
            using (Graphics g = Graphics.FromImage(image))
            {
                g.DrawString(ID_ISS_AGCY, new Font("黑体", 13), Brushes.Black, new PointF(800, 860));
                g.DrawString(iddate, new Font("黑体", 13), Brushes.Black, new PointF(800, 1000));
                g.Flush();
            }
            image.Save(Environment.CurrentDirectory + @"\身份证背面.jpg");
            System.Diagnostics.Process.Start(Environment.CurrentDirectory + @"\身份证背面.jpg");
        }

        /// <summary>
        /// 根据查询条件从柜台取得客户号
        /// </summary>
        private async Task queryCustCode()
        {
            if (tbxCustCode.Text.Trim() == "" && tbxCuacctCondition.Text.Trim() == "")
            {
                throw new Exception("客户号或资金账号不能同时为空");
            }

            Response response;

            if (tbxCuacctCondition.Text.Trim() != "")
            {
                response = await kess.queryCustInfoByCuacct(tbxCuacctCondition.Text.Trim(), tbxCustCode.Text.Trim());
                if (response.length == 0)
                {
                    throw new Exception("没有找到对应的客户，请确认客户代码和资金账号是否正确？");
                }
                tbxCustCode.Text = response.getValue("USER_CODE");
            }
        }

        private async Task queryCustBasicInfoList()
        {
            try
            {
                // 基本资料
                Response response = await kess.queryCustBasicInfoList(tbxCustCode.Text.Trim());
                user_name.Text = response.getValue("user_name");
                id_code.Text = response.getValue("id_code");
                id_iss_agcy.Text = response.getValue("id_iss_agcy");
                id_beg_date.Text = response.getValue("id_beg_date");
                id_exp_date.Text = response.getValue("id_exp_date");
                id_addr.Text = response.getValue("id_addr");
                zip_code.Text = response.getValue("zip_code");
                address.Text = response.getValue("address");
                mobile_tel.Text = response.getValue("mobile_tel");
                citizenship.SelectedValue = response.getValue("citizenship");
                nationality.SelectedValue = response.getValue("NATIONALITY");
                sex.SelectedValue = response.getValue("sex");
                education.SelectedValue = response.getValue("education");
            }
            catch (Exception ex)
            {
                resultForm.Append("查询基本资料失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 查询职业信息
        /// </summary>
        private async Task getUserOccuInfo()
        {
            try
            {
                // 职业信息
                Response response = await kess.getUserOccuInfo(tbxCustCode.Text.Trim());
                if (response.length == 0)
                {
                    resultForm.Append("没有找到职业信息");
                }
                else
                {
                    occu_type.SelectedValue = response.getValue("OCCU_TYPE");
                    if (response.Record.Columns.Contains("OCCUPATION"))
                    {
                        cbxOccupation.Text = response.getValue("OCCUPATION");
                    }
                    else
                    {
                        cbxOccupation.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                resultForm.Append("查询职业信息失败：" + ex.Message);
            }
        }
        
        /// <summary>
        /// 查询最近一条风险测评记录
        /// </summary>
        private async Task queryLastRiskSurveyResult()
        {
            try
            {
                // 风险测评
                Response response = await kess.queryRiskSurveyResult(Settings.Default.SURVEY_SN, tbxCustCode.Text.Trim(), Dict.USER_ROLE.客户);
                if (response.length == 0)
                {
                    resultForm.Append("没有找到风险测评记录");
                }
                else
                {
                    resultForm.Append("风险测评级别为：" + response.getValue("RATING_LVL_NAME"));
                }
            }
            catch (Exception ex)
            {
                resultForm.Append("查询风险测评记录失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 查询资金账号
        /// </summary>
        private async Task listCuacct()
        {
            try
            {
                // 查询资金账号
                Response response = await kess.listCuacct(tbxCustCode.Text.Trim());
                resultForm.Append("客户号下找到" + response.length.ToString() + "个资金账号。");

                if (response.length > 1)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        Dict.CUACCT_ATTR cuacct_attrList = new Dict.CUACCT_ATTR();
                        Dict.CUACCT_STATUS cuacct_statusList = new Dict.CUACCT_STATUS();

                        resultForm.Append(
                            "类型：" + cuacct_attrList.getNameByValue(dr["CUACCT_ATTR"].ToString())
                            + "，账户：" + dr["CUACCT_CODE"].ToString()
                            + "，状态：" + cuacct_statusList.getNameByValue(dr["CUACCT_STATUS"].ToString())
                            + "，是否主资产账户：" + dr["MAIN_FLAG"].ToString()
                        );

                        if (dr["CUACCT_ATTR"].ToString() == Dict.CUACCT_ATTR.普通账户
                            && dr["MAIN_FLAG"].ToString() == "1"
                            && dr["CUACCT_STATUS"].ToString() == Dict.CUACCT_STATUS.正常)
                        {
                            tbxCuacct.Text = dr["CUACCT_CODE"].ToString();
                        }
                        else if (dr["CUACCT_ATTR"].ToString() == Dict.CUACCT_ATTR.信用账户
                            && dr["CUACCT_STATUS"].ToString() == Dict.CUACCT_STATUS.正常)
                        {
                            tbxFislCuacct.Text = dr["CUACCT_CODE"].ToString();
                        }
                    }
                }
                else if (response.length == 1)
                {
                    tbxCuacct.Text = response.getValue("CUACCT_CODE");
                }
                else if (response.length == 0)
                {
                    throw new Exception("客户号下没有开立资金账号，停止处理。");
                }
            }
            catch (Exception ex)
            {
                resultForm.Append("查询资金账号失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 查询一码通
        /// </summary>
        private async Task<int> queryStkYmt()
        {
            try
            {
                // 查询一码通
                Response response = await kess.queryStkYmt(tbxCustCode.Text.Trim());
                if (response.length == 0)
                {
                    resultForm.Append("没有查询到柜台一码通信息，原因：" + response.prompt);

                }
                else
                {
                    response.translate(new Dict.YMT_STATUS());
                    resultForm.Append("从柜台查询到" + response.length + "条一码通信息，一码通状态：" + response.getValue("YMT_STATUS"));
                    tbxYMTCode.Text = response.getValue("YMT_CODE");
                }
                return response.length;
            }
            catch (Exception ex)
            {
                resultForm.Append("查询一码通失败：" + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// 从中登查询一码通
        /// </summary>
        /// <returns></returns>
        private async void queryYMT()
        {
            try
            {
                resultForm.Append("正在发起中登查询一码通信息，请稍候。。。");
                Response response = await kess.queryYMT(id_code.Text.Trim(), user_name.Text.Trim());
                resultForm.Append("从中登找到该客户的" + response.length.ToString() + "个一码通账号。");

                foreach (DataRow row in response.TranslatedRecord.Rows)
                {
                    resultForm.Append(
                        "一码通账号：" + row["YMT_CODE"] +
                        "，账户状态：" + row["YMT_STATUS"]
                    );
                }
                tbxYMTCode.Text = response.getValue("YMT_CODE");
            }
            catch (Exception ex)
            {
                resultForm.Append("中登查询一码通失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 查询系统内股东账户
        /// </summary>
        private async Task listOfStkTrdAcct()
        {
            try
            {
                // 查询系统内股东账号
                Response response = await kess.listOfStkTrdAcct(tbxCustCode.Text.Trim());
                resultForm.Append("系统内找到" + response.length.ToString() + "个股东账号。");

                if (response.length > 0)
                {
                    foreach (DataRow ds in response.Rows)
                    {
                        if (ds["STKBD"].ToString() == Dict.STKBD.上海A股)
                        {
                            tbxSHAcct.Text = ds["TRDACCT"].ToString();
                        }
                        if (ds["STKBD"].ToString() == Dict.STKBD.深圳A股)
                        {
                            tbxSZAcct.Text = ds["TRDACCT"].ToString();
                        }
                    }

                    // 显示所有股东账户的信息，包括卡号、市场、状态等
                    foreach (DataRow dr in response.TranslatedRecord.Rows)
                    {
                        resultForm.Append(
                            "交易版块：" + dr["STKBD"].ToString() +
                            "，交易账户：" + dr["TRDACCT"].ToString() +
                            "，账户类别：" + dr["TRDACCT_EXCLS"].ToString() +
                            "，账户状态：" + dr["TRDACCT_STATUS"].ToString()
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                resultForm.Append("查询股东账户失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 查询诚信记录
        /// </summary>
        private async Task queryCreditRecord()
        {
            // 诚信记录
            Response response = await kess.qryCreditRecord(tbxCustCode.Text.Trim());
            if (response.length == 0)
            {
                resultForm.Append("没有找到不良诚信记录");
                dgvClear(ref dgv诚信记录);
            }
            else
            {
                resultForm.Append("找到" + response.length + "条不良诚信记录");
                dgv诚信记录.DataSource = response.TranslatedRecord;
            }
        }

        /// <summary>
        /// 查询风险测评记录
        /// </summary>
        private async Task queryRiskSurveyResult()
        {
            btnQueryRiskSurveyResult.Enabled = false;
            try
            {
                // 风险测评
                Response response = await kess.queryRiskSurveyResult(Settings.Default.SURVEY_SN, tbxCustCode.Text.Trim(), Dict.USER_ROLE.客户, dtpBGN_DATE.Value.ToString("yyyyMMdd"), dtpEND_DATE.Value.ToString("yyyyMMdd"));
                if (response.length == 0)
                {
                    resultForm.Append("没有找到风险测评记录");
                    dgvClear(ref dgvRiskSurvey);
                }
                else
                {
                    resultForm.Append("找到" + response.length + "条风险测评记录");
                    dgvRiskSurvey.DataSource = response.TranslatedRecord;
                    dgvRiskSurvey.Sort(dgvRiskSurvey.Columns["ORDINAL"], System.ComponentModel.ListSortDirection.Descending);
                }
            }
            catch (Exception ex)
            {
                resultForm.Append("查询风险测评结果失败：" + ex.Message);
            }
            btnQueryRiskSurveyResult.Enabled = true;
        }

        /// <summary>
        /// 查询受益人信息
        /// </summary>
        private async Task queryUserBeneficiaryInfo()
        {
            // 受益人信息
            Response response = await kess.queryUserBeneficiaryInfo(tbxCustCode.Text.Trim());
            if (response.length == 0)
            {
                resultForm.Append("没有找到受益人信息");
                dgvClear(ref dgv受益人);
            }
            else
            {
                resultForm.Append("找到" + response.length + "条受益人信息");
                dgv受益人.DataSource = response.TranslatedRecord;
            }
        }

        /// <summary>
        /// 查询控制人信息
        /// </summary>
        private async Task queryControllerInfo()
        {
            // 控制人信息
            Response response = await kess.queryControllerInfo(tbxCustCode.Text.Trim());
            if (response.length == 0)
            {
                resultForm.Append("没有找到控制人信息");
                dgvClear(ref dgv控制人);
            }
            else
            {
                resultForm.Append("找到" + response.length + "条控制人信息");
                dgv控制人.DataSource = response.TranslatedRecord;
            }
        }

        /// <summary>
        /// 查询协议签署情况
        /// </summary>
        private async Task queryCustAgreement()
        {
            // 查询协议签署情况
            Response response = await kess.queryCustAgreement(tbxCustCode.Text.Trim());
            if (response.length == 0)
            {
                resultForm.Append(response.prompt);
                dgvClear(ref dgv已签署协议);
            }
            else if (response.length > 0)
            {
                resultForm.Append("客户已经签署了" + response.length + "种协议。");
                dgv已签署协议.DataSource = response.TranslatedRecord;
            }
        }

        /// <summary>
        /// 一键查询所有客户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnQueryByUserCode_Click(object sender, EventArgs e)
        {
            btnQueryByUserCode.Enabled = false;

            try
            {
                Reset();

                await queryCustCode();

                await queryCustBasicInfoList();

                await getUserOccuInfo();

                await queryLastRiskSurveyResult();

                await queryCreditRecord();

                await queryUserBeneficiaryInfo();

                await queryControllerInfo();

                await queryCustAgreement();

                await queryRiskSurveyResult();

                await listCuacct();

                if(await queryStkYmt() == 0)
                {
                    queryYMT();
                }

                await listOfStkTrdAcct();
                
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }

            btnQueryByUserCode.Enabled = true;
        }

        /// <summary>
        /// 清空一个DataGridView的数据
        /// </summary>
        /// <param name="dgv"></param>
        private void dgvClear(ref DataGridView dgv)
        {
            if (dgv.DataSource!=null)
            {
                DataTable dt = (DataTable)dgv.DataSource;
                dt.Rows.Clear();
                dgv.DataSource = dt;
            }
        }

        private async void btnBindSHAcct_Click(object sender, EventArgs e)
        {
            btnBindSHAcct.Enabled = false;

            try
            {
                saveUserInfo();
                await bindSHAcct();
            }
            catch (Exception ex)
            {
                resultForm.Append("上海证券账户" + user.shacct + "指定交易失败：" + ex.Message);
            }

            btnBindSHAcct.Enabled = true;
        }
        
        private void btnLoadRequestXml_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Environment.CurrentDirectory, Request.xmlPath);
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message + "：" + path);
            }
        }

        private void occu_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (occu_type.SelectedValue.ToString() != Dict.OCCU_EXTYPE.其他)
            {
                cbxOccupation.ResetText();
                cbxOccupation.Enabled = false;
            }
            else
            {
                cbxOccupation.Enabled = true;
            }
        }
        
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Settings.Default.默认开通的资产账户类别 = tbCuacct_cls.Text.Trim();
            //Settings.Default.默认开通的操作渠道 = tbChannels.Text.Trim();
            //Settings.Default.默认开通的银行类型 = bank_code.SelectedValue.ToString();
            //Settings.Default.Save();
        }

        private void cbxCubsbScOpenAcctOpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCubsbScOpenAcctOpType.SelectedValue.ToString() == Dict.CubsbScOpenAcctOpType.一步式)
            {
                tbxBankAcctCode.Enabled = true;
            }
            else if (cbxCubsbScOpenAcctOpType.SelectedValue.ToString() == Dict.CubsbScOpenAcctOpType.预指定)
            {
                tbxBankAcctCode.Enabled = false;
            }
        }

        private void frmExistAccount_Shown(object sender, EventArgs e)
        {
            tbxCustCode.Focus();
        }

        /// <summary>
        /// 清空所有信息
        /// </summary>
        private void Reset()
        {
            user_name.Text = "";
            tbxYMTCode.Text = "";
            tbxCuacct.Text = "";
            tbxSHAcct.Text = "";
            tbxSZAcct.Text = "";
            tbxCybSignDate.Text = "";
            tbxBankAcctCode.Text = "";

            address.Text = "";
            id_addr.Text = "";
            id_code.Text = "";
            id_iss_agcy.Text = "";
            id_beg_date.Text = "";
            id_exp_date.Text = "";
            mobile_tel.Text = "";
            zip_code.Text = "";
            password.Text = "111111";

            nationality.SelectedIndex = 0;
            citizenship.SelectedIndex = 0;
            risk_level.SelectedIndex = 0;
            sex.SelectedIndex = 0;
            occu_type.SelectedIndex = 0;
            education.SelectedIndex = 0;
            cbxOccupation.Text = "";

            dgvClear(ref dgv受益人);
            dgvClear(ref dgv已签署协议);
            dgvClear(ref dgv控制人);
            dgvClear(ref dgv诚信记录);
            dgvClear(ref dgvRiskSurvey);
        }

        private void tbxCustCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQueryByUserCode.PerformClick();
            }
        }

        private void tbxCuacctCondition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQueryByUserCode.PerformClick();
            }
        }

        async private void btnMdfCustBasicInfo_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = await kess.mdfUserGenInfo(
                    tbxCustCode.Text.Trim(),
                    zip_code.Text.Trim(),
                    address.Text.Trim(),
                    EDUCATION: education.SelectedValue.ToString(),
                    CITIZENSHIP: citizenship.SelectedValue.ToString(),
                    MOBILE_TEL: mobile_tel.Text.Trim(),
                    NATIONALITY: nationality.SelectedValue.ToString(),
                    ID_ISS_AGCY: id_iss_agcy.Text.Trim(),
                    ID_ADDR: id_addr.Text.Trim(),
                    ID_BEG_DATE: id_beg_date.Text.Trim(),
                    ID_EXP_DATE: id_exp_date.Text.Trim(),
                    SEX: sex.SelectedValue.ToString()
                );
                resultForm.Append("修改客户基本信息成功");
            }
            catch (Exception ex)
            {
                resultForm.Append("修改客户资料失败：" + ex.Message);
            }

            try
            {
                await kess.mdfUserExtInfo(tbxCustCode.Text.Trim(),"1",OCCU_TYPE: occu_type.SelectedValue.ToString(),OCCUPATION:cbxOccupation.Text.Trim());
                resultForm.Append("修改客户职业信息成功");
            }
            catch (Exception ex)
            {
                resultForm.Append("修改客户职业信息失败：" + ex.Message);
            }
        }

        async private void btnMdfChannels_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = await kess.mdfFmsCustChannel(tbxCustCode.Text.Trim(), "", "", tbChannels.Text.Trim(), "测试工具修改");
                resultForm.Append("修改客户操作渠道成功");
            }
            catch (Exception ex)
            {
                resultForm.Append("修改客户资料失败：" + ex.Message);
            }
        }

        private async void btnAddBeneficirayInfo_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = await kess.mdfUserBeneficiaryInfo(
                    tbxCustCode.Text.Trim(),
                    Dict.OPERATION_TYPE.增加密码,
                    "1",
                    user_name.Text.Trim(),
                    Dict.ID_TYPE.身份证,
                    id_code.Text.Trim(),
                    id_exp_date.Text.Trim(),
                    mobile_tel.Text.Trim()
                );
                resultForm.Append("增加受益人成功");
                await queryUserBeneficiaryInfo();
            }
            catch (Exception ex)
            {
                resultForm.Append("增加受益人失败：" + ex.Message);
            }
        }

        private async void btnAddControllerInfo_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = await kess.mdfControlLerInfo(
                    Dict.OPERATION_TYPE.增加密码,
                    tbxCustCode.Text.Trim(),
                    id_code.Text.Trim(),
                    "1",
                    user_name.Text.Trim(),
                    Dict.ID_TYPE.身份证,
                    id_exp_date.Text.Trim(),
                    mobile_tel.Text.Trim()
                );
                resultForm.Append("增加控制人成功");
                await queryControllerInfo();
            }
            catch (Exception ex)
            {
                resultForm.Append("增加控制人失败：" + ex.Message);
            }
        }

        private async void btnQueryRiskSurveyResult_Click(object sender, EventArgs e)
        {
            await queryRiskSurveyResult();
        }
    }
}
