using System.Text.Json.Serialization;

namespace VorNet.SharpFlow.Engine.Data.Models
{
    public class Handle
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
