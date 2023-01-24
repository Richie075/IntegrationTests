using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.Enumerations;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class PCSManagerCreator : AgentCreator
    {
        internal override Agent ConstructModel(string prefix = "")
        {
            return new AgentBuilder()
                .WithName($"{prefix}{TestConstants.PcsManagerName}")
                .WithAgentType(0)
                .WithAssemblyName("Laetus.NT.Core.PCSManager.Agent")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("HostHmiUrl", "http://localhost:7435").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("FrontendPath", "./agents/Laetus.NT.Core.PCSManager.Agent/Frontend").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("PageSize", "10").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("ControlPanelHmiUrl", "http://localhost:12000").WithDataTypeId(DataTypeEnum.String).GetObject(),
            };
        }
    }
}
