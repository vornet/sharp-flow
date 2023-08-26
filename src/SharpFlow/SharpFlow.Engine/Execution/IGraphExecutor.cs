using VorNet.SharpFlow.Engine.Execution;

namespace VorNet.SharpFlow.Engine.Executor
{
    public interface IGraphExecutor
    {
        Task<string> ExecuteAsync(Graph graph);
    }
}