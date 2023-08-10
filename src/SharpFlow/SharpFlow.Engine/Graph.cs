using VorNet.SharpFlow.Engine.Nodes;

namespace VorNet.SharpFlow.Engine
{
    public class Graph
    {
        public List<INode> Nodes { get; } = new List<INode>();
        public List<Connection> Connections { get; } = new List<Connection>();

        public Graph()
        {
            AddNode(new StartNode());
            AddNode(new EndNode());
        }

        public void AddConnection(Connection connection)
        {
            Connections.Add(connection);
        }

        public void AddNode(INode node)
        {
            Nodes.Add(node);
        }

        public StartNode StartNode
        {
            get
            { 
                return (StartNode)Nodes.FirstOrDefault(node => node is StartNode);
            }
        }

        public EndNode EndNode
        {
            get
            {
                return (EndNode)Nodes.FirstOrDefault(node => node is EndNode);
            }
        }
    }
}
