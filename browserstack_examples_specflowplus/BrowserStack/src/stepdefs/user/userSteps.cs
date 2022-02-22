using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using SpecflowBrowserStack.src.stepdefs;

namespace SpecflowBrowserStack.Steps
{
	[Binding]
	public class UserSteps
	{
		private readonly IWebDriver _driver;
		private static bool result;
		WebDriverWait wait;
		public UserSteps()
		{
			_driver = Hooks.threadLocalDriver.Value;
			wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
		}

		[Then(@"I should see no image loaded")]
		public void ThenIShouldSeeNoImageLoaded()
		{
			String src = _driver.FindElement(By.XPath("//img[@alt='iPhone 12']")).GetAttribute("src");
			Assert.AreEqual("img", src);
		}
	}
}
