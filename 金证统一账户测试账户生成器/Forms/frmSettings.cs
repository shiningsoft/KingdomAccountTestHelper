using System;
using System.Windows.Forms;
using Settings = 金证统一账户测试账户生成器.Properties.Settings;
using Yushen.WebService.KessClient;
using System.Data;

namespace 金证统一账户测试账户生成器
{
    public partial class frmSettings : Form
    {
        frmFramework frmFramework;
        DataTable webservices = new DataTable();

        public frmSettings(frmFramework form)
        {
            frmFramework = form;
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            tbxBranchNo.Text = Settings.Default.开户营业部;
            tbxOperatorId.Text = Settings.Default.操作员代码;
            tbxPassword.Text = Settings.Default.操作员密码;
            tbxChannel.Text = Settings.Default.操作渠道;
            tbxZdTimeout.Text = Settings.Default.中登超时时间.ToString();

            // 加载风险测评信息
            tbxSurveySN.Text = Settings.Default.SURVEY_SN;
            tbxCols.Text = Settings.Default.Cols;
            tbxCellsA.Text = Settings.Default.保守型;
            tbxCellsB.Text = Settings.Default.谨慎型;
            tbxCellsC.Text = Settings.Default.稳健型;
            tbxCellsD.Text = Settings.Default.积极型;
            tbxCellsE.Text = Settings.Default.激进型;
            tbxMaxConnections.Text = Settings.Default.最大并发数.ToString();

            webservices.Columns.Add(new DataColumn("webservice"));
            foreach (var url in Settings.Default.webservices)
            {
                DataRow dr = webservices.NewRow();
                dr["webservice"] = url;
                webservices.Rows.Add(dr);
            }
            dgvWebServices.DataSource = webservices;
        }

        private void accept_Click(object sender, EventArgs e)
        {
            Settings.Default.开户营业部 = tbxBranchNo.Text.Trim();
            Settings.Default.操作员代码 = tbxOperatorId.Text.Trim();
            Settings.Default.操作员密码 = tbxPassword.Text.Trim();
            Settings.Default.操作渠道 = tbxChannel.Text.Trim();
            Settings.Default.中登超时时间 = int.Parse(tbxZdTimeout.Text.Trim());
            if (rbU.Checked)
            {
                Settings.Default.统一账户版本 = Kess.Edtion.U.ToString();
            }
            else
            {
                Settings.Default.统一账户版本 = Kess.Edtion.Win.ToString();
            }
            Settings.Default.最大并发数 = int.Parse(tbxMaxConnections.Text.Trim());

            Settings.Default.SURVEY_SN = tbxSurveySN.Text;
            Settings.Default.Cols = tbxCols.Text;
            Settings.Default.保守型 = tbxCellsA.Text;
            Settings.Default.谨慎型 = tbxCellsB.Text;
            Settings.Default.稳健型 = tbxCellsC.Text;
            Settings.Default.积极型 = tbxCellsD.Text;
            Settings.Default.激进型 = tbxCellsE.Text;

            Settings.Default.webservices.Clear();
            foreach (DataGridViewRow dr in dgvWebServices.Rows)
            {
                if (dr.IsNewRow)
                {
                    continue;
                }
                if (!Uri.IsWellFormedUriString(dr.Cells["webservice"].Value.ToString(),UriKind.RelativeOrAbsolute))
                {
                    MessageBox.Show("WebService地址格式不合法，请检查！");
                    return;
                }
                Settings.Default.webservices.Add(dr.Cells["webservice"].Value.ToString());
            }

            if (Settings.Default.webservices.Count>0)
            {
                Settings.Default.webservice = Settings.Default.webservices[0];
            }
            else
            {
                Settings.Default.webservice = "";
            }
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
        }

        private void tbxMaxConnections_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void tbxZdTimeout_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }
    }
}
