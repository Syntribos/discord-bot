namespace DiscordBot.DataModels.LastFm.Implementations;

public class Song : ISong
{
    private readonly string? _songLink;

    internal Song(string name, IArtist artist, string? songLink = null)
    {
        Name = name;
        Artist = artist;
        _songLink = songLink;
    }

    public string Name { get; }

    public IArtist Artist { get; }
    
    public string GetLinkedName()
    {
        return _songLink != null ? $"[{Name}]({_songLink})" : Name;
    }
}