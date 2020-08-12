using Microsoft.Extensions.Configuration;

namespace L0.WebDriver.Configuration
{
	public static class Config
	{
		private static IConfiguration _config => InitConfiguration();

		public static string BrowserType => _config["browserType"];

		public static int ImplicitlyWaitMs => int.Parse(_config["implicitlyWait"]);

		public static bool IsHeadless => bool.Parse(_config["isHeadless"]);

		public static string DownloadsFolder => _config["downloadsFolder"];
		public static string RemoteWebDriverAddress => _config["remoteWebDriverAddress"];
		public static string ScreenshotFolder => _config["screenshotFolder"];

		private static IConfiguration InitConfiguration()
		{
			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.test.json")
				.Build();
			return config;
		}
    }
}