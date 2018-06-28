using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yushen.WebService.KessClient;
using 金证统一账户测试账户生成器.Properties;

namespace 金证统一账户测试账户生成器
{
    public partial class frmFramework : Form
    {
        public Kess kess;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public frmResultForm resultForm = new frmResultForm();
        frmAboutBox aboutBox;
        frmSettings frmSettings;
        List<Form> forms = new List<Form> { };
        Dictionary<string, Form> dicForms = new Dictionary<string, Form>();
        Timer timerRefreshQueue;

        frmNewAccount frmNewAccount;

        public frmFramework()
        {
            InitializeComponent();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aboutBox == null || aboutBox.IsDisposed)
            {
                aboutBox = new frmAboutBox();
                aboutBox.Show();
            }
            else
            {
                aboutBox.Activate();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        private async void Main_Load(object sender, EventArgs e)
        {

            frmNewAccount = new frmNewAccount(this);
            dicForms.Add("新开账户", new frmNewAccount(this));
            dicForms.Add("数据字典查询", new frmDictQuery(this));
            dicForms.Add("公共参数查询", new frmCommonParamQuery(this));
            dicForms.Add("接口测试工具", new frmWebServiceInterfaceTest(this));

            int i = 0;
            foreach (var form in dicForms)
            {
                form.Value.TopLevel = false;  // 非顶级窗口  
                form.Value.FormBorderStyle = FormBorderStyle.None;  // 不显示标题栏  
                form.Value.Dock = DockStyle.Fill;  // 填充panel  

                ToolStripMenuItem item = new ToolStripMenuItem(form.Key);
                item.Click += Item_Click;
                tsmiFunction.DropDownItems.Insert(i, item);

                if (i==0)
                {
                    item.PerformClick();
                }

                i++;
            }

            try
            {
                tsslVersion.Text = "当前版本：" + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch (System.Deployment.Application.InvalidDeploymentException ex)
            {
                tsslVersion.Text = "未部署状态，无法获取当前版本";
                Console.WriteLine(ex.Message.ToString());
            }

            try
            {
                // 初始化WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice);
                }
            }
            catch (Exception ex)
            {
                resultForm.Append("初始化失败：" + ex.Message);
            }
            
            toolStripStatusLabelCurrentServer.Text = "当前环境：获取环境信息中，请稍候......";

            // 更新状态栏信息
            string serverName = "未能获取服务器名称";
            try
            {
                serverName = await kess.getSingleCommonParamValue("SERVER_NAME");
                if (serverName.IndexOf("测试")==-1)
                {
                    resultForm.Append("服务器公共参数（SERVER_NAME）中未检测到目标字符“测试”，请确认是否在测试环境中运行！");
                }
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }

            Uri uri = new Uri(Settings.Default.webservice);
            toolStripStatusLabelCurrentServer.Text = "当前环境：" + uri.Host + ":" + uri.Port + "，" + serverName;
            currentUser.Text = "用户：" + Settings.Default.操作员代码;

            timerRefreshQueue = new Timer();
            timerRefreshQueue.Interval = 100;
            timerRefreshQueue.Tick += TimerRefreshQueue_Tick;
            timerRefreshQueue.Start();
        }

        private void Item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (dicForms[item.Text] == null || dicForms[item.Text].IsDisposed)
            {
                MessageBox.Show(item.Text + "窗体未创建或已关闭", "错误提示");
                return;
            }
            panel.Controls.Clear();  // 清空原有的控件  
            panel.Controls.Add(dicForms[item.Text]);  // 添加新窗体  
            dicForms[item.Text].Show();
        }

        /// <summary>
        /// 刷新当前请求队列长度
        /// </summary>
        /// <returns></returns>
        private async void TimerRefreshQueue_Tick(object sender, EventArgs e)
        {
            await Task.Run(()=>
            {
                requestQueueCount.Text = "请求队列长度：" + kess.requestQueueCount.ToString() + "，当前并发：" + kess.activeConnectionsNum.ToString();
            });
        }
        
        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmSettings==null||frmSettings.IsDisposed)
            {
                frmSettings = new frmSettings();
                frmSettings.Show();
            }
            else
            {
                frmSettings.Activate();
            }
        }
        
        
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnModifyCustomDict_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.CurrentDirectory + @"\CustomDict\";
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        private void 修改数据字典ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.CurrentDirectory + @"\CustomDict\";
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }
    }
}
