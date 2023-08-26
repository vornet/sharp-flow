using System.Text.Json.Serialization;

namespace VorNet.SharpFlow.Engine.Models
{
    public class Node
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("position")]
        public NodePosition Position { get; set; }

        [JsonPropertyName("data")]
        public NodeData Data { get; set; }

        [JsonPropertyName("className")]
        public string? ClassName { get; set; }
    }
}
