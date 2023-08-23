namespace VorNet.SharpFlow.Engine.Models
{
    public class Edge
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
