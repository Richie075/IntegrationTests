using IntegrationTest.API.DataFactory.Agents;
using IntegrationTest.API.DataFactory.Agents.Builders;
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.DataModel.pd;
using Laetus.NT.Core.PersistenceApi.Interfaces;
using LineTopologyBuilder = Laetus.NT.Core.Persistence.Test.TestUtils.LineTopologyBuilder;

namespace IntegrationTest.API.SeedData.SEConfiguration.MVSetup.SimpleMv
{
    public class MvLinePhysicalTopologyCreator : ModelCreator<LineTopology>
    {
        private readonly IPersistenceProvider _persistenceProvider;

        public MvLinePhysicalTopologyCreator(IPersistenceProvider persistencePorvider)
        {
            _persistenceProvider = persistencePorvider ?? throw new ArgumentNullException(nameof(persistencePorvider));
        }
        internal override LineTopology ConstructModel(string prefix = "")
        {
            var agentId = _persistenceProvider.CrudProvider.Read<Agent>(a => a.Name.Contains(TestConstants.CrevisName)).First().Id;
            var countryId = _persistenceProvider.CrudProvider.Read<Country>(c => c.Name == "*").First().Id;
            return new LineTopologyBuilder($"{prefix}LineTopology")
                .WithVariant("A")
                .WithCountryId(countryId)
                .WithLine(new LineBuilder("Line")
                        .WithPlantTopologyLine(new List<PlantTopologyLine>{new PlantTopologyLineBuilder("Line1")
                                    .WithDomainId(TestConstants.LineDomainId)
                                    .WithPlantTopology(new PlantTopologyBuilder($"{prefix}PlantTopology")
                                        .WithPhysicalTopologyId(1)
                                        .GetObject())
                                    .GetObject()
                                })
                        .WithPhysicalTopology(new PhysicalTopologyBuilder($"{prefix}LinePhysicalTopoloy")
                                                        .WithPhysicalTopologyHost(new List<PhysicalTopologyHost> {
                                                            new PhysicalTopologyHostBuilder($"{prefix}LinePFS")
                                                                .WithSubType("Line")
                                                                .WithHostName("DevLine")
                                                                .WithIpAddress("127.0.0.1")
                                                                .WithPorts("7401")
                                                                .WithMacAddress("some mac")
                                                                .WithInstanceId(new Guid(TestConstants.LinePfsInstanceId))
                                                                .WithAgents(new List<PhysicalTopologyAgent>
                                                                {
                                                                    new PhysicalTopologyAgentBuilder(TestConstants.CrevisName)
                                                                        .WithAgentId(GetAgentId(TestConstants.CrevisName))
                                                                        .GetObject(),
                                                                    new PhysicalTopologyAgentBuilder(TestConstants.DeviceServerName)
                                                                        .WithAgentId(GetAgentId(TestConstants.DeviceServerName))
                                                                        .GetObject(),
                                                                    new PhysicalTopologyAgentBuilder(TestConstants.MvWorkflowName)
                                                                        .WithAgentId(GetAgentId(TestConstants.MvWorkflowName))
                                                                        .GetObject(),
                                                                    new PhysicalTopologyAgentBuilder(TestConstants.LineName)
                                                                        .WithAgentId(GetAgentId(TestConstants.LineName))
                                                                        .GetObject(),
                                                                    new PhysicalTopologyAgentBuilder(TestConstants.ICamName)
                                                                        .WithAgentId(GetAgentId(TestConstants.ICamName))
                                                                        .GetObject(),
                                                                    new PhysicalTopologyAgentBuilder(TestConstants.WolkeName)
                                                                        .WithAgentId(GetAgentId(TestConstants.WolkeName))
                                                                        .GetObject()

                                                                })
                                                                .GetObject()})
                                                        .GetObject())
                        .GetObject())
                .WithRootProcessUnit(new LineTopologyProcessUnitBuilder($"{prefix}Root")
                    .WithIoModule(new LineTopologyProcessUnitIoModuleBuilder("IO PU")
                        .WithExternalStart(false)
                        .WithDisableCommunication(true)
                        .WithExternalStartChannel("")
                        .WithResetDevicesChannel("")
                        .WithAgentId(1)
                        .GetObject())
                    .WithShowOnUI(false)
                    .WithProcessPhase("")
                    .WithProcessUnitId(GetProcessUnitId($"{prefix}RootPU"))
                    .WithChildren(new List<LineTopologyProcessUnit>
                    {
                        new LineTopologyProcessUnitBuilder($"{prefix}PU1")
                            .WithIoModule(new LineTopologyProcessUnitIoModuleBuilder("")
                                .WithExternalStart(true)
                                .WithDisableCommunication(false)
                                .WithExternalStartChannel($"{prefix}MVIoResumePo")
                                .WithResetDevicesChannel($"{prefix}MVIoResetDevicePins")
                                .WithAgentId(agentId)
                                .GetObject())
                            .WithShowOnUI(true)
                            .WithAgents(new List<LineTopologyAgent>
                            {
                                new LineTopologyAgentBuilder($"{prefix}IO-Module")
                                    .WithAgentId(GetAgentId(TestConstants.CrevisName))
                                    .WithSkipReconfiguration(false)
                                    .WithAgentRoleId(1)
                                    .GetObject(),
                                new LineTopologyAgentBuilder($"{prefix}MV-Camera")
                                    .WithAgentId(GetAgentId(TestConstants.ICamName))
                                    .WithSkipReconfiguration(false)
                                    .WithAgentRoleId(2)
                                    .GetObject(),
                                new LineTopologyAgentBuilder($"{prefix}MV-Printer")
                                    .WithAgentId(GetAgentId(TestConstants.WolkeName))
                                    .WithSkipReconfiguration(false)
                                    .WithAgentRoleId(3)
                                    .GetObject(),
                                new LineTopologyAgentBuilder($"{prefix}MV-Scripting")
                                    .WithAgentId(GetAgentId(TestConstants.MvWorkflowName))
                                    .WithSkipReconfiguration(false)
                                    .WithAgentRoleId(4)
                                    .GetObject(),
                            })
                            .WithProcessPhase("black")
                            .WithProcessUnitId(GetProcessUnitId($"{prefix}CartonPU"))
                            .GetObject()
                    })
                    .GetObject())
                .GetObject();
        }

        private int GetAgentId(string name)
        {
            return _persistenceProvider.CrudProvider.Read<Agent>(a => a.Name.Contains(name)).First().Id;
        }
        private int GetProcessUnitId(string name)
        {
            return _persistenceProvider.CrudProvider.Read<ProcessUnit>(a => a.Name.Contains(name)).First().Id;
        }
    }
}
