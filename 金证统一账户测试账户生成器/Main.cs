using System;
using System.Windows.Forms;
using Yushen.Util;
using Yushen.WebService.KessClient;
using NLog;
using System.Threading.Tasks;
using Dict = Yushen.WebService.KessClient.Dict;
using System.Data;
using 金证统一账户测试账户生成器.Properties;
using System.Drawing;
using System.IO;

namespace 金证统一账户测试账户生成器
{
    public partial class Main : Form
    {
        Kess kess;
        static Logger logger = LogManager.GetCurrentClassLogger();
        ResultForm resultForm = new ResultForm();
        User user;
        AboutBox aboutBox;
        frmSettings frmSettings;

        public Main()
        {
            InitializeComponent();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 关于ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (aboutBox == null || aboutBox.IsDisposed)
            {
                aboutBox = new AboutBox();
                aboutBox.Show();
            }
            else
            {
                aboutBox.Activate();
            }
        }

        private void 新开随机账户ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 查询数据字典
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dictName.Text = dictName.Text.ToUpper().Trim();
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

                try
                {
                    Response response = await kess.getDictData(dictName.Text);
                    dataGridView1.DataSource = response.DataSet.Tables["row"];
                    if (dataGridView1.ColumnCount >= 2)
                    {
                        dataGridView1.AutoResizeColumn(2);
                    }
                }
                catch (Exception ex)
                {
                    resultForm.Show();
                    resultForm.Append(ex.Message);
                    if (dataGridView1.DataSource != null)
                    {
                        DataTable dt = (DataTable)dataGridView1.DataSource;
                        dt.Rows.Clear();
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // 初始化风险评级选项
            // RiskTest riskTest = new RiskTest();
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

            Dict.BankCode bankCodeList = new Dict.BankCode();
            bank_code.DisplayMember = "name";
            bank_code.ValueMember = "value";
            bank_code.DataSource = bankCodeList.DataTable;

            Dict.OPEN_TYPE openTypeList = new Dict.OPEN_TYPE();
            cbxOpenType.DisplayMember = "name";
            cbxOpenType.ValueMember = "value";
            cbxOpenType.DataSource = openTypeList.DataTable;

            dtpCybSignDate.Value = DateTime.Now;

            try
            {
                // 初始化WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }
            }
            catch (Exception ex)
            {
                resultForm.Append("初始化失败：" + ex.Message);
            }

            // 生成随机用户信息
            reCreateUserInfo();

            // 更新状态栏信息
            Uri uri = new Uri(Settings.Default.webservice);
            toolStripStatusLabelCurrentServer.Text = "当前环境：" + uri.Host + ":" + uri.Port;
            currentUser.Text = "用户：" + Settings.Default.操作员代码;

            // 初始化测试工具下拉列表
            DirectoryInfo folder = new DirectoryInfo(Request.xmlPath);
            foreach (FileInfo file in folder.GetFiles("*.xml"))
            {
                cbxMethonList.Items.Add(file.Name.Replace(file.Extension,""));
            }

        }

        /// <summary>
        /// 随机生成用户信息
        /// </summary>
        private void reCreateUserInfo()
        {
            user_name.Text = Generator.CreateChineseName();
            if (cbxShortIdNo.Checked)
            {
                id_code.Text = Generator.CreateIdNO(15);
            }
            else
            {
                id_code.Text = Generator.CreateIdNO(18);
            }
            IDCardNumber idcard = new IDCardNumber(id_code.Text);

            sex.SelectedValue = idcard.Sex.ToString();
            risk_level.SelectedValue = Dict.RiskTestLevel.积极型;
            occu_type.SelectedValue = Dict.OCCU_EXTYPE.行政企事业单位工人;
            citizenship.SelectedValue = Dict.CITIZENSHIP.中国;
            education.SelectedIndex = Generator.CreateRandomInteger(0, education.Items.Count);
            // bank_code.SelectedIndex = Generator.CreateRandomInteger(0, bank_code.Items.Count);   // 随机选中三方银行
            bank_code.SelectedValue = Dict.BankCode.工商银行;
            cbxOpenType.SelectedValue = Dict.OPEN_TYPE.T加2;

            saveUserInfo();
        }

        /// <summary>
        /// 保存当前用户信息到User对象
        /// </summary>
        private void saveUserInfo()
        {
            user = new User();
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
            user.education = education.SelectedValue.ToString();
            user.bank_code = bank_code.SelectedValue.ToString();
            user.zip_code = zip_code.Text.Trim();
            user.sex = sex.SelectedValue.ToString();
            user.int_org = Settings.Default.开户营业部;
            user.cust_cls = Dict.CUST_CLS.标准客户;
            user.cust_type = Dict.CUST_TYPE.普通;
            user.channels = Dict.CHANNEL.柜台系统 + Dict.CHANNEL.电话委托 + Dict.CHANNEL.网上委托 + Dict.CHANNEL.手机炒股;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reCreateUserInfo();
        }

        private async void btnBankSign_Click(object sender, EventArgs e)
        {
            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

                await signBank(bank_code.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                resultForm.Append("预指定失败：" + ex.Message);
            }
        }

        private async void btnSubmitRiskTest_Click(object sender, EventArgs e)
        {
            resultForm.Show();

            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

                await syncSurveyAns2Kbss(risk_level.SelectedValue.ToString());

            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        private async void btnSetPassword_Click(object sender, EventArgs e)
        {
            resultForm.Show();

            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

                await mdfUserPassword();

            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        /// <summary>
        /// 执行开立资金账户操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnOpenCuacct_Click(object sender, EventArgs e)
        {
            resultForm.Show();

            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

                await openCuacctCode();
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        private async void btnOpenYMT_Click(object sender, EventArgs e)
        {
            resultForm.Show();

            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

                await openYMTCode();

            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        private async void btnOpenStockAccount_Click(object sender, EventArgs e)
        {
            resultForm.Show();

            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

                await openSHACode();

            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        private async void btnQueryStockAccount_Click(object sender, EventArgs e)
        {
            resultForm.Show();

            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

                saveUserInfo();
                Response response = await kess.queryStkAcct(user);
                resultForm.Append("该客户有" + response.length.ToString() + "个股东卡号");
                foreach (DataRow ds in response.DataSet.Tables["row"].Rows)
                {
                    // resultForm.Append("该客户有" + response.length.ToString() + "个股东卡号");
                }
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

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
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

                await registerSHACode();

            }
            catch (Exception ex)
            {
                resultForm.Append("股东账号加挂失败：" + ex.Message);
            }
        }

        private async void btnOpenCYB_Click(object sender, EventArgs e)
        {
            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

                await kess.openCyb2ZD(user, cbxOpenType.SelectedValue.ToString(), dtpCybSignDate.Text, timeout: Settings.Default.中登超时时间);
                resultForm.Append("中登创业板开通成功");
                await kess.openCyb2KBSS(user, Dict.OPEN_TYPE.T加2, dtpCybSignDate.Text, dtpCybSignDate.Text);
                resultForm.Append("系统内创业板协议签署成功");
            }
            catch (Exception ex)
            {
                resultForm.Append("创业板开通失败：" + ex.Message);
            }
        }

        private async void btnValidateId_Click(object sender, EventArgs e)
        {
            resultForm.Show();

            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

                Response response = await kess.validateIdCode(user);

                // 返回结果
                if (response.getValue("ID_CODE_CHKRLT") == "一致")
                {
                    resultForm.Append("公安校验通过");
                }
                else
                {
                    resultForm.Append("公安校验未通过：" + response.getValue("ID_CODE_CHKRLT"));
                }
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        private async void btnOpenSZAStkAcct_Click(object sender, EventArgs e)
        {

            resultForm.Show();

            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

                await openSZACode();

            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        private async void btnRegisterSZAStkAcct_Click(object sender, EventArgs e)
        {
            resultForm.Show();
            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

                await registerSZACode();

            }
            catch (Exception ex)
            {
                resultForm.Append("股东账号加挂失败：" + ex.Message);
            }
        }

        private void btnOpenAccountByOneClick_Click(object sender, EventArgs e)
        {
            openAllAccount();
        }

        /// <summary>
        /// 一次性开立所有账户
        /// </summary>
        async private void openAllAccount()
        {
            resultForm.Show();

            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

                // 使用当前用户信息
                saveUserInfo();

                await openCustCode();

                await openCuacctCode();

                await mdfUserPassword();

                await syncSurveyAns2Kbss(risk_level.SelectedValue.ToString());

                await signBank(bank_code.SelectedValue.ToString());

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
        private async Task signBank(string bank_code)
        {
            bool result = await kess.cubsbScOpenAcct("1", user.cuacct_code, bank_code);
            if (result)
            {
                resultForm.Append("三方存管预指定成功");
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

        private async void btnQueryCYB_Click(object sender, EventArgs e)
        {
            resultForm.Show();
            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }

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
        }

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

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmSettings==null||frmSettings.IsDisposed)
            {
                frmSettings = new frmSettings();
                frmSettings.Show();
            }
            else
            {
                frmSettings.Activate();
            }
        }

        private void btnCreateIDCardImg_Click(object sender, EventArgs e)
        {
            if (id_code.Text.Length != 18)
            {
                resultForm.Append("只支持生成18位身份证的正面照。");
                return;
            }
            saveUserInfo();
            createIdCardImgFaceSide(user.user_name, user.sex, user.nationality, user.birthday, user.id_addr, user.id_code);
        }

        private void btnCreateIDCardImgBackSide_Click(object sender, EventArgs e)
        {
            saveUserInfo();
            createIdCardImgBackSide(user.id_iss_agcy, user.id_beg_date, user.id_exp_date);
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

        private void cbxLongTerm_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxLongTerm.Checked)
            {
                id_exp_date.Enabled = false;
                id_exp_date.Value = DateTime.Parse("3000-12-31");
            }
            else
            {
                id_exp_date.Enabled = true;
            }
        }

        async private void btnOpenUserCode_Click(object sender, EventArgs e)
        {
            resultForm.Clear();
            resultForm.Show();

            // 建立WebService连接
            if (kess == null)
            {
                kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
            }

            saveUserInfo();

            await openCustCode();
        }

        private async void btnBindSHAcct_Click(object sender, EventArgs e)
        {
            try
            {
                await bindSHAcct();
            }
            catch (Exception ex)
            {
                resultForm.Append("上海证券账户" + user.shacct + "指定交易失败：" + ex.Message);
            }
        }

        private void cbxShortIdNo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxShortIdNo.Checked)
            {
                if (id_code.Text.Length == 18)
                {
                    id_code.Text = IDCardNumber.per18To15(id_code.Text);
                }
            }
            else
            {
                if (id_code.Text.Length == 15)
                {
                    id_code.Text = IDCardNumber.per15To18(id_code.Text);
                }
            }
        }

        private void btnLoadRequestXml_Click(object sender, EventArgs e)
        {
            try
            {
                Request request = new Request(Settings.Default.操作员代码, cbxMethonList.Text, false);
                tbxRequest.Text = Formatter.formatXml(request.xml);
            }
            catch (NotImplementedException)
            {
                resultForm.Append("不支持的WebService方法：" + cbxMethonList.Text);
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }
                Request request = new Request(Settings.Default.操作员代码, cbxMethonList.Text, tbxRequest.Text);
                Response response = await kess.invoke(request);
                tbxResponse.Text = response.xml;
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        private async void btnQueryCommonParams_Click(object sender, EventArgs e)
        {
            try
            {
                tbxCommonParamValue.Text = await kess.getSingleCommonParamValue(tbxCommonParamKey.Text.Trim());
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }
    }
}
