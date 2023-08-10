namespace DiscordBot.DataModels.LastFm.Implementations;

internal class Album : IAlbum
{
    internal Album(string name, IArtist artist, IEnumerable<ISong>? tracks = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Artist = artist ?? throw new ArgumentNullException(nameof(artist));
        Tracks = tracks;
    }

    public string Name { get; }

    public IArtist Artist { get; }

    public IEnumerable<ISong>? Tracks { get; }
}