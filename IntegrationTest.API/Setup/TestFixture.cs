
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Laetus.NT.Core.PersistenceApi.Context;

namespace IntegrationTest.API.Setup
{
    public class TestFixture
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;

        private string _dockerContainerId;
        private string _dockerSqlPort;

        public async Task RunBeforeAnyTests()
        {

            (_dockerContainerId, _dockerSqlPort) = await DockerSqlDatabaseUtilities.EnsureDockerStartedAndGetContainerIdAndPortAsync();
            var dockerConnectionString = DockerSqlDatabaseUtilities.GetSqlConnectionString(_dockerSqlPort);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "UseInMemoryDatabase", "false" },
                    { "ConnectionStrings:PersistenceContext", dockerConnectionString }
                });
                //.AddEnvironmentVariables();

            _configuration = builder.Build();

            //var startup = new Startup(_configuration, _env);

            var services = new ServiceCollection();

            services.AddLogging();

            //startup.ConfigureServices(services);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();



            EnsureDatabase();
        }

        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<PersistenceContext>();

            context.Database.Migrate();
        }



        public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<PersistenceContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<PersistenceContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PersistenceContext>();

            try
            {
                await action(scope.ServiceProvider);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PersistenceContext>();

            try
            {
                var result = await action(scope.ServiceProvider);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Task ExecuteDbContextAsync(Func<PersistenceContext, Task> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<PersistenceContext>()));

        public static Task ExecuteDbContextAsync(Func<PersistenceContext, ValueTask> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<PersistenceContext>()).AsTask());

  
        public static Task<T> ExecuteDbContextAsync<T>(Func<PersistenceContext, Task<T>> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<PersistenceContext>()));

        public static Task<T> ExecuteDbContextAsync<T>(Func<PersistenceContext, ValueTask<T>> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<PersistenceContext>()).AsTask());

 
        public static Task<int> InsertAsync<T>(params T[] entities) where T : class
        {
            return ExecuteDbContextAsync(db =>
            {
                foreach (var entity in entities)
                {
                    db.Set<T>().Add(entity);
                }
                return db.SaveChangesAsync();
            });
        }

        [OneTimeTearDown]
        public Task RunAfterAnyTests()
        {
            return Task.CompletedTask;
        }
    }
}
