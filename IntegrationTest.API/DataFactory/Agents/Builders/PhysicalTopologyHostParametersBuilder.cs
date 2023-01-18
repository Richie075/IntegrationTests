
using Laetus.NT.Base.Platform.Contracts;
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.Enumerations;

namespace IntegrationTest.API.DataFactory.Agents.Builders
{
    public class PhysicalTopologyHostParametersBuilder : TestDataBuilder<PhysicalTopologyHostParameters>
    {
        public PhysicalTopologyHostParametersBuilder(string name, string value)
        {
            testObject.Name = name;
            testObject.Value = value;
        }

        public PhysicalTopologyHostParametersBuilder WithDescription(string description)
        {
            testObject.Description = description;
            return this;
        }

        public PhysicalTopologyHostParametersBuilder WithDataType(DataTypeEnum dataType)
        {
            testObject.DataTypeId = (int)dataType;
            return this;
        }

        public PhysicalTopologyHostParametersBuilder WithIsRequired(bool isRequired)
        {
            testObject.Required = isRequired;
            return this;
        }
        public PhysicalTopologyHostParametersBuilder WithIsCollection(bool isCollection)
        {
            testObject.IsCollection = isCollection;
            return this;
        }
    }
}
