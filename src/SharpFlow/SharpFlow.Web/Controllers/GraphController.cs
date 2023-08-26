using Microsoft.AspNetCore.Mvc;
using VorNet.SharpFlow.Engine.Data;
using VorNet.SharpFlow.Engine.Models;
using VorNet.SharpFlow.Engine.Nodes;
using VorNet.SharpFlow.Engine.Serilaizer;

namespace VorNet.SharpFlow.Editor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GraphController : ControllerBase
    {
        private readonly IGraphSerializer _graphSerializer;
        private readonly IGraphDataAccess _graphDataAccess;

        public GraphController(IGraphSerializer graphSerializer, IGraphDataAccess graphDataAccess)
        {
            _graphSerializer = graphSerializer ?? throw new ArgumentNullException(nameof(graphSerializer));
            _graphDataAccess = graphDataAccess ?? throw new ArgumentNullException(nameof(graphDataAccess));
        }

        [HttpGet("{name}")]
        public async Task<Graph> GetByNameAsync(string name)
        {
            var graph = new Engine.Execution.Graph();

            var literalStringNode = new LiteralStringNode("searchText");
            literalStringNode.Text = "hello";
            var searchGoogleNode = new SearchGoogleNode("googleSearch");

            graph.AddNode(searchGoogleNode);
            graph.AddNode(literalStringNode);

            graph.GetNodeById("start").X = -47;
            graph.GetNodeById("start").Y = -240;
            graph.GetNodeById("end").X = 462;
            graph.GetNodeById("end").Y = -235;
            graph.GetNodeById("googleSearch").X = 209.5;
            graph.GetNodeById("googleSearch").Y = -242;
            graph.GetNodeById("searchText").X = -67;
            graph.GetNodeById("searchText").Y = -126;

            graph.AddEdge(new Engine.Execution.Edges.Edge(graph.StartNode.ExecOut, searchGoogleNode.ExecIn));
            graph.AddEdge(new Engine.Execution.Edges.Edge(literalStringNode.GetHandleById("literalText"), searchGoogleNode.GetHandleById("searchText")));
            graph.AddEdge(new Engine.Execution.Edges.Edge(searchGoogleNode.ExecOut, graph.EndNode.ExecIn));

            return _graphSerializer.Serialize(graph);
        }

        [HttpPost]
        public async Task UpdateGraphAsync(Graph graph)
        {
            await _graphDataAccess.SaveGraphAsync(graph);
        }
    }
}
