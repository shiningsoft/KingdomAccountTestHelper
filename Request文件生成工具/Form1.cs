using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Request文件生成工具
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnProccess_Click(object sender, EventArgs e)
        {
            string text = tbRaw.Text;
            Regex rxTitle = new Regex(@"2\.\d+\t.+?\r");
            MatchCollection mc = rxTitle.Matches(text);
            // 循环处理
            for (int i = 0; i < mc.Count - 1; i++)
            {
                string functionComment = mc[i].Value.Trim();
                string part = text.Substring(mc[i].Index, mc[i + 1].Index - mc[i].Index);
                Regex rxFunction= new Regex(@"函数原型");
                Match mcFunction = rxFunction.Match(part);
                
                Regex rxParamsInTitle = new Regex(@"入参格式");
                Match mcParamsInTitle = rxParamsInTitle.Match(part);

                Regex rxParamsOutTitle = new Regex(@"出参格式");
                Match mcParamsOutTitle = rxParamsOutTitle.Match(part);

                string partFunction = part.Substring(mcFunction.Index, mcParamsInTitle.Index - mcFunction.Index);
                string partParamsIn = part.Substring(mcParamsInTitle.Index, mcParamsOutTitle.Index - mcParamsInTitle.Index).Replace(" ", "");

                Regex rxFunctionName = new Regex(@"\s(\w+)\s?\(String xml\)");
                Match functionName = rxFunctionName.Match(partFunction);

                string comment = "";
                // 函数原型部分超过两行，说明存在注释
                MatchCollection mcFunctionPartLineCount = Regex.Matches(partFunction, @"\r\n");
                if (mcFunctionPartLineCount.Count > 2)
                {
                    comment = partFunction.Substring(mcFunctionPartLineCount[1].Index + 2);
                }

                //Console.WriteLine(functionName.Groups[1]);
                //Console.WriteLine(functionComment);

                // 处理入参列表
                int start = partParamsIn.ToLower().IndexOf("<request>");
                if (start>-1)
                {
                    int end = partParamsIn.ToLower().IndexOf("</request>") + 10;
                    string body = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<!--" + functionComment;
                    if (comment!="")
                    {
                        body = body + "\r\n" + comment.Trim() + "\r\n";
                    }
                    body = body + "-->\r\n" + partParamsIn.Substring(start, end - start).Replace("\t", "").Replace(" ", "").Trim();

                    // 处理字段注释
                    Regex rxComment = new Regex(@"//(.+)\r");
                    body = rxComment.Replace(body, "<!--$1-->");

                    // 有些<auth>节点没有关闭，需要处理
                    if (Regex.Matches(body, @"<auth>").Count > 1)
                    {
                        body = body.Replace("<auth>\r\n<data>", "</auth>\r\n<data>");
                    }

                    try
                    {
                        string file = Path.Combine(Environment.CurrentDirectory, functionName.Groups[1].Value.Trim() + ".xml");
                        File.WriteAllText(file, formatXml(body));
                    }
                    catch (XmlException ex)
                    {
                        Console.WriteLine(functionComment + "Xml格式校验失败：" + ex.Message );
                    }
                }

            }
            Console.WriteLine("匹配项个数：" + mc.Count);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnPreProccess.PerformClick();
        }

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
    }
}
