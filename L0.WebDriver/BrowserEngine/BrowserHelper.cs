using System;
using L0.WebDriver.Configuration;

namespace L0.WebDriver.BrowserEngine
{
	public static class BrowserHelper
	{
		public static BrowserTypes EvaluateType(string browserType)
		{
			var type = Config.BrowserType switch
			{
				"chrome" => BrowserTypes.Chrome,
				"firefox" => BrowserTypes.Firefox,
				_ => throw new ArgumentException($"Not supported browser type requested: '{Config.BrowserType}'.")
			};

			return type;
		}
	}
}