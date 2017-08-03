using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

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
            request.setAttr("USER_CODE", user.user_code);    // 客户名称

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
