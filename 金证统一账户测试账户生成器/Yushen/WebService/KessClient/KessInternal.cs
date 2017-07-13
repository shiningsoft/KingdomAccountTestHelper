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
        internal string openCustomer(string USER_NAME, string ID_CODE, string ID_ISS_AGCY, string ID_BEG_DATE, string ID_EXP_DATE, string CITIZENSHIP, string NATIONALITY)
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

        /// <summary>
        /// 记录错误日志并且抛出一个异常
        /// </summary>
        /// <param name="message">异常消息内容</param>
        internal void throwNewException(string message)
        {
            logger.Error(message);
            throw new Exception(message);
        }

        /// <summary>
        /// 通过XML字符串创建DataSet对象
        /// </summary>
        /// <param name="xmlString"></param>
        internal DataSet createDataSetFromXmlString(string xmlString)
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
