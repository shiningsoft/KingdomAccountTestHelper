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
    class Kess:IDisposable
    {
        string kessWebserviceURL = "http://60.173.222.38:30002/kess/services/KessService?wsdl";
        string kessClassName = "金证统一账户测试账户生成器.KessService.KessServiceClient";
        object kessClient;
        Type kessClientType;
        string operatorId;
        string password;
        string channel;
        static string WindowsCounter = "Win";
        static string UnixCounter = "Uinx";

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="operatorId">操作员代码</param>
        /// <param name="password">操作员密码</param>
        /// <param name="channel">统一账户操作渠道</param>
        public Kess(string operatorId, string password, string channel)
        {
            this.operatorId = operatorId;
            this.password = password;
            this.channel = channel;

            try
            {
                // 利用反射建立WebService的实例
                this.kessClientType = Type.GetType(this.kessClassName);
                this.kessClient = Activator.CreateInstance(this.kessClientType, new object[] { "KessService", this.kessWebserviceURL });
            }
            catch (Exception ex)
            {
                this.throwNewException("WebService连接失败：" + ex.Message);
            }

            this.operatorLogin();
        }

        /// <summary>
        /// 操作员登录
        /// </summary>
        /// <returns></returns>
        internal void operatorLogin()
        {
            Request request = new Request(this.operatorId, "operatorLogin");
            request.setAttr("USER_CODE", this.operatorId);
            request.setAttr("PASSWORD", this.password);
            request.setAttr("F_CHANNEL", this.channel);

            Response response = new Response(this.invoke(request));
            
            if (response.flag == "0" && response.prompt.IndexOf("用户已登陆，不用重复登陆")==-1)
            {
                this.throwNewException("用户登录失败：" + response.prompt);
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
            
            Response response = new Response(this.invoke(request));
            
            return response.xml;
        }

        /// <summary>
        /// 公共参数查询
        /// </summary>
        /// <param name="regkeyId"></param>
        /// <returns></returns>
        public string getCommonParams(string regkeyId)
        {
            Request request = new Request(this.operatorId, "getCommonParams");
            request.setAttr("REGKEY_ID", regkeyId);

            Response response = new Response(this.invoke(request));
            return response.xml;
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
        /// 开客户号
        /// </summary>
        /// <param name="USER_NAME"></param>
        /// <param name="USER_TYPE"></param>
        /// <param name="ID_TYPE"></param>
        /// <param name="ID_CODE"></param>
        /// <param name="USER_FNAME"></param>
        /// <param name="ID_ISS_AGCY"></param>
        /// <param name="ID_EXP_DATE"></param>
        /// <param name="CITIZENSHIP"></param>
        /// <param name="NATIONALITY"></param>
        /// <returns></returns>
        public string openCustomer(string USER_NAME,string ID_CODE,string ID_ISS_AGCY,string ID_BEG_DATE,string ID_EXP_DATE,string CITIZENSHIP, string NATIONALITY)
        {
            Request request = new Request(this.operatorId, "openCustomer");
            request.setAttr("USER_NAME", USER_NAME);    // 客户名称
            request.setAttr("ID_CODE", ID_CODE);        // 证件号码
            request.setAttr("USER_FNAME", USER_NAME);   // 用户全称
            request.setAttr("ID_ISS_AGCY", ID_ISS_AGCY);    // 发证机关
            request.setAttr("ID_BEG_DATE", ID_BEG_DATE);    // 证件开始日期
            request.setAttr("ID_EXP_DATE", ID_EXP_DATE);    // 证件有效日期
            request.setAttr("CITIZENSHIP", CITIZENSHIP);    // 国籍
            request.setAttr("NATIONALITY", NATIONALITY);    // 民族

            Response response = new Response(this.invoke(request));
            return response.xml;
        }

        /// <summary>
        /// 根据请求参数调用金证WebService的对应接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal string invoke(Request request)
        {
            string result = "";

            logger.Info("调用Webservice功能<" + request.operateName + ">|" + request.xml);

            // 利用反射调用WebService的成员函数
            try
            {
                result = (string)this.kessClientType.GetMethod(request.operateName).Invoke(this.kessClient, new object[] { request.xml });
            }
            catch (Exception ex)
            {
                this.throwNewException("WebService调用失败：" + this.kessWebserviceURL + ex.Message);
            }
            
            logger.Info("响应Webservice功能<" + request.operateName + ">|" + result);

            return result;
        }

        internal void throwNewException(string message)
        {
            logger.Error(message);
            throw new Exception(message);
        }

        public void Dispose()
        {
            this.kessClientType.GetMethod("Close").Invoke(this.kessClient, new object[] {}); ;
        }
    }
}
