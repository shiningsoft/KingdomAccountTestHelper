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
    public class ValidatorTests
    {
        [TestMethod()]
        public void IsTelephoneTest()
        {
            Assert.IsTrue(Validator.IsTelephone("0551-62207837"));
            Assert.IsTrue(Validator.IsTelephone("0551-1234567"));
            Assert.IsTrue(Validator.IsTelephone("055162207837"));
            Assert.IsFalse(Validator.IsTelephone("18655958868"));
            Assert.IsFalse(Validator.IsTelephone("0551-62207837;62207114"));
            Assert.IsFalse(Validator.IsTelephone("0551-62207837,123456789"));
            Assert.IsFalse(Validator.IsTelephone("不知道"));
            Assert.IsFalse(Validator.IsTelephone("abc"));
        }

        [TestMethod()]
        public void IsMobileTest()
        {
            Assert.IsTrue(Validator.IsMobile("18655958868"));
            Assert.IsFalse(Validator.IsMobile("08655958868"));
            Assert.IsFalse(Validator.IsMobile("18655958868123456"));
            Assert.IsFalse(Validator.IsMobile("62207837"));
            Assert.IsFalse(Validator.IsMobile("abc"));
            Assert.IsFalse(Validator.IsMobile("中国"));
        }

        [TestMethod()]
        public void IsPostalcodeTest()
        {
            Assert.IsTrue(Validator.IsPostalcode("230001"));
            Assert.IsFalse(Validator.IsPostalcode("2300010"));
            Assert.IsFalse(Validator.IsPostalcode("2300"));
        }

        [TestMethod()]
        public void IsIntegerTest()
        {
            Assert.IsTrue(Validator.IsInteger("123456"));
            Assert.IsFalse(Validator.IsInteger("123.123"));
            Assert.IsFalse(Validator.IsInteger("123abc"));
        }

        [TestMethod()]
        public void IsIDCardTest()
        {
            Assert.IsTrue(Validator.IsIDCard("342221198603165576"));    //正常
            Assert.IsTrue(Validator.IsIDCard("43042219760424384X"));    //大写X
            Assert.IsTrue(Validator.IsIDCard("43042219760424384x"));    //小写x
            Assert.IsTrue(Validator.IsIDCard("340103701028202"));   // 15位
            Assert.IsFalse(Validator.IsIDCard("43042219760424384"));    //太短
            Assert.IsFalse(Validator.IsIDCard("34010370102820"));   // 太短
            Assert.IsFalse(Validator.IsIDCard("43042219760424384x1"));  //超长
            Assert.IsFalse(Validator.IsIDCard("43042230760424384x"));   //年份错误
            Assert.IsFalse(Validator.IsIDCard("43042219761324384x"));   //月份错误
            Assert.IsFalse(Validator.IsIDCard("abcdefghijklmnopqr"));
            Assert.IsFalse(Validator.IsIDCard("中国"));
        }

        [TestMethod()]
        public void IsNumTest()
        {
            Assert.IsTrue(Validator.IsNum("123456"));
            Assert.IsTrue(Validator.IsNum("123.123"));
            Assert.IsFalse(Validator.IsNum("123abc"));
        }

        [TestMethod()]
        public void IsDateTest()
        {
            Assert.IsTrue(Validator.IsDate("2017-07-28"));
            Assert.IsFalse(Validator.IsDate("20170728"));
            Assert.IsFalse(Validator.IsDate("1752-01-01"));
            Assert.IsFalse(Validator.IsDate("123abc"));
        }

        [TestMethod()]
        public void IsEmailTest()
        {
            Assert.IsTrue(Validator.IsEmail("nuoyan_cfan@163.com"));
            Assert.IsTrue(Validator.IsEmail("nuoyan.cfan@gmail.com"));
            Assert.IsTrue(Validator.IsEmail("yushen@gyzq.com.cn"));
            Assert.IsFalse(Validator.IsEmail("yushen#gyzq.com.cn"));
            Assert.IsFalse(Validator.IsEmail("123abc"));
        }

        [TestMethod()]
        public void IsFaxTest()
        {
            Assert.IsTrue(Validator.IsFax("86-0551-62207952"));
            Assert.IsTrue(Validator.IsFax("86-0551-62207952"));
            Assert.IsTrue(Validator.IsFax("0551-6220795"));
            Assert.IsFalse(Validator.IsFax("123abc"));
        }

        [TestMethod()]
        public void IsIPTest()
        {
            Assert.IsTrue(Validator.IsIP("10.0.0.1"));
            Assert.IsTrue(Validator.IsIP("255.255.255.255"));
            Assert.IsFalse(Validator.IsIP("256.256.256.256"));
            Assert.IsFalse(Validator.IsIP("300.0.0.1"));
            Assert.IsFalse(Validator.IsIP("127.0.0.1.2"));
            Assert.IsFalse(Validator.IsIP("127.0.0"));
            Assert.IsFalse(Validator.IsIP("www.gyzq.com.cn"));
        }

        [TestMethod()]
        public void IsIPSectTest()
        {
            Assert.IsTrue(Validator.IsIPSect("10.0.0.1"));
            Assert.IsTrue(Validator.IsIPSect("255.255.255.255"));
            Assert.IsFalse(Validator.IsIPSect("256.256.256.256"));
            Assert.IsFalse(Validator.IsIPSect("300.0.0.1"));
            Assert.IsFalse(Validator.IsIPSect("127.0.0.1.2"));
            Assert.IsFalse(Validator.IsIPSect("127.0.0"));
            Assert.IsFalse(Validator.IsIPSect("www.gyzq.com.cn"));
        }

        [TestMethod()]
        public void IsDateStringTest()
        {
            Assert.IsTrue(Validator.IsDateString("2017-07-28"));
            Assert.IsTrue(Validator.IsDateString("1752-01-01"));
            Assert.IsFalse(Validator.IsDateString("20170728"));
            Assert.IsFalse(Validator.IsDateString("123abc"));
        }

        [TestMethod()]
        public void IsIDCard18Test()
        {
            Assert.IsTrue(Validator.IsIDCard18("342221198603165576"));    //正常
            Assert.IsTrue(Validator.IsIDCard18("43042219760424384X"));    //大写X
            Assert.IsTrue(Validator.IsIDCard18("43042219760424384x"));    //小写x
            Assert.IsFalse(Validator.IsIDCard18("340103701028202"));   // 15位
            Assert.IsFalse(Validator.IsIDCard18("43042219760424384"));    //太短
            Assert.IsFalse(Validator.IsIDCard18("34010370102820"));   // 太短
            Assert.IsFalse(Validator.IsIDCard18("43042219760424384x1"));  //超长
            Assert.IsFalse(Validator.IsIDCard18("43042230760424384x"));   //年份错误
            Assert.IsFalse(Validator.IsIDCard18("43042219761324384x"));   //月份错误
            Assert.IsFalse(Validator.IsIDCard18("abcdefghijklmnopqr"));
            Assert.IsFalse(Validator.IsIDCard18("中国"));
        }

        [TestMethod()]
        public void IsIDCard15Test()
        {
            Assert.IsTrue(Validator.IsIDCard15("340103701028202"));   // 15位
            Assert.IsFalse(Validator.IsIDCard15("34010370102820"));   // 太短
            Assert.IsFalse(Validator.IsIDCard15("342221198603165576"));    //18位
            Assert.IsFalse(Validator.IsIDCard15("43042219760424384X"));    //18位
            Assert.IsFalse(Validator.IsIDCard15("43042219760424384x"));    //18位
            Assert.IsFalse(Validator.IsIDCard15("43042219760424384"));    //太短
            Assert.IsFalse(Validator.IsIDCard15("43042219760424384x1"));  //超长
            Assert.IsFalse(Validator.IsIDCard15("43042230760424384x"));   //年份错误
            Assert.IsFalse(Validator.IsIDCard15("43042219761324384x"));   //月份错误
            Assert.IsFalse(Validator.IsIDCard15("abcdefghijklmnopqr"));
            Assert.IsFalse(Validator.IsIDCard15("中国"));
        }
    }
}