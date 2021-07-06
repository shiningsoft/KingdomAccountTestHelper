using NLog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yushen.WebService.KessClient;
using 金证统一账户测试账户生成器.Properties;

namespace 金证统一账户测试账户生成器
{
    public partial class frmFramework : Form
    {
        /// <summary>
        /// 软件使用期限，已作废
        /// </summary>
        DateTime expiredDate = DateTime.Parse("2019/12/31");

        /// <summary>
        /// 金证接口调用工具
        /// </summary>
        public Kess kess;

        /// <summary>
        /// 日志记录器
        /// </summary>
        public static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 用于显示执行结果和错误消息的窗体
        /// </summary>
        public frmResultForm resultForm;

        /// <summary>
        /// 关于窗体
        /// </summary>
        frmAboutBox aboutBox;

        /// <summary>
        /// 设置窗体
        /// </summary>
        frmSettings frmSettings;

        /// <summary>
        /// 程序启动封面
        /// </summary>
        frmStartLogo frmStartLogo = new frmStartLogo();
        
        /// <summary>
        /// 功能窗体列表
        /// </summary>
        Dictionary<string, Form> forms = new Dictionary<string, Form>();
        
        /// <summary>
        /// 用于刷新当前WebService连接数和队列数的计时器
        /// </summary>
        Timer timerRefreshQueue;

        /// <summary>
        /// 用于保存原始的窗体标题，以便切换窗体时显示不同的标题
        /// </summary>
        string defaultTitle = "";
        
        /// <summary>
        /// 用于检查软件有效期的计时器
        /// </summary>
        Timer timerCheckExpired = new Timer();
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmFramework()
        {
#if DEBUG
            frmStartLogo.Close();
#else
            frmStartLogo.Show();
#endif
            InitializeComponent();
        }

        /// <summary>
        /// 程序初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
#if DEBUG
            购买授权ToolStripMenuItem.Visible = false;
#endif
            // 保存默认窗体标题
            defaultTitle = Text;

            // 启动有效期检查
            //Console.WriteLine(expiredDate);
            //tsslExpired.Text = "有效期：" + expiredDate.ToLongDateString();
            //timerCheckExpired.Interval = 15000;
            //timerCheckExpired.Tick += TimerCheckExpired_Tick;
            //timerCheckExpired.Start();

            resultForm = new frmResultForm(this);
            resultForm.Location = new System.Drawing.Point(Location.X + Width, Location.Y);

            // 添加功能窗口列表
            forms.Add("存量账户处理", new frmExistAccount(this));
            forms.Add("新开账户", new frmNewAccount(this));
            forms.Add("数据字典查询", new frmDictQuery(this));
            forms.Add("公共参数查询", new frmCommonParamQuery(this));
            forms.Add("接口测试工具", new frmWebServiceInterfaceTest(this));
            forms.Add("接口测试工具图形版", new frmWebServiceInterfaceTestAdvance(this));

            // 将功能窗口添加到菜单
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

            // 启用有效期检查
            // checkExpired();

            tsslVersion.Text = "当前版本：" + Application.ProductVersion;
            
            InitWebService();
        }

        /// <summary>
        /// 定时检查软件是否过期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerCheckExpired_Tick(object sender, EventArgs e)
        {
            checkExpired();
        }

        /// <summary>
        /// 检查软件是否过期
        /// </summary>
        private void checkExpired()
        {
#if DEBUG
            if (DateTime.Now.Date > expiredDate)
            {
                TopMost = true;
                Show();
                Activate();
                timerCheckExpired.Stop();
                if (MessageBox.Show("软件已经过期，请更新到最新版本。") == DialogResult.OK)
                {
                    Close();
                }
            }
#else
            if (DateTime.Now.Date > expiredDate)
            {
                TopMost = true;
                Show();
                Activate();
                timerCheckExpired.Stop();
                if (MessageBox.Show("软件已经过期，请更新到最新版本。\r\n\r\n购买或续费请您联系申星软件客户服务邮箱：service@shiningsoft.com.cn。") == DialogResult.OK)
                {
                    Close();
                }
            }
            else if(DateTime.Now.Date > expiredDate.AddDays(-15))
            {
                MessageBox.Show("软件将于" + (expiredDate - DateTime.Now).Days.ToString() + "天后到期。\r\n\r\n购买或续费请您联系申星软件客户服务邮箱：service@shiningsoft.com.cn。");
            }
#endif
        }

        /// <summary>
        /// 初始化
        /// </summary>
        async public void InitWebService()
        {
            try
            {
                // 关闭已经存在的服务
                if (kess!=null)
                {
                    if (timerRefreshQueue != null)
                    {
                        timerRefreshQueue.Tick -= TimerRefreshQueue_Tick;
                        timerRefreshQueue.Dispose();
                        timerRefreshQueue = null;
                    }
                    kess.Dispose();
                    kess = null;
                }

                // 初始化WebService连接
                if (kess == null)
                {
                    kess = new Kess(Settings.Default.操作员代码, Settings.Default.操作员密码, Settings.Default.操作渠道, Settings.Default.webservice,Settings.Default.最大并发数);
                }

                // 设置柜台版本
                if (Settings.Default.统一账户版本 == Kess.Edtion.Win.ToString())
                {
                    kess.edition = Kess.Edtion.Win;
                }
                else
                {
                    kess.edition = Kess.Edtion.U;
                }

                toolStripDropDownButtonWebServices.DropDownItems.Clear();
                foreach (string url in Settings.Default.webservices)
                {
                    Uri server = new Uri(url);
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Text = url;
                    item.Click += Webservice_Item_Click;

                    toolStripDropDownButtonWebServices.DropDownItems.Add(item);
                }

                Uri uri = new Uri(Settings.Default.webservice);
                toolStripDropDownButtonWebServices.Text = uri.Host + ":" + uri.Port;

                toolStripStatusLabelCurrentServer.Text = "获取环境信息中，请稍候......";

                currentUser.Text = "用户：" + Settings.Default.操作员代码;

                // 更新状态栏信息
                string serverName = "未能获取服务器名称";
                try
                {
                    serverName = await kess.getSingleCommonParamValue("SERVER_NAME");

                    resultForm.Append("成功连接到" + serverName);
                    if (serverName.IndexOf("测试") == -1)
                    {
                        resultForm.Append("服务器公共参数（SERVER_NAME）中未检测到目标字符“测试”，请确认是否在测试环境中运行！");
                    }
                }
                catch (Exception ex)
                {
                    resultForm.Append("获取服务器信息失败：" + ex.Message);
                }

                toolStripStatusLabelCurrentServer.Text = serverName;
                timerRefreshQueue = new Timer();
                timerRefreshQueue.Interval = 100;
                timerRefreshQueue.Tick += TimerRefreshQueue_Tick;
                timerRefreshQueue.Start();
            }
            catch (TargetInvocationException ex)
            {
                resultForm.Append("初始化失败：" + ex.Message + "请检查设置是否正确或WebService接口状态是否正常？");
                系统设置ToolStripMenuItem.PerformClick();
            }
            catch (Exception ex)
            {
                resultForm.Append("初始化失败：" + ex.Message);
                系统设置ToolStripMenuItem.PerformClick();
            }
        }

        private void Webservice_Item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            Settings.Default.webservice = item.Text;
            Settings.Default.Save();
            InitWebService();
        }

        /// <summary>
        /// 点击功能窗体按钮时自动打开对应功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            Text = defaultTitle + " - " + item.Text;
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
                frmSettings = new frmSettings(this);
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
        /// 自动展开下拉菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiFunction_MouseHover(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 关闭窗体时自动保存程序设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFramework_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        /// <summary>
        /// 查看当前日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 查看当前日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.CurrentDirectory + @"\logs\" + DateTime.Now.ToString("yyyy-MM-dd") + @".log";
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        /// <summary>
        /// 打开日志文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 打开日志目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.CurrentDirectory + @"\logs\";
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                resultForm.Append(ex.Message);
            }
        }

        /// <summary>
        /// 操作员登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void tsmiOperatorLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (await kess.operatorLogin())
                {
                    resultForm.Show();
                    resultForm.Append("操作员登录成功");
                }
            }
            catch (Exception ex)
            {
                resultForm.Append("操作员登录失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 显示系统设置窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmSettings == null || frmSettings.IsDisposed)
            {
                frmSettings = new frmSettings(this);
                frmSettings.Show();
                frmSettings.Activate();
            }
            else
            {
                frmSettings.Activate();
            }
        }

        /// <summary>
        /// 打开用户自定义数据字典的所在文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 自定义数据字典ToolStripMenuItem_Click(object sender, EventArgs e)
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

        /// <summary>
        /// 操作员退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void 操作员退出toolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (await kess.operatorLogout())
                {
                    resultForm.Show();
                    resultForm.Append("操作员退出成功");
                }
            }
            catch (Exception ex)
            {
                resultForm.Append("操作员登录失败：" + ex.Message);
            }
        }

        private void 自动重新登录toolStripMenuItem_Click(object sender, EventArgs e)
        {
            kess.autoRelogin = 自动重新登录toolStripMenuItem.Checked;
        }

        private void frmFramework_LocationChanged(object sender, EventArgs e)
        {
            if (resultForm != null)
            {
                resultForm.refreshLocation();
            }
        }

        private void frmFramework_Resize(object sender, EventArgs e)
        {
            if (resultForm == null)
            {
                return;
            }

            if (WindowState == FormWindowState.Minimized)
            {
                resultForm.WindowState = FormWindowState.Minimized;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                resultForm.WindowState = FormWindowState.Normal;
            }
        }

        private void 购买授权ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.shiningsoft.com.cn/price.html");
        }
    }
}
