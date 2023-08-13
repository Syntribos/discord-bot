namespace DiscordBot.DataModels.LastFm.Implementations;

public class RecentSong : ISong
{
    private readonly string? _songLink;

    internal RecentSong(string name, IArtist artist, string? songLink, bool? currentlyPlaying, DateTimeOffset? lastPlayDateTime)
    {
        Name = name;
        Artist = artist;
        _songLink = songLink;
        CurrentlyPlaying = currentlyPlaying ?? false;
        LastPlayDateTime = lastPlayDateTime;
    }

    public string Name { get; }

    public IArtist Artist { get; }
    
    public bool CurrentlyPlaying { get; }

    public DateTimeOffset? LastPlayDateTime { get; }

    public string LastPlayedRelativeTimestamp => CurrentlyPlaying
        ? "Now playing"
        : LastPlayDateTime is not null
            ? $" - <t:{LastPlayDateTime?.ToUnixTimeSeconds()}:R>"
            : string.Empty;

    public string GetLinkedName()
    {
        return _songLink != null ? $"[{Name}]({_songLink})" : Name;
    }
}