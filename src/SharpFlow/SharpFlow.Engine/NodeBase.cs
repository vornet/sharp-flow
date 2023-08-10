namespace VorNet.SharpFlow.Engine
{
    public class NodeBase : INode
    {
        private List<IHandle> _handles;
        public IEnumerable<IHandle> Handles { get => _handles; }

        protected void AddHandle(IHandle handle)
        {
            _handles.Add(handle);
        }

        protected IHandle GetOutputHandleById(string handleId)
        {
            return _handles.FirstOrDefault(h => h.Type == IHandle.HandleType.Output && h.Id == handleId);
        }

        protected IHandle GetInputHandleById(string handleId)
        {
            return _handles.FirstOrDefault(h => h.Type == IHandle.HandleType.Input && h.Id == handleId);
        }
    }
}
