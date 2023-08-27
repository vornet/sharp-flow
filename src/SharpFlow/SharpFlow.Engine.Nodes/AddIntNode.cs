using VorNet.SharpFlow.Engine.Execution;
using VorNet.SharpFlow.Engine.Execution.Nodes;
using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Nodes
{
    public class AddIntNode : NodeBase
    {
        public AddIntNode(IBufferedLogger logger, string id)
            : base(logger, "addInt", id)
        {
            AddHandle(new IntHandle("a", IHandle.HandleDireciton.Source));
            AddHandle(new IntHandle("b", IHandle.HandleDireciton.Source));
            AddHandle(new IntHandle("c", IHandle.HandleDireciton.Target));
        }

        public override async Task ExecuteAsync()
        {
            int a = (int)GetHandleById("a").Value;
            int b = (int)GetHandleById("b").Value;

            int c = a + b;

            GetHandleById("c").Value = c;
        }
    }
}
