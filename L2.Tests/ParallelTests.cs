using System.Threading;
using L1.Domain.Logs;
using L1.Domain.NUnit;
using L1.Domain.PageObject;
using NUnit.Framework;

namespace L2.Tests
{
	[TestFixture]
	[Parallelizable]
	public class ParallelTests : TestSetUp
	{
		protected GoogleSearchPage GoogleSearchPage;

		[SetUp]
		public void SetUp()
		{
			GoogleSearchPage = Browser.OpenPage<GoogleSearchPage>();
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