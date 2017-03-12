using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegressionEISS.eCHAS
{
    [TestClass]
    public class TestSample : NetSeleniumBaseUnitTest
    {
        [TestMethod]
        public void TMIntranetHCICodeTest()
        {
            // Find the text input element by its name
            IWebElement query = getDriver().FindElement(By.Name("q"));

            // Enter something to search for
            query.SendKeys("Cheese");

            // Now submit the form. WebDriver will find the form for us from the element
            query.Submit();

            // Google's search is rendered dynamically with JavaScript.
            // Wait for the page to load, timeout after 10 seconds
            var wait = new WebDriverWait(getDriver(), TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.StartsWith("cheese", StringComparison.OrdinalIgnoreCase));

            // Should see: "Cheese - Google Search" (for an English locale)
            Console.WriteLine("Page title is: " + getDriver().Title);
            Assert.AreEqual("Cheese - Google Search", getDriver().Title);
        }

        [TestMethod]
        public void TMTest()
        {
            Assert.AreEqual("1", "2");
        }

        [TestMethod]
        public void TMTest234()
        {
            Assert.AreEqual("1", "1");
        }
    }
}
