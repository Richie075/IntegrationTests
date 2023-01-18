using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class ControlPanelCreator : AgentCreator
    {
        internal override Agent ConstructModel(string prefix = "")
        {
            return new AgentBuilder()
                .WithName($"{prefix}Control Panel")
                .WithAgentType(0)
                .WithAssemblyName("Laetus.NT.Core.ControlPanel")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("HostHmiUrl", "http://localhost:12000").WithDataTypeId(Laetus.NT.Core.PersistenceApi.Enumerations.DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("FrontendPath", "./agents/Laetus.NT.Core.ControlPanel/Frontend").WithDataTypeId(Laetus.NT.Core.PersistenceApi.Enumerations.DataTypeEnum.String).GetObject()
            };
        }
    }
}
