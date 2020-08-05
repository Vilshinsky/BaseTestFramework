using System;

namespace L1.Domain.CleanUp
{
	public class CleanupAction
	{
		public Action Method { get; set; }
		public CleanupAction Next { get; set; }
	}
}