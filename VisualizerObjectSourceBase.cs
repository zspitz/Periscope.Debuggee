using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.IO;

namespace Periscope.Debuggee {
    public abstract class VisualizerObjectSourceBase<TTarget, TConfig> : VisualizerObjectSource  where TConfig : ConfigBase<TConfig> {
        private object? value;

        public override void GetData(object target, Stream outgoingData) {
            // Fix for https://github.com/zspitz/Periscope/issues/24
            if (target is ValueType) { value = target; }
            Serialize(outgoingData, GetConfigKey(target));
        }

        public override void TransferData(object? target, Stream incomingData, Stream outgoingData) {
            void logException(string logMessage, Exception ex) => Serialize(outgoingData, new ExceptionData(ex, logMessage));

            // Fix for https://github.com/zspitz/Periscope/issues/24
            target ??= value;

            TConfig? config;
            try {
                config = (TConfig)Deserialize(incomingData);
            } catch (Exception ex) {
                logException("Deserialize incoming config", ex);
                return;
            }

            TTarget t;
            if (target is TTarget t1) {
                t = t1;
            } else {
                var message =
                    target is null ?
                        "Target is null." :
                        $"Target is of type {target.GetType()}; expected {typeof(TTarget)}.";
                logException("Mismatched target type", new Exception(message));
                return;
            }

            object? model;
            try {
                model = GetResponse(t, config!);
            } catch (Exception ex) {
                logException("Get response", ex);
                return;
            }

            try {
                Serialize(outgoingData, model);
            } catch (Exception ex) {
                logException("Serialize outgoing", ex);
                return;
            }
        }

        public virtual string GetConfigKey(object target) => "";
        public abstract object GetResponse(TTarget target, TConfig config);
    }
}
