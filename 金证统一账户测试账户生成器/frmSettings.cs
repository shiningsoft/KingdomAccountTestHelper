using System;
using System.Windows.Forms;
using Settings = 金证统一账户测试账户生成器.Properties.Settings;

namespace 金证统一账户测试账户生成器
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            tbxWebserviceUrl.Text = Settings.Default.webservice;
            tbxBranchNo.Text = Settings.Default.开户营业部;
            tbxOperatorId.Text = Settings.Default.操作员代码;
            tbxPassword.Text = Settings.Default.操作员密码;
            tbxChannel.Text = Settings.Default.操作渠道;
            tbxZdTimeout.Text = Settings.Default.中登超时时间.ToString();
        }

        private void accept_Click(object sender, EventArgs e)
        {
            Settings.Default.webservice = tbxWebserviceUrl.Text.Trim();
            Settings.Default.开户营业部 = tbxBranchNo.Text.Trim();
            Settings.Default.操作员代码 = tbxOperatorId.Text.Trim();
            Settings.Default.操作员密码 = tbxPassword.Text.Trim();
            Settings.Default.操作渠道 = tbxChannel.Text.Trim();
            Settings.Default.中登超时时间 = int.Parse(tbxZdTimeout.Text.Trim());
            Settings.Default.Save();
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
