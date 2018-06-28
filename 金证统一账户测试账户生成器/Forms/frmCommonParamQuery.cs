using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yushen.WebService.KessClient;

namespace 金证统一账户测试账户生成器
{
    public partial class frmCommonParamQuery : Form
    {
        frmFramework frmFramework;

        frmResultForm resultForm
        {
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

        public frmCommonParamQuery(frmFramework form)
        {
            frmFramework = form;
            InitializeComponent();
        }

        private async void btnQueryCommonParams_Click(object sender, EventArgs e)
        {
            try
            {
                tbxCommonParamValue.Text = await kess.getSingleCommonParamValue(tbxCommonParamKey.Text.Trim().ToUpper());
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        private void frmCommonParamQuery_Load(object sender, EventArgs e)
        {
            tbxCommonParamKey.Focus();
        }
    }
}
