using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.Enumerations;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class UscExportImportCreator : AgentCreator
    {
        internal override Agent ConstructModel(string prefix = "")
        {
            return new AgentBuilder()
                .WithName($"{prefix}USC Export-Import")
                .WithAgentType(0)
                .WithAssemblyName("Laetus.NT.Core.USCExportImport")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("Separator", "Comma")
                    .WithDataTypeId(DataTypeEnum.String)
                    .WithDescription("Separator of different information in same row. When each row contains only numeric USC number, use 'None' as separator.")
                    .WithIsRequired(true)
                    .WithIsCollection(false)
                    .WithIsGlobal(false)
                    .GetObject(),
                new SystemParameterBuilder("FrontendPath", "./agents/Laetus.NT.Core.USCExportImport/Frontend")
                    .WithDataTypeId(DataTypeEnum.String)
                    .WithDescription("Defines the Location of the Frontend code (HTML & JS)")
                    .WithIsRequired(true)
                    .WithIsCollection(false)
                    .WithIsGlobal(false)
                    .GetObject(),
                new SystemParameterBuilder("ControlPanelOrder", "8")
                    .WithDataTypeId(DataTypeEnum.Int32)
                    .WithDescription("Default USCExportImport Control Panel Order")
                    .WithIsRequired(true)
                    .WithIsCollection(false)
                    .WithIsGlobal(false)
                    .GetObject(),
                new SystemParameterBuilder("OutputCsvSeparator", "Comma")
                    .WithDataTypeId(DataTypeEnum.String)
                    .WithDescription("Separator used in output .csv files.")
                    .WithIsRequired(true)
                    .WithIsCollection(false)
                    .WithIsGlobal(false)
                    .GetObject(),
                new SystemParameterBuilder("ControlPanelHmiUrl", "http://localhost:12000")
                    .WithDataTypeId(DataTypeEnum.String)
                    .WithDescription("Control Panel URL, so user can get back from USC Export-Import UI upon clicking on the link.")
                    .WithIsRequired(true)
                    .WithIsCollection(false)
                    .WithIsGlobal(false)
                    .GetObject(),
                new SystemParameterBuilder("HostHmiUrl", "http://localhost:7439")
                    .WithDescription("URL address for accessing  USC Export-Import UI.")
                    .WithIsRequired(true)
                    .WithIsCollection(false)
                    .WithIsGlobal(false)
                    .WithDataTypeId(DataTypeEnum.String)
                    .GetObject(),

            };
        }
    }
}
