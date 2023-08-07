using Discord;

namespace DiscordBot.Tests.Dummies;

public class GuildUserDummy : UserDummy, IGuildUser
#nullable disable
{
    public GuildUserDummy(IGuild guild)
        : base(1)
    {
        Guild = guild;
    }

    public GuildUserDummy(ulong id, string username, IGuild guild)
    : base(id, username)
    {
        Guild = guild;
    }

    public bool IsDeafened => throw new NotImplementedException();

    public bool IsMuted => throw new NotImplementedException();

    public bool IsSelfDeafened => throw new NotImplementedException();

    public bool IsSelfMuted => throw new NotImplementedException();

    public bool IsSuppressed => throw new NotImplementedException();

    public IVoiceChannel VoiceChannel => throw new NotImplementedException();

    public string VoiceSessionId => throw new NotImplementedException();

    public bool IsStreaming => throw new NotImplementedException();

    public bool IsVideoing => throw new NotImplementedException();

    public DateTimeOffset? RequestToSpeakTimestamp => throw new NotImplementedException();
    
    public DateTimeOffset? JoinedAt => throw new NotImplementedException();

    public string DisplayName => $"display {Username}";

    public string Nickname => $"nick {Username}";

    public string DisplayAvatarId => $"display avatar {Id}";

    public string GuildAvatarId => $"guild avatar {Id}";

    public GuildPermissions GuildPermissions => throw new NotImplementedException();

    public IGuild Guild { get; set; }

    public ulong GuildId => throw new NotImplementedException();

    public DateTimeOffset? PremiumSince => throw new NotImplementedException();

    public IReadOnlyCollection<ulong> RoleIds => throw new NotImplementedException();

    public bool? IsPending => throw new NotImplementedException();

    public int Hierarchy => throw new NotImplementedException();

    public DateTimeOffset? TimedOutUntil => throw new NotImplementedException();

    public GuildUserFlags Flags => throw new NotImplementedException();

    public ChannelPermissions GetPermissions(IGuildChannel channel)
    {
        throw new NotImplementedException();
    }

    public string GetGuildAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
    {
        throw new NotImplementedException();
    }

    public string GetDisplayAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
    {
        throw new NotImplementedException();
    }

    public Task KickAsync(string reason = null, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task ModifyAsync(Action<GuildUserProperties> func, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task AddRoleAsync(ulong roleId, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task AddRoleAsync(IRole role, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task AddRolesAsync(IEnumerable<ulong> roleIds, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task AddRolesAsync(IEnumerable<IRole> roles, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRoleAsync(ulong roleId, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRoleAsync(IRole role, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRolesAsync(IEnumerable<ulong> roleIds, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRolesAsync(IEnumerable<IRole> roles, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task SetTimeOutAsync(TimeSpan span, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task RemoveTimeOutAsync(RequestOptions options = null)
    {
        throw new NotImplementedException();
    }
}