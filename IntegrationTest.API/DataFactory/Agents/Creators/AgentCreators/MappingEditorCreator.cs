using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.Enumerations;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class MappingEditorCreator : AgentCreator
    {
        internal override Agent ConstructModel(string prefix = "")
        {
            return new AgentBuilder()
                .WithName($"{prefix}{TestConstants.MappingEditorName}")
                .WithAgentType(0)
                .WithAssemblyName("Laetus.NT.Core.MappingEditor.Agent")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("HostHmiUrl", "http://localhost:7432").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("FrontendPath", "./agents/Laetus.NT.Core.MappingEditor.Agent/Frontend").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("ControlPanelHmiUrl", "http://localhost:12000").WithDataTypeId(DataTypeEnum.String).GetObject(),
            };
        }
    }
}
