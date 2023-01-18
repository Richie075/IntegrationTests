using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.wf;

namespace IntegrationTest.API.DataFactory.Agents.Builders
{
    public class WorkflowScriptBuilder : TestDataBuilder<WorkflowScript>
    {
        public WorkflowScriptBuilder(string name)
        {
            testObject.Name = name;
        }

        public WorkflowScriptBuilder WithScript(string script)
        {
            testObject.Script = script;
            return this;
        }
        public WorkflowScriptBuilder WithChecksum(int checksum)
        {
            testObject.Checksum = checksum;
            return this;
        }
        public WorkflowScriptBuilder WithVersion(int version)
        {
            testObject.Version = version;
            return this;
        }

        public WorkflowScriptBuilder WithScriptEngineParameter(ScriptEngineParameters scriptEngineParameters)
        {
            testObject.ScriptEngineParameters = scriptEngineParameters;
            return this;
        }
    }
}
