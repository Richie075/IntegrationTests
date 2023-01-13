using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.DataModel.md;
using Laetus.NT.Core.PersistenceApi.DataModel.pd;
using Laetus.NT.Core.PersistenceApi.Interfaces;

namespace IntegrationTest.API.Setup.Seeding
{
    internal class DataSeeder
    {
        private IPersistenceProvider _persistenceProvider;

        public DataSeeder(IPersistenceProvider persistenceProvider)
        {
            _persistenceProvider = persistenceProvider ?? throw new ArgumentNullException(nameof(persistenceProvider));
        }

        public DataSeeder WithAgent(Agent agent)
        {
            _persistenceProvider.CrudProvider.Create(agent);
            return this;
        }

        public DataSeeder WithMasterData(MasterData masterData)
        {
            _persistenceProvider.CrudProvider.Create(masterData);
            return this;
        }
        public DataSeeder WithPo(PO po)
        {
            _persistenceProvider.CrudProvider.Create(po);
            return this;
        }
    }
}
