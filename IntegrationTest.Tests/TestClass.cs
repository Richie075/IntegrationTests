using IntegrationTest.API;
using IntegrationTest.API.AgentHosting;
using IntegrationTest.API.SeedData.SEConfiguration.MVSetup.Plant;
using IntegrationTest.API.Setup;
using Laetus.NT.Core.PersistenceApi.Interfaces;
using NUnit.Framework;

namespace IntegrationTest.Tests
{
    public class TestClass
    {
        private ConfigurationFactory _configurationFactory;
        private IPersistenceProvider _plantPersistenceProvider;
        private IPersistenceProvider _linePersistenceProvider;
        [SetUp]
        public void SetupMv()
        {
            _plantPersistenceProvider = PersistenceInitializer.CreateProvider(TestConstants.PlantDbName);
            _linePersistenceProvider = PersistenceInitializer.CreateProvider(TestConstants.LineDbName);
            _configurationFactory = new ConfigurationFactory(new SimpleMvSetup(_plantPersistenceProvider, _linePersistenceProvider));
            _configurationFactory.CreateSetup();
            PlantApplicationHost pah = new PlantApplicationHost(_plantPersistenceProvider);
            pah.DoSomething();
        }
        [Test]
        public void TestSomething()
        {
            Assert.True(true);
        }
    }
}
