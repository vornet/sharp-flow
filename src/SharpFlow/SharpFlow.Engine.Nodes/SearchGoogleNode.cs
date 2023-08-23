using VorNet.SharpFlow.Engine;
using VorNet.SharpFlow.Engine.Handles;
using VorNet.SharpFlow.Engine.Nodes;

namespace VorNet.SharpFlow.Engine.Nodes
{
    public class SearchGoogleNode : NodeBase
    {
        public SearchGoogleNode(string id)
            : base(id)
        {
            AddHandle(new ExecHandle("execIn", IHandle.HandleType.Target));
            AddHandle(new StringHandle("searchText", IHandle.HandleType.Target));


            AddHandle(new ExecHandle("execOut", IHandle.HandleType.Source));
            AddHandle(new StringHandle("searchResult", IHandle.HandleType.Source));
        }

        public override async Task ExecuteAsync()
        {
            var inputHandle = GetHandleById("searchText");
            string searchText = (string)inputHandle.Value;

            // Google search.
            string searchResult = "search_result";

            var outputHandle = GetHandleById("searchResult");
            outputHandle.Value = searchResult;
        }
    }
}
