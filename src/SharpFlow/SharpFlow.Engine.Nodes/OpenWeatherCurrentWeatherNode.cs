using System.Text.Json;
using System.Text.Json.Serialization;
using VorNet.SharpFlow.Engine.Execution;
using VorNet.SharpFlow.Engine.Execution.Nodes;
using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Nodes
{
    internal class CurrentWeatherResponseBody
    {
        [JsonPropertyName("main")]
        public CurrentWeatherMainBody Main { get; set; }
    }

    internal class CurrentWeatherMainBody
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }
    }

    public class OpenWeatherCurrentWeatherNode : NodeBase
    {
        public OpenWeatherCurrentWeatherNode(IBufferedLogger logger, string id)
            : base(logger, "openWeatherCurrentWeather", id)
        {
            AddHandle(new ExecHandle("execIn", IHandle.HandleDireciton.Target));
            AddHandle(new DoubleHandle("lat", IHandle.HandleDireciton.Target));
            AddHandle(new DoubleHandle("lon", IHandle.HandleDireciton.Target));
            AddHandle(new StringHandle("appid", IHandle.HandleDireciton.Target));


            AddHandle(new ExecHandle("execOut", IHandle.HandleDireciton.Source));
            AddHandle(new DoubleHandle("temp", IHandle.HandleDireciton.Source));
        }

        public override async Task ExecuteAsync()
        {
            var latHandle = GetHandleById("lat");
            double lat = (double)latHandle.Value;

            var lonHandle = GetHandleById("lon");
            double lon = (double)lonHandle.Value;

            var appidHandle = GetHandleById("appid");
            string appid = (string)appidHandle.Value;

            using HttpClient client = new();
            var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={appid}&units=imperial");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            CurrentWeatherResponseBody weatherResponseBody = JsonSerializer.Deserialize<CurrentWeatherResponseBody>(responseBody);

            var tempHandle = GetHandleById("temp");
            tempHandle.Value = weatherResponseBody.Main.Temp;

            BufferedLogger.Log($"Getting the weather for ({lat}, {lon}) and found {weatherResponseBody.Main.Temp}.");
        }
    }
}
