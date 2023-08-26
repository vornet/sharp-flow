namespace VorNet.SharpFlow.Engine.Handles
{
    public class HandleBase : IHandle
    {
        public HandleBase(string displayType, string id, IHandle.HandleDireciton direction)
        {
            DisplayType = displayType;
            Id = id;
            Direction = direction;
        }

        public string DisplayType { get; private set; }

        public string Id { get; private set; }

        public object Value { get; set; }

        public IHandle.HandleDireciton Direction { get; private set; }
    }
}
