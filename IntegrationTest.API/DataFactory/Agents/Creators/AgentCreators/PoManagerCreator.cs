using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.Enumerations;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class PoManagerCreator : AgentCreator
    {
        internal override Agent ConstructModel(string prefix = "")
        {
            return new AgentBuilder()
                .WithName($"{prefix}PO Manager")
                .WithAgentType(0)
                .WithAssemblyName("Laetus.NT.Core.POManager.Agent")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("HostHmiUrl","http://localhost:7437").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("FrontendPath","./agents/Laetus.NT.Core.POManager.Agent/Frontend").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("ControlPanelHmiUrl","http://localhost:12000").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("ReportAssetsPath","./agents/Laetus.NT.Core.POManager.Agent/Frontend/assets").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("PoAutoSeal","false").WithDataTypeId(DataTypeEnum.Boolean).GetObject(),
                new SystemParameterBuilder("AutomaticEndOfBatchReport","false").WithDataTypeId(DataTypeEnum.Boolean).GetObject(),
                new SystemParameterBuilder("PoReportRejectedUscCount","10").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("MaxNumberOfCodeRecycleRetries","3").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("PageSize","10").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("PreventDifferentMaterialsForLevels","false").WithDataTypeId(DataTypeEnum.Boolean).GetObject(),
                new SystemParameterBuilder("SdeTransferTimeout","60000").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("ChunkSize","5000").WithDataTypeId(DataTypeEnum.Int32).GetObject()
            };
        }
    }
}
