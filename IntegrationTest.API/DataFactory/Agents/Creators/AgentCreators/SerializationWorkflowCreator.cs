
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.DataModel.wf;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    internal class SerializationWorkflowCreator : ScriptingAgentCreator
    {
        internal override Agent ConstructModel(string prefix = "")
        {
            return new AgentBuilder()
                .WithName($"{prefix}{TestConstants.MvWorkflowName}")
                .WithAgentType(2)
                .WithAssemblyName("Laetus.NT.Core.ScriptExecutionAgent")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("ExecutionScope","Global").WithDataTypeId(Laetus.NT.Core.PersistenceApi.Enumerations.DataTypeEnum.String).GetObject()
            };
        }

        internal override IList<WorkflowScriptsScriptingAgent> ConstructWorkflowScripts()
        {
            return Enumerable.Empty<WorkflowScriptsScriptingAgent>().ToList();
        }
    }
}
