
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.dr;

namespace IntegrationTest.API.DataFactory.Agents.Builders
{
    internal class DRParameterSetBuilder : TestDataBuilder<DRParameterSet>

    {
        public DRParameterSetBuilder()
        {}
        public DRParameterSetBuilder(string name)
        {
            testObject.Name = name;
        }

        public DRParameterSetBuilder WithName(string name)
        {
            testObject.Name = name;
            return this;
        }
        public DRParameterSetBuilder WithParameter(List<DRParameter> drParameter)
        {
            testObject.DRParameter = drParameter;
            return this;
        }

    }
}
