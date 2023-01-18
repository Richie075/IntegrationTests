
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;

namespace IntegrationTest.API.DataFactory.Agents.Builders
{
    public  class PhysicalTopologyAgentBuilder : TestDataBuilder<PhysicalTopologyAgent>
    {
        public PhysicalTopologyAgentBuilder(string name)
        {
            testObject.Name = name;
        }

        public PhysicalTopologyAgentBuilder WithAgentId(int agentId)
        {
            testObject.AgentId = agentId;
            return this;
        }
        public PhysicalTopologyAgentBuilder WithAgent(Agent agent)
        {
            testObject.Agent = agent;
            return this;
        }

        public PhysicalTopologyAgentBuilder WithPhysicalTopologyId(int physicalTopologyHostId)
        {
            testObject.PhysicalTopologyHostId = physicalTopologyHostId;
            return this;
        }
        public PhysicalTopologyAgentBuilder WithPhysicalTopologyHost(PhysicalTopologyHost physicalTopologyHost)
        {
            testObject.PhysicalTopologyHost = physicalTopologyHost;
            return this;
        }
    }
}
