using VorNet.SharpFlow.Engine.Execution;
using VorNet.SharpFlow.Engine.Execution.Nodes;
using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Nodes
{
    public class LiteralDoubleNode : NodeBase
    {
        public LiteralDoubleNode(IBufferedLogger logger, string id)
            : base(logger, "literalDouble", id)
        {
            ;
            AddHandle(new DoubleHandle("literalDouble", IHandle.HandleDireciton.Source));
        }

        public override async Task ExecuteAsync()
        {
            GetHandleById("literalDouble").Value = Number;
        }

        public double Number { get; set; }
    }
}
