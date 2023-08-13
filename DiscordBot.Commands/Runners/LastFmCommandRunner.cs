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
        if (count is < 1 or > 30)
        {
            await context.Interaction.RespondAsync("Can only fetch between 1 and 30 songs");
            return;
        }

        var songs = (await _lastFmService.GetRecentSongsForUser(lastFmUsername, count)).ToList();

        if (!songs.Any())
        {
            await context.Interaction.RespondAsync($"Couldn't fetch recently played for {lastFmUsername}");
            return;
        }

        var userImg = context.User.GetAvatarUrl() ?? context.User.GetDefaultAvatarUrl();
        await context.Interaction.RespondAsync(embed: songs.BuildRecentlyPlayed(context.User.Username, userImg, lastFmUsername));
    }
}