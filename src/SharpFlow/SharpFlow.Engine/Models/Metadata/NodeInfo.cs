using System.Text.Json.Serialization;
using VorNet.SharpFlow.Engine.Models;

namespace VorNet.SharpFlow.Engine.Data.Models.Metadata
{
    public class NodeInfo
    {
        [JsonPropertyName("displayType")]
        public string DisplayType { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("handles")]
        public IEnumerable<Handle> Handles { get; set; }
    }
}
