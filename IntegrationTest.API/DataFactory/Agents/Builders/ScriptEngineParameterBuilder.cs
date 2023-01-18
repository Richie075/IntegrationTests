using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laetus.NT.Core.Persistence.Test.TestUtils;
using Laetus.NT.Core.PersistenceApi.DataModel.wf;

namespace IntegrationTest.API.DataFactory.Agents.Builders
{
    public class ScriptEngineParameterBuilder : TestDataBuilder<ScriptEngineParameters>
    {
        public ScriptEngineParameterBuilder()
        {}

        public ScriptEngineParameterBuilder WithDebugEnabled(bool debugEnabled)
        {
            testObject.DebugEnabled = debugEnabled;
            return this;
        }

        public ScriptEngineParameterBuilder WithWaitForDebugger(bool waitForDebugger)
        {
            testObject.WaitForDebugger = waitForDebugger;
            return this;
        }
        public ScriptEngineParameterBuilder WithRemoteDebugEnabled(bool remoteDebugEnabled)
        {
            testObject.RemoteDebugEnabled = remoteDebugEnabled;
            return this;
        }
        public ScriptEngineParameterBuilder WithDebugPort(ushort debugPort)
        {
            testObject.DebugPort = debugPort;
            return this;
        }
        public ScriptEngineParameterBuilder WithModuleSearchPath(string moduleSearchPath)
        {
            testObject.ModuleSearchPath = moduleSearchPath;
            return this;
        }
    }
}
