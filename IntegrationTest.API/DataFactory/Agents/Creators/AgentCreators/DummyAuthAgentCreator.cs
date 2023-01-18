using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class DummyAuthAgentCreator : AgentCreator
    {

        internal override Agent ConstructModel(string prefix = "")
        {
            AgentBuilder ab = new AgentBuilder()
                .WithName($"{prefix}Authentication")
                .WithAgentType(0)
                .WithAssemblyName("DummyAuthAgent")
                .WithInstanceId(Guid.NewGuid());
            return ab.GetObject();
        }
        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("AuthTokenExpiration", "7200000").WithDataTypeId(Laetus.NT.Core.PersistenceApi.Enumerations.DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("AuthDomain", ".").WithDataTypeId(Laetus.NT.Core.PersistenceApi.Enumerations.DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("AuthType", "Windows").WithDataTypeId(Laetus.NT.Core.PersistenceApi.Enumerations.DataTypeEnum.String).GetObject(),
            };
        }
    }
}
