namespace VorNet.SharpFlow.Engine
{
    public interface IHandle
    {
        public enum HandleType
        {
            Input,
            Output
        }

        string Id { get; }

        HandleType Type { get; }

        object Value { get; set;  }
    }
}
