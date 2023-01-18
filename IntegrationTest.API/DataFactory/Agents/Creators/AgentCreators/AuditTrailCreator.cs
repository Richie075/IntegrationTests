using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.Enumerations;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class AuditTrailCreator : AgentCreator
    {
        internal override Agent ConstructModel(string prefix = "")
        {
            return new AgentBuilder()
                .WithName($"{prefix}Audit Trail")
                .WithAgentType(0)
                .WithAssemblyName("Laetus.NT.Core.Platform.AuditTrail")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("ControlPanelOrder", "7")
                    .WithDataTypeId(DataTypeEnum.String)
                    .WithDescription("Default AuditTrail Control Panel Order")
                    .WithIsRequired(true)
                    .WithIsCollection(false)
                    .WithIsGlobal(false)
                    .GetObject(),
                new SystemParameterBuilder("ControlPanelHmiUrl", "http://localhost:12000")
                    .WithDataTypeId(DataTypeEnum.String)
                    .WithDescription("Default AuditTrail Control Panel Order")
                    .WithIsRequired(true)
                    .WithIsCollection(false)
                    .WithIsGlobal(false)
                    .GetObject(),
                new SystemParameterBuilder("HostHmiUrl", "http://localhost:7442")
                    .WithDataTypeId(DataTypeEnum.String)
                    .WithDescription("URL address for accessing Audit Trail UI.")
                    .WithIsRequired(true)
                    .WithIsCollection(false)
                    .WithIsGlobal(false)
                    .GetObject(),
                new SystemParameterBuilder("FrontendPath", "./agents/Laetus.NT.Core.Platform.AuditTrail/Frontend")
                    .WithDataTypeId(DataTypeEnum.String)
                    .WithDescription("Path to Frontend folder")
                    .WithIsRequired(true)
                    .WithIsCollection(false)
                    .WithIsGlobal(false)
                    .GetObject(),
                new SystemParameterBuilder("DisabledAuditTrailCategoriesCsv", "Item,GlobalParameter")
                    .WithDescription("Configuration for disabling certain Audit Trail categories.")
                    .WithIsRequired(false)
                    .WithIsCollection(true)
                    .WithIsGlobal(false).WithDataTypeId(DataTypeEnum.String)
                    .GetObject()
            };
        }
    }
}
