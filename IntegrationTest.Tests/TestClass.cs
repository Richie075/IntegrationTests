using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationTest.API.SeedData.SEConfiguration.MVSetup.Plant;
using IntegrationTest.API.Setup;
using Laetus.NT.Core.PersistenceApi.Interfaces;
using NUnit.Framework;

namespace IntegrationTest.Tests
{
    public class TestClass
    {
        private ConfigurationFactory _configurationFactory;
        private IPersistenceProvider _persistenceProvider;
        [SetUp]
        public void SetupMv()
        {
            _persistenceProvider = PersistenceInitializer.CreateProvider(false);
            _configurationFactory = new ConfigurationFactory(new SimpleMvSetup(_persistenceProvider));
            _configurationFactory.CreateSetup();
        }
        [Test]
        public void TestSomething()
        {
            Assert.True(true);
        }
    }
}
