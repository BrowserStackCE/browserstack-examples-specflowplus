using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using BrowserStack.App;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace SpecflowBrowserStack.Steps
{
    [Binding]
    [TestFixtureSource(typeof(WebDriverTestRunner))]
    public class userSteps : WebDriverTestRunner
    {
        private readonly IWebDriver _driver;
        private static bool result;
        WebDriverWait wait;
        public userSteps(DesiredCapabilities driverOptions)
        {
            _driver = GetDriver(driverOptions);
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Then(@"I should see no image loaded")]
        public void ThenIShouldSeeNoImageLoaded()
        {
            String src = _driver.FindElement(By.XPath("//img[@alt='iPhone 12']")).GetAttribute("src");
            result = Assert.Equals("img", src);
        }
    }
}
