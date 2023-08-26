namespace VorNet.SharpFlow.Engine.Handles
{
    public interface IHandle
    {
        public enum HandleDireciton
        {
            Target,
            Source
        }

        string DisplayType { get; }

        string Id { get; }

        HandleDireciton Direction { get; }

        object Value { get; set; }
    }
}
