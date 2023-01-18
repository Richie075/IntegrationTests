using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationTest.API.DataFactory.Agents;
using IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators;
using IntegrationTest.API.DataFactory.Agents.Creators.SeSetupCreators;
using IntegrationTest.API.DataFactory.Agents.Creators.SeSetupCreators.SimpleMv;
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.DataModel.pd;
using Laetus.NT.Core.PersistenceApi.Interfaces;
using NUnit.Framework;

namespace IntegrationTest.API.SeedData.SEConfiguration.MVSetup.Plant
{
    public class SimpleMvSetup : ISetupCreator
    {
        private IPersistenceProvider _persistenceProvider;
        private ModelFactory _modelFactory;

        public SimpleMvSetup(IPersistenceProvider persistenceProvider)
        {
            _persistenceProvider = persistenceProvider ?? throw new ArgumentNullException(nameof(persistenceProvider));
            _modelFactory = new ModelFactory();
        }
        public void CreateSetup()
        {
            CreateAgents();
            CreateProcessUnits();
            CreatePackagingTopology();
            CreatePlantPhysicalTopology();
        }

        private void CreateAgents()
        {
            List<Agent> agents = new List<Agent>
            {
                _modelFactory.CreateModel(new DummyAuthAgentCreator()),
                _modelFactory.CreateModel(new ControlPanelCreator()),
                _modelFactory.CreateModel(new MasterDataManagerCreator()),
                _modelFactory.CreateModel(new PoManagerCreator()),
                _modelFactory.CreateModel(new CodePoolCreator()),
                _modelFactory.CreateModel(new MappingEditorCreator()),
                _modelFactory.CreateModel(new UscExportImportCreator()),
                _modelFactory.CreateModel(new PCSManagerCreator()),
                _modelFactory.CreateModel(new SerializationWorkflowCreator()),
                _modelFactory.CreateModel(new CrevisCreator()),
                _modelFactory.CreateModel(new WolkeCreator()),
                _modelFactory.CreateModel(new ICamCreator()),
                _modelFactory.CreateModel(new DeviceServerCreator()),
                _modelFactory.CreateModel(new LineCreator()),
                _modelFactory.CreateModel(new ExternalCommunicatorCreator()),
                _modelFactory.CreateModel(new AuditTrailCreator()),
            };
            _persistenceProvider.CrudProvider.Create(agents, 20);
        }
        private void CreateProcessUnits()
        {
            var processUnits = new List<ProcessUnit>
            {
                _modelFactory.CreateModel(new MvProcessUnitCreator("RootPU", 1)),
                _modelFactory.CreateModel(new MvProcessUnitCreator("CartonPU", 1))
            };
            _persistenceProvider.CrudProvider.Create(processUnits, 5);
        }

        private void CreatePackagingTopology()
        {
            var countryId = _persistenceProvider.CrudProvider.Read<Country>(c => c.Name == "*").Single();
            var pt = _modelFactory.CreateModel(new MvPackagingTopologyCreator(countryId.Id, 1), "Carton");
            _persistenceProvider.CrudProvider.Create(pt);

        }

        private void CreatePlantPhysicalTopology()
        {
            var pt = _modelFactory.CreateModel(new MvPlantPhysicalTopologyCreator(_persistenceProvider));
            _persistenceProvider.CrudProvider.Create(pt);
        }

    }
}
