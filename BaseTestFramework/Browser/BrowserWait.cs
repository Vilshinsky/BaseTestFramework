using System;
using L0.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace L0.WebDriver.Browser
{
	public class BrowserWait
	{
		private readonly Browser _browser;
		private readonly IWebDriver _driver;

        public BrowserWait(Browser browser)
        {
	        _browser = browser;
	        _driver = _browser.Driver;
        }

        /// <summary>
        /// This method could be used if you need to implement additional page waiting logic
        /// </summary>
        public Action<IWebDriver> WaitTemplateMethod { get; set; }

        private void UntilDocumentReadyStateComplete()
        {
	        try
	        {
		        IWait<IWebDriver> wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30.00));
		        wait.Until(driver1 => _browser.JsExecutor.ExecuteScript("return document.readyState").Equals("complete"));
            }
	        catch (Exception e)
	        {
		        Log.Error("UntilDocumentReadyStateComplete: ", e);
            }
        }

        private void UntilAjaxInactive()
        {
	        try
	        {
		        IWait<IWebDriver> wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(15000));
		        wait.Until(driver1 => (bool)_browser.JsExecutor.ExecuteScript("return jQuery.active == 0"));
	        }
	        catch (Exception ex)
	        {
		        Log.Error("UntilAjaxInactive: ", ex);
	        }
        }

        public void UntilPageReady()
        {
	        UntilDocumentReadyStateComplete();
	        UntilAjaxInactive();
        }

        public void UntilRefreshComplete()
        {
            IWait<IWebDriver> wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(20000));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//div[@class='modal']")));
            UntilPageReady();
        }

        private void ForAjax(int timeout = 20000)
        {
            try
            {
                IWait<IWebDriver> wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(timeout));
                wait.Until(driver1 => (bool)_browser.JsExecutor.ExecuteScript("return jQuery.active == 0"));
            }
            catch (Exception ex)
            {
	            Log.Error("ForAjax: ", ex);
            }
        }
    }
}