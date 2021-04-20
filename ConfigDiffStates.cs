﻿namespace Periscope.Debuggee {
    // It would be natural to use a flag attribute for this
    // but I had problems in passing the default value across serialization boundaries
    public enum ConfigDiffStates {
        NoAction,
        NeedsWrite,
        NeedsTransfer
    }
}
