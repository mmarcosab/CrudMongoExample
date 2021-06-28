using Exemplo.Infrastructure.MongoDB.Configurations;
using Exemplo.Infrastructure.MongoDB.Repositories;
using Exemplo.Infrastructure.MongoDB.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Net.Sockets;

namespace Exemplo.Infrastructure.MongoDB.Installers
{ 
    internal static class MongoDBInstaller
    {
        internal static IServiceCollection AddRepository(this IServiceCollection services, MongoDBConfiguration configuration)
        {
            services.AddSingleton<ICustomerRepository, CustomerRepository>(); //injetando dependencias
            services.AddMongoClient(configuration);
            return services;
        }

        internal static IServiceCollection InstallMongoDB(this IServiceCollection services, Action<MongoDBConfiguration> configuration)
        {
            var mongoDBSettings = new MongoDBConfiguration();
            configuration(mongoDBSettings);
            services.AddSingleton(mongoDBSettings);
            services.AddRepository(mongoDBSettings);
            return services;
        }
        private static IServiceCollection AddMongoClient(this IServiceCollection services, MongoDBConfiguration configuration)
        {
            //mantem o ciclo de vida por todo o processamento
            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(configuration.ConnectionString));
                mongoClientSettings.MaxConnectionIdleTime = TimeSpan.FromSeconds(configuration.MaxConnectionIdleTime);
                mongoClientSettings.ClusterConfigurator = Configure();
                var mongoClient = new MongoClient(mongoClientSettings);
                return mongoClient;
            });
           return services;
        }

        private static Action<ClusterBuilder> Configure() => builder => 
        {
            builder.ConfigureTcp(configurator => configurator.With(socketConfigurator: (Action<Socket>)SocketConfigurator));
        };

        private static void SocketConfigurator(Socket socket) => socket.SetSocketOption(SocketOptionLevel.Socket,
            SocketOptionName.KeepAlive, true);

    }
}