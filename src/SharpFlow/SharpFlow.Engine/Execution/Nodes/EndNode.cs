using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Execution.Nodes
{
    public class EndNode : NodeBase
    {
        public IHandle ExecIn { get { return GetHandleById("execIn"); } }

        public EndNode(IBufferedLogger logger)
            : base(logger, "", "end")
        {
            AddHandle(new ExecHandle("execIn", IHandle.HandleDireciton.Target));
        }

        public override Task ExecuteAsync()
        {
            return Task.CompletedTask;
        }
    }
}
