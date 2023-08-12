using Discord.Interactions;
using DiscordBot.Commands.Runners;
using IF.Lastfm.Core.Api.Enums;

namespace DiscordBot.Commands;

public class LastFmCommands : InteractionModuleBase<SocketInteractionContext>
{
    private readonly LastFmCommandRunner _lastFmCommandRunner;

    public LastFmCommands(LastFmCommandRunner lastFmCommandRunner)
    {
        _lastFmCommandRunner = lastFmCommandRunner ?? throw new ArgumentNullException(nameof(lastFmCommandRunner));
    }

    [SlashCommand("topartist", "Gets the name of a user's top artist.")]
    public async Task GetTopArtistForUser(string lastFmUsername, LastStatsTimeSpan lastStatsTime = LastStatsTimeSpan.Year)
    {
        await _lastFmCommandRunner.GetTopArtistForUser(Context, lastFmUsername, lastStatsTime);
    }

    [SlashCommand("lastplayed", "Gets the last scrobbled for a user")]
    public async Task GetLastScrobbledForUser(string lastFmUsername, int count = 10)
    {
        await Context.Safely(
            () => _lastFmCommandRunner.GetLastSongsForUser(Context, lastFmUsername, count),
            "Couldn't get info for user.");
    }
}