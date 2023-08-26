using System.Text;
using VorNet.SharpFlow.Engine.Execution;
using VorNet.SharpFlow.Engine.Execution.Nodes;

namespace VorNet.SharpFlow.Engine.Executor
{
    public class GraphExecutor : IGraphExecutor
    {
        public async Task<string> ExecuteAsync(Graph graph)
        {
            StringBuilder outputBuffer = new StringBuilder();

            outputBuffer.AppendLine($"Executing graph {graph.Name}");

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

                foreach (var handle in currentNode.Handles)
                {
                    if (handle == currentNode.ExecIn || handle == currentNode.ExecOut) { continue; }
                    var conn = graph.Edges.FirstOrDefault(c => c.ToHandle == handle);
                    if (conn == null) { continue; }
                    var connectedNode = graph.Nodes.FirstOrDefault(node => node.Handles.Contains(conn.FromHandle));
                    await connectedNode.ExecuteAsync();
                    conn.ToHandle.Value = conn.FromHandle.Value;
                }

                outputBuffer.AppendLine($"Executing node {currentNode.Id}");
                await currentNode.ExecuteAsync();
            }

            outputBuffer.AppendLine($"Finished graph {graph.Name}");

            return outputBuffer.ToString();
        }
    }
}
