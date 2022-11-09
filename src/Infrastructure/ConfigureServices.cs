using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using BadBroker.Application.Common.Interfaces;
using BadBroker.Infrastructure.Services;
using BadBroker.Application.Commands;
using BadBroker.Application.Commands.GetBestRate;
using BadBroker.Domain.Entities;
using BadBroker.Infrastructure.Options;

namespace BadBroker.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ExchangeRatesDataApiOptions>(
                configuration.GetSection(nameof(ExchangeRatesDataApiOptions)));

            services.AddHttpClient();
            services.AddTransient<IRateData, RateDataService>();
            services.AddTransient<ICommandHandler<GetBestRateCommand, BestRate>, GetBestRateCommandHandler>();

            return services;
        }
    }
}
