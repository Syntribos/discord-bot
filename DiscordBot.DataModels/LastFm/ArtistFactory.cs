using DiscordBot.DataModels.LastFm.Implementations;

namespace DiscordBot.DataModels.LastFm;

public static class ArtistFactory
{
    public static IArtist BuildArtist(string artistName)
    {
        return new Artist(artistName);
    }
}