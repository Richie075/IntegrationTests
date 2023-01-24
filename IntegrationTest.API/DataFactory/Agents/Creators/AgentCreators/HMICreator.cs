using IntegrationTest.API.DataFactory.Agents.Builders;
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.DataModel.dr;
using Laetus.NT.Core.PersistenceApi.Enumerations;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class HMICreator : AgentCreator
    {
        private string _prefix = "";
        internal override Agent ConstructModel(string prefix = "")
        {
            _prefix = prefix;
            return new AgentBuilder()
                .WithName($"{_prefix}{TestConstants.HmiName}")
                .WithAgentType(0)
                .WithAssemblyName("Laetus.NT.Core.HMI.Agent")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("ControlPanelHmiUrl",@"http://localhost:12000").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("DefaultRequestTimeout","15000").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("FrontendPath",".\\\\agents\\\\Laetus.NT.Core.HMI.Agent\\\\Frontend").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("HostHmiUrl","http://localhost:2001").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("PageSize","10").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("UITopology","1").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("MaxAmountItemsWithSameSNonUI","10").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("POClosureCheck001","false").WithDataTypeId(DataTypeEnum.Boolean).GetObject(),
                new SystemParameterBuilder("POClosureCheck002","false").WithDataTypeId(DataTypeEnum.Boolean).GetObject(),
                new SystemParameterBuilder("POClosureCheck003","false").WithDataTypeId(DataTypeEnum.Boolean).GetObject()
            };
        }

    }
}
