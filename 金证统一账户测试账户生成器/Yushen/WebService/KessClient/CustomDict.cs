using System;
using System.Data;
using System.IO;
using System.Xml;

namespace Yushen.WebService.KessClient.Dict
{
    /// <summary>
    /// 自定义数据字典，可用XML进行配置
    /// </summary>
    class CustomDict : Dict
    {
        /// <summary>
        /// 自定义数据字典，可用XML文件进行配置
        /// </summary>
        /// <param name="DictName">字典名称，即Xml文件名称</param>
        public CustomDict(string DictName)
        {
            try
            {
                this.DataTable = LoadXml(DictName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("XML文件加载失败");
                throw ex;
            }
        }

        public new DataTable DataTable;

        /// <summary>
        /// 从XML文件中读取字典配置
        /// </summary>
        /// <param name="DictName"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private DataTable LoadXml(string DictName, string name = "name", string value = "code")
        {
            XmlDocument docXml = new XmlDocument();
            string file = Environment.CurrentDirectory + "\\CustomDict\\" + DictName + ".xml";
            if (!File.Exists(file))
            {
                throw new Exception("自定义数据字典文件" + file + "不存在！");
            }
            docXml.Load(file);
            XmlNodeList nodelist = docXml.GetElementsByTagName(DictName);

            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("value");

            if (selectable)
            {
                DataRow dr = dt.NewRow();
                dr["name"] = "请选择";
                dr["value"] = "";
                dt.Rows.Add(dr);
            }

            foreach (XmlNode node in nodelist)
            {
                DataRow dr = dt.NewRow();
                dr["name"] = node.Attributes["name"].Value;
                dr["value"] = node.Attributes["code"].Value;
                dt.Rows.Add(dr);
            }

            return dt;
        }
    }

}
