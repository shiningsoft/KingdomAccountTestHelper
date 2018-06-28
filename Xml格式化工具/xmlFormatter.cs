using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yushen.Util
{
    class xmlFormatter
    {
        public string xmlstr = "";
        private Regex regex;

        /// <summary>
        /// 预处理
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public string preProccess(string strData)
        {
            Regex regex;

            regex = new Regex(@" >", RegexOptions.Multiline);
            Console.WriteLine(regex.ToString() + "匹配了" + regex.Matches(strData).Count.ToString() + "次");
            strData = regex.Replace(strData, ">");

            regex = new Regex(@"< ", RegexOptions.Multiline);
            Console.WriteLine(regex.ToString() + "匹配了" + regex.Matches(strData).Count.ToString() + "次");
            strData = regex.Replace(strData, "<");

            regex = new Regex(@"\r\n网上开户系统WebService 接口\r\n\d+\r\n", RegexOptions.Multiline);
            Console.WriteLine(regex.ToString() + "匹配了" + regex.Matches(strData).Count.ToString() + "次");
            strData = regex.Replace(strData, "");

            regex = new Regex(@" ", RegexOptions.Multiline);
            Console.WriteLine(regex.ToString() + "匹配了" + regex.Matches(strData).Count.ToString() + "次");
            strData = regex.Replace(strData, "");

            regex = new Regex(@">//", RegexOptions.Multiline);
            Console.WriteLine(regex.ToString() + "匹配了" + regex.Matches(strData).Count.ToString() + "次");
            strData = regex.Replace(strData, ">\r\n//");
            
            regex = new Regex(@"(.+)//(.+)", RegexOptions.Multiline);
            Console.WriteLine(regex.ToString() + "匹配了" + regex.Matches(strData).Count.ToString() + "次");
            strData = regex.Replace(strData, "$1\r\n//$2");

            return strData;
        }

        /// <summary>
        /// 转换为参数列表
        /// </summary>
        /// <param name="str">预处理后的标准格式字符串</param>
        /// <returns></returns>
        public string getParams()
        {
            string str = this.xmlstr;

            regex = new Regex(@"//(.*)<(.*)>.*</.*>");

            Console.WriteLine(regex.ToString() + "匹配了" + regex.Matches(str).Count.ToString() + "次：");
            foreach (Match match in regex.Matches(str))
            {
                Console.WriteLine(match);
            }

            str = regex.Replace(str, "string $2 = \"\", //$1");

            return str;
        }

        /// <summary>
        /// 转换成request.setAttr字段列表
        /// </summary>
        /// <returns></returns>
        public string getSetAttr()
        {
            string str = this.xmlstr;

            regex = new Regex(@"//(.*)<(.*)>.*</.*>");

            Console.WriteLine(regex.ToString() + "匹配了" + regex.Matches(str).Count.ToString() + "次：");
            foreach (Match match in regex.Matches(str))
            {
                Console.WriteLine(match);
            }

            str = regex.Replace(str, "request.setAttr(\"$2\", $2); //$1");

            return str;
        }


        /// <summary>
        /// 转换成注释字段列表
        /// </summary>
        /// <returns></returns>
        public string getMemo()
        {
            string str = this.xmlstr;

            regex = new Regex(@"//(.*)<(.*)>.*</.*>");

            Console.WriteLine(regex.ToString() + "匹配了" + regex.Matches(str).Count.ToString() + "次：");
            foreach (Match match in regex.Matches(str))
            {
                Console.WriteLine(match);
            }

            str = regex.Replace(str, "/// <param name=\"$2\">$1</param>");

            return str;
        }
    }
}
