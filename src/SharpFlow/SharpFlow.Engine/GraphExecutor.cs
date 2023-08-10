namespace VorNet.SharpFlow.Engine
{
    public class GraphExecutor
    {
        public async Task ExecuteAsync(Graph graph)
        {
            // Connection from the start node.
            var connection = graph.Connections.FirstOrDefault(connection => connection.FromHandle == graph.StartNode.ExecOut);
            // Get the node at the end of the connection.
            var node = graph.Nodes.FirstOrDefault(node => node.Handles.Contains(connection.ToHandle));
            await node.ExecuteAsync();
            connection = graph.Connections.FirstOrDefault(connection => node.Handles.Contains(connection.FromHandle));

        }
    }
}
