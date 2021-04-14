using System;
using SpotifyAPI.Web;

namespace SpotifyCLI.Utilities {
    public static class PKCETokenResponseExtensions {
        public static bool HasExpired(this PKCETokenResponse token) {
            var created = token.CreatedAt;
            var expirationTime = token.ExpiresIn;

            return created.AddSeconds(expirationTime) < DateTime.UtcNow;
        }
    }
}