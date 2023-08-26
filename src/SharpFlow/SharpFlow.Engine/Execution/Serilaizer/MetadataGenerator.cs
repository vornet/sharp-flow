using System.Reflection;
using VorNet.SharpFlow.Engine.Data.Models.Metadata;
using VorNet.SharpFlow.Engine.Execution.Nodes;

namespace VorNet.SharpFlow.Engine.Serilaizer
{
    public class MetadataGenerator : IMetadataGenerator
    {
        public Metadata Generate()
        {
            // Load all assemblies first.
            List<Assembly> allAssemblies = new List<Assembly>();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (string dll in Directory.GetFiles(path, "*.dll"))
            {
                allAssemblies.Add(Assembly.LoadFile(dll));
            }

            List<NodeInfo> nodeInfoList = new List<NodeInfo>();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    nodeInfoList.AddRange(assembly.GetTypes()
                        .Where(t => t.IsSubclassOf(typeof(NodeBase)) && t != typeof(StartNode) && t != typeof(EndNode))
                        .Select(t =>
                        {
                            NodeBase instance = (NodeBase)Activator.CreateInstance(t, "foo");

                            return new NodeInfo
                            {
                                Type = instance.GetType().FullName,
                                DisplayType = instance.DisplayType,
                                Handles = instance.Handles.Select(t => new Models.Handle { Id = t.Id, DisplayType = t.DisplayType, Type = t.GetType().FullName, Direction = t.Direction.ToString().ToLower() })
                            };
                        }));
                } catch
                {
                }
            }

            return new Metadata { Nodes = nodeInfoList };
        }
    }
}
