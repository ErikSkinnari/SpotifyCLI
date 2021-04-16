using System.Collections.Generic;
using System.Net.Http;
using SpotifyAPI.Web;

namespace SpotifyCLI.Utilities {
    public static class PKCETokenRefreshRequestExtensions {
        public static FormUrlEncodedContent ToUrlEncoded(this PKCETokenRefreshRequest refreshRequest) {
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();

            headers.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
            headers.Add(new KeyValuePair<string, string>("refresh_token", refreshRequest.RefreshToken));
            headers.Add(new KeyValuePair<string, string>("client_id", refreshRequest.ClientId));

            return new FormUrlEncodedContent(headers);
        }
    }
}