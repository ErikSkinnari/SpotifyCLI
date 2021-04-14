
using Draws.CLI;
using SpotifyAPI.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpotifyCLI.Commands {
    [Command("playback", "Toggles the current player between play and pause", true)]
    public class ChangePlayStatusCommand : ICommand {
        private readonly ISpotifyClient _spotifyClient;
        private CurrentlyPlayingContext _currentlyPlaying;

        public ChangePlayStatusCommand(ISpotifyClient spotifyClient) {
            _spotifyClient = spotifyClient;

            GetCurrentlyPlaying().Wait();
        }

        private async Task GetCurrentlyPlaying() {
            _currentlyPlaying = await _spotifyClient.Player.GetCurrentPlayback();
        }

        public string RunCommand() {
            if (_currentlyPlaying.IsPlaying)
                _spotifyClient.Player.PausePlayback().Wait();
            else if (!_currentlyPlaying.IsPlaying)
                _spotifyClient.Player.ResumePlayback().Wait();

            return (_currentlyPlaying.IsPlaying) ? "Paused" : "Playing";
        }

        // This command requires no arguments, so this is unused
        public void SetArguments(Dictionary<string, string> args) {
            // throw new System.NotImplementedException();
        }
    }
}