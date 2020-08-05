using System.Threading;
using L0.Helpers;
using L1.Domain.NUnit;
using NUnit.Framework;
using OpenQA.Selenium;

namespace L2.Tests
{
	[TestFixture]
	[Parallelizable]
	public class ParallelTests : TestSetUp
	{
		[SetUp]
		public void SetUp()
		{
			Browser.OpenPage("http://www.google.com");
			
		}

		[Test]
		public void Test0()
		{
			var insertText = "Test0";

			Browser.Driver.FindElement(By.Name("q")).SendKeys(insertText);
			Log.Info($"Text {insertText} inserted into input.");

			Thread.Sleep(2000);
			Log.Info("Waited 2 seconds.");

			Assert.That(Browser.Driver.FindElement(By.Name("q")).GetAttribute("value") == insertText);
		}

		[Test]
		public void Test1()
		{
			var insertText = "Test1";

			Browser.Driver.FindElement(By.Name("q")).SendKeys(insertText);
			Log.Info($"Text {insertText} inserted into input.");

			Thread.Sleep(2000);
			Log.Info("Waited 2 seconds.");

			Assert.That(Browser.Driver.FindElement(By.Name("q")).GetAttribute("value") == insertText);
		}
	}
}