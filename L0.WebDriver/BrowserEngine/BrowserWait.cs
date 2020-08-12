using System;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace L0.WebDriver.BrowserEngine
{
	public class BrowserWait
	{
		private readonly Browser _browser;
		private readonly IWebDriver _driver;
		private readonly ILog _log;

        public BrowserWait(Browser browser, ILog log)
        {
	        _browser = browser;
	        _driver = _browser.Driver;
	        _log = log;
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
		        _log.Error("UntilDocumentReadyStateComplete: ", e);
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
		        _log.Error("UntilAjaxInactive: ", ex);
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
	            _log.Error("ForAjax: ", ex);
            }
        }
    }
}