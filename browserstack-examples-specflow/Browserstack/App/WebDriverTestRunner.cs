using System;
using System.Collections;
using System.Collections.Generic;
using BrowserStack.WebDriver.Core;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Chrome;

namespace BrowserStack.App
{
    public class WebDriverTestRunner: IEnumerable 
    {

        protected IWebDriver driver;
        private static readonly ILog log = LogManager.GetLogger(typeof(WebDriverTestRunner));

        public IEnumerator GetEnumerator()
        {
         
            DriverFactory webDriverFactory = DriverFactory.GetInstance();
            List<WebDriver.Config.Platform> list;
            list = webDriverFactory.GetPlatforms();
            foreach (WebDriver.Config.Platform platform in list)
            {
                object fixtureArgs = webDriverFactory.CreateRemoteWebCapabilities(platform);
                log.Info(("Initialising driver with capabilities : {}", fixtureArgs));
                yield return fixtureArgs;
            }
        }

        protected void SetTestName(String name)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionName\", \"arguments\": {\"name\":\"" + name + "\" }}");
        }

        protected void MarkTestStatus()
        {
            if (TestContext.CurrentContext.Result.FailCount == 0)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \"All " + TestContext.CurrentContext.Result.PassCount + " test(s) completed successfully.\"}}");
            }
            else
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + TestContext.CurrentContext.Result.Message + ". Failed: " + TestContext.CurrentContext.Result.FailCount +
                    ", Passed: " + TestContext.CurrentContext.Result.PassCount +
                    ". \"}}");
            }
        }

        protected void Shutdown()
        {
            driver.Quit();
        }

        protected IWebDriver GetDriver(ChromeOptions driverOptions)
        {
            DriverFactory webDriverFactory = DriverFactory.GetInstance();
            return webDriverFactory.CreateRemoteWebDriver(driverOptions);
        }

    }

}
