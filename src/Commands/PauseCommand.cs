using System.Collections.Generic;
using Draws.CLI;
using SpotifyAPI.Web;

namespace SpotifyCLI.Commands {
    [Command("pause", "Sets the playback-status to pause.", isSingleArgument: true)]
    public class PauseCommand : ICommand {
        private readonly ISpotifyClient _spotify;

        public PauseCommand(ISpotifyClient spotifyClient) {
            _spotify = spotifyClient;
        }

        public string RunCommand() {
            bool isSuccess = _spotify.Player.PausePlayback().Result;

            return (isSuccess) ? "Playback paused" : "Could not pause playback";
        }

        public void SetArguments(Dictionary<string, string> args) {
        }
    }
}
