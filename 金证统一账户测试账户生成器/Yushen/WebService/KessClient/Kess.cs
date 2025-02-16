﻿using System;
using System.Threading.Tasks;

namespace Yushen.WebService.KessClient
{
    /// <summary>
    /// 用于金证统一账户系统WebService接口的操作类
    /// </summary>
    public partial class Kess : IDisposable
    {
        /// <summary>
        /// 金证系统类型
        /// 分为Win版和U版两种
        /// </summary>
        public enum Edtion{
            Win = 0,
            U = 1
        }

        /// <summary>
        /// 柜台系统类型
        /// </summary>
        public Edtion edition
        {
            set
            {
                _edition = value;
            }
            get
            {
                return _edition;
            }
        }

        /// <summary>
        /// 同时发起的WebService请求的最大数量，超过则必须等待
        /// </summary>
        public int maxConnections = 10;

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
        /// <param name="edtion">统一账户系统版本，默认为U版</param>
        public Kess(string operatorId, string password, string channel, string kessWebserviceURL = "", int maxConnections = 10, Edtion edtion = Edtion.U)
        {
            this.operatorId = operatorId;
            this.password = password;
            this.channel = channel;
            if (kessWebserviceURL != "")
            {
                this.kessWebserviceURL = kessWebserviceURL;
            }
            this.edition = edtion;
            this.maxConnections = maxConnections;

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
        /// <param name="OP_USER"></param>
        /// <param name="OPERATION_TYPE">操作类型（必传）0：增加密码，1：修改密码3：重置密码</param>
        /// <param name="USER_CODE">用户代码（必传）</param>
        /// <param name="USER_ROLE">用户角色（必传）DD[USER_ROLE]</param>
        /// <param name="USE_SCOPE">使用范围（必传）DD[USE_SCOPE]</param>
        /// <param name="AUTH_TYPE">认证类型（必传）DD[AUTH]_TYPE]</param>
        /// <param name="OLD_AUTH_DATA">原认证信息（非必传）</param>
        /// <param name="NEW_AUTH_DATA">新认证信息（必传）</param>
        /// <param name="OP_REMARK">操作备注（非必传）</param>
        /// <param name="SUBSYS_FLAG">同步标志0-本地和对接系统均更新1-仅更新对接系统2-仅更新本地</param>
        /// <returns></returns>
        async public Task<bool> mdfUserPassword(
                string OPERATION_TYPE, //操作类型（必传）0：增加密码，1：修改密码3：重置密码
                string USER_CODE, //用户代码（必传）
                string NEW_AUTH_DATA, //新认证信息（必传）
                string USE_SCOPE, //使用范围（必传）DD[USE_SCOPE]
                string OP_USER = "", //
                string USER_ROLE = Dict.USER_ROLE.客户, //用户角色（必传）DD[USER_ROLE]
                string AUTH_TYPE = Dict.AUTH_TYPE.密码, //认证类型（必传）DD[AUTH]_TYPE]
                string OLD_AUTH_DATA = "", //原认证信息（非必传）
                string OP_REMARK = "", //操作备注（非必传）
                string SUBSYS_FLAG = "" //同步标志0-本地和对接系统均更新1-仅更新对接系统2-仅更新本地
            )
        {
            // 前置条件判断
            if (OPERATION_TYPE == "")
            {
                string message = "客户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (USER_CODE == "")
            {
                string message = "客户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (NEW_AUTH_DATA == "")
            {
                string message = "密码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "mdfUserPassword");
            request.setAttr("OP_USER", OP_USER); //
            request.setAttr("OPERATION_TYPE", OPERATION_TYPE); //操作类型（必传）0：增加密码，1：修改密码3：重置密码
            request.setAttr("USER_CODE", USER_CODE); //用户代码（必传）
            request.setAttr("USER_ROLE", USER_ROLE); //用户角色（必传）DD[USER_ROLE]
            request.setAttr("USE_SCOPE", USE_SCOPE); //使用范围（必传）DD[USE_SCOPE]
            request.setAttr("AUTH_TYPE", AUTH_TYPE); //认证类型（必传）DD[AUTH]_TYPE]
            request.setAttr("OLD_AUTH_DATA", OLD_AUTH_DATA); //原认证信息（非必传）
            request.setAttr("NEW_AUTH_DATA", NEW_AUTH_DATA); //新认证信息（必传）
            request.setAttr("OP_REMARK", OP_REMARK); //操作备注（非必传）
            request.setAttr("SUBSYS_FLAG", SUBSYS_FLAG); //同步标志0-本地和对接系统均更新1-仅更新对接系统2-仅更新本地



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
        /// <param name="USER_CODE">客户代码(必输)</param>
        /// <param name="SURVEY_SN">调查表编码(必输)</param>
        /// <param name="SURVEY_COLS">调查表栏目（复数）(以‘|’隔开)(必输)</param>
        /// <param name="SURVEY_CELLS">调查表单元（复数）(以‘|’隔开)(必输)</param>
        /// <param name="SURVEY_ANS_VALS">调查表作答分值（复数）(以’|’隔开)(非必输)</param>
        /// <param name="ANS_STATUS">作答状态1-答题中2-答题结束</param>
        /// <param name="VERSION">版本号</param>
        /// <returns></returns>
        async public Task<bool> syncSurveyAns2Kbss(
                string USER_CODE, //客户代码(必输)
                string SURVEY_SN, //调查表编码(必输)
                string SURVEY_COLS, //调查表栏目（复数）(以‘|’隔开)(必输)
                string SURVEY_CELLS, //调查表单元（复数）(以‘|’隔开)(必输)
                string SURVEY_ANS_VALS = "", //调查表作答分值（复数）(以’|’隔开)(非必输)
                string ANS_STATUS = "", //作答状态1-答题中2-答题结束
                string VERSION = "" //版本号
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
            Request request = new Request(this.operatorId, "syncSurveyAns2Kbss");
            request.setAttr("USER_CODE", USER_CODE); //客户代码(必输)
            request.setAttr("SURVEY_SN", SURVEY_SN); //调查表编码(必输)
            request.setAttr("SURVEY_COLS", SURVEY_COLS); //调查表栏目（复数）(以‘|’隔开)(必输)
            request.setAttr("SURVEY_CELLS", SURVEY_CELLS); //调查表单元（复数）(以‘|’隔开)(必输)
            request.setAttr("SURVEY_ANS_VALS", SURVEY_ANS_VALS); //调查表作答分值（复数）(以’|’隔开)(非必输)
            request.setAttr("ANS_STATUS", ANS_STATUS); //作答状态1-答题中2-答题结束
            request.setAttr("VERSION", VERSION); //版本号

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

        /// <summary>
        /// 查询客户签署协议
        /// 实现2.159	查询客户签署协议
        /// </summary>
        /// <param name="CUST_CODE">客户代码（必传）</param>
        /// <param name="STKBD">交易板块（非必传）DD[STKBD]00-深港通，10-沪港通</param>
        /// <param name="CUST_AGMT_TYPE">协议类型（非必传）DD[CUST_AGMT_TYPE]若未输则查询全部</param>
        /// <param name="REMOTE_SYS">对接远程系统（非必传）</param>
        /// <param name="CUACCT_CODE">资产账户（非必传）</param>
        /// <param name="TRDACCT">交易账户（非必传）</param>
        /// <returns></returns>
        async public Task<Response> queryCustAgreement(
                string CUST_CODE = "", //客户代码（必传）
                string STKBD = "", //交易板块（非必传）DD[STKBD]00-深港通，10-沪港通
                string CUST_AGMT_TYPE = "", //协议类型（非必传）DD[CUST_AGMT_TYPE]若未输则查询全部
                string REMOTE_SYS = "", //对接远程系统（非必传）
                string CUACCT_CODE = "", //资产账户（非必传）
                string TRDACCT = "" //交易账户（非必传）
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
            Request request = new Request(this.operatorId, "queryCustAgreement");
            request.setAttr("CUST_CODE", CUST_CODE); //客户代码（必传）
            request.setAttr("STKBD", STKBD); //交易板块（非必传）DD[STKBD]00-深港通，10-沪港通
            request.setAttr("CUST_AGMT_TYPE", CUST_AGMT_TYPE); //协议类型（非必传）DD[CUST_AGMT_TYPE]若未输则查询全部
            request.setAttr("REMOTE_SYS", REMOTE_SYS); //对接远程系统（非必传）
            request.setAttr("CUACCT_CODE", CUACCT_CODE); //资产账户（非必传）
            request.setAttr("TRDACCT", TRDACCT); //交易账户（非必传）

            // 调用WebService获取返回值
            Response response = await this.invoke(request);
            
            // 返回结果
            return response;
        }

        /// <summary>
        /// 证券账户查询
        /// 实现2.53 证券账户查询
        /// </summary>
        /// <param name="CUACCT_CODE">资产账户（必传）</param>
        /// <param name="USER_CODE">客户代码（非必传）</param>
        /// <returns></returns>
        async public Task<Response> queryCustInfoByCuacct(
                string CUACCT_CODE, //资产账户（必传）
                string USER_CODE = "" //客户代码（非必传）
            )
        {
            // 前置条件判断
            if (CUACCT_CODE == "")
            {
                string message = "资产账户不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "queryCustInfoByCuacct");
            request.setAttr("USER_CODE", USER_CODE); //客户代码（非必传）
            request.setAttr("CUACCT_CODE", CUACCT_CODE); //资产账户（必传）

            // 调用WebService获取返回值
            Response response = await this.invoke(request);
            
            // 返回结果
            return response;
        }

        /// <summary>
        /// 客户诚信记录信息查询
        /// 实现2.229 客户诚信记录信息查询
        /// </summary>
        /// <param name="CUST_CODE">客户代码（必传）</param>
        /// <param name="RECORD_SOURCE">诚信记录来源（非必传）DD[RECORD_SOURCE]</param>
        /// <returns></returns>
        async public Task<Response> qryCreditRecord(
                string CUST_CODE, //客户代码（必传）
                string RECORD_SOURCE = "" //诚信记录来源（非必传）DD[RECORD_SOURCE]
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
            Request request = new Request(this.operatorId, "qryCreditRecord");
            request.setAttr("CUST_CODE", CUST_CODE); //客户代码（必传）
            request.setAttr("RECORD_SOURCE", RECORD_SOURCE); //诚信记录来源（非必传）DD[RECORD_SOURCE]

            // 调用WebService获取返回值
            Response response = await this.invoke(request);
            
            // 返回结果
            return response;
        }

        /// <summary>
        /// 受益人信息查询
        /// 实现2.246 受益人信息查询
        /// </summary>
        /// <param name="USER_CODE">用户代码（必传）</param>
        /// <param name="BENEFICIARY_NO">受益人编号（非必传）</param>
        /// <returns></returns>
        async public Task<Response> queryUserBeneficiaryInfo(
                string USER_CODE, //用户代码（必传）
                string BENEFICIARY_NO = "" //受益人编号（非必传）
            )
        {
            // 前置条件判断
            if (USER_CODE == "")
            {
                string message = "用户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "queryUserBeneficiaryInfo");
            request.setAttr("USER_CODE", USER_CODE); //用户代码（必传）
            request.setAttr("BENEFICIARY_NO", BENEFICIARY_NO); //受益人编号（非必传）

            // 调用WebService获取返回值
            Response response = await this.invoke(request);

            // 返回结果
            return response;
        }

        /// <summary>
        /// 受益人信息查询
        /// 实现2.246 受益人信息查询
        /// </summary>
        /// <param name="CUST_CODE">客户代码（必传）</param>
        /// <param name="CONTROLER_NUM">控制人编号（非必传）</param>
        /// <returns></returns>
        async public Task<Response> queryControllerInfo(
                string CUST_CODE = "", //客户代码（必传）
                string CONTROLER_NUM = "" //控制人编号（非必传）
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
            Request request = new Request(this.operatorId, "queryControllerInfo");
            request.setAttr("CUST_CODE", CUST_CODE); //客户代码（必传）
            request.setAttr("CONTROLER_NUM", CONTROLER_NUM); //控制人编号（非必传）

            // 调用WebService获取返回值
            Response response = await this.invoke(request);

            // 返回结果
            return response;
        }

        /// <summary>
        /// 修改用户基本信息
        /// 实现2.90 修改用户基本信息
        /// </summary>
        /// <param name="USER_CODE">客户代码（必传）</param>
        /// <param name="ZIP_CODE">邮政编码（必传）</param>
        /// <param name="ADDRESS">联系地址（必传）</param>
        /// <param name="FISL_EMAIL">电子邮箱（非必传）同步到融资融券的时候必传</param>
        /// <param name="EDUCATION">学历（非必传）</param>
        /// <param name="TEL">联系电话（非必传）</param>
        /// <param name="FAX">传真电话（非必传）</param>
        /// <param name="EMAIL">电子邮箱（非必传）</param>
        /// <param name="MOBILE_TEL">移动电话（非必传）</param>
        /// <param name="CITIZENSHIP">国籍DD[CITIZENSHIP]（非必传）</param>
        /// <param name="NATIONALITY">民族DD[NATIONALITY]（非必传）</param>
        /// <param name="NATIVE_PLACE">籍贯（非必传）</param>
        /// <param name="SEX">性别DD[SEX]（非必传）</param>
        /// <param name="BIRTHDAY">出生日期（非必传）</param>
        /// <param name="REMARK">备注信息（非必传）</param>
        /// <param name="ID_ISS_AGCY">发证机关（非必传）</param>
        /// <param name="ID_EXP_DATE">证件有效日期（非必传）</param>
        /// <param name="ID_BEG_DATE">证件开始日期（非必传）</param>
        /// <param name="ID_ZIP_CODE">证件编码（非必传）</param>
        /// <param name="ID_ADDR">证件地址（非必传）</param>
        /// <param name="MARRY">婚姻状态（非必传）</param>
        /// <param name="AVOCATION">兴趣爱好（非必传）</param>
        /// <param name="VEHICLE">交通工具DD[VEHICLE]（非必传）</param>
        /// <param name="HOUSE_OWNER">住宅所有权状况（非必传）</param>
        /// <param name="OFFICE_TEL">办工电话（非必传）</param>
        /// <param name="WELL_TEL">小灵通电话（非必传）</param>
        /// <param name="LINKTEL_ORDER">首选联系电话（非必传）</param>
        /// <param name="OFFICE_ADDR">办工地址（非必传）</param>
        /// <param name="CORP_ADDR">公司地址（非必传）</param>
        /// <param name="LINKADDR_ORDER">首选联系地址（非必传）</param>
        /// <param name="OPEN_SOURCE">开户来源（非必传）</param>
        /// <param name="SUBSYS">同步子系统（非必传）</param>
        /// <param name="RISK_FACTOR">风险要素（非必传）</param>
        /// <param name="CRITERION">开户规范（非必传）</param>
        /// <param name="CUST_CLS">客户分类（非必传）</param>
        /// <param name="TEL_CHK_FLAG">手机校验标识（非必传）</param>
        /// <param name="EMAIL_CHK_FLAG">邮箱校验标识（非必传）</param>
        /// <param name="FIN_EDU_FLAG">金融相关专业学历校验标识（非必传）</param>
        /// <param name="VOCATION">职业（非必传）</param>
        /// <returns></returns>
        async public Task<Response> mdfUserGenInfo(
                string USER_CODE, //客户代码（必传）
                string ZIP_CODE, //邮政编码（必传）
                string ADDRESS, //联系地址（必传）
                string FISL_EMAIL = "", //电子邮箱（非必传）同步到融资融券的时候必传
                string EDUCATION = "", //学历（非必传）
                string TEL = "", //联系电话（非必传）
                string FAX = "", //传真电话（非必传）
                string EMAIL = "", //电子邮箱（非必传）
                string MOBILE_TEL = "", //移动电话（非必传）
                string CITIZENSHIP = "", //国籍DD[CITIZENSHIP]（非必传）
                string NATIONALITY = "", //民族DD[NATIONALITY]（非必传）
                string NATIVE_PLACE = "", //籍贯（非必传）
                string SEX = "", //性别DD[SEX]（非必传）
                string BIRTHDAY = "", //出生日期（非必传）
                string REMARK = "", //备注信息（非必传）
                string ID_ISS_AGCY = "", //发证机关（非必传）
                string ID_EXP_DATE = "", //证件有效日期（非必传）
                string ID_BEG_DATE = "", //证件开始日期（非必传）
                string ID_ZIP_CODE = "", //证件编码（非必传）
                string ID_ADDR = "", //证件地址（非必传）
                string MARRY = "", //婚姻状态（非必传）
                string AVOCATION = "", //兴趣爱好（非必传）
                string VEHICLE = "", //交通工具DD[VEHICLE]（非必传）
                string HOUSE_OWNER = "", //住宅所有权状况（非必传）
                string OFFICE_TEL = "", //办工电话（非必传）
                string WELL_TEL = "", //小灵通电话（非必传）
                string LINKTEL_ORDER = "", //首选联系电话（非必传）
                string OFFICE_ADDR = "", //办工地址（非必传）
                string CORP_ADDR = "", //公司地址（非必传）
                string LINKADDR_ORDER = "", //首选联系地址（非必传）
                string OPEN_SOURCE = "", //开户来源（非必传）
                string SUBSYS = "", //同步子系统（非必传）
                string RISK_FACTOR = "", //风险要素（非必传）
                string CRITERION = "", //开户规范（非必传）
                string CUST_CLS = "", //客户分类（非必传）
                string TEL_CHK_FLAG = "", //手机校验标识（非必传）
                string EMAIL_CHK_FLAG = "", //邮箱校验标识（非必传）
                string FIN_EDU_FLAG = "", //金融相关专业学历校验标识（非必传）
                string VOCATION = "" //职业（非必传）
            )
        {
            // 前置条件判断
            if (USER_CODE == "")
            {
                string message = "客户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (ZIP_CODE == "")
            {
                string message = "邮政编码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (ADDRESS == "")
            {
                string message = "联系地址不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "mdfUserGenInfo");
            request.setAttr("USER_CODE", USER_CODE); //客户代码（必传）
            request.setAttr("ZIP_CODE", ZIP_CODE); //邮政编码（必传）
            request.setAttr("ADDRESS", ADDRESS); //联系地址（必传）
            request.setAttr("FISL_EMAIL", FISL_EMAIL); //电子邮箱（非必传）同步到融资融券的时候必传
            request.setAttr("EDUCATION", EDUCATION); //学历（非必传）
            request.setAttr("TEL", TEL); //联系电话（非必传）
            request.setAttr("FAX", FAX); //传真电话（非必传）
            request.setAttr("EMAIL", EMAIL); //电子邮箱（非必传）
            request.setAttr("MOBILE_TEL", MOBILE_TEL); //移动电话（非必传）
            request.setAttr("CITIZENSHIP", CITIZENSHIP); //国籍DD[CITIZENSHIP]（非必传）
            request.setAttr("NATIONALITY", NATIONALITY); //民族DD[NATIONALITY]（非必传）
            request.setAttr("NATIVE_PLACE", NATIVE_PLACE); //籍贯（非必传）
            request.setAttr("SEX", SEX); //性别DD[SEX]（非必传）
            request.setAttr("BIRTHDAY", BIRTHDAY); //出生日期（非必传）
            request.setAttr("REMARK", REMARK); //备注信息（非必传）
            request.setAttr("ID_ISS_AGCY", ID_ISS_AGCY); //发证机关（非必传）
            request.setAttr("ID_EXP_DATE", ID_EXP_DATE); //证件有效日期（非必传）
            request.setAttr("ID_BEG_DATE", ID_BEG_DATE); //证件开始日期（非必传）
            request.setAttr("ID_ZIP_CODE", ID_ZIP_CODE); //证件编码（非必传）
            request.setAttr("ID_ADDR", ID_ADDR); //证件地址（非必传）
            request.setAttr("MARRY", MARRY); //婚姻状态（非必传）
            request.setAttr("AVOCATION", AVOCATION); //兴趣爱好（非必传）
            request.setAttr("VEHICLE", VEHICLE); //交通工具DD[VEHICLE]（非必传）
            request.setAttr("HOUSE_OWNER", HOUSE_OWNER); //住宅所有权状况（非必传）
            request.setAttr("OFFICE_TEL", OFFICE_TEL); //办工电话（非必传）
            request.setAttr("WELL_TEL", WELL_TEL); //小灵通电话（非必传）
            request.setAttr("LINKTEL_ORDER", LINKTEL_ORDER); //首选联系电话（非必传）
            request.setAttr("OFFICE_ADDR", OFFICE_ADDR); //办工地址（非必传）
            request.setAttr("CORP_ADDR", CORP_ADDR); //公司地址（非必传）
            request.setAttr("LINKADDR_ORDER", LINKADDR_ORDER); //首选联系地址（非必传）
            request.setAttr("OPEN_SOURCE", OPEN_SOURCE); //开户来源（非必传）
            request.setAttr("SUBSYS", SUBSYS); //同步子系统（非必传）
            request.setAttr("RISK_FACTOR", RISK_FACTOR); //风险要素（非必传）
            request.setAttr("CRITERION", CRITERION); //开户规范（非必传）
            request.setAttr("CUST_CLS", CUST_CLS); //客户分类（非必传）
            request.setAttr("TEL_CHK_FLAG", TEL_CHK_FLAG); //手机校验标识（非必传）
            request.setAttr("EMAIL_CHK_FLAG", EMAIL_CHK_FLAG); //邮箱校验标识（非必传）
            request.setAttr("FIN_EDU_FLAG", FIN_EDU_FLAG); //金融相关专业学历校验标识（非必传）
            request.setAttr("VOCATION", VOCATION); //职业（非必传）

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
        /// 修改用户重要信息
        /// 实现2.90 修改用户重要信息
        /// </summary>
        /// <param name="USER_CODE">用户代码</param>
        /// <param name="USER_NAME">用户名称</param>
        /// <param name="ID_TYPE">证件类型</param>
        /// <param name="ID_CODE">证件号码</param>
        /// <param name="USER_FNAME">用户全称</param>
        /// <param name="ID_ISS_AGCY">发证机关</param>
        /// <param name="ID_BEG_DATE">证件开始日期</param>
        /// <param name="ID_EXP_DATE">证件有效日期</param>
        /// <param name="ID_ZIP_CODE">证件编码</param>
        /// <param name="ID_ADDR">证件地址</param>
        /// <param name="OP_REMARK">操作备注</param>
        /// <param name="BSB_USER_FNAME">签约客户姓名</param>
        /// <param name="BSB_ID_TYPE">签约证件类型</param>
        /// <param name="BSB_ID_CODE">签约证件号码</param>
        /// <param name="BSB_ID_EXP_DATE">签约证件有效日期</param>
        /// <param name="BUSINESS_TAX_NO">税务登记证</param>
        /// <param name="TAX_NO_EXP_DATE">登记证有效日期</param>
        /// <param name="BUSINESS_LICENCE_NO">营业执照号</param>
        /// <param name="LICENCE_NO_EXP_DATE">执照有效日期</param>
        /// <param name="ORG_ID_CODE">组织机构代码</param>
        /// <param name="ORG_ID_EXP_DATE">代码证有效日期</param>
        /// <param name="MAIN_YEAR_CHK_DATE">组织证年检日期</param>
        /// <param name="IDCARD_READ_FLAG">证件卡读卡标志</param>
        /// <param name="BIRTHDAY">出生日期/注册日期</param>
        /// <returns></returns>
        async public Task<Response> updateUserImportantInfo(
                string USER_CODE, //用户代码
                string USER_NAME = "", //用户名称
                string ID_TYPE = "", //证件类型
                string ID_CODE = "", //证件号码
                string USER_FNAME = "", //用户全称
                string ID_ISS_AGCY = "", //发证机关
                string ID_BEG_DATE = "", //证件开始日期
                string ID_EXP_DATE = "", //证件有效日期
                string ID_ZIP_CODE = "", //证件编码
                string ID_ADDR = "", //证件地址
                string OP_REMARK = "", //操作备注
                string BSB_USER_FNAME = "", //签约客户姓名
                string BSB_ID_TYPE = "", //签约证件类型
                string BSB_ID_CODE = "", //签约证件号码
                string BSB_ID_EXP_DATE = "", //签约证件有效日期
                string BUSINESS_TAX_NO = "", //税务登记证
                string TAX_NO_EXP_DATE = "", //登记证有效日期
                string BUSINESS_LICENCE_NO = "", //营业执照号
                string LICENCE_NO_EXP_DATE = "", //执照有效日期
                string ORG_ID_CODE = "", //组织机构代码
                string ORG_ID_EXP_DATE = "", //代码证有效日期
                string MAIN_YEAR_CHK_DATE = "", //组织证年检日期
                string IDCARD_READ_FLAG = "", //证件卡读卡标志
                string BIRTHDAY = "" //出生日期/注册日期
            )
        {
            // 前置条件判断
            if (USER_CODE == "")
            {
                string message = "客户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (USER_NAME == "")
            {
                string message = "用户名称不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (ID_TYPE == "")
            {
                string message = "证件类型不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (ID_CODE == "")
            {
                string message = "证件号码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "updateUserImportantInfo");
            request.setAttr("USER_CODE", USER_CODE); //用户代码
            request.setAttr("USER_NAME", USER_NAME); //用户名称
            request.setAttr("ID_TYPE", ID_TYPE); //证件类型
            request.setAttr("ID_CODE", ID_CODE); //证件号码
            request.setAttr("USER_FNAME", USER_FNAME); //用户全称
            request.setAttr("ID_ISS_AGCY", ID_ISS_AGCY); //发证机关
            request.setAttr("ID_BEG_DATE", ID_BEG_DATE); //证件开始日期
            request.setAttr("ID_EXP_DATE", ID_EXP_DATE); //证件有效日期
            request.setAttr("ID_ZIP_CODE", ID_ZIP_CODE); //证件编码
            request.setAttr("ID_ADDR", ID_ADDR); //证件地址
            request.setAttr("OP_REMARK", OP_REMARK); //操作备注
            request.setAttr("BSB_USER_FNAME", BSB_USER_FNAME); //签约客户姓名
            request.setAttr("BSB_ID_TYPE", BSB_ID_TYPE); //签约证件类型
            request.setAttr("BSB_ID_CODE", BSB_ID_CODE); //签约证件号码
            request.setAttr("BSB_ID_EXP_DATE", BSB_ID_EXP_DATE); //签约证件有效日期
            request.setAttr("BUSINESS_TAX_NO", BUSINESS_TAX_NO); //税务登记证
            request.setAttr("TAX_NO_EXP_DATE", TAX_NO_EXP_DATE); //登记证有效日期
            request.setAttr("BUSINESS_LICENCE_NO", BUSINESS_LICENCE_NO); //营业执照号
            request.setAttr("LICENCE_NO_EXP_DATE", LICENCE_NO_EXP_DATE); //执照有效日期
            request.setAttr("ORG_ID_CODE", ORG_ID_CODE); //组织机构代码
            request.setAttr("ORG_ID_EXP_DATE", ORG_ID_EXP_DATE); //代码证有效日期
            request.setAttr("MAIN_YEAR_CHK_DATE", MAIN_YEAR_CHK_DATE); //组织证年检日期
            request.setAttr("IDCARD_READ_FLAG", IDCARD_READ_FLAG); //证件卡读卡标志
            request.setAttr("BIRTHDAY", BIRTHDAY); //出生日期/注册日期

            // 调用WebService获取返回值
            Response response = await this.invoke(request);

            // 判断返回的操作结果是否异常
            if (response.flag != "1")
            {
                string message = "修改客户重要信息失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }

            // 返回结果
            return response;
        }

        /// <summary>
        /// 客户操作渠道修改
        /// 实现2.278	客户操作渠道修改
        /// </summary>
        /// <param name="CUST_CODE">客户代码（非必传）</param>
        /// <param name="CUACCT_CODE">资产账户（非必传）</param>
        /// <param name="SUBSYS">子系统（非必传）</param>
        /// <param name="CHANNELS">操作渠道（必传）</param>
        /// <param name="OP_REMARK">备注（非必传）</param>
        /// <returns></returns>
        async public Task<Response> mdfFmsCustChannel(
                string CUST_CODE = "", //客户代码（非必传）
                string CUACCT_CODE = "", //资产账户（非必传）
                string SUBSYS = "", //子系统（非必传）
                string CHANNELS = "", //操作渠道（必传）
                string OP_REMARK = "" //备注（非必传）
            )
        {
            // 前置条件判断
            if (CUST_CODE == "")
            {
                string message = "客户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (CHANNELS == "")
            {
                string message = "操作渠道不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "mdfFmsCustChannel");
            request.setAttr("CUST_CODE", CUST_CODE); //客户代码（非必传）
            request.setAttr("CUACCT_CODE", CUACCT_CODE); //资产账户（非必传）
            request.setAttr("SUBSYS", SUBSYS); //子系统（非必传）
            request.setAttr("CHANNELS", CHANNELS); //操作渠道（必传）
            request.setAttr("OP_REMARK", OP_REMARK); //备注（非必传）

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
        /// 查询客户职业信息
        /// 实现2.91	查询客户职业信息
        /// </summary>
        /// <param name="USER_CODE">客户代码（必传）</param>
        /// <param name="OUTPUT_TYPE">出参类型（非必传）</param>
        /// <returns></returns>
        async public Task<Response> getUserOccuInfo(
                string USER_CODE, //客户代码（必传）
                string OUTPUT_TYPE = "" //出参类型（非必传）
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
            Request request = new Request(this.operatorId, "getUserOccuInfo");
            request.setAttr("USER_CODE", USER_CODE); //客户代码（必传）
            request.setAttr("OUTPUT_TYPE", OUTPUT_TYPE); //出参类型（非必传）

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
        /// 受益人信息维护
        /// 实现2.234	受益人信息维护
        /// </summary>
        /// <param name="USER_CODE">用户代码（必传）</param>
        /// <param name="OPERATION_TYPE">操作类型（必传）</param>
        /// <param name="BENEFICIARY_NO">受益人编号（必传）</param>
        /// <param name="BENEFICIARY_NAME">受益人名称（非必传）</param>
        /// <param name="BENEFICIARY_ID_TYPE">受益人证件类型（非必传）</param>
        /// <param name="BENEFICIARY_ID">受益人证件号码（非必传）</param>
        /// <param name="BENEFICIARY_EXP_DATE">受益人证件有效期（非必传）</param>
        /// <param name="BENEFICIARY_TEL">受益人电话号码（非必传）</param>
        /// <param name="BENEFICIARY_RELA">收益人关系（非必传）</param>
        /// <param name="OP_REMARK">备注（非必传）</param>
        /// <returns></returns>
        async public Task<Response> mdfUserBeneficiaryInfo(
                string USER_CODE, //用户代码（必传）
                string OPERATION_TYPE, //操作类型（必传）
                string BENEFICIARY_NO, //受益人编号（必传）
                string BENEFICIARY_NAME = "", //受益人名称（非必传）
                string BENEFICIARY_ID_TYPE = "", //受益人证件类型（非必传）
                string BENEFICIARY_ID = "", //受益人证件号码（非必传）
                string BENEFICIARY_EXP_DATE = "", //受益人证件有效期（非必传）
                string BENEFICIARY_TEL = "", //受益人电话号码（非必传）
                string BENEFICIARY_RELA = "", //收益人关系（非必传）
                string OP_REMARK = "" //备注（非必传）
            )
        {
            // 前置条件判断
            if (USER_CODE == "")
            {
                string message = "客户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (OPERATION_TYPE == "")
            {
                string message = "操作类型不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (BENEFICIARY_NO == "")
            {
                string message = "受益人编号不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "mdfUserBeneficiaryInfo");
            request.setAttr("USER_CODE", USER_CODE); //用户代码（必传）
            request.setAttr("OPERATION_TYPE", OPERATION_TYPE); //操作类型（必传）
            request.setAttr("BENEFICIARY_NO", BENEFICIARY_NO); //受益人编号（必传）
            request.setAttr("BENEFICIARY_NAME", BENEFICIARY_NAME); //受益人名称（非必传）
            request.setAttr("BENEFICIARY_ID_TYPE", BENEFICIARY_ID_TYPE); //受益人证件类型（非必传）
            request.setAttr("BENEFICIARY_ID", BENEFICIARY_ID); //受益人证件号码（非必传）
            request.setAttr("BENEFICIARY_EXP_DATE", BENEFICIARY_EXP_DATE); //受益人证件有效期（非必传）
            request.setAttr("BENEFICIARY_TEL", BENEFICIARY_TEL); //受益人电话号码（非必传）
            request.setAttr("BENEFICIARY_RELA", BENEFICIARY_RELA); //收益人关系（非必传）
            request.setAttr("OP_REMARK", OP_REMARK); //备注（非必传）

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
        /// 控制人信息维护
        /// 实现2.241	控制人信息维护
        /// </summary>
        /// <param name="OPER_TYPE">操作类型（必传）</param>
        /// <param name="CUST_CODE">客户代码（必传）</param>
        /// <param name="CONTROLER_ID_NO">控制人证件号码（必传）</param>
        /// <param name="CONTROLER_NUM">控制人编码（非必传）</param>
        /// <param name="CONTROLER_NAME">控制人姓名（非必传）</param>
        /// <param name="CONTROLER_ID_TYPE">控制人证件类型（非必传）</param>
        /// <param name="CONTROLER_ID_EXP_DATE">控制人证件有效期（非必传）</param>
        /// <param name="CONTROLER_TEL">控制人电话（非必传）</param>
        /// <param name="CONTROLER_EMAIL">控制人邮箱（非必传）</param>
        /// <param name="CONTROLER_RELATION">控制人与本人关系（非必传）</param>
        /// <param name="REMARK">备注（非必传）</param>
        /// <returns></returns>
        async public Task<Response> mdfControlLerInfo(
                string OPER_TYPE, //操作类型（必传）
                string CUST_CODE, //客户代码（必传）
                string CONTROLER_ID_NO, //控制人证件号码（必传）
                string CONTROLER_NUM = "", //控制人编码（非必传）
                string CONTROLER_NAME = "", //控制人姓名（非必传）
                string CONTROLER_ID_TYPE = "", //控制人证件类型（非必传）
                string CONTROLER_ID_EXP_DATE = "", //控制人证件有效期（非必传）
                string CONTROLER_TEL = "", //控制人电话（非必传）
                string CONTROLER_EMAIL = "", //控制人邮箱（非必传）
                string CONTROLER_RELATION = "", //控制人与本人关系（非必传）
                string REMARK = "" //备注（非必传）
            )
        {
            // 前置条件判断
            if (OPER_TYPE == "")
            {
                string message = "操作类型不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (CUST_CODE == "")
            {
                string message = "客户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (CONTROLER_ID_NO == "")
            {
                string message = "控制人证件号码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "mdfControlLerInfo");
            request.setAttr("OPER_TYPE", OPER_TYPE); //操作类型（必传）
            request.setAttr("CUST_CODE", CUST_CODE); //客户代码（必传）
            request.setAttr("CONTROLER_NUM", CONTROLER_NUM); //控制人编码（非必传）
            request.setAttr("CONTROLER_NAME", CONTROLER_NAME); //控制人姓名（非必传）
            request.setAttr("CONTROLER_ID_TYPE", CONTROLER_ID_TYPE); //控制人证件类型（非必传）
            request.setAttr("CONTROLER_ID_NO", CONTROLER_ID_NO); //控制人证件号码（必传）
            request.setAttr("CONTROLER_ID_EXP_DATE", CONTROLER_ID_EXP_DATE); //控制人证件有效期（非必传）
            request.setAttr("CONTROLER_TEL", CONTROLER_TEL); //控制人电话（非必传）
            request.setAttr("CONTROLER_EMAIL", CONTROLER_EMAIL); //控制人邮箱（非必传）
            request.setAttr("CONTROLER_RELATION", CONTROLER_RELATION); //控制人与本人关系（非必传）
            request.setAttr("REMARK", REMARK); //备注（非必传）

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
        /// 风险测评结果查询
        /// 实现2.104 风险测评结果查询
        /// </summary>
        /// <param name="SURVEY_SN">调查表编码（必传）（默认为1）</param>
        /// <param name="USER_CODE">用户代码（必传）</param>
        /// <param name="USER_ROLE">用户角色（必传）</param>
        /// <param name="BGN_DATE">开始日期（非必传）当需要查询某个时间段内，客户做的所有的风险测评时，送开始日期和结束日期。若取最新一条则无需送开始日期和结束日期。</param>
        /// <param name="END_DATE">结束日期（非必传）</param>
        /// <param name="VERSION">版本（非必传）</param>
        /// <param name="REMARK">备注（非必传）</param>
        /// <returns></returns>
        async public Task<Response> queryRiskSurveyResult(
                string SURVEY_SN, //调查表编码（必传）（默认为1）
                string USER_CODE, //用户代码（必传）
                string USER_ROLE, //用户角色（必传）
                string BGN_DATE = "", //开始日期（非必传）当需要查询某个时间段内，客户做的所有的风险测评时，送开始日期和结束日期。若取最新一条则无需送开始日期和结束日期。
                string END_DATE = "", //结束日期（非必传）
                string VERSION = "" //版本（非必传）
            )
        {
            // 前置条件判断
            if (SURVEY_SN == "")
            {
                string message = "调查表编码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (USER_CODE == "")
            {
                string message = "客户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (USER_ROLE == "")
            {
                string message = "客户角色不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "queryRiskSurveyResult");
            request.setAttr("SURVEY_SN", SURVEY_SN); //调查表编码（必传）（默认为1）
            request.setAttr("USER_CODE", USER_CODE); //用户代码（必传）
            request.setAttr("USER_ROLE", USER_ROLE); //用户角色（必传）
            request.setAttr("BGN_DATE", BGN_DATE); //开始日期（非必传）当需要查询某个时间段内，客户做的所有的风险测评时，送开始日期和结束日期。若取最新一条则无需送开始日期和结束日期。
            request.setAttr("END_DATE", END_DATE); //结束日期（非必传）
            request.setAttr("VERSION", VERSION); //版本（非必传）

            // 调用WebService获取返回值
            Response response = await this.invoke(request);
            
            // 返回结果
            return response;
        }

        /// <summary>
        /// 风险测评结果查询
        /// 实现2.104 风险测评结果查询
        /// </summary>
        /// <param name="CUST_CODE">客户代码（非必传）</param>
        /// <param name="YMT_CODE">一码通账号（非必传）</param>
        /// <returns></returns>
        async public Task<Response> queryStkYmt(
                string CUST_CODE = "", //客户代码（非必传）
                string YMT_CODE = "" //一码通账号（非必传）
            )
        {
            // 前置条件判断

            // 初始化请求
            Request request = new Request(this.operatorId, "queryStkYmt");
            request.setAttr("CUST_CODE", CUST_CODE); //客户代码（非必传）
            request.setAttr("YMT_CODE", YMT_CODE); //一码通账号（非必传）

            // 调用WebService获取返回值
            Response response = await this.invoke(request);

            // 返回结果
            return response;
        }

        /// <summary>
        /// 非居民金融账户涉税信息查询
        /// 实现2.272	非居民金融账户涉税信息查询
        /// </summary>
        /// <param name="CUST_CODE">客户代码（必传）</param>
        /// <param name="CTRL_FLAG">是否控制人（0非控制人,默认为0,1控制人）（非必传）</param>
        /// <param name="CTRL_NO">控制人编号（非必传）</param>
        /// <param name="CITIZENSHIP">国籍（非必传）</param>
        /// <param name="TAXPAYER_IDNO">纳税人识别号（非必传）</param>
        /// <param name="SERIAL_NO">流水序号（非必传）</param>
        /// <returns></returns>
        async public Task<Response> qryCustNraTaxInfo(
                string CUST_CODE, //客户代码（必传）
                string CTRL_FLAG = "", //是否控制人（0非控制人,默认为0,1控制人）（非必传）
                string CTRL_NO = "", //控制人编号（非必传）
                string CITIZENSHIP = "", //国籍（非必传）
                string TAXPAYER_IDNO = "", //纳税人识别号（非必传）
                string SERIAL_NO = "" //流水序号（非必传）
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
            Request request = new Request(this.operatorId, "qryCustNraTaxInfo");
            request.setAttr("CUST_CODE", CUST_CODE); //客户代码（必传）
            request.setAttr("CTRL_FLAG", CTRL_FLAG); //是否控制人（0非控制人,默认为0,1控制人）（非必传）
            request.setAttr("CTRL_NO", CTRL_NO); //控制人编号（非必传）
            request.setAttr("CITIZENSHIP", CITIZENSHIP); //国籍（非必传）
            request.setAttr("TAXPAYER_IDNO", TAXPAYER_IDNO); //纳税人识别号（非必传）
            request.setAttr("SERIAL_NO", SERIAL_NO); //流水序号（非必传）

            // 调用WebService获取返回值
            Response response = await this.invoke(request);

            // 返回结果
            return response;
        }

        /// <summary>
        /// 查询20个交易日日均资产
        /// 实现2.170	查询20个交易日日均资产
        /// </summary>
        /// <param name="CUST_CODE">客户代码（必传）</param>
        /// <param name="INT_ORG">机构代码（必传）</param>
        /// <returns></returns>
        async public Task<string> getCustAvgAssets(
            string CUST_CODE, //客户代码（必传）
            string INT_ORG = "" //机构代码（必传）
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
            Request request = new Request(this.operatorId, "getCustAvgAssets");
            request.setAttr("CUST_CODE", CUST_CODE); //客户代码（必传）
            request.setAttr("INT_ORG", INT_ORG); //机构代码（必传）

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
            if (edition == Edtion.U)
            {
                return response.getValue("AVG_MKT");
            }
            else
            {
                return response.getValue("AVG_FUNDASSET");
            }
        }

        /// <summary>
        /// 登记账号查询
        /// 实现2.195	登记账号查询
        /// </summary>
        /// <param name="CUST_CODE">客户代码（必传）</param>
        /// <param name="CUACCT_CODE">资产账户（非必传）</param>
        /// <param name="OTC_CODE">登记机构（非必传）</param>
        /// <param name="TRANS_ACCT">交易账号（非必传）</param>
        /// <param name="INST_CLS">产品子类（非必传）</param>
        /// <returns></returns>
        async public Task<Response> queryOtcAcct(
                string CUST_CODE, //客户代码（必传）
                string CUACCT_CODE = "", //资产账户（非必传）
                string OTC_CODE = "", //登记机构（非必传）
                string TRANS_ACCT = "", //交易账号（非必传）
                string INST_CLS = "" //产品子类（非必传）
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
            Request request = new Request(this.operatorId, "queryOtcAcct");
            request.setAttr("CUST_CODE", CUST_CODE); //客户代码（必传）
            request.setAttr("CUACCT_CODE", CUACCT_CODE); //资产账户（非必传）
            request.setAttr("OTC_CODE", OTC_CODE); //登记机构（非必传）
            request.setAttr("TRANS_ACCT", TRANS_ACCT); //交易账号（非必传）
            request.setAttr("INST_CLS", INST_CLS); //产品子类（非必传）

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
        /// 登记账号查询
        /// 实现2.195	登记账号查询
        /// </summary>
        /// <param name="OPER_TYPE">操作类型（必传）</param>
        /// <param name="CUST_CODE">客户代码（必传）</param>
        /// <param name="RECORD_NUM">诚信记录编号（非必传）</param>
        /// <param name="RECORD_SOURCE">诚信记录来源（非必传）DD[RECORD_SOURCE]</param>
        /// <param name="RECORD_TXT">诚信记录内容（非必传）</param>
        /// <param name="RECORD_SCORE">加扣分（非必传）加分项直接填写分数，扣分项填负分，例如-25</param>
        /// <returns></returns>
        async public Task<bool> mdfCreditRecord(
                string OPER_TYPE = "", //操作类型（必传）
                string CUST_CODE = "", //客户代码（必传）
                string RECORD_NUM = "", //诚信记录编号（非必传）
                string RECORD_SOURCE = "", //诚信记录来源（非必传）DD[RECORD_SOURCE]
                string RECORD_TXT = "", //诚信记录内容（非必传）
                string RECORD_SCORE = "" //加扣分（非必传）加分项直接填写分数，扣分项填负分，例如-25
            )
        {
            // 前置条件判断
            if (OPER_TYPE == "")
            {
                string message = "操作类型不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (CUST_CODE == "")
            {
                string message = "客户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "mdfCreditRecord");
            request.setAttr("OPER_TYPE", OPER_TYPE); //操作类型（必传）
            request.setAttr("CUST_CODE", CUST_CODE); //客户代码（必传）
            request.setAttr("RECORD_NUM", RECORD_NUM); //诚信记录编号（非必传）
            request.setAttr("RECORD_SOURCE", RECORD_SOURCE); //诚信记录来源（非必传）DD[RECORD_SOURCE]
            request.setAttr("RECORD_TXT", RECORD_TXT); //诚信记录内容（非必传）
            request.setAttr("RECORD_SCORE", RECORD_SCORE); //加扣分（非必传）加分项直接填写分数，扣分项填负分，例如-25

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
        /// 非居民金融账户涉税信息维护
        /// 实现2.271	非居民金融账户涉税信息维护
        /// </summary>
        /// <param name="CUST_CODE">客户代码（必传）</param>
        /// <param name="TAX_RESIDENT_TYPE">税收居民身份（非必传）</param>
        /// <param name="CTRL_FLAG">是否控制人（0非控制人,默认为0,1控制人）（非必传）</param>
        /// <param name="CUST_NAME">客户名称（非必传）</param>
        /// <param name="SURNAME_ENG">姓(英文或拼音)（非必传）</param>
        /// <param name="NAME_ENG">名(英文或拼音)（非必传）</param>
        /// <param name="CITIZENSHIP">国籍（非必传）国籍、地址等信息用"||"拼接，如广东省深圳市南山区：广东省||深圳市||南山区</param>
        /// <param name="ADDRESS">地址（非必传）</param>
        /// <param name="ADDRESS_ENG">地址(英文或拼音)（非必传）</param>
        /// <param name="TAXPAYER_IDNO">纳税人识别号（必传）</param>
        /// <param name="NO_TAXPAYERID_REASON">无纳税人识别号原因（非必传）</param>
        /// <param name="BIRTHDAY">出生日期（非必传）</param>
        /// <param name="BIRTH_ADDRESS">出生地址（非必传）</param>
        /// <param name="BIRTH_ADDRESS_ENG">出生地址(英文或拼音)（非必传）</param>
        /// <param name="PASSIVE_NFE">是否消极非金融机构（必传）</param>
        /// <param name="CTRL_NON_RESIDENT">控制人是否非居民（必传）</param>
        /// <param name="OPERATOR_TYPE">操作类型（必传）</param>
        /// <param name="CTRL_NO">控制人编号（非必传）</param>
        /// <param name="GET_INVEST_CERFLAG">取得投资人声明标识（非必传）</param>
        /// <param name="ADDRESS_TYPE">地址类型（非必传）</param>
        /// <param name="CTRL_TYPE">控制人类型（非必传）</param>
        /// <param name="CTRL_SHARE_RATIO">控制人持股比例（非必传）</param>
        /// <param name="REMARK">备注（非必传）</param>
        /// <param name="REMARK2">备注2（非必传）</param>
        /// <param name="REG_COUNTRY">注册国家代码（非必传）</param>
        /// <param name="LIVING_COUNTRY">现居国家代码（非必传）</param>
        /// <param name="BIRTH_COUNTRY">出生国家代码（非必传）</param>
        /// <param name="BIRTH_CITY_ENG">出生地市(英文或拼音)（非必传）</param>
        /// <param name="BIRTH_NATION_ENG">出生地国家(英文或拼音)（非必传）</param>
        /// <param name="BIRTH_PROVINCE_ENG">出生地省(英文或拼音)（非必传）</param>
        /// <param name="CITIZENSHIP2">税收居民国籍2（非必传）</param>
        /// <param name="CITIZENSHIP3">税收居民国籍3（非必传）</param>
        /// <param name="CITY_ENG">居住地市(英文或拼音)（非必传）</param>（非必传）
        /// <param name="NATION_ENG">居住地国家(英文或拼音)（非必传）</param>
        /// <param name="NO_TAXPAYERID_REASON2">纳税人识别号原因2（非必传）</param>
        /// <param name="NO_TAXPAYERID_REASON3">无纳税人识别号原因3（非必传）</param>
        /// <param name="PROVINCE_ENG">居住地省(英文或拼音)（非必传）</param>
        /// <param name="TAXPAYER_IDNO2">纳税人识别号2（非必传）</param>
        /// <param name="TAXPAYER_IDNO3">纳税人识别号3（非必传）</param>
        /// <param name="MONAMNT">金额(账户余额)（非必传）</param>
        /// <param name="CURR_CODE">货币代码（非必传）</param>
        /// <param name="PAYMENT_TYPE1">收入类型1（非必传）</param>
        /// <param name="PAYMENT_TYPE2">收入类型2（非必传）</param>
        /// <param name="PAYMENT_TYPE3">收入类型3（非必传）</param>
        /// <param name="PAYMENT_TYPE4">收入类型4（非必传）</param>
        /// <param name="PAYMENT_AMNT1">收入金额和货币类型1（非必传）</param>
        /// <param name="PAYMENT_AMNT2">收入金额和货币类型2（非必传）</param>
        /// <param name="PAYMENT_AMNT3">收入金额和货币类型3（非必传）</param>
        /// <param name="PAYMENT_AMNT4">收入金额和货币类型4（非必传）</param>
        /// <param name="PROVINCE">省级行政区划代码（非必传）</param>
        /// <param name="CITYCN">地市级行政区划代码（非必传）</param>
        /// <param name="DISTRICT_NAME">县级行政区划代码（非必传）</param>
        /// <param name="CITYEN">所在城市（英文）（非必传）</param>
        /// <returns></returns>
        async public Task<bool> mdfCustNraTaxInfo(
                string OPERATOR_TYPE, //操作类型（必传）
                string CUST_CODE, //客户代码（必传）
                string TAXPAYER_IDNO, //纳税人识别号（必传）
                string PASSIVE_NFE, //是否消极非金融机构（必传）
                string CTRL_NON_RESIDENT, //控制人是否非居民（必传）
                string TAX_RESIDENT_TYPE = "", //税收居民身份（非必传）
                string CTRL_FLAG = "", //是否控制人（0非控制人,默认为0,1控制人）（非必传）
                string CUST_NAME = "", //客户名称（非必传）
                string SURNAME_ENG = "", //姓(英文或拼音)（非必传）
                string NAME_ENG = "", //名(英文或拼音)（非必传）
                string CITIZENSHIP = "", //国籍（非必传）国籍、地址等信息用"||"拼接，如广东省深圳市南山区：广东省||深圳市||南山区
                string ADDRESS = "", //地址（非必传）
                string ADDRESS_ENG = "", //地址(英文或拼音)（非必传）
                string NO_TAXPAYERID_REASON = "", //无纳税人识别号原因（非必传）
                string BIRTHDAY = "", //出生日期（非必传）
                string BIRTH_ADDRESS = "", //出生地址（非必传）
                string BIRTH_ADDRESS_ENG = "", //出生地址(英文或拼音)（非必传）
                string CTRL_NO = "", //控制人编号（非必传）
                string GET_INVEST_CERFLAG = "", //取得投资人声明标识（非必传）
                string ADDRESS_TYPE = "", //地址类型（非必传）
                string CTRL_TYPE = "", //控制人类型（非必传）
                string CTRL_SHARE_RATIO = "", //控制人持股比例（非必传）
                string REMARK = "", //备注（非必传）
                string REMARK2 = "", //备注2（非必传）
                string REG_COUNTRY = "", //注册国家代码（非必传）
                string LIVING_COUNTRY = "", //现居国家代码（非必传）
                string BIRTH_COUNTRY = "", //出生国家代码（非必传）
                string BIRTH_CITY_ENG = "", //出生地市(英文或拼音)（非必传）
                string BIRTH_NATION_ENG = "", //出生地国家(英文或拼音)（非必传）
                string BIRTH_PROVINCE_ENG = "", //出生地省(英文或拼音)（非必传）
                string CITIZENSHIP2 = "", //税收居民国籍2（非必传）
                string CITIZENSHIP3 = "", //税收居民国籍3（非必传）
                string CITY_ENG = "", //居住地市(英文或拼音)（非必传）（非必传）
                string NATION_ENG = "", //居住地国家(英文或拼音)（非必传）
                string NO_TAXPAYERID_REASON2 = "", //纳税人识别号原因2（非必传）
                string NO_TAXPAYERID_REASON3 = "", //无纳税人识别号原因3（非必传）
                string PROVINCE_ENG = "", //居住地省(英文或拼音)（非必传）
                string TAXPAYER_IDNO2 = "", //纳税人识别号2（非必传）
                string TAXPAYER_IDNO3 = "", //纳税人识别号3（非必传）
                string MONAMNT = "", //金额(账户余额)（非必传）
                string CURR_CODE = "", //货币代码（非必传）
                string PAYMENT_TYPE1 = "", //收入类型1（非必传）
                string PAYMENT_TYPE2 = "", //收入类型2（非必传）
                string PAYMENT_TYPE3 = "", //收入类型3（非必传）
                string PAYMENT_TYPE4 = "", //收入类型4（非必传）
                string PAYMENT_AMNT1 = "", //收入金额和货币类型1（非必传）
                string PAYMENT_AMNT2 = "", //收入金额和货币类型2（非必传）
                string PAYMENT_AMNT3 = "", //收入金额和货币类型3（非必传）
                string PAYMENT_AMNT4 = "", //收入金额和货币类型4（非必传）
                string PROVINCE = "", //省级行政区划代码（非必传）
                string CITYCN = "", //地市级行政区划代码（非必传）
                string DISTRICT_NAME = "", //县级行政区划代码（非必传）
                string CITYEN = "" //所在城市（英文）（非必传）
            )
        {
            // 前置条件判断
            if (TAXPAYER_IDNO == "")
            {
                string message = "纳税人识别号不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (CUST_CODE == "")
            {
                string message = "客户代码不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (OPERATOR_TYPE == "")
            {
                string message = "操作类型不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (PASSIVE_NFE == "")
            {
                string message = "是否消极非金融机构不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (CTRL_NON_RESIDENT == "")
            {
                string message = "控制人是否非居民不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "mdfCustNraTaxInfo");
            //Request request = new Request(this.operatorId, System.Reflection.MethodBase.GetCurrentMethod().Name);
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            request.setAttr("CUST_CODE", CUST_CODE); //客户代码（必传）
            request.setAttr("TAX_RESIDENT_TYPE", TAX_RESIDENT_TYPE); //税收居民身份（非必传）
            request.setAttr("CTRL_FLAG", CTRL_FLAG); //是否控制人（0非控制人,默认为0,1控制人）（非必传）
            request.setAttr("CUST_NAME", CUST_NAME); //客户名称（非必传）
            request.setAttr("SURNAME_ENG", SURNAME_ENG); //姓(英文或拼音)（非必传）
            request.setAttr("NAME_ENG", NAME_ENG); //名(英文或拼音)（非必传）
            request.setAttr("CITIZENSHIP", CITIZENSHIP); //国籍（非必传）国籍、地址等信息用"||"拼接，如广东省深圳市南山区：广东省||深圳市||南山区
            request.setAttr("ADDRESS", ADDRESS); //地址（非必传）
            request.setAttr("ADDRESS_ENG", ADDRESS_ENG); //地址(英文或拼音)（非必传）
            request.setAttr("TAXPAYER_IDNO", TAXPAYER_IDNO); //纳税人识别号（必传）
            request.setAttr("NO_TAXPAYERID_REASON", NO_TAXPAYERID_REASON); //无纳税人识别号原因（非必传）
            request.setAttr("BIRTHDAY", BIRTHDAY); //出生日期（非必传）
            request.setAttr("BIRTH_ADDRESS", BIRTH_ADDRESS); //出生地址（非必传）
            request.setAttr("BIRTH_ADDRESS_ENG", BIRTH_ADDRESS_ENG); //出生地址(英文或拼音)（非必传）
            request.setAttr("PASSIVE_NFE", PASSIVE_NFE); //是否消极非金融机构（必传）
            request.setAttr("CTRL_NON_RESIDENT", CTRL_NON_RESIDENT); //控制人是否非居民（必传）
            request.setAttr("OPERATOR_TYPE", OPERATOR_TYPE); //操作类型（必传）
            request.setAttr("CTRL_NO", CTRL_NO); //控制人编号（非必传）
            request.setAttr("GET_INVEST_CERFLAG", GET_INVEST_CERFLAG); //取得投资人声明标识（非必传）
            request.setAttr("ADDRESS_TYPE", ADDRESS_TYPE); //地址类型（非必传）
            request.setAttr("CTRL_TYPE", CTRL_TYPE); //控制人类型（非必传）
            request.setAttr("CTRL_SHARE_RATIO", CTRL_SHARE_RATIO); //控制人持股比例（非必传）
            request.setAttr("REMARK", REMARK); //备注（非必传）
            request.setAttr("REMARK2", REMARK2); //备注2（非必传）
            request.setAttr("REG_COUNTRY", REG_COUNTRY); //注册国家代码（非必传）
            request.setAttr("LIVING_COUNTRY", LIVING_COUNTRY); //现居国家代码（非必传）
            request.setAttr("BIRTH_COUNTRY", BIRTH_COUNTRY); //出生国家代码（非必传）
            request.setAttr("BIRTH_CITY_ENG", BIRTH_CITY_ENG); //出生地市(英文或拼音)（非必传）
            request.setAttr("BIRTH_NATION_ENG", BIRTH_NATION_ENG); //出生地国家(英文或拼音)（非必传）
            request.setAttr("BIRTH_PROVINCE_ENG", BIRTH_PROVINCE_ENG); //出生地省(英文或拼音)（非必传）
            request.setAttr("CITIZENSHIP2", CITIZENSHIP2); //税收居民国籍2（非必传）
            request.setAttr("CITIZENSHIP3", CITIZENSHIP3); //税收居民国籍3（非必传）
            request.setAttr("CITY_ENG", CITY_ENG); //居住地市(英文或拼音)（非必传）（非必传）
            request.setAttr("NATION_ENG", NATION_ENG); //居住地国家(英文或拼音)（非必传）
            request.setAttr("NO_TAXPAYERID_REASON2", NO_TAXPAYERID_REASON2); //纳税人识别号原因2（非必传）
            request.setAttr("NO_TAXPAYERID_REASON3", NO_TAXPAYERID_REASON3); //无纳税人识别号原因3（非必传）
            request.setAttr("PROVINCE_ENG", PROVINCE_ENG); //居住地省(英文或拼音)（非必传）
            request.setAttr("TAXPAYER_IDNO2", TAXPAYER_IDNO2); //纳税人识别号2（非必传）
            request.setAttr("TAXPAYER_IDNO3", TAXPAYER_IDNO3); //纳税人识别号3（非必传）
            request.setAttr("MONAMNT", MONAMNT); //金额(账户余额)（非必传）
            request.setAttr("CURR_CODE", CURR_CODE); //货币代码（非必传）
            request.setAttr("PAYMENT_TYPE1", PAYMENT_TYPE1); //收入类型1（非必传）
            request.setAttr("PAYMENT_TYPE2", PAYMENT_TYPE2); //收入类型2（非必传）
            request.setAttr("PAYMENT_TYPE3", PAYMENT_TYPE3); //收入类型3（非必传）
            request.setAttr("PAYMENT_TYPE4", PAYMENT_TYPE4); //收入类型4（非必传）
            request.setAttr("PAYMENT_AMNT1", PAYMENT_AMNT1); //收入金额和货币类型1（非必传）
            request.setAttr("PAYMENT_AMNT2", PAYMENT_AMNT2); //收入金额和货币类型2（非必传）
            request.setAttr("PAYMENT_AMNT3", PAYMENT_AMNT3); //收入金额和货币类型3（非必传）
            request.setAttr("PAYMENT_AMNT4", PAYMENT_AMNT4); //收入金额和货币类型4（非必传）
            request.setAttr("PROVINCE", PROVINCE); //省级行政区划代码（非必传）
            request.setAttr("CITYCN", CITYCN); //地市级行政区划代码（非必传）
            request.setAttr("DISTRICT_NAME", DISTRICT_NAME); //县级行政区划代码（非必传）
            request.setAttr("CITYEN", CITYEN); //所在城市（英文）（非必传）

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
    }
}
