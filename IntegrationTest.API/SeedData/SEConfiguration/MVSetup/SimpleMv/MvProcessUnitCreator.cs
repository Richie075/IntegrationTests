using IntegrationTest.API.DataFactory.Agents;
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.pd;

namespace IntegrationTest.API.SeedData.SEConfiguration.MVSetup.SimpleMv
{
    internal class MvProcessUnitCreator : ModelCreator<ProcessUnit>
    {
        private readonly string _name;

        private readonly int _level;

        public MvProcessUnitCreator(string name, int level)
        {
            _name = name;
            _level = level;
        }
        internal override ProcessUnit ConstructModel(string prefix = "")
        {
            return new ProcessUnitBuilder()
                .WithName($"{prefix}{_name}")
                .WithLevelId(_level)
                .GetObject();
        }
    }
}
