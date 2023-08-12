using DiscordBot.DataModels.LastFm.Implementations;

namespace DiscordBot.DataModels.LastFm;

public static class ArtistFactory
{
    public static IArtist BuildArtist(string artistName, string? artistLink)
    {
        return new Artist(artistName, artistLink);
    }
}