using System;
using L0.WebDriver.Configuration;
using L0.WebDriver.PageObject;
using L0.WebDriver.WebDriver;
using log4net;
using OpenQA.Selenium;

namespace L0.WebDriver.Browser
{
	public enum BrowserTypes
	{
		Chrome,
		Firefox,
		Explorer,
		FromConfig
	}

	public class Browser
	{
		private readonly ILog _log;
		private readonly BrowserTypes _browserType;

		public Browser(BrowserTypes browserType, ILog log)
		{
			_log = log;
			_browserType = browserType;
			Driver = GetDriver(_browserType);
		}

		public IWebDriver Driver { get; private set; }
		public BrowserTypes BrowserType => _browserType;
		public string CurrentUrl => Driver.Url;
		public BrowserAlert Alert => new BrowserAlert(this);
		public BrowserScreenShot ScreenShot => new BrowserScreenShot(Driver, HandleWebDriverException);
		public JsExecutor JsExecutor => new JsExecutor(Driver);
		public BrowserWait Wait => new BrowserWait(this, _log);

		public Action<IWebDriver> WaitTemplateMethod { get; set; }

		private IWebDriver GetDriver(BrowserTypes browserType)
		{
			IWebDriver driver;

			try
			{
				driver = WebDriverProvider.CreateWebDriver(browserType);
			}
			catch (Exception ex)
			{
				_log.Error("", ex);
				throw;
			}

			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(Config.ImplicitlyWaitMs);
			driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromMilliseconds(30000);
			driver.Manage().Window.Maximize();

			return driver;
		}

		public void Close()
		{
			try
			{
				if (Driver != null)
				{
					Driver.Quit();
					Driver = null;
				}
			}
			catch (Exception ex)
			{
				_log.Error("Close: ", ex);
			}
		}

		public void Restart()
		{
			_log.Info("Restart: Restarting browser");

			Close();
			Driver = GetDriver(_browserType);

			_log.Info("Restart: done");
		}

		public void OpenPage(string url)
		{
			try
			{
				Driver.Navigate().GoToUrl(url);
				_log.Info($"OpenPage: {url}");
			}
			catch (Exception ex)
			{
				HandleWebDriverException(ex);
			}
		}

		public T OpenPage<T>(bool waitPage = true) where T : BasePage, new()
		{
			var page = new T();

			_log.Debug($"OpenPage: {page.PageUrl}");
			OpenPage(page.PageUrl);
			if (waitPage)
			{
				_log.Debug("OpenPage: waiting page starting");
				//Wait.UntilPageReady();
				page.WaitUntilPageLoaded();
			}

			_log.Debug("OpenPage: done");

			return page;
		}

		private void HandleWebDriverException(Exception ex)
		{
			//if session was timed out we need to start new browser in order to not fail whole session
			_log.Error("", ex);
			if (ex is InvalidOperationException || ex is WebDriverException)
			{
				_log.Debug("HandleWebDriverException: starting new driver");
				Restart();
				_log.Debug("HandleWebDriverException: starting new driver - done");
			}
			_log.Debug("HandleWebDriverException: Throwing exception to fail test.");
			throw new Exception("HandleWebDriverException: done. Please see inner exception for details.", ex);
		}
	}
}