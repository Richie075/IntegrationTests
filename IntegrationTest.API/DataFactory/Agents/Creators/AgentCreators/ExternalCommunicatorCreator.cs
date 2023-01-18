using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.Enumerations;

namespace IntegrationTest.API.DataFactory.Agents.Creators.AgentCreators
{
    public class ExternalCommunicatorCreator : AgentCreator
    {
        internal override Agent ConstructModel(string prefix = "")
        {
            return new AgentBuilder()
                .WithName($"{prefix}External Communicator")
                .WithAgentType(0)
                .WithAssemblyName("Laetus.NT.Core.EC.Agent")
                .WithInstanceId(Guid.NewGuid())
                .GetObject();
        }

        internal override IList<SystemParameter> ConstructSystemParameter()
        {
            return new List<SystemParameter>
            {
                new SystemParameterBuilder("HostHmiUrl", "http://localhost:55555").WithDataTypeId(DataTypeEnum.String).GetObject(),
                new SystemParameterBuilder("SdeTransferTimeout", "60000").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("ChunkSize", "5000").WithDataTypeId(DataTypeEnum.Int32).GetObject(),
                new SystemParameterBuilder("CreateExportTransactionSqlTimeout", "900000").WithDataTypeId(DataTypeEnum.Int32).WithMinValue(600000).GetObject(),
                new SystemParameterBuilder("CpmMasterDataCodeReorderQuantity", "300").WithDataTypeId(DataTypeEnum.Int32).WithDescription("Defines reorder quantity for each Master Data created trough EC.").GetObject(),
                new SystemParameterBuilder("LineVariantPropertyName", "Line_Variant").WithDataTypeId(DataTypeEnum.String).WithDescription("This parameter prepresents property key in EC Material DTO which represents LineVariant.").GetObject(),
                new SystemParameterBuilder("CpmUseReusableCodePatternAsDefault", "true").WithDataTypeId(DataTypeEnum.Boolean).WithDescription("Propagated functionality from CPM. Defines whether reusable code pattern would be used or PO Specific Code Pattern.").GetObject(),
                new SystemParameterBuilder("PoStartupTimeout", "200000").WithDataTypeId(DataTypeEnum.Int32).WithDescription("A timeout in milliseconds to wait for an order to start, before assuming it has failed to start.").GetObject(),
                new SystemParameterBuilder("OrderManufacturingDatePropertyName", "Bulk1MfgDate").WithDataTypeId(DataTypeEnum.String).WithDescription("This parameter defines which key is used in PO.AdditionalInfo that maps to ManufacturingDate from EC Order DTO.").GetObject(),
                new SystemParameterBuilder("MasterDataEditability", "false").WithDataTypeId(DataTypeEnum.Boolean).WithDescription("This parameter covers certain scenarios, in which it is necessary to give the User the possibility to change some Product Master Data fields even if one or more Packaging Orders based on that Product have been created and downloaded to the target Line. If this flag is set to true Master Data field can be changed, including any Additional Info.").GetObject(),
                new SystemParameterBuilder("LineCodeThreshold", "300").WithDataTypeId(DataTypeEnum.Int32).WithDescription("The default value to set for the code threshold when creating a PO. The Line agent will maintain the specified number of codes for this PO for each packaging level when the PO is downloaded to the line.").GetObject(),
                new SystemParameterBuilder("CpmCodePatternIDForPOSpecificCodes", "3").WithDataTypeId(DataTypeEnum.Int32).WithDescription("Defines GUID of PO specific code pattern for each Master Data created trough EC.").GetObject(),
                new SystemParameterBuilder("LineCodeReorderQuantity", "300").WithDataTypeId(DataTypeEnum.Int32).WithDescription("The default value to set for the code reorder quantity when creating a PO. The Line agent will request this amount of code once the number of codes are below threshold").GetObject(),
                new SystemParameterBuilder("CpmCodePatternIDForReusableCodes", "2").WithDataTypeId(DataTypeEnum.Int32).WithDescription("Defines GUID of Reusable code pattern for each Master Data created trough EC.").GetObject(),
                new SystemParameterBuilder("CodeRecyclingRequired", "false").WithDataTypeId(DataTypeEnum.Boolean).WithDescription("If set to true, codes shall be recycled after PO closure at Line").GetObject(),
                new SystemParameterBuilder("CpmMasterDataCodeThreshold", "1000").WithDataTypeId(DataTypeEnum.Int32).WithDescription("Defines threshold for each Master Data created trough EC.").GetObject(),
                new SystemParameterBuilder("CpmCodeSupplierID", "2").WithDataTypeId(DataTypeEnum.Int32).WithDescription("Defines ID of code supplier for each Master Data created trough EC.").GetObject(),
                new SystemParameterBuilder("CpmTestModeCodeSupplierID", "2").WithDataTypeId(DataTypeEnum.Int32).WithDescription("Defines ID of code supplier for each Master Data created trough EC, that is used for Test POs.").GetObject(),
                new SystemParameterBuilder("MasterDataUniqueness", "true").WithDataTypeId(DataTypeEnum.Boolean).WithDescription("If this parameter is true the uniqueness of the ProductId in the scope of the additional id/material number will be checked. Otherwise only the ProductId has to be unique.").GetObject(),
                new SystemParameterBuilder("LocaleId", "en-US").WithDataTypeId(DataTypeEnum.String).WithDescription("Default language used for translations.").GetObject(),
                new SystemParameterBuilder("AutoDownloadToLine", "false").WithDataTypeId(DataTypeEnum.Boolean).WithDescription("Defines whether PO will be automatically downloaded to the line upon the approval trough Level 4 or not.").GetObject(),
                new SystemParameterBuilder("TransactionSerialCodesMaxCount", "5000").WithDataTypeId(DataTypeEnum.Int32).WithDescription("The maximum number of codes transported in one EC code export transaction.").GetObject(),
                new SystemParameterBuilder("EnableWebApiMethodLogging", "false").WithDataTypeId(DataTypeEnum.Boolean).WithDescription("Debug setting.").GetObject(),
                new SystemParameterBuilder("EcProductTypeMappings", "CONST:GTIN, USC:GTIN, RANDUSC:GTIN, USCCN:IPC, NSC:NTIN, PSC:PPN, SSCC:IPC, OC:IPC, RSC:GTIN, RANDRSC:GTIN").WithDataTypeId(DataTypeEnum.String).WithIsCollection(true).WithDescription("CSV string of available EC-UP product type mappings.").GetObject()
            };
        }
    }
}
