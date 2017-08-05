using System;
using System.Data;
using System.IO;
using System.Xml;

namespace Yushen.WebService.KessClient
{
    /// <summary>
    /// 统一WebService响应结果类
    /// </summary>
    public class Response
    {
        /// <summary>
        /// 用于存储WebService返回值的Xml文档对象
        /// </summary>
        private XmlDocument xmlDoc = new XmlDocument();
        /// <summary>
        /// WebService返回的服务器处理结果
        /// </summary>
        public string flag = "";
        /// <summary>
        /// WebService返回的服务器消息
        /// </summary>
        public string prompt = "";
        /// <summary>
        /// WebService返回的查询结果条数
        /// </summary>
        public int length = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xmlString"></param>
        public Response(string xmlString)
        {
            this.createXmlDocumentFromString(xmlString);

            if (xmlDoc.SelectSingleNode("/response/result/flag")==null)
            {
                throw new Exception("找不到属性/response/result/flag");
            }
            if (xmlDoc.SelectSingleNode("/response/result/prompt") == null)
            {
                throw new Exception("找不到属性/response/result/prompt");
            }
            if (xmlDoc.SelectSingleNode("/response/result/length") == null)
            {
                throw new Exception("找不到属性/response/result/length");
            }
            if (xmlDoc.SelectSingleNode("/response/record") == null)
            {
                throw new Exception("找不到属性/response/record");
            }

            this.flag = xmlDoc.SelectSingleNode("/response/result/flag").InnerText;
            this.prompt = xmlDoc.SelectSingleNode("/response/result/prompt").InnerText;
            this.length = int.Parse(xmlDoc.SelectSingleNode("/response/result/length").InnerText);

            this.validResult();
        }

        /// <summary>
        /// 对服务器返回的XML内容和操作结果进行检查，如果不符合标准格式则抛出异常
        /// </summary>
        private void validResult()
        {
            XmlNode node = xmlDoc.SelectSingleNode("/response/result");
            if (node == null)
            {
                throw new Exception("找不到属性：/response/result");
            }
            else if (node.SelectSingleNode("flag").InnerText != "1" && node.SelectSingleNode("flag").InnerText != "0")
            {
                throw new Exception("操作结果异常，flag=" + node.SelectSingleNode("flag").InnerText + "，应为0或1。错误消息：" + node.SelectSingleNode("prompt").InnerText);
            }

            // 验证length值与record中的结果数量是否一致
            int rowCount = xmlDoc.SelectNodes("/response/record/row").Count;
            if (length == 0)
            {
                bool rowWithChildNodes = rowCount == 1 && xmlDoc.SelectSingleNode("/response/record/row").ChildNodes.Count > 0;
                bool rowCountMoreThanOne = rowCount > 1;

                if (rowWithChildNodes || rowCountMoreThanOne)
                {
                    // length=0但是有结果
                    throw new Exception("返回值中length=0但是record节点有结果");
                }
            }
            else if (length == 1)
            {
                bool rowCountMoreThanOne = xmlDoc.SelectNodes("/response/record/row").Count > 1;
                if (rowCountMoreThanOne)
                {
                    throw new Exception("length=1但是返回值中row的数量等于" + rowCount.ToString());
                }
            }
            else if (length > 1)
            {
                if (length != rowCount)
                {
                    throw new Exception("返回值中row的数量" + rowCount.ToString() + "与length值" + length.ToString() + "不一致");
                }
            }
            else
            {
                throw new Exception("length的值小于0：" + length.ToString());
            }
        }

        /// <summary>
        /// WebService返回结果中record节点的InnerXml值
        /// </summary>
        public string record
        {
            get
            {
                return xmlDoc.SelectSingleNode("/response/record").InnerXml;
            }
        }

        /// <summary>
        /// 通过xpath获取单一节点的txt内容
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public string getSingleNodeText(string xpath)
        {
            XmlNode node = xmlDoc.SelectSingleNode(xpath);
            if (node==null)
            {
                throw new Exception(xpath + "找不到");
            }
            return xmlDoc.SelectSingleNode(xpath).InnerText;
        }

        /// <summary>
        /// 返回record中第一行对应字段名称的值
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <returns></returns>
        public string getValue(string columnName)
        {
            return getSingleNodeText("/response/record/row/" + columnName);
        }

        /// <summary>
        /// WebService返回结果的Xml原始字符串
        /// </summary>
        public string xml
        {
            get
            {
                return this.formatXml(this.xmlDoc.InnerXml);
            }
        }

        /// <summary>
        /// 格式化输出XML字符串
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private string formatXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            StringWriter sw = new StringWriter();
            using (XmlTextWriter writer = new XmlTextWriter(sw))
            {
                writer.Indentation = 2;  // the Indentation
                writer.Formatting = Formatting.Indented;
                doc.WriteContentTo(writer);
                writer.Close();
            }

            return sw.ToString();
        }

        /// <summary>
        /// 通过XML字符串生成XmlDocument对象
        /// </summary>
        /// <param name="xmlString"></param>
        private void createXmlDocumentFromString(string xmlString)
        {
            try
            {
                StringReader reader = new StringReader(xmlString);
                xmlDoc.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("读取response内容失败：" + ex.Message);
            }
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
        /// 通过XML字符串创建DataTable对象
        /// </summary>
        /// <param name="xmlString"></param>
        private DataTable createDataTableFromXmlString(string xmlString)
        {
            DataTable dt = new DataTable();
            using (StringReader xmlSR = new StringReader(xmlString))
            {
                // 读取xml到DataSet
                // XmlReadMode.InferTypedSchema：忽视任何内联架构，从数据推断出强类型架构并加载数据。如果无法推断，则解释成字符串数据
                XmlTextReader xr = new XmlTextReader(xmlSR);
                dt.ReadXml(xmlSR);
            }

            if (dt == null)
            {
                throw new Exception("DataSet读取XML失败");
            }
            return dt;
        }

        public DataSet DataSet
        {
            get
            {
                DataSet ds = createDataSetFromXmlString(this.xml);
                return ds;
            }
        }

        /// <summary>
        /// 清空record节点的所有内容，并将length设置为0
        /// </summary>
        public void empty()
        {
            this.length = 0;
            this.xmlDoc.SelectSingleNode("/response/record").RemoveAll();
        }
    }
}
