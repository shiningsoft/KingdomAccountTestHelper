using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace Yushen.Util
{
    class Formatter
    {

        /// <summary>
        /// 格式化输出XML字符串
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string formatXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            StringWriter sw = new StringWriter();
            using (XmlTextWriter writer = new XmlTextWriter(sw))
            {
                writer.Indentation = 2;  // the Indentation
                writer.Formatting = Formatting.Indented;
                doc.WriteContentTo(writer);
            }
            string str = sw.ToString();
            Regex regex = new Regex("^( *)<(.*)>\r\n *</(.*)>", RegexOptions.Multiline);
            str = regex.Replace(str, "$1<$2></$3>");
            return str;
        }
    }
}
