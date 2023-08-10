using VorNet.SharpFlow.Engine;
using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Playground.Nodes
{
    public class StartNode : NodeBase
    {
        public IHandle ExecOut { get { return GetOutputHandleById("execOut"); } }

        public StartNode()
        {
            AddHandle(new ExecHandle("execOut", IHandle.HandleType.Output));
        }
    }
}
