using Laetus.NT.Core.Persistence.Test.TestUtils;
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
