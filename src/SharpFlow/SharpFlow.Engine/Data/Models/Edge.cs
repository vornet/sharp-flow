using System.Text.Json.Serialization;

namespace VorNet.SharpFlow.Engine.Data.Models
{
    public class Edge
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("sourceHandle")]
        public string SourceHandle { get; set; }

        [JsonPropertyName("target")]
        public string Target { get; set; }

        [JsonPropertyName("targetHandle")]
        public string TargetHandle { get; set; }
    }
}
