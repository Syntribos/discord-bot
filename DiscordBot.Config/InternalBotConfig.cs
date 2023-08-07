using System.Text.Json.Serialization;

namespace DiscordBot.Config;

public class InternalBotConfig
{
    private string _prefix;

    [JsonConstructor]
    public InternalBotConfig(string token, string prefix, string databaseLocation)
    {
        Token = token;
        _prefix = prefix;
        DatabaseLocation = databaseLocation;
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
}