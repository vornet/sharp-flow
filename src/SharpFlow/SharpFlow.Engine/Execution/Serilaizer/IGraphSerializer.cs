using VorNet.SharpFlow.Engine.Execution;
using VorNet.SharpFlow.Engine.Models;

namespace VorNet.SharpFlow.Engine.Serilaizer
{
    public interface IGraphSerializer
    {
        Execution.Graph Deserialize(Models.Graph graph);

        Models.Graph Serialize(Execution.Graph graph);
    }
}