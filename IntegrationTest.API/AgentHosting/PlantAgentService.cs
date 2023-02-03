using System.Reflection;
using IntegrationTest.API.Setup;
using Laetus.NT.Core.Persistence.Test.TestApi;
using LocalSde;

namespace IntegrationTest.API.AgentHosting
{
    [Serializable]
    public class PlantAgentService : AbstractAgentService
    {
        public PlantAgentService()
        {
            _logger = new TestLogger("IntegrationTest_Plant");

            _persistenceProvider = PersistenceInitializer.CreateProvider(TestConstants.PlantDbName, false);
            _sde = new EventBasedSde(TestConstants.PlantDomainId, _logger);
            var assemblyDirs = new List<string> { _agentPath };
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CreateAssemblyResolver(assemblyDirs, s => null, Assembly.ReflectionOnlyLoadFrom, _logger);
            AppDomain.CurrentDomain.AssemblyResolve += CreateAssemblyResolver(assemblyDirs, s => null, Assembly.LoadFrom, _logger);
            AppDomain.CurrentDomain.TypeResolve += CreateAssemblyResolver(assemblyDirs, s => null, Assembly.LoadFrom, _logger);

        }


    }
}
