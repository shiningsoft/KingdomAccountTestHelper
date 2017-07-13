using System;
using System.Xml;

namespace Yushen.WebService.KessClient
{
    class Request
    {
        /// <summary>
        /// Webservice操作的名称
        /// </summary>
        public string operateName;

        string xmlPath = "Yushen/WebService/KessClient/Xml/";
        XmlDocument xmlDoc = new XmlDocument();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="operatorId">操作员代码</param>
        /// <param name="operateName">Webservice操作的名称</param>
        public Request(string operatorId, string operateName)
        {
            this.getXmlDocumentFromFile(operateName);

            // 设置操作员代码
            this.setOperator(operatorId);
            this.operateName = operateName;
        }

        /**
         * 根据关键字设置某个值的属性
         * 
         **/
        public void setAttr(string name, string value)
        {
            XmlNode node = xmlDoc.SelectSingleNode("/request/data/" + name);
            if (node == null)
            {
                throw new Exception("找不到属性：" + name);
            }
            node.InnerText = value;
        }

        /**
         * 返回XML字符串
         * 
         **/
        public string xml
        {
            get
            {
                return xmlDoc.InnerXml;
            }
        }

        // 设置本次请求的操作员
        internal void setOperator(string id)
        {
            XmlNode node = xmlDoc.SelectSingleNode("/request/auth/OPERATOR");
            if (node == null)
            {
                throw new Exception("找不到属性：/request/auth/OPERATOR");
            }
            node.InnerText = id;
        }

        /**
         * 读取用于生成WebService请求的Xml配置文件
         * 
         **/
        internal void getXmlDocumentFromFile(string xmlFilename = "")
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;//忽略文档里面的注释
                xmlPath = xmlPath + xmlFilename + ".xml";
                Console.WriteLine("加载request配置文件：" + xmlPath);
                XmlReader reader = XmlReader.Create(@xmlPath, settings);
                xmlDoc.Load(reader);
                reader.Close();
            }
            catch (Exception)
            {
                throw new Exception("WebService XML文件加载失败：" + xmlPath);
            }
        }
    }
}
