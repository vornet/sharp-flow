using VorNet.SharpFlow.Engine.Handles;
using VorNet.SharpFlow.Engine;

namespace VorNet.SharpFlow.Playground.Nodes
{
    public class LiteralStringNode : NodeBase
    {
        private string _literalText;

        public IHandle LiteralText { get { return GetOutputHandleById("literalText"); } }

        public LiteralStringNode(string literalText)
        {
            _literalText = literalText;
            AddHandle(new StringHandle("literalText", IHandle.HandleType.Output));
        }

        public async Task ExecuteAsync()
        {
            var outputHandle = GetOutputHandleById("literalText");
            outputHandle.Value = _literalText;
        }
    }
}
