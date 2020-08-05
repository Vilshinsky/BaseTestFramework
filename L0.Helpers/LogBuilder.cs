using System;
using System.IO;
using System.Text;

namespace L0.Helpers
{
	public class LogBuilder
	{

		private static StringBuilder builder = new StringBuilder();

		public static void Debug(string message)
		{
			builder.AppendLine(DateTime.Now.ToString("[yy-MM-dd hh-mm-ss] ") + "DEBUG: " + message);
		}

		public static void Info(string message)
		{
			builder.AppendLine(DateTime.Now.ToString("[yy-MM-dd hh-mm-ss] ") + "INFO: " + message);
		}


		public static void Error(object message, Exception exception)
		{
			builder.AppendLine(DateTime.Now.ToString("[yy-MM-dd hh-mm-ss] ") + "ERROR: " + message);
		}

		public static void WriteFile(string filePath)
		{
			using (StreamWriter file = new StreamWriter(filePath))
			{
				file.WriteLine(builder.ToString());
			}

			builder.Clear();
		}
    }
}