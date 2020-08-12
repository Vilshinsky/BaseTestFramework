using System;
using System.IO;
using System.Reflection;
using System.Xml;
using log4net;

namespace L1.Domain.Logs
{
	public static class Log
	{
		public static void Info(string message)
		{
			ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
			log.Info(message);
		}

		public static void Debug(string message)
		{
			ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
			log.Debug(message);
		}

		public static void Error(object message, Exception exception)
		{
			ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
			log.Error(message, exception);
		}

		public static ILog GetLogger()
		{
			XmlDocument log4netConfig = new XmlDocument();
			log4netConfig.Load(File.OpenRead("log4net.config"));
			var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(),
				typeof(log4net.Repository.Hierarchy.Hierarchy));
			log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

			return LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}
    }
}