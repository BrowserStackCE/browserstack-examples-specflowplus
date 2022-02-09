using BoDi;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TechTalk.SpecFlow;
using BrowserStack.WebDriver.Core;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using BrowserStack.WebDriver.Core;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SpecflowBrowserStack.src.stepdefs
{
    [Binding]
    public class Hooks
    { 
        private static FeatureContext _featureContext;
        private static ScenarioContext _scenarioContext;

        public static ThreadLocal<IWebDriver> threadLocalDriver = new ThreadLocal<IWebDriver>();
        //protected IWebDriver driver;
        private static readonly ILog log = LogManager.GetLogger(typeof(Hooks));

        public static DriverOptions GetWebDriverObject()
        {

            DriverFactory webDriverFactory = DriverFactory.GetInstance();
            List<BrowserStack.WebDriver.Config.Platform> list;
            list = webDriverFactory.GetPlatforms();
            DriverOptions fixtureArgs = null;
            foreach (BrowserStack.WebDriver.Config.Platform platform in list)
            {
                fixtureArgs = webDriverFactory.CreateRemoteWebCapabilities(platform);
                log.Info(("Initialising driver with capabilities : {}", fixtureArgs));

            }

            return fixtureArgs;
        }

        public Hooks( FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            Console.WriteLine("Hello World");
            //Initialize Extent report before test starts
           
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            //Flush report once test completes
        
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            //Create dynamic feature name
            
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            // use for after hooks
        }


        [BeforeScenario]
        public static void Initialize(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Hello World");

            GetDriver(GetWebDriverObject());

            SetTestName(scenarioContext.ScenarioInfo.Title);
            //Create dynamic scenario name

        }


        [AfterScenario]
        public static void TearDown(ScenarioContext scenarioContext)
        {
            try
            {
                MarkTestStatus();
            }
            catch
            {

            }
            finally
            {
                Shutdown();
            }
            Console.WriteLine("Hello World");

           
            //Create dynamic scenario name

        }

        protected static void SetTestName(String name)
        {
            ((IJavaScriptExecutor)threadLocalDriver.Value).ExecuteScript("browserstack_executor: {\"action\": \"setSessionName\", \"arguments\": {\"name\":\"" + name + "\" }}");
        }

        protected static void MarkTestStatus()
        {
            if (TestContext.CurrentContext.Result.FailCount == 0)
            {
                ((IJavaScriptExecutor)threadLocalDriver.Value).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \"All " + TestContext.CurrentContext.Result.PassCount + " test(s) completed successfully.\"}}");
            }
            else
            {
                String errorMsg = TestContext.CurrentContext.Result.Message + ". Failed: " + TestContext.CurrentContext.Result.FailCount +
                    ", Passed: " + TestContext.CurrentContext.Result.PassCount +
                    ".".Substring(0, 255);
                ((IJavaScriptExecutor)threadLocalDriver.Value).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + errorMsg + " \"}}");
            }
        }

        protected static void Shutdown()
        {
            if (threadLocalDriver.IsValueCreated)
            {
                threadLocalDriver.Value.Quit();
            }

        }

        protected static IWebDriver GetDriver(DriverOptions driverOptions)
        {
                DriverFactory webDriverFactory = DriverFactory.GetInstance();
                threadLocalDriver.Value = webDriverFactory.CreateRemoteWebDriver(driverOptions);


            return threadLocalDriver.Value;
        }
    }
}

