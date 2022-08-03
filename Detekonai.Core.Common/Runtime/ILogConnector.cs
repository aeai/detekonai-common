using System;
using System.Runtime.CompilerServices;
namespace Detekonai.Core.Common
{
	public interface ILogConnector
	{
		public enum LogLevel
		{
			Verbose,
			Info,
			Warning,
			Error,
		}

		public void Log(object sender, string msg,  LogLevel level = LogLevel.Verbose, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
		public void Log(object sender, string msg, Exception ex, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
	}

}
