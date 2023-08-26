using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;
using VorNet.SharpFlow.Engine.Data.Models;
using VorNet.SharpFlow.Engine.Nodes;

namespace VorNet.SharpFlow.Engine
{
    public class GraphSerializer
    {
        public Data.Models.Graph Serialize(Models.Graph graph)
        {
            Data.Models.Graph result = new Data.Models.Graph();
            result.Name = graph.Name;

            result.Nodes = graph.Nodes.Select(node =>
            {
                return new Data.Models.Node
                {
                    Id = node.Id,
                    Type = "sharpflow",
                    Data = new Data.Models.NodeData
                    {
                        Label = node.Id,
                        Type = node.GetType().FullName,
                        Handles = node.Handles.Select(handle => new Data.Models.Handle { Id = handle.Id, Type = handle.Type.ToString().ToLower() }),
                        State = node.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.DeclaredOnly).ToDictionary(p => p.Name, p => p.GetValue(node))
                    },
                    Position = new Data.Models.NodePosition { X = node.X, Y = node.Y },
                    ClassName = node.Id == "start" || node.Id == "end" ? "circle" : null,
                };
            });

            result.Edges = graph.Edges.Select(conn =>
            {
                return new Data.Models.Edge
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

        public Models.Graph Deserialize(Data.Models.Graph graph)
        {
            Models.Graph result = new Models.Graph();
            result.Name = graph.Name;

            foreach(Node node in graph.Nodes)
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

                INode instance = (INode)Activator.CreateInstance(type, node.Id);

                instance.X = node.Position.X;
                instance.Y = node.Position.Y;

                foreach(string key in node.Data.State.Keys)
                {
                    var p = instance.GetType().GetProperty(key);
                    var valueJsonElement = (JsonElement)node.Data.State[key];
                    if (valueJsonElement.ValueKind == JsonValueKind.String)
                    {
                        p.SetValue(instance, valueJsonElement.GetString());
                    }
                    
                }

                result.AddNode(instance);
            }

            foreach(var edge in graph.Edges)
            {
                var sourceNode = result.GetNodeById(edge.Source);
                var targetNode = result.GetNodeById(edge.Target);

                result.AddEdge(new Models.Edge(sourceNode.Handles.FirstOrDefault(h => h.Id == edge.SourceHandle), targetNode.Handles.FirstOrDefault(h => h.Id == edge.TargetHandle)));
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
