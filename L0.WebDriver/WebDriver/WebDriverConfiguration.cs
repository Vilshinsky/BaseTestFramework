using System;
using L0.WebDriver.Browser;

namespace L0.WebDriver.WebDriver
{
	public class WebDriverConfiguration
	{
		private static string Config = "App.config";
		public static BrowserTypes BrowserType = BrowserTypes.Chrome;
		public static string DownloadsFolder => AppDomain.CurrentDomain.BaseDirectory;
		public static bool IsHeadless { get; set; }
		public static bool UseRemoteDriver { get; set; }
		public static bool UseLocalDriverWhenDebug => true;
		public static string RemoteWebDriverAddress { get; set; }
		public static int ImplicitlyWaitMs => 1000;

		private static string GetSettingValue(string settingName)
		{
			throw new NotImplementedException();
		}
	}
}