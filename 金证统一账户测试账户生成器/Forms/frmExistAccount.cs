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
                nationality.DisplayMember = "name";
                nationality.ValueMember = "value";
                nationality.DataSource = nationalityList.DataTable;

                Dict.SEX sexList = new Dict.SEX();
                sex.DisplayMember = "name";
                sex.ValueMember = "value";
                sex.DataSource = sexList.DataTable;

                Dict.OCCU_EXTYPE occuList = new Dict.OCCU_EXTYPE();
                occu_type.DisplayMember = "name";
                occu_type.ValueMember = "value";
                occu_type.DataSource = occuList.DataTable;

                Dict.EDUCATION eduList = new Dict.EDUCATION();
                education.DisplayMember = "name";
                education.ValueMember = "value";
                education.DataSource = eduList.DataTable;

                Dict.CITIZENSHIP citizenshipList = new Dict.CITIZENSHIP();
                citizenship.DisplayMember = "name";
                citizenship.ValueMember = "value";
                citizenship.DataSource = citizenshipList.DataTable;

                Dict.CubsbScOpenAcctOpType cubsbScOpenAcctOpTypeList = new Dict.CubsbScOpenAcctOpType();
                cbxCubsbScOpenAcctOpType.DisplayMember = "name";
                cbxCubsbScOpenAcctOpType.ValueMember = "value";
                cbxCubsbScOpenAcctOpType.DataSource = cubsbScOpenAcctOpTypeList.DataTable;

                Dict.CustomDict bankCodeList = new Dict.CustomDict("存管银行");
                bank_code.DisplayMember = "name";
                bank_code.ValueMember = "value";
                bank_code.DataSource = bankCodeList.DataTable;

                Dict.OPEN_TYPE openTypeList = new Dict.OPEN_TYPE();
                cbxOpenType.DisplayMember = "name";
                cbxOpenType.ValueMember = "value";
                cbxOpenType.DataSource = openTypeList.DataTable;

                //tbChannels.Text = Settings.Default.默认开通的操作渠道;
                //tbCuacct_cls.Text = Settings.Default.默认开通的资产账户类别;

                if (occu_type.SelectedValue.ToString() != Dict.OCCU_EXTYPE.其他)
                {
                    cbxOccupation.Enabled = false;
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
                user.address = id_addr.Text.Trim();
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
                user.cuacct_code = tbxCuacct.Text.Trim();
                await signBank();
            }
            catch (Exception ex)
            {
                resultForm.Append("预指定失败：" + ex.Message);
            }
            btnBankSign.Enabled = true;
        }

        private async void btnSubmitRiskTest_Click(object sender, EventArgs e)
        {
            btnSubmitRiskTest.Enabled = false;

            try
            {
                await syncSurveyAns2Kbss(risk_level.SelectedValue.ToString());
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
                saveUserInfo();
                Response response = await kess.queryStkAcct(user);
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
        /// 一次性开立所有账户
        /// </summary>
        async private Task openAllAccount()
        {
            try
            {
                // 使用当前用户信息
                saveUserInfo();

                await openCustCode();

                await openCuacctCode();

                await mdfUserPassword();

                await syncSurveyAns2Kbss(risk_level.SelectedValue.ToString());

                await signBank();

                await openYMTCode();

                await openSHACode();

                await registerSHACode();

                await openSZACode();

                await registerSZACode();

                await bindSHAcct();

            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
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
            // 设置交易密码
            bool result = await kess.mdfUserPassword(user, Dict.USE_SCOPE.登录和交易, Dict.OPERATION_TYPE.增加密码);
            if (result)
            {
                resultForm.Append("添加交易密码成功，新密码：" + user.password);
            }

            // 设置资金密码
            result = await kess.mdfUserPassword(user, Dict.USE_SCOPE.资金业务, Dict.OPERATION_TYPE.增加密码);
            if (result)
            {
                resultForm.Append("添加资金密码成功，新密码：" + user.password);
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
                result = await kess.cubsbScOpenAcct("1", user.cuacct_code, bank_code.SelectedValue.ToString());

                if (result)
                {
                    resultForm.Append("三方存管预指定成功");
                }
            }
            else if (cbxCubsbScOpenAcctOpType.SelectedValue.ToString() == Dict.CubsbScOpenAcctOpType.一步式)
            {
                result = await kess.cubsbScOpenAcct("0", user.cuacct_code, bank_code.SelectedValue.ToString(), user.cust_code, tbxBankAcctCode.Text.Trim());

                if (result)
                {
                    resultForm.Append("三方存管一步式签约成功");
                }
            }
        }

        /// <summary>
        /// 提交风险测评
        /// </summary>
        private async Task syncSurveyAns2Kbss(string riskLevel)
        {
            // 提交风险测评
            string cols = Settings.Default.Cols;
            string cells = "";
            switch (riskLevel)
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

            bool result = await kess.syncSurveyAns2Kbss(user, cols, cells);
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
                Response response = await kess.openYMTAcct(user.user_type, user.user_fname, user.id_type, user.id_code, user.int_org, user.cust_code, user.birthday, user.id_beg_date, user.id_exp_date, user.citizenship, user.id_addr, user.id_addr, user.zip_code, user.occu_type, user.nationality, user.education, user.tel, user.mobile_tel, user.sex);
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
                user.cust_code,
                response.getValue("STKPBU"),
                Dict.STKBD.上海A股,
                user.shacct,
                Dict.TREG_STATUS.首日指定
            );
            resultForm.Append("上海证券账户" + user.shacct + "指定交易成功" + "，交易单元为：" + response.getValue("STKPBU"));
        }

        /// <summary>
        /// 新开深A账户
        /// </summary>
        private async Task openSZACode()
        {
            // 新开深A账户
            Response response = await kess.openStkAcct(user, Dict.ACCT_TYPE.深市A股账户);
            user.szacct = response.getValue("TRDACCT");
            resultForm.Append("深A股东账号开立成功：" + user.szacct);
            tbxSZAcct.Text = user.szacct;
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
        
        async private void btnQueryByUserCode_Click(object sender, EventArgs e)
        {
            btnQueryByUserCode.Enabled = false;

            try
            {
                Reset();

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
                
                // 查询资金账号
                response = await kess.listCuacct(tbxCustCode.Text.Trim());
                resultForm.Append("客户号下找到" + response.length.ToString() + "个资金账号。");

                if (response.length > 1)
                {
                    throw new Exception("不支持多个资金账号，停止处理。");
                }
                if (response.length == 0)
                {
                    throw new Exception("客户号下没有开立资金账号，停止处理。");
                }
                tbxCuacct.Text = response.getValue("CUACCT_CODE");

                // 查询一码通
                resultForm.Append("正在发起中登查询一码通信息，请稍候。。。");
                response = await kess.queryYMT(id_code.Text.Trim(), user_name.Text.Trim());
                resultForm.Append("从中登找到该客户的" + response.length.ToString() + "个一码通账号。");
                Dict.YMT_STATUS ymtStatusList = new Dict.YMT_STATUS();
                foreach (DataRow row in response.Rows)
                {
                    string ymtStatus = ymtStatusList.getNameByValue(row["YMT_STATUS"].ToString());

                    resultForm.Append(
                        "一码通账号：" + response.getValue("YMT_CODE") +
                        "，账户状态：" + ymtStatus
                    );
                }
                tbxYMTCode.Text = response.getValue("YMT_CODE");

                // 查询系统内股东账号
                response = await kess.listOfStkTrdAcct(tbxCustCode.Text.Trim());
                resultForm.Append("系统内找到" + response.length.ToString() + "个股东账号。");

                if (response.length > 0)
                {
                    // 显示所有股东账户的信息，包括卡号、市场、状态等
                    Dict.STKBD stkbdList = new Dict.STKBD();
                    Dict.TRDACCT_EXCLS trdAcctExclsList = new Dict.TRDACCT_EXCLS();
                    Dict.TRDACCT_STATUS trdAcctStatusList = new Dict.TRDACCT_STATUS();
                    foreach (DataRow ds in response.Rows)
                    {
                        string stkbd = stkbdList.getNameByValue(ds["STKBD"].ToString());
                        string trdAcctExcls = trdAcctExclsList.getNameByValue(ds["TRDACCT_EXCLS"].ToString());
                        string status = trdAcctStatusList.getNameByValue(ds["TRDACCT_STATUS"].ToString());

                        resultForm.Append(
                            "交易版块：" + stkbd +
                            "，交易账户：" + ds["TRDACCT"].ToString() +
                            "，账户类别：" + trdAcctExcls +
                            "，账户状态：" + status
                        );
                        if (ds["STKBD"].ToString() == Dict.STKBD.上海A股)
                        {
                            tbxSHAcct.Text = ds["TRDACCT"].ToString();
                        }
                        if (ds["STKBD"].ToString() == Dict.STKBD.深圳A股)
                        {
                            tbxSZAcct.Text = ds["TRDACCT"].ToString();
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }

            btnQueryByUserCode.Enabled = true;
        }

        private async void btnBindSHAcct_Click(object sender, EventArgs e)
        {
            btnBindSHAcct.Enabled = false;

            try
            {
                saveUserInfo();
                user.cuacct_code = tbxCuacct.Text.Trim();
                user.ymt_code = tbxYMTCode.Text.Trim();
                user.shacct = tbxSHAcct.Text.Trim();

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

        }
    }
}
