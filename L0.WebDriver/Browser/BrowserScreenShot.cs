using System;
using OpenQA.Selenium;

namespace L0.WebDriver.Browser
{
	public class BrowserScreenShot
	{
		private readonly IWebDriver _driver;
		private Action<Exception> _handleException;

		public BrowserScreenShot(IWebDriver driver, Action<Exception> handleException)
		{
			_driver = driver;
			_handleException = handleException;
		}

		public void Take(string filePath)
		{
			try
			{
				var screenshotsDriver = _driver as ITakesScreenshot;
				var screenshots = screenshotsDriver.GetScreenshot();
				screenshots.SaveAsFile(filePath, ScreenshotImageFormat.Png);
			}
			catch (Exception e)
			{
				_handleException(e);
			}
		}
	}
}