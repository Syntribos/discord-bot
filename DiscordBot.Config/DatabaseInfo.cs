using DiscordBot.DataModels;

namespace DiscordBot.Config
{
    public class DatabaseInfo : IDatabaseInfo
    {
        public DatabaseInfo(BotConfig botConfig)
        {
            DatabasePath = botConfig.DatabaseLocation;
        }

        public string DatabasePath { get; }

        public string ConnectionString => $"Data Source={DatabasePath};";
    }
}