using System.Text;
using VorNet.SharpFlow.Engine.Execution;
using VorNet.SharpFlow.Engine.Execution.Nodes;

namespace VorNet.SharpFlow.Engine.Executor
{
    public class GraphExecutor : IGraphExecutor
    {
        private readonly IBufferedLogger _bufferedLogger;

        public GraphExecutor(IBufferedLogger bufferedLogger)
        {
            _bufferedLogger = bufferedLogger ?? throw new ArgumentNullException(nameof(bufferedLogger));
        }

        public async Task<string> ExecuteAsync(Graph graph)
        {
            List<INode> executedNodes = new List<INode>();

            _bufferedLogger.Log($"Executing graph {graph.Name}");

            INode currentNode = graph.StartNode;

            while (true)
            {
                // Connection from the current node.
                var connection = graph.Edges.FirstOrDefault(connection => connection.FromHandle == currentNode.ExecOut);
                // Get the node at the end of the connection.
                currentNode = graph.Nodes.FirstOrDefault(node => node.Handles.Contains(connection.ToHandle));

                if (currentNode.Id == "end")
                {
                    break;
                }

                foreach (var handle in currentNode.Handles.Where(handle => handle.Direction == Handles.IHandle.HandleDireciton.Target))
                {
                    if (handle == currentNode.ExecIn) { continue; }
                    var conn = graph.Edges.FirstOrDefault(c => c.ToHandle == handle);
                    if (conn == null) { continue; }
                    var connectedNode = graph.Nodes.FirstOrDefault(node => node.Handles.Contains(conn.FromHandle));
                    if (!executedNodes.Contains(connectedNode))
                    {
                        _bufferedLogger.Log($"Executing node {connectedNode.Id}");
                        await connectedNode.ExecuteAsync();
                        executedNodes.Add(connectedNode);
                    }
                    conn.ToHandle.Value = conn.FromHandle.Value;
                }

                if (!executedNodes.Contains(currentNode))
                {
                    _bufferedLogger.Log($"Executing node {currentNode.Id}");                
                    await currentNode.ExecuteAsync();
                    executedNodes.Add(currentNode);
                }
            }

            _bufferedLogger.Log($"Finished graph {graph.Name}");

            return _bufferedLogger.GetBuffer();
        }
    }
}
