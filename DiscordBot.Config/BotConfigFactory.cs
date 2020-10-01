using System;
using System.IO;
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

            return JsonConvert.DeserializeObject<BotConfig>(json);
        }
    }
}
