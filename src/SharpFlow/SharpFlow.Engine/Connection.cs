namespace VorNet.SharpFlow.Engine
{
    public class Connection
    {
        private IHandle _fromHandle;
        private IHandle _toHandle;

        public Connection(IHandle fromHandle, IHandle toHandle)
        {
            _fromHandle = fromHandle;
            _toHandle = toHandle;
        }
    }
}
