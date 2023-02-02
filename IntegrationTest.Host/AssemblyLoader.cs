using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Host
{
    [Serializable]
    public class AssemblyLoader : MarshalByRefObject
    {
        private string ApplicationBase { get; set; }

        public AssemblyLoader()
        {
            ApplicationBase = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
            AppDomain.CurrentDomain.AssemblyResolve += Resolve;
        }

        private Assembly Resolve(object sender, ResolveEventArgs args)
        {
            AssemblyName assemblyName = new AssemblyName(args.Name);
            string fileName = string.Format("{0}.dll", assemblyName.Name);
            return Assembly.LoadFile(Path.Combine(ApplicationBase, fileName));
        }
    }
}
