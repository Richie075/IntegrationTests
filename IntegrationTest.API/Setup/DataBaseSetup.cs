using Autofac;
using Autofac.Configuration;
using System;
using System.IO;
using System.Reflection;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Laetus.NT.Core.Persistence.Test.TestApi;
using Laetus.NT.Core.PersistenceApi.DataModel.conf;
using Laetus.NT.Core.PersistenceApi.Implementations;
using Laetus.NT.Core.PersistenceApi.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using TechTalk.SpecFlow.Infrastructure;
using ContainerBuilder = Autofac.ContainerBuilder;

namespace IntegrationTest.API.Setup
{
    public class DataBaseSetup
    {
        private readonly string _autofacConfigFile = "autofacconfigWithTestLogger.json";
        public IPersistenceProvider Persistence { get; private set; }

        public DataBaseSetup()
        {
            //Environment.SetEnvironmentVariable("DOCKER_HOST", "npipe://./pipe/docker_engine");
            Environment.SetEnvironmentVariable("DOCKER_HOST", "tcp://localhost:2375");
        }

        private TestcontainersContainer _dbContainer;

        //private async Task CreateDocker()
        //{
        //    DockerClient client = new DockerClientConfiguration()
        //        .CreateClient();
        //    var dockerFile = GetResourceFile("Dockerfile");
        //    var imageFromFile = new ImageFromDockerfileBuilder().WithDockerfile(dockerFile).Build();
        //    var image = client.Images.CreateImageAsync(
        //            new ImagesCreateParameters
        //            {
        //                FromImage = "mcr.microsoft.com/mssql/server",
        //                Tag = "2022-latest"
        //            },
        //            null,
        //            new Progress<JSONMessage>()
        //        );
        //}
        public async Task CreateDataBase()
        {
            try
            {
                //CreateDocker();
                var builder =
                    new TestcontainersBuilder<TestcontainersContainer>()
                        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                        .WithDockerEndpoint("npipe://./pipe/docker_engine")
                        .WithName(Guid.NewGuid().ToString("D"))
                        .WithEnvironment("SA_PASSWORD", "PaSSw0rd_04")
                        .WithEnvironment("ACCEPT_EULA", "true")
                        .WithEnvironment("MSSQL_TCP_PORT", "1433")
                        .WithEnvironment("MSSQL_PID", "Developer")
                        .WithPortBinding(1633, 1433)
                        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
                        ;
                //var _ = await 
                //        new ImageFromDockerfileBuilder()
                //            .WithName(Guid.NewGuid().ToString("D"))
                //            .WithDockerfileDirectory(CommonDirectoryPath.GetSolutionDirectory(), "Setup/Docker/MSSQL")
                //            .WithDockerfile("Dockerfile").Build();
                //    ;
                //    _.
                _dbContainer = builder.Build();
                await _dbContainer.StartAsync();

                Persistence = CreateProvider(true);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                await _dbContainer.StopAsync();
            }
        }

        
        public async Task DeleteDataBase()
        {
            TestCleanup(true);
            await _dbContainer.StopAsync();
        }

        private IPersistenceProvider CreateProvider(bool deleteExisting)
        {
            Persistence ??= GetPersistenceProvider();

            var dataBaseExists = CheckIfDatabaseExists(Persistence);
            if (dataBaseExists && deleteExisting)
            {
                Persistence.Delete();
                dataBaseExists = false;
            }
            if (!dataBaseExists)
            {
                Persistence.Create();
                Persistence.Migrate();
            }

            return Persistence;
        }

        private void TestCleanup(bool force)
        {
            TestContext.WriteLine($"DB CleanUp forced: {force} for config: {_autofacConfigFile}");
            var persistenceProvider = GetPersistenceProvider();
            var dataBaseExists = CheckIfDatabaseExists(persistenceProvider);
            if (dataBaseExists && force)
            {
                TestContext.WriteLine($"Deleting DB : {force} for config: {_autofacConfigFile}");
                persistenceProvider.Delete();
            }
        }

        private IPersistenceProvider GetPersistenceProvider()
        {
            var logPath = Path.Combine(typeof(DataBaseSetup).Assembly.Location, "log");
            var logger = new TestLogger(logPath);
            var pers = new PersistenceProvider(
                new DbAccessProviders(new SimpleCrudProvider(logger), new StoredProcedureProvider(), new Versioning(logger)),
                Laetus.NT.Core.PersistenceApi.Enumerations.DataBaseType.MsSqlServer,
                "Data Source=localhost,1633;Initial Catalog=TestPlant_2.0;User ID=sa;Password=PaSSw0rd_04; MultipleActiveResultSets=True",
                logger
                );
            //var config = new ConfigurationBuilder();
            //var fileName = GetResourceFile(_autofacConfigFile);
            //config.AddJsonFile(fileName);
            //var module = new ConfigurationModule(config.Build());
            //var builder = new ContainerBuilder();
            //builder.RegisterModule(module);
            //var Container = builder.Build();
            //using var scope = Container.BeginLifetimeScope();
            //var persistenceProvider = scope.Resolve<IPersistenceProvider>();
            return pers;
        }

        private bool CheckIfDatabaseExists(IPersistenceProvider persistenceProvider)
        {
            using var connection = new SqlConnection(persistenceProvider.Connector.ConnectionString);
            using (var command = new SqlCommand("SELECT db_id(@databaseName)", connection))
            {
                command.Parameters.Add(new SqlParameter("databaseName", persistenceProvider.Connector.DataBaseName));
                try
                {
                    connection.Open();

                    var result = command.ExecuteScalar();
                    connection.Close();
                    return result != DBNull.Value;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        private string GetResourceFile(string resName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceNames().Single(str => str.EndsWith(resName));
            //string fileContent = String.Empty;
            //using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    fileContent = reader.ReadToEnd();
            //}
            //return fileContent;
        }
    }

    [TestFixture]
    public class SeedDataBase : DataBaseSetup
    {
        [SetUp]
        public void SeedData()
        {

        }

        [TearDown]
        public void DeleteSeedData()
        {}
    }
}
