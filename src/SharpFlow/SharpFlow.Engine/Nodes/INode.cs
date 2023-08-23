namespace VorNet.SharpFlow.Engine.Nodes
{
    public interface INode
    {
        public string Id { get; }

        public string Name { get; }

        List<IHandle> Handles { get; }

        public IHandle ExecIn { get; }

        public IHandle ExecOut { get; }

        public double X { get; set; }

        public double Y { get; set; }

        Task ExecuteAsync();
    }
}
