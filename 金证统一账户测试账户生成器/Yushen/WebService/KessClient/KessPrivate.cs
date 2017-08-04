using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yushen.WebService.KessClient
{
    /// <summary>
    /// 用于金证统一账户系统WebService接口的操作类
    /// </summary>
    /// 
    /// 此文件存储内部方法
    /// 
    public partial class Kess
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
        private object kessClient;

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
        /// <returns>业务流水号</returns>
        private Response submitStkAcctBizOpReq2NewZD(string OPERATOR_TYPE, string ACCTBIZ_EXCODE, string YMT_CODE = "", string CUST_CODE = "", string INT_ORG = "", string USER_TYPE = "", string CUST_FNAME = "", string ID_TYPE = "", string ID_CODE = "", string STKBD = "", string TRDACCT_EXCLS = "", string TRDACCT = "", string TRDACCT_EX = "", string BIRTHDAY = "", string ID_BEG_DATE = "", string ID_EXP_DATE = "", string CITIZENSHIP = "", string ID_ADDR = "", string ADDRESS = "", string ZIP_CODE = "", string OCCU_TYPE = "", string EDUCATION = "", string TEL = "", string MOBILE_TEL = "", string NET_SERVICE = "", string NET_SERVICEPASS = "", string SEX = "", string CHK_STATUS = "", string ACCT_OPENTYPE = "1", string ACCT_TYPE = "", string CORP_EXTYPE = "", string EMAIL = "", string FAX = "", string ACCTBIZ_CLS = "", string FIRST_TRD_DATE = "", string PROPER_CLS = "", string SIGN_CLS = "", string SIGN_DATE = "", string EFT_DATE = "", string UNTRD_FLAG = "", string PESEND_FLAG = "", string NEW_CUST_FNAME = "", string NEW_ID_TYPE = "", string NEW_ID_CODE = "")
        {
            // 前置条件判断
            if (OPERATOR_TYPE == "")
            {
                throwNewException("必要参数操作类型不能为空");
            }
            if (ACCTBIZ_EXCODE == "")
            {
                throwNewException("必要参数业务类型不能为空");
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
            Response response = new Response(this.invoke(request));

            // 判断返回的操作结果是否异常
            if (response.flag != "1")
            {
                throw new Exception("操作失败：" + response.prompt);
            }

            string serialNo = response.getValue("SERIAL_NO");

            // 查询中登处理结果，并返回
            bool result = false;
            string status = "";
            bool isTimeOut = false;
            int sleepInterval = 3000;
            int currentCostTime = 0;
            int timeout = 30 * 1000;

            while (result == false && isTimeOut == false)
            {
                response = searchStkAcctBizInfo(serialNo, ACCTBIZ_EXCODE);

                // 判断账户业务处理状态
                status = response.getValue("ACCTBIZ_STATUS");
                if (status == "2" || status == "3")
                {
                    return response;
                }

                // 延时处理
                Task.Delay(sleepInterval);
                // Thread.Sleep(sleepInterval);

                // 计算是否超时
                currentCostTime += sleepInterval;
                if (currentCostTime > timeout)
                {
                    isTimeOut = true;
                }
            }

            throw new Exception("中登处理超时，未查到结果。中登流水号：" + serialNo);
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
        private Response openStkAcctByNewZD(
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
                string ACCT_OPENTYPE = "1",
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
            Response response = new Response(this.invoke(request));

            // 判断返回的操作结果是否异常
            if (response.flag != "0" && response.flag != "1")
            {
                throw new Exception("操作失败：" + response.prompt);
            }

            // 返回结果
            return response;
        }

        /// <summary>
        /// 2.120 证券账户业务信息查询（新） 
        /// 注1：此接口的入参SERIAL_NO为2.119的出参。 
        /// 注2：此接口为实时获取中登返回信息，根据返回参数ACCTBIZ_STATUS（0-未发送中登，1-已发送中登，2-处理成功，3-处理失败）来确定接口是否正确查询到了中登返回结果，需外围控制超时时间。
        /// 在超时时间范围内，需循环调用此接口，直至获取到ACCTBIZ_STATUS=2或是3。
        /// </summary>
        /// <param name="SERIAL_NO">流水序号</param>
        /// <param name="ACCTBIZ_EXCODE">业务类型 DD-[ACCTBIZ_EXCODE]</param>
        /// <param name="ACCT_TYPE">账户类型</param>
        /// <param name="TRDACCT">交易账户</param>
        /// <param name="INT_ORG">机构代码</param>
        /// <param name="ID_CODE">证件号码</param>
        /// <returns></returns>
        private Response searchStkAcctBizInfo(string SERIAL_NO, string ACCTBIZ_EXCODE, string ACCT_TYPE = "", string TRDACCT = "", string INT_ORG = "", string ID_CODE = "")
        {
            // 前置条件判断
            if (SERIAL_NO == "")
            {
                throwNewException("必要参数流水号不能为空");
            }
            if (ACCTBIZ_EXCODE == "")
            {
                throwNewException("必要参数业务类型不能为空");
            }

            // 初始化请求
            Request request = new Request(this.operatorId, "searchStkAcctBizInfo");
            request.setAttr("SERIAL_NO", this.operatorId);
            request.setAttr("ACCTBIZ_EXCODE", ACCTBIZ_EXCODE);
            request.setAttr("ACCT_TYPE", ACCT_TYPE);
            request.setAttr("TRDACCT", TRDACCT);
            request.setAttr("INT_ORG", INT_ORG);
            request.setAttr("ID_CODE", ID_CODE);

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
        /// 直接发起新开客户号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private Response openCustomer(User user)
        {
            Request request = new Request(this.operatorId, "openCustomer");
            request.setAttr("USER_NAME", user.user_name);    // 客户名称
            request.setAttr("ID_CODE", user.id_code);        // 证件号码
            request.setAttr("USER_FNAME", user.user_name);   // 用户全称
            request.setAttr("ID_ISS_AGCY", user.id_iss_agcy);    // 发证机关
            request.setAttr("ID_BEG_DATE", user.id_beg_date);    // 证件开始日期
            request.setAttr("ID_EXP_DATE", user.id_exp_date);    // 证件有效日期
            request.setAttr("CITIZENSHIP", user.citizenship);    // 国籍
            request.setAttr("NATIONALITY", user.nationality);    // 民族

            Response response = new Response(this.invoke(request));
            if (response.flag != "1")
            {
                throw new Exception("操作失败：" + response.prompt);
            }
            return response;
        }

        /// <summary>
        /// 直接发起新开资金账户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private Response openCuacct(User user)
        {
            Request request = new Request(this.operatorId, "openCuacct");
            request.setAttr("USER_CODE", user.cust_code);    // 客户名称

            Response response = new Response(this.invoke(request));
            if (response.flag != "1")
            {
                throw new Exception("操作失败：" + response.prompt);
            }
            return response;
        }

        /// <summary>
        /// 发起一步式签约
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
        private Response cubsbScOpenAcctOneStep( string CUST_CODE, string CUACCT_CODE,string BANK_ACCT_CODE, string EXT_ORG="",string BANK_ACCT="",string BANK_AUTH_DATA="", string FUND_AUTH_DATA = "", string SERIAL_NO="", string SMS_NO="", string OP_TYPE = "0", string CUBSB_TYPE = "16", string CURRENCY = "0")
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

            Response response = new Response(this.invoke(request));
            if (response.flag != "1")
            {
                throw new Exception("操作失败：" + response.prompt);
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
        private Response getCubsbLog(string SERIAL_NO,string OCCUR_DATE,string CURRENCY, string CUACCT_CODE,string EXT_ORG,string INT_ORG)
        {
            Request request = new Request(this.operatorId, "getCubsbLog");
            request.setAttr("SERIAL_NO", SERIAL_NO); 

            Response response = new Response(this.invoke(request));
            if (response.flag != "1")
            {
                throw new Exception("操作失败：" + response.prompt);
            }
            
            return response;
        }


        /// <summary>
        /// 查询用户基本资料
        /// </summary>
        /// <param name="userCode">客户代码</param>
        /// <returns></returns>
        private Response queryCustBasicInfoList(string userCode)
        {
            Request request = new Request(this.operatorId, "queryCustBasicInfoList");
            request.setAttr("USER_CODE", userCode);

            Response response = new Response(this.invoke(request));
            if (response.flag != "1")
            {
                throw new Exception("操作失败：" + response.prompt);
            }

            return response;
        }

        /// <summary>
        /// 根据请求参数调用金证WebService的对应接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string invoke(Request request)
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

        /// <summary>
        /// 记录错误日志并且抛出一个异常
        /// </summary>
        /// <param name="message">异常消息内容</param>
        private void throwNewException(string message)
        {
            logger.Error(message);
            throw new Exception(message);
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
    }
}
