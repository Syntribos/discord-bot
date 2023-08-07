namespace DiscordBot.DataModels
{
    public interface IDatabaseInfo
    {
        string DatabasePath { get; }

        string ConnectionString { get; }
    }
}