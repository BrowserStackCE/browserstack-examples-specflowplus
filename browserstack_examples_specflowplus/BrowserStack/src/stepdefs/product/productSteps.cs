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
	public class productSteps
	{
		private readonly IWebDriver _driver;
		private static bool result;
		WebDriverWait wait;

		public productSteps()
		{
			_driver = Hooks.threadLocalDriver.Value;
			wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
		}
		[Given(@"I navigate to website locally")]
		public void GivenINavigateToWebsiteLocally()
		{
			_driver.Navigate().GoToUrl("http://localhost:3000/");
		}


		[Given(@"I press the Apple Vendor Filter")]
		public void GivenIPressTheAppleVendorFilter()
		{
			_driver.FindElement(By.XPath("//span[@class='checkmark' and text()='Apple']")).Click();
		}

		[Then(@"I should see (.*) items in the list")]
		public void ThenIShouldSeeItemsInTheList(int noOfproducts)
		{
			string numberOfProducts = _driver.FindElement(By.XPath("//small[@class='products-found']")).Text;
		}

		[Given(@"I order by lowest to highest")]
		public void GivenIOrderByLowestToHighest()
		{
			IWebElement dropDown = _driver.FindElement(By.XPath("//select"));
			SelectElement select = new SelectElement(dropDown);
			select.SelectByText("Lowest to highest");
		}

		[Then(@"I should see prices in ascending order")]
		public void ThenIShouldSeePricesInAscendingOrder()
		{
			String fristElementPrice = _driver.FindElement(By.XPath("(//div[@class='val']//b)[1]")).Text;
			String secondElementPrice = _driver.FindElement(By.XPath("(//div[@class='val']//b)[2]")).Text;
			Assert.True(Convert.ToInt32(fristElementPrice) < Convert.ToInt32(secondElementPrice));
		}
	}
}
