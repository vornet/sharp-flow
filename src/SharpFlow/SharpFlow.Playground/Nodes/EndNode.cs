using VorNet.SharpFlow.Engine;
using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Playground.Nodes
{
    public class EndNode : NodeBase
    {
        public IHandle ExecIn { get { return GetOutputHandleById("execIn"); } }

        public EndNode()
        {
            AddHandle(new ExecHandle("execIn", IHandle.HandleType.Output));
        }
    }
}
