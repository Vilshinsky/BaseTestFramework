using System;
using L0.WebDriver.BrowserEngine;
using L0.WebDriver.Configuration;
using L0.WebDriver.PageObject;
using L1.Domain.Configuration;
using OpenQA.Selenium;

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

		protected BaseAppPage() : this(DefaultBrowser)
		{
			Browser = DefaultBrowser;
			Driver = Browser.Driver;
		}

		private static Browser DefaultBrowser => BrowserProvider.GetBrowser(BrowserHelper.EvaluateType(Config.BrowserType));
		
		public override string Title => Driver.Title;
		public override string PageUrl { get; }

		protected IWebElement ScrollIntoView(IWebElement element)
		{
			Browser.JsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
			return element;
		}
	}
}