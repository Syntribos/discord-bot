using DiscordBot.Localization;

using Newtonsoft.Json;

namespace DiscordBot.Config
{
    public class BotConfig
    {
        private string _prefix;

        [JsonConstructor]
        private BotConfig(string token, string prefix, string database)
        {
            Token = token;
            _prefix = prefix;
            DatabaseLocation = database;
        }

        public string Token { get; }

        public string Prefix
        {
            get
            {
                return _prefix;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length > 2)
                {
                    return;
                }
                else
                {
                    _prefix = value;
                }
            }
        }

        public string DatabaseLocation { get; }
    }
}