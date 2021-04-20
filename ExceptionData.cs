using System;
using System.Linq;

namespace Periscope.Debuggee {
    [Serializable]
    public class ExceptionData {
        public ExceptionData(Exception ex, string? logMessage = null) {
            TypeName = ex.GetType().Name;
            Message = ex.Message;
            StackTrace = ex.StackTrace;
            if (ex.InnerException is { }) {
                InnerException = new ExceptionData(ex.InnerException);
            }
            LogMessage = logMessage;
        }

        public string TypeName { get; }
        public string Message { get; }
        public string StackTrace { get; }
        public ExceptionData? InnerException { get; }
        public string? LogMessage { get; }

        public override string ToString() {
            var parts = new[] {
                ("Log message",LogMessage),
                ("TypeName", TypeName),
                ("Message", Message),
                ("Inner exception", InnerException?.TypeName)
            }
            .Where(x => !string.IsNullOrWhiteSpace(x.Item2))
            .Select(x => $"{x.Item1}: {x.Item2}");
            var ret = string.Join("\n", parts);
            if (!string.IsNullOrWhiteSpace(StackTrace)) {
                ret += $@"
Stack trace:
{StackTrace}
";
            }

            return ret;
        }
    }
}