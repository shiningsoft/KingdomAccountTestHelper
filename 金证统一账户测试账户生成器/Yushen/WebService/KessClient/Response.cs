using System;
using System.IO;
using System.Xml;

namespace Yushen.WebService.KessClient
{
    class Response
    {
        XmlDocument xmlDoc = new XmlDocument();
        public string flag = "";
        public string prompt = "";
        public int length = 0;

        public Response(string xmlString)
        {
            this.createXmlDocumentFromString(xmlString);

            this.flag = xmlDoc.SelectSingleNode("/response/result/flag").InnerText;
            this.prompt = xmlDoc.SelectSingleNode("/response/result/prompt").InnerText;
            this.length = int.Parse(xmlDoc.SelectSingleNode("/response/result/length").InnerText);

            this.checkResult();
        }

        internal void checkResult()
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
        }

        public string getRecord()
        {
            return xmlDoc.SelectSingleNode("/response/record").InnerXml;
        }

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
        internal string formatXml(string xml)
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

        internal void createXmlDocumentFromString(string xmlString)
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
    }
}
