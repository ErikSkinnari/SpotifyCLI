using System.Collections.Generic;
using Draws.CLI;
using SpotifyAPI.Web;

namespace SpotifyCLI.Commands {
    [Command("play", "Sets the current player-status to play.", isSingleArgument: true)]
    public class PlayCommand : ICommand {
        private ISpotifyClient _spotify;

        public PlayCommand(ISpotifyClient spotifyClient) {
            _spotify = spotifyClient;
        }

        public string RunCommand() {
            bool isSuccess = _spotify.Player.ResumePlayback().Result;

            return (isSuccess) ? "Playback resumed" : "Could not set status.";
        }

        public void SetArguments(Dictionary<string, string> args) {
        }
    }
}