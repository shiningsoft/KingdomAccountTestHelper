using System;
using System.Drawing;
using System.Xml;

namespace Yushen.WebService.KessClient
{
    public class Request
    {
        /// <summary>
        /// Webservice操作的名称
        /// </summary>
        public string methonName;

        /// <summary>
        /// 存放XML文件的相对路径
        /// </summary>
        public static string xmlPath = "Yushen/WebService/KessClient/Xml/";

        XmlDocument xmlDoc = new XmlDocument();

        /// <summary>
        /// 图片附件，上传照片时作为第二个参数使用。
        /// </summary>
        // Image img;

        /// <summary>
        /// 创建Request对象
        /// </summary>
        /// <param name="operatorId">操作员代码</param>
        /// <param name="methonName">Webservice操作的名称</param>
        /// <param name="isIgnoreComments">是否忽略XML文件中的注释，默认忽略</param>
        public Request(string operatorId, string methonName, bool isIgnoreComments = true)
        {
            this.getXmlDocumentFromFile(methonName, isIgnoreComments);

            // 设置操作员代码
            this.setOperator(operatorId);
            this.methonName = methonName;

            validate();
        }

        /// <summary>
        /// 通过XML字符串创建Request对象
        /// </summary>
        /// <param name="operatorId">操作员代码</param>
        /// <param name="operateName">Webservice操作的名称</param>
        /// <param name="xml">XML字符串</param>
        public Request(string operatorId, string operateName, string xml)
        {
            xmlDoc.LoadXml(xml);

            // 设置操作员代码
            this.setOperator(operatorId);
            this.methonName = operateName;

            validate();
        }

        /// <summary>
        /// 根据关键字设置某个值的属性
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="value">字段值</param>
        public void setAttr(string name, string value)
        {
            XmlNode node = xmlDoc.SelectSingleNode("/request/data/" + name);
            if (node == null)
            {
                throw new Exception("在" + methonName + "的xml模板文件中找不到属性：" + name);
            }
            node.InnerText = value;
        }

        /// <summary>
        /// 为请求增加第二个参数，用于图片上传接口。
        /// </summary>
        /// <param name="img">img是将图片转化为byte[]再用BASE64编码的字符串。</param>
        public void addImg(string img)
        {
            XmlNodeList nodelist = xmlDoc.SelectNodes("/request/data");
            if (nodelist.Count == 2)
            {
                nodelist[1].InnerText = img;
            }
            else if (nodelist.Count == 1)
            {
                throw new Exception("缺少第二个data元素，无法添加图片数据");
            }
            else
            {
                throw new Exception("存在多个data元素");
            }
        }

        /// <summary>
        /// 返回XML字符串
        /// </summary>
        public string xml
        {
            get
            {
                return xmlDoc.InnerXml;
            }
        }

        /// <summary>
        /// 返回Data节点
        /// </summary>
        public XmlNode data
        {
            get
            {
                return xmlDoc.GetElementsByTagName("data")[0];
            }
        }

        /// <summary>
        /// 返回请求的标题
        /// </summary>
        public string title
        {
            get
            {
                if (xmlDoc.ChildNodes[1].NodeType==XmlNodeType.Comment)
                {
                    string[] comments = xmlDoc.ChildNodes[1].InnerText.Trim().Split('\n');
                    return comments[0];
                }
                else
                {
                    return "没有注明接口名称";
                }
            }
        }


        /// <summary>
        /// 返回请求的备注说明
        /// </summary>
        public string comment
        {
            get
            {
                if (xmlDoc.ChildNodes[1].NodeType == XmlNodeType.Comment)
                {
                    string[] comments = xmlDoc.ChildNodes[1].InnerText.Trim().Split('\n');
                    return xmlDoc.ChildNodes[1].InnerText.Substring(comments[0].Length);
                }
                else
                {
                    return "没有注明接口名称";
                }
            }
        }

        /// <summary>
        /// 设置本次请求的操作员
        /// </summary>
        /// <param name="id"></param>
        internal void setOperator(string id)
        {
            XmlNode node = xmlDoc.SelectSingleNode("/request/auth/OPERATOR");
            if (node == null)
            {
                throw new Exception("找不到属性：/request/auth/OPERATOR");
            }
            node.InnerText = id;
        }

        /// <summary>
        /// 读取用于生成WebService请求的Xml配置文件
        /// </summary>
        /// <param name="methonName">WebService操作方法名称，即不含.xml后缀的XML文件名</param>
        /// <param name="ignoreComments">是否忽略文档里面的注释，默认忽略</param>
        internal void getXmlDocumentFromFile(string methonName = "", bool ignoreComments = true)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = ignoreComments;// 是否忽略文档里面的注释
                string path = xmlPath + methonName + ".xml";
                XmlReader reader = XmlReader.Create(path, settings);
                xmlDoc.Load(reader);
                reader.Close();
            }
            catch (Exception)
            {
                throw new NotImplementedException("WebService XML文件加载失败：" + xmlPath);
            }
        }

        /// <summary>
        /// 检查是否存在重复节点
        /// </summary>
        internal void validate()
        {
            XmlNodeList dataList = data.ChildNodes;
            foreach (XmlNode node in dataList)
            {
                if (node.NodeType==XmlNodeType.Element)
                {
                    if (xmlDoc.GetElementsByTagName(node.Name).Count>1)
                    {
                        Console.WriteLine(methonName + "发现重复节点：" + node.Name);
                        throw new Exception(methonName + "发现重复节点：" + node.Name);
                    }
                }
            }
        }
    }
}
