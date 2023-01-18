using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.DataModel.wf;

namespace IntegrationTest.API.DataFactory.Agents
{
    public abstract class ScriptingAgentCreator : AgentCreator
    {

        protected ScriptingAgentCreator():base()
        {
            
        }

        public override Agent CreateModel(string prefix = "")
        {
            var agent = ConstructModel(prefix);
            agent.SystemParameter = ConstructSystemParameter();
            agent.WorkflowScriptsScriptingAgent = ConstructWorkflowScripts();
            
            return agent;
        }

        internal abstract IList<WorkflowScriptsScriptingAgent> ConstructWorkflowScripts();

    }
}
