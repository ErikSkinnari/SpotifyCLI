using SpotifyAPI.Web;
using SpotifyCLI.Services;
using SpotifyCLI.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyCLI {
    public class App {
        private readonly IAuthService _authService;
        private readonly IOutputHandler _outputHandler;
        private ISpotifyClient _spotify;

        public App (IAuthService authService, IOutputHandler outputHandler) {
            _authService = authService;
            _outputHandler = outputHandler;
        }

        public async Task Run() {
            _spotify = await _authService.SetSpotifyClientAsync(_spotify);
            var result = await _spotify.Search.Item(new SearchRequest(SearchRequest.Types.All, "Never gonna give you up"));
            var track = result.Tracks.Items.First();

            _outputHandler.Output(track.Name);
        }
    }
}