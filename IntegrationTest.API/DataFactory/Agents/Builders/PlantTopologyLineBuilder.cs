using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;

namespace IntegrationTest.API.DataFactory.Agents.Builders
{
    public class PlantTopologyLineBuilder : TestDataBuilder<PlantTopologyLine>
    {
        public PlantTopologyLineBuilder(string name)
        {
            testObject.Name = name;
        }

        public PlantTopologyLineBuilder WithDomainId(int domainId)
        {
            testObject.DomainId = domainId;
            return this;
        }
        public PlantTopologyLineBuilder WithLineId(int lineId)
        {
            testObject.LineId = lineId;
            return this;
        }

        public PlantTopologyLineBuilder WithPlantTopologyId(int plantTopologyId)
        {
            testObject.PlantTopology = plantTopologyId;
            return this;
        }
        public PlantTopologyLineBuilder WithLine(Line line)
        {
            testObject.Line = line;
            return this;
        }

        public PlantTopologyLineBuilder WithPlantTopology(PlantTopology plantTopology)
        {
            testObject.PlantTopologyNavigation = plantTopology;
            return this;
        }
    }
}
