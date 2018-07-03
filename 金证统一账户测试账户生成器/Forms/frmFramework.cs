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
        /// <summary>
        /// 金证接口调用工具
        /// </summary>
        public Kess kess;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public frmResultForm resultForm = new frmResultForm();
        frmAboutBox aboutBox;
        frmSettings frmSettings;
        Dictionary<string, Form> forms = new Dictionary<string, Form>();
        Timer timerRefreshQueue;

        public frmFramework()
        {
            InitializeComponent();
        }

        private async void Main_Load(object sender, EventArgs e)
        {
            forms.Add("新开账户", new frmNewAccount(this));
            forms.Add("存量账户处理", new frmExistAccount(this));
            forms.Add("数据字典查询", new frmDictQuery(this));
            forms.Add("公共参数查询", new frmCommonParamQuery(this));
            forms.Add("接口测试工具", new frmWebServiceInterfaceTest(this));

            int i = 0;
            foreach (var form in forms)
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
            
            tsslVersion.Text = "当前版本：" + Application.ProductVersion;

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
            if (forms[item.Text] == null || forms[item.Text].IsDisposed)
            {
                MessageBox.Show(item.Text + "窗体未创建或已关闭", "错误提示");
                return;
            }
            panel.Controls.Clear();  // 清空原有的控件  
            panel.Controls.Add(forms[item.Text]);  // 添加新窗体  
            forms[item.Text].Show();
            Text = "金证统一账户测试账户生成器 - " + item.Text;
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
        
        /// <summary>
        /// 显示设置窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 显示关于窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        /// <summary>
        /// 打开用户自定义数据字典的所在文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void tsmiFunction_MouseHover(object sender, EventArgs e)
        {
            if (sender is ToolStripDropDownItem)
            {
                ToolStripDropDownItem item = sender as ToolStripDropDownItem;
                if (item.HasDropDownItems && !item.DropDown.Visible)
                {
                    item.ShowDropDown();
                }
            }
        }
    }
}
