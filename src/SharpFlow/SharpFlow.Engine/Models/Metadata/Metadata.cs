using System.Text.Json.Serialization;

namespace VorNet.SharpFlow.Engine.Data.Models.Metadata
{
    public class Metadata
    {
        [JsonPropertyName("nodes")]
        public IEnumerable<NodeInfo> Nodes { get; set; }
    }
}
