using System;
using System.Threading.Tasks;

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
        /// 同时发起的WebService请求的最大数量，超过则必须等待
        /// </summary>
        public int maxConnections = 5;

        /// <summary>
        /// 当前并发数量
        /// </summary>
        public int activeConnectionsNum
        {
            get
            {
                return _webserviceConnectionsNum;
            }
        }

        /// <summary>
        /// 提交请求时，如果操作员已经退出，是否自动重新登录
        /// </summary>
        public bool autoRelogin {
            set
            {
                _autoRelogin = value;
            }
            get
            {
                return _autoRelogin;
            }
        }

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

            this.CreateInstance();
        }

        /// <summary>
        /// 请求队列长度
        /// </summary>
        public int requestQueueCount
        {
            get
            {
                return _requestQueueCount;
            }
        }

        /// <summary>
        /// 操作员登录
        /// 登陆成功返回true，登录失败抛出异常
        /// </summary>
        /// <returns></returns>
        async public Task<bool> operatorLogin(string operatorId = "", string password = "")
        {
            this.CreateInstance();

            // 开始登陆
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

            Response response = await this.invoke(request);

            if (response.flag == "0" && response.prompt.IndexOf("用户已登陆，不用重复登陆") == -1)
            {
                string message = "用户登录失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }

            return true;
        }

        /// <summary>
        /// 操作员退出
        /// 退出成功返回true，退出失败抛出异常
        /// </summary>
        /// <returns></returns>
        async public Task<bool> operatorLogout()
        {
            Request request = new Request(this.operatorId, "operatorLogout");
            Response response = await this.invoke(request);

            if (response.flag == "0")
            {
                string message = "用户退出失败：" + response.prompt;
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
        async public Task<string> createCustomerCode(User user)
        {
            Response response = await this.getUserInfoById(user.id_code);
            if (response.length == 0 || await this.getSingleCommonParamValue("OPEN_CUST_CHECK_ID_FLAG") == "1")
            {
                response = await this.openCustomer(user);
                string usercode = response.getValue("USER_CODE");
                // response = await this;
                return usercode;
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
        async public Task<string> openCuacctCode(User user)
        {
            Response response = await this.listCuacct(user.cust_code);
            // 判断是否已经开立过资金账号
            if (response.length == 0)
            {
                bool useUserCodeAsCuacctCode = await this.getSingleCommonParamValue("CUST_CUACCT_SHARE_SERIAL") == "1" ? true : false;
                if (useUserCodeAsCuacctCode)
                {
                    user.cuacct_code = user.cust_code;
                }
                response = await this.openCuacct(USER_CODE: user.cust_code, CUACCT_CLS: user.cuacct_cls, INT_ORG: user.int_org);
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
        /// <param name="cust_code">客户号</param>
        /// <returns></returns>
        async public Task<Response> listCuacct(string cust_code)
        {
            if (cust_code == "")
            {
                string message = "用户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            Request request = new Request(this.operatorId, "listCuacct");
            request.setAttr("USER_CODE", cust_code);    // 客户名称

            Response response = await this.invoke(request);
            if (response.flag != "0" && response.flag != "1")
            {
                string msg = "操作失败：" + response.prompt;
                logger.Error(msg);
                throw new NotImplementedException(msg);
            }
            return response;
        }

        async public Task bankSign(User user)
        {
            await this.cubsbScOpenAcctOneStep(user.cust_code, user.cuacct_code, user.bank_acct_code);
        }

        /// <summary>
        /// 管理用户密码
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="OPERATION_TYPE">操作类型，0增加密码，1修改密码，3重置密码</param>
        /// <returns></returns>
        async public Task<bool> mdfUserPassword(User user, string USE_SCOPE, string OPERATION_TYPE = "0")
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
            Response response = await this.invoke(request);

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
        /// <param name="user">用户信息对象</param>
        /// <param name="SURVEY_COLS">问题序号列字符串</param>
        /// <param name="SURVEY_CELLS">答案序号列字符串</param>
        /// <returns></returns>
        async public Task<bool> syncSurveyAns2Kbss(User user, string SURVEY_COLS, string SURVEY_CELLS, string SURVEY_SN = "1")
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
            request.setAttr("SURVEY_SN", SURVEY_SN);
            request.setAttr("SURVEY_COLS", SURVEY_COLS);
            request.setAttr("SURVEY_CELLS", SURVEY_CELLS);

            // 调用WebService获取返回值
            Response response = await this.invoke(request);

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
        async public Task<Response> openYMTAcct(string USER_TYPE,
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
            if (CUST_CODE == "")
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
            Response response = await this.invoke(request);

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
        async public Task<Response> onSearchNewZD(User user, string ACCT_TYPE = "", string ACCTBIZ_EXCODE = Dict.ACCTBIZ_EXCODE.证券账户查询)
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
            request.setAttr("ACCTBIZ_EXCODE", ACCTBIZ_EXCODE);


            // 调用WebService获取返回值
            Response response = await this.invoke(request);

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
        /// <param name="USER_TYPE">用户类型（必传）DD[USER_TYPE]</param>
        /// <param name="CUST_FNAME">客户名称（必传）</param>
        /// <param name="ID_TYPE">证件类型（必传）</param>
        /// <param name="ID_CODE">证件号码（必传）</param>
        /// <param name="INT_ORG">机构代码（必传）</param>
        /// <param name="ACCTBIZ_EXCODE">账户代理业务(非必输)DD[ACCTBIZ_EXCODE]默认为07-证券账户查询</param>
        /// <param name="ACCT_TYPE">证券账户类别(非必输)11:沪A，21:深A。DD[ACCT_TYPE]</param>
        /// <returns></returns>
        async public Task<Response> queryStkAcct(
                    string USER_TYPE = "", //用户类型（必传）DD[USER_TYPE]
                    string CUST_FNAME = "", //客户名称（必传）
                    string ID_TYPE = "", //证件类型（必传）
                    string ID_CODE = "", //证件号码（必传）
                    string INT_ORG = "", //机构代码（必传）
                    string ACCT_TYPE = "" //证券账户类别(非必输)DD[ACCT_TYPE]
            )
        {
            // 前置条件判断
            if (USER_TYPE == "")
            {
                throw new Exception("用户类型不能为空");
            }
            if (CUST_FNAME == "")
            {
                throw new Exception("用户全称不能为空");
            }
            if (ID_TYPE == "")
            {
                throw new Exception("证件类型不能为空");
            }
            if (ID_CODE == "")
            {
                throw new Exception("证件编号不能为空");
            }
            if (INT_ORG == "")
            {
                throw new Exception("机构代码不能为空");
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "onSearchNewZD");
            request.setAttr("USER_TYPE", USER_TYPE); //用户类型（必传）DD[USER_TYPE]
            request.setAttr("CUST_FNAME", CUST_FNAME); //客户名称（必传）
            request.setAttr("ID_TYPE", ID_TYPE); //证件类型（必传）
            request.setAttr("ID_CODE", ID_CODE); //证件号码（必传）
            request.setAttr("INT_ORG", INT_ORG); //机构代码（必传）
            request.setAttr("ACCTBIZ_EXCODE", Dict.ACCTBIZ_EXCODE.证券账户查询); //账户代理业务(非必输)DD[ACCTBIZ_EXCODE]默认为07-证券账户查询
            request.setAttr("ACCT_TYPE", ACCT_TYPE); //证券账户类别(非必输)DD[ACCT_TYPE]


            // 调用WebService获取返回值
            Response response = await this.invoke(request);

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
            else if (response.flag == "0" && response.prompt == "中登接口调用失败: 在当前条件下查找不到相应的记录.")
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
        async public Task<Response> openStkAcct(User user, string ACCT_TYPE, int timeout = 30)
        {
            // 前置条件判断
            if (user.ymt_code == "")
            {
                throw new Exception("一码通不能为空");
            }

            // 初始化请求

            // 调用WebService获取返回值
            string serialNo = await this.submitStkAcctBizOpReq2NewZD(
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
                YMT_CODE: user.ymt_code,
                BIRTHDAY: user.birthday,
                ACCT_OPENTYPE: Dict.ACCT_OPENTYPE.客户网上自助,
                timeout: timeout
                );

            Response response = await searchStkAcctBizInfo(serialNo, Dict.ACCTBIZ_EXCODE.证券账户开立, timeout: timeout);

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
        async public Task<Response> queryCYB(string TRDACCT, int timeout = 30)
        {
            // 前置条件判断
            if (TRDACCT == "")
            {
                throw new Exception("深圳股东代码不能为空");
            }

            // 初始化请求

            // 调用WebService获取返回值
            // 发送中登请求
            string serialNo = await this.submitStkAcctBizOpReq2NewZD(
                                    OPERATOR_TYPE: Dict.OPERATOR_TYPE.增加,
                                    ACCTBIZ_EXCODE: Dict.ACCTBIZ_EXCODE.适当性管理信息维护,
                                    ACCTBIZ_CLS: Dict.AcctBiz_CLS.创业板_查询,
                                    ACCT_TYPE: Dict.ACCT_TYPE.深市A股账户,
                                    STKBD: Dict.STKBD.深圳A股,
                                    CHK_STATUS: Dict.CHK_STATUS.已通过,
                                    NET_SERVICE: Dict.NET_SERVICE.否,
                                    PROPER_CLS: "",  // 适当性类别必须为空
                                    ACCT_OPENTYPE: "",  // 必须送空值
                                    TRDACCT: TRDACCT    // 深圳A股账号
                                );
            // 获取中登处理结果
            Response rspsStkAcctBizInfo = await this.searchStkAcctBizInfo(serialNo, timeout: timeout);

            // 判断返回的操作结果是否异常
            if (rspsStkAcctBizInfo.length == 1 && rspsStkAcctBizInfo.getValue("RTN_ERR_CODE") != "0000")
            {
                throw new Exception("中登返回错误：" + rspsStkAcctBizInfo.getValue("RTN_ERR_CODE") + "，错误信息：" + rspsStkAcctBizInfo.getValue("RETURN_MSG"));
            }

            // 通过扩展信息查询接口获取创业板签署信息
            Response response = await this.searchStkAcctBizInfoEx(serialNo);

            return response;
        }

        /// <summary>
        /// 中登开通创业板
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="SIGN_CLS">签约类别</param>
        /// <param name="SIGN_DATE">签约日期</param>
        /// <returns></returns>
        async public Task openCyb2ZD(User user, string SIGN_CLS, string SIGN_DATE, int timeout = 30)
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
            string serialNo = await this.submitStkAcctBizOpReq2NewZD(
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
            Response responseStkAcctBizInfo = await this.searchStkAcctBizInfo(serialNo, Dict.ACCTBIZ_EXCODE.适当性管理信息维护, TRDACCT: user.szacct, timeout: timeout);

            // 中登处理是否成功
            if (responseStkAcctBizInfo.flag != "1")
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
        async public Task openCyb2KBSS(
            User user,
            string OPEN_TYPE,
            string SIGN_DATE = "",
            string EFT_DATE = ""
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
            Response response = await this.setCustAgreement(
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

            if (response.flag != "1")
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
        async public Task<string> getSingleCommonParamValue(string regkeyId)
        {
            Response response = await this.getCommonParams(regkeyId);

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
        public async Task<bool> registerSHAStkTrdAcct(User user)
        {
            try
            {
                await openStkTrdAcct(user, Dict.STKBD.上海A股, user.shacct, "上海A股");
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
        public async Task<bool> registerSZAStkTrdAcct(User user)
        {
            try
            {
                await openStkTrdAcct(user, Dict.STKBD.深圳A股, user.szacct, "深圳A股");
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
        async private Task<Response> getCommonParams(string regkeyId)
        {
            Request request = new Request(this.operatorId, "getCommonParams");
            request.setAttr("REGKEY_ID", regkeyId);

            Response response = await this.invoke(request);

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
        async public Task<Response> getDictData(string dictName)
        {
            if (dictName == "")
            {
                string message = "字典项名称不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            Request request = new Request(this.operatorId, "getDictData");
            request.setAttr("DD_ID", dictName);
            request.setAttr("INT_ORG", "0");

            Response response = await this.invoke(request);

            if (response.flag != "1")
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
        async public Task<Response> getUserInfoById(string id)
        {
            Request request = new Request(this.operatorId, "getUserInfoById");
            request.setAttr("ID_CODE", id);

            Response response = await this.invoke(request);
            return response;
        }

        /// <summary>
        /// 2.14 公安联网校验查询
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        async public Task<Response> validateIdCode(User user)
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
            Response response = await this.invoke(request);

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
                for (int i = 0; i < kessClientList.Count; i++)
                {
                    this.kessClientType.GetMethod("Close").Invoke(this.kessClientList[i].executor, new object[] { });
                }
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
        async public Task<Response> listStkPbuOrg(string STKBD, string INT_ORG, string STKPBU_TYPE = "0")
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
            Response response = await this.invoke(request);

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
        async public Task<bool> stkTrdacctBind(
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
            Response response = await this.invoke(request);

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
        async public Task<bool> cubsbScOpenAcct(
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
            request.setAttr("OP_TYPE", OP_TYPE); //操作类型OP_TYPE为0时表示券商发起银证开户一步式，为1时表示券商发起预指定，即两步式中的第一步，为2时BANK_ACCT、FUND_AUTH_DATA、BANK_AUTH_DATA均传空。
            request.setAttr("CURRENCY", CURRENCY); //货币代码DD[CURRENCY]
            request.setAttr("CUST_CODE", CUST_CODE); //客户代码
            request.setAttr("CUACCT_CODE", CUACCT_CODE); //资金代码
            request.setAttr("BANK_ACCT_CODE", BANK_ACCT_CODE); //银行账户卡号
            request.setAttr("EXT_ORG", EXT_ORG); //外部机构
            request.setAttr("BANK_ACCT", BANK_ACCT); //外部银行账户
            request.setAttr("FUND_AUTH_DATA", FUND_AUTH_DATA); //资金密码
            request.setAttr("CUBSB_TYPE", CUBSB_TYPE); //银证业务类型DD[CUBSB_TYPE]
            request.setAttr("BANK_AUTH_DATA", BANK_AUTH_DATA); //银行密码
            request.setAttr("SERIAL_NO", SERIAL_NO); //流水序号
            request.setAttr("SMS_NO", SMS_NO); //短信验证码
            
            // 调用WebService获取返回值
            Response response = await this.invoke(request);

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
        /// 2.58 用户扩展信息维护
        /// </summary>
        /// <param name="CUST_CODE">客户代码</param>
        /// <param name="OPERATION_TYPE">操作类型0—增加，1—修改，2—删除。</param>
        /// <param name="WORKPLACE">工作地点</param>
        /// <param name="INCOME">家庭收入</param>
        /// <param name="TRADE">行业类型DD-[CIF_TRADE]</param>
        /// <param name="VOCATION">职业通过数据指点查询具体值</param>
        /// <param name="PRACTICE_DATE">从业日期</param>
        /// <param name="INDUS_TYPE">行业代码DD-[INDUS_TYPE]</param>
        /// <param name="CERTIFICATE_TYPE">资格证书种类</param>
        /// <param name="CERTIFICATE_NO">资格证书号</param>
        /// <param name="OCCU_TYPE">职业代码</param>
        /// <param name="POSITION">职位</param>
        /// <param name="WORKING_TEL">公司电话</param>
        /// <param name="WORKING_FAX">公司传真</param>
        /// <param name="INDUS_GB">国标行业大类</param>
        /// <param name="INDUS_GB_SUB">国标行业子类</param>
        /// <param name="OCCU_GB">国标职业大类</param>
        /// <param name="OCCU_GB_SUB">国标职业子类</param>
        /// <param name="OCCUPATION">手输职业</param>
        /// <param name="LINKMAN_NO">联系人编号</param>
        /// <param name="LINKMAN">第二联系人姓名</param>
        /// <param name="LINKMAN_ID_TYPE">证件类别</param>
        /// <param name="LINKMAN_ID">证件号码</param>
        /// <param name="LINKMAN_TEL">第二联系人电话</param>
        /// <param name="LINKMAN_EMAIL">EMAIL地址</param>
        /// <param name="LINKMAN_ADDR">通信地址</param>
        /// <param name="LINKMAN_ZIP">邮政编码</param>
        /// <param name="IME_TYPE"></param>
        /// <param name="IME_NAME"></param>
        /// <param name="IS_SALE_CUST">营销确认</param>
        /// <param name="DELIVER_FLAG">邮寄对账单</param>
        /// <param name="SMS_SERVICE">短信服务</param>
        /// <param name="WLT_CARD_NO">卡号</param>
        /// <param name="COMMENDER_NAME">推荐人姓名</param>
        /// <param name="COMMENDER_ID_TYPE">推荐人证件类型</param>
        /// <param name="COMMENDER_ID_NO">推荐人证件号码</param>
        /// <param name="EN_PRODUCT_CODE">拥有本公司以外的产品</param>
        /// <param name="QUALITY_TYPE">品质类型</param>
        /// <param name="PRIVACY_LEVEL">隐私级别DD[PRIVACY_LEVEL]</param>
        /// <param name="OPEN_ORDER_ID">开户合同号</param>
        /// <param name="BSB_ID_TYPE">签约客户证件类型</param>
        /// <param name="BSB_ID_CODE">签约客户证件号码</param>
        /// <param name="BSB_ID_EXP_DATE">签约证件有效期</param>
        /// <param name="BSB_USER_FNAME">签约客户姓名</param>
        /// <param name="APPT_SOURCE">预约来源</param>
        /// <param name="ACCT_MANAGER">客户经理</param>
        /// <param name="LINGKINDS_ORDER">首选联系人方式</param>
        /// <param name="FISL_EMAIL">信用电邮</param>
        /// <param name="SALESSTAFF_CODE">营销人员编码</param>
        /// <param name="SALESSTAFF_NAME">营销人员姓名</param>
        /// <param name="OTHER_REMARK">其他备注</param>
        /// <param name="SPEC_REMARK">操作/特殊备注</param>
        /// <returns></returns>
        async public Task<bool> mdfUserExtInfo(
            string CUST_CODE = "", //客户代码 
            string OPERATION_TYPE = "", //操作类型0—增加，1—修改，2—删除。 
            string WORKPLACE = "", //工作地点 
            string INCOME = "", //家庭收入 
            string TRADE = "", //行业类型DD-[CIF_TRADE] 
            string VOCATION = "", //职业通过数据指点查询具体值 
            string PRACTICE_DATE = "", //从业日期 
            string INDUS_TYPE = "", //行业代码DD-[INDUS_TYPE] 
            string CERTIFICATE_TYPE = "", //资格证书种类 
            string CERTIFICATE_NO = "", //资格证书号 
            string OCCU_TYPE = "", //职业代码 
            string POSITION = "", //职位 
            string WORKING_TEL = "", //公司电话 
            string WORKING_FAX = "", //公司传真 
            string INDUS_GB = "", //国标行业大类 
            string INDUS_GB_SUB = "", //国标行业子类 
            string OCCU_GB = "", //国标职业大类 
            string OCCU_GB_SUB = "", //国标职业子类 
            string OCCUPATION = "", //手输职业 
            string LINKMAN_NO = "", //联系人编号 
            string LINKMAN = "", //第二联系人姓名 
            string LINKMAN_ID_TYPE = "", //证件类别 
            string LINKMAN_ID = "", //证件号码 
            string LINKMAN_TEL = "", //第二联系人电话 
            string LINKMAN_EMAIL = "", //EMAIL地址 
            string LINKMAN_ADDR = "", //通信地址 
            string LINKMAN_ZIP = "", //邮政编码 
            string IME_TYPE = "", // 
            string IME_NAME = "", // 
            string IS_SALE_CUST = "", //营销确认 
            string DELIVER_FLAG = "", //邮寄对账单 
            string SMS_SERVICE = "", //短信服务 
            string WLT_CARD_NO = "", //卡号 
            string COMMENDER_NAME = "", //推荐人姓名 
            string COMMENDER_ID_TYPE = "", //推荐人证件类型 
            string COMMENDER_ID_NO = "", //推荐人证件号码 
            string EN_PRODUCT_CODE = "", //拥有本公司以外的产品 
            string QUALITY_TYPE = "", //品质类型 
            string PRIVACY_LEVEL = "", //隐私级别DD[PRIVACY_LEVEL] 
            string OPEN_ORDER_ID = "", //开户合同号 
            string BSB_ID_TYPE = "", //签约客户证件类型 
            string BSB_ID_CODE = "", //签约客户证件号码 
            string BSB_ID_EXP_DATE = "", //签约证件有效期 
            string BSB_USER_FNAME = "", //签约客户姓名 
            string APPT_SOURCE = "", //预约来源 
            string ACCT_MANAGER = "", //客户经理 
            string LINGKINDS_ORDER = "", //首选联系人方式 
            string FISL_EMAIL = "", //信用电邮 
            string SALESSTAFF_CODE = "", //营销人员编码 
            string SALESSTAFF_NAME = "", //营销人员姓名 
            string OTHER_REMARK = "", //其他备注 
            string SPEC_REMARK = "" //操作/特殊备注 
        )
        {
            // 前置条件判断
            if (CUST_CODE == "")
            {
                string message = "客户号不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (OPERATION_TYPE == "")
            {
                string message = "操作类型不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "mdfUserExtInfo");
            request.setAttr("CUST_CODE", CUST_CODE); //客户代码
            request.setAttr("OPERATION_TYPE", OPERATION_TYPE); //操作类型0—增加，1—修改，2—删除。
            request.setAttr("WORKPLACE", WORKPLACE); //工作地点
            request.setAttr("INCOME", INCOME); //家庭收入
            request.setAttr("TRADE", TRADE); //行业类型DD-[CIF_TRADE]
            request.setAttr("VOCATION", VOCATION); //职业通过数据指点查询具体值
            request.setAttr("PRACTICE_DATE", PRACTICE_DATE); //从业日期
            request.setAttr("INDUS_TYPE", INDUS_TYPE); //行业代码DD-[INDUS_TYPE]
            request.setAttr("CERTIFICATE_TYPE", CERTIFICATE_TYPE); //资格证书种类
            request.setAttr("CERTIFICATE_NO", CERTIFICATE_NO); //资格证书号
            request.setAttr("OCCU_TYPE", OCCU_TYPE); //职业代码
            request.setAttr("POSITION", POSITION); //职位
            request.setAttr("WORKING_TEL", WORKING_TEL); //公司电话
            request.setAttr("WORKING_FAX", WORKING_FAX); //公司传真
            request.setAttr("INDUS_GB", INDUS_GB); //国标行业大类
            request.setAttr("INDUS_GB_SUB", INDUS_GB_SUB); //国标行业子类
            request.setAttr("OCCU_GB", OCCU_GB); //国标职业大类
            request.setAttr("OCCU_GB_SUB", OCCU_GB_SUB); //国标职业子类
            request.setAttr("OCCUPATION", OCCUPATION); //手输职业
            request.setAttr("LINKMAN_NO", LINKMAN_NO); //联系人编号
            request.setAttr("LINKMAN", LINKMAN); //第二联系人姓名
            request.setAttr("LINKMAN_ID_TYPE", LINKMAN_ID_TYPE); //证件类别
            request.setAttr("LINKMAN_ID", LINKMAN_ID); //证件号码
            request.setAttr("LINKMAN_TEL", LINKMAN_TEL); //第二联系人电话
            request.setAttr("LINKMAN_EMAIL", LINKMAN_EMAIL); //EMAIL地址
            request.setAttr("LINKMAN_ADDR", LINKMAN_ADDR); //通信地址
            request.setAttr("LINKMAN_ZIP", LINKMAN_ZIP); //邮政编码
            request.setAttr("IS_SALE_CUST", IS_SALE_CUST); //营销确认
            request.setAttr("DELIVER_FLAG", DELIVER_FLAG); //邮寄对账单
            request.setAttr("SMS_SERVICE", SMS_SERVICE); //短信服务
            request.setAttr("WLT_CARD_NO", WLT_CARD_NO); //卡号
            request.setAttr("COMMENDER_NAME", COMMENDER_NAME); //推荐人姓名
            request.setAttr("COMMENDER_ID_TYPE", COMMENDER_ID_TYPE); //推荐人证件类型
            request.setAttr("COMMENDER_ID_NO", COMMENDER_ID_NO); //推荐人证件号码
            request.setAttr("EN_PRODUCT_CODE", EN_PRODUCT_CODE); //拥有本公司以外的产品
            request.setAttr("QUALITY_TYPE", QUALITY_TYPE); //品质类型
            request.setAttr("PRIVACY_LEVEL", PRIVACY_LEVEL); //隐私级别DD[PRIVACY_LEVEL]
            request.setAttr("OPEN_ORDER_ID", OPEN_ORDER_ID); //开户合同号
            request.setAttr("BSB_ID_TYPE", BSB_ID_TYPE); //签约客户证件类型
            request.setAttr("BSB_ID_CODE", BSB_ID_CODE); //签约客户证件号码
            request.setAttr("BSB_ID_EXP_DATE", BSB_ID_EXP_DATE); //签约证件有效期
            request.setAttr("BSB_USER_FNAME", BSB_USER_FNAME); //签约客户姓名
            request.setAttr("APPT_SOURCE", APPT_SOURCE); //预约来源
            request.setAttr("ACCT_MANAGER", ACCT_MANAGER); //客户经理
            request.setAttr("LINGKINDS_ORDER", LINGKINDS_ORDER); //首选联系人方式
            request.setAttr("FISL_EMAIL", FISL_EMAIL); //信用电邮
            request.setAttr("SALESSTAFF_CODE", SALESSTAFF_CODE); //营销人员编码
            request.setAttr("SALESSTAFF_NAME", SALESSTAFF_NAME); //营销人员姓名
            request.setAttr("OTHER_REMARK", OTHER_REMARK); //其他备注
            request.setAttr("SPEC_REMARK", SPEC_REMARK); //操作/特殊备注


            // 调用WebService获取返回值
            Response response = await this.invoke(request);

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
        /// 证券账户指定交易
        /// </summary>
        /// <param name="USER_CODE">客户代码，必要参数</param>
        /// <param name="CUACCT_CODE">资产账户</param>
        /// <returns></returns>
        async public Task<Response> queryAccountInfo(
            string USER_CODE, //客户代码
            string CUACCT_CODE = "" //资产账户
            )
        {
            // 前置条件判断
            if (USER_CODE == "")
            {
                string message = "客户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "queryAccountInfo");
            request.setAttr("USER_CODE", USER_CODE); //客户代码必要参数
            request.setAttr("CUACCT_CODE", CUACCT_CODE); //资产账户

            // 调用WebService获取返回值
            Response response = await this.invoke(request);

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
        /// 一码通信息查询
        /// </summary>
        /// <param name="id_code">身份证号码</param>
        /// <param name="cust_name">姓名</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        async public Task<Response> queryYMT(string id_code, string cust_name, int timeout = 30)
        {
            // 前置条件判断
            if (id_code == "")
            {
                throw new Exception("身份证号码不能为空");
            }

            // 初始化请求

            // 调用WebService获取返回值
            // 发送中登请求
            string serialNo = await this.submitStkAcctBizOpReq2NewZD(
                                    OPERATOR_TYPE: Dict.OPERATOR_TYPE.增加,
                                    ACCTBIZ_EXCODE: Dict.ACCTBIZ_EXCODE.一码通账户查询,
                                    ID_TYPE: Dict.ID_TYPE.身份证,
                                    ID_CODE: id_code,
                                    CUST_FNAME: cust_name,
                                    CHK_STATUS: Dict.CHK_STATUS.已通过,
                                    PROPER_CLS: "",  // 适当性类别必须为空
                                    ACCT_OPENTYPE: ""  // 必须送空值
                                );
            // 获取中登处理结果
            Response rspsStkAcctBizInfo = await this.searchStkAcctBizInfo(serialNo, timeout: timeout);

            // 判断返回的操作结果是否异常
            if (rspsStkAcctBizInfo.length == 1 && rspsStkAcctBizInfo.getValue("RTN_ERR_CODE") != "0000")
            {
                throw new Exception("中登返回错误：" + rspsStkAcctBizInfo.getValue("RTN_ERR_CODE") + "，错误信息：" + rspsStkAcctBizInfo.getValue("RETURN_MSG"));
            }

            return rspsStkAcctBizInfo;
        }


        /// <summary>
        /// 证券账户查询
        /// 实现2.53 证券账户查询
        /// </summary>
        /// <param name="CUST_CODE">客户代码</param>
        /// <param name="STKBD">交易版块10-沪A11-沪B等，见数据字典DD[STKBD]</param>
        /// <param name="TRDACCT">股东账户</param>
        /// <returns></returns>
        async public Task<Response> listOfStkTrdAcct(
                string CUST_CODE, //客户代码
                string STKBD = "", //交易版块10-沪A11-沪B等，见数据字典DD[STKBD]
                string TRDACCT = "" //股东账户
            )
        {
            // 前置条件判断
            if (CUST_CODE == "")
            {
                string message = "客户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "listOfStkTrdAcct");
            request.setAttr("CUST_CODE", CUST_CODE); //客户代码
            request.setAttr("STKBD", STKBD); //交易版块10-沪A11-沪B等，见数据字典DD[STKBD]
            request.setAttr("TRDACCT", TRDACCT); //股东账户

            // 调用WebService获取返回值
            Response response = await this.invoke(request);

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

    }
}
