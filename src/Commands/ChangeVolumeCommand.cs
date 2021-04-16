using Draws.CLI;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;

namespace SpotifyCLI.Commands {
    [Command("vol", "Changes the volume.", isSingleArgument: false)]
    [Argument("volume", "The number to set the volume to", required: true, shortName: 'v')]
    public class ChangeVolumeCommand : ICommand {
        private readonly ISpotifyClient _spotify;
        private int _newVolume;

        public ChangeVolumeCommand(ISpotifyClient spotifyClient) {
            _spotify = spotifyClient;
        }

        public string RunCommand() {
            _spotify.Player.SetVolume(new PlayerVolumeRequest(_newVolume));

            return $"Set the volume to {_newVolume}%";
        }

        public void SetArguments(Dictionary<string, string> args) {
            string newVolumeString = "";
            args.TryGetValue("volume", out newVolumeString);

            _newVolume = Convert.ToInt32(newVolumeString);
        }
    }
}