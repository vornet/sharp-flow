namespace VorNet.SharpFlow.Engine.Nodes
{
    public abstract class NodeBase : INode
    {
        public List<IHandle> Handles { get; } = new List<IHandle>();

        public IHandle ExecIn { get { return GetHandleById("execIn"); } }

        public IHandle ExecOut { get { return GetHandleById("execOut"); } }

        public string Id { get; }

        public double X { get; set; }

        public double Y { get; set; }

        public string Name { get; set; }

        public NodeBase(string id)
        {
            Id = id;
        }

        protected void AddHandle(IHandle handle)
        {
            Handles.Add(handle);
        }

        public IHandle GetHandleById(string handleId)
        {
            return Handles.FirstOrDefault(h => h.Id == handleId);
        }        

        public abstract Task ExecuteAsync();
    }
}
