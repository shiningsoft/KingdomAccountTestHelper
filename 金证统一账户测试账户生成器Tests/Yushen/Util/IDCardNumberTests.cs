using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yushen.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yushen.Util.Tests
{
    [TestClass()]
    public class IDCardNumberTests
    {
        [TestMethod()]
        public void RandomTest()
        {
            string id = IDCardNumber.Random().CardNumber;
            Assert.IsTrue(id.Length == 18);
        }
    }
}