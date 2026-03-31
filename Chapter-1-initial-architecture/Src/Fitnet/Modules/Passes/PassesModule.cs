namespace EvolutionaryArchitecture.Fitnet.Modules.Passes;

using EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;
using EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Infrastructure;
using EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Infrastructure.Sagas;

internal static class PassesModule
{
    internal static IServiceCollection AddPasses(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddScoped<IPassRepository, PassRepository>();
        services.AddScoped<ISagaRepository, SagaRepository>();
        services.AddHostedService<OutboxProcessor>();

        return services;
    }

    internal static IApplicationBuilder UsePasses(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDatabase();

        return applicationBuilder;
    }
}
