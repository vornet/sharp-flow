using System.Text.Json.Serialization;

namespace VorNet.SharpFlow.Engine.Models
{
    public class NodeData
    {
        [JsonPropertyName("displayType")]
        public string DisplayType { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("handles")]
        public IEnumerable<Handle> Handles { get; set; }

        [JsonPropertyName("state")]
        public Dictionary<string, object>? State { get; set; }
    }
}
