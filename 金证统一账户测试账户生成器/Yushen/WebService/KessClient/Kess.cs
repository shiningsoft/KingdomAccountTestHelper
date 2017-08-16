using System;
using Dict = Yushen.WebService.KessClient.Dict;

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
                string message = "WebService连接失败：" + this.kessWebserviceURL;
                logger.Error(message);
                throw new Exception(message);
            }

            // 操作员登录
            this.operatorLogin();
        }

        /// <summary>
        /// 操作员登录
        /// 登陆成功返回true，登录失败抛出异常
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
                    string message = "操作员密码不能为空";
                    logger.Error(message);
                    throw new Exception(message);
                }
            }

            Request request = new Request(this.operatorId, "operatorLogin");
            request.setAttr("USER_CODE", this.operatorId);
            request.setAttr("PASSWORD", this.password);
            request.setAttr("F_CHANNEL", this.channel);

            Response response = this.invoke(request);

            if (response.flag == "0" && response.prompt.IndexOf("用户已登陆，不用重复登陆") == -1)
            {
                string message = "用户登录失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
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
            Response response = this.getUserInfoById(user.id_code);
            if (response.length == 0 || this.getSingleCommonParamValue("OPEN_CUST_CHECK_ID_FLAG") == "1")
            {
                response = this.openCustomer(user);
                return response.getValue("USER_CODE");
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
                response = this.openCuacct(user.cust_code,"z");
            }
            else if (response.length > 0)
            {
                string msg = "客户号已有资金账户的处理逻辑暂未实现";
                logger.Error(msg);
                throw new NotImplementedException(msg);
            }

            if (response.flag == "1")
            {
                return response.getValue("CUACCT_CODE");
            }
            else
            {
                string msg = "开立资金账号失败：" + response.prompt;
                logger.Error(msg);
                throw new NotImplementedException(msg);
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
                string message = "用户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            Request request = new Request(this.operatorId, "listCuacct");
            request.setAttr("USER_CODE", user.cust_code);    // 客户名称

            Response response = this.invoke(request);
            if (response.flag != "0" && response.flag != "1")
            {
                string msg = "操作失败：" + response.prompt;
                logger.Error(msg);
                throw new NotImplementedException(msg);
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
                string message = "客户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (user.password == "")
            {
                string message = "密码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "mdfUserPassword");
            request.setAttr("OP_USER", this.operatorId);    // 操作用户
            request.setAttr("OPERATION_TYPE", OPERATION_TYPE);    // 操作类型，0增加密码，1修改密码，3重置密码
            request.setAttr("USER_CODE", user.cust_code);    // 客户名称
            request.setAttr("NEW_AUTH_DATA", user.password);    // 新密码
            request.setAttr("USE_SCOPE", USE_SCOPE);    // 设置交易密码

            // 调用WebService获取返回值
            Response response = this.invoke(request);

            // 判断返回的操作结果是否异常
            if (response.flag != "1")
            {
                string message = "操作失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
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
                string message = "客户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
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
                    string message = "风险等级" + riskLevel + "不存在";
                    logger.Error(message);
                    throw new Exception(message);
            }
            request.setAttr("SURVEY_CELLS", cells);

            // 调用WebService获取返回值
            Response response = this.invoke(request);

            // 判断返回的操作结果是否异常
            if (response.flag != "1")
            {
                string message = "操作失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
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
                                    string ACCT_OPENTYPE = Dict.ACCT_OPENTYPE.客户网上自助
                                    )
        {
            // 前置条件判断
            if (CUST_CODE=="")
            {
                throw new Exception("客户代码不能为空");
            }

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
            Response response = this.invoke(request);

            // 判断返回的操作结果是否异常
            if (response.flag != "1")
            {
                string message = "操作失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }

            // 返回结果
            return response;
        }

        /// <summary>
        /// 查询中登股东账户信息
        /// 实现2.95 新中登实时查询
        /// </summary>
        /// <param name="user">用户信息对象</param>
        /// <param name="ACCT_TYPE">证券账户类别(非必输)11:沪A，21:深A。DD[ACCT_TYPE]</param>
        /// <param name="ACCTBIZ_EXCODE">账户代理业务(非必输)DD[ACCTBIZ_EXCODE]默认为07-证券账户查询</param>
        /// <returns></returns>
        public Response onSearchNewZD(User user,string ACCT_TYPE = "", string ACCTBIZ_EXCODE = Dict.ACCTBIZ_EXCODE.证券账户查询)
        {
            // 前置条件判断
            if (user.user_type=="")
            {
                throw new Exception("用户类型不能为空");
            }
            if (user.user_fname == "")
            {
                throw new Exception("用户全称不能为空");
            }
            if (user.id_type == "")
            {
                throw new Exception("证件类型不能为空");
            }
            if (user.id_code == "")
            {
                throw new Exception("证件编号不能为空");
            }
            if (user.int_org == "")
            {
                throw new Exception("机构代码不能为空");
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "onSearchNewZD");
            request.setAttr("USER_TYPE", user.user_type);
            request.setAttr("CUST_FNAME", user.user_fname);
            request.setAttr("ID_TYPE", user.id_type);
            request.setAttr("ID_CODE", user.id_code);
            request.setAttr("INT_ORG", user.int_org);
            request.setAttr("ACCT_TYPE", ACCT_TYPE); 
            request.setAttr("ACCTBIZ_EXCODE", ACCTBIZ_EXCODE);


            // 调用WebService获取返回值
            Response response = this.invoke(request);

            // 判断返回的操作结果是否异常
            if (response.flag != "1")
            {
                string message = "操作失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }

            // 返回结果
            return response;
        }

        /// <summary>
        /// 查询用户在对应市场的中登账户列表
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ACCT_TYPE">证券账户类别(非必输)11:沪A，21:深A。DD[ACCT_TYPE]</param>
        /// <returns></returns>
        public Response queryStkAcct(User user, string ACCT_TYPE = "")
        {
            // 前置条件判断
            if (user.user_type == "")
            {
                throw new Exception("用户类型不能为空");
            }
            if (user.user_fname == "")
            {
                throw new Exception("用户全称不能为空");
            }
            if (user.id_type == "")
            {
                throw new Exception("证件类型不能为空");
            }
            if (user.id_code == "")
            {
                throw new Exception("证件编号不能为空");
            }
            if (user.int_org == "")
            {
                throw new Exception("机构代码不能为空");
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "onSearchNewZD");
            request.setAttr("USER_TYPE", user.user_type);
            request.setAttr("CUST_FNAME", user.user_fname);
            request.setAttr("ID_TYPE", user.id_type);
            request.setAttr("ID_CODE", user.id_code);
            request.setAttr("INT_ORG", user.int_org);
            request.setAttr("ACCT_TYPE", ACCT_TYPE);
            request.setAttr("ACCTBIZ_EXCODE", Dict.ACCTBIZ_EXCODE.证券账户查询);


            // 调用WebService获取返回值
            Response response = this.invoke(request);

            // 判断返回的操作结果是否异常
            if (response.flag == "1")
            {
                string RTN_ERR_CODE = response.getValue("RTN_ERR_CODE"); // 获取中登返回的错误代码
                if (response.length == 1 && RTN_ERR_CODE == "3031") // 证券账户不存在时清空数据
                {
                    response.prompt = response.getValue("RETURN_MSG");
                    response.empty();
                }
            }
            else if (response.flag == "0" && response.prompt== "中登接口调用失败: 在当前条件下查找不到相应的记录.")
            {
                response.flag = "1";
            }
            else
            {
                string message = "操作失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }

            // 返回结果
            return response;
        }

        /// <summary>
        /// 开立对应市场的股东账号
        /// </summary>
        /// <param name="user">用户信息对象</param>
        /// <param name="ACCT_TYPE">证券账户类别(非必输)11:沪A，21:深A。DD[ACCT_TYPE]</param>
        /// <param name="timeout">超时时间，单位为秒</param>
        /// <returns></returns>
        public Response openStkAcct(User user, string ACCT_TYPE, int timeout = 30)
        {
            // 前置条件判断
            if (user.ymt_code=="")
            {
                throw new Exception("一码通不能为空");
            }

            // 初始化请求

            // 调用WebService获取返回值
            string serialNo = this.submitStkAcctBizOpReq2NewZD(
                OPERATOR_TYPE: Dict.OPERATOR_TYPE.增加,
                ACCTBIZ_EXCODE: Dict.ACCTBIZ_EXCODE.证券账户开立,
                ACCT_TYPE: ACCT_TYPE,
                CUST_CODE: user.cust_code,
                CUST_FNAME: user.user_fname,
                USER_TYPE: user.user_type,
                ID_TYPE: user.id_type,
                ID_CODE: user.id_code,
                ID_ADDR: user.id_addr,
                ID_BEG_DATE: user.id_beg_date,
                ID_EXP_DATE: user.id_exp_date,
                CITIZENSHIP: user.citizenship,
                ADDRESS: user.id_addr,
                MOBILE_TEL: user.mobile_tel,
                TEL: user.tel,
                SEX: user.sex,
                INT_ORG: user.int_org,
                OCCU_TYPE: user.occu_type,
                EDUCATION: user.education,
                ZIP_CODE: user.zip_code,
                CHK_STATUS: Dict.CHK_STATUS.已通过,
                NET_SERVICE: Dict.NET_SERVICE.否,
                YMT_CODE:user.ymt_code,
                BIRTHDAY:user.birthday,
                ACCT_OPENTYPE:Dict.ACCT_OPENTYPE.客户网上自助,
                timeout: timeout
                );

            Response response = searchStkAcctBizInfo(serialNo, Dict.ACCTBIZ_EXCODE.证券账户开立, timeout:timeout);

            // 判断返回的操作结果是否异常
            string RTN_ERR_CODE = response.getValue("RTN_ERR_CODE"); // 获取中登返回的错误代码
            if (response.length == 1 && RTN_ERR_CODE != "0000") // 证券账户开立失败
            {
                throw new Exception("证券账户开立失败：" + response.getValue("RETURN_MSG"));
            }

            // 返回结果
            return response;
        }

        /// <summary>
        /// 创业板签约信息查询
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Response queryCYB(string TRDACCT, int timeout = 30) // TODO:: 测试入参只保留深圳A股账号是否可行？
        {
            // 前置条件判断
            if (TRDACCT == "")
            {
                throw new Exception("深圳股东代码不能为空");
            }

            // 初始化请求

            // 调用WebService获取返回值
            // 发送中登请求
            string serialNo = this.submitStkAcctBizOpReq2NewZD(
                                    OPERATOR_TYPE: Dict.OPERATOR_TYPE.增加,
                                    ACCTBIZ_EXCODE: Dict.ACCTBIZ_EXCODE.适当性管理信息维护,
                                    ACCTBIZ_CLS: Dict.AcctBiz_CLS.创业板_查询,
                                    ACCT_TYPE: Dict.ACCT_TYPE.深市A股账户,
                                    STKBD:Dict.STKBD.深圳A股,
                                    CHK_STATUS: Dict.CHK_STATUS.已通过,
                                    NET_SERVICE: Dict.NET_SERVICE.否,
                                    PROPER_CLS: "",  // 适当性类别必须为空
                                    ACCT_OPENTYPE: "",  // 必须送空值
                                    TRDACCT: TRDACCT    // 深圳A股账号
                                );
            // 获取中登处理结果
            Response rspsStkAcctBizInfo = this.searchStkAcctBizInfo(serialNo,timeout:timeout);

            // 判断返回的操作结果是否异常
            if (rspsStkAcctBizInfo.length == 1 && rspsStkAcctBizInfo.getValue("RTN_ERR_CODE") != "0000")
            {
                throw new Exception("中登返回错误：" + rspsStkAcctBizInfo.getValue("RTN_ERR_CODE") + "，错误信息：" + rspsStkAcctBizInfo.getValue("RETURN_MSG"));
            }

            // 通过扩展信息查询接口获取创业板签署信息
            Response response = this.searchStkAcctBizInfoEx(serialNo);

            return response;
        }

        /// <summary>
        /// 中登开通创业板
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="SIGN_CLS">签约类别</param>
        /// <param name="SIGN_DATE">签约日期</param>
        /// <returns></returns>
        public void openCyb2ZD(User user,string SIGN_CLS, string SIGN_DATE,int timeout=30)
        {
            // 前置条件判断
            if (user.cust_code == "")
            {
                throw new Exception("客户号不能为空");
            }
            if (user.szacct == "")
            {
                throw new Exception("深A账户不能为空");
            }
            if (SIGN_DATE == "")
            {
                throw new Exception("签署日期不能为空");
            }
            if (SIGN_CLS == "")
            {
                throw new Exception("签约类别不能为空");
            }

            // 初始化请求

            // 调用WebService获取返回值
            // 发送中登请求
            string serialNo = this.submitStkAcctBizOpReq2NewZD(
                OPERATOR_TYPE: Dict.OPERATOR_TYPE.增加,
                ACCTBIZ_EXCODE: Dict.ACCTBIZ_EXCODE.适当性管理信息维护,
                PROPER_CLS: Dict.PROPER_CLS.创业板,
                ACCTBIZ_CLS: Dict.AcctBiz_CLS.创业板_开通,
                ACCT_TYPE: Dict.ACCT_TYPE.深市A股账户,
                STKBD: Dict.STKBD.深圳A股,
                CHK_STATUS: Dict.CHK_STATUS.已通过,
                NET_SERVICE: Dict.NET_SERVICE.否,
                ACCT_OPENTYPE: "",  // 必须送空值
                CUST_CODE: user.cust_code,
                YMT_CODE: user.ymt_code,
                TRDACCT: user.szacct,
                SIGN_CLS: SIGN_CLS,
                SIGN_DATE: SIGN_DATE,
                // 以上参数不要随意修改
                CUST_FNAME: user.user_fname,
                USER_TYPE: user.user_type,
                ID_TYPE: user.id_type,
                ID_CODE: user.id_code,
                ID_ADDR: user.id_addr,
                ID_BEG_DATE: user.id_beg_date,
                ID_EXP_DATE: user.id_exp_date,
                CITIZENSHIP: user.citizenship,
                ADDRESS: user.id_addr,
                MOBILE_TEL: user.mobile_tel,
                TEL: user.tel,
                SEX: user.sex,
                INT_ORG: user.int_org,
                OCCU_TYPE: user.occu_type,
                EDUCATION: user.education,
                ZIP_CODE: user.zip_code,
                BIRTHDAY: user.birthday
                );

            // 获取中登处理结果
            Response responseStkAcctBizInfo = this.searchStkAcctBizInfo(serialNo, Dict.ACCTBIZ_EXCODE.适当性管理信息维护, TRDACCT: user.szacct,timeout:timeout);

            // 中登处理是否成功
            if (responseStkAcctBizInfo.flag!="1")
            {
                throw new Exception("中登开通创业板失败：" + responseStkAcctBizInfo.prompt);
            }

            // 返回成功
            // return true;
        }

        /// <summary>
        /// 系统内开通创业板
        /// </summary>
        /// <param name="user"></param>
        /// <param name="OPEN_TYPE">开通类型，见数据字典</param>
        /// <param name="SIGN_DATE">签署日期，空值表示当天</param>
        /// <param name="EFT_DATE">生效日期，空值表示当天</param>
        /// <returns></returns>
        public void openCyb2KBSS(
            User user,
            string OPEN_TYPE,
            string SIGN_DATE="",
            string EFT_DATE=""
        )
        {
            // 前置条件判断
            if (user.cust_code == "")
            {
                throw new Exception("客户号不能为空");
            }
            if (user.szacct == "")
            {
                throw new Exception("深A账户不能为空");
            }
            if (OPEN_TYPE == "")
            {
                throw new Exception("开通类别不能为空");
            }
            if (SIGN_DATE == "")
            {
                throw new Exception("签署日期不能为空");
            }
            if (EFT_DATE == "")
            {
                throw new Exception("生效日期不能为空");
            }

            // 初始化请求

            // 调用WebService获取返回值
            Response response = this.setCustAgreement(
                OPERATION_TYPE: "0",
                CUST_CODE: user.cust_code,
                CUACCT_CODE: user.cuacct_code,
                TRDACCT: user.szacct,
                CUST_AGMT_TYPE: Dict.CUST_AGMT_TYPE.创业板协议,
                STKBD: Dict.STKBD.深圳A股,
                OPEN_TYPE: Dict.OPEN_TYPE.T加5,
                SIGN_PLACE: Dict.SIGN_PLACE.本证券公司,
                REDO_FLAG: "",
                SIGN_DATE: SIGN_DATE,
                EFT_DATE: EFT_DATE
            );

            if (response.flag!="1")
            {
                throw new Exception("创业板协议签署失败：" + response.prompt);
            }

            // 返回结果
            // return true;
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
                string message = "单一公共参数查询时返回值不能多于1个";
                logger.Error(message);
                throw new Exception(message);
            }

            return response.getValue("REGKEY_VAL");
        }

        /// <summary>
        /// 加挂上海股东户
        /// </summary>
        /// <param name="user"></param>
        public bool registerSHAStkTrdAcct(User user)
        {
            try
            {
                openStkTrdAcct(user, Dict.STKBD.上海A股, user.shacct, "上海A股");
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 加挂深圳股东户
        /// </summary>
        /// <param name="user"></param>
        public bool registerSZAStkTrdAcct(User user)
        {
            try
            {
                openStkTrdAcct(user, Dict.STKBD.深圳A股, user.szacct, "深圳A股");
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 公共参数查询
        /// </summary>
        /// <param name="regkeyId"></param>
        /// <returns></returns>
        private Response getCommonParams(string regkeyId)
        {
            Request request = new Request(this.operatorId, "getCommonParams");
            request.setAttr("REGKEY_ID", regkeyId);

            Response response = this.invoke(request);

            if (response.flag != "1")
            {
                string message = "查询公共参数" + regkeyId + "失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
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
                string message = "字典项名称不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            Request request = new Request(this.operatorId, "getDictData");
            request.setAttr("DD_ID", dictName);
            request.setAttr("INT_ORG", "0");

            Response response = this.invoke(request);

            if(response.flag != "1")
            {
                string message = "查询数据字典" + dictName + "失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }

            return response;
        }

        /// <summary>
        /// 根据身份证号查询已有客户代码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Response getUserInfoById(string id)
        {
            Request request = new Request(this.operatorId, "getUserInfoById");
            request.setAttr("ID_CODE", id);

            Response response = this.invoke(request);
            return response;
        }

        /// <summary>
        /// 2.14 公安联网校验查询
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        public Response validateIdCode(User user)
        {
            // 前置条件判断
            if (user.id_code == "")
            {
                throw new Exception("证件代码不能为空");
            }
            if (user.user_fname == "")
            {
                throw new Exception("用户全称不能为空");
            }
            if (user.birthday == "")
            {
                throw new Exception("出生日期不能为空");
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "validateIdCode");
            request.setAttr("WEB_BIZ", Dict.WEB_BIZ.网上自助);
            request.setAttr("USER_CODE", this.operatorId);
            request.setAttr("USER_NAME", user.user_fname);
            request.setAttr("REQFLAG", "0");
            request.setAttr("ID_CODE", user.id_code);
            request.setAttr("NATIONALITY", user.nationality);
            request.setAttr("BIRTHDAY", user.birthday);
            
            // 调用WebService获取返回值
            Response response = this.invoke(request);

            // 判断返回的操作结果是否异常
            if (response.flag != "1") 
            {
                throw new Exception("公安校验失败：" + response.prompt);
            }

            // 返回结果
            return response;
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        // The bulk of the clean-up code is implemented in Dispose(bool)  
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources  
                this.kessClientType.GetMethod("Close").Invoke(this.kessClient, new object[] { });
            }
            // free native resources if there are any. 
        }

        /// <summary>
        /// 机构交易单元查询
        /// 实现2.56 机构交易单元查询
        /// </summary>
        /// <param name="STKBD">交易版块DD[STKBD]</param>
        /// <param name="INT_ORG">内部机构</param>
        /// <param name="STKPBU_TYPE">交易单元类型，默认为0：普通交易单元</param>
        /// <returns></returns>
        public Response listStkPbuOrg(string STKBD, string INT_ORG, string STKPBU_TYPE = "0")
        {
            // 前置条件判断
            if (STKBD == "")
            {
                string message = "交易板块不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (INT_ORG == "")
            {
                string message = "内部机构不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (INT_ORG == "")
            {
                string message = "交易单元类型不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "listStkPbuOrg");
            request.setAttr("STKBD", STKBD); // 交易版块DD[STKBD]
            request.setAttr("INT_ORG", INT_ORG); // 内部机构
            request.setAttr("STKPBU_TYPE", STKPBU_TYPE); // 交易单元类型

            // 调用WebService获取返回值
            Response response = this.invoke(request);

            // 判断返回的操作结果是否异常
            if (response.flag != "1")
            {
                string message = "操作失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }

            // 返回结果
            return response;
        }
        /// <summary>
        /// 证券账户指定交易
        /// </summary>
        /// <param name="CUST_CODE">客户代码</param>
        /// <param name="STKPBU">交易单元</param>
        /// <param name="STKBD">交易板块DD[STKBD]</param>
        /// <param name="TRDACCT">交易账户</param>
        /// <param name="TREG_STATUS">交易指定状态0-撤指定1-指定</param>
        /// <param name="BREG_STATUS">交易指定状态0-撤指定1-指定</param>
        /// <param name="FIRMID">代理商号</param>
        /// <returns></returns>
        public bool stkTrdacctBind(
            string CUST_CODE,
            string STKPBU,
            string STKBD,
            string TRDACCT,
            string TREG_STATUS,
            string BREG_STATUS = "",
            string FIRMID = ""
            )
        {
            // 前置条件判断
            if (CUST_CODE == "")
            {
                string message = "客户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (STKPBU == "")
            {
                string message = "交易单元不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (STKBD == "")
            {
                string message = "交易板块不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (TRDACCT == "")
            {
                string message = "交易账户不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (TREG_STATUS == "")
            {
                string message = "交易指定状态TREG_STATUS不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "stkTrdacctBind");
            request.setAttr("CUST_CODE", CUST_CODE); // 客户代码
            request.setAttr("STKBD", STKBD); // 交易板块DD[STKBD]
            request.setAttr("STKPBU", STKPBU); // 交易单元
            request.setAttr("TRDACCT", TRDACCT); // 交易账户
            request.setAttr("TREG_STATUS", TREG_STATUS); // 交易指定状态0-撤指定1-指定
            request.setAttr("BREG_STATUS", BREG_STATUS); // 交易指定状态0-撤指定1-指定
            request.setAttr("FIRMID", FIRMID); // 代理商号

            // 调用WebService获取返回值
            Response response = this.invoke(request);

            // 判断返回的操作结果是否异常
            if (response.flag != "1")
            {
                string message = "操作失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }

            // 返回结果
            return true;
        }

        /// <summary>
        /// 2.61 券商发起银证开户、预指定和存管签约
        /// </summary>
        /// <param name="OP_TYPE">操作类型OP_TYPE为0时表示券商发起银证开户一步式，为1时表示券商发起预指定，即两步式中的第一步，为2时BANK_ACCT、FUND_AUTH_DATA、BANK_AUTH_DATA均传空。</param>
        /// <param name="CURRENCY">货币代码DD[CURRENCY]</param>
        /// <param name="CUST_CODE">客户代码</param>
        /// <param name="CUACCT_CODE">资金代码</param>
        /// <param name="BANK_ACCT_CODE">银行账户卡号</param>
        /// <param name="EXT_ORG">外部机构</param>
        /// <param name="BANK_ACCT">外部银行账户</param>
        /// <param name="FUND_AUTH_DATA">资金密码</param>
        /// <param name="CUBSB_TYPE">银证业务类型DD[CUBSB_TYPE]</param>
        /// <param name="BANK_AUTH_DATA">银行密码</param>
        /// <param name="SERIAL_NO">流水序号</param>
        /// <param name="SMS_NO">短信验证码</param>
        /// <returns></returns>
        public bool cubsbScOpenAcct(
                string OP_TYPE,
                string CUACCT_CODE,
                string EXT_ORG,
                string CUST_CODE = "",
                string BANK_ACCT_CODE = "",
                string BANK_ACCT = "",
                string FUND_AUTH_DATA = "",
                string CUBSB_TYPE = Dict.CUBSB_TYPE.存管,
                string BANK_AUTH_DATA = "",
                string SERIAL_NO = "",
                string CURRENCY = Dict.CURRENCY.人民币,
                string SMS_NO = ""
            )
        {
            // 前置条件判断
            if (OP_TYPE == "")
            {
                string message = "操作类型不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (EXT_ORG == "")
            {
                string message = "银行代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (CUACCT_CODE == "")
            {
                string message = "资金代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (CURRENCY == "")
            {
                string message = "货币类型不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "cubsbScOpenAcct");
            request.setAttr("OP_TYPE", OP_TYPE); // 操作类型
            request.setAttr("CURRENCY", CURRENCY); // 货币代码DD[CURRENCY]
            request.setAttr("CUST_CODE", CUST_CODE); // 客户代码
            request.setAttr("CUACCT_CODE", CUACCT_CODE); // 资金代码
            request.setAttr("BANK_ACCT_CODE", BANK_ACCT_CODE); // 银行账户卡号
            request.setAttr("EXT_ORG", EXT_ORG); // 外部机构
            request.setAttr("BANK_ACCT", BANK_ACCT); // 外部银行账户
            request.setAttr("FUND_AUTH_DATA", FUND_AUTH_DATA); // 资金密码
            request.setAttr("CUBSB_TYPE", CUBSB_TYPE); // 银证业务类型DD[CUBSB_TYPE]
            request.setAttr("BANK_AUTH_DATA", BANK_AUTH_DATA); // 银行密码
            request.setAttr("SERIAL_NO", SERIAL_NO); // 流水序号
            request.setAttr("SMS_NO", SMS_NO); // 短信验证码


            // 调用WebService获取返回值
            Response response = this.invoke(request);

            // 判断返回的操作结果是否异常
            if (response.flag != "1")
            {
                string message = "操作失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }

            // 返回结果
            return true;
        }
    }
}
