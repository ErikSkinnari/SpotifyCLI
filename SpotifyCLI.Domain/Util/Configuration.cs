using SpotifyAPI.Web;

namespace SpotifyCLI.Utilities {
    internal class Configuration {
        public string ClientId { get; set; }
        public PKCETokenResponse Tokens { get; set; }
    }
}