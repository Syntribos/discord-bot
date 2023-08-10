namespace DiscordBot.DataModels.LastFm.Implementations;

internal class RecentSong : ISong
{
    internal RecentSong(string name, IArtist artist, long lastPlayedUnixTimestamp, bool? currentlyPlaying)
    {
        Name = name;
        Artist = artist;
        LastPlayedUnixTimestamp = lastPlayedUnixTimestamp;
        CurrentlyPlaying = currentlyPlaying ?? false;
    }

    public string Name { get; }

    public IArtist Artist { get; }
    
    public long LastPlayedUnixTimestamp { get; }
    
    public bool CurrentlyPlaying { get; }
}