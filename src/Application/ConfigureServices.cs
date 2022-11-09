using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

using BadBroker.Application.Calculator;
using BadBroker.Application.Commands.GetBestRate;

namespace BadBroker.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IBestRateCalculator, BestRateCalculator>();
            services.AddScoped<IValidator<GetBestRateCommand>, GetBestRateCommandValidator>();

            return services;
        }
    }
}