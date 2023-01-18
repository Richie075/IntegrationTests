
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.dr;

namespace IntegrationTest.API.DataFactory.Agents.Builders
{
    internal class DRParameterBuilder : TestDataBuilder<DRParameter>
    {

        public DRParameterBuilder(string name, string value)
        {
            testObject.Name = name;
            testObject.Value = value;
        }
    }
}
