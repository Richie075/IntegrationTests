using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;

namespace IntegrationTest.API.DataFactory.Agents.Builders
{
    public class PhysicalTopologyHostBuilder : TestDataBuilder<PhysicalTopologyHost>
    {
        public PhysicalTopologyHostBuilder(string name)
        {
            testObject.Name = name;
        }

        public PhysicalTopologyHostBuilder WithInstanceId(Guid guid)
        {
            testObject.InstanceId = guid;
            return this;
        }
        public PhysicalTopologyHostBuilder WithSubType(string guid)
        {
            testObject.SubType = guid;
            return this;
        }

        public PhysicalTopologyHostBuilder WithHostName(string hostName)
        {
            testObject.Hostname = hostName;
            return this;
        }

        public PhysicalTopologyHostBuilder WithIpAddress(string ipAddress)
        {
            testObject.IpAddress = ipAddress;
            return this;
        }

        public PhysicalTopologyHostBuilder WithPorts(string ports)
        {
            testObject.Ports = ports;
            return this;
        }

        public PhysicalTopologyHostBuilder WithMacAddress(string macAddress)
        {
            testObject.MacAddress = macAddress;
            return this;
        }

        public PhysicalTopologyHostBuilder WithAgents(IList<PhysicalTopologyAgent> physicalTopologyAgents)
        {
            testObject.PhysicalTopologyAgent = physicalTopologyAgents;
            return this;
        }

        public PhysicalTopologyHostBuilder WithParameters(IList<PhysicalTopologyHostParameters> physicalTopologyHostParameters)
        {
            testObject.PhysicalTopologyHostParameters = physicalTopologyHostParameters;
            return this;
        }

        public PhysicalTopologyHostBuilder WithPhysicalTopology(PhysicalTopology physicalTopology)
        {
            testObject.PhysicalTopology = physicalTopology;
            return this;
        }
    }
}
