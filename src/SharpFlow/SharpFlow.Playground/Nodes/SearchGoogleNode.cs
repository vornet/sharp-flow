using VorNet.SharpFlow.Engine;
using VorNet.SharpFlow.Engine.Handles;

namespace VorNet.SharpFlow.Playground.Nodes
{
    internal class SearchGoogleNode : NodeBase
    {
        public IHandle ExecIn { get { return GetOutputHandleById("execIn"); } }

        public IHandle ExecOut { get { return GetOutputHandleById("execOut"); } }

        public IHandle SearchText { get { return GetOutputHandleById("searchText"); } }

        public IHandle SearchResult { get { return GetOutputHandleById("searchResult"); } }

        public SearchGoogleNode()
        {
            AddHandle(new ExecHandle("execIn", IHandle.HandleType.Input));
            AddHandle(new StringHandle("searchText", IHandle.HandleType.Input));


            AddHandle(new ExecHandle("execOut", IHandle.HandleType.Output));
            AddHandle(new StringHandle("searchResult", IHandle.HandleType.Output));
        }

        public async Task ExecuteAsync()
        {
            var inputHandle = GetInputHandleById("searchText");
            string searchText = (string)inputHandle.Value;

            // Google search.
            string searchResult = "search_result";

            var outputHandle = GetOutputHandleById("searchResult");
            outputHandle.Value = searchResult;
        }
    }
}
