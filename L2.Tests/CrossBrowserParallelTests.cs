using System.Threading;
using L1.Domain.Logs;
using L1.Domain.NUnit;
using L1.Domain.PageObject;
using NUnit.Framework;

namespace L2.Tests
{
	[TestFixture("chrome")]
	[TestFixture("firefox")]
	[Parallelizable]
	public class CrossBrowserParallelTests : TestSetUp
	{
		public CrossBrowserParallelTests(string browserType) : base(browserType) { }

		protected GoogleSearchPage GoogleSearchPage;

		[SetUp]
		public void SetUp()
		{
			GoogleSearchPage = new GoogleSearchPage(Browser);
			Browser.OpenPage(GoogleSearchPage.PageUrl);
			Log.Info("Navigated to URL.");
		}

		[Test]
		public void Test0()
		{
			var insertText = "Test0";

			GoogleSearchPage.SearchInput = insertText;
			Log.Info($"Text {insertText} inserted into input.");

			Thread.Sleep(2000);
			Log.Info("Waited 2 seconds.");

			Assert.That(GoogleSearchPage.SearchInput == insertText);
		}

		[Test]
		public void Test1()
		{
			var insertText = "Test1";

			GoogleSearchPage.SearchInput = insertText;
			Log.Info($"Text {insertText} inserted into input.");

			Thread.Sleep(2000);
			Log.Info("Waited 2 seconds.");

			Assert.That(GoogleSearchPage.SearchInput == insertText);
		}
	}
}