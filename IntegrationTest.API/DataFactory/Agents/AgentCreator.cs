
using Laetus.NT.Core.PersistenceApi.DataModel.conf;

namespace IntegrationTest.API.DataFactory.Agents
{
    public abstract class AgentCreator : ModelCreator<Agent>
    {

        protected AgentCreator()
        {
            
        }

        public override Agent CreateModel(string prefix = "")
        {
            var agent = ConstructModel(prefix);
            agent.SystemParameter = ConstructSystemParameter();
            return agent;
        }

        internal abstract override Agent ConstructModel(string prefix = "");

        internal abstract IList<SystemParameter> ConstructSystemParameter();
    }
}
