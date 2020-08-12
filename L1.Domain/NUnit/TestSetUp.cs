using System;
using System.IO;
using L0.WebDriver.Browser;
using L0.WebDriver.Configuration;
using L1.Domain.CleanUp;
using L1.Domain.Configuration;
using L1.Domain.Logs;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace L1.Domain.NUnit
{
	public class TestSetUp
	{
		protected BrowserTypes BrowserType;
		protected Browser Browser;
		private readonly CleanupQueue _cleanupActions = new CleanupQueue();
		private string _currentScreenshotFolder;

		public TestSetUp(string browserType)
		{
			BrowserType = BrowserHelper.EvaluateType(browserType);
		}

		public TestSetUp()
		{
			BrowserType = BrowserTypes.FromConfig;
		}
		
		private bool IsTestFailed() => TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Error) 
		                               || TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Failure);

		[OneTimeSetUp]
        public void BaseOneTimeSetUp()
        {
	        Browser = BrowserProvider.GetBrowser(BrowserType);
	        Log.Info("BaseOneTimeSetUp: Test suite started: " + TestContext.CurrentContext.Test.ClassName);
	        Log.Info("BaseOneTimeSetUp: Current url is " + (Browser.Driver != null ? Browser.Driver.Url : "null"));

	        if (_currentScreenshotFolder == null)
		        _currentScreenshotFolder = Path.Combine(Config.ScreenshotFolder,
			        DateTime.Now.ToString("yyyy.MM.dd_HHmmss"));
		}

        [SetUp]
        public void BaseSetUp()
        {
	        Log.Info("Test started: " + TestContext.CurrentContext.Test.FullName);
        }

        [TearDown]
        public void BaseTearDown()
        {
	        if (IsTestFailed())
		        TakeScreenShot();

	        try
            {
	            _cleanupActions.ExecuteQueue();
            }
            catch (Exception ignore)
            {
	            Log.Error("Error in test cleanup: " + TestContext.CurrentContext.Test.Name, ignore);
            }
            
            Log.Info($"BaseTearDown: Test finished ({GetStatusString()}): " + TestContext.CurrentContext.Test.FullName + "\r\n");
        }

        [OneTimeTearDown]
        public void BaseOneTimeTearDown()
        {
	        Log.Info("BaseOneTimeTearDown: Test suite finished: " + TestContext.CurrentContext.Test.ClassName + "\r\n");
        }

        protected void AddCleanupAction(Action cleanupAction)
        {
	        _cleanupActions.Enqueue(cleanupAction);
        }

        protected void RunAndAddCleanupAction(Action cleanupAction)
        {
	        cleanupAction();
	        AddCleanupAction(cleanupAction);
        }

        private void TakeScreenShot()
        {
	        if (!Directory.Exists(_currentScreenshotFolder))
	        {
		        Directory.CreateDirectory(_currentScreenshotFolder);
		        Log.Info($"Current screen shot folder created: '{_currentScreenshotFolder}'");
	        }
	        var fileName = TestContext.CurrentContext.Test.FullName + ".png";
	        var screenshotFileName = Path.Combine(_currentScreenshotFolder, fileName);
	        Browser.ScreenShot.Take(screenshotFileName);
		}

        private string GetStatusString()
        {
	        if (IsTestFailed())
		        return "failed";
	        if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Success))
		        return "passed";
	        return "unknown";
        }
    }
}