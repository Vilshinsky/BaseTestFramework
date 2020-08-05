using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace L0.WebDriver.WebDriver
{
	public static class WebDriverExt
	{
		private static readonly int ImplicitlyWaitTimeMs = WebDriverConfiguration.ImplicitlyWaitMs;

        public static bool IsElementPresent(this IWebDriver driver, By by)
        {
            bool result;

            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(0);
                result = driver.FindElements(by).Count > 0;
            }
            finally
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(ImplicitlyWaitTimeMs);
            }

            return result;
        }

        public static IWebElement WaitElementPresent(this IWebDriver driver, By by, int timeoutMs = 15000)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutMs));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
            
            return element;
        }

        public static IWebElement WaitElementVisible(this IWebDriver driver, By by, int timeoutMs = 15000)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutMs));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException), typeof(ElementNotVisibleException));

            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));

            return element;
        }

        public static IWebElement WaitElementReady(this IWebDriver driver, By by, int timeout = 15000)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));

            wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));

            return driver.FindElement(by);
        }
	}
}