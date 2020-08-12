using L0.WebDriver.Browser;
using OpenQA.Selenium;

namespace L1.Domain.PageObject
{
	public class GoogleSearchPage : BaseAppPage
	{
		public GoogleSearchPage(Browser browser) : base(browser) { }

		public GoogleSearchPage() { }

		public override string Title { get; }
		public override string PageUrl => "http://www.google.com";

		public string SearchInput
		{
			get => Driver.FindElement(By.Name("q")).GetAttribute("value");
			set => Driver.FindElement(By.Name("q")).SendKeys(value);
		}
	}
}