using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.Enumerations;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class CodePoolCreator : AgentCreator
    {
        internal override Agent ConstructModel(string prefix = "")
        {
            return new AgentBuilder()
                .WithName($"{prefix}Code Pool Manager")
                .WithAgentType(0)
                .WithAssemblyName("Laetus.NT.Core.CodePoolManagement.Agent")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("CodeSupplierIdManualCodeSupplier", "3")
                    .WithDataTypeId(DataTypeEnum.Int32)
                    .WithDescription("ID of Manual code supplier used for configuration of MasterData. This is indicating that when line requests codes for certain Master Data, CPM assumes codes are already imported in the database using USC Export/Import.")
                    .WithIsRequired(true)
                    .WithIsCollection(false)
                    .WithIsGlobal(false)
                    .GetObject(),
                new SystemParameterBuilder("CodeSupplierIdLevel4Interface", "2")
                    .WithDataTypeId(DataTypeEnum.Int32)
                    .WithDescription("ID of Level 4 interface code supplier used for configuration of MasterData. This is indicating that when line requests codes for certain Master Data, CPM assumes codes are already imported in the database using Level 4.")
                    .WithIsRequired(true)
                    .WithIsCollection(false)
                    .WithIsGlobal(false)
                    .GetObject(),
                new SystemParameterBuilder("CodeSupplierIdLaetusCodeGenerator", "1")
                    .WithDataTypeId(DataTypeEnum.Int32)
                    .WithDescription("ID of Laetus code generator used for configuration of MasterData. This is indicating that when line requests codes for certain Master Data, CPM will use internal generator for code creation and supply.")
                    .WithIsRequired(true)
                    .WithIsCollection(false)
                    .WithIsGlobal(false)
                    .GetObject(),
                new SystemParameterBuilder("ChunkSize", "5000").WithDataTypeId(DataTypeEnum.Int32).GetObject()
            };
        }
    }
}
