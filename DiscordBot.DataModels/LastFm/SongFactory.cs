using DiscordBot.DataModels.LastFm.Implementations;

namespace DiscordBot.DataModels.LastFm;

public static class SongFactory
{
    public static ISong BuildSong(string songName, IArtist artist, string? songLink = null, bool? currentlyPlaying = null, DateTimeOffset? lastPlayedTime = null)
    {
        return lastPlayedTime == null && currentlyPlaying == false
            ? new Song(songName, artist, songLink)
            : new RecentSong(songName, artist, songLink, currentlyPlaying, lastPlayedTime);
    }
}