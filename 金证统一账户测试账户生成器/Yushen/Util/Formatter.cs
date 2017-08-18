using System;
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
            // 删除空白内容节点的换行
            string str = sw.ToString();
            Regex regex = new Regex(@"^( *)<(\w*)>\r\n *</(.*)>", RegexOptions.Multiline);
            str = regex.Replace(str, "$1<$2></$3>");
            return str;
        }

        /// <summary>
        /// 超长字符串自动换行
        /// </summary>
        /// <param name="str"></param>
        /// <param name="letterNumber"></param>
        /// <returns></returns>
        public static string lineWarp(string str, int letterNumber)
        {
            int lineCnt = (int)Math.Ceiling((double)str.Length / (double)letterNumber); // 计算行数
            string newStr = "";
            for (int i = 0; i < lineCnt; i++)
            {
                int leftLength = str.Length - i * letterNumber;
                newStr += str.Substring(0 + i * letterNumber, leftLength < letterNumber ? leftLength : letterNumber);
                if (i < lineCnt - 1)
                {
                    newStr += Environment.NewLine;
                }
            }
            return newStr;
        }
    }
}
