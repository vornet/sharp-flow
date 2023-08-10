namespace VorNet.SharpFlow.Engine
{
    public class Connection
    {
        public IHandle FromHandle { get; }
        public IHandle ToHandle { get; }

        public Connection(IHandle fromHandle, IHandle toHandle)
        {
            FromHandle = fromHandle;
            ToHandle = toHandle;
        }
    }
}
