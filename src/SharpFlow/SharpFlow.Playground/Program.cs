using System.Reflection;
using VorNet.SharpFlow.Engine;
using VorNet.SharpFlow.Engine.Data;
using VorNet.SharpFlow.Engine.Nodes;

var graph = new VorNet.SharpFlow.Engine.Models.Graph();
graph.Name = "Testing";

var literalStringNode = new LiteralStringNode("search_text");
literalStringNode.Text = "hello";
var searchGoogleNode = new SearchGoogleNode("google_search");

graph.AddNode(searchGoogleNode);
graph.AddNode(literalStringNode);

graph.GetNodeById("start").X = -47;
graph.GetNodeById("start").Y = -240;
graph.GetNodeById("end").X = 462;
graph.GetNodeById("end").Y = -235;
graph.GetNodeById("google_search").X = 209.5;
graph.GetNodeById("google_search").Y = -242;
graph.GetNodeById("search_text").X = -67;
graph.GetNodeById("search_text").Y = -126;

graph.AddEdge(new VorNet.SharpFlow.Engine.Models.Edge(graph.StartNode.ExecOut, searchGoogleNode.ExecIn));
graph.AddEdge(new VorNet.SharpFlow.Engine.Models.Edge(literalStringNode.GetHandleById("literalText"), searchGoogleNode.GetHandleById("searchText")));
graph.AddEdge(new VorNet.SharpFlow.Engine.Models.Edge(searchGoogleNode.ExecOut, graph.EndNode.ExecIn));

GraphExecutor graphExecutor = new GraphExecutor();
GraphSerializer graphSerializer = new GraphSerializer();

await graphExecutor.ExecuteAsync(graph);


IGraphDataAccess dataAccess = new SqlLiteGraphDataAccess(new Microsoft.Data.Sqlite.SqliteConnection("Data Source=sharpflow_graphs.db"));


var result = graphSerializer.Serialize(graph);

long id = await dataAccess.SaveGraphAsync(result);

result = await dataAccess.GetGraphByIdAsync(id);

var graph2 = graphSerializer.Deserialize(result);

await graphExecutor.ExecuteAsync(graph2);