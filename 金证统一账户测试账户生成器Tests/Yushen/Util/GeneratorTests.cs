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
    public class GeneratorTests
    {
        [TestMethod()]
        public void CreateIdNOTest()
        {
            Assert.IsTrue(Generator.CreateIdNO().Length == 18);
            Assert.IsTrue(Generator.CreateIdNO(15).Length == 15);
        }
    }
}