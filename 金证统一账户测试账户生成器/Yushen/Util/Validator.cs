using System;
using System.Text.RegularExpressions;

namespace Yushen.Util
{
    public static class Validator
    {
        /// <summary>
        /// 验证电话号码是否有效
        /// </summary>
        /// <param name="str_telephone"></param>
        /// <returns></returns>
        public static bool IsTelephone(string str_telephone)
        {
            return Regex.IsMatch(str_telephone, @"^(\d{3,4}-)?\d{6,8}$");
        }

        /// <summary>
        /// 验证手机号是否有效
        /// </summary>
        /// <param name="str_mobile"></param>
        /// <returns></returns>
        public static bool IsMobile(string str_mobile)
        {
            return Regex.IsMatch(str_mobile, @"^\d{11}$");
        }


        /// <summary>
        /// 验证邮编是否有效
        /// </summary>
        /// <param name="str_postalcode"></param>
        /// <returns></returns>
        public static bool IsPostalcode(string str_postalcode)
        {
            return Regex.IsMatch(str_postalcode, @"^\d{6}$");
        }

        /// <summary>
        /// 验证字符串是否全部为数字
        /// </summary>
        /// <param name="str_number"></param>
        /// <returns></returns>
        public static bool IsInteger(string str_number)
        {
            return Regex.IsMatch(str_number, @"^[0-9]*$");
        }

        /// <summary>
        /// 验证身份证号码是否有效
        /// </summary>
        /// <param name="Id"></param>  
        /// <returns></returns>  
        public static bool IsIDCard(string idNumber)
        {
            if (idNumber.Length == 18)
            {
                bool check = IsIDCard18(idNumber);
                return check;
            }
            else if (idNumber.Length == 15)
            {
                bool check = IsIDCard15(idNumber);
                return check;
            }
            else
            {
                return false;
            }
        }

        #region 验证文本框输入为数字
        /// <summary>
        /// 验证是不是数字(包含整数和小数)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNum(string str)
        {
            return Regex.IsMatch(str, @"^[-]?\d+[.]?\d*$");
        }
        #endregion

        #region 验证文本框输入为日期
        /// <summary>
        /// 判断日期是否为YYYY-MM-DD格式
        /// 日期必须不小于1753-01-01
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static bool IsDate(string Date)
        {
            if (!IsDateString(Date))
            {
                return false;
            }
            //验证YYYY-MM-DD格式,基本上把闰年和2月等的情况都考虑进去
            bool bValid = Regex.IsMatch(Date, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
            return (bValid && Date.CompareTo("1753-01-01") >= 0);

            //将平年和闰年的日期验证表达式合并，我们得到最终的验证日期格式为YYYY-MM-DD的正则表达式为：

            //(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|
            //[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-
            //(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|
            //(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|
            //[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)
        }
        #endregion

        #region 验证文本框输入为电子邮件
        /// <summary>
        /// 验证电子邮件
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        #endregion

        #region  验证文本框输入为传真号码
        /// <summary>
        /// 验证文本框输入为传真号码
        /// </summary>
        /// <param name="strFax">输入字符串</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool IsFax(string strFax)
        {
            return Regex.IsMatch(strFax, @"(86-)?\d{3,4}-\d{7,8}");
        }
        #endregion

        #region  验证是否为ip
        //获取IP的字符串 HttpContext.Current.Request.UserHostAddress

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");
        }
        #endregion

        #region  验证字符串是否是yyyy-mm-dd字符串
        /// <summary>
        /// 判断字符串是否是yyyy-mm-dd格式的字符串
        /// 不限制年份
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateString(string str)
        {
            return Regex.IsMatch(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
        }
        #endregion

        /// <summary>
        /// 18位身份证号码验证
        /// </summary>
        public static bool IsIDCard18(string idNumber)
        {
            if (idNumber.Length != 18)
            {
                return false;
            }
            long n = 0;
            if (long.TryParse(idNumber.Remove(17), out n) == false
                || n < Math.Pow(10, 16)
                || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证  
            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");

            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');

            char[] Ai = idNumber.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());

            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
            {
                return false;//校验码验证  

            }
            return true;//符合GB11643-1999标准  
        }


        /// <summary>  
        /// 15位身份证号码验证
        /// </summary>  
        public static bool IsIDCard15(string idNumber)
        {
            if (idNumber.Length != 15)
            {
                return false;
            }
            long n = 0;
            if (long.TryParse(idNumber, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证  
            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 6).Insert(4, "-").Insert(2, "-");

            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {

                return false;//生日验证  
            }
            return true;
        }
    }
}
