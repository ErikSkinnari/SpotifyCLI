using Draws.CLI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpotifyCLI.Commands;
using SpotifyCLI.Services;
using SpotifyCLI.Utilities;
using System.Threading.Tasks;

namespace SpotifyCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Find a way to inject a list/array of commands into the CommandParser
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((Context, services) => {
                    services.AddScoped<App>();
                    services.AddScoped<IAuthService, AuthService>();
                    services.AddScoped<CommandParser>();

                    services.AddSingleton<IAppConfig, AppConfig>();
                    services.AddSingleton<IOutputHandler, CommandOutputHandler>();

                    services.AddSpotifyClient();
                    services.AddSpotifyCLICommands();
                })
                .Build();

            var app = ActivatorUtilities.CreateInstance<App>(host.Services);

            app.Run(args);
        }
    }
}