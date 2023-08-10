using Discord;
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

        var response = string.Join("\n\n", songs.Select((song, idx) => $"{idx + 1}. {song.Artist.Name} - {song.Name}"));

        await context.Interaction.RespondAsync(response);
    }
}