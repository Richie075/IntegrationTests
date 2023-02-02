using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IntegrationTest.API;
using IntegrationTest.API.Setup;
using Laetus.NT.Base.Common.Logger;
using Laetus.NT.Base.Platform.Agent;
using Laetus.NT.Base.Platform.SDE;
using Laetus.NT.Core.Persistence.Test.TestApi;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.Interfaces;
using LocalSde;

namespace IntegrationTest.Host
{
    [Serializable]
    internal class AgentService : MarshalByRefObject
    {
        private  ILogger _logger;
        private string _agentPath;
        public AgentService()
        {
            _logger = new TestLogger("IntegrationTest");
            
            _agentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "agents");
            var assemblyDirs = new List<string> { _agentPath };
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CreateAssemblyResolver(assemblyDirs, s => null, Assembly.ReflectionOnlyLoadFrom, _logger);//Assembly.ReflectionOnlyLoad
            AppDomain.CurrentDomain.AssemblyResolve += CreateAssemblyResolver(assemblyDirs, s => null, Assembly.LoadFrom, _logger);
            AppDomain.CurrentDomain.TypeResolve += CreateAssemblyResolver(assemblyDirs, s => null, Assembly.LoadFrom, _logger);
        }
        public static ResolveEventHandler CreateAssemblyResolver(IList<string> dirs, Func<string, Assembly> systemLoader, Func<string, Assembly> loader, ILogger logger)
        {
            return (sender, args) =>
            {
                var requesting = args.RequestingAssembly;
                var dll = args.Name.Split(',')[0] + ".dll";
                var file = dirs.SelectMany(dir =>
                {
                    return Directory.EnumerateFileSystemEntries(dir, dll, SearchOption.AllDirectories);
                }).OrderBy(f => f == "Common" ? 1 : 0).FirstOrDefault(File.Exists);

                if (file == null)
                {
                    return systemLoader(args.Name);
                }
                logger.LogVerbose("Lib {0} requires {1}, found dll in {2}", requesting == null ? "null" : requesting.GetName().Name, dll, file);
                var res = loader(file);
                //var referencedAssemblies = res.GetReferencedAssemblies();
                //foreach (var referencedAssembly in referencedAssemblies)
                //{
                //    var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                //    if (!loadedAssemblies.AsEnumerable().Select(a => a.GetName()).Contains(referencedAssembly))
                //    {
                //        var assemblyFile = dirs.SelectMany(dir =>
                //        {
                //            return Directory.EnumerateFileSystemEntries(dir, dll, SearchOption.AllDirectories);
                //        }).OrderBy(f => f == "Common" ? 1 : 0).FirstOrDefault(File.Exists);
                //        if (File.Exists(assemblyFile))
                //            Assembly.Load(File.ReadAllBytes(assemblyFile));
                //    }
                //}
                return res;
            };
        }


        public void Start()
        {
            var persistenceProvider = PersistenceInitializer.CreateProvider(TestConstants.PlantDbName, false);
            var configuredAgents = persistenceProvider.CrudProvider.Read<PhysicalTopologyHost>(pth => pth.InstanceId.Equals(new Guid(TestConstants.PlantPfsInstanceId))).Single().PhysicalTopologyAgent.Select(pta => pta.Agent);
            var dlls = configuredAgents.Select(ca => ca.AssemblyName); //GetAgentAssemblies();
            ISde sde = new EventBasedSde(TestConstants.PlantDomainId, _logger);
            List<IAgent> agents = new List<IAgent>();
            foreach (var dll in dlls)
            {
                try
                {
                    var a = LoadAssembly(dll);
                    var agentType = DetermineAgentType(a);
                    if (agentType != null)
                    {
                        var instance = CreateInstance(agentType,
                            persistenceProvider.CrudProvider.Read<Agent>(ag => ag.AssemblyName == a.GetName().Name)
                                .First().Id, sde,
                            _logger, persistenceProvider);
                        
                        agents.Add(instance);
                    }
                    //LaunchCommandLineApp();
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

                }, TaskContinuationOptions.OnlyOnFaulted); ;
            }
            //var sm = new ServiceManager(agents, _logger);
            //sm.Start();
        }
        private string GetAssemblyName(ResolveEventArgs args)
        {
            String name;
            if (args.Name.IndexOf(",") > -1)
            {
                name = args.Name.Substring(0, args.Name.IndexOf(","));
            }
            else
            {
                name = args.Name;
            }
            return name;
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
        private TypeInfo DetermineAgentType(Assembly lib)
        {
            try
            {
                var agentType = lib.DefinedTypes.Single(t0 => typeof(IAgent).IsAssignableFrom(t0));
                return agentType;
            }
            catch (Exception ex)
            {
                if (ex is System.Reflection.ReflectionTypeLoadException typeLoadException)
                {
                    var loaderExceptions = typeLoadException.LoaderExceptions;
                    Console.WriteLine(String.Join(";", loaderExceptions.Select(e => e.Message)));
                }

                return null;
            }


        }
        private Assembly LoadAssembly(string assemblyName)
        {
            //Assembly lib = Assembly.Load(File.ReadAllBytes(Path.Combine(_agentPath, assemblyName, $"{assemblyName}.dll")));
            Assembly lib = Assembly.LoadFrom(Path.Combine(_agentPath, assemblyName, $"{assemblyName}.dll"));
            //var referencedAssemblies = lib.GetReferencedAssemblies();
            //foreach (var referencedAssembly in referencedAssemblies)
            //{
            //    var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            //    if (!loadedAssemblies.AsEnumerable().Select(a => a.GetName()).Contains(referencedAssembly))
            //    {
            //        var assemblyFile = Path.Combine(_agentPath, assemblyName, $"{referencedAssembly.Name}.dll");
            //        if (File.Exists(assemblyFile))
            //            Assembly.Load(File.ReadAllBytes(assemblyFile));
            //    }
            //}
            _logger.LogDebug("Assembly {0}  loaded. Instance: {1}", lib, assemblyName);
            return lib;
        }


        private bool IsAgent(Type type)
        {
            return typeof(IAgent).IsAssignableFrom(type);
        }
        
    }

    internal class ServiceManager
    {
        private readonly List<IAgent> _agents;
        private readonly ILogger _logger;
        public ServiceManager(List<IAgent> agents, ILogger logger)
        {
            _agents = agents;
            _logger = logger;   
        }

        public void Start()
        {
            _agents.ForEach(a =>
            {
                try
                {
                    Task.Factory.StartNew(a.Start, TaskCreationOptions.LongRunning).ContinueWith((t) =>
                    {
                        if (t.Exception != null)
                        {
                            var aggException = t.Exception.Flatten();
                            foreach (var exception in aggException.InnerExceptions)
                                _logger.LogError("Error executing agent task.{0} {1}", exception, t.Id, a.GetInstanceGuid());
                        }

                    }, TaskContinuationOptions.OnlyOnFaulted); ;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        public void Stop()
        {
            _agents.ForEach(a => a.Stop());
        }
    }
}
