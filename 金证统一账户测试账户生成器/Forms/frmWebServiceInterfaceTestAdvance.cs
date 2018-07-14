using NLog;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Yushen.Util;
using Yushen.WebService.KessClient;
using 金证统一账户测试账户生成器.Properties;

namespace 金证统一账户测试账户生成器
{
    public partial class frmWebServiceInterfaceTestAdvance : Form
    {
        frmFramework frmFramework;
        
        XmlDocument xmlDoc = new XmlDocument();
        DataTable dt = new DataTable();
        Request request;
        Response response;
        
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

        public frmWebServiceInterfaceTestAdvance(frmFramework form)
        {
            frmFramework = form;
            InitializeComponent();
        }

        /// <summary>
        /// 清空一个DataGridView的数据
        /// </summary>
        /// <param name="dgv"></param>
        private void dgvClear(ref DataGridView dgv)
        {
            if (dgv.DataSource != null)
            {
                DataTable dt = (DataTable)dgv.DataSource;
                dt.Rows.Clear();
                dgv.DataSource = dt;
            }
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                // 重置结果窗体
                tbxResponse.Text = "正在执行";
                lbResult.Text = "状态：正在执行";
                dgvResponse.DataSource = new DataTable();

                string innerXml = "";
                foreach (DataRow dr in dt.Rows)
                {
                    innerXml = innerXml + "<" + dr["字段名"] + ">" + dr["字段值"] + "</" + dr["字段名"]  + ">";
                }
                request = new Request(Settings.Default.操作员代码, cbxMethonList.Text);
                request.data.InnerXml = innerXml;
                response = await kess.invoke(request);
                tbxResponse.Text = response.xml;

                lbResult.Text = "状态：Flag:" + response.flag + "  Prompt:" + response.prompt + "  Length:" + response.length;

                if (response.length>0)
                {
                    if (cbxAutoTranslate.Checked)
                    {
                        dgvResponse.DataSource = response.TranslatedRecord;
                    }
                    else
                    {
                        dgvResponse.DataSource = response.Record;
                    }
                    dgvResponse.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                tbxResponse.Text = "执行失败";
                lbResult.Text = "状态：执行失败";
                resultForm.Append(ex.Message);
            }
        }

        private void btnLoadRequestXml_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Environment.CurrentDirectory, Request.xmlPath);
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message + "：" + path);
            }
        }
        
        private void btnRefreshMethonList_Click(object sender, EventArgs e)
        {
            refreshMethonList();
        }

        /// <summary>
        /// 初始化测试工具下拉列表
        /// </summary>
        private void refreshMethonList()
        {
            cbxMethonList.Items.Clear();

            DirectoryInfo folder = new DirectoryInfo(Request.xmlPath);
            foreach (FileInfo file in folder.GetFiles("*.xml"))
            {
                cbxMethonList.Items.Add(file.Name.Replace(file.Extension, ""));
            }
        }

        private void cbxMethonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Request request = new Request(Settings.Default.操作员代码, cbxMethonList.Text, false);
                lbInterfaceTitle.Text = "接口名称：" + request.title;
                
                XmlNodeList dataList = request.data.ChildNodes;
                XmlNode node;

                dt = new DataTable();
                dt.Columns.Add(new DataColumn("字段名"));
                dt.Columns.Add(new DataColumn("字段值"));
                dt.Columns.Add(new DataColumn("备注"));
                
                for (int i = 0; i < dataList.Count; i++)
                {
                    node = dataList[i];
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        DataRow dr = dt.NewRow();
                        dr["字段名"] = node.Name;
                        dr["字段值"] = node.InnerText;

                        if (i > 0 && dataList[i-1].NodeType == XmlNodeType.Comment)
                        {
                            dr["备注"] = dataList[i - 1].InnerText.Replace("//", "");
                        }
                        dt.Rows.Add(dr);
                    }
                }
                
                dgvParams.DataSource = dt;
                
            }
            catch (NotImplementedException)
            {
                resultForm.Append("不支持的WebService方法：" + cbxMethonList.Text);
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        private void frmWebServiceInterfaceTest_Load(object sender, EventArgs e)
        {
            // 初始化测试工具下拉列表
            refreshMethonList();

            cbxMethonList.Focus();
        }
        
        /// <summary>
        /// 查询数据字典
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async public void queryDictionary(string dictName)
        {
            lbQueryDictStatus.Text = "正在查询：" + dictName;
            lbQueryDictStatus.Visible = true;

            dictName = dictName.ToUpper().Trim();

            try
            {
                Response response = await kess.getDictData(dictName);
                dgvDict.DataSource = response.DataSet.Tables["row"];
                dgvDict.ClearSelection();
                if (dgvDict.ColumnCount >= 2)
                {
                    dgvDict.AutoResizeColumn(2);
                }
            }
            catch (Exception)
            {
                if (dgvDict.DataSource != null)
                {
                    DataTable dt = (DataTable)dgvDict.DataSource;
                    dt.Rows.Clear();
                    dgvDict.DataSource = dt;
                }
            }

            lbQueryDictStatus.Visible = false;
        }
        
        private void dgvParams_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvParams.CurrentRow!= null)
                {
                    queryDictionary(dgvParams.CurrentRow.Cells["ColumnName"].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void dgvDict_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvParams.CurrentRow.Cells["ColumnValue"].Value = dgvDict.CurrentRow.Cells["DD_ITEM"].Value;
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        private void cbxAutoTranslate_CheckedChanged(object sender, EventArgs e)
        {
            if (response!=null)
            {
                if (cbxAutoTranslate.Checked)
                {
                    dgvResponse.DataSource = response.TranslatedRecord;
                }
                else
                {
                    dgvResponse.DataSource = response.Record;
                }
                dgvResponse.ClearSelection();
            }
        }
    }
}
