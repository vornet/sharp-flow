using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Execution.Nodes
{
    public interface INode
    {
        public string Id { get; }

        public string DisplayType { get; }

        List<IHandle> Handles { get; }

        public IHandle ExecIn { get; }

        public IHandle ExecOut { get; }

        public double X { get; set; }

        public double Y { get; set; }

        Task ExecuteAsync();
    }
}
