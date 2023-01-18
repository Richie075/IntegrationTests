using IntegrationTest.API.DataFactory.Agents.Builders;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.Interfaces;

namespace IntegrationTest.API.DataFactory.Agents.Creators.SeSetupCreators.SimpleMv
{
    public class MvPlantPhysicalTopologyCreator : ModelCreator<PhysicalTopology>
    {
        private readonly IPersistenceProvider _persistenceProvider;

        public MvPlantPhysicalTopologyCreator(IPersistenceProvider persistencePorvider)
        {
            _persistenceProvider = persistencePorvider ?? throw new ArgumentNullException(nameof(persistencePorvider));
        }
        internal override PhysicalTopology ConstructModel(string prefix = "")
        {
            return new PhysicalTopologyBuilder($"{prefix}PlantPhysicalTopoloy")
                .WithPlantTopology(new List<PlantTopology>
                {
                    new PlantTopologyBuilder($"{prefix}PlantMvTopology").GetObject()
                })
                .WithPhysicalTopologyHost(new List<PhysicalTopologyHost> {
                    new PhysicalTopologyHostBuilder($"{prefix}PlantPFS")
                        .WithSubType("Line")
                        .WithHostName("DevPlant")
                        .WithIpAddress("127.0.0.1")
                        .WithPorts("7401")
                        .WithMacAddress("some mac")
                        .WithInstanceId(new Guid("2174082B-DA91-4F04-A9D1-63B3CE12E8A4"))
                        .WithAgents(new List<PhysicalTopologyAgent>
                        {
                            new PhysicalTopologyAgentBuilder("USC Export-Import")
                                .WithAgentId(GetAgentId("USC Export-Import"))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder("Authentication")
                                .WithAgentId(GetAgentId("Authentication"))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder("Mapping Editor")
                                .WithAgentId(GetAgentId("Mapping Editor"))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder("Code Pool Manager")
                                .WithAgentId(GetAgentId("Code Pool Manager"))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder("PO Manager")
                                .WithAgentId(GetAgentId("PO Manager"))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder("PCS Manager")
                                .WithAgentId(GetAgentId("PCS Manager"))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder("Control Panel")
                                .WithAgentId(GetAgentId("Control Panel"))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder("External Communicator")
                                .WithAgentId(GetAgentId("External Communicator"))
                                .GetObject(),
                            new PhysicalTopologyAgentBuilder("Audit Trail")
                                .WithAgentId(GetAgentId("Audit Trail"))
                                .GetObject(),

                        })
                        .GetObject()
                })
                .GetObject();
        }

        private int GetAgentId(string name)
        {
            return _persistenceProvider.CrudProvider.Read<Agent>(a => a.Name == name).First().Id;
        }
    }
}
