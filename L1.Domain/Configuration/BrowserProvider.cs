using System;
using System.Collections.Generic;
using System.Threading;
using L0.Helpers;
using L0.WebDriver.Browser;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace L1.Domain.Configuration
{
	public class BrowserProvider
	{
		private static readonly object ThisLock = new object();
		private static readonly Dictionary<int, Browser> BrowserCollection = new Dictionary<int, Browser>();
		private static ILog ThisLog => Log.GetLogger();

		public static Browser GetBrowser(BrowserTypes browserType)
		{
			lock (ThisLock)
			{
				var threadId = Thread.CurrentThread.ManagedThreadId;
				var testContext = TestContext.CurrentContext.Test.FullName;

				if (BrowserCollection.ContainsKey(threadId))
				{
					if (BrowserCollection[threadId].BrowserType == browserType)
					{
						ThisLog.Debug($"{testContext}: Browser '{browserType}' for thread '{threadId}' existed in the BrowserCollection. Reused.");
						return BrowserCollection[threadId];
					}
				}

				BrowserCollection.Add(threadId, GetNewBrowser(browserType));
				ThisLog.Debug($"{testContext}: Browser '{browserType}' for thread '{threadId}' added into BrowserCollection.");
				return BrowserCollection[threadId];
			}
		}

		private static Browser GetNewBrowser(BrowserTypes browserType)
		{
			return new Browser(browserType, ThisLog) { WaitTemplateMethod = WaitWhileBodyLoading };
		}

		public static void CloseAll()
		{
			try
			{
				foreach (var browser in BrowserCollection)
				{
					browser.Value.Close();
				}

			}
			catch (Exception)
			{
				// Ignore errors if unable to close the browser
			}
		}

		private static void WaitWhileBodyLoading(IWebDriver driver)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));

			wait.Until(drv =>
			{
				try
				{
					var body = drv.FindElement(By.TagName("body"));
					//waiting while spinner still exists
					var modalExists = drv.ExecuteJavaScript<long>("return $('.modal').length") > 0;

					return body.GetAttribute("class") != "loading" && !modalExists;
				}
				catch
				{
					return false;
				}
			});
		}
    }
}