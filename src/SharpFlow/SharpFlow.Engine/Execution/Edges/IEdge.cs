using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Execution.Edges
{
    public interface IEdge
    {
        IHandle FromHandle { get; }
        IHandle ToHandle { get; }
    }
}