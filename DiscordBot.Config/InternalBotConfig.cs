using System.Text.Json.Serialization;

namespace DiscordBot.Config;

public class InternalBotConfig
{
    private string _prefix;

    [JsonConstructor]
    public InternalBotConfig(string token, string prefix, string databaseLocation, string lastFmApiKey, string lastFmSecret)
    {
        Token = token ?? throw new ArgumentNullException(nameof(token));
        _prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));
        DatabaseLocation = databaseLocation ?? throw new ArgumentNullException(nameof(databaseLocation));
        LastFmApiKey = lastFmApiKey ?? throw new ArgumentNullException(nameof(lastFmApiKey));
        LastFmSecret = lastFmSecret ?? throw new ArgumentNullException(nameof(lastFmSecret));
    }

    public string Token { get; }

    public string Prefix
    {
        get => _prefix;

        set
        {
            if (!string.IsNullOrEmpty(value) && value.Length <= 2)
            {
                _prefix = value;
            }
        }
    }

    public string DatabaseLocation { get; }

    public string LastFmApiKey { get; }

    public string LastFmSecret { get; }
}