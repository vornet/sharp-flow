using VorNet.SharpFlow.Engine.Data.Models.Metadata;

namespace VorNet.SharpFlow.Engine.Serilaizer
{
    public interface IMetadataGenerator
    {
        Metadata Generate();
    }
}