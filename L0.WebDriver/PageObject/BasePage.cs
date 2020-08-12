using L0.WebDriver.BrowserEngine;
using OpenQA.Selenium;

namespace L0.WebDriver.PageObject
{
	public abstract class BasePage
	{
		protected Browser Browser;

		protected IWebDriver Driver;

		public abstract string Title { get; }
		public abstract string PageUrl { get; }

		public virtual void WaitUntilPageLoaded()
		{
		}
    }
}