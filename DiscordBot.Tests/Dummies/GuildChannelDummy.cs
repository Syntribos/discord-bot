using Discord;
#pragma warning disable CS0108, CS0114

namespace DiscordBot.Tests.Dummies;

public class GuildChannelDummy : ChannelDummy, IGuildChannel
#nullable disable
{
    public Task DeleteAsync(RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task ModifyAsync(Action<GuildChannelProperties> func, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public OverwritePermissions? GetPermissionOverwrite(IRole role)
    {
        throw new NotImplementedException();
    }

    public OverwritePermissions? GetPermissionOverwrite(IUser user)
    {
        throw new NotImplementedException();
    }

    public Task RemovePermissionOverwriteAsync(IRole role, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task RemovePermissionOverwriteAsync(IUser user, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task AddPermissionOverwriteAsync(IRole role, OverwritePermissions permissions, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task AddPermissionOverwriteAsync(IUser user, OverwritePermissions permissions, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<IReadOnlyCollection<IGuildUser>> GetUsersAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IGuildUser> GetUserAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public int Position => throw new NotImplementedException();

    public ChannelFlags Flags => throw new NotImplementedException();

    public IGuild Guild => throw new NotImplementedException();

    public ulong GuildId => throw new NotImplementedException();

    public IReadOnlyCollection<Overwrite> PermissionOverwrites => throw new NotImplementedException();
}