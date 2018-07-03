using NLog;
using System;
using System.IO;
using System.Windows.Forms;
using Yushen.Util;
using Yushen.WebService.KessClient;
using 金证统一账户测试账户生成器.Properties;

namespace 金证统一账户测试账户生成器
{
    public partial class frmWebServiceInterfaceTest : Form
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

        public frmWebServiceInterfaceTest(frmFramework form)
        {
            frmFramework = form;
            InitializeComponent();
        }
        
        private async void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                Request request = new Request(Settings.Default.操作员代码, cbxMethonList.Text, tbxRequest.Text);
                Response response = await kess.invoke(request);
                tbxResponse.Text = response.xml;
            }
            catch (Exception ex)
            {
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
                tbxRequest.Text = Formatter.formatXml(request.xml);
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
    }
}
