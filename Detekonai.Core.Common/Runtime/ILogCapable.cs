using System.Runtime.CompilerServices;
namespace Detekonai.Core.Common
{
	public interface ILogCapable
	{
		public enum LogLevel
		{
			Verbose,
			Info,
			Warning,
			Error,
		}

		public delegate void LogHandler(object sender, string msg,  LogLevel level = LogLevel.Verbose, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
		event LogHandler Logger;
	}

}
