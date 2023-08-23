﻿using System.Text.Json.Serialization;

namespace VorNet.SharpFlow.Engine.Data.Models
{
    public class NodePosition
    {
        [JsonPropertyName("x")]
        public double X { get; set; }

        [JsonPropertyName("y")]
        public double Y { get; set; }
    }
}
