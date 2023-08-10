using VorNet.SharpFlow.Engine;
using VorNet.SharpFlow.Engine.Handles;
using VorNet.SharpFlow.Engine.Nodes;

namespace VorNet.SharpFlow.Playground.Nodes
{
    internal class SearchGoogleNode : NodeBase
    {
        public IHandle SearchText { get { return GetHandleById("searchText"); } }

        public IHandle SearchResult { get { return GetHandleById("searchResult"); } }

        public SearchGoogleNode(string id)
            : base(id)
        {
            AddHandle(new ExecHandle("execIn", IHandle.HandleType.Input));
            AddHandle(new StringHandle("searchText", IHandle.HandleType.Input));


            AddHandle(new ExecHandle("execOut", IHandle.HandleType.Output));
            AddHandle(new StringHandle("searchResult", IHandle.HandleType.Output));
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
