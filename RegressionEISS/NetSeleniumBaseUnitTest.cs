using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressionEISS
{
    public class NetSeleniumBaseUnitTest
    {
        String rootURL = ConfigurationManager.AppSettings[TestConstants.AppConfigRootURL];
        protected IWebDriver driver;

        public void setDriver(IWebDriver inputDriver)
        {
            driver = inputDriver;
        }
        
        public IWebDriver getDriver()
        {
            if (driver == null)
            {
                InitialDriver();
            }
            return driver;
        }

        #region Initial and clean up for each of the test
        [TestInitialize]
        public void InitialDriver()
        {
            driver = new FirefoxDriver();
            //Notice navigation is slightly different than the Java version
            //This is because 'get' is a keyword in C#
            driver.Navigate().GoToUrl(rootURL);
        }

        [TestCleanup]
        public void CloseDriver()
        {
            driver.Dispose();
            driver.Quit();
        }
        #endregion
    }
}
