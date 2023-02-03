using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using IntegrationTest.API.AgentHosting;
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

        private static AgentHost service;
        public static void Main(string[] args)
        {
            Start();

            
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
            var type = typeof(AgentHost);
            service = (AgentHost)domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
            service.InjectAgentService(new PlantAgentService());
            service.Start();
        }


    }
}
