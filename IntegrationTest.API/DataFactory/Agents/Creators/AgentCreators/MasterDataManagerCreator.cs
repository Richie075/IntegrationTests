using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class MasterDataManagerCreator : AgentCreator
    {
        internal override Agent ConstructModel(string prefix = "")
        {
            return new AgentBuilder()
                .WithName($"{prefix}{TestConstants.MasterDataManagerName}")
                .WithAgentType(0)
                .WithAssemblyName("Laetus.NT.Core.MasterDataManager.Agent")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("HostHmiUrl", "http://localhost:7434").WithDataTypeId(Laetus.NT.Core.PersistenceApi.Enumerations.DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("MasterDataUniqueness", "false").WithDataTypeId(Laetus.NT.Core.PersistenceApi.Enumerations.DataTypeEnum.Boolean).GetObject(),
                new SystemParameterBuilder("MasterDataEditability", "false").WithDataTypeId(Laetus.NT.Core.PersistenceApi.Enumerations.DataTypeEnum.Boolean).GetObject(),
                new SystemParameterBuilder("PageSize", "10").WithDataTypeId(Laetus.NT.Core.PersistenceApi.Enumerations.DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("FrontendPath", "./agents/Laetus.NT.Core.MasterDataManager.Agent/Frontend").WithDataTypeId(Laetus.NT.Core.PersistenceApi.Enumerations.DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("ControlPanelHmiUrl", "http://localhost:12000").WithDataTypeId(Laetus.NT.Core.PersistenceApi.Enumerations.DataTypeEnum.String).GetObject(),
            };
        }
    }
}
