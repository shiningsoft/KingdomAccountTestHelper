using System;
using System.Data;
using System.IO;
using System.Xml;

namespace Yushen.WebService.KessClient.Dict
{
    /// <summary>
    /// 自定义数据字典，可用XML进行配置
    /// </summary>
    class CustomDict : IDict
    {
        public static string path = Path.Combine(Environment.CurrentDirectory, "CustomDict");

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
        }

        private bool _selectable = false;

        /// <summary>
        /// 设置是否可选
        /// 设置为true之后，DataTable的第一行将是“请选择”
        /// </summary>
        public bool selectable
        {
            set
            {
                this._selectable = value;
            }
            get
            {
                return this._selectable;
            }
        }

        /// <summary>
        /// 自定义数据字典，可用XML文件进行配置
        /// </summary>
        /// <param name="DictName">字典名称，即Xml文件名称</param>
        public CustomDict(string DictName)
        {
            try
            {
                this._dataTable = LoadXml(DictName.ToUpper());
                this._name = DictName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("XML文件加载失败");
                throw ex;
            }
        }

        private DataTable _dataTable;

        public DataTable DataTable
        {
            get
            {
                return _dataTable;
            }
        }

        /// <summary>
        /// 根据指定的字典值取得对应的字典项名称。
        /// 找不到时返回原值。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string getNameByValue(string value)
        {
            foreach (DataRow dr in _dataTable.Rows)
            {
                if (dr["value"].ToString() == value)
                {
                    return dr["name"].ToString();
                }
            }
            return value;
        }

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
            string file = Path.Combine(path, DictName + ".xml");
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

        /// <summary>
        /// 判断指定值是否在字典中存在
        /// 如果存在则返回索引值，否则返回-1。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(string value)
        {
            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                if (value == _dataTable.Rows[i]["value"].ToString())
                {
                    return i;
                }
            }
            return -1;
        }
    }

}
