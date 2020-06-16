using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periscope.Debuggee {
    [Serializable]
    public class ExceptionData {
        public ExceptionData(Exception ex) {
            TypeName = ex.GetType().Name;
            Message = ex.Message;
            StackTrace = ex.StackTrace;
            if (ex.InnerException is { }) {
                InnerException = new ExceptionData(ex.InnerException);
            }
        }

        public string TypeName { get; }
        public string Message { get; }
        public string StackTrace { get; }
        public ExceptionData? InnerException { get; }

        public override string ToString() =>
            $@"Typename: {TypeName}
Message: {Message}
InnerException: {InnerException?.TypeName}

Stack trace:
{StackTrace}";
    }
}
