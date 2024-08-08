using System.Reflection;

namespace BookingsAPI.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            //  Create the automapper registration in the DI.
            services.AddAutoMapper(
                new List<Assembly?>
                {
                    Assembly.GetAssembly(typeof(AssemblyMarker)),
                    Assembly.GetAssembly(typeof(core.AssemblyMarker))
                },
                ServiceLifetime.Singleton);

            return services;
        }
    }
}
