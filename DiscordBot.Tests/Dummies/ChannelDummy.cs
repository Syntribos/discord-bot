using Discord;

namespace DiscordBot.Tests.Dummies;

public class ChannelDummy : IChannel
#nullable disable
{
    public ulong Id => throw new NotImplementedException();

    public DateTimeOffset CreatedAt => throw new NotImplementedException();

    public IAsyncEnumerable<IReadOnlyCollection<IUser>> GetUsersAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IUser> GetUserAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public string Name => throw new NotImplementedException();
}