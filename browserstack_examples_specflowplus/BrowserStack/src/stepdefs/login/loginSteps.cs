using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using NUnit.Framework;
using SpecflowBrowserStack.src.stepdefs;
using SeleniumExtras.WaitHelpers;

namespace SpecflowBrowserStack.Steps
{
	[Binding]
	public class LoginSteps
	{
		private readonly IWebDriver _driver;
		private static bool result;
		WebDriverWait wait;

		public LoginSteps()
		{
			_driver = Hooks.ThreadLocalDriver.Value;
			wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
		}

		[Given(@"I navigate to website")]
		public void GivenINavigatedTowebsite()
		{
			_driver.Navigate().GoToUrl("https://bstackdemo.com");
		}

		[Then(@"I click on Sign In link")]
		public void ThenIClickOnSignInLink()
		{
			_driver.FindElement(By.Id("signin")).Click();
		}

		[When(@"I type '(.*)' in username")]
		public void ITypeUsername(string username)
		{
			wait.Until(ExpectedConditions.ElementExists(By.Id("react-select-2-input")));
			_driver.FindElement(By.Id("react-select-2-input")).SendKeys(username);
			_driver.FindElement(By.Id("react-select-2-input")).SendKeys(Keys.Enter);
		}
		[When(@"I type '(.*)' in password")]
		public void ITypePassword(string password)
		{
			_driver.FindElement(By.Id("react-select-3-input")).SendKeys(password);
			_driver.FindElement(By.Id("react-select-3-input")).SendKeys(Keys.Enter);
		}

		[Then(@"I press Log In Button")]
		public void IPressLogInButton()
		{
			_driver.FindElement(By.Id("login-btn")).Click();
		}
		[Then(@"I should see user '(.*)' logged in")]
		public void IshouldSeeUsername(string username)
		{
			if (username == "locked_user")
			{
				wait.Until(ExpectedConditions.ElementExists(By.XPath("//h3[@class='api-error']")));
				string errorMsg = _driver.FindElement(By.XPath("//h3[@class='api-error']")).Text;
				Assert.AreEqual(errorMsg, "Your account has been locked.");

			}
			else
			{
				wait.Until(ExpectedConditions.ElementExists(By.XPath("//span[@class='username']")));
				string displayedUsername = _driver.FindElement(By.XPath("//span[@class='username']")).Text;
				Assert.AreEqual(username, displayedUsername);

			}
		}
	}
}
