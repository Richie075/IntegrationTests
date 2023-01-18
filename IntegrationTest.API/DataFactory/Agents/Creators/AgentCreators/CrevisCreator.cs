
using IntegrationTest.API.DataFactory.Agents.Builders;
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.DataModel.dr;
using Laetus.NT.Core.PersistenceApi.Enumerations;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class CrevisCreator : DeviceAgentCreator
    {
        private string _prefix = "";
        internal override Agent ConstructModel(string prefix = "")
        {
            _prefix = prefix;
            var agent = new AgentBuilder()
                .WithName($"{prefix}Crevis IO Module")
                .WithAgentType(1)
                .WithAssemblyName("Laetus.NT.Core.DeviceReflector.DeviceReflectorAgent")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();


            return agent;
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("AgentName", $"{_prefix}Crevis").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("GoToOperational", "true").WithDataTypeId(DataTypeEnum.Boolean).GetObject(),
            };
        }

        internal override DRBootstrapData ConstructBootstrapData()
        {
            return new DRBootstrapDataBuilder()
                .WithDRDeviceDriver(DeviceDriver.CrevisIOModuleDeviceDriver)
                .GetObject();
        }

        internal override IList<DRParameterSet> ConstructParameterSet()
        {
            return new List<DRParameterSet>
            {
                new DRParameterSetBuilder("ConnectionParameterSet")
                    .WithParameter(new List<DRParameter>
                    {
                        new DRParameterBuilder("IOModuleIPAddress", "127.0.0.1").GetObject(),
                        new DRParameterBuilder("IOModulePort", "502").GetObject(),
                    })
                    .GetObject(),
                new DRParameterSetBuilder("MachineEnable")
                    .WithParameter(new List<DRParameter>
                    {
                        new DRParameterBuilder("MachineEnableId", "Output2").GetObject()
                    })
                    .GetObject(),
                new DRParameterSetBuilder("SystemReady")
                    .WithParameter(new List<DRParameter>
                    {
                        new DRParameterBuilder("SystemReadyId", "Output3").GetObject()
                    })
                    .GetObject(),
            };
        }

        internal override DRConfigurationData ConstructConfigurationData()
        {
            return new ConfigurationDataBuilder()
                .WithMethodChannel($"{_prefix}IoMethodCall")
                .WithInputDataChannel($"{_prefix}IoInputChannel")
                .WithOutputDataChannel($"{_prefix}IoOutputChannel")
                .GetObject();
        }
    }
}
