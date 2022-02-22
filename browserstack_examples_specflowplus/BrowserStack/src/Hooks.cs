using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using BrowserStack.WebDriver.Core;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace SpecflowBrowserStack.src.stepdefs
{
	[Binding]
	public class Hooks
	{
		private static FeatureContext _featureContext;
		private static ScenarioContext _scenarioContext;

		public static ThreadLocal<IWebDriver> ThreadLocalDriver = new ThreadLocal<IWebDriver>();
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

		public Hooks(FeatureContext featureContext, ScenarioContext scenarioContext)
		{
			_featureContext = featureContext;
			_scenarioContext = scenarioContext;
		}


		[BeforeScenario]
		public static void Initialize(ScenarioContext scenarioContext)
		{

			GetDriver(GetWebDriverObject());

			SetTestName(scenarioContext.ScenarioInfo.Title);
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

		}

		protected static void SetTestName(String name)
		{
			((IJavaScriptExecutor)ThreadLocalDriver.Value).ExecuteScript("browserstack_executor: {\"action\": \"setSessionName\", \"arguments\": {\"name\":\"" + name + "\" }}");
		}

		protected static void MarkTestStatus()
		{
			if (TestContext.CurrentContext.Result.FailCount == 0)
			{
				((IJavaScriptExecutor)ThreadLocalDriver.Value).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \"All " + TestContext.CurrentContext.Result.PassCount + " test(s) completed successfully.\"}}");
			}
			else
			{
				((IJavaScriptExecutor)ThreadLocalDriver.Value).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"Atleast 1 assertion failed\"}}");
			}
		}

		protected static void Shutdown()
		{
			if (ThreadLocalDriver.IsValueCreated)
			{
				ThreadLocalDriver.Value.Quit();
			}

		}

		protected static IWebDriver GetDriver(DriverOptions driverOptions)
		{
			DriverFactory webDriverFactory = DriverFactory.GetInstance();
			ThreadLocalDriver.Value = webDriverFactory.CreateRemoteWebDriver(driverOptions);


			return ThreadLocalDriver.Value;
		}
	}
}

