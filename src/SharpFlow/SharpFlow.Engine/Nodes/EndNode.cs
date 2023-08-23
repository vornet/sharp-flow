using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Nodes
{
    public class EndNode : NodeBase
    {
        public IHandle ExecIn { get { return GetHandleById("execIn"); } }

        public EndNode()
            : base("end")
        {
            AddHandle(new ExecHandle("execIn", IHandle.HandleType.Target));
        }

        public override Task ExecuteAsync()
        {
            return Task.CompletedTask;
        }
    }
}
