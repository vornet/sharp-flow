using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Execution.Nodes
{
    public abstract class NodeBase : INode
    {
        protected IBufferedLogger BufferedLogger { get; }

        public List<IHandle> Handles { get; } = new List<IHandle>();

        public IHandle ExecIn { get { return GetHandleById("execIn"); } }

        public IHandle ExecOut { get { return GetHandleById("execOut"); } }

        public string Id { get; }

        public double X { get; set; }

        public double Y { get; set; }

        public string DisplayType { get; set; }

        public bool AlreadyExecuted { get; private set; }

        public NodeBase(IBufferedLogger bufferedLogger, string displayType, string id)
        {
            BufferedLogger = bufferedLogger ?? throw new ArgumentNullException(nameof(bufferedLogger));
            DisplayType = displayType;
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
