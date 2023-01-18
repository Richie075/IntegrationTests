using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.pd;

namespace IntegrationTest.API.DataFactory.Agents.Creators.SeSetupCreators.SimpleMv
{
    internal class MvProcessUnitCreator : ModelCreator<ProcessUnit>
    {
        private string _name;

        private int _level;

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
