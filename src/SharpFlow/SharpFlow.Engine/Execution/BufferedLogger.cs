using System.Text;

namespace VorNet.SharpFlow.Engine.Execution
{
    public class BufferedLogger : IBufferedLogger
    {
        private readonly StringBuilder _outputBuffer = new StringBuilder();

        public void ClearBuffer()
        {
            _outputBuffer.Clear();
        }

        public string GetBuffer()
        {
            return _outputBuffer.ToString();
        }

        public void Log(string log)
        {
            _outputBuffer.AppendLine(log);
        }
    }
}
