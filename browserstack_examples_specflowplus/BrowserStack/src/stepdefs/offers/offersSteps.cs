using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System;
using BrowserStack.WebDriver.Core;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SpecflowBrowserStack.src.stepdefs;

namespace SpecflowBrowserStack.Steps
{
	[Binding]
	public class OffersSteps
	{
		private readonly IWebDriver _driver;
		private static bool result;
		WebDriverWait wait;

		public OffersSteps()
		{
			_driver = Hooks.threadLocalDriver.Value;
			wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
		}

		[Given(@"I navigate to website with mumbai geo-location")]
		public void GivenINavigateToWebsiteWithMumbaiGeo_Location()
		{
			_driver.Navigate().GoToUrl(DriverFactory.GetInstance().GetBaseUrl());
			((IJavaScriptExecutor)_driver).ExecuteScript("window.navigator.geolocation.getCurrentPosition = function(cb){cb({ coords: {accuracy: 20,altitude: null,altitudeAccuracy: null,heading: null,latitude: 19.043192,longitude: 75.86305240000002,speed: null}}); }");
		}

		[Then(@"I click on Offers link")]
		public void ThenIClickOnOffersLink()
		{
			wait.Until(ExpectedConditions.ElementExists(By.Id("offers")));
			_driver.FindElement(By.Id("offers")).Click();
		}

		[Then(@"I should see Offer elements")]
		public void ThenIShouldSeeOfferElements()
		{
			String text = _driver.FindElement(By.XPath("//div[@class='p-6 text-2xl tracking-wide text-center text-red-50']")).Text;
			Assert.AreEqual("We've promotional offers in your location.", text);

		}
	}
}
