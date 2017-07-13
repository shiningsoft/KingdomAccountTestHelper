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
                }
                infoBox.Text += kess.getSingleCommonParamValue(userCode.Text) + "\r\n";
                
                infoBox.Select(infoBox.TextLength, 0);
                infoBox.ScrollToCaret();
            }
            catch (Exception ex)
            {
                infoBox.Text += ex.Message + "\r\n";
            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
