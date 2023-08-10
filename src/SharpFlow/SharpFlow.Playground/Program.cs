using VorNet.SharpFlow.Engine;
using VorNet.SharpFlow.Engine.Nodes;
using VorNet.SharpFlow.Playground.Nodes;

var graph = new Graph();


var literalStringNode = new LiteralStringNode("search_text", "hello");
var searchGoogleNode = new SearchGoogleNode("google_search");


graph.AddNode(searchGoogleNode);
graph.AddNode(literalStringNode);

graph.AddConnection(new Connection(graph.StartNode.ExecOut, searchGoogleNode.ExecIn));
graph.AddConnection(new Connection(literalStringNode.LiteralText, searchGoogleNode.SearchText));
graph.AddConnection(new Connection(searchGoogleNode.ExecOut, graph.EndNode.ExecIn));

GraphExecutor graphExecutor = new GraphExecutor();

await graphExecutor.ExecuteAsync(graph);