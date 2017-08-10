using System;
using System.Text;
using System.Net;
using System.IO;

namespace Yushen.WebService
{
    class Webservice
    {
        Encoding encoding = Encoding.UTF8;
        string responseData = String.Empty;
        System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
        WebRequest request;

        public Webservice(string wsdl = "http://60.173.222.38:30004/kess/services/KessService?wsdl")
        {
            request = HttpWebRequest.Create(wsdl);
        }

        // 调用Webservice的对应方法
        public string invoke(string methodName,string requestXml)
        {
            string param = this.perpareParam(methodName, requestXml);

            byte[] bs = System.Text.ASCIIEncoding.UTF8.GetBytes(param);
            request.Method = "POST";
            request.ContentType = "text/xml;charset=UTF-8"; ;
            request.Headers["SOAPAction"] = "";
            request.ContentLength = bs.Length;

            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    responseData = reader.ReadToEnd().ToString();
                }
            }

            xml.LoadXml(responseData);
            return xml.InnerXml;
        }

        internal string perpareParam(string methodName, string requestString)
        {
            return "<soapenv:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:impl=\"http://impl.service.wsdl.kess.szkingdom.com\">"
                + "<soapenv:Header/>"
                + "   <soapenv:Body>"
                + "      <impl:" + methodName + " soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">"
                + "         <in0 xsi:type=\"soapenc:string\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\"><![CDATA["
                + requestString
                + "]]></in0>"
                + "      </impl:" + methodName + ">"
                + "   </soapenv:Body>"
                + "</soapenv:Envelope>";
        }

        public string perpareReauqestString()
        {
            string header = "";
            string body = "";
            string footer = "";
            return header + body + footer;
        }

        /**
         * 测试WebService是否正常
         **/
        public string test()
        {
            string param = "<soapenv:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:impl=\"http://impl.service.wsdl.kess.szkingdom.com\">"
                + "<soapenv:Header/>"
                +"   <soapenv:Body>"
                +"      <impl:operatorLogin soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">"
                +"         <in0 xsi:type=\"soapenc:string\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\"><![CDATA["
                +"<?xml version=\"1.0\" encoding=\"UTF-8\"?><request><auth><OPERATOR>90018</OPERATOR></auth><data><USER_CODE>90018</USER_CODE><PASSWORD>888888</PASSWORD><F_CHANNEL>j</F_CHANNEL><AGENT_IP>192.168.40.118</AGENT_IP><PRINT_FLAG></PRINT_FLAG></data></request>"
                +"]]></in0>"
                +"      </impl:operatorLogin>"
                +"   </soapenv:Body>"
                +"</soapenv:Envelope>";
            byte[] bs = System.Text.ASCIIEncoding.UTF8.GetBytes(param);

            request.Method = "POST";
            request.ContentType = "text/xml;charset=UTF-8"; ;
            request.ContentLength = bs.Length;
            request.Headers["SOAPAction"] = "";
            
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    responseData = reader.ReadToEnd().ToString();
                }
            }

            xml.LoadXml(responseData);
            return xml.InnerXml;
        }
    }
}