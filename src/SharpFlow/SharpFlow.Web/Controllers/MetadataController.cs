using Microsoft.AspNetCore.Mvc;
using VorNet.SharpFlow.Engine.Data.Models.Metadata;
using VorNet.SharpFlow.Engine.Serilaizer;

namespace VorNet.SharpFlow.Editor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetadataController : ControllerBase
    {
        private readonly IMetadataGenerator _metadataGenerator;

        public MetadataController(IMetadataGenerator metadataGenerator)
        {
            _metadataGenerator = metadataGenerator ?? throw new ArgumentNullException(nameof(metadataGenerator));
        }

        public async Task<Metadata> GetNodeInfosAsync()
        {
            return _metadataGenerator.Generate();
        }
    }
}
