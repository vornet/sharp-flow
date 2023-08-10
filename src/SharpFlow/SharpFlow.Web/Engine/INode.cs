namespace SharpFlow.Web.Engine
{
    public interface INode
    {
        IEnumerable<IHandle> InputHandles { get; set; }
        IEnumerable<IHandle> OutputHandles { get; set; }
    }
}
