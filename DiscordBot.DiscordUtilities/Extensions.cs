using Discord;
using DiscordBot.DataModels.LastFm;
using DiscordBot.DataModels.LastFm.Implementations;

namespace DiscordBot.DiscordUtilities;

public static class Extensions
{
    public static Embed BuildRecentlyPlayed(this IEnumerable<ISong> songs, string discordUserName, string? userAvatarUrl, string lastFmUsername)
    {
        var eb = new EmbedBuilder()
            .WithTitle($"{lastFmUsername}'s recent tracks");

        var index = 0;
        var embedFields = songs.OfType<RecentSong>().Select(x => x.ToEmbedFieldBuilder(ref index));

        eb.WithTitle($"{discordUserName}'s recent scrobbles")
            .WithFields(embedFields);

        if (userAvatarUrl != null)
        {
            eb.WithThumbnailUrl(userAvatarUrl);
        }

        return eb.Build();
    }

    private static EmbedFieldBuilder ToEmbedFieldBuilder(this RecentSong song, ref int index)
    {
        var lastTime = song.CurrentlyPlaying
            ? "Now Playing:"
            : $"{++index}:";

        return new EmbedFieldBuilder()
            .WithIsInline(false)
            .WithName(lastTime)
            .WithValue($"{song.Artist.GetLinkedName()} - {song.GetLinkedName()}");
    }
}