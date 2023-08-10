using System.Data;

namespace VorNet.SharpFlow.Engine
{
    public class Graph
    {
        public List<INode> _nodes = new List<INode>();
        public List<Connection> _connections = new List<Connection>();

        public void AddConnection(Connection connection)
        {
            _connections.Add(connection);
        }

        public void AddNode(INode node)
        {
            _nodes.Add(node);
        }
    }
}
