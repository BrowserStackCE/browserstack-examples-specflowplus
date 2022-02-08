//using TechTalk.SpecFlow;
//using OpenQA.Selenium;
//using System;
//using OpenQA.Selenium.Support.UI;
//using BrowserStack.App;
//using OpenQA.Selenium.Chrome;
//using NUnit.Framework;
//using OpenQA.Selenium.Remote;

//namespace SpecflowBrowserStack.Steps
//{
//    [Binding]
//    [TestFixtureSource(typeof(WebDriverTestRunner))]
//    public class productSteps : WebDriverTestRunner
//    {
//        private readonly IWebDriver _driver;
//        private static bool result;
//        WebDriverWait wait;

//        public productSteps(DesiredCapabilities driverOptions)
//        {
//            _driver = GetDriver(driverOptions);
//            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
//        }
//        [Given(@"I navigate to website locally")]
//        public void GivenINavigateToWebsiteLocally()
//        {
//            _driver.Navigate().GoToUrl("http://localhost:3000/");
//        }


//        [Given(@"I press the Apple Vendor Filter")]
//        public void GivenIPressTheAppleVendorFilter()
//        {
//            _driver.FindElement(By.XPath("//span[@class='checkmark' and text()='Apple']")).Click();
//        }

//        [Then(@"I should see (.*) items in the list")]
//        public void ThenIShouldSeeItemsInTheList(int noOfproducts)
//        {
//            string numberOfProducts = _driver.FindElement(By.XPath("//small[@class='products-found']")).Text;
//        }

//        [Given(@"I order by lowest to highest")]
//        public void GivenIOrderByLowestToHighest()
//        {
//            IWebElement dropDown = _driver.FindElement(By.XPath("//select"));
//            SelectElement select = new SelectElement(dropDown);
//            select.SelectByText("Lowest to highest");
//        }

//        [Then(@"I should see prices in ascending order")]
//        public void ThenIShouldSeePricesInAscendingOrder()
//        {
//            String fristElementPrice = _driver.FindElement(By.XPath("(//div[@class='val']//b)[1]")).Text;
//            String secondElementPrice = _driver.FindElement(By.XPath("(//div[@class='val']//b)[2]")).Text;
//            Assert.True(Convert.ToInt32(fristElementPrice) < Convert.ToInt32(secondElementPrice));
//        }
//    }
//}
