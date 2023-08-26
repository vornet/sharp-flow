using VorNet.SharpFlow.Engine.Models;

namespace VorNet.SharpFlow.Engine.Data
{
    public interface IGraphDataAccess
    {
        Task SaveGraphAsync(Graph graph);

        Task<Graph> GetGraphByNameAsync(string name);
    }
}
