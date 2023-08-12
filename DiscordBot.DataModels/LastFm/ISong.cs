namespace DiscordBot.DataModels.LastFm;

public interface ISong
{
    string Name { get; }
    
    IArtist Artist { get; }

    public string GetLinkedName();
}