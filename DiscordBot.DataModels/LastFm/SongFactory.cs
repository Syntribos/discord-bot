using DiscordBot.DataModels.LastFm.Implementations;

namespace DiscordBot.DataModels.LastFm;

public static class SongFactory
{
    public static ISong BuildSong(string songName, IArtist artist, long? lastPlayedUnixTimestamp = null, bool? currentlyPlaying = null)
    {
        return lastPlayedUnixTimestamp == null
            ? new Song(songName, artist)
            : new RecentSong(songName, artist, lastPlayedUnixTimestamp.Value, currentlyPlaying);
    }
}