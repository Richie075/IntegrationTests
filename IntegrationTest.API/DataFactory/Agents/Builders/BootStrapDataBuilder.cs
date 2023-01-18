using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.DataModel.dr;
using Laetus.NT.Core.PersistenceApi.Enumerations;

namespace IntegrationTest.API.DataFactory.Agents.Builders
{
    internal class BootStrapDataBuilder: TestDataBuilder<DRBootstrapData>
    {
        public BootStrapDataBuilder(DeviceDriver deviceDriver)
        {
            testObject.DRDeviceDriverID = (int)deviceDriver;
        }
    }
}
