using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SpecflowBrowserStack.src.stepdefs;
using SeleniumExtras.WaitHelpers;

namespace SpecflowBrowserStack.Steps
{
	[Binding]
	public class E2eSteps

	{
		private readonly IWebDriver _driver;
		private bool result;
		WebDriverWait wait;

		public E2eSteps()
		{
			_driver = Hooks.ThreadLocalDriver.Value;
			wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
		}

		[Given(@"I navigate to website bogus")]
		public void GivenINavigatedTowebsite()
		{
			_driver.Navigate().GoToUrl("https://bstackdemo.com");
		}

		[Then(@"I click on Sign In link bogus")]
		public void ThenIClickOnSignInLink()
		{
			_driver.FindElement(By.Id("signin")).Click();
		}

		[When(@"I type ""(.*)"" in username")]
		public void WhenITypeInUsername(string username)
		{
            wait.Until(ExpectedConditions.ElementExists(By.Id("react-select-2-input")));
            _driver.FindElement(By.Id("react-select-2-input")).SendKeys(username);
			_driver.FindElement(By.Id("react-select-2-input")).SendKeys(Keys.Enter);
		}

		[When(@"I type ""(.*)"" in password")]
		public void WhenITypeInPassword(string password)
		{
			_driver.FindElement(By.Id("react-select-3-input")).SendKeys(password);
			_driver.FindElement(By.Id("react-select-3-input")).SendKeys(Keys.Enter);
		}

		[Then(@"I add two products to cart")]
		public void ThenIAddTwoProductsToCart()
		{

			wait.Until(ExpectedConditions.ElementExists(By.XPath("(//div[text()='Add to cart'])[1]")));
			_driver.FindElement(By.XPath("(//div[text()='Add to cart'])[1]")).Click();
			_driver.FindElement(By.XPath("(//div[text()='Add to cart'])[1]")).Click();

		}

		[Then(@"I click on Buy Button")]
		public void ThenIClickOnBuyButton()
		{
			_driver.FindElement(By.XPath("//div[text()='Checkout']")).Click();
		}

		[When(@"I type ""(.*)"" in firstNameInput input")]
		public void WhenITypeInFirstNameInputInput(string firstName)
		{
			wait.Until(ExpectedConditions.ElementExists(By.Id("firstNameInput")));
			_driver.FindElement(By.Id("firstNameInput")).SendKeys(firstName);
		}

		[When(@"I type ""(.*)"" in lastNameInput input")]
		public void WhenITypeInLastNameInputInput(string lastName)
		{
			_driver.FindElement(By.Id("lastNameInput")).SendKeys(lastName);
		}

		[When(@"I type ""(.*)"" in addressLineInput input")]
		public void WhenITypeInAddressLineInputInput(string address)
		{
			_driver.FindElement(By.Id("addressLine1Input")).SendKeys(address);
		}

		[When(@"I type ""(.*)"" in provinceInput input")]
		public void WhenITypeInProvinceInputInput(string province)
		{
			_driver.FindElement(By.Id("provinceInput")).SendKeys(province);
		}

		[When(@"I type ""(.*)"" in postCodeInput input")]
		public void WhenITypeInPostCodeInputInput(int postalCode)
		{
			_driver.FindElement(By.Id("postCodeInput")).SendKeys(postalCode.ToString());
		}

		[Then(@"I click on Checkout Button")]
		public void ThenIClickOnCheckoutButton()
		{
			_driver.FindElement(By.Id("checkout-shipping-continue")).Click();
			wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(text(),'Continue Shopping')]")));
			_driver.FindElement(By.XPath("//button[contains(text(),'Continue Shopping')]")).Click();
		}

		[Then(@"I click on ""(.*)"" link")]
		public void ThenIClickOnLink(string orders)
		{
			wait.Until(ExpectedConditions.ElementExists(By.Id("orders")));
			_driver.FindElement(By.Id("orders")).Click();
		}

		[Then(@"I should see elements in list")]
		public void ThenIShouldSeeElementsInList()
		{
			wait.Until(ExpectedConditions.ElementExists(By.XPath("(//span[@class='a-color-secondary label' ])[1]")));
			string orderPlaced = _driver.FindElement(By.XPath("(//span[@class='a-color-secondary label' ])[1]")).Text;
			Assert.AreEqual("order placed", orderPlaced.ToLower());

		}
	}
}
