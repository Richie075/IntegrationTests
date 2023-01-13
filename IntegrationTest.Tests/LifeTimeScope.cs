using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationTest.API.Setup;
using NUnit.Framework;

namespace IntegrationTest.Tests
{
    [SetUpFixture]
    internal class LifeTimeScope : DataBaseSetup

    {
        public LifeTimeScope() : base()
        {}

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            await CreateDataBase();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await DeleteDataBase();
        }
    }
}
