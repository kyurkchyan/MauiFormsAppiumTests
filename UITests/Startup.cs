using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UITests.Configuration;
using UITests.Drivers;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace UITests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
        {
            SetupLaunchSettings();
            string environment = context.HostingEnvironment.EnvironmentName.ToLower();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                                               .AddJsonFile("appsettings.json", false)
                                               .AddJsonFile($"appsettings.{environment}.json", true)
                                               .AddJsonFile("appsettings.local.json", true)
                                               .AddEnvironmentVariables()
                                               .Build();

            services
                .AddSingleton<IConfiguration>(configuration)
                .RegisterDrivers()
                .AddTransient<ITestOutputHelper, TestOutputHelper>()
                .Configure<AutomationConfiguration>(automationConfiguration => configuration.GetSection("Automation").Bind(automationConfiguration));
        }

        private void SetupLaunchSettings()
        {
            string launchSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "Properties", "launchSettings.json");
            if(!File.Exists(launchSettingsPath))
            {
                return;
            }

            using StreamReader file = File.OpenText(launchSettingsPath);
            var reader = new JsonTextReader(file);
            JObject jObject = JObject.Load(reader);

            List<JProperty> variables = jObject
                                        .GetValue("profiles")!
                                        .SelectMany(profiles => profiles.Children())
                                        .SelectMany(profile => profile.Children<JProperty>())
                                        .Where(prop => prop.Name == "environmentVariables")
                                        .SelectMany(prop => prop.Value.Children<JProperty>())
                                        .ToList();

            foreach(JProperty variable in variables)
            {
                Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
            }
        }
    }
}