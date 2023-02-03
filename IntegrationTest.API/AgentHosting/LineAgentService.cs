using System.Reflection;
using IntegrationTest.API.Setup;
using Laetus.NT.Core.Persistence.Test.TestApi;
using LocalSde;

namespace IntegrationTest.API.AgentHosting
{
    [Serializable]
    public class LineAgentService : AbstractAgentService
    {
        public LineAgentService()
        {
            _logger = new TestLogger("IntegrationTest_Line");

            _persistenceProvider = PersistenceInitializer.CreateProvider(TestConstants.LineDbName, false);
            _sde = new EventBasedSde(TestConstants.LineDomainId, _logger);
            var assemblyDirs = new List<string> { _agentPath };
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CreateAssemblyResolver(assemblyDirs, s => null, Assembly.ReflectionOnlyLoadFrom, _logger);
            AppDomain.CurrentDomain.AssemblyResolve += CreateAssemblyResolver(assemblyDirs, s => null, Assembly.LoadFrom, _logger);
            AppDomain.CurrentDomain.TypeResolve += CreateAssemblyResolver(assemblyDirs, s => null, Assembly.LoadFrom, _logger);
        }


    }
}
