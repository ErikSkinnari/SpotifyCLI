using Draws.CLI;
using Microsoft.Extensions.DependencyInjection;
using SpotifyAPI.Web;
using SpotifyCLI.Commands;
using SpotifyCLI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpotifyCLI.Utilities {
    public static class ServiceCollectionExtensions {
        public static void AddSpotifyClient(this IServiceCollection services) {
            services.AddSingleton<ISpotifyClient>(_ => CreateSpotifyClient(_).Result);
        }

        public static void AddSpotifyCLICommands(this IServiceCollection services) {
            services.AddTransient<IEnumerable<ICommand>>(_ => GetCommands(_));
        }

        private static async Task<ISpotifyClient> CreateSpotifyClient(IServiceProvider serviceProvider) {
            var authService = serviceProvider.GetRequiredService<IAuthService>();
            var client = await authService.CreateSpotifyClientAsync();

            return client;
        }

        private static IEnumerable<ICommand> GetCommands(IServiceProvider serviceProvider) {
            List<ICommand> commands = new List<ICommand>();
            var spotifyClient = (ISpotifyClient)serviceProvider.GetService<ISpotifyClient>();

            commands.Add(new ChangePlayStatusCommand(spotifyClient));
            commands.Add(new NextCommand(spotifyClient));
            commands.Add(new PreviousCommand(spotifyClient));
            commands.Add(new PlayCommand(spotifyClient));
            commands.Add(new PauseCommand(spotifyClient));

            return commands;
        }
    }
}