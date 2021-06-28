using Exemplo.Application.UseCases.GetCustomers;
using Exemplo.Application.UseCases.GetCustomers.Abstractions;
using Exemplo.Infrastructure.MongoDB.Configurations;
using Exemplo.Infrastructure.MongoDB.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exemplo.API.Installers
{
    public static class InstallerExtensions
    {
        public static IServiceCollection InstallServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGetCustomersUseCase, GetCustomersUseCase>();
            //injetar todos os usecases aqui
            services.InstallMongoDB(settings => configuration.Bind(nameof(MongoDBConfiguration), settings));
            return services;
        }
    }
}
