using Draws.CLI;
using SpotifyAPI.Web;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpotifyCLI.Utilities {
    public class AppConfig : IAppConfig {
        private readonly string _clientId;
        private const string _filePath = "./.Config/appConfig.json";
        private readonly IOutputHandler _outputHandler;
        private PKCETokenResponse _tokens;

        public string ClientId { get { return _clientId; }}

        public PKCETokenResponse Tokens { get { return _tokens; }}

        public AppConfig(IOutputHandler outputHandler) {
            _outputHandler = outputHandler;

            Configuration configData = Initialise();

            _clientId = configData.ClientId;
            _tokens = configData.Tokens;
        }

        private static Configuration GetConfiguration() {
            using StreamReader sr = new StreamReader(_filePath);

            var jsonData = sr.ReadToEnd();
            sr.Close();
            return JsonSerializer.Deserialize<Configuration>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        private Configuration Initialise() {
            var directoryInfo = new DirectoryInfo("./.Config/");

            if (!directoryInfo.Exists) {
                directoryInfo.Create();
                directoryInfo.Attributes = FileAttributes.Directory;
            }
            
            var configFile = new FileInfo(_filePath);

            if (!configFile.Exists) {
                _outputHandler.Output("No config file was found. Creating...");
                configFile.Create();

                // TODO: Use future input-handler to take a client id from the user
                var configData = new Configuration() { ClientId = "ClientId123", Tokens = new PKCETokenResponse() { AccessToken = "" }};
                string jsonData = JsonSerializer.Serialize<Configuration>(configData);

                File.WriteAllLines(_filePath, new string[] { jsonData });

                // TODO: Remove this when the input logic is available
                _outputHandler.Output("The file has been created. Please replace the current client-id with one of your own.");
                _outputHandler.Output("You can get a client id from the spotify developer site.");
            }

            return GetConfiguration();
        }

        public async Task SaveTokens(PKCETokenResponse tokenReponse) {
            _tokens = tokenReponse;

            Configuration configuration = GetConfiguration();
            configuration.Tokens = tokenReponse;
            string jsonData = JsonSerializer.Serialize<Configuration>(configuration);

            await File.WriteAllLinesAsync(_filePath, new[] { jsonData } );
        }
    }
}