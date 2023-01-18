
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.DataModel.dr;

namespace IntegrationTest.API.DataFactory.Agents
{
    public abstract class DeviceAgentCreator : AgentCreator
    {

        protected DeviceAgentCreator():base()
        {
            
        }

        public override Agent CreateModel(string name = "")
        {
            var agent = ConstructModel(name);
            agent.SystemParameter = ConstructSystemParameter();
            agent.BootstrapData = ConstructBootstrapData();
            if (agent.BootstrapData != null)
            {
                agent.BootstrapData.DRParameterSet = ConstructParameterSet();
            }
            agent.ConfigurationData = ConstructConfigurationData();
            return agent;
        }


        internal abstract DRBootstrapData ConstructBootstrapData();
        internal abstract IList<DRParameterSet> ConstructParameterSet();
        internal abstract DRConfigurationData ConstructConfigurationData();
    }
}
