using Microsoft.Extensions.DependencyInjection;
using UITests.Drivers.Implementation;

namespace UITests.Drivers
{
    internal static class Bootstrapper
    {
        public static IServiceCollection RegisterDrivers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAppiumDriver, LocalAppiumDriver>();
            return serviceCollection;
        }
    }
}