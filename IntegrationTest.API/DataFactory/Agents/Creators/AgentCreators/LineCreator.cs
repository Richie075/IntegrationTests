using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.Enumerations;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class LineCreator : AgentCreator
    {
        internal override Agent ConstructModel(string prefix = "")
        {
            return new AgentBuilder()
                .WithName($"{prefix}Line")
                .WithAgentType(0)
                .WithAssemblyName("Laetus.NT.Core.Line.Agent")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("IoModuleMethodCallRetryDelay","5000").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("IoModuleMethodCallRetries","3").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("ReportAssetsPath","./agents/Laetus.NT.Core.Line.Agent/assets").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("AgentHeartBeatCheckInterval","2000").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("LineHeartbeatPublishInterval","2000").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("uploadChunkRetriesMaxCount","3").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("AgentHeartBeatTimeout","5000").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("AskAgentStateDelay","100").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("CheckAgentHeartBeatsEnabled","false").WithDataTypeId(DataTypeEnum.Boolean).GetObject(),
                new SystemParameterBuilder("IoModuleMethodCallTimeout","2000").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("AgentStateChangeRequestDelay","500").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("ProcessUnitStartupTimeout","180000").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("PoReportRejectedUscCount","10").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
            };
        }
    }
}
