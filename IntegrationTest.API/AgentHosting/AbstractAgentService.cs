using System.Reflection;
using Laetus.NT.Base.Common.Logger;
using Laetus.NT.Base.Platform.Agent;
using Laetus.NT.Base.Platform.SDE;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.Interfaces;

namespace IntegrationTest.API.AgentHosting
{
    [Serializable]
    public abstract class AbstractAgentService : MarshalByRefObject, IAgentService
    {
        internal IPersistenceProvider _persistenceProvider;
        internal ILogger _logger;
        internal ISde _sde;
        internal readonly string _agentPath;

        protected AbstractAgentService()
        {
            _agentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "agents");
            

        }
        internal static ResolveEventHandler CreateAssemblyResolver(IList<string> dirs, Func<string, Assembly> systemLoader, Func<string, Assembly> loader, ILogger logger)
        {
            return (sender, args) =>
            {
                var requesting = args.RequestingAssembly;
                var dll = args.Name.Split(',')[0] + ".dll";
                var file = dirs.SelectMany(dir => Directory.EnumerateFileSystemEntries(dir, dll, SearchOption.AllDirectories)).OrderBy(f => f == "Common" ? 1 : 0).FirstOrDefault(File.Exists);

                if (file == null)
                {
                    return systemLoader(args.Name);
                }
                logger.LogVerbose("Lib {0} requires {1}, found dll in {2}", requesting == null ? "null" : requesting.GetName().Name, dll, file);
                var res = loader(file);
                return res;
            };
        }
        public void Start()
        {
            var configuredAgents = _persistenceProvider.CrudProvider.Read<PhysicalTopologyHost>(pth => pth.InstanceId.Equals(new Guid(TestConstants.PlantPfsInstanceId))).Single().PhysicalTopologyAgent.Select(pta => pta.Agent);
            var assemblyNames = configuredAgents.Select(ca => ca.AssemblyName);
            
            List<IAgent> agents = new List<IAgent>();
            foreach (var dll in assemblyNames)
            {
                try
                {
                    var a = LoadAssembly(dll);
                    var agentType = DetermineAgentType(a);
                    if (agentType != null)
                    {
                        var instance = CreateInstance(agentType,
                            _persistenceProvider.CrudProvider.Read<Agent>(ag => ag.AssemblyName == a.GetName().Name)
                                .First().Id, _sde,
                            _logger, _persistenceProvider);

                        agents.Add(instance);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            foreach (var a in agents)
            {
                Task.Factory.StartNew(a.Start, TaskCreationOptions.LongRunning).ContinueWith((t) =>
                {
                    if (t.Exception != null)
                    {
                        var aggException = t.Exception.Flatten();
                        foreach (var exception in aggException.InnerExceptions)
                            _logger.LogError("Error executing agent task.{0} {1}", exception, t.Id, a.GetInstanceGuid());
                    }

                }, TaskContinuationOptions.OnlyOnFaulted);
            }
        }

        private Assembly LoadAssembly(string assemblyName)
        {
            Assembly lib = Assembly.LoadFrom(Path.Combine(_agentPath, assemblyName, $"{assemblyName}.dll"));
            _logger.LogDebug("Assembly {0}  loaded. Instance: {1}", lib, assemblyName);
            return lib;
        }

        private TypeInfo DetermineAgentType(Assembly lib)
        {
            try
            {
                var agentType = lib.DefinedTypes.Single(t0 => typeof(IAgent).IsAssignableFrom(t0));
                return agentType;
            }
            catch (Exception ex)
            {
                if (ex is ReflectionTypeLoadException typeLoadException)
                {
                    var loaderExceptions = typeLoadException.LoaderExceptions;
                    Console.WriteLine(String.Join(";", loaderExceptions.Select(e => e.Message)));
                }

                return null;
            }


        }

        private IAgent CreateInstance(Type t, int agentId, ISde sde, ILogger logger, IPersistenceProvider persistence)
        {
            if (!IsAgent(t))
            {
                throw new ArgumentException(String.Format($"Type {t} does not implement IAgent"));
            }

            return Activator.CreateInstance(
                t, agentId, sde, logger, persistence) as IAgent;

        }

        private bool IsAgent(Type type)
        {
            return typeof(IAgent).IsAssignableFrom(type);
        }
    }
}
