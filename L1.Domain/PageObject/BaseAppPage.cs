using System;
using L0.WebDriver.Browser;
using L0.WebDriver.Configuration;
using L0.WebDriver.PageObject;
using L1.Domain.Configuration;

namespace L1.Domain.PageObject
{
	public class BaseAppPage : BasePage
	{
		//For cross-browser test suites
		protected BaseAppPage(Browser browser)
		{
			Browser = browser;
			Driver = Browser.Driver;
		}

		protected BaseAppPage()
		{
			var browserType = BrowserHelper.EvaluateType(Config.BrowserType);

			Browser = BrowserProvider.GetBrowser(browserType);
			Driver = Browser.Driver;
		}

		public override string Title => Driver.Title;
		public override string PageUrl { get; }
	}
}