using System;
using L0.WebDriver.WebDriver;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace L2.Tests.Specflow.Steps
{
    [Binding]
    public class GoogleSearchSteps : IDisposable
    {
	    private IWebDriver _driver;

        [Given(@"I am on the search page")]
        public void GivenIAmOnTheSearchPage()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(5000);
            _driver.Navigate().GoToUrl("https://www.google.com/");
        }
        
        [When(@"I enter '(.*)' search value")]
        public void WhenIEnterSearchValue(string p0)
        {
            _driver.FindElement(By.Name("q")).SendKeys(p0);
            _driver.WaitElementReady(By.XPath("//div[contains(@class,'suggestions-inner-container')]")).Click();
        }
        
        [Then(@"expected link '(.*)' should be listed in the suggestions")]
        public void ThenExpectedLinkShouldBeListedInTheSuggestions(string p0)
        {
            Assert.True(_driver.FindElements(By.XPath($"//h3[text()='{p0}']")).Count > 0, 
	            $"There wasn't {p0} value in the search suggestions.");
        }

        public void Dispose()
        {
	        _driver?.Dispose();
        }
    }
}