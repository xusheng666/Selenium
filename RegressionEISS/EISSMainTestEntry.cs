using System;
using RegressionEISS.eCHAS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace RegressionEISS
{
    [TestClass]
    public class EISSMainTestEntry
    {
        [TestMethod]
        public void RunAllTests()
        {
            List<String> errorList = new ReflectAllTestClass().ExecuteTests(TestConstants.MethodsToTestNameSpace);
        }
    }
}
