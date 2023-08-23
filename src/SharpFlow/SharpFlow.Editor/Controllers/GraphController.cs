using Microsoft.AspNetCore.Mvc;
using VorNet.SharpFlow.Engine;
using VorNet.SharpFlow.Engine.Data.Models;
using VorNet.SharpFlow.Engine.Nodes;

namespace VorNet.SharpFlow.Editor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GraphController : ControllerBase
    {
        /*
         const initialNodes = [
            { id: '1', position: { x: 0, y: 0 }, data: { label: 'Turn off the TV' } },
            { id: '2', position: { x: 0, y: 100 }, data: { label: 'Turn on the lights' } },
            ];
        
        const initialEdges = [{ id: 'e1-2', source: '1', target: '2' }]; */


        [HttpGet("{id}")]
        public async Task<Graph> GetByIdAsync(string id)
        {
            var graph = new Engine.Models.Graph();

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

            graph.AddEdge(new Engine.Models.Edge(graph.StartNode.ExecOut, searchGoogleNode.ExecIn));
            graph.AddEdge(new Engine.Models.Edge(literalStringNode.GetHandleById("literalText"), searchGoogleNode.GetHandleById("searchText")));
            graph.AddEdge(new Engine.Models.Edge(searchGoogleNode.ExecOut, graph.EndNode.ExecIn));

            return new GraphSerializer().Serialize(graph);
        }

        [HttpPut("{id}")]
        public async Task<Graph> UpdateGraphAsync(string id)
        {
            return null;
        }
    }
}
