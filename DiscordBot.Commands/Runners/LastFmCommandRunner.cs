using Discord;
using DiscordBot.DiscordUtilities;
using DiscordBot.Services;
using IF.Lastfm.Core.Api.Enums;

namespace DiscordBot.Commands.Runners;

public class LastFmCommandRunner
{
    private readonly LastFmService _lastFmService;

    public LastFmCommandRunner(LastFmService lastFmService)
    {
        _lastFmService = lastFmService ?? throw new ArgumentNullException(nameof(lastFmService));
    }

    public async Task GetTopArtistForUser(IInteractionContext context, string lastFmUsername, LastStatsTimeSpan lastStatsTime)
    {
        var artistName = await _lastFmService.GetTopArtistForUser(lastFmUsername, lastStatsTime);
        await context.Interaction.RespondAsync(artistName);
    }

    public async Task GetLastSongsForUser(IInteractionContext context, string lastFmUsername, int count)
    {
        var songs = await _lastFmService.GetRecentSongsForUser(lastFmUsername, count);
        var userImg = context.User.GetAvatarUrl() ?? context.User.GetDefaultAvatarUrl();
        await context.Interaction.RespondAsync(embed: songs.BuildRecentlyPlayed(context.User.Username, userImg, lastFmUsername));
    }
}