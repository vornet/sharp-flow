using System.Reflection;
using VorNet.SharpFlow.Engine.Data.Models.Metadata;
using VorNet.SharpFlow.Engine.Execution;
using VorNet.SharpFlow.Engine.Execution.Nodes;

namespace VorNet.SharpFlow.Engine.Serilaizer
{
    public class MetadataGenerator : IMetadataGenerator
    {
        private readonly IBufferedLogger _logger;

        public MetadataGenerator(IBufferedLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Metadata Generate()
        {
            // Load all assemblies first.
            List<Assembly> allAssemblies = new List<Assembly>();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (string dll in Directory.GetFiles(path, "SharpFlow.Engine.Nodes.dll"))
            {
                allAssemblies.Add(Assembly.LoadFile(dll));
            }

            List<NodeInfo> nodeInfoList = new List<NodeInfo>();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var t in assembly.GetTypes())
                {
                    try
                    {
                        if (t.IsSubclassOf(typeof(NodeBase)) && t != typeof(StartNode) && t != typeof(EndNode))
                        {
                            NodeBase instance = (NodeBase)Activator.CreateInstance(t, _logger, null);

                            nodeInfoList.Add(new NodeInfo
                            {
                                Type = instance.GetType().FullName,
                                DisplayType = instance.DisplayType,
                                Handles = instance.Handles.Select(t => new Models.Handle { Id = t.Id, DisplayType = t.DisplayType, Type = t.GetType().FullName, Direction = t.Direction.ToString().ToLower() })
                            });
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            return new Metadata { Nodes = nodeInfoList };
        }
    }
}
