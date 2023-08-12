namespace DiscordBot.DataModels.LastFm;

public interface IArtist
{
    string Name { get; }

    string GetLinkedName();
}