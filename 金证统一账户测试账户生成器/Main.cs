using System;
using System.Windows.Forms;
using Yushen.WebService.KessClient;
using NLog;

namespace 金证统一账户测试账户生成器
{
    public partial class Main : Form
    {
        private Kess kess;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (kess == null)
                {
                    kess = new Kess(operatorId.Text, password.Text, "");
                    infoBox.Text += "用户登录成功\r\n";
                }
                infoBox.Text += kess.getUserInfoById(userCode.Text) + "\r\n";
                
                infoBox.Select(infoBox.TextLength, 0);
                infoBox.ScrollToCaret();
            }
            catch (Exception ex)
            {
                infoBox.Text += ex.Message + "\r\n";
            }
        }
    }
}
