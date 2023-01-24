using Laetus.NT.Core.Persistence.Test.TestApi;
using Laetus.NT.Core.PersistenceApi.Implementations;
using Laetus.NT.Core.PersistenceApi.Interfaces;
using Microsoft.Data.SqlClient;

namespace IntegrationTest.API.Setup
{
    public static class PersistenceInitializer
    {
        public static IPersistenceProvider CreateProvider(string dbName)
        {
            var persistence = GetPersistenceProvider(dbName);

            var dataBaseExists = CheckIfDatabaseExists(persistence);
            if (dataBaseExists)
            {
                persistence.Delete();
            }

            persistence.Create();
            persistence.Migrate();

            return persistence;
        }
        public static  IPersistenceProvider GetPersistenceProvider(string dbName)
        {
            var logPath = Path.Combine(typeof(DataBaseSetup).Assembly.Location, "log");
            var logger = new TestLogger(logPath);
            var pers = new PersistenceProvider(
                new DbAccessProviders(new SimpleCrudProvider(logger), new StoredProcedureProvider(), new Versioning(logger)),
                Laetus.NT.Core.PersistenceApi.Enumerations.DataBaseType.MsSqlServer,
                $"Data Source=localhost,1633;Initial Catalog={dbName};User ID=sa;Password=PaSSw0rd_04; MultipleActiveResultSets=True;;TrustServerCertificate=True;Encrypt=False",
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

        public static  bool CheckIfDatabaseExists(IPersistenceProvider persistenceProvider)
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
    }
}
