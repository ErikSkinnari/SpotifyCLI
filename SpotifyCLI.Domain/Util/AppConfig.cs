using SpotifyAPI.Web;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpotifyCLI.Utilities {
    public class AppConfig : IAppConfig {
        private readonly string _clientId;
        private const string _filePath = "./.Config/appConfig.json";
        private PKCETokenResponse _tokens;

        public string ClientId {
            get {
                return _clientId;
            }
        }

        public PKCETokenResponse Tokens {
            get {
                return _tokens;
            }
        }

        public AppConfig() {
            var directoryInfo = new DirectoryInfo("./.Config/");

            if (!directoryInfo.Exists) {
                directoryInfo.Create();
                directoryInfo.Attributes = FileAttributes.Directory;
            }
            
            var configFile = new FileInfo(_filePath);

            if (!configFile.Exists) {
                throw new FileLoadException("Cannot find config file. (Is it placed in the correct directory? [./.Config/appConfig.json]?)");
            }

            Configuration configData = GetConfiguration();

            _clientId = configData.ClientId;
            _tokens = configData.Tokens;
        }

        private static Configuration GetConfiguration() {
            using StreamReader sr = new StreamReader(_filePath);

            var jsonData = sr.ReadToEnd();
            sr.Close();
            return JsonSerializer.Deserialize<Configuration>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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