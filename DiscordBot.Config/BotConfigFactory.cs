using System.Text;

using Newtonsoft.Json;

namespace DiscordBot.Config
{
    public class BotConfigFactory
    {
        private readonly string _configFile;

        public BotConfigFactory(string configFile)
        {
            _configFile = configFile;
        }

        public BotConfig Create()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead(_configFile))
            using (var sr = new StreamReader(fs, Encoding.UTF8))
            {
                json = sr.ReadToEnd();
            }

            if (json == string.Empty)
            {
                throw new Exception("Failed to read config file.");
            }

            var internalConfig = JsonConvert.DeserializeObject<InternalBotConfig>(json) ?? throw new ArgumentException($"Couldn't deserialize file {_configFile} properly. Check the format and try again.");
            return new BotConfig(_configFile, internalConfig);
        }
    }
}