using IntegrationTest.API.DataFactory.Agents;
using IntegrationTest.API.DataFactory.Agents.Builders;
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.Interfaces;

namespace IntegrationTest.API.SeedData.SEConfiguration.MVSetup.SimpleMv
{
    public class MvPlantPhysicalTopologyCreator : ModelCreator<PlantTopology>
    {
        private readonly IPersistenceProvider _persistenceProvider;

        public MvPlantPhysicalTopologyCreator(IPersistenceProvider persistenceProvider)
        {
            _persistenceProvider = persistenceProvider ?? throw new ArgumentNullException(nameof(persistenceProvider));
        }
        internal override PlantTopology ConstructModel(string prefix = "")
        {
            return new PlantTopologyBuilder($"{prefix}PlantMvTopology")
                .WithPhysicalTopology( new PhysicalTopologyBuilder($"{prefix}PlantPhysicalTopology")
                .WithPhysicalTopologyHost(new List<PhysicalTopologyHost> {
                    new PhysicalTopologyHostBuilder($"{prefix}PlantPFS")
                        .WithSubType("Plant")
                        .WithHostName("DevPlant")
                        .WithIpAddress("127.0.0.1")
                        .WithPorts("7401")
                        .WithMacAddress("some mac")
                        .WithInstanceId(new Guid(TestConstants.PlantPfsInstanceId))
                        .WithAgents(new List<PhysicalTopologyAgent>
                        {
                            new PhysicalTopologyAgentBuilder(TestConstants.UscExportImportName)
                                .WithAgentId(GetAgentId(TestConstants.UscExportImportName))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder(TestConstants.AuthenticationName)
                                .WithAgentId(GetAgentId(TestConstants.AuthenticationName))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder(TestConstants.MappingEditorName)
                                .WithAgentId(GetAgentId(TestConstants.MappingEditorName))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder(TestConstants.CodePoolManagerName)
                                .WithAgentId(GetAgentId(TestConstants.CodePoolManagerName))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder(TestConstants.PoManagerName)
                                .WithAgentId(GetAgentId(TestConstants.PoManagerName))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder(TestConstants.PcsManagerName)
                                .WithAgentId(GetAgentId(TestConstants.PcsManagerName))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder(TestConstants.ControlPanelName)
                                .WithAgentId(GetAgentId(TestConstants.ControlPanelName))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder(TestConstants.ExternalCommunicatorName)
                                .WithAgentId(GetAgentId(TestConstants.ExternalCommunicatorName))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder(TestConstants.AuditTrailName)
                                .WithAgentId(GetAgentId(TestConstants.AuditTrailName))
                                .GetObject(),

                        })
                        .GetObject()
                })
                .WithLine(new List<Line> {new LineBuilder($"{prefix}Line").GetObject()})
                .GetObject())
                .WithPlantTopologyLine(new List<PlantTopologyLine> {new PlantTopologyLineBuilder("Line1")
                    .WithDomainId(TestConstants.LineDomainId)
                    .WithLineId(1)
                    .GetObject()})
                .GetObject();
        }

        private int GetAgentId(string name)
        {
            return _persistenceProvider.CrudProvider.Read<Agent>(a => a.Name.Contains(name)).First().Id;
        }
    }
}
