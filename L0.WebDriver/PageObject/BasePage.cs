using OpenQA.Selenium;

namespace L0.WebDriver.PageObject
{
	public abstract class BasePage
	{
		protected L0.WebDriver.Browser.Browser Browser;

		protected IWebDriver Driver;

		public abstract string Title { get; }
		public abstract string PageUrl { get; }

		public virtual void WaitUntilPageLoaded()
		{
		}
    }
}