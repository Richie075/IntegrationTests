using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class DeviceServerCreator : AgentCreator
    {
        internal override Agent ConstructModel(string prefix = "")
        {
            return new AgentBuilder()
                .WithName($"{prefix}DeviceServer")
                .WithAgentType(0)
                .WithAssemblyName("Laetus.NT.Core.Platform.Cameraserver.Agent")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return Enumerable.Empty<SystemParameter>().ToList();
        }
    }
}
