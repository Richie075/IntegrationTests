using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using IntegrationTest.API.Setup;
using Laetus.NT.Base.Common.Logger;
using Laetus.NT.Base.Platform.Agent;
using Laetus.NT.Base.Platform.PlatformService;
using Laetus.NT.Base.Platform.SDE;
using Laetus.NT.Core.Persistence.Test.TestApi;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.Interfaces;
using LocalSde;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Topshelf;
using Timer = System.Timers.Timer;

namespace IntegrationTest.API.AgentHosting
{
    public class PlantApplicationHost
    {
        private IPersistenceProvider _persistenceProvider;
        private Dictionary<string, Type> _agents = new Dictionary<string, Type>();
        private string _agentPath;
        private string _commonPath;
        private string _currentPath;
        public PlantApplicationHost(IPersistenceProvider persistenceProvider)
        {
             AppDomain currentDomain = AppDomain.CurrentDomain;
            
            _persistenceProvider = PersistenceInitializer.CreateProvider(TestConstants.PlantDbName, false);
            _agentPath = Path.Combine(@"D:\SVN\current\up2\global\bin\x64\Debug", "Agents");
            _commonPath = Path.Combine(@"D:\SVN\current\up2\global\bin\x64\Debug", "Common");
            _currentPath = new FileInfo(typeof(PlantApplicationHost).Assembly.Location).Directory.FullName;

            var configuredAgents = _persistenceProvider.CrudProvider.Read<Agent>().Select(a => a.AssemblyName);
            string[] dlls = Directory.GetFiles(_agentPath, "Laetus.*.dll", SearchOption.AllDirectories);

            currentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            foreach (var dll in dlls)
            {
                var fI = new FileInfo(dll);
                if (configuredAgents.Contains(Path.GetFileNameWithoutExtension(fI.Name)))
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFile(dll);
                        var match = assembly.GetTypes().SingleOrDefault(t => typeof(IAgent).IsAssignableFrom(t));
                        if (match != null && !_agents.ContainsKey(assembly.GetName().Name))
                        {
                            _agents.Add(assembly.GetName().Name, match);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                var founddlls = Directory.GetFiles(_agentPath, $"{GetAssemblyName(args)}.dll", SearchOption.AllDirectories).ToList();
                founddlls.AddRange(Directory
                    .GetFiles(_commonPath, $"{GetAssemblyName(args)}.dll", SearchOption.AllDirectories).ToList());
                founddlls.AddRange(
                    Directory.GetFiles(_currentPath, $"{GetAssemblyName(args)}.dll", SearchOption.AllDirectories));
                var dll = founddlls.FirstOrDefault();

                if (dll == null)
                {
                    return null;
                }
                return Assembly.LoadFile(new FileInfo(dll).FullName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
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
        public async void DoSomething()
        {
            var agentPath = Path.Combine(@"D:\SVN\current\up2\global\bin\x64\Debug\PLant.PFS");
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = agentPath;
            // setup.ConfigurationFile = "(some file)";

            // Set up the Evidence
            Evidence baseEvidence = AppDomain.CurrentDomain.Evidence;
            Evidence evidence = new Evidence(baseEvidence);
            //evidence.AddAssembly("(some assembly)");
            //evidence.AddHost("(some host)");

            //AppDomain domain = AppDomain.CreateDomain("IntegrationTests", evidence, setup);
            AppDomain domain = AppDomain.CreateDomain("IntegrationTests");
            domain.GetAssemblies().ToList().ForEach(Console.WriteLine);
            //domain.InitializeLifetimeService();
            var type = typeof(AgentHost);
            var service = (AgentHost)domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
            service.InjectAgentService(new PlantAgentService());
            service.Start();
        }
        
        static void LaunchCommandLineApp()
        {
            var currentPath = new FileInfo(typeof(PlantApplicationHost).Assembly.Location).DirectoryName;
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            startInfo.FileName = Path.Combine(currentPath, "IntegrationTest.Host.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            //startInfo.Arguments = "-f j -o \"" + ex1 + "\" -z 1.0 -s y " + ex2;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    //exeProcess.WaitForExit();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private IAgent CreateInstance(Type t, int agentId, ISde sde, ILogger logger, IPersistenceProvider persistence)
        {
            if (!typeof(IAgent).IsAssignableFrom(t))
            {
                throw new ArgumentException(String.Format($"Type {t} does not implement IAgent"));
            }

            return Activator.CreateInstance(
                t, agentId, sde, logger, persistence) as IAgent;

        }
    }

    public class TownCrier
    {
        readonly System.Timers.Timer _timer;
        public TownCrier()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
        }
        public void Start() { _timer.Start(); }

        public void Stop()
        {
            _timer.Stop();
        }
    }
    internal class Bootstrapper
    {
        public static IServiceProvider GetServiceProvider(IServiceCollection services, IEnumerable<IAgent> agents)
        {
            foreach(var agent in agents)
                services.AddSingleton<IAgent>(agent);
            //services.AddSingleton<agent>(provider =>
            //{
            //    var jobFactory = new agent(provider);
            //    return jobFactory;
            //});
            //services.AddSingleton<InvoiceProcessingJob>();

            //services.AddSingleton<ITodoRepository, TodoRepository>();

            //services.AddHttpClient<ITodoApiClient, TodoApiClient>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
