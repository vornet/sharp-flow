using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorNet.SharpFlow.Engine.Execution;
using VorNet.SharpFlow.Engine.Execution.Nodes;
using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Nodes
{
    public class IfNode : NodeBase
    {
        public IfNode(IBufferedLogger logger, string id)
            : base(logger, "if", id)
        {
            AddHandle(new ExecHandle("execIn", IHandle.HandleDireciton.Target));
            AddHandle(new BoolHandle("condition", IHandle.HandleDireciton.Target));


            AddHandle(new ExecHandle("execOutTrue", IHandle.HandleDireciton.Source));
            AddHandle(new ExecHandle("execOutFalse", IHandle.HandleDireciton.Source));
        }

        public override async Task ExecuteAsync()
        {
            bool condition = (bool)GetHandleById("condition").Value;
        }
    }
}
