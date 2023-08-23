namespace VorNet.SharpFlow.Engine
{
    public interface IHandle
    {
        public enum HandleType
        {
            Target,
            Source
        }

        string Id { get; }

        HandleType Type { get; }

        object Value { get; set;  }
    }
}
