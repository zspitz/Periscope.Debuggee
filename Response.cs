using System;

namespace Periscope.Debuggee {
    [Serializable]
    public class Response {
        public ExceptionData? ExceptionData { get; set; }
        public object? Model { get; set;  }
    }
}
