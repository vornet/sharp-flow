using System.Text.Json.Serialization;

namespace VorNet.SharpFlow.Engine.Models
{
    public class Handle
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("displayType")]
        public string DisplayType { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("direction")]
        public string Direction { get; set; }
    }
}
