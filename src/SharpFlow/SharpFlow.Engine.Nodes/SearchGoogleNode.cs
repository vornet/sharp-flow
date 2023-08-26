using VorNet.SharpFlow.Engine.Execution;
using VorNet.SharpFlow.Engine.Execution.Nodes;
using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Engine.Nodes
{
    public class SearchGoogleNode : NodeBase
    {
        public SearchGoogleNode(IBufferedLogger logger, string id)
            : base(logger, "searchGoogle", id)
        {
            AddHandle(new ExecHandle("execIn", IHandle.HandleDireciton.Target));
            AddHandle(new StringHandle("searchText", IHandle.HandleDireciton.Target));


            AddHandle(new ExecHandle("execOut", IHandle.HandleDireciton.Source));
            AddHandle(new StringHandle("searchResult", IHandle.HandleDireciton.Source));
        }

        public override async Task ExecuteAsync()
        {
            var inputHandle = GetHandleById("searchText");
            string searchText = (string)inputHandle.Value;

            // Google search.
            string searchResult = "search_result";

            BufferedLogger.Log($"Searching google for {searchText} and found {searchResult}.");

            var outputHandle = GetHandleById("searchResult");
            outputHandle.Value = searchResult;
        }
    }
}
