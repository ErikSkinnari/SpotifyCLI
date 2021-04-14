using Draws.CLI;
using SpotifyAPI.Web;
using System.Collections.Generic;

namespace SpotifyCLI.Commands {
    [Command("skip", "Skip either backwards or forwards")]
    [Argument("fwd", "Go to next", isFlag: true, shortName: 'f')]
    [Argument("bck", "Go to previous", isFlag: true, shortName: 'b')]
    public class SkipCommand : ICommand {
        private JumpDirection _direction;
        private ISpotifyClient _spotify;

        public SkipCommand(ISpotifyClient spotifyClient) {
            _spotify = spotifyClient;
        }

        public string RunCommand() {
            string direction; 

            switch (_direction) {
                case JumpDirection.Forward: 
                    _spotify.Player.SkipNext().Wait();
                    direction = "forward";
                    break;

                case JumpDirection.Back: 
                    _spotify.Player.SkipPrevious().Wait();
                    direction = "backward";
                    break;

                default: 
                    direction = "nowhere. Something went wrong :(";
                    break;
            }

            return $"Skipped {direction}";
        }

        public void SetArguments(Dictionary<string, string> args) {
            string forwardValue = "";
            string backValue = "";
            if (args.TryGetValue("fwd", out forwardValue) && forwardValue == "true")
                _direction = JumpDirection.Forward;
            else if (args.TryGetValue("bck", out backValue) && backValue == "true")
                _direction = JumpDirection.Back;
        }

        private enum JumpDirection {
            Forward,
            Back,
        }
    }
}