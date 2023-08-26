using System.Text.Json.Serialization;

namespace VorNet.SharpFlow.Engine.Data.Models.Metadata
{
    public class NodeInfo
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
