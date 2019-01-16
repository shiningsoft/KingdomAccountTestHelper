using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yushen.ZhengTong.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Yushen.ZhengTong.Validator.Tests
{
    [TestClass()]
    public class ZtMobileTests
    {
        [TestMethod()]
        public void DecryptTest()
        {
            string result = ZtMobile.Decrypt("UoK4S3AxpgfSnC+tQD9gi0oYPjsJ3Nqgqhlu8hZBp5M=", "guoyuanzhengquankey");
            Console.WriteLine(result);
        }
    }
}

namespace 金证统一账户测试账户生成器.Yushen.ZhengTong.Validator.Tests
{
    [TestClass()]
    public class ZtMobileTests
    {
        [TestMethod()]
        public void TestTest()
        {
            ZtMobile zt = new ZtMobile();
            zt.Test("18655958868", "于申", "342221198603165576");
        }
    }
}