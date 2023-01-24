using IntegrationTest.API.DataFactory.Agents.Builders;
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.DataModel.dr;
using Laetus.NT.Core.PersistenceApi.Enumerations;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class WolkeCreator : DeviceAgentCreator
    {
        private string _prefix = "";
        internal override Agent ConstructModel(string prefix = "")
        {
            _prefix = prefix;
            return new AgentBuilder()
                .WithName($"{_prefix}{TestConstants.WolkeName}")
                .WithAgentType(1)
                .WithAssemblyName("Laetus.NT.Core.DeviceReflector.DeviceReflectorAgent")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("AgentName",$"{_prefix}Printer").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("GoToOperational","false").WithDataTypeId(DataTypeEnum.Boolean).GetObject(),
                new SystemParameterBuilder("UseBinaryProtocol","true").WithDataTypeId(DataTypeEnum.Boolean).GetObject(),
            };
        }

        internal override DRBootstrapData ConstructBootstrapData()
        {
            return new DRBootstrapDataBuilder()
                .WithDRDeviceDriver(DeviceDriver.WolkeDeviceDriver)
                .GetObject();
        }

        internal override IList<DRParameterSet> ConstructParameterSet()
        {
            return new List<DRParameterSet>
            {
                new DRParameterSetBuilder("ConnectionParameterSet")
                    .WithParameter(new List<DRParameter>
                    {
                        new DRParameterBuilder("WolkeIPAddress", "127.0.0.1").GetObject(),
                        new DRParameterBuilder("WolkePort", "34568").GetObject(),
                    })
                    .GetObject()
            };
        }

        internal override DRConfigurationData ConstructConfigurationData()
        {
            return new ConfigurationDataBuilder()
                .WithMethodChannel($"{_prefix}WolkeMethodCall")
                .WithInputDataChannel($"{_prefix}WolkeInputChannel")
                .WithOutputDataChannel($"{_prefix}WolkeOutputChannel")
                .GetObject();
        }
    }
}
