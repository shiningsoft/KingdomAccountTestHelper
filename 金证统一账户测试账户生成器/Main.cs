using System;
using System.Windows.Forms;
using Yushen.Util;
using Yushen.WebService.KessClient;
using NLog;
using System.Threading.Tasks;
using Dict = Yushen.WebService.KessClient.Dict;
using System.Data;

namespace 金证统一账户测试账户生成器
{
    public partial class Main : Form
    {
        private Kess kess;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        ResultForm resultForm = new ResultForm();
        private User user;

        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            resultForm.Clear();
            resultForm.Show();

            // 异步方式调用WebService查询
            Task task = Task.Run(() =>
            {
                this.Invoke((MethodInvoker)(() => {
                    try
                    {
                        // 建立WebService连接
                        if (kess == null)
                        {
                            kess = new Kess(Properties.Settings.Default.operatorId, Properties.Settings.Default.operatorPassword, Properties.Settings.Default.channel, Properties.Settings.Default.webservice);
                        }
                        
                        user.cust_code = kess.createCustomerCode(user);
                        user.user_code = user.cust_code;

                        resultForm.Append("客户号开立成功："+user.cust_code);
                        tbxCustCode.Text = user.cust_code;
                    }
                    catch (Exception ex)
                    {
                        resultForm.Append(ex.Message);
                    }
                }));
            });
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
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
        }

        private void 新开随机账户ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dictName.Text = dictName.Text.ToUpper().Trim();
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Properties.Settings.Default.operatorId, Properties.Settings.Default.operatorPassword, Properties.Settings.Default.channel, Properties.Settings.Default.webservice);
                }

                // 异步方式调用WebService查询
                Task task = Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)(() => {
                        try
                        {
                            Response response = kess.getDictData(dictName.Text);
                            dataGridView1.DataSource = response.DataSet.Tables["row"];
                            if (dataGridView1.ColumnCount>=2)
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
                    }));
                });
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

            // 初始化WebService连接
            if (kess == null)
            {
                kess = new Kess(Properties.Settings.Default.operatorId, Properties.Settings.Default.operatorPassword, Properties.Settings.Default.channel, Properties.Settings.Default.webservice);
            }

            reCreateUserInfo();
        }

        /// <summary>
        /// 重新生成随机用户信息
        /// </summary>
        private void reCreateUserInfo()
        {
            user_name.Text = Generator.CreateChineseName();
            IDCardNumber idcard = IDCardNumber.Random();
            id_code.Text = idcard.CardNumber;

            sex.SelectedValue = idcard.Sex.ToString();
            risk_level.SelectedValue = Dict.RiskTestLevel.积极型;
            occu_type.SelectedValue = Dict.OCCU_EXTYPE.行政企事业单位工人;
            citizenship.SelectedValue = Dict.CITIZENSHIP.中国;
            education.SelectedIndex = Generator.CreateRandomNumber(0, education.Items.Count);
            bank_code.SelectedIndex = Generator.CreateRandomNumber(0, bank_code.Items.Count);

            saveUserInfo();
        }

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
            user.int_org = "19";
            user.cust_cls = Dict.CUST_CLS.标准客户;
            user.cust_type = Dict.CUST_TYPE.普通;
            user.channels = Dict.CHANNEL.柜台系统 + Dict.CHANNEL.电话委托 + Dict.CHANNEL.网上委托 + Dict.CHANNEL.手机炒股;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reCreateUserInfo();
        }

        private void btnBankSign_Click(object sender, EventArgs e)
        {

        }

        private void btnSubmitRiskTest_Click(object sender, EventArgs e)
        {
            resultForm.Show();

            // 异步方式调用WebService查询
            Task task = Task.Run(() =>
            {
                this.Invoke((MethodInvoker)(() => {
                    try
                    {
                        // 建立WebService连接
                        if (kess == null)
                        {
                            kess = new Kess(Properties.Settings.Default.operatorId, Properties.Settings.Default.operatorPassword, Properties.Settings.Default.channel, Properties.Settings.Default.webservice);
                        }

                        bool result = kess.syncSurveyAns2Kbss(user, risk_level.SelectedValue.ToString());
                        if (result)
                        {
                            resultForm.Append("提交风险测评成功");
                        }
                    }
                    catch (Exception ex)
                    {
                        resultForm.Append(ex.Message);
                    }
                }));
            });
        }

        private void btnSetPassword_Click(object sender, EventArgs e)
        {
            resultForm.Show();

            // 异步方式调用WebService查询
            Task task = Task.Run(() =>
            {
                this.Invoke((MethodInvoker)(() => {
                    try
                    {
                        // 建立WebService连接
                        if (kess == null)
                        {
                            kess = new Kess(Properties.Settings.Default.operatorId, Properties.Settings.Default.operatorPassword, Properties.Settings.Default.channel, Properties.Settings.Default.webservice);
                        }

                        bool result = kess.mdfUserPassword(user,"0", "0");
                        if (result)
                        {
                            resultForm.Append("添加交易密码成功，新密码：" + user.password);
                        }

                        result = kess.mdfUserPassword(user, "1", "0");
                        if (result)
                        {
                            resultForm.Append("添加资金密码成功，新密码：" + user.password);
                        }
                    }
                    catch (Exception ex)
                    {
                        resultForm.Append(ex.Message);
                    }
                }));
            });
        }

        /// <summary>
        /// 执行开立资金账户操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenCuacct_Click(object sender, EventArgs e)
        {
            resultForm.Show();

            // 异步方式调用WebService查询
            Task task = Task.Run(() =>
            {
                this.Invoke((MethodInvoker)(() => {
                    try
                    {
                        // 建立WebService连接
                        if (kess == null)
                        {
                            kess = new Kess(Properties.Settings.Default.operatorId, Properties.Settings.Default.operatorPassword, Properties.Settings.Default.channel, Properties.Settings.Default.webservice);
                        }

                        user.cuacct_code = kess.openCuacctCode(user);

                        resultForm.Append("资金账号开立成功：" + user.cuacct_code);
                        tbxCuacct.Text = user.cuacct_code;
                    }
                    catch (Exception ex)
                    {
                        resultForm.Append(ex.Message);
                    }
                }));
            });
        }

        private void btnOpenYMT_Click(object sender, EventArgs e)
        {
            resultForm.Show();

            // 异步方式调用WebService查询
            Task task = Task.Run(() =>
            {
                this.Invoke((MethodInvoker)(() => {
                    try
                    {
                        // 建立WebService连接
                        if (kess == null)
                        {
                            kess = new Kess(Properties.Settings.Default.operatorId, Properties.Settings.Default.operatorPassword, Properties.Settings.Default.channel, Properties.Settings.Default.webservice);
                        }

                        Response response = kess.openYMTAcct(user.user_type, user.user_fname, user.id_type, user.id_code, user.int_org, user.cust_code, user.birthday, user.id_beg_date, user.id_exp_date, user.citizenship, user.id_addr, user.id_addr, user.zip_code, user.occu_type, user.nationality, user.education, user.tel, user.mobile_tel, user.sex);

                        if (response.length > 2)
                        {
                            throw new Exception("该客户有" + response.length.ToString() + "个一码通账号");
                        }
                        else if(response.length == 0)
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
                        resultForm.Append(ex.Message);
                    }
                }));
            });
        }

        private void btnOpenStockAccount_Click(object sender, EventArgs e)
        {
            resultForm.Show();

            // 异步方式调用WebService查询
            Task task = Task.Run(() =>
            {
                this.Invoke((MethodInvoker)(() => {
                    try
                    {
                        // 建立WebService连接
                        if (kess == null)
                        {
                            kess = new Kess(Properties.Settings.Default.operatorId, Properties.Settings.Default.operatorPassword, Properties.Settings.Default.channel, Properties.Settings.Default.webservice);
                        }

                        Response response = kess.openStkAcct(user,Dict.ACCT_TYPE.沪市A股账户);
                        user.shacct = response.getValue("TRDACCT");
                        resultForm.Append("沪A股东账号开立成功：" + user.shacct);
                        tbxSHAcct.Text = user.shacct;

                        response = kess.openStkAcct(user, Dict.ACCT_TYPE.深市A股账户);
                        user.szacct = response.getValue("TRDACCT");
                        resultForm.Append("深A股东账号开立成功：" + user.szacct);
                        tbxSZAcct.Text = user.szacct;

                    }
                    catch (Exception ex)
                    {
                        resultForm.Append(ex.Message);
                    }
                }));
            });
        }

        private void btnQueryStockAccount_Click(object sender, EventArgs e)
        {
            resultForm.Show();
            
            try
            {
                // 建立WebService连接
                if (kess == null)
                {
                    kess = new Kess(Properties.Settings.Default.operatorId, Properties.Settings.Default.operatorPassword, Properties.Settings.Default.channel, Properties.Settings.Default.webservice);
                }

                Response response = kess.queryStkAcct(user);
                resultForm.Append("该客户有" + response.length.ToString() + "个股东卡号");
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 建立WebService连接
            if (kess == null)
            {
                kess = new Kess(Properties.Settings.Default.operatorId, Properties.Settings.Default.operatorPassword, Properties.Settings.Default.channel, Properties.Settings.Default.webservice);
            }

            if (kess.operatorLogin())
            {
                resultForm.Show();
                resultForm.Append("操作员登录成功");
            }
        }

        private void btnRegisterStockAccount_Click(object sender, EventArgs e)
        {
            resultForm.Show();

            // 异步方式调用WebService查询
            Task task = Task.Run(() =>
            {
                this.Invoke((MethodInvoker)(() => {
                    try
                    {
                        // 建立WebService连接
                        if (kess == null)
                        {
                            kess = new Kess(Properties.Settings.Default.operatorId, Properties.Settings.Default.operatorPassword, Properties.Settings.Default.channel, Properties.Settings.Default.webservice);
                        }
                        
                        if (kess.registerSHAStkTrdAcct(user))
                        {
                            resultForm.Append("沪A股东账号加挂成功");
                        }

                        if (kess.registerSZAStkTrdAcct(user))
                        {
                            resultForm.Append("深A股东账号加挂成功");
                        }

                    }
                    catch (Exception ex)
                    {
                        resultForm.Append("股东账号加挂失败：" + ex.Message);
                    }
                }));
            });
        }

        private void btnOpenAccountByOneClick_Click(object sender, EventArgs e)
        {
            
        }

        private void btnOpenCYB_Click(object sender, EventArgs e)
        {

        }

        private void btnValidateId_Click(object sender, EventArgs e)
        {
            resultForm.Show();

            // 异步方式调用WebService查询
            Task task = Task.Run(() =>
            {
                this.Invoke((MethodInvoker)(() => {
                    try
                    {
                        // 建立WebService连接
                        if (kess == null)
                        {
                            kess = new Kess(Properties.Settings.Default.operatorId, Properties.Settings.Default.operatorPassword, Properties.Settings.Default.channel, Properties.Settings.Default.webservice);
                        }

                        Response response = kess.validateIdCode(user);

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
                }));
            });
        }
    }
}
