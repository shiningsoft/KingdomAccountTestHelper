using System;
using System.Windows.Forms;
using Yushen.Util;
using Yushen.WebService.KessClient;
using NLog;
using System.Threading.Tasks;
using System.Collections.Generic;

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
                        user = new User();
                        user.user_name = user_name.Text;
                        user.user_fname = user_name.Text;
                        user.id_code = id_code.Text;
                        user.id_addr = id_addr.Text;
                        user.id_iss_agcy = id_iss_agcy.Text;
                        user.id_beg_date = id_beg_date.Text;
                        user.id_exp_date = id_exp_date.Text;
                        user.citizenship = citizenship.Text;
                        user.nationality = nationality.Text;
                        user.password = password.Text;
                        user.mobile_tel = mobile_tel.Text;
                        user.occu_type = occu_type.Text;
                        user.education = education.Text;
                        user.bank_code = bank_code.Text;

                        user.user_code = kess.createCustomerCode(user);

                        resultForm.Append("客户号开立成功："+user.user_code);
                        tbxUserCode.Text = user.user_code;
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
            this.Close();
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
            resultForm.Show();
            try
            {
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
                            resultForm.Append(kess.getDictData(dictName.Text));
                        }
                        catch (Exception ex)
                        {
                            resultForm.Append(ex.Message);
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
            RiskTest riskTest = new RiskTest();
            risk_level.DisplayMember = "name";
            risk_level.ValueMember = "value";
            risk_level.DataSource = riskTest.riskLevelList;
            
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
            id_code.Text = Generator.CreateIdNO();

            risk_level.SelectedValue = "E"; // 激进型
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
                    // 建立WebService连接
                    if (kess == null)
                    {
                        kess = new Kess(Properties.Settings.Default.operatorId, Properties.Settings.Default.operatorPassword, Properties.Settings.Default.channel, Properties.Settings.Default.webservice);
                    }

                    user.cuacct_code = kess.openCuacctCode(user);

                    resultForm.Append("资金账号开立成功：" + user.cuacct_code);
                    tbxCuacct.Text = user.cuacct_code;
                }));
            });
        }
    }
}
