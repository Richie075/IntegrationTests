
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;

namespace IntegrationTest.API.DataFactory.Agents.Builders
{
    public class PhysicalTopologyBuilder : TestDataBuilder<PhysicalTopology>

    {
        public PhysicalTopologyBuilder(string name)
        {
            testObject.Name = name;
        }

        public PhysicalTopologyBuilder WithPhysicalTopologyHost(IList<PhysicalTopologyHost> physicalTopologyHosts)
        {
            testObject.PhysicalTopologyHost = physicalTopologyHosts;
            return this;
        }

        public PhysicalTopologyBuilder WithPlantTopology(IList<PlantTopology> plantTopologies)
        {
            testObject.PlantTopology = plantTopologies;
            return this;
        }

        public PhysicalTopologyBuilder WithLine(IList<Line> lines)
        {
            testObject.Line = lines;
            return this;
        }

    }
}
