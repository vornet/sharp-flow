using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorNet.SharpFlow.Engine.Execution
{
    public interface IBufferedLogger
    {
        void Log(string log);

        string GetBuffer();
        void ClearBuffer();
    }
}
