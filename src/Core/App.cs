using Draws.CLI;
using SpotifyAPI.Web;
using SpotifyCLI.Services;
using SpotifyCLI.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyCLI {
    public class App {
        private readonly IOutputHandler _outputHandler;
        private readonly CommandParser _parser;

        public App (CommandParser parser, IOutputHandler outputHandler) {
            _outputHandler = outputHandler;
            _parser = parser;
        }

        public void Run(string[] args) {
            _parser.Parse(args);
        }
    }
}