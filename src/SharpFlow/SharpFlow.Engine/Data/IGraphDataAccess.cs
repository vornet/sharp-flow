using VorNet.SharpFlow.Engine.Data.Models;

namespace VorNet.SharpFlow.Engine.Data
{
    public interface IGraphDataAccess
    {
        Task<long> SaveGraphAsync(Graph graph);

        Task<Graph> GetGraphByIdAsync(long id);
    }
}
