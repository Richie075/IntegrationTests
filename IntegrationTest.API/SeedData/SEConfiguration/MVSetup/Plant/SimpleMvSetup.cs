using IntegrationTest.API.DataFactory.Agents;
using IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators;
using IntegrationTest.API.SeedData.SEConfiguration.MVSetup.SimpleMv;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.DataModel.pd;
using Laetus.NT.Core.PersistenceApi.Interfaces;

namespace IntegrationTest.API.SeedData.SEConfiguration.MVSetup.Plant
{
    public class SimpleMvSetup : ISetupCreator
    {
        private IPersistenceProvider _plantPersistenceProvider;
        private IPersistenceProvider _linePersistenceProvider;
        private ModelFactory _modelFactory;

        public SimpleMvSetup(IPersistenceProvider plantPersistenceProvider, IPersistenceProvider linePersistenceProvider)
        {
            _plantPersistenceProvider = plantPersistenceProvider ?? throw new ArgumentNullException(nameof(plantPersistenceProvider));
            _linePersistenceProvider = linePersistenceProvider ?? throw new ArgumentNullException(nameof(linePersistenceProvider));
            _modelFactory = new ModelFactory();
        }
        public void CreateSetup()
        {
            CreateAgents();
            CreateProcessUnits();
            CreatePackagingTopology();
            CreatePlantPhysicalTopology();
            CreateLinePhysicalTopology();
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
                _modelFactory.CreateModel(new AuditTrailCreator())
            };
            _plantPersistenceProvider.CrudProvider.Create(agents, 20);

            agents = new List<Agent>
            {
                _modelFactory.CreateModel(new SerializationWorkflowCreator()),
                _modelFactory.CreateModel(new CrevisCreator()),
                _modelFactory.CreateModel(new WolkeCreator()),
                _modelFactory.CreateModel(new ICamCreator()),
                _modelFactory.CreateModel(new DeviceServerCreator()),
                _modelFactory.CreateModel(new LineCreator()),
                _modelFactory.CreateModel(new HMICreator())
            };
            _linePersistenceProvider.CrudProvider.Create(agents, 20);
        }
        private void CreateProcessUnits()
        {
            var processUnits = new List<ProcessUnit>
            {
                _modelFactory.CreateModel(new MvProcessUnitCreator("RootPU", 1)),
                _modelFactory.CreateModel(new MvProcessUnitCreator("CartonPU", 1))
            };
            _plantPersistenceProvider.CrudProvider.Create(processUnits, 5);
            _linePersistenceProvider.CrudProvider.Create(processUnits, 5);
        }

        private void CreatePackagingTopology()
        {
            var countryId = _plantPersistenceProvider.CrudProvider.Read<Country>(c => c.Name == "*").Single();
            var pt = _modelFactory.CreateModel(new MvPackagingTopologyCreator(countryId.Id, 1), "Carton");
            _plantPersistenceProvider.CrudProvider.Create(pt);
            _linePersistenceProvider.CrudProvider.Create(pt);

        }

        private void CreatePlantPhysicalTopology()
        {
            var pt = _modelFactory.CreateModel(new MvPlantPhysicalTopologyCreator(_plantPersistenceProvider));
            _plantPersistenceProvider.CrudProvider.Create(pt);
            
        }

        private void CreateLinePhysicalTopology()
        {
            var pt = _modelFactory.CreateModel(new MvLinePhysicalTopologyCreator(_linePersistenceProvider));

            _linePersistenceProvider.CrudProvider.Create(pt);
        }

    }
}
