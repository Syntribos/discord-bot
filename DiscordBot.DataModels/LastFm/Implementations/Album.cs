namespace DiscordBot.DataModels.LastFm.Implementations;

public class Album : IAlbum
{
    private readonly string? _albumLink;

    internal Album(string name, IArtist artist, IEnumerable<ISong>? tracks = null, string albumLink = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Artist = artist ?? throw new ArgumentNullException(nameof(artist));
        Tracks = tracks;
        _albumLink = albumLink;
    }

    public string Name { get; }

    public IArtist Artist { get; }

    public IEnumerable<ISong>? Tracks { get; }

    public string GetLinkedName()
    {
        return _albumLink != null ? $"[{Name}]({_albumLink})" : Name;
    }
}