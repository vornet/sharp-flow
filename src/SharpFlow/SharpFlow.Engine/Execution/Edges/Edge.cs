using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Execution.Edges
{
    public class Edge : IEdge
    {
        public IHandle FromHandle { get; }
        public IHandle ToHandle { get; }

        public Edge(IHandle fromHandle, IHandle toHandle)
        {
            FromHandle = fromHandle;
            ToHandle = toHandle;
        }
    }
}
