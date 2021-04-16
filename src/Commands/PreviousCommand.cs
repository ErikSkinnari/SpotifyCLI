using System.Collections.Generic;
using Draws.CLI;
using SpotifyAPI.Web;

namespace SpotifyCLI.Commands {
    [Command("back", "Skips to the previous track", isSingleArgument: true)]
    public class PreviousCommand : ICommand {
        private readonly ISpotifyClient _spotify;

        public PreviousCommand(ISpotifyClient spotifyClient) {
            _spotify = spotifyClient;
        }

        public string RunCommand() {
            _spotify.Player.SkipPrevious();
            return "Skipped to previous track";
        }

        public void SetArguments(Dictionary<string, string> args) {
        }
    }
}