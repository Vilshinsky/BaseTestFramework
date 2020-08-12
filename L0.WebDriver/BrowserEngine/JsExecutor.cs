using OpenQA.Selenium;

namespace L0.WebDriver.BrowserEngine
{
	public class JsExecutor
	{
		private readonly IWebDriver _driver;

		public JsExecutor(IWebDriver driver)
		{
			_driver = driver;
		}

		public object ExecuteScript(string jsCode, params object[] args)
		{
			return ((IJavaScriptExecutor)_driver).ExecuteScript(jsCode, args);
		}

		public IWebElement ScrollIntoView(IWebElement element)
		{
			((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
			return element;
		}
	}
}