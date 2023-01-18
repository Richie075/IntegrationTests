using IntegrationTest.API.DataFactory.Agents.Builders;
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.DataModel.dr;
using Laetus.NT.Core.PersistenceApi.Enumerations;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class ICamCreator : DeviceAgentCreator
    {
        private string _prefix = "";
        internal override Agent ConstructModel(string prefix = "")
        {
            _prefix = prefix;
            return new AgentBuilder()
                .WithName($"{_prefix}ICam")
                .WithAgentType(1)
                .WithAssemblyName("Laetus.NT.Core.DeviceReflector.DeviceReflectorAgent")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("AgentName","PU1Cam").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("GoToOperational","false").WithDataTypeId(DataTypeEnum.Boolean).GetObject()
            };
        }

        internal override DRBootstrapData ConstructBootstrapData()
        {
            return new DRBootstrapDataBuilder()
                .WithDRDeviceDriver(DeviceDriver.InspectCamera)
                .GetObject();
        }

        internal override IList<DRParameterSet> ConstructParameterSet()
        {
            return new List<DRParameterSet>
            {
                new DRParameterSetBuilder("ConnectionParameterSet")
                    .WithParameter(new List<DRParameter>
                    {
                        new DRParameterBuilder("InspectWtIPAddress","127.0.0.1").GetObject(),
                        new DRParameterBuilder("InspectWtGuiPort","32212").GetObject(),
                        new DRParameterBuilder("InspectWtSoiPort","32210").GetObject(),
                        new DRParameterBuilder("InspectWtSiiPort","32211").GetObject()
                    })
                    .GetObject(),
                new DRParameterSetBuilder("CameraParameterSet")
                    .WithParameter(new List<DRParameter>
                    {
                        new DRParameterBuilder("CameraMode","0").GetObject()
                    })
                    .GetObject(),
            };
        }

        internal override DRConfigurationData ConstructConfigurationData()
        {
            return new DRConfigurationDataBuilder()
                .WithDRMethodChannel($"{_prefix}CameraMethodCall")
                .WithDRInputDataChannel($"{_prefix}CameraInputChannel")
                .WithDROutputDataChannel($"{_prefix}CameraOutputChannel")
                .GetObject();
        }
    }
}
