using VorNet.SharpFlow.Engine.Execution.Edges;
using VorNet.SharpFlow.Engine.Execution.Nodes;

namespace VorNet.SharpFlow.Engine.Execution
{
    public class Graph
    {
        public List<INode> Nodes { get; } = new List<INode>();
        public List<IEdge> Edges { get; } = new List<IEdge>();

        public Graph(IBufferedLogger logger)
        {
            AddNode(new StartNode(logger));
            AddNode(new EndNode(logger));
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
