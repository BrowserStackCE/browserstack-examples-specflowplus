using TechTalk.SpecFlow;
//using SpecflowBrowserStack.Drivers;
using OpenQA.Selenium;
using BrowserStack.App;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace SpecflowBrowserStack.Steps
{
    [Binding]
	[TestFixtureSource(typeof(WebDriverTestRunner))]
	public class loginSteps : WebDriverTestRunner
	{
		private readonly IWebDriver _driver;
		private static bool result;
		WebDriverWait wait;

		public loginSteps(DesiredCapabilities driverOptions)
		{
			_driver = GetDriver(driverOptions);
			 wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
			
		}

		[Given(@"I navigate to website")]
		public void GivenINavigatedTowebsite()
		{
			_driver.Navigate().GoToUrl("https://bstackdemo.com/");
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
				result = Assert.Equals(errorMsg, "Your account has been locked.");

			}
			else
			{
				wait.Until(ExpectedConditions.ElementExists(By.XPath("//span[@class='username']")));
				string displayedUsername = _driver.FindElement(By.XPath("//span[@class='username']")).Text;
				result = Assert.Equals(username, displayedUsername);

			}
		}
	}
}
