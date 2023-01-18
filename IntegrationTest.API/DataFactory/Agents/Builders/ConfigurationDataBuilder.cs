
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.dr;

namespace IntegrationTest.API.DataFactory.Agents.Builders
{
    internal class ConfigurationDataBuilder : TestDataBuilder<DRConfigurationData>
    {
        public ConfigurationDataBuilder()
        {}

        public ConfigurationDataBuilder WithMethodChannel(string methodChannel)
        {
            testObject.DRMethodChannel = methodChannel;
            return this;
        }

        public ConfigurationDataBuilder WithInputDataChannel(string inputDataChannel)
        {
            testObject.DRInputDataChannel = inputDataChannel;
            return this;
        }

        public ConfigurationDataBuilder WithOutputDataChannel(string outputDataChannel)
        {
            testObject.DROutputDataChannel = outputDataChannel;
            return this;
        }
    }
}
