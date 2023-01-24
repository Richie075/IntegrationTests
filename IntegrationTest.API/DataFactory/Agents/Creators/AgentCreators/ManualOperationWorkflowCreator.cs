using IntegrationTest.API.DataFactory.Agents.Builders;
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.DataModel.wf;
using Laetus.NT.Core.PersistenceApi.Enumerations;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class ManualOperationWorkflowCreator : ScriptingAgentCreator
    {
        private string _prefix = "";
        internal override Agent ConstructModel(string prefix = "")
        {
            _prefix = prefix;
            return new AgentBuilder()
                .WithName($"{prefix}{TestConstants.MoWorkflowNameName}")
                .WithAgentType(2)
                .WithAssemblyName("Laetus.NT.Core.ScriptExecutionAgent")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("ExecutionScope","Global").WithDataTypeId(DataTypeEnum.String).GetObject()
            };
        }

        internal override IList<WorkflowScriptsScriptingAgent> ConstructWorkflowScripts()
        {
            var wfs = new WorkflowScriptsScriptingAgentBuilder().GetObject();
            wfs.WorkflowScript = new WorkflowScriptBuilder($"{_prefix}Manual-Workflow")
                .WithScript(@"var logger = host.Find('LogContext','1.0');\nlogger.info('Script Executing');")
                .WithScriptEngineParameter(new ScriptEngineParameterBuilder()
                    .WithDebugEnabled(true)
                    .WithWaitForDebugger(true)
                    .WithRemoteDebugEnabled(true)
                    .WithDebugPort(9223)
                    .GetObject())
                .GetObject();
            return new List<WorkflowScriptsScriptingAgent>
            {
            };
        }
    }
}
