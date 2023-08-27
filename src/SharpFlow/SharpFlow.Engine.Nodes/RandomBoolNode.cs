using VorNet.SharpFlow.Engine.Execution;
using VorNet.SharpFlow.Engine.Execution.Nodes;
using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Nodes
{
    public class RandomBoolNode : NodeBase
    {
        private readonly Random _random = new Random();
        public RandomBoolNode(IBufferedLogger logger, string id)
            : base(logger, "randomBool", id)
        {
            ;
            AddHandle(new BoolHandle("randomBool", IHandle.HandleDireciton.Source));
        }

        public override Task ExecuteAsync()
        {
            GetHandleById("randomBool").Value = _random.Next(0, 2) == 0;
            return Task.CompletedTask;
        }
    }
}
