using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Draws.CLI;
using SpotifyAPI.Web;
using SpotifyCLI.Utilities;

namespace SpotifyCLI.Services {
    public class AuthService : IAuthService {
        private readonly string _challenge;
        private readonly IAppConfig _config;
        private readonly Uri _loginRequestUri;
        private readonly IOutputHandler _outputHandler;
        private const string _redirectUri = "http://localhost:1337/callback/";
        private readonly string _verifier;

        public AuthService(IAppConfig config, IOutputHandler outputHandler) {
            (_verifier, _challenge) = PKCEUtil.GenerateCodes();
            _config = config;
            _outputHandler = outputHandler;

            var loginRequest = new LoginRequest(new Uri(_redirectUri), _config.ClientId, LoginRequest.ResponseType.Code) {
                CodeChallengeMethod = "S256",
                CodeChallenge = _challenge,
                Scope = new [] { 
                    Scopes.AppRemoteControl,
                    Scopes.PlaylistModifyPrivate,
                    Scopes.PlaylistModifyPublic,
                    Scopes.PlaylistReadCollaborative,
                    Scopes.PlaylistReadPrivate,
                    Scopes.Streaming,
                    Scopes.UserFollowModify,
                    Scopes.UserFollowRead,
                    Scopes.UserLibraryModify,
                    Scopes.UserLibraryRead,
                    Scopes.UserModifyPlaybackState,
                    Scopes.UserReadCurrentlyPlaying,
                    Scopes.UserReadPlaybackPosition,
                    Scopes.UserReadPlaybackState,
                    Scopes.UserReadPrivate,
                    Scopes.UserReadRecentlyPlayed,
                    Scopes.UserTopRead,
                }
            };

            _loginRequestUri = loginRequest.ToUri();
        } 

        private async Task<PKCETokenResponse> GetCallbackTokens(string code) {
            var initialResponse = await new OAuthClient().RequestToken(new PKCETokenRequest(_config.ClientId, code, new Uri(_redirectUri), _verifier));
            _outputHandler.Output("Exchanged code for tokens.");

            return initialResponse;
        }

        public async Task<ISpotifyClient> CreateSpotifyClientAsync() {
            PKCETokenResponse tokenResponse = _config.Tokens;

            if (String.IsNullOrEmpty(tokenResponse.AccessToken) || tokenResponse.HasExpired())
                tokenResponse = await UseNewTokens();

            var authenticator = new PKCEAuthenticator(_config.ClientId, tokenResponse);
            authenticator.TokenRefreshed += async (sender, token) => await _config.SaveTokens(token);

            var config = SpotifyClientConfig.CreateDefault().WithAuthenticator(authenticator);

            return new SpotifyClient(config);
        }

        private async Task<PKCETokenResponse> UseNewTokens() {
            _outputHandler.Output("Please authorize the app in the browser-window.");

            Process.Start(new ProcessStartInfo(_loginRequestUri.ToString()) { UseShellExecute = true });
            string authCode = "";

            using (var server = new HttpListener()) {
                server.Prefixes.Add(_redirectUri);

                _outputHandler.Output("Waiting for response from browser...");
                server.Start();

                while (server.IsListening) {
                    var ctx = await server.GetContextAsync();
                    var request = ctx.Request;

                    var response = request.QueryString;
                    if (response.Get("error") != null) throw new AccessViolationException("Authorization failed");

                    authCode = response.Get("code");
                    _outputHandler.Output("Recieved code from callback.");

                    server.Stop();
                }
            }

            if (String.IsNullOrEmpty(authCode)) throw new Exception("Something went wrong while fetching the code");

            var tokenResponse = await GetCallbackTokens(authCode);
            await _config.SaveTokens(tokenResponse);
            return tokenResponse;
        }
    }
}