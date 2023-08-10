using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Nodes
{
    public class StartNode : NodeBase
    {
        public IHandle ExecOut { get { return GetHandleById("execOut"); } }

        public StartNode()
            : base("start")
        {
            AddHandle(new ExecHandle("execOut", IHandle.HandleType.Output));
        }

        public override Task ExecuteAsync()
        {
            return Task.CompletedTask;
        }
    }
}
