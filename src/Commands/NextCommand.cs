using Draws.CLI;
using SpotifyAPI.Web;
using System.Collections.Generic;

namespace SpotifyCLI.Commands {
    [Command("next", "Skips forward to the next track", isSingleArgument: true)]
    public class NextCommand : ICommand {
        private ISpotifyClient _spotify;

        public NextCommand(ISpotifyClient spotifyClient) {
            _spotify = spotifyClient;
        }

        public string RunCommand() {
            _spotify.Player.SkipNext();
            return $"Skipped forward";
        }

        public void SetArguments(Dictionary<string, string> args) {
        }
    }
}