using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;

namespace IntegrationTest.API.DataFactory.Agents.Builders
{
    public class PlantTopologyBuilder : TestDataBuilder<PlantTopology>
    {
        public PlantTopologyBuilder(string name)
        {
            testObject.Name = name;
        }

        public PlantTopologyBuilder WithPlantTopologyLine(IList<PlantTopologyLine> plantTopologyLines)
        {
            testObject.PlantTopologyLine = plantTopologyLines;
            return this;
        }

        public PlantTopologyBuilder WithPhysicalTopology(PhysicalTopology physicalTopology)
        {
            testObject.PhysicalTopology = physicalTopology;
            return this;
        }
        public PlantTopologyBuilder WithPhysicalTopologyId(int physicalTopologyId)
        {
            testObject.PhysicalTopologyId = physicalTopologyId;
            return this;
        }

    }
}
