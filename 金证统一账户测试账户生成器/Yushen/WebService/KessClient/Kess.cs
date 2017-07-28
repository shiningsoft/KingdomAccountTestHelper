using NLog;
using System;
using System.Reflection;
using System.ServiceModel;
using 金证统一账户测试账户生成器.KessService;

namespace Yushen.WebService.KessClient
{
    /// <summary>
    /// 用于金证统一账户系统WebService接口的操作类
    /// </summary>
    public partial class Kess : IDisposable
    {

        /// <summary>
        /// 设置金证WebService的URL地址
        /// </summary>
        string kessWebserviceURL = "http://60.173.222.38:30004/kess/services/KessService?wsdl";

        /// <summary>
        /// 设置金证WebService接口的类名，用于建立反射调用WebService
        /// </summary>
        readonly string kessClassName = "金证统一账户测试账户生成器.KessService.KessServiceClient";

        /// <summary>
        /// 用于建立反射调用WebService
        /// </summary>
        object kessClient;

        /// <summary>
        /// 用于建立反射调用WebService
        /// </summary>
        Type kessClientType;

        /// <summary>
        /// 统一账户系统操作员编号
        /// </summary>
        string operatorId;

        /// <summary>
        /// 统一账户系统操作员密码
        /// </summary>
        string password;

        /// <summary>
        /// 操作渠道
        /// </summary>
        string channel;

        /// <summary>
        /// 字典项，金证Win版柜台系统
        /// </summary>
        static string WindowsCounter = "Win";

        /// <summary>
        /// 字典项，金证U版柜台系统
        /// </summary>
        static string UnixCounter = "Uinx";

        /// <summary>
        /// 日志记录器
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="operatorId">操作员代码</param>
        /// <param name="password">操作员密码</param>
        /// <param name="channel">统一账户操作渠道</param>
        public Kess(string operatorId, string password, string channel, string kessWebserviceURL = "")
        {
            this.operatorId = operatorId;
            this.password = password;
            this.channel = channel;
            if (kessWebserviceURL != "")
            {
                this.kessWebserviceURL = kessWebserviceURL;
            }

            // 利用反射建立WebService的实例
            this.kessClientType = Type.GetType(this.kessClassName);
            this.kessClient = Activator.CreateInstance(this.kessClientType, new object[] { "KessService", this.kessWebserviceURL });
            if (this.kessClient == null)
            {
                this.throwNewException("WebService连接失败：" + this.kessWebserviceURL);
            }

            // 操作员登录
            this.operatorLogin();
        }

        /// <summary>
        /// 操作员登录
        /// </summary>
        /// <returns></returns>
        public bool operatorLogin(string operatorId = "", string password = "")
        {
            if (operatorId != "")
            {
                if (password != "")
                {
                    this.operatorId = operatorId;
                    this.password = password;
                }
                else
                {
                    this.throwNewException("操作员密码不能为空");
                }
            }

            Request request = new Request(this.operatorId, "operatorLogin");
            request.setAttr("USER_CODE", this.operatorId);
            request.setAttr("PASSWORD", this.password);
            request.setAttr("F_CHANNEL", this.channel);

            Response response = new Response(this.invoke(request));

            if (response.flag == "0" && response.prompt.IndexOf("用户已登陆，不用重复登陆") == -1)
            {
                this.throwNewException("用户登录失败：" + response.prompt);
            }

            return true;
        }

        public void createCustomerCode(string USER_NAME, string ID_CODE, string ID_ISS_AGCY, string ID_BEG_DATE, string ID_EXP_DATE, string CITIZENSHIP, string NATIONALITY)
        {
            Response response = new Response(this.getUserInfoById(ID_CODE));
            if (response.length > 0)
            {
                this.getCommonParams("OPEN_CUST_CHECK_ID_FLAG");
            }

        }

        /// <summary>
        /// 查询用户基本资料
        /// </summary>
        /// <param name="userCode">客户代码</param>
        /// <returns></returns>
        public string queryCustBasicInfoList(string userCode)
        {
            Request request = new Request(this.operatorId, "queryCustBasicInfoList");
            request.setAttr("USER_CODE", userCode);

            return this.invoke(request);
        }

        /// <summary>
        /// 单个公共参数查询，返回value值。
        /// </summary>
        /// <param name="regkeyId"></param>
        /// <returns></returns>
        public string getSingleCommonParamValue(string regkeyId)
        {
            Response response = this.getCommonParams(regkeyId);

            if (response.length > 1)
            {
                this.throwNewException("单一公共参数查询时返回值不能多于1个");
            }

            return (string)this.createDataSetFromXmlString(response.record).Tables[0].Rows[0]["REGKEY_VAL"];
        }

        /// <summary>
        /// 公共参数查询
        /// </summary>
        /// <param name="regkeyId"></param>
        /// <returns></returns>
        public Response getCommonParams(string regkeyId)
        {
            Request request = new Request(this.operatorId, "getCommonParams");
            request.setAttr("REGKEY_ID", regkeyId);

            Response response = new Response(this.invoke(request));

            if (response.flag != "1")
            {
                this.throwNewException("查询公共参数" + regkeyId + "失败：" + response.prompt);
            }

            return response;
        }

        /// <summary>
        /// 根据身份证号查询已有客户代码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string getUserInfoById(string id)
        {
            Request request = new Request(this.operatorId, "getUserInfoById");
            request.setAttr("ID_CODE", id);

            Response response = new Response(this.invoke(request));
            return response.xml;
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        public void Dispose()
        {
            this.kessClientType.GetMethod("Close").Invoke(this.kessClient, new object[] { }); ;
        }
    }
}
