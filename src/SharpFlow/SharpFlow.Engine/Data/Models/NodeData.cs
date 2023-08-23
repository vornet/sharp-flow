using System.Text.Json.Serialization;

namespace VorNet.SharpFlow.Engine.Data.Models
{
    public class NodeData
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("handles")]
        public IEnumerable<Handle> Handles { get; set; }

        public Dictionary<string, object> State { get; set; }
    }
}
