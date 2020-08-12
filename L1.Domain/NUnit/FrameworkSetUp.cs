using L1.Domain.Configuration;
using L1.Domain.Logs;
using NUnit.Framework;

namespace L1.Domain.NUnit
{
	public class FrameworkSetUp
	{
		[OneTimeSetUp]
		public void BaseOneTimeSetUp()
		{
			Log.Info("* * * * * * * * * * * * Test run started * * * * * * * * * * * *");
		}

		[OneTimeTearDown]
		public void BaseOneTimeTearDown()
		{
			BrowserProvider.CloseAll();
			Log.Info("* * * * * * * * * * * * Test run finished * * * * * * * * * * * *");
		}
	}
}