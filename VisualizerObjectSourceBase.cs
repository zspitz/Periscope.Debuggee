using Microsoft.VisualStudio.DebuggerVisualizers;
using System.IO;

namespace Periscope.Debuggee {
    public abstract class VisualizerObjectSourceBase<TTarget, TConfig> : VisualizerObjectSource  where TConfig : ConfigBase<TConfig> {
        public override void GetData(object target, Stream outgoingData) => Serialize(outgoingData, "");

        public override void TransferData(object target, Stream incomingData, Stream outgoingData) {
            var config = (TConfig)Deserialize(incomingData);
            var response = GenerateResponse((TTarget)target, config);
            Serialize(outgoingData, response);
        }

        public abstract object GenerateResponse(TTarget target, TConfig config);
    }
}
