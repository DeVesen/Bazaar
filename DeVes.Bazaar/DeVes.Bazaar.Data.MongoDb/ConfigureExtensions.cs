using System;
using DeVes.Bazaar.Data.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DeVes.Bazaar.Data.MongoDb
{
    public static class ConfigureExtensions
    {
        public static void AddMongoDbRepository(this IServiceCollection services, string connectionString, string databaseName, Action<IRepository> repositoryInitialize = null)
        {
            services.AddSingleton<ISellerRepository>(_ =>
            {
                var repo = new SellerRepository(GetMongoDbBazaarDatabase(connectionString, databaseName));
                repositoryInitialize?.Invoke(repo);
                return repo;
            });
            services.AddSingleton<IArticleRepository>(_ =>
            {
                var repo = new ArticleRepository(GetMongoDbBazaarDatabase(connectionString, databaseName));
                repositoryInitialize?.Invoke(repo);
                return repo;
            });
            services.AddSingleton<IManufacturerRepository>(_ =>
            {
                var repo = new ManufacturerRepository(GetMongoDbBazaarDatabase(connectionString, databaseName));
                repositoryInitialize?.Invoke(repo);
                return repo;
            });
            services.AddSingleton<ICategoryRepository>(_ =>
            {
                var repo = new CategoryRepository(GetMongoDbBazaarDatabase(connectionString, databaseName));
                repositoryInitialize?.Invoke(repo);
                return repo;
            });
        }

        private static IMongoDatabase GetMongoDbBazaarDatabase(string connectionString, string databaseName)
        {
            return new MongoClient(connectionString).GetDatabase(databaseName);
        }
    }
}
