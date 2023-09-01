using VorNet.SharpFlow.Engine.Execution;
using VorNet.SharpFlow.Engine.Execution.Nodes;
using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Nodes
{
    public class SecretStringNode : NodeBase
    {
        public SecretStringNode(IBufferedLogger logger, string id)
            : base(logger, "secretString", id)
        {
            AddHandle(new StringHandle("secretText", IHandle.HandleDireciton.Source));
        }

        public override async Task ExecuteAsync()
        {
            var outputHandle = GetHandleById("secretText");
            outputHandle.Value = Secret;
        }

        public string Secret { get; set; }
    }
}
