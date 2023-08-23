using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Nodes
{
    public class LiteralStringNode : NodeBase
    {
        public LiteralStringNode(string id)
            : base(id)
        {;
            AddHandle(new StringHandle("literalText", IHandle.HandleType.Source));
        }

        public override Task ExecuteAsync()
        {
            var outputHandle = GetHandleById("literalText");
            outputHandle.Value = Text;
            return Task.CompletedTask;
        }

        public string Text { get; set; }
    }
}
