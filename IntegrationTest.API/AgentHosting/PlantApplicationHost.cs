using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

                ILogger logger = new TestLogger("IntegrationTest");

                ISde sde = new EventBasedSde(TestConstants.PlantDomainId, logger);
                var services = new ServiceCollection();
                var agents = new List<Task>();
                //foreach (var a in _agents.Skip(0).Take(1))
                //{
                //    try
                //    {
                //        var agent = CreateInstance(a.Value,
                //            _persistenceProvider.CrudProvider.Read<Agent>(ag => ag.AssemblyName == a.Key).First().Id,
                //            sde,
                //            logger, _persistenceProvider);
                //        agents.Add(Task.Run(() => agent.Start()));
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex.Message);
                //    }
                //}

                ;
                //await Task.WhenAll(agents);
                //var servProvider = Bootstrapper.GetServiceProvider(services,
                //    _agents.Select(a => CreateInstance(a.Value, _persistenceProvider.CrudProvider.Read<Agent>(ag => ag.AssemblyName == a.Key).First().Id, sde, logger, _persistenceProvider)));

                var cts = new CancellationTokenSource();
                foreach (var a in _agents)
                {
                    try { 
                        var rc = HostFactory.Run(x => //1
                        {
                            x.Service<IAgent>(s => //2
                            {
                                s.ConstructUsing(name => CreateInstance(a.Value, _persistenceProvider.CrudProvider.Read<Agent>(ag => ag.AssemblyName == a.Key).First().Id, sde, logger, _persistenceProvider)); //3
                                s.WhenStarted(tc => tc.Start()); //4
                                s.WhenStopped(tc => tc.Stop()); //5
                            });
                            x.RunAsLocalSystem(); //6

                            x.SetDescription($"{a.Key} Sample Topshelf Host"); //7
                            x.SetDisplayName($"{a.Key}_Stuff"); //8
                            x.SetServiceName($"{a.Key}_Stuff"); //9
                        });
                        var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());

                    //LaunchCommandLineApp();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
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
