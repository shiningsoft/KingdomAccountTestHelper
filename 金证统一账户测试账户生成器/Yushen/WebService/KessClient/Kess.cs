using NLog;
using System;
using System.Data;
using System.Reflection;
using System.ServiceModel;

namespace Yushen.WebService.KessClient
{
    /// <summary>
    /// 用于金证统一账户系统WebService接口的操作类
    /// </summary>
    public partial class Kess : IDisposable
    {

        /// <summary>
        /// 字典项，金证Win版柜台系统
        /// </summary>
        public static string WindowsCounter = "Win";

        /// <summary>
        /// 字典项，金证U版柜台系统
        /// </summary>
        public static string UnixCounter = "Uinx";

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

        /// <summary>
        /// 按照金证标准流程开立客户号
        /// </summary>
        /// <param name="USER_NAME"></param>
        /// <param name="ID_CODE"></param>
        /// <param name="ID_ISS_AGCY"></param>
        /// <param name="ID_BEG_DATE"></param>
        /// <param name="ID_EXP_DATE"></param>
        /// <param name="CITIZENSHIP"></param>
        /// <param name="NATIONALITY"></param>
        public string createCustomerCode(User user)
        {
            Response response = new Response(this.getUserInfoById(user.id_code));
            if (response.length == 0 || this.getSingleCommonParamValue("OPEN_CUST_CHECK_ID_FLAG") == "1")
            {
                response = this.openCustomer(user);
                return response.getSingleNodeText("/response/record/row/USER_CODE");
            }
            else
            {
                throw new Exception("系统不允许同一证件开多个客户代码");
            }
        }

        /// <summary>
        /// 按照金证标准流程开立资金账号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string openCuacctCode(User user)
        {
            Response response = this.listCuacct(user);
            // 判断是否已经开立过资金账号
            if (response.length == 0)
            {
                bool useUserCodeAsCuacctCode = this.getSingleCommonParamValue("CUST_CUACCT_SHARE_SERIAL") == "1" ? true : false;
                if (useUserCodeAsCuacctCode)
                {
                    user.cuacct_code = user.user_code;
                }
                response = this.openCuacct(user);
            }
            else if (response.length > 0)
            {
                throw new NotImplementedException("客户号已有资金账户的处理逻辑暂未实现");
            }

            if (response.flag == "1")
            {
                return response.getSingleNodeText("/response/record/row/CUACCT_CODE");
            }
            else
            {
                throw new Exception("开立资金账号失败：" + response.prompt);
            }
        }

        /// <summary>
        /// 查询客户资产账户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Response listCuacct(User user)
        {
            if (user.user_code =="")
            {
                throwNewException("必要参数用户代码不能为空");
            }
            Request request = new Request(this.operatorId, "listCuacct");
            request.setAttr("USER_CODE", user.user_code);    // 客户名称

            Response response = new Response(this.invoke(request));
            if (response.flag != "0" && response.flag != "1")
            {
                throw new Exception("操作失败：" + response.prompt);
            }
            return response;
        }

        public void bankSign(User user)
        {
            this.cubsbScOpenAcctOneStep(user.user_code, user.cuacct_code, user.bank_acct_code);
        }

        /// <summary>
        /// 管理用户密码
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="OPERATION_TYPE">操作类型，0增加密码，1修改密码，3重置密码</param>
        /// <returns></returns>
        public bool mdfUserPassword(User user, string USE_SCOPE, string OPERATION_TYPE="0")
        {
            // 前置条件判断
            if (user.user_code == "")
            {
                throwNewException("必要参数用户代码不能为空");
            }
            if (user.password == "")
            {
                throwNewException("必要参数密码不能为空");
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "mdfUserPassword");
            request.setAttr("OP_USER", this.operatorId);    // 操作用户
            request.setAttr("OPERATION_TYPE", OPERATION_TYPE);    // 操作类型，0增加密码，1修改密码，3重置密码
            request.setAttr("USER_CODE", user.user_code);    // 客户名称
            request.setAttr("NEW_AUTH_DATA", user.password);    // 新密码
            request.setAttr("USE_SCOPE", USE_SCOPE);    // 设置交易密码

            // 调用WebService获取返回值
            Response response = new Response(this.invoke(request));

            // 判断返回的操作结果是否异常
            if (response.flag != "1")
            {
                throw new Exception("操作失败：" + response.prompt);
            }

            // 返回结果
            return true;
        }

        public bool syncSurveyAns2Kbss(User user, string riskLevel)
        {
            // 前置条件判断
            if (user.user_code == "")
            {
                throwNewException("必要参数用户代码不能为空");
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "syncSurveyAns2Kbss");
            request.setAttr("USER_CODE", user.user_code);    // 客户名称
            request.setAttr("SURVEY_SN", "1");
            request.setAttr("SURVEY_COLS", RiskTest.cols);

            string cells="";
            switch (riskLevel)
            {
                case "A":
                    cells = RiskTest.cells_A;
                    break;
                case "B":
                    cells = RiskTest.cells_B;
                    break;
                case "C":
                    cells = RiskTest.cells_C;
                    break;
                case "D":
                    cells = RiskTest.cells_D;
                    break;
                case "E":
                    cells = RiskTest.cells_E;
                    break;

                default:
                    throwNewException("风险等级" + riskLevel + "不存在");
                    break;
            }
            request.setAttr("SURVEY_CELLS", cells);

            // 调用WebService获取返回值
            Response response = new Response(this.invoke(request));

            // 判断返回的操作结果是否异常
            if (response.flag != "1")
            {
                throw new Exception("操作失败：" + response.prompt);
            }

            // 返回结果
            return true;
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

            return response.getSingleNodeText("/response/record/row/REGKEY_VAL");
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
        /// 查询数据字典
        /// </summary>
        /// <param name="dictName">字典项名称</param>
        /// <returns></returns>
        public string getDictData(string dictName)
        {
            if (dictName=="")
            {
                throwNewException("字典项名称不能为空");
            }
            Request request = new Request(this.operatorId, "getDictData");
            request.setAttr("DD_ID", dictName);
            request.setAttr("INT_ORG", "0");

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
        /// 析构函数
        /// </summary>
        public void Dispose()
        {
            this.kessClientType.GetMethod("Close").Invoke(this.kessClient, new object[] { }); ;
        }
    }
}
