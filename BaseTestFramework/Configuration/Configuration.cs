using Microsoft.Extensions.Configuration;

namespace L0.WebDriver.Configuration
{
	public static class Configuration
	{
		public static string BrowserType => InitConfiguration()["browserType"];

		private static IConfiguration InitConfiguration()
		{
			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.test.json")
				.Build();
			return config;
		}
    }
}