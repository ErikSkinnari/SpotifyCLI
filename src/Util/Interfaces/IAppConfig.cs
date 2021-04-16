using SpotifyAPI.Web;
using System.IO;
using System.Threading.Tasks;

namespace SpotifyCLI.Utilities {
    public interface IAppConfig {
        string ClientId { get; }
        PKCETokenResponse Tokens { get; }

        Task SaveTokens(PKCETokenResponse tokenResponse);
        Task<PKCETokenResponse> SaveTokens(Stream jsonData);
    }
}