using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.IO;

namespace Periscope.Debuggee {
    public abstract class VisualizerObjectSourceBase<TTarget, TConfig> : VisualizerObjectSource  where TConfig : ConfigBase<TConfig> {
        public override void GetData(object target, Stream outgoingData) => Serialize(outgoingData, GetConfigKey());

        public override void TransferData(object target, Stream incomingData, Stream outgoingData) {
            var config = (TConfig)Deserialize(incomingData);
            var model = GetResponse((TTarget)target, config);
            Serialize(outgoingData, model);
        }

        public virtual string GetConfigKey() => "";
        public abstract object GetResponse(TTarget target, TConfig config);
    }
}
