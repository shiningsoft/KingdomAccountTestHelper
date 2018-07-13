using System;
using System.Windows.Forms;
using Settings = 金证统一账户测试账户生成器.Properties.Settings;
using Yushen.WebService.KessClient;

namespace 金证统一账户测试账户生成器
{
    public partial class frmSettings : Form
    {
        frmFramework frmFramework;

        public frmSettings(frmFramework form)
        {
            frmFramework = form;
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
            tbxVersion.Text = Settings.Default.风险测评版本号;

            // 加载风险测评信息
            tbxSurveySN.Text = Settings.Default.SURVEY_SN;
            tbxCols.Text = Settings.Default.Cols;
            tbxCellsA.Text = Settings.Default.保守型;
            tbxCellsB.Text = Settings.Default.谨慎型;
            tbxCellsC.Text = Settings.Default.稳健型;
            tbxCellsD.Text = Settings.Default.积极型;
            tbxCellsE.Text = Settings.Default.激进型;
        }

        private void accept_Click(object sender, EventArgs e)
        {
            Settings.Default.webservice = tbxWebserviceUrl.Text.Trim();
            Settings.Default.开户营业部 = tbxBranchNo.Text.Trim();
            Settings.Default.操作员代码 = tbxOperatorId.Text.Trim();
            Settings.Default.操作员密码 = tbxPassword.Text.Trim();
            Settings.Default.操作渠道 = tbxChannel.Text.Trim();
            Settings.Default.中登超时时间 = int.Parse(tbxZdTimeout.Text.Trim());

            Settings.Default.SURVEY_SN = tbxSurveySN.Text;
            Settings.Default.Cols = tbxCols.Text;
            Settings.Default.保守型 = tbxCellsA.Text;
            Settings.Default.谨慎型 = tbxCellsB.Text;
            Settings.Default.稳健型 = tbxCellsC.Text;
            Settings.Default.积极型 = tbxCellsD.Text;
            Settings.Default.激进型 = tbxCellsE.Text;
            Settings.Default.风险测评版本号 = tbxVersion.Text;

            Settings.Default.Save();

            frmFramework.InitWebService();

            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnResetRiskSettings_Click(object sender, EventArgs e)
        {
            tbxSurveySN.Text = RiskTest.survey_sn;
            tbxCols.Text = RiskTest.cols;
            tbxCellsA.Text = RiskTest.cells_A;
            tbxCellsB.Text = RiskTest.cells_B;
            tbxCellsC.Text = RiskTest.cells_C;
            tbxCellsD.Text = RiskTest.cells_D;
            tbxCellsE.Text = RiskTest.cells_E;
            tbxVersion.Text = "3.0";
        }
    }
}
