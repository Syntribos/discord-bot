using DiscordBot.Config;
using DiscordBot.DataModels.LastFm;
using IF.Lastfm.Core.Api;
using IF.Lastfm.Core.Api.Enums;

namespace DiscordBot.Services;

public class LastFmService
{
    private readonly BotConfig _botConfig;

    public LastFmService(BotConfig botConfig)
    {
        _botConfig = botConfig ?? throw new ArgumentNullException(nameof(botConfig));
    }

    public async Task<string> GetTopArtistForUser(string user, LastStatsTimeSpan lastStatsTime)
    {
        try
        {
            var client = new LastfmClient(_botConfig.LastFmApiKey, _botConfig.LastFmSecret);
            var artist = await client.User.GetTopArtists(user, lastStatsTime, 1, 1);
            var topArtist = artist.Content.FirstOrDefault();

            if (topArtist == null)
            {
                throw new Exception($"{nameof(topArtist)} was null.");
            }

            return $"{user}'s top artist was {topArtist.Name} with {topArtist.PlayCount} plays{TimeSpanToString(lastStatsTime)}";
        }
        catch(Exception ex)
        {
            await Console.Error.WriteLineAsync(ex.Message);
            return "Couldn't find user's top artist.";
        }
    }

    public async Task<IEnumerable<ISong>> GetRecentSongsForUser(string user, int count)
    {
        try
        {
            var client = new LastfmClient(_botConfig.LastFmApiKey, _botConfig.LastFmSecret);
            var tracks = await client.User.GetRecentScrobbles(user, count: count);

            if (tracks is not { Success: true })
            {
                throw new Exception($"Couldn't get recent tracks for user {user}.");
            }

            return tracks.Content.Select(x =>
            {
                var artist = ArtistFactory.BuildArtist(x.ArtistName, x.ArtistUrl.AbsoluteUri);
                return SongFactory.BuildSong(x.Name, artist, x.Url.AbsoluteUri, x.IsNowPlaying, x.TimePlayed);
            }).ToList();
        }
        catch(Exception ex)
        {
            await Console.Error.WriteLineAsync(ex.Message);
            return Enumerable.Empty<ISong>();
        }
    }

    private static string TimeSpanToString(LastStatsTimeSpan lastStatsTime)
    {
        return lastStatsTime switch
        {
            LastStatsTimeSpan.Year => " over the last year.",
            LastStatsTimeSpan.Month => " over the last month.",
            LastStatsTimeSpan.Half => " over the last 180 days.",
            LastStatsTimeSpan.Quarter => " over the last 90 days.",
            LastStatsTimeSpan.Week => " over the last week.",
            LastStatsTimeSpan.Overall => " ever.",
            _ => "."
        };
    }
}