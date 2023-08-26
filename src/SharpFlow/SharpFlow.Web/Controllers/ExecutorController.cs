using Microsoft.AspNetCore.Mvc;
using VorNet.SharpFlow.Engine.Data;
using VorNet.SharpFlow.Engine.Executor;
using VorNet.SharpFlow.Engine.Serilaizer;

namespace VorNet.SharpFlow.Editor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExecutorController
    {
        private readonly IGraphDataAccess _graphDataAccess;
        private readonly IGraphSerializer _graphSerializer;
        private readonly IGraphExecutor _graphExecutor;

        public ExecutorController(IGraphDataAccess graphDataAccess, IGraphSerializer graphSerializer, IGraphExecutor graphExecutor)
        {
            _graphDataAccess = graphDataAccess ?? throw new ArgumentNullException(nameof(graphDataAccess));
            _graphSerializer = graphSerializer ?? throw new ArgumentNullException(nameof(graphSerializer));
            _graphExecutor = graphExecutor ?? throw new ArgumentNullException(nameof(graphExecutor));
        }

        [HttpPost("execute")]
        public async Task<string> ExecuteGraphByNameAsync([FromQuery] string name)
        {
            var result = await _graphDataAccess.GetGraphByNameAsync(name);
            var graph = _graphSerializer.Deserialize(result);
            return await _graphExecutor.ExecuteAsync(graph);
        }
    }
}
