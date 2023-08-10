using VorNet.SharpFlow.Engine;
using VorNet.SharpFlow.Playground.Nodes;

var graph = new Graph();

var startNode = new StartNode();
var endNode = new EndNode();
var literalStringNode = new LiteralStringNode("hello");
var searchGoogleNode = new SearchGoogleNode();

graph.AddNode(startNode);
graph.AddNode(endNode);
graph.AddNode(searchGoogleNode);
graph.AddNode(literalStringNode);

graph.AddConnection(new Connection(startNode.ExecOut, searchGoogleNode.ExecIn));
graph.AddConnection(new Connection(literalStringNode.LiteralText, searchGoogleNode.SearchText));
graph.AddConnection(new Connection(searchGoogleNode.ExecOut, endNode.ExecIn));

GraphExecutor graphExecutor = new GraphExecutor();

await graphExecutor.ExecuteAsync(graph);