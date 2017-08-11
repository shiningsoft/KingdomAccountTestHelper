using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            tbxOperatorId.Text = Settings.Default.操作员代码;
            tbxPassword.Text = Settings.Default.操作员密码;
            tbxChannel.Text = Settings.Default.操作渠道;
        }

        private void tbxWebserviceUrl_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.webservice = tbxWebserviceUrl.Text.Trim();
        }

        private void tbxOperatorId_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.操作员代码 = tbxOperatorId.Text.Trim();
        }

        private void tbxPassword_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.操作员密码 = tbxPassword.Text.Trim();
        }

        private void tbxChannel_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.操作渠道 = tbxChannel.Text.Trim();
        }

        private void accept_Click(object sender, EventArgs e)
        {
            Settings.Default.webservice = tbxWebserviceUrl.Text.Trim();
            Settings.Default.操作员代码 = tbxOperatorId.Text.Trim();
            Settings.Default.操作员密码 = tbxPassword.Text.Trim();
            Settings.Default.操作渠道 = tbxChannel.Text.Trim();
            Settings.Default.Save();
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
