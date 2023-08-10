namespace VorNet.SharpFlow.Engine
{
    public interface INode
    {
        IEnumerable<IHandle> Handles { get; }
    }
}
