using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;
using VorNet.SharpFlow.Engine.Execution;
using VorNet.SharpFlow.Engine.Execution.Nodes;

namespace VorNet.SharpFlow.Engine.Serilaizer
{
    public class GraphSerializer : IGraphSerializer
    {
        private readonly IBufferedLogger _logger;
        public GraphSerializer(IBufferedLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Models.Graph Serialize(Graph graph)
        {
            Models.Graph result = new Models.Graph();
            result.Name = graph.Name;

            result.Nodes = graph.Nodes.Select(node =>
            {
                return new Models.Node
                {
                    Id = node.Id,
                    Type = "sharpflow",
                    Data = new Models.NodeData
                    {
                        DisplayType = node.DisplayType,
                        Type = node.GetType().FullName,
                        Handles = node.Handles.Select(handle => new Models.Handle { Id = handle.Id, DisplayType = handle.DisplayType, Type = handle.GetType().FullName, Direction = handle.Direction.ToString().ToLower() }),
                        State = node.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.DeclaredOnly).ToDictionary(p => p.Name, p => p.GetValue(node))
                    },
                    Position = new Models.NodePosition { X = node.X, Y = node.Y },
                    ClassName = node.Id == "start" || node.Id == "end" ? "circle" : null,
                };
            });

            result.Edges = graph.Edges.Select(conn =>
            {
                return new Models.Edge
                {
                    Id = new Random().Next(10000).ToString(),
                    Source = graph.Nodes.FirstOrDefault(node => node.Handles.Contains(conn.FromHandle)).Id,
                    SourceHandle = conn.FromHandle.Id,
                    Target = graph.Nodes.FirstOrDefault(node => node.Handles.Contains(conn.ToHandle)).Id,
                    TargetHandle = conn.ToHandle.Id,
                };
            });

            return result;
        }

        public Graph Deserialize(Models.Graph graph)
        {
            Graph result = new Graph(_logger);
            result.Name = graph.Name;

            foreach (Models.Node node in graph.Nodes)
            {
                Type type = GetTypeFromLoadedAssemblies(node.Data.Type);

                if (type == typeof(StartNode))
                {
                    result.StartNode.X = node.Position.X;
                    result.StartNode.Y = node.Position.Y;
                    continue;
                }
                else if (type == typeof(EndNode))
                {
                    result.EndNode.X = node.Position.X;
                    result.EndNode.Y = node.Position.Y;
                    continue;
                }

                INode instance = (INode)Activator.CreateInstance(type, _logger, node.Id);

                instance.X = node.Position.X;
                instance.Y = node.Position.Y;

                if (node?.Data?.State != null)
                {
                    foreach (string key in node.Data.State.Keys)
                    {
                        var p = instance.GetType().GetProperty(key);
                        var valueJsonElement = (JsonElement)node.Data.State[key];
                        if (p.PropertyType == typeof(string))
                        {
                            p.SetValue(instance, valueJsonElement.GetString());
                        }

                        if (p.PropertyType == typeof(double))
                        {
                            p.SetValue(instance, double.Parse(valueJsonElement.GetString()));
                        }

                        if (p.PropertyType == typeof(int))
                        {
                            p.SetValue(instance, int.Parse(valueJsonElement.GetString()));
                        }
                    }
                }

                result.AddNode(instance);
            }

            foreach (var edge in graph.Edges)
            {
                var sourceNode = result.GetNodeById(edge.Source);
                var targetNode = result.GetNodeById(edge.Target);

                result.AddEdge(new Execution.Edges.Edge(sourceNode.Handles.FirstOrDefault(h => h.Id == edge.SourceHandle), targetNode.Handles.FirstOrDefault(h => h.Id == edge.TargetHandle)));
            }

            //var literalStringNode = new LiteralStringNode("search_text", "hello");
            //var searchGoogleNode = new SearchGoogleNode("google_search");

            //graph.AddNode(searchGoogleNode);
            //graph.AddNode(literalStringNode);

            //graph.GetNodeById("start").X = -47;
            //graph.GetNodeById("start").Y = -240;
            //graph.GetNodeById("end").X = 462;
            //graph.GetNodeById("end").Y = -235;
            //graph.GetNodeById("google_search").X = 209.5;
            //graph.GetNodeById("google_search").Y = -242;
            //graph.GetNodeById("search_text").X = -67;
            //graph.GetNodeById("search_text").Y = -126;

            //graph.AddEdge(new VorNet.SharpFlow.Engine.Models.Edge(graph.StartNode.ExecOut, searchGoogleNode.ExecIn));
            //graph.AddEdge(new VorNet.SharpFlow.Engine.Models.Edge(literalStringNode.LiteralText, searchGoogleNode.SearchText));
            //graph.AddEdge(new VorNet.SharpFlow.Engine.Models.Edge(searchGoogleNode.ExecOut, graph.EndNode.ExecIn));

            return result;
        }

        private Type GetTypeFromLoadedAssemblies(string typeToLoad)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type type = assembly.GetType(typeToLoad);

                if (type != null) { return type; }
            }

            return null;
        }
    }
}
