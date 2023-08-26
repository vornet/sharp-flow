using VorNet.SharpFlow.Engine.Execution;
using VorNet.SharpFlow.Engine.Execution.Nodes;
using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Nodes
{
    public class LiteralStringNode : NodeBase
    {
        public LiteralStringNode(IBufferedLogger logger, string id)
            : base(logger, "literalString", id)
        {;
            AddHandle(new StringHandle("literalText", IHandle.HandleDireciton.Source));
        }

        public override async Task ExecuteAsync()
        {
            var outputHandle = GetHandleById("literalText");
            outputHandle.Value = Text;
        }

        public string Text { get; set; }
    }
}
