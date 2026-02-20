namespace EvolutionaryArchitecture.Fitnet.Passes
{
    using Data.Database;
    using Passes.Application;

    internal static class PassesModule
    {
        internal static IServiceCollection AddPasses(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddScoped<IPassService, Passes.Infrastructure.PassService>();
            return services;
        }

        internal static IApplicationBuilder UsePasses(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseDatabase();
            return applicationBuilder;
        }
    }
}
