using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;

namespace IntegrationTest.API.DataFactory.Agents.Creators.SeSetupCreators.SimpleMv
{
    public class MvPackagingTopologyCreator : ModelCreator<PackagingTopology>
    {
        private readonly int _countryId;
        private readonly int _levelId;
        public MvPackagingTopologyCreator(int countryId, int levelId)
        {
            _countryId = countryId;
            _levelId = levelId;
        }
        internal override PackagingTopology ConstructModel(string prefix = "")
        {
            var pt = new PackagingTopologyBuilder()
                .WithName($"{prefix} MV")
                .WithCountryId(_countryId)
                .GetObject();
            pt.RootPackagingNodeNavigation = new PackagingNodeBuilder()
                .WithColumns(1)
                .WithRows(1)
                .WithLayers(1)
                .WithAutomaticPrintout(false)
                .WithLabelCount(1)
                .WithProcessType(1)
                .WithCameraInspectionMode(0)
                .WithPartialAggregation(0)
                .WithLevelId(_levelId)
                .WithChildren(new List<PackagingNode>
                {
                    new PackagingNodeBuilder()
                        .WithColumns(1)
                        .WithRows(1)
                        .WithLayers(1)
                        .WithAutomaticPrintout(false)
                        .WithLabelCount(1)
                        .WithProcessType(1)
                        .WithCameraInspectionMode(1)
                        .WithPartialAggregation(0)
                        .WithLevelId(_levelId)
                        .GetObject()
                })
                .GetObject();

            return pt;
        }
    }
}
