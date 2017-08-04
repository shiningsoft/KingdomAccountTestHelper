using NLog;
using System;
using System.Data;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

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
                    user.cuacct_code = user.cust_code;
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
            if (user.cust_code == "")
            {
                throwNewException("必要参数用户代码不能为空");
            }
            Request request = new Request(this.operatorId, "listCuacct");
            request.setAttr("USER_CODE", user.cust_code);    // 客户名称

            Response response = new Response(this.invoke(request));
            if (response.flag != "0" && response.flag != "1")
            {
                throw new Exception("操作失败：" + response.prompt);
            }
            return response;
        }

        public void bankSign(User user)
        {
            this.cubsbScOpenAcctOneStep(user.cust_code, user.cuacct_code, user.bank_acct_code);
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
            if (user.cust_code == "")
            {
                throwNewException("必要参数客户代码不能为空");
            }
            if (user.password == "")
            {
                throwNewException("必要参数密码不能为空");
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "mdfUserPassword");
            request.setAttr("OP_USER", this.operatorId);    // 操作用户
            request.setAttr("OPERATION_TYPE", OPERATION_TYPE);    // 操作类型，0增加密码，1修改密码，3重置密码
            request.setAttr("USER_CODE", user.cust_code);    // 客户名称
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

        /// <summary>
        /// 同步风险测评答案到统一账户系统
        /// </summary>
        /// <param name="user"></param>
        /// <param name="riskLevel"></param>
        /// <returns></returns>
        public bool syncSurveyAns2Kbss(User user, string riskLevel)
        {
            // 前置条件判断
            if (user.cust_code == "")
            {
                throwNewException("必要参数客户代码不能为空");
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "syncSurveyAns2Kbss");
            request.setAttr("USER_CODE", user.cust_code);    // 客户名称
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
        /// 新中登一码通开户
        /// 实现 2.97 新中登一码通开户
        /// 功能说明：查询中登一码通，若查到状态正常的一码通，将返回所有一码通结果，否则报送中登开立一码通，同时将一码通加挂至账户系统内。
        /// </summary>
        /// <param name="USER_TYPE">客户类别（必传）DD[USER_TYPE]</param>
        /// <param name="CUST_FNAME">客户名称（必传）</param>
        /// <param name="ID_TYPE">证件类型（必传）</param>
        /// <param name="ID_CODE">证件号码（必传）</param>
        /// <param name="INT_ORG">机构编码（必传）</param>
        /// <param name="CUST_CODE">客户代码（必传）</param>
        /// <param name="BIRTHDAY">出生日期/注册日期（必传）</param>
        /// <param name="ID_BEG_DATE">证件开始日期（必传）</param>
        /// <param name="ID_EXP_DATE">证件有效日期（必传）</param>
        /// <param name="CITIZENSHIP">国籍（必传）DD[CITIZENSHIP]</param>
        /// <param name="ID_ADDR">证件地址（必传）</param>
        /// <param name="ADDRESS">联系地址（必传）</param>
        /// <param name="ZIP_CODE">邮政编码（必传）</param>
        /// <param name="OCCU_TYPE">职业类型（必传）</param>
        /// <param name="NATIONALITY">民族（必传）</param>
        /// <param name="EDUCATION">学历（个人必传）</param>
        /// <param name="TEL">联系电话（必传）</param>
        /// <param name="MOBILE_TEL">移动电话（必传）</param>
        /// <param name="NET_SERVICE">网络服务（必传）DD[NET_SERVICE]</param>
        /// <param name="NET_SERVICEPASS">网络服务密码</param>
        /// <param name="SEX">性别（必传）DD[SEX]</param>
        /// <param name="ID_TYPE2">证件类型（机构必传）</param>
        /// <param name="ID_CODE2">组织机构代码证（机构必传）</param>
        /// <param name="ID_EXP_DATE2">有效日期（机构必传）</param>
        /// <param name="ORGANIZATION_CLS">机构类别（机构必传）</param>
        /// <param name="CAPITAL_ATTR">资本属性（机构必传）</param>
        /// <param name="NATIONAL_ATTR">国有属性（机构必传）</param>
        /// <param name="ORG_SIMPLE_NAME">机构简称（机构必传）</param>
        /// <param name="LEGAL_REP">法人代表（机构必传）</param>
        /// <param name="LEGAL_REP_ID_TYPE">法人代表证件类型（机构必传）</param>
        /// <param name="LEGAL_REP_ID_CODE">法人代表证件代码（机构必传）</param>
        /// <param name="LINKMAN">联系人姓名（机构必传）</param>
        /// <param name="LINKMAN_ID_TYPE">联系人证件类型（机构必传）</param>
        /// <param name="LINKMAN_ID_CODE">联系人证件代码（机构必传）</param>
        /// <param name="ID_ADDR2">证件2地址（机构必传）</param>
        /// <param name="ACCT_OPENTYPE">开户方式(必传)DD[ACCT_OPENTYPE]</param>
        /// <returns></returns>
        public Response openYMTAcct(string USER_TYPE,
                                    string CUST_FNAME,
                                    string ID_TYPE,
                                    string ID_CODE,
                                    string INT_ORG,
                                    string CUST_CODE,
                                    string BIRTHDAY,
                                    string ID_BEG_DATE,
                                    string ID_EXP_DATE,
                                    string CITIZENSHIP,
                                    string ID_ADDR,
                                    string ADDRESS,
                                    string ZIP_CODE,
                                    string OCCU_TYPE,
                                    string NATIONALITY,
                                    string EDUCATION,
                                    string TEL,
                                    string MOBILE_TEL,
                                    string SEX,
                                    string NET_SERVICE = "0",
                                    string NET_SERVICEPASS = "",
                                    string ID_TYPE2 = "",
                                    string ID_CODE2 = "",
                                    string ID_EXP_DATE2 = "",
                                    string ORGANIZATION_CLS = "",
                                    string CAPITAL_ATTR = "",
                                    string NATIONAL_ATTR = "",
                                    string ORG_SIMPLE_NAME = "",
                                    string LEGAL_REP = "",
                                    string LEGAL_REP_ID_TYPE = "",
                                    string LEGAL_REP_ID_CODE = "",
                                    string LINKMAN = "",
                                    string LINKMAN_ID_TYPE = "",
                                    string LINKMAN_ID_CODE = "",
                                    string ID_ADDR2 = "",
                                    string ACCT_OPENTYPE = "1"
                                    )
        {
            // 前置条件判断


            // 初始化请求
            Request request = new Request(this.operatorId, "openYMTAcct");
            request.setAttr("USER_TYPE", USER_TYPE);
            request.setAttr("CUST_FNAME", CUST_FNAME);
            request.setAttr("ID_TYPE", ID_TYPE);
            request.setAttr("ID_CODE", ID_CODE);
            request.setAttr("INT_ORG", INT_ORG);
            request.setAttr("CUST_CODE", CUST_CODE);
            request.setAttr("BIRTHDAY", BIRTHDAY);
            request.setAttr("ID_BEG_DATE", ID_BEG_DATE);
            request.setAttr("ID_EXP_DATE", ID_EXP_DATE);
            request.setAttr("CITIZENSHIP", CITIZENSHIP);
            request.setAttr("ID_ADDR", ID_ADDR);
            request.setAttr("ADDRESS", ADDRESS);
            request.setAttr("ZIP_CODE", ZIP_CODE);
            request.setAttr("OCCU_TYPE", OCCU_TYPE);
            request.setAttr("NATIONALITY", NATIONALITY);
            request.setAttr("EDUCATION", EDUCATION);
            request.setAttr("TEL", TEL);
            request.setAttr("MOBILE_TEL", MOBILE_TEL);
            request.setAttr("NET_SERVICE", NET_SERVICE);
            request.setAttr("NET_SERVICEPASS", NET_SERVICEPASS);
            request.setAttr("SEX", SEX);
            request.setAttr("ID_TYPE2", ID_TYPE2);
            request.setAttr("ID_CODE2", ID_CODE2);
            request.setAttr("ID_EXP_DATE2", ID_EXP_DATE2);
            request.setAttr("ORGANIZATION_CLS", ORGANIZATION_CLS);
            request.setAttr("CAPITAL_ATTR", CAPITAL_ATTR);
            request.setAttr("NATIONAL_ATTR", NATIONAL_ATTR);
            request.setAttr("ORG_SIMPLE_NAME", ORG_SIMPLE_NAME);
            request.setAttr("LEGAL_REP", LEGAL_REP);
            request.setAttr("LEGAL_REP_ID_TYPE", LEGAL_REP_ID_TYPE);
            request.setAttr("LEGAL_REP_ID_CODE", LEGAL_REP_ID_CODE);
            request.setAttr("LINKMAN", LINKMAN);
            request.setAttr("LINKMAN_ID_TYPE", LINKMAN_ID_TYPE);
            request.setAttr("LINKMAN_ID_CODE", LINKMAN_ID_CODE);
            request.setAttr("ID_ADDR2", ID_ADDR2);
            request.setAttr("ACCT_OPENTYPE", ACCT_OPENTYPE);


            // 调用WebService获取返回值
            Response response = new Response(this.invoke(request));

            // 判断返回的操作结果是否异常
            if (response.flag != "1")
            {
                throw new Exception("操作失败：" + response.prompt);
            }

            // 返回结果
            return response;
        }

        /// <summary>
        /// 实现2.95 新中登实时查询
        /// </summary>
        /// <param name="user">用户信息对象</param>
        /// <param name="ACCT_TYPE">证券账户类别(非必输)11:沪A，21:深A。DD[ACCT_TYPE]</param>
        /// <returns></returns>
        public Response onSearchNewZD(User user,string ACCT_TYPE = "")
        {
            // 前置条件判断


            // 初始化请求
            Request request = new Request(this.operatorId, "onSearchNewZD");
            request.setAttr("USER_TYPE", user.user_type);
            request.setAttr("CUST_FNAME", user.user_fname);
            request.setAttr("ID_TYPE", user.id_type);
            request.setAttr("ID_CODE", user.id_code);
            request.setAttr("INT_ORG", user.int_org);
            request.setAttr("ACCT_TYPE", ACCT_TYPE);


            // 调用WebService获取返回值
            Response response = new Response(this.invoke(request));

            // 判断返回的操作结果是否异常
            if (response.flag != "1")
            {
                throw new Exception("操作失败：" + response.prompt);
            }

            // 返回结果
            return response;
        }

        /// <summary>
        /// 按照金证标准流程开立一码通账号
        /// </summary>
        /// <param name="user"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public Response openStkAcct(User user, string stkbd, int timeout = 30)
        {
            //Response response = this.submitStkAcctBizOpReq2NewZD("0", "07",
            //    CUST_CODE: user.cust_code,
            //    CUST_FNAME: user.user_fname,
            //    ID_TYPE: user.id_type,
            //    ID_CODE: user.id_code,
            //    ID_ADDR: user.id_addr,
            //    ID_BEG_DATE: user.id_beg_date,
            //    ID_EXP_DATE: user.id_exp_date,
            //    CITIZENSHIP: user.citizenship,
            //    ADDRESS: user.id_addr,
            //    SEX: user.sex,
            //    OCCU_TYPE: user.occu_type,
            //    EDUCATION: user.education);
            Response response = this.onSearchNewZD(user, "11");
            response = this.onSearchNewZD(user, "21");


            // 判断账户业务处理状态
            string status = response.getValue("ACCTBIZ_STATUS");
            if (status == "2")
            {
                string ACCT_TYPE = response.getValue("ACCT_TYPE");
                string ACCT_STATUS = response.getValue("ACCT_STATUS");
                if (ACCT_TYPE == "21" && ACCT_STATUS == "00")
                {
                    // 系统内加挂
                    Console.WriteLine("准备系统内加挂");
                    throw new NotImplementedException("暂未实现此功能");
                }
                else
                {
                    // 新开股东账户
                    Console.WriteLine("准备新开股东账户");
                    throw new NotImplementedException("暂未实现此功能");
                }

            }
            else
            {
                // 证券账户处理状态=3
                throw new NotImplementedException("暂未实现此功能");
            }
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
        public Response getDictData(string dictName)
        {
            if (dictName=="")
            {
                throwNewException("字典项名称不能为空");
            }
            Request request = new Request(this.operatorId, "getDictData");
            request.setAttr("DD_ID", dictName);
            request.setAttr("INT_ORG", "0");

            Response response = new Response(this.invoke(request));

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
