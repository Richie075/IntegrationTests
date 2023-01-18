using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
