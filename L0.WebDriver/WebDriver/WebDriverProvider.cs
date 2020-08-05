using System;
using L0.WebDriver.Browser;
using L0.WebDriver.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace L0.WebDriver.WebDriver
{
	static class WebDriverProvider
	{
		internal static IWebDriver CreateWebDriver(BrowserTypes browserType = BrowserTypes.FromConfig)
		{
			return GetDriverType(browserType.ToString().ToLower());
		}

		private static IWebDriver GetDriverType(string browserType)
		{
			IWebDriver driver;
			var browser = browserType == "fromconfig" ? Config.BrowserType : browserType;

			switch (browser)
			{
				case "chrome":
					driver = CreateChromeDriver();
					break;
				case "firefox":
					driver = CreateFirefoxDriver();
					break;

				default: throw new Exception($"WebDriverProvider: Unexpected browser type [{browser}] was selected.");
			}

			return driver;
		}

		private static IWebDriver CreateChromeDriver(params string[] addArgument)
		{
			var options = new ChromeOptions
			{
				BrowserVersion = "84.0.4147.89",
				PlatformName = "WINDOWS"
			};
			options.AddUserProfilePreference("download.default_directory", Config.DownloadsFolder);
			options.AddUserProfilePreference("prompt_for_download", false);
			options.AddArgument("--start-maximized");
			if (Config.IsHeadless)
				options.AddArgument("headless");

			if (addArgument.Length > 0)
				foreach (var argument in addArgument)
					options.AddArgument(argument);

			var remoteSettings = new RemoteSessionSettings(options);
			var uri = new Uri(Config.RemoteWebDriverAddress);
			var drv = new RemoteWebDriver(uri, remoteSettings) { FileDetector = new LocalFileDetector() };

			return drv;
		}

		private static IWebDriver CreateFirefoxDriver(params string[] addArgument)
		{
			var options = new FirefoxOptions
			{
				BrowserVersion = "79.0",
				PlatformName = "WINDOWS"
			};

			options.AddArgument("--start-maximized");
			if (Config.IsHeadless)
				options.AddArgument("headless");

			if (addArgument.Length > 0)
				foreach (var argument in addArgument)
					options.AddArgument(argument);

			var remoteSettings = new RemoteSessionSettings(options);
			var uri = new Uri(Config.RemoteWebDriverAddress);
			var drv = new RemoteWebDriver(uri, remoteSettings) { FileDetector = new LocalFileDetector() };

			return drv;
		}
	}
}