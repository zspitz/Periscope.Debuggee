using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.IO;

namespace Periscope.Debuggee {
    public abstract class VisualizerObjectSourceBase<TTarget, TConfig> : VisualizerObjectSource  where TConfig : ConfigBase<TConfig> {
        public override void GetData(object target, Stream outgoingData) => Serialize(outgoingData, "");

        public override void TransferData(object target, Stream incomingData, Stream outgoingData) {
            var config = (TConfig)Deserialize(incomingData);
            var response = new Response();
            try {
                response.Model = GetResponseModel((TTarget)target, config);
            } catch (Exception ex) {
                response.ExceptionData = new ExceptionData(ex);
            }
            Serialize(outgoingData, response);
        }

        public abstract object GetResponseModel(TTarget target, TConfig config);
    }
}
