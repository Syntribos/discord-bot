namespace DiscordBot.DataModels.LastFm.Implementations;

internal class Song : ISong
{
    internal Song(string name, IArtist artist)
    {
        Name = name;
        Artist = artist;
    }

    public string Name { get; }

    public IArtist Artist { get; }
}