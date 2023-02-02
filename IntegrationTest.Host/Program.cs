using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using Laetus.NT.Base.Common.Logger;
using Laetus.NT.Base.Platform.Agent;
using Laetus.NT.Core.PersistenceApi.Interfaces;
using LocalSde;
using Microsoft.Extensions.DependencyInjection;
using Topshelf;
using Timer = System.Timers.Timer;

namespace IntegrationTest.Host
{
    [Serializable]
    internal class Program
    {

        private static AgentService service;
        public static void Main(string[] args)
        {
            Start();

            //foreach (var dll in dlls)
            //{
            //    if (configuredAgents.Any(ca => ca.AssemblyName.Contains(dll.GetName().Name)))
            //    {
            //        try
            //        {
                        
            //            if (!_agents.ContainsKey(dll.GetName().Name))
            //            {
            //                _agents.Add(dll.GetName().Name, dll.GetType());
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.ToString());
            //        }
            //    }
            //}

            //var rc = HostFactory.Run(x =>                                   //1
            //{
            //    x.Service<TownCrier>(s =>                                   //2
            //    {
            //        s.ConstructUsing(name => new TownCrier());                //3
            //        s.WhenStarted(tc => tc.Start());                         //4
            //        s.WhenStopped(tc => tc.Stop());                          //5
            //    });
            //    x.RunAsLocalSystem();                                       //6

            //    x.SetDescription("Sample Topshelf Host");                   //7
            //    x.SetDisplayName("Stuff");                                  //8
            //    x.SetServiceName("Stuff");                                  //9
            //});                                                             //10
            
            
            
            //var rc1 = HostFactory.Run(x => //1
            //{
            //    x.Service<ServiceManager>(s => //2
            //    {
            //        s.ConstructUsing(name => new ServiceManager(agents)); //3
            //        s.WhenStarted(tc => tc.Start()); //4
            //        s.WhenStopped(tc => tc.Stop()); //5
            //    });
            //    x.RunAsLocalSystem(); //6

            //    x.SetDescription("IntegrationTestPFS Topshelf Host"); //7
            //    x.SetDisplayName("IntegrationTestPFS_Stuff"); //8
            //    x.SetServiceName("IntegrationTestPFS_Stuff"); //9
            //});
            //var exitCode = (int)Convert.ChangeType(rc1, rc1.GetTypeCode());
            //var rc1 = HostFactory.Run(x =>                                   //1
            //{
            //    x.Service<LineAgent>(s =>                                   //2
            //    {
            //        var agent = servProvider.GetRequiredService<IAgent>();
            //        //s.ConstructUsing(name => new LineAgent(1,sde, logger, persistenceProvider));                //3
            //        s.ConstructUsing(name => servProvider.GetRe);                //3
            //        s.WhenStarted(tc => tc.Start());                         //4
            //        s.WhenStopped(tc => tc.Stop());                          //5
            //    });
            //    x.RunAsLocalSystem();                                       //6

            //    x.SetDescription("Sample Topshelf LineAgent");                   //7
            //    x.SetDisplayName("LA Stuff");                                  //8
            //    x.SetServiceName("LA Stuff");                                  //9
            //});
            // var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());  //11
            Console.ReadLine();
            //Environment.ExitCode = exitCode;
        }

        

        static void Start()
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
            var type = typeof(AgentService);
            service = (AgentService)domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
            //AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CreateAssemblyResolver(assemblyDirs, s => null, Assembly.ReflectionOnlyLoadFrom, _logger);//Assembly.ReflectionOnlyLoad
            //AppDomain.CurrentDomain.AssemblyResolve += CreateAssemblyResolver(assemblyDirs, s => null, Assembly.LoadFrom, _logger);
            //AppDomain.CurrentDomain.TypeResolve += CreateAssemblyResolver(assemblyDirs, s => null, Assembly.LoadFrom, _logger);
            //AgentService agentService = new AgentService();
            service.Start();
        }


    }

    
    public class TownCrier
    {
        readonly Timer _timer;
        public TownCrier()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
        }

        public void Start()
        {
            _timer.Start();
        }

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
