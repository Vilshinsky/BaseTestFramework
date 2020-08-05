using System;
using L0.Helpers;
using L0.WebDriver.Browser;
using L1.Domain.CleanUp;
using L1.Domain.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace L1.Domain.NUnit
{
	public class TestSetUp
	{
		protected BrowserTypes BrowserType;
		protected Browser Browser;
		private readonly CleanupQueue _cleanupActions = new CleanupQueue();

		public TestSetUp(string browserType)
		{
			BrowserType = browserType switch
			{
				"chrome" => BrowserTypes.Chrome,
				"firefox" => BrowserTypes.Firefox,
				_ => throw new ArgumentException($"Not supported browser type requested: '{browserType}'.")
			};
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
			//Browser = new Browser(BrowserType, Log.GetLogger());
	        Browser = BrowserProvider.GetBrowser(BrowserType);
	        Log.Info("BaseOneTimeSetUp: Test suite started: " + TestContext.CurrentContext.Test.ClassName);
	        Log.Info("BaseOneTimeSetUp: Current url is " + (Browser.Driver != null ? Browser.Driver.Url : "null"));
        }

        [SetUp]
        public void BaseSetUp()
        {
	        //Browser = BrowserProvider.GetBrowser(BrowserType);
            Log.Info("Test started: " + TestContext.CurrentContext.Test.FullName);
        }

        [TearDown]
        public void BaseTearDown()
        {
	        if (IsTestFailed())
	            Browser.ScreenShot.Take(@"D:\");
            try
            {
	            //BrowserProvider.CloseBrowser(BrowserType);
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
	        Log.Info($"BaseOneTimeTearDown: Test suite finished: " + TestContext.CurrentContext.Test.ClassName + "\r\n");
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