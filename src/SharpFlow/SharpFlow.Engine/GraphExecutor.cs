using VorNet.SharpFlow.Engine.Models;
using VorNet.SharpFlow.Engine.Nodes;

namespace VorNet.SharpFlow.Engine
{
    public class GraphExecutor
    {
        public async Task ExecuteAsync(Graph graph)
        {
            INode currentNode = graph.StartNode;

            while (true)
            {

                // Connection from the current node.
                var connection = graph.Edges.FirstOrDefault(connection => connection.FromHandle == currentNode.ExecOut);
                // Get the node at the end of the connection.
                currentNode = graph.Nodes.FirstOrDefault(node => node.Handles.Contains(connection.ToHandle));

                if (currentNode.Id == "end")
                {
                    return;
                }

                foreach(var handle in currentNode.Handles)
                {
                    if (handle == currentNode.ExecIn || handle == currentNode.ExecOut) { continue;  }
                    var conn = graph.Edges.FirstOrDefault(c => c.ToHandle == handle);
                    if (conn == null) { continue;  }
                    var connectedNode = graph.Nodes.FirstOrDefault(node => node.Handles.Contains(conn.FromHandle));
                    await connectedNode.ExecuteAsync();
                    conn.ToHandle.Value = conn.FromHandle.Value;
                }

                await currentNode.ExecuteAsync();
            }
        }
    }
}
