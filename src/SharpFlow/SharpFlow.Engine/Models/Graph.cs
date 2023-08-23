using VorNet.SharpFlow.Engine.Nodes;

namespace VorNet.SharpFlow.Engine.Models
{
    public class Graph
    {
        public List<INode> Nodes { get; } = new List<INode>();
        public List<Edge> Edges { get; } = new List<Edge>();

        public Graph()
        {
            AddNode(new StartNode());
            AddNode(new EndNode());
        }

        public void AddEdge(Edge edge)
        {
            Edges.Add(edge);
        }

        public void AddNode(INode node)
        {
            Nodes.Add(node);
        }

        public INode GetNodeById(string id)
        {
            return Nodes.FirstOrDefault(node => node.Id == id);
        }

        public string Name { get; set; }

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
