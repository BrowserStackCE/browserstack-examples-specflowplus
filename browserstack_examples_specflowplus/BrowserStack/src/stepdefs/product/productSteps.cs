using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SpecflowBrowserStack.src.stepdefs;

namespace SpecflowBrowserStack.Steps
{
	[Binding]
	public class ProductSteps
	{
		private readonly IWebDriver _driver;
		private static bool result;
		WebDriverWait wait;

		public ProductSteps()
		{
			_driver = Hooks.ThreadLocalDriver.Value;
			wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
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
