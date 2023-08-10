namespace DiscordBot.DataModels.LastFm.Implementations;

internal class Artist : IArtist
{
    internal Artist(string name)
    {
        Name = name;
    }

    public string Name { get; }
}