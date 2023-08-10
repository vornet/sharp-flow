namespace VorNet.SharpFlow.Engine.Nodes
{
    public interface INode
    {
        public string Id { get; }
        List<IHandle> Handles { get; }

        Task ExecuteAsync();
    }
}
