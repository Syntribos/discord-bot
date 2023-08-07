using Newtonsoft.Json;
using System.Collections.Generic;
using Discord;
using Discord.WebSocket;

namespace DiscordBot.Config
{
    public class BotConfig
    {
        private readonly string _configFileLocation;
        private readonly InternalBotConfig _internalBotConfig;

        internal BotConfig(string configFileLocation, InternalBotConfig internalBotConfig)
        {
            _configFileLocation = configFileLocation ?? throw new ArgumentNullException(nameof(configFileLocation));
            _internalBotConfig = internalBotConfig ?? throw new ArgumentNullException(nameof(internalBotConfig));
        }

        public string Token => _internalBotConfig.Token;

        public string Prefix => _internalBotConfig.Prefix;

        public string DatabaseLocation => _internalBotConfig.DatabaseLocation;

        public static DiscordSocketConfig SocketConfig => new()
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent | GatewayIntents.GuildPresences,
            AlwaysDownloadUsers = true,
        };

        public bool UpdatePrefix(string newPrefix)
        {
            var oldPrefix = _internalBotConfig.Prefix;

            try
            {
                using var sw = new StreamWriter(_configFileLocation);
                using var jtw = new JsonTextWriter(sw);

                _internalBotConfig.Prefix = newPrefix;
                var newConfig = JsonConvert.SerializeObject(_internalBotConfig);
                
                sw.WriteLine(newConfig);
                sw.Flush();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Failed to update bot prefix: {e.Message}");
                _internalBotConfig.Prefix = oldPrefix;
                return false;
            }

            return true;
        }
    }
}