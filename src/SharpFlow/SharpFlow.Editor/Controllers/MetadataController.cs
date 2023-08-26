using Microsoft.AspNetCore.Mvc;
using VorNet.SharpFlow.Engine.Data.Models.Metadata;

namespace VorNet.SharpFlow.Editor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetadataController : ControllerBase
    {
        [HttpGet("nodes")]
        public async Task<IEnumerable<NodeInfo>> GetNodeInfosAsync()
        {
            return new NodeInfo[] { new NodeInfo() };
        }
    }
}
