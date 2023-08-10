namespace VorNet.SharpFlow.Engine.Handles
{
    public class HandleBase : IHandle
    {
        public HandleBase(string id, IHandle.HandleType type)
        {
            Id = id;
            Type = type;
        }

        public string Id { get; private set; }

        public object Value { get; set; }

        public IHandle.HandleType Type { get; private set; }
    }
}
