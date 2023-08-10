using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Nodes
{
    public class LiteralStringNode : NodeBase
    {
        private string _literalText;

        public IHandle LiteralText { get { return GetHandleById("literalText"); } }

        public LiteralStringNode(string id, string literalText)
            : base(id)
        {
            _literalText = literalText;
            AddHandle(new StringHandle("literalText", IHandle.HandleType.Output));
        }

        public override Task ExecuteAsync()
        {
            var outputHandle = GetHandleById("literalText");
            outputHandle.Value = _literalText;
            return Task.CompletedTask;
        }
    }
}
