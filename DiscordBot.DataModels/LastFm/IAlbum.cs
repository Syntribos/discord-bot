namespace DiscordBot.DataModels.LastFm;

public interface IAlbum
{
    string Name { get; }
    
    IArtist Artist { get; }
    
    IEnumerable<ISong>? Tracks { get; }
}