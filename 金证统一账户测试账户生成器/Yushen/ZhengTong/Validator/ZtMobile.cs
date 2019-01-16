using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yushen.Util;
using System.Net;
using System.IO;
using System.Security.Cryptography;

namespace Yushen.ZhengTong.Validator
{
    public class ZtMobile
    {
        /// <summary>
        /// 公钥
        /// </summary>
        string publicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCYfAQ1OK2nCgJhXuafKaP5Pdotp7YgmAQkOne/5NqchH0sH9BusY1YLKqQk1T2PghgpTRASnuWoDb+wjRLcm0RTDWa7/DmFPkp/c+eGKAZq8Eq/Tib8fvGdTIrBh20MiKlAPpbkZmrhKBzGg+V/SAbB/9R+eZKH52LenDlN1QSEwIDAQAB";

        /// <summary>
        /// 会话密钥
        /// </summary>
        string sessionKey = "guoyuanzhengquankey";

        /// <summary>
        /// 接口URL地址
        /// </summary>
        string url = "https://rz.ect888.com:9090/servlet/json";

        string 机构编号 = "0101600008";

        string 机构账号 = "guoyuanzq01";

        string 机构密码 = "xnt123456";

        /// <summary>
        /// 手机号码实名验证
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="name">姓名</param>
        /// <param name="idno">证件号码</param>
        public void Test(string mobile,string name,string idno)
        {
            //string result = Function2000239Test.mobileTest(url, sessionKey, 机构账号, 机构编号, "于申", "342221198603165576", "18655958868");
            //Console.WriteLine(result);

            Dictionary<string, string> datas = new Dictionary<string, string>();
            datas.Add("certseq", EncryptHelper.AES_Encrypt(idno, sessionKey));
            datas.Add("ptyacct", 机构账号);
            datas.Add("ptycd", 机构编号);
            datas.Add("sourcechnl", "0");
            datas.Add("placeid", "00");
            datas.Add("biztyp", "A001");
            datas.Add("biztypdesc", "");
            datas.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            datas.Add("phoneno", mobile);
            string sign = signature(datas);
            datas.Add("sign", sign);
            datas.Add("usernm", name);
            datas["certseq"] = Base64Helper.EncodeBase64(Encoding.UTF8, System.Web.HttpUtility.UrlEncode(EncryptHelper.AES_Encrypt(idno, sessionKey)));
            datas.Add("funcNo", "2000239");

            Console.WriteLine(Post(datas));
        }
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        
        /// <summary>
        /// SHA512签名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string signature(Dictionary<string, string> datas)
        {
            Dictionary<string, string> ascdic = datas.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value.ToString());//对key进行升序
            string str = "";
            foreach (var param in ascdic)
            {
                str += param.Key + param.Value;
            }
            str += sessionKey;

            Console.WriteLine("待签名字符串为：" + str);

            return EncryptHelper.SHA512(str);
        }

        private string Post(Dictionary<string, string> paramsDict)
        {
            string postDatas = "";
            foreach (var param in paramsDict)
            {
                postDatas += param.Key + "=" + param.Value + "&";
            }
            Console.WriteLine(postDatas);
            HttpWebResponse response = HTTPS.CreatePost(url, postDatas);

            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string responseData = reader.ReadToEnd().ToString();
                return responseData;
            }
        }
    }
}
