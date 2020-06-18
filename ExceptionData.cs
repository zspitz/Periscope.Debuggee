using System;

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

        public override string ToString() =>
            $@"Log message: {LogMessage}
Typename: {TypeName}
Message: {Message}
InnerException: {InnerException?.TypeName}

Stack trace:
{StackTrace}";
    }
}