using Discord;
using DiscordBot.DataModels.LastFm;
using DiscordBot.DataModels.LastFm.Implementations;
using DiscordBot.Utilities;

namespace DiscordBot.DiscordUtilities;

public static class Extensions
{
    public static Embed BuildRecentlyPlayed(this IEnumerable<ISong> songs, string discordUserName, string? userAvatarUrl, string lastFmUsername)
    {
        var recentSongs = songs.OfType<RecentSong>().ToList();
        var index = 0;
        var songString = string.Join(
            $"{Environment.NewLine}{Environment.NewLine}",
            recentSongs.Select(x => x.SongToStringWithLinks(ref index)));

        if (songString.Length >= 4096)
        {
            var newIndex = 0;
            songString = string.Join(
                $"{Environment.NewLine}{Environment.NewLine}",
                recentSongs.Select(x => x.SongToStringNoLinks(ref newIndex)));

            if (songString.Length >= 4096)
            {
                return new EmbedBuilder().WithTitle("That's too damn many songs!").Build();
            }
        }

        var eb = new EmbedBuilder();
        eb.WithTitle($"{discordUserName}'s recent scrobbles")
            .WithDescription(songString);

        if (userAvatarUrl != null)
        {
            eb.WithThumbnailUrl(userAvatarUrl);
        }

        return eb.Build();
    }

    private static string SongToStringWithLinks(this RecentSong song, ref int index)
    {
        var lastTime = song.CurrentlyPlaying
            ? "**Now Playing**"
            : $"**{++index}**";

        return $"{lastTime}: {song.Artist.GetLinkedName()} - {song.GetLinkedName()}{song.LastPlayedRelativeTimestamp}";
    }

    private static string SongToStringNoLinks(this RecentSong song, ref int index)
    {
        var lastTime = song.CurrentlyPlaying
            ? "**Now Playing**"
            : $"**{++index}**";

        return $"{lastTime}: {song.Artist.Name} - {song.Name}{song.LastPlayedRelativeTimestamp}";
    }
}