using Draws.CLI;
using SpotifyAPI.Web;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyCLI.Commands {
    [Command("search", "Searches for a specified query etc")]
    [Argument("query", "The query to search with", false, true, 'q')]
    [Argument("track", "Searches for only tracks", true, false)]
    public class SearchCommand : ICommand {
        private Dictionary<string, string> _arguments;
        private readonly ISpotifyClient _spotify;

        public SearchCommand(ISpotifyClient spotify) {
            _spotify = spotify;
        }

        public string RunCommand() {
            string query = _arguments.FirstOrDefault(x => x.Key == "query").Value;
            SearchRequest.Types types = SearchRequest.Types.All;

            if (_arguments.ContainsKey("track")) {
                types = SearchRequest.Types.Track;
            }

            SearchRequest request = new SearchRequest(types, query);

            var res = _spotify.Search.Item(request).Result;

            string trackNames = "";

            foreach (var track in res.Tracks.Items) {
                trackNames += $"{track.Name}\n";
            }

            return trackNames;
        }

        public void SetArguments(Dictionary<string, string> args) {
            _arguments = args;
        }
    }
}