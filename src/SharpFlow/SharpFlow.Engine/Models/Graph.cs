using System.Text.Json.Serialization;

namespace VorNet.SharpFlow.Engine.Models
{
    public class Graph
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nodes")]
        public IEnumerable<Node> Nodes { get; set; }

        [JsonPropertyName("edges")]
        public IEnumerable<Edge> Edges { get; set; }
    }
}
