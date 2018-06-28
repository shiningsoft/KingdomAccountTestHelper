using NLog;
using System;
using System.Data;
using System.Windows.Forms;
using Yushen.WebService.KessClient;

namespace 金证统一账户测试账户生成器
{
    public partial class frmDictQuery : Form
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

        public frmDictQuery(frmFramework form)
        {
            frmFramework = form;
            InitializeComponent();
        }

        /// <summary>
        /// 查询数据字典
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void btnQueryDict_Click(object sender, EventArgs e)
        {
            btnQueryDict.Enabled = false;

            try
            {
                dictName.Text = dictName.Text.ToUpper().Trim();

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

            btnQueryDict.Enabled = true;
        }

        private void frmDictQuery_Load(object sender, EventArgs e)
        {
            dictName.Focus();
        }
    }
}
