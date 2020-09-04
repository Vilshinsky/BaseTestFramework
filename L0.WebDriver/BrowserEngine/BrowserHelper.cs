using System;

namespace L0.WebDriver.BrowserEngine
{
	public static class BrowserHelper
	{
		public static BrowserTypes EvaluateType(string browserType)
		{
			var type = browserType switch
			{
				"chrome" => BrowserTypes.Chrome,
				"firefox" => BrowserTypes.Firefox,
				_ => throw new ArgumentException($"Not supported browser type requested: '{browserType}'.")
			};

			return type;
		}
	}
}