using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationTest.API.Setup;
using Laetus.NT.Base.Common.Logger;
using Laetus.NT.Base.Platform.SDE;
using Laetus.NT.Core.HMI.Agent;
using Laetus.NT.Core.Line.Agent;
using Laetus.NT.Core.Persistence.Test.TestApi;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.Interfaces;
using LocalSde;
using Topshelf;

namespace IntegrationTest.API.AgentHosting
{
    public class PlantApplicationHost
    {
        private IPersistenceProvider _persistenceProvider;

        public PlantApplicationHost(IPersistenceProvider persistenceProvider)
        {
            _persistenceProvider = persistenceProvider ?? throw new ArgumentNullException(nameof(persistenceProvider));
        }
        public void DoSomething()
        {
            string lineAssemblyName = typeof(LineAgent).Assembly.GetName().Name;
            int lineId = _persistenceProvider.CrudProvider.Read<Agent>(a => a.AssemblyName == lineAssemblyName).First().Id;
            ILogger logger = new TestLogger("log");
            int domainId = _persistenceProvider.CrudProvider.Read<PlantTopologyLine>().First().DomainId;
            ISde sde = new EventBasedSde(domainId, logger);
            try
            {
                var host = HostFactory.Run(x =>
                {
                    
                    x.AddCommandLineDefinition("fooBar", f =>
                    {
                        var fooBar = f;
                    });
                    x.ApplyCommandLine();
                    x.Service<LineAgent>(s =>
                    {
                        s.ConstructUsing(name => new LineAgent(lineId, sde, logger, _persistenceProvider));
                        s.WhenStarted(s => s.Start());
                        s.WhenStopped(s => s.Stop());
                    });
                    //x.Service<HmiAgent>(s =>
                    //{
                    //    s.ConstructUsing(name => new HmiAgent(lineId, sde, logger, _persistenceProvider));
                    //    s.WhenStarted(s => s.Start());
                    //    s.WhenStopped(s => s.Stop());
                    //});
                    x.RunAsLocalSystem();
                    x.SetDescription("Line");
                    x.SetDisplayName("MyLine");
                    x.StartAutomatically();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
