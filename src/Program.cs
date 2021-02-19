using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpotifyAPI.Web;
using SpotifyCLI.Services;
using SpotifyCLI.Utilities;

namespace SpotifyCLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((Context, services) => {
                    services.AddScoped<App>();
                    services.AddScoped<IAuthService, AuthService>();

                    services.AddSingleton<IAppConfig, AppConfig>();
                    services.AddSingleton<IOutputHandler, ConsoleOutputHandler>();
                })
                .Build();

            var app = ActivatorUtilities.CreateInstance<App>(host.Services);

            await app.Run();
        }
    }
}