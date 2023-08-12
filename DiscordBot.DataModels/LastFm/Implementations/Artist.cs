namespace DiscordBot.DataModels.LastFm.Implementations;

public class Artist : IArtist
{
    private readonly string? _artistLink;

    internal Artist(string name, string? artistLink)
    {
        Name = name;
        _artistLink = artistLink;
    }

    public string Name { get; }

    public string GetLinkedName()
    {
        return _artistLink != null ? $"[{Name}]({_artistLink})" : Name;
    }
}