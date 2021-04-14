using System.Threading.Tasks;
using SpotifyAPI.Web;

namespace SpotifyCLI.Services { 
    public interface IAuthService { 
        Task<ISpotifyClient> CreateSpotifyClientAsync();
    } 
}