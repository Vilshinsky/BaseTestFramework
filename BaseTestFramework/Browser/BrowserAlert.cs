using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace L0.WebDriver.Browser
{
	public class BrowserAlert
	{
		private readonly Browser _browser;
		public BrowserAlert(Browser browser)
		{
			_browser = browser;
		}

		public string Text => WaitForAlert().Text;

		public string Dismiss()
		{
			return HandleAlert(false);
		}

		public string Accept()
		{
			return HandleAlert(true);
		}

		private string HandleAlert(bool action)
		{
			var alert = WaitForAlert();
			var alertText = alert.Text;
			if (action)
				alert.Accept();
			else
				alert.Dismiss();

			return alertText;
		}

		private IAlert WaitForAlert(int timeoutMs = 15000)
		{
			var ww = new WebDriverWait(_browser.Driver, TimeSpan.FromMilliseconds(timeoutMs));
			ww.IgnoreExceptionTypes(typeof(NoAlertPresentException));
			try
			{
				ww.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
			}
			catch (WebDriverTimeoutException e)
			{
				throw new WebDriverTimeoutException($"BrowserAlert is not appeared after {timeoutMs} milliseconds timeout", e);
			}
			return _browser.Driver.SwitchTo().Alert();
		}

		public string AcceptIfAny(bool toWait = false)
		{
			string message = string.Empty;

			try
			{
				var alert = toWait ? WaitForAlert() : _browser.Driver.SwitchTo().Alert();
				message = alert.Text;
				alert.Accept();
			}
			catch (Exception)
			{
				//_log.Error(ignore);
			}

			return message;
		}
	}
}