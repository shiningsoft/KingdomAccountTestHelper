
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Yushen.Util
{
    /// <summary>
    /// 身份证号码解析与生成
    /// 作者：宋雷鸣 10522779@qq.com
    /// </summary>
    class IDCardNumber
    {
        #region 身份证信息属性
        private string _province;
        /// <summary>
        /// 所在省份信息
        /// </summary>
        public string Province
        {
            get { return _province; }
            set { _province = value; }
        }
        private string _area;
        /// <summary>
        /// 所在地区信息
        /// </summary>
        public string Area
        {
            get { return _area; }
            set { _area = value; }
        }
        private string _city;
        /// <summary>
        /// 所在区县信息
        /// </summary>
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        private DateTime _age;
        /// <summary>
        /// 年龄
        /// </summary>
        public DateTime Age
        {
            get { return _age; }
            set { _age = value; }
        }
        private int _sex;
        /// <summary>
        /// 性别，1为女，0为男
        /// </summary>
        public int Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        private string _cardnumber;
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string CardNumber
        {
            get { return _cardnumber; }
            set { _cardnumber = value; }
        }
        private string _json;
        /// <summary>
        /// 生成Javascript对象；
        /// </summary>
        public string Json
        {
            get { return _json; }
            set { _json = value; }
        }

        #endregion
        
        private static readonly List<string[]> Areas = new List<string[]>();
        /// <summary>
        /// 获取区域信息
        /// </summary>
        private static void FillAreas()
        {
            XmlDocument docXml = new XmlDocument();
            string file = Environment.CurrentDirectory + "\\Yushen\\Util\\AreaCodeInfo.xml";
            docXml.Load(file);
            XmlNodeList nodelist = docXml.GetElementsByTagName("area");
            foreach (XmlNode node in nodelist)
            {
                string code = node.Attributes["code"].Value;
                string name = node.Attributes["name"].Value;
                IDCardNumber.Areas.Add(new string[] { code, name });
            }
        }
        /// <summary>
        /// 解析身份证信息
        /// </summary>
        /// <param name="idCardNumber"></param>
        public static IDCardNumber Get(string idCardNumber)
        {
            if (IDCardNumber.Areas.Count < 1)
                IDCardNumber.FillAreas();
            if (!IDCardNumber.CheckIDCardNumber(idCardNumber))
                throw new Exception("非法的身份证号码");
            //
            IDCardNumber cardInfo = new IDCardNumber(idCardNumber);
            return cardInfo;
        }
        /// <summary>
        /// 校验身份证号码是否合法
        /// </summary>
        /// <param name="idCardNumber"></param>
        /// <returns></returns>
        public static bool CheckIDCardNumber(string idCardNumber)
        {
            //正则验证
            Regex rg = new Regex(@"^\d{17}(\d|X)$");
            Match mc = rg.Match(idCardNumber);
            if (!mc.Success) return false;
            //加权码
            string code = idCardNumber.Substring(17, 1);
            double sum = 0;
            string checkCode = null;
            for (int i = 2; i <= 18; i++)
            {
                sum += int.Parse(idCardNumber[18 - i].ToString(), NumberStyles.HexNumber) * (Math.Pow(2, i - 1) % 11);
            }
            string[] checkCodes = { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };
            checkCode = checkCodes[(int)sum % 11];
            if (checkCode != code) return false;
            //
            return true;
        }
        /// <summary>
        /// 随机生成一个身份证号
        /// </summary>
        /// <returns></returns>
        public static IDCardNumber Random()
        {
            long tick = DateTime.Now.Ticks;
            return new IDCardNumber(_randomCardNumber((int)tick));
        }
        /// <summary>
        /// 批量生成身份证
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<IDCardNumber> Random(int count)
        {
            List<IDCardNumber> list = new List<IDCardNumber>();
            string cardNumber;
            bool isExits;
            for (int i = 0; i < count; i++)
            {
                do
                {
                    isExits = false;
                    int tick = (int)DateTime.Now.Ticks;
                    cardNumber = IDCardNumber._randomCardNumber(tick * (i + 1));
                    foreach (IDCardNumber c in list)
                    {
                        if (c.CardNumber == cardNumber)
                        {
                            isExits = true;
                            break;
                        }
                    }

                } while (isExits);
                list.Add(new IDCardNumber(cardNumber));
            }
            return list;
        }
        /// <summary>
        /// 生成随机身份证号
        /// </summary>
        /// <param name="seed">随机数种子</param>
        /// <returns></returns>
        private static string _randomCardNumber(int seed)
        {
            if (IDCardNumber.Areas.Count < 1)
                IDCardNumber.FillAreas();
            System.Random rd = new System.Random(seed);
            //随机生成发证地
            string area = "";
            do
            {
                area = IDCardNumber.Areas[rd.Next(0, IDCardNumber.Areas.Count - 1)][0];
            } while (area.Substring(4, 2) == "00");
            //随机出生日期
            DateTime birthday = DateTime.Now;
            birthday = birthday.AddYears(-rd.Next(16, 60));
            birthday = birthday.AddMonths(-rd.Next(0, 12));
            birthday = birthday.AddDays(-rd.Next(0, 31));
            //随机码
            string code = rd.Next(1000, 9999).ToString("####");
            //生成完整身份证号
            string codeNumber = area + birthday.ToString("yyyyMMdd") + code;
            double sum = 0;
            string checkCode = null;
            for (int i = 2; i <= 18; i++)
            {
                sum += int.Parse(codeNumber[18 - i].ToString(), NumberStyles.HexNumber) * (Math.Pow(2, i - 1) % 11);
            }
            string[] checkCodes = { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };
            checkCode = checkCodes[(int)sum % 11];
            codeNumber = codeNumber.Substring(0, 17) + checkCode;
            //
            return codeNumber;
        }

        #region 身份证解析方法
        public IDCardNumber(string idCardNumber)
        {
            this._cardnumber = idCardNumber;
            _analysis();
        }
        /// <summary>
        /// 解析身份证
        /// </summary>
        private void _analysis()
        {
            int flag = _cardnumber.Length == 18 ? 0 : 2;
            //取省份，地区，区县
            string provCode = _cardnumber.Substring(0, 2).PadRight(6, '0');
            string areaCode = _cardnumber.Substring(0, 4).PadRight(6, '0');
            string cityCode = _cardnumber.Substring(0, 6).PadRight(6, '0');
            for (int i = 0; i < IDCardNumber.Areas.Count; i++)
            {
                if (provCode == IDCardNumber.Areas[i][0])
                    this._province = IDCardNumber.Areas[i][1];
                if (areaCode == IDCardNumber.Areas[i][0])
                    this._area = IDCardNumber.Areas[i][1];
                if (cityCode == IDCardNumber.Areas[i][0])
                    this._city = IDCardNumber.Areas[i][1];
                if (_province != null && _area != null && _city != null) break;
            }
            //取年龄
            string ageCode = _cardnumber.Substring(6, 8 - flag);
            try
            {
                int year = Convert.ToInt16(ageCode.Substring(0, 4 - flag));
                if (flag==2)
                {
                    year += 1900;
                }
                int month = Convert.ToInt16(ageCode.Substring(4 - flag, 2));
                int day = Convert.ToInt16(ageCode.Substring(6 - flag, 2));
                _age = new DateTime(year, month, day);
            }
            catch
            {
                throw new Exception("非法的出生日期");
            }
            //取性别
            string orderCode = _cardnumber.Substring(14 - flag, 3);
            this._sex = Convert.ToInt16(orderCode) % 2 == 0 ? 1 : 0;
            //生成Javascript对象
            _json = @"prov:'{0}',area:'{1}',city:'{2}',year:{3},month:{4},day:{5},sex:{6},number:'{7}'";
            _json = string.Format(_json, _province, _area, _city, _age.Year, _age.Month, _age.Day, _sex, _cardnumber);
            _json = "{" + _json + "}";
        }
        #endregion

        /// <summary>
        /// 15位身份证号码转18位
        /// </summary>
        /// <param name="perIDSrc"></param>
        /// <returns></returns>
        public static string per15To18(string perIDSrc)
        {
            if (perIDSrc.Length != 15)
            {
                throw new Exception(perIDSrc + "不是15位身份证号码");
            }
            int iS = 0;

            //加权因子常数  
            int[] iW = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            //校验码常数  
            string LastCode = "10X98765432";
            //新身份证号  
            string perIDNew;

            perIDNew = perIDSrc.Substring(0, 6);
            //填在第6位及第7位上填上‘1’，‘9’两个数字  
            perIDNew += "19";

            perIDNew += perIDSrc.Substring(6, 9);

            //进行加权求和  
            for (int i = 0; i < 17; i++)
            {
                iS += int.Parse(perIDNew.Substring(i, 1)) * iW[i];
            }

            //取模运算，得到模值  
            int iY = iS % 11;
            //从LastCode中取得以模为索引号的值，加到身份证的最后一位，即为新身份证号。  
            perIDNew += LastCode.Substring(iY, 1);

            return perIDNew;
        }

        /// <summary>
        /// 18位身份证号码转15位
        /// </summary>
        /// <param name="idno"></param>
        /// <returns></returns>
        public static string per18To15(string idno)
        {
            if (idno.Length != 18)
            {
                throw new Exception(idno + "不是18位身份证号码");
            }
            return idno.Substring(0, 6) + idno.Substring(8, 9);
        }
    }

}