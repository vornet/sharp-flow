using VorNet.SharpFlow.Engine.Execution;
using VorNet.SharpFlow.Engine.Execution.Nodes;
using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Nodes
{
    public class LiteralIntNode : NodeBase
    {
        public LiteralIntNode(IBufferedLogger logger, string id)
            : base(logger, "literalInt", id)
        {
            ;
            AddHandle(new IntHandle("literalInt", IHandle.HandleDireciton.Source));
        }

        public override async Task ExecuteAsync()
        {
            GetHandleById("literalInt").Value = Number;
        }

        public int Number { get; set; }
    }
}
