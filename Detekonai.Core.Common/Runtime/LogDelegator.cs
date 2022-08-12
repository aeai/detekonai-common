using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Detekonai.Core.Common.Runtime
{
    public class LogDelegator : ILogger
    {
        public ILogger Connector { get; set; }

        public LogDelegator(ILogger innerLog)
        {
            Connector = innerLog;
        }
        public LogDelegator() : this(null)
        {
        }

        public void Log(object sender, string msg, ILogger.LogLevel level = ILogger.LogLevel.Verbose, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Connector?.Log(sender, msg, level, memberName, sourceFilePath, sourceLineNumber);
        }

        public void Log(object sender, string msg, Exception ex, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Connector?.Log(sender, msg, ex, memberName, sourceFilePath, sourceLineNumber);
        }
    }
}
