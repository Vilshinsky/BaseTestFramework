using NUnit.Framework;

namespace L2.Tests
{
	[TestFixture(1, 2, 3)]
	[TestFixture(0, 0, 6)]
	[Parallelizable(ParallelScope.Fixtures)]
	public class TestClass1
	{
		private int _a;
		private int _b;
		private int _c;

		public TestClass1(int a, int b, int c)
		{
			_a = a;
			_b = b;
			_c = c;
		}

		[Test]
		public void SumTest()
		{
			Assert.True(_a + _b + _c == 6);
		}

		[Test]
		public void SumNegTest()
		{
			Assert.True(_a + _b + _c != 0);
		}
	}
}