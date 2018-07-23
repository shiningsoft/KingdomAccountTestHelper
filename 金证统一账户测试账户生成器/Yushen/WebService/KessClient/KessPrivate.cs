using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Yushen.WebService.KessClient
{
    /// <summary>
    /// 用于金证统一账户系统WebService接口的操作类
    /// </summary>
    /// 
    /// 此文件存储内部方法
    /// 
    public partial class Kess : IDisposable
    {

        /// <summary>
        /// 设置金证WebService的URL地址
        /// </summary>
        private string kessWebserviceURL = "http://60.173.222.38:30004/kess/services/KessService?wsdl";

        /// <summary>
        /// 设置金证WebService接口的类名，用于建立反射调用WebService
        /// </summary>
        private readonly string kessClassName = "金证统一账户测试账户生成器.KessService.KessServiceClient";

        /// <summary>
        /// 用于建立反射调用WebService
        /// </summary>
        // private object kessClient;

        /// <summary>
        /// 用于建立反射调用WebService
        /// </summary>
        private List<KessClient> kessClientList = new List<KessClient>();

        /// <summary>
        /// 用于建立反射调用WebService
        /// </summary>
        private Type kessClientType;

        /// <summary>
        /// 统一账户系统操作员编号
        /// </summary>
        private string operatorId;

        /// <summary>
        /// 统一账户系统操作员密码
        /// </summary>
        private string password;

        /// <summary>
        /// 操作渠道
        /// </summary>
        private string channel;

        /// <summary>
        /// 日志记录器
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 当前正在发起的WebService连接数量
        /// </summary>
        private int _webserviceConnectionsNum = 0;

        /// <summary>
        /// 存储当前排队中的请求的数量
        /// </summary>
        private int _requestQueueCount = 0;

        //创建一个Stopwatch实例，用于计算Webservice请求花费的时间
        private Stopwatch stopWatch = new Stopwatch();

        /// <summary>
        /// 是否自动重新登录
        /// </summary>
        private bool _autoRelogin = true;

        /// <summary>
        /// 柜台系统类型
        /// </summary>
        private Edtion _edition;
        
        /// <summary>
        /// 创建WebService实例
        /// </summary>
        private void CreateInstance()
        {
            // 创建实例
            if (this.kessClientList.Count == 0)
            {
                // 利用反射建立WebService的实例
                this.kessClientType = Type.GetType(this.kessClassName);
                if (this.kessClientType == null)
                {
                    throw new Exception("未能正确识别" + kessClassName + "的类型");
                }

                // 创建多个WebService执行器，并加入List
                for (int i = 0; i < maxConnections; i++)
                {
                    KessClient kessClient = new KessClient();
                    kessClient.executor = Activator.CreateInstance(this.kessClientType, new object[] { "KessService", this.kessWebserviceURL });
                    kessClient.available = true;
                    kessClientList.Add(kessClient);
                }

                if (this.kessClientList.Count == 0)
                {
                    string message = "WebService连接失败：" + this.kessWebserviceURL;
                    logger.Error(message);
                    throw new Exception(message);
                }
            }
        }

        /// <summary>
        /// 报送中登业务，并查询返回结果
        /// 调用WebService接口：2.119 证券账户业务信息操作（新）
        /// 注1：此接口只做中登业务，功能为报送中登，接口入参中的非必输字段要根据具体的中登业务来判断是否必输，可参考10-附件，若需最新接口说明可参考中登下发的接口文档； 
        /// </summary>
        /// <param name="OPERATOR_TYPE">操作类型（必传）0-增加1-修改2-删除一般情况都送0<</param>
        /// <param name="ACCTBIZ_EXCODE">账户业务代码（必传）DD-[ACCTBIZ_EXCODE]</param>
        /// <param name="YMT_CODE">一码通号码（非必传）</param>
        /// <param name="CUST_CODE">客户代码（非必传）</param>
        /// <param name="INT_ORG">机构编码（非必传）</param>
        /// <param name="USER_TYPE">用户类别（非必传）</param>
        /// <param name="CUST_FNAME">客户全称（非必传）</param>
        /// <param name="ID_TYPE">证件类型（非必传）</param>
        /// <param name="ID_CODE">证件号码（非必传）</param>
        /// <param name="STKBD">交易板块（非必传）DD[STKBD]</param>
        /// <param name="TRDACCT_EXCLS">证券账户类别（非必传）DD[TRDACCT_EXCLS]</param>
        /// <param name="TRDACCT">股东账户（非必传）</param>
        /// <param name="TRDACCT_EX">配号证券账户号码沪深A股信用证券账户开户时必填，沪市衍生品合约账户开户时必填，深市衍生品合约账户开户时必填，其他无需填写</param>
        /// <param name="BIRTHDAY">出生日期/注册日期（非必传）</param>
        /// <param name="ID_BEG_DATE">证件开始日期（非必传）</param>
        /// <param name="ID_EXP_DATE">证件有效日期（非必传）</param>
        /// <param name="CITIZENSHIP">国籍（非必传）DD[CITIZENSHIP]</param>
        /// <param name="ID_ADDR">证件地址（非必传）</param>
        /// <param name="ADDRESS">联系地址（非必传）</param>
        /// <param name="ZIP_CODE">邮政编码（非必传）</param>
        /// <param name="OCCU_TYPE">职业类型（非必传）</param>
        /// <param name="EDUCATION">学历（非必传）</param>
        /// <param name="TEL">联系电话（非必传）</param>
        /// <param name="MOBILE_TEL">移动电话（非必传）</param>
        /// <param name="NET_SERVICE">网络服务（非必传）DD[NET_SERVICE]</param>
        /// <param name="NET_SERVICEPASS">网络服务密码（非必传）</param>
        /// <param name="SEX">性别（非必传）DD[SEX]</param>
        /// <param name="CHK_STATUS">复核状态（非必传）DD[CHK_STATUS]，默认为0-未复核，若操作不需要进行操作员复核，则CHK_STATUS必须传2-已通过</param>
        /// <param name="ACCT_OPENTYPE">开户方式（非必传）1：网上自助开户 DD[ACCT_OPENTYPE]</param>
        /// <param name="ACCT_TYPE">账户类型（非必传）</param>
        /// <param name="CORP_EXTYPE">企业类型（非必传）</param>
        /// <param name="EMAIL">电子邮箱（非必传）</param>
        /// <param name="FAX">传真电话</param>
        /// <param name="ACCTBIZ_CLS">业务类型（非必传）（创业板开通或查询时必输，01-开通03-查询；使用信息维护时必输，01-新增，02-撤销，03-查询；中登身份校验时必输，01-简项查询，无照片返回，02-简项查询，有照片返回）</param>
        /// <param name="FIRST_TRD_DATE">首次交易日期（非必传）</param>
        /// <param name="PROPER_CLS">适当性类别（非必传）1-创业板2-全国中小企业股份转让系统9-其它</param>
        /// <param name="SIGN_CLS">签约类别（非必传）0-T+0开通2-T+2开通5-T+5开通A-无需在本公司签署风险揭示书DD[SIGN_CLS]</param>
        /// <param name="SIGN_DATE">签约日期（非必传）</param>
        /// <param name="EFT_DATE">生效日期（非必传）</param>
        /// <param name="UNTRD_FLAG">报送标志（非必传）</param>
        /// <param name="PESEND_FLAG">重发标志</param>
        /// <param name="NEW_CUST_FNAME">新客户名称（非必输）</param>
        /// <param name="NEW_ID_TYPE">新证件类型（非必输）</param>
        /// <param name="NEW_ID_CODE">新证件号码（非必输）</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>业务流水号</returns>
        async private Task<string> submitStkAcctBizOpReq2NewZD(
                    string OPERATOR_TYPE,
                    string ACCTBIZ_EXCODE,
                    string YMT_CODE = "",
                    string CUST_CODE = "",
                    string INT_ORG = "",
                    string USER_TYPE = "",
                    string CUST_FNAME = "",
                    string ID_TYPE = "",
                    string ID_CODE = "",
                    string STKBD = "",
                    string TRDACCT_EXCLS = "",
                    string TRDACCT = "",
                    string TRDACCT_EX = "",
                    string BIRTHDAY = "",
                    string ID_BEG_DATE = "",
                    string ID_EXP_DATE = "",
                    string CITIZENSHIP = "",
                    string ID_ADDR = "",
                    string ADDRESS = "",
                    string ZIP_CODE = "",
                    string OCCU_TYPE = "",
                    string EDUCATION = "",
                    string TEL = "",
                    string MOBILE_TEL = "",
                    string NET_SERVICE = "",
                    string NET_SERVICEPASS = "",
                    string SEX = "",
                    string CHK_STATUS = "",
                    string ACCT_OPENTYPE = "",
                    string ACCT_TYPE = "",
                    string CORP_EXTYPE = "",
                    string EMAIL = "",
                    string FAX = "",
                    string ACCTBIZ_CLS = "",
                    string FIRST_TRD_DATE = "",
                    string PROPER_CLS = "",
                    string SIGN_CLS = "",
                    string SIGN_DATE = "",
                    string EFT_DATE = "",
                    string UNTRD_FLAG = "",
                    string PESEND_FLAG = "",
                    string NEW_CUST_FNAME = "",
                    string NEW_ID_TYPE = "",
                    string NEW_ID_CODE = "",
                    int timeout = 30
            )
        {
            // 前置条件判断
            if (OPERATOR_TYPE == "")
            {
                string message = "操作类型不能为空";
                logger.Error(message);
                throw new Exception(message);
            }
            if (ACCTBIZ_EXCODE == "")
            {
                string message = "业务类型不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "submitStkAcctBizOpReq2NewZD");
            request.setAttr("OPERATOR_TYPE", OPERATOR_TYPE);
            request.setAttr("ACCTBIZ_EXCODE", ACCTBIZ_EXCODE);
            request.setAttr("YMT_CODE", YMT_CODE);
            request.setAttr("CUST_CODE", CUST_CODE);
            request.setAttr("INT_ORG", INT_ORG);
            request.setAttr("USER_TYPE", USER_TYPE);
            request.setAttr("CUST_FNAME", CUST_FNAME);
            request.setAttr("ID_TYPE", ID_TYPE);
            request.setAttr("ID_CODE", ID_CODE);
            request.setAttr("STKBD", STKBD);
            request.setAttr("TRDACCT_EXCLS", TRDACCT_EXCLS);
            request.setAttr("TRDACCT", TRDACCT);
            request.setAttr("TRDACCT_EX", TRDACCT_EX);
            request.setAttr("BIRTHDAY", BIRTHDAY);
            request.setAttr("ID_BEG_DATE", ID_BEG_DATE);
            request.setAttr("ID_EXP_DATE", ID_EXP_DATE);
            request.setAttr("CITIZENSHIP", CITIZENSHIP);
            request.setAttr("ID_ADDR", ID_ADDR);
            request.setAttr("ADDRESS", ADDRESS);
            request.setAttr("ZIP_CODE", ZIP_CODE);
            request.setAttr("OCCU_TYPE", OCCU_TYPE);
            request.setAttr("EDUCATION", EDUCATION);
            request.setAttr("TEL", TEL);
            request.setAttr("MOBILE_TEL", MOBILE_TEL);
            request.setAttr("NET_SERVICE", NET_SERVICE);
            request.setAttr("NET_SERVICEPASS", NET_SERVICEPASS);
            request.setAttr("SEX", SEX);
            request.setAttr("CHK_STATUS", CHK_STATUS);
            request.setAttr("ACCT_OPENTYPE", ACCT_OPENTYPE);
            request.setAttr("ACCT_TYPE", ACCT_TYPE);
            request.setAttr("CORP_EXTYPE", CORP_EXTYPE);
            request.setAttr("EMAIL", EMAIL);
            request.setAttr("FAX", FAX);
            request.setAttr("ACCTBIZ_CLS", ACCTBIZ_CLS);
            request.setAttr("FIRST_TRD_DATE", FIRST_TRD_DATE);
            request.setAttr("PROPER_CLS", PROPER_CLS);
            request.setAttr("SIGN_CLS", SIGN_CLS);
            request.setAttr("SIGN_DATE", SIGN_DATE);
            request.setAttr("EFT_DATE", EFT_DATE);
            request.setAttr("UNTRD_FLAG", UNTRD_FLAG);
            request.setAttr("PESEND_FLAG", PESEND_FLAG);
            request.setAttr("NEW_CUST_FNAME", NEW_CUST_FNAME);
            request.setAttr("NEW_ID_TYPE", NEW_ID_TYPE);
            request.setAttr("NEW_ID_CODE", NEW_ID_CODE);

            // 调用WebService获取返回值
            Response response = await this.invoke(request);

            // 判断返回的操作结果是否异常
            if (response.flag != "1")
            {
                string message = "操作失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }

            return response.getValue("SERIAL_NO");

        }


        /// <summary>
        /// 实现 2.121 中登开立股东账户。 
        /// 注：该接口调用时，请确保OPEN_TYPE、ACCT_TYPE和STKBD的关系要对应正确。
        /// 如要开立深A信用股东卡，则OPEN_TYPE=3，ACCT_TYPE=24，STKBD=00； 
        /// </summary>
        /// <param name="OPEN_TYPE">业务类型（必传）0-A股账户开户1-基金账户开户2-B股账户开户3-信用账户开户</param>
        /// <param name="NEW_OPEN_FLAG">新开户标志（必传）0-已有1-新开</param>
        /// <param name="STKBD">交易板块（非必传）DD[STKBD]</param>
        /// <param name="USER_TYPE">客户类别（必传）DD[USER_TYPE]</param>
        /// <param name="CUST_FNAME">客户名称（非必传）</param>
        /// <param name="YMT_CODE">一码通代码（必传）</param>
        /// <param name="ACCT_TYPE">证券账户类别（非必传）</param>
        /// <param name="ID_TYPE">证件类型（必传）</param>
        /// <param name="ID_CODE">证件号码（必传）</param>
        /// <param name="INT_ORG">机构编码（非必传）</param>
        /// <param name="ACCT_OPENTYPE">开户方式（必传）DD[ACCT_OPENTYPE]</param>
        /// <param name="TRDACCT">证券账户号码（非必传）</param>
        /// <param name="TRDACCT_EX">配号证券账户号码（非必传）沪深A股信用证券账户开户时必填，沪市衍生品合约账户开户时必填，深市衍生品合约账户开户时必填，其他无需填写</param>
        /// <param name="STKPBU">交易单元（非必传）</param>
        /// <param name="FIRMID">指定结算参与人（非必传）</param>
        /// <param name="CITIZENSHIP">国籍（非必传）DD[CITIZENSHIP]</param>
        /// <param name="REMARK">备用字段（非必传）</param>
        /// <returns></returns>
        async private Task<Response> openStkAcctByNewZD(
                string OPEN_TYPE,
                string NEW_OPEN_FLAG,
                string USER_TYPE,
                string YMT_CODE,
                string ID_TYPE,
                string ID_CODE,
                string STKBD = "",
                string CUST_FNAME = "",
                string ACCT_TYPE = "",
                string INT_ORG = "",
                string ACCT_OPENTYPE = Dict.ACCT_OPENTYPE.客户网上自助,
                string TRDACCT = "",
                string TRDACCT_EX = "",
                string STKPBU = "",
                string FIRMID = "",
                string CITIZENSHIP = "",
                string REMARK = ""
            )
        {
            // 前置条件判断


            // 初始化请求
            Request request = new Request(this.operatorId, "openStkAcctByNewZD");

            request.setAttr("OPEN_TYPE", OPEN_TYPE);
            request.setAttr("NEW_OPEN_FLAG", NEW_OPEN_FLAG);
            request.setAttr("STKBD", STKBD);
            request.setAttr("USER_TYPE", USER_TYPE);
            request.setAttr("CUST_FNAME", CUST_FNAME);
            request.setAttr("YMT_CODE", YMT_CODE);
            request.setAttr("ACCT_TYPE", ACCT_TYPE);
            request.setAttr("ID_TYPE", ID_TYPE);
            request.setAttr("ID_CODE", ID_CODE);
            request.setAttr("INT_ORG", INT_ORG);
            request.setAttr("ACCT_OPENTYPE", ACCT_OPENTYPE);
            request.setAttr("TRDACCT", TRDACCT);
            request.setAttr("TRDACCT_EX", TRDACCT_EX);
            request.setAttr("STKPBU", STKPBU);
            request.setAttr("FIRMID", FIRMID);
            request.setAttr("CITIZENSHIP", CITIZENSHIP);
            request.setAttr("REMARK", REMARK);


            // 调用WebService获取返回值
            Response response = await this.invoke(request);

            // 判断返回的操作结果是否异常
            if (response.flag != "0" && response.flag != "1")
            {
                string message = "操作失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }

            // 返回结果
            return response;
        }

        /// <summary>
        /// 证券账户系统内开户（加挂股东账户）
        /// 实现 2.49 证券账户开户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="STKBD"></param>
        /// <param name="TRDACCT"></param>
        /// <param name="TRDACCT_NAME"></param>
        /// <returns></returns>
        async private Task<bool> openStkTrdAcct(User user, string STKBD = "", string TRDACCT = "", string TRDACCT_NAME = "")
        {
            // 前置条件判断
            if (user.cust_code == "")
            {
                throw new Exception("客户代码不能为空");
            }
            if (user.cuacct_code == "")
            {
                throw new Exception("资产账号不能为空");
            }
            if (user.id_type == "")
            {
                throw new Exception("证件类型不能为空");
            }
            if (user.id_code == "")
            {
                throw new Exception("证件编号不能为空");
            }
            if (STKBD == "")
            {
                throw new Exception("交易板块不能为空");
            }
            if (TRDACCT == "")
            {
                throw new Exception("交易账号不能为空");
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "openStkTrdAcct");
            request.setAttr("CUST_CODE", user.cust_code);
            request.setAttr("CUACCT_CODE", user.cuacct_code);
            request.setAttr("ID_TYPE", user.id_type);
            request.setAttr("ID_CODE", user.id_code);
            request.setAttr("STKBD", STKBD);
            request.setAttr("TRDACCT", TRDACCT);


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
        /// 证券账户业务信息查询（新） 
        /// 自动轮询中登处理结果，默认30秒超时。
        /// 注1：此接口的入参SERIAL_NO为submitStkAcctBizOpReq2NewZD的出参。 
        /// 注2：此接口为实时获取中登返回信息，根据返回参数ACCTBIZ_STATUS（0-未发送中登，1-已发送中登，2-处理成功，3-处理失败）来确定接口是否正确查询到了中登返回结果，需外围控制超时时间。
        /// 在超时时间范围内，需循环调用此接口，直至获取到ACCTBIZ_STATUS=2或是3。
        /// </summary>
        /// <param name="SERIAL_NO">流水序号</param>
        /// <param name="ACCTBIZ_EXCODE">业务类型 DD-[ACCTBIZ_EXCODE]</param>
        /// <param name="ACCT_TYPE">账户类型</param>
        /// <param name="TRDACCT">交易账户</param>
        /// <param name="INT_ORG">机构代码</param>
        /// <param name="ID_CODE">证件号码</param>
        /// <param name="timeout">超时时间，单位：秒</param>
        /// <param name="interval">每次轮询的间隔时间，单位：秒</param>
        /// <returns></returns>
        async private Task<Response> searchStkAcctBizInfo(
            string SERIAL_NO,
            string ACCTBIZ_EXCODE = "",
            string ACCT_TYPE = "",
            string TRDACCT = "",
            string INT_ORG = "",
            string ID_CODE = "",
            int timeout = 30,
            int interval = 3
         )
        {
            // 前置条件判断
            if (SERIAL_NO == "")
            {
                string msg = "流水号不能为空";
                logger.Error(msg);
                throw new Exception(msg);
            }

            bool result = false;
            string status = "";
            int sleepInterval = interval * 1000;
            int currentCostTime = 0;
            timeout = timeout * 1000;

            // 查询处理结果
            Request request = new Request(this.operatorId, "searchStkAcctBizInfo");
            request.setAttr("SERIAL_NO", SERIAL_NO);
            request.setAttr("ACCTBIZ_EXCODE", ACCTBIZ_EXCODE);
            request.setAttr("ACCT_TYPE", ACCT_TYPE);
            request.setAttr("TRDACCT", TRDACCT);
            request.setAttr("INT_ORG", INT_ORG);
            request.setAttr("ID_CODE", ID_CODE);

            Response response;

            while (result == false)
            {
                // 延时处理
                await Task.Delay(sleepInterval);

                // 计算是否超时
                currentCostTime += sleepInterval;
                if (currentCostTime > timeout)
                {
                    break;
                }

                // 调用WebService获取返回值
                response = await this.invoke(request);

                // 判断返回的操作结果是否异常
                if (response.flag != "1")
                {
                    string msg = "操作失败：" + response.prompt;
                    logger.Error(msg);
                    throw new Exception(msg);
                }

                // 判断账户业务处理状态
                status = response.getValue("ACCTBIZ_STATUS");
                if (status == Dict.ACCTBIZ_STATUS.处理失败)
                {
                    throw new Exception("“证券账户业务信息查询”处理失败，错误信息：" + response.getValue("RETURN_MSG"));
                }
                else if (status == Dict.ACCTBIZ_STATUS.处理成功)
                {
                    // 返回结果
                    return response;
                }
            }

            string message = "中登处理超时，没有返回结果。中登流水号：" + SERIAL_NO;
            logger.Error(message);
            throw new Exception(message);
        }

        /// <summary>
        /// 2.132 证券账户业务信息拓展查询
        /// 自动轮询中登处理结果，默认30秒超时。
        /// 注1：此接口的入参SERIAL_NO为submitStkAcctBizOpReq2NewZD的出参。
        /// </summary>
        /// <param name="SERIAL_NO"></param>
        /// <returns></returns>
        async private Task<Response> searchStkAcctBizInfoEx(
           string SERIAL_NO
        )
        {
            // 前置条件判断
            if (SERIAL_NO == "")
            {
                string message = "流水号不能为空";
                logger.Error(message);
                throw new Exception(message);
            }

            // 查询处理结果
            Request request = new Request(this.operatorId, "searchStkAcctBizInfoEx");
            request.setAttr("SERIAL_NO", SERIAL_NO);

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
        /// 直接发起新开客户号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        async private Task<Response> openCustomer(User user)
        {
            Request request = new Request(this.operatorId, "openCustomer");
            request.setAttr("USER_NAME", user.user_name); // 客户名称（必传）
            request.setAttr("USER_TYPE", user.user_type); // 用户类型（必传）dd[user_type]
            request.setAttr("ID_TYPE", user.id_type); // 证件类型（必传）dd[id_type]
            request.setAttr("ID_CODE", user.id_code); // 证件号码（必传）
            request.setAttr("USER_FNAME", user.user_fname); // 用户全称（必传）
            request.setAttr("ID_ISS_AGCY", user.id_iss_agcy); // 发证机关（必传）
            request.setAttr("ID_EXP_DATE", user.id_exp_date); // 证件有效日期（必传）
            request.setAttr("CITIZENSHIP", user.citizenship); // 国籍（必传）dd[citizenship]
            request.setAttr("NATIONALITY", user.nationality); // 民族（必传）dd[nationality]
            request.setAttr("CUST_CODE", user.cust_code); // 客户代码（非必传）
            request.setAttr("INT_ORG", user.int_org); // 机构代码（非必传）
            request.setAttr("ID_BEG_DATE", user.id_beg_date); // 证件开始日期（非必传）
            request.setAttr("ID_ZIP_CODE", user.id_zip_code); // 证件邮编（非必传）
            request.setAttr("ID_ADDR", user.id_addr); // 证件地址（非必传）
            request.setAttr("ZIP_CODE", user.zip_code); // 邮政编码（非必传）
            request.setAttr("ADDRESS", user.address); // 联系地址（非必传）
            request.setAttr("TEL", user.tel); // 联系电话（非必传）
            request.setAttr("FAX", user.fax); // 传真电话（非必传）
            request.setAttr("EMAIL", user.email); // 电子邮箱（非必传）
            request.setAttr("MOBILE_TEL", user.mobile_tel); // 移动电话（非必传）
            request.setAttr("EDUCATION", user.education); // 学历（非必传）dd[education]
            request.setAttr("NATIVE_PLACE", user.native_place); // 籍贯/注册地（非必传）
            request.setAttr("SEX", user.sex); // 性别（非必传）dd[sex]
            request.setAttr("BIRTHDAY", user.birthday); // 出生日期（非必传）
            request.setAttr("REMARK", user.remark); // 备注信息（非必传）
            request.setAttr("MARRY", user.marry); // 婚姻状况（非必传）dd[marry]
            request.setAttr("INTEREST", user.interest); // 兴趣爱好（非必传）
            request.setAttr("VEHICLE", user.vehicle); // 交通工具（非必传）dd[vehicle]
            request.setAttr("HOUSE_OWNER", user.house_owner); // 住宅所有权状况（非必传）
            request.setAttr("OFFICE_TEL", user.office_tel); // 办公电话（非必传）
            request.setAttr("WELL_TEL", user.well_tel); // 小灵通电话（非必传）
            request.setAttr("LINKTEL_ORDER", user.linktel_order); // 首选联系电话（非必传）
            request.setAttr("OFFICE_ADDR", user.office_addr); // 办公地址（非必传）
            request.setAttr("CORP_ADDR", user.corp_addr); // 公司地址（非必传）
            request.setAttr("LINKADDR_ORDER", user.linkaddr_order); // 首选联系地址（非必传）
            request.setAttr("CUST_CLS", user.cust_cls); // 客户类别（非必传）dd[cust_cls]
            request.setAttr("CUST_TYPE", user.cust_type); // 客户类型（非必传）dd[cust_type]
            request.setAttr("CHANNELS", user.channels); // 操作渠道（非必传）dd[channel]
            request.setAttr("CRITERION", user.criterion); // 规范客户标志（非必传）dd[criterion]
            request.setAttr("RISK_FACTOR", user.risk_factor); // 风险因素（非必传）dd[risk_factor]
            request.setAttr("CREDIT_LVL", user.credit_lvl); // 信用级别（非必传）
            request.setAttr("REMOTE_PROTOCOL", user.remote_protocol); // 远程签署协议（非必传）
            request.setAttr("CUST_SOURCE", user.cust_source); // 客户来源（非必传）
            request.setAttr("SERVICE_LVL", user.service_lvl); // 服务级别（非必传）
            request.setAttr("BSB_USER_FNAME", user.bsb_user_fname); // 签约客户姓名（非必传）
            request.setAttr("BSB_ID_TYPE", user.bsb_id_type); // 签约证件类型（非必传）dd[id_type]
            request.setAttr("BSB_ID_CODE", user.bsb_id_code); // 签约证件号码（非必传）
            request.setAttr("BSB_ID_EXP_DATE", user.bsb_id_exp_date); // 签约证件有效日期（非必传）
            request.setAttr("CARD_ID", user.card_id); // 磁卡号码（非必传）
            request.setAttr("CHK_RIGHT_FLAG", user.chk_right_flag); // 校验标志（非必传）
            request.setAttr("OPEN_SOURCE", user.open_source); // 开户来源（非必传）dd[open_source]
            request.setAttr("OTHER_REMARK", user.other_remark); // 其它备注（非必传）
            request.setAttr("SPEC_REMARK", user.spec_remark); // 特殊备注（非必传）
            request.setAttr("LINTEL_PD", user.lintel_pd); // 联络频度（非必传）
            request.setAttr("AML_LVL", user.aml_lvl); // 反洗钱等级（非必传）dd[aml_lvl]
            request.setAttr("FILINGCABINET_NO", user.filingcabinet_no); // 文件柜编号（非必传）
            request.setAttr("CUST_GRP", user.cust_grp); // 客户分组（非必传）dd[cust_grp]
            request.setAttr("IDCARD_TYPE", user.idcard_type); // 证件卡类型（非必传）
            request.setAttr("IDCARD_CHECK_FLAG", user.idcard_check_flag); // 证件卡校验标志（非必传）dd[idcard_check_flag]
            request.setAttr("OPEN_AGENT", user.open_agent); // 开户代理人（非必传）
            request.setAttr("SUBJECT_IDENTITY", user.subject_identity); // 主体身份（非必传）
            request.setAttr("INCOME", user.income); // 年收入（非必传）
            request.setAttr("INOUTSIDE_IDENTITY", user.inoutside_identity); // 境内外身份（非必传）
            request.setAttr("SPECIAL_STATUS", user.special_status); // 特殊身份（非必传）
            request.setAttr("ENTERPRISE_LEVEL", user.enterprise_level); // 企业层级（非必传）
            request.setAttr("SZORGTYPE", user.szorgtype); // 机构产品类别（深）（非必传）
            request.setAttr("NATIONAL_ATTR", user.national_attr); // 国有属性（非必传）dd[national_attr]
            request.setAttr("LISTED_ATTR", user.listed_attr); // 上市属性（非必传）dd[listed_attr]
            request.setAttr("IDCARD_READ_FLAG", user.idcard_read_flag); // 证件卡读卡标志（非必传）
            request.setAttr("MAIN_YEAR_CHK_DATE", user.main_year_chk_date); // 主证件年检日期（非必传）
            request.setAttr("WORKPLACE", user.workplace); // 工作单位（非必传）
            request.setAttr("TRADE", user.trade); // 行业类型（非必传）
            request.setAttr("OCCU_TYPE", user.occu_type); // 当前职业（非必传）
            request.setAttr("TEL_CHK_FLAG", user.tel_chk_flag); // 手机校验标识（非必传）
            request.setAttr("EMAIL_CHK_FLAG", user.email_chk_flag); // 邮箱校验标识（非必传）
            request.setAttr("FIN_EDU_FLAG", user.fin_edu_flag); // 金融相关专业学历校验标识（非必传）

            Response response = await this.invoke(request);
            if (response.flag != "1")
            {
                string message = "操作失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }
            return response;
        }

        /// <summary>
        /// 直接发起新开资金账户
        /// </summary>
        /// <param name="USER_CODE">用户代码 (必传)</param>
        /// <param name="CUACCT_CLS">资产账户类别（必传） DD[CUACCT_CLS]</param>
        /// <param name="CUACCT_LVL">资产账户级别（必传） DD[CUACCT_LVL]</param>
        /// <param name="CUACCT_GRP">资产账户组别（必传） DD[CUACCT_GRP]</param>
        /// <param name="CURRENCY">货币（必传） DD[CURRENCY]</param>
        /// <param name="INT_ORG">内部机构（非必传</param>
        /// <param name="CUACCT_CODE">资产账户（非必传）</param>
        /// <param name="FUND_TYPE">帐户类型（非必传）</param>
        /// <param name="OP_REMARK">备注信息（非必传）</param>
        /// <returns></returns>
        async private Task<Response> openCuacct(string USER_CODE, string CUACCT_CLS = "", string CUACCT_LVL = "0", string CUACCT_GRP = "0", string CURRENCY = "0", string INT_ORG = "", string CUACCT_CODE = "", string FUND_TYPE = "", string OP_REMARK = "")
        {
            Request request = new Request(this.operatorId, "openCuacct");
            request.setAttr("USER_CODE", USER_CODE);
            if (CUACCT_CLS != "") request.setAttr("CUACCT_CLS", CUACCT_CLS);
            request.setAttr("CUACCT_LVL", CUACCT_LVL);
            request.setAttr("CUACCT_GRP", CUACCT_GRP);
            request.setAttr("CURRENCY", CURRENCY);
            request.setAttr("INT_ORG", INT_ORG);
            request.setAttr("CUACCT_CODE", CUACCT_CODE);
            request.setAttr("FUND_TYPE", FUND_TYPE);
            request.setAttr("OP_REMARK", OP_REMARK);

            Response response = await this.invoke(request);
            if (response.flag != "1")
            {
                string message = "操作失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }
            return response;
        }

        /// <summary>
        /// 发起一步式签约
        /// 尚未完成
        /// </summary>
        /// <param name="OP_TYPE">操作类型OP_TYPE为0时表示券商发起银证开户一步式，为1时表示券商发起预指定，即两步式中的第一步，为2时BANK_ACCT、FUND_AUTH_DATA、BANK_AUTH_DATA均传空。</param>
        /// <param name="CUST_CODE">客户代码</param>
        /// <param name="CUACCT_CODE">资金代码</param>
        /// <param name="BANK_ACCT_CODE">银行账户卡号</param>
        /// <param name="EXT_ORG">外部机构</param>
        /// <param name="BANK_ACCT">外部银行账户</param>
        /// <param name="BANK_AUTH_DATA">银行密码</param>
        /// <param name="FUND_AUTH_DATA">资金密码</param>
        /// <param name="SERIAL_NO">流水序号</param>
        /// <param name="SMS_NO">短信验证码</param>
        /// <param name="CUBSB_TYPE">银证业务类型DD[CUBSB_TYPE]</param>
        /// <param name="CURRENCY"></param>
        /// <returns></returns>
        async private Task<Response> cubsbScOpenAcctOneStep(string CUST_CODE, string CUACCT_CODE, string BANK_ACCT_CODE, string EXT_ORG = "", string BANK_ACCT = "", string BANK_AUTH_DATA = "", string FUND_AUTH_DATA = "", string SERIAL_NO = "", string SMS_NO = "", string OP_TYPE = "0", string CUBSB_TYPE = "16", string CURRENCY = "0")
        {
            Request request = new Request(this.operatorId, "cubsbScOpenAcct");
            request.setAttr("OP_TYPE", "0");
            request.setAttr("CURRENCY", CURRENCY);
            request.setAttr("CUST_CODE", CUST_CODE);
            request.setAttr("CUACCT_CODE", CUACCT_CODE);
            request.setAttr("BANK_ACCT_CODE", BANK_ACCT_CODE);
            //request.setAttr("EXT_ORG", EXT_ORG);
            //request.setAttr("BANK_ACCT", BANK_ACCT);
            //request.setAttr("FUND_AUTH_DATA", FUND_AUTH_DATA);
            //request.setAttr("BANK_AUTH_DATA", BANK_AUTH_DATA);
            //request.setAttr("SERIAL_NO", SERIAL_NO);
            //request.setAttr("SMS_NO", SMS_NO);
            request.setAttr("CUBSB_TYPE", "16"); // 券商发起-银证开户

            Response response = await this.invoke(request);
            if (response.flag != "1")
            {
                string message = "操作失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }

            return response;
        }

        /// <summary>
        /// 查询银证业务流水
        /// </summary>
        /// <param name="SERIAL_NO"></param>
        /// <param name="OCCUR_DATE"></param>
        /// <param name="CURRENCY"></param>
        /// <param name="CUACCT_CODE"></param>
        /// <param name="EXT_ORG"></param>
        /// <param name="INT_ORG"></param>
        /// <returns></returns>
        async private Task<Response> getCubsbLog(string SERIAL_NO, string OCCUR_DATE, string CURRENCY, string CUACCT_CODE, string EXT_ORG, string INT_ORG)
        {
            Request request = new Request(this.operatorId, "getCubsbLog");
            request.setAttr("SERIAL_NO", SERIAL_NO);

            Response response = await this.invoke(request);
            if (response.flag != "1")
            {
                string message = "操作失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }

            return response;
        }


        /// <summary>
        /// 查询用户基本资料
        /// </summary>
        /// <param name="userCode">客户代码</param>
        /// <returns></returns>
        async public Task<Response> queryCustBasicInfoList(string userCode)
        {
            Request request = new Request(this.operatorId, "queryCustBasicInfoList");
            request.setAttr("USER_CODE", userCode);

            Response response = await this.invoke(request);
            if (response.flag != "1")
            {
                string message = "操作失败：" + response.prompt;
                logger.Error(message);
                throw new Exception(message);
            }

            return response;
        }

        /// <summary>
        /// 根据请求参数调用金证WebService的对应接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        async public Task<Response> invoke(Request request)
        {
            string result = "";

            try
            {
                // 利用反射调用WebService的成员函数
                result = await execute(request);

                // 检查是否提示操作员已经退出。如果操作员已经退出，则先重新登录然后再次执行
                if (_autoRelogin && result.IndexOf("<prompt>您必须先登陆，才能进行其它操作。</prompt>") > -1)
                {
                    await this.operatorLogin();

                    result = await execute(request);
                }
            }
            catch (Exception ex)
            {
                string message = "WebService调用失败：" + this.kessWebserviceURL + " " + ex.Message;
                logger.Error(message);
                throw new Exception(message);
            }

            return new Response(result);
        }

        /// <summary>
        /// 异步方式调用WebService
        /// </summary>
        /// <param name="methonName"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        async private Task<string> execute(Request request)
        {
            System.Reflection.MethodInfo method = this.kessClientType.GetMethod(request.methonName);
            if (method == null)
            {
                throw new Exception("“" + request.methonName + "”接口不存在或暂不支持！");
            }

            // 请求队列数+1
            _requestQueueCount++;

            // 可用执行器的索引号
            int index = -1;

            // 查找可用的执行器
            while (index == -1)
            {
                for (int i = 0; i < kessClientList.Count; i++)
                {
                    lock (kessClientList[i])
                    {
                        if (kessClientList[i].available == true)
                        {
                            kessClientList[i].available = false;
                            index = i;
                            break;
                        }
                    }
                }

                // 无可用时延迟再试
                if (index == -1)
                {
                    await Task.Delay(10);
                }
            }

            // 找到可用的执行器之后，请求队列数-1
            _requestQueueCount--;

            return await Task.Run(() =>
            {
                // 调用WebService接口，获取返回值

                _webserviceConnectionsNum += 1;

                logger.Info("执行器" + index.ToString() + "调用Webservice功能<" + request.methonName + ">|" + request.xml);

                stopWatch.Restart();

                string result = "";
                try
                {
                    result = (string)method.Invoke(kessClientList[index].executor, new object[] { request.xml });
                    if (result == null)
                    {
                        throw new Exception("服务器返回Null");
                    }
                    if (result == "")
                    {
                        throw new Exception("服务器没有返回数据");
                    }
                }
                catch (Exception)
                {
                    _webserviceConnectionsNum -= 1;
                    kessClientList[index].available = true;
                    throw;
                }

                //获取stopWatch从开始到现在的时间差，单位是毫秒
                long diff = stopWatch.ElapsedMilliseconds;
                stopWatch.Stop();   //停止计时

                logger.Info("执行器" + index.ToString() + "响应Webservice功能<" + request.methonName + ">，耗时" + diff.ToString() + "毫秒|" + result);

                _webserviceConnectionsNum -= 1;

                kessClientList[index].available = true;

                return result;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// 通过XML字符串创建DataSet对象
        /// </summary>
        /// <param name="xmlString"></param>
        private DataSet createDataSetFromXmlString(string xmlString)
        {
            DataSet ds = new DataSet();
            using (StringReader xmlSR = new StringReader(xmlString))
            {
                // 读取xml到DataSet
                // XmlReadMode.InferTypedSchema：忽视任何内联架构，从数据推断出强类型架构并加载数据。如果无法推断，则解释成字符串数据
                ds.ReadXml(xmlSR, XmlReadMode.Auto);
            }

            if (ds == null)
            {
                throw new Exception("DataSet读取XML失败");
            }
            return ds;
        }

        /// <summary>
        /// 客户协议维护
        /// 实现 2.59 客户协议维护
        /// </summary>
        /// <param name="OPERATION_TYPE">操作类型0-增加1-修改2-删除</param>
        /// <param name="CUST_CODE">客户代码</param>
        /// <param name="CUST_AGMT_TYPE">协议类型DD[CUST_AGMT_TYPE]</param>
        /// <param name="EXP_DATE">有效截止日期</param>
        /// <param name="EFT_DATE">生效日期</param>
        /// <param name="OP_REMARK">操作备注</param>
        /// <param name="TRDACCT">交易账户</param>
        /// <param name="STKBD">交易板块DD[STKBD]</param>
        /// <param name="OPEN_TYPE">开通类型0-T+02-T+25-T+5A</param>
        /// <param name="OPEN_TYPE_WIN">w版开通类型0-T+02-T+25-T+5</param>
        /// <param name="SIGN_PLACE">签署地0-本证券公司1-其它证券公司</param>
        /// <param name="REDO_FLAG">是否重新申报1-重新申报</param>
        /// <param name="DEAL_STATUS">报送状态0-未报,1-已报,9-报送失败</param>
        /// <param name="SIGN_DATE">签署日期</param>
        /// <param name="FRT_BIZ_SN">任务流序号</param>
        /// <param name="BATCHNO">批次号</param>
        /// <param name="APPLY_DATE">报送时间</param>
        /// <param name="RETURN_MSG">应答原因</param>
        /// <param name="REMOTE_SYS">对接远程系统</param>
        /// <param name="ACCT_TYPE">开通类型</param>
        /// <param name="CUACCT_CODE">资产账户</param>
        /// <param name="AGMT_OPER_FLAG">协议操作标志</param>
        /// <param name="OPEN_FLAG">申请标志</param>
        /// <param name="SETT_DEALLOG">是否处理报送流水</param>
        /// <param name="CHECK_RES_RIGHT">检查资源权限</param>
        /// <returns></returns>
        async public Task<Response> setCustAgreement(
            string OPERATION_TYPE = "",
            string CUST_CODE = "",
            string CUST_AGMT_TYPE = "",
            string EXP_DATE = "",
            string EFT_DATE = "",
            string OP_REMARK = "",
            string TRDACCT = "",
            string STKBD = "",
            string OPEN_TYPE = "",
            string OPEN_TYPE_WIN = "",
            string SIGN_PLACE = "",
            string REDO_FLAG = "",
            string DEAL_STATUS = "0",
            string SIGN_DATE = "",
            string FRT_BIZ_SN = "",
            string BATCHNO = "",
            string APPLY_DATE = "",
            string RETURN_MSG = "",
            string REMOTE_SYS = "",
            string ACCT_TYPE = "",
            string CUACCT_CODE = "",
            string AGMT_OPER_FLAG = "",
            string OPEN_FLAG = "",
            string SETT_DEALLOG = "",
            string CHECK_RES_RIGHT = ""
        )
        {
            // 前置条件判断
            if (CUST_CODE == "")
            {
                throw new Exception("客户代码不能为空");
            }

            // 查询处理结果
            Request request = new Request(this.operatorId, "setCustAgreement");
            request.setAttr("OPERATION_TYPE", OPERATION_TYPE); // 操作类型0-增加1-修改2-删除
            request.setAttr("CUST_CODE", CUST_CODE); // 客户代码
            request.setAttr("CUST_AGMT_TYPE", CUST_AGMT_TYPE); // 协议类型DD[CUST_AGMT_TYPE]
            request.setAttr("EXP_DATE", EXP_DATE); // 有效截止日期
            request.setAttr("EFT_DATE", EFT_DATE); // 生效日期
            request.setAttr("OP_REMARK", OP_REMARK); // 操作备注
            request.setAttr("TRDACCT", TRDACCT); // 交易账户
            request.setAttr("STKBD", STKBD); // 交易板块DD[STKBD]
            request.setAttr("OPEN_TYPE", OPEN_TYPE); // 开通类型0-T+02-T+25-T+5A
            request.setAttr("OPEN_TYPE_WIN", OPEN_TYPE_WIN); // w版开通类型0-T+02-T+25-T+5
            request.setAttr("SIGN_PLACE", SIGN_PLACE); // 签署地0-本证券公司1-其它证券公司
            request.setAttr("REDO_FLAG", REDO_FLAG); // 是否重新申报1-重新申报
            request.setAttr("DEAL_STATUS", DEAL_STATUS); // 报送状态0-未报,1-已报,9-报送失败
            request.setAttr("SIGN_DATE", SIGN_DATE); // 签署日期
            request.setAttr("FRT_BIZ_SN", FRT_BIZ_SN); // 任务流序号
            request.setAttr("BATCHNO", BATCHNO); // 批次号
            request.setAttr("APPLY_DATE", APPLY_DATE); // 报送时间
            request.setAttr("RETURN_MSG", RETURN_MSG); // 应答原因
            request.setAttr("REMOTE_SYS", REMOTE_SYS); // 对接远程系统
            request.setAttr("ACCT_TYPE", ACCT_TYPE); // 开通类型
            request.setAttr("CUACCT_CODE", CUACCT_CODE); // 资产账户
            request.setAttr("AGMT_OPER_FLAG", AGMT_OPER_FLAG); // 协议操作标志
            request.setAttr("OPEN_FLAG", OPEN_FLAG); // 申请标志
            request.setAttr("SETT_DEALLOG", SETT_DEALLOG); // 是否处理报送流水
            request.setAttr("CHECK_RES_RIGHT", CHECK_RES_RIGHT); // 检查资源权限

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
