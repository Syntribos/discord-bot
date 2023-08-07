using System.Globalization;
using Discord;
using Discord.Audio;

namespace DiscordBot.Tests.Dummies;

public class GuildDummy : IGuild
#nullable disable
{
    public GuildDummy()
    : this(new List<IGuildUser>(), new List<IUser>())
    {
    }

    public GuildDummy(List<IGuildUser> guildUsers)
        : this(guildUsers, new List<IUser>())
    {
    }

    public GuildDummy(List<IGuildUser> guildUsers, List<IUser> bannedUsers)
    {
        GuildUsers = guildUsers;
        BannedUsers = bannedUsers;
    }

    public Task DeleteAsync(RequestOptions options = null)
    {
        return Task.CompletedTask;
    }

    public void AddUser(IGuildUser guildUser)
    {
        GuildUsers.Add(guildUser);
    }

    public void AddUsers(IEnumerable<IGuildUser> users)
    {
        users.ToList().ForEach(AddUser);
    }
    
    public List<IGuildUser> GuildUsers { get; }
    
    public List<IUser> BannedUsers { get; }

    public ulong Id => throw new NotImplementedException();

    public DateTimeOffset CreatedAt => throw new NotImplementedException();

    public Task ModifyAsync(Action<GuildProperties> func, RequestOptions options = null)
    {
        return Task.CompletedTask;
    }

    public Task ModifyWidgetAsync(Action<GuildWidgetProperties> func, RequestOptions options = null)
    {
        return Task.CompletedTask;
    }

    public Task ReorderChannelsAsync(IEnumerable<ReorderChannelProperties> args, RequestOptions options = null)
    {
        return Task.CompletedTask;
    }

    public Task ReorderRolesAsync(IEnumerable<ReorderRoleProperties> args, RequestOptions options = null)
    {
        return Task.CompletedTask;
    }

    public Task LeaveAsync(RequestOptions options = null)
    {
        return Task.CompletedTask;
    }

    public async IAsyncEnumerable<IReadOnlyCollection<IBan>> GetBansAsync(int limit = 1000, RequestOptions options = null)
    {
        await Task.Delay(0);
        yield return new List<IBan> { new BanDummy() };
    }

    public async IAsyncEnumerable<IReadOnlyCollection<IBan>> GetBansAsync(ulong fromUserId, Direction dir, int limit = 1000, RequestOptions options = null)
    {
        await Task.Delay(0);
        yield return new List<IBan> { new BanDummy() };
    }

    public async IAsyncEnumerable<IReadOnlyCollection<IBan>> GetBansAsync(IUser fromUser, Direction dir, int limit = 1000, RequestOptions options = null)
    {
        await Task.Delay(0);
        yield return new List<IBan> { new BanDummy() };
    }

    public Task<IBan> GetBanAsync(IUser user, RequestOptions options = null)
    {
        return Task.FromResult(new BanDummy(user) as IBan);
    }

    public Task<IBan> GetBanAsync(ulong userId, RequestOptions options = null)
    {
        return Task.FromResult(new BanDummy(userId.ToString()) as IBan);
    }

    public Task AddBanAsync(IUser user, int pruneDays = 0, string reason = null, RequestOptions options = null)
    {
        GuildUsers.Remove(GuildUsers.First(x => x.Id == user.Id));
        BannedUsers.Add(user);
        return Task.CompletedTask;
    }

    public Task AddBanAsync(ulong userId, int pruneDays = 0, string reason = null, RequestOptions options = null)
    {
        return Task.CompletedTask;
    }

    public Task RemoveBanAsync(IUser user, RequestOptions options = null)
    {
        return Task.CompletedTask;
    }

    public Task RemoveBanAsync(ulong userId, RequestOptions options = null)
    {
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<IGuildChannel>> GetChannelsAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        return Task.FromResult(new List<IGuildChannel>{ new GuildChannelDummy() } as IReadOnlyCollection<IGuildChannel>);
    }

    public Task<IGuildChannel> GetChannelAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        return Task.FromResult(new GuildChannelDummy() as IGuildChannel);
    }

    public Task<IReadOnlyCollection<ITextChannel>> GetTextChannelsAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        return Task.FromResult(new List<ITextChannel>{ new TextChannelDummy() } as IReadOnlyCollection<ITextChannel>);
    }

    public Task<ITextChannel> GetTextChannelAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        return Task.FromResult(new TextChannelDummy() as ITextChannel);
    }

    public Task<IReadOnlyCollection<IVoiceChannel>> GetVoiceChannelsAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<ICategoryChannel>> GetCategoriesAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IVoiceChannel> GetVoiceChannelAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IStageChannel> GetStageChannelAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IStageChannel>> GetStageChannelsAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IVoiceChannel> GetAFKChannelAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<ITextChannel> GetSystemChannelAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<ITextChannel> GetDefaultChannelAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IGuildChannel> GetWidgetChannelAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<ITextChannel> GetRulesChannelAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<ITextChannel> GetPublicUpdatesChannelAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IThreadChannel> GetThreadChannelAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IThreadChannel>> GetThreadChannelsAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<ITextChannel> CreateTextChannelAsync(string name, Action<TextChannelProperties> func = null, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IVoiceChannel> CreateVoiceChannelAsync(string name, Action<VoiceChannelProperties> func = null, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IStageChannel> CreateStageChannelAsync(string name, Action<VoiceChannelProperties> func = null, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<ICategoryChannel> CreateCategoryAsync(string name, Action<GuildChannelProperties> func = null, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IForumChannel> CreateForumChannelAsync(string name, Action<ForumChannelProperties> func = null, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IVoiceRegion>> GetVoiceRegionsAsync(RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IIntegration>> GetIntegrationsAsync(RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task DeleteIntegrationAsync(ulong id, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IInviteMetadata>> GetInvitesAsync(RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IInviteMetadata> GetVanityInviteAsync(RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public IRole GetRole(ulong id)
    {
        throw new NotImplementedException();
    }

    public Task<IRole> CreateRoleAsync(string name, GuildPermissions? permissions = null, Color? color = null, bool isHoisted = false,
        RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IRole> CreateRoleAsync(string name, GuildPermissions? permissions = null, Color? color = null, bool isHoisted = false,
        bool isMentionable = false, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IGuildUser> AddGuildUserAsync(ulong userId, string accessToken, Action<AddGuildUserProperties> func = null, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task DisconnectAsync(IGuildUser user)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IGuildUser>> GetUsersAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IGuildUser> GetUserAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
#pragma warning disable CS8619
        return Task.FromResult(GuildUsers.FirstOrDefault(x => x.Id == id));
#pragma warning restore CS8619
    }

    public Task<IGuildUser> GetCurrentUserAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IGuildUser> GetOwnerAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task DownloadUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> PruneUsersAsync(int days = 30, bool simulate = false, RequestOptions options = null,
        IEnumerable<ulong> includeRoleIds = null)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IGuildUser>> SearchUsersAsync(string query, int limit = 1000, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IAuditLogEntry>> GetAuditLogsAsync(int limit = 100, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null,
        ulong? beforeId = null, ulong? userId = null, ActionType? actionType = null, ulong? afterId = null)
    {
        throw new NotImplementedException();
    }

    public Task<IWebhook> GetWebhookAsync(ulong id, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IWebhook>> GetWebhooksAsync(RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<GuildEmote>> GetEmotesAsync(RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<GuildEmote> GetEmoteAsync(ulong id, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<GuildEmote> CreateEmoteAsync(string name, Image image, Optional<IEnumerable<IRole>> roles = new Optional<IEnumerable<IRole>>(), RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<GuildEmote> ModifyEmoteAsync(GuildEmote emote, Action<EmoteProperties> func, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task MoveAsync(IGuildUser user, IVoiceChannel targetChannel)
    {
        throw new NotImplementedException();
    }

    public Task DeleteEmoteAsync(GuildEmote emote, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<ICustomSticker> CreateStickerAsync(string name, Image image, IEnumerable<string> tags, string description = null,
        RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<ICustomSticker> CreateStickerAsync(string name, string path, IEnumerable<string> tags, string description = null,
        RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<ICustomSticker> CreateStickerAsync(string name, Stream stream, string filename, IEnumerable<string> tags, string description = null,
        RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<ICustomSticker> GetStickerAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<ICustomSticker>> GetStickersAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task DeleteStickerAsync(ICustomSticker sticker, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IGuildScheduledEvent> GetEventAsync(ulong id, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IGuildScheduledEvent>> GetEventsAsync(RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IGuildScheduledEvent> CreateEventAsync(string name, DateTimeOffset startTime, GuildScheduledEventType type,
        GuildScheduledEventPrivacyLevel privacyLevel = GuildScheduledEventPrivacyLevel.Private, string description = null, DateTimeOffset? endTime = null,
        ulong? channelId = null, string location = null, Image? coverImage = null, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IApplicationCommand>> GetApplicationCommandsAsync(bool withLocalizations = false, string locale = null, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IApplicationCommand> GetApplicationCommandAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IApplicationCommand> CreateApplicationCommandAsync(ApplicationCommandProperties properties, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IApplicationCommand>> BulkOverwriteApplicationCommandsAsync(ApplicationCommandProperties[] properties, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<WelcomeScreen> GetWelcomeScreenAsync(RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<WelcomeScreen> ModifyWelcomeScreenAsync(bool enabled, WelcomeScreenChannelProperties[] channels, string description = null,
        RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IAutoModRule[]> GetAutoModRulesAsync(RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IAutoModRule> GetAutoModRuleAsync(ulong ruleId, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IAutoModRule> CreateAutoModRuleAsync(Action<AutoModRuleProperties> props, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public Task<IGuildOnboarding> GetOnboardingAsync(RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public string Name => throw new NotImplementedException();

    public int AFKTimeout => throw new NotImplementedException();

    public bool IsWidgetEnabled => throw new NotImplementedException();

    public DefaultMessageNotifications DefaultMessageNotifications => throw new NotImplementedException();

    public MfaLevel MfaLevel => throw new NotImplementedException();

    public VerificationLevel VerificationLevel => throw new NotImplementedException();

    public ExplicitContentFilterLevel ExplicitContentFilter => throw new NotImplementedException();

    public string IconId => throw new NotImplementedException();

    public string IconUrl => throw new NotImplementedException();

    public string SplashId => throw new NotImplementedException();

    public string SplashUrl => throw new NotImplementedException();

    public string DiscoverySplashId => throw new NotImplementedException();

    public string DiscoverySplashUrl => throw new NotImplementedException();

    public bool Available => throw new NotImplementedException();

    public ulong? AFKChannelId => throw new NotImplementedException();

    public ulong? WidgetChannelId => throw new NotImplementedException();

    public ulong? SafetyAlertsChannelId => throw new NotImplementedException();

    public ulong? SystemChannelId => throw new NotImplementedException();

    public ulong? RulesChannelId => throw new NotImplementedException();

    public ulong? PublicUpdatesChannelId => throw new NotImplementedException();

    public ulong OwnerId => throw new NotImplementedException();

    public ulong? ApplicationId => throw new NotImplementedException();

    public string VoiceRegionId => throw new NotImplementedException();

    public IAudioClient AudioClient => throw new NotImplementedException();

    public IRole EveryoneRole => throw new NotImplementedException();

    public IReadOnlyCollection<GuildEmote> Emotes => throw new NotImplementedException();

    public IReadOnlyCollection<ICustomSticker> Stickers => throw new NotImplementedException();

    public GuildFeatures Features => throw new NotImplementedException();

    public IReadOnlyCollection<IRole> Roles => throw new NotImplementedException();

    public PremiumTier PremiumTier => throw new NotImplementedException();

    public string BannerId => throw new NotImplementedException();

    public string BannerUrl => throw new NotImplementedException();

    public string VanityURLCode => throw new NotImplementedException();

    public SystemChannelMessageDeny SystemChannelFlags => throw new NotImplementedException();

    public string Description => throw new NotImplementedException();

    public int PremiumSubscriptionCount => throw new NotImplementedException();

    public int? MaxPresences => throw new NotImplementedException();

    public int? MaxMembers => throw new NotImplementedException();

    public int? MaxVideoChannelUsers => throw new NotImplementedException();

    public int? MaxStageVideoChannelUsers => throw new NotImplementedException();

    public int? ApproximateMemberCount => throw new NotImplementedException();

    public int? ApproximatePresenceCount => throw new NotImplementedException();

    public int MaxBitrate => throw new NotImplementedException();

    public string PreferredLocale => throw new NotImplementedException();

    public NsfwLevel NsfwLevel => throw new NotImplementedException();

    public CultureInfo PreferredCulture => throw new NotImplementedException();

    public bool IsBoostProgressBarEnabled => throw new NotImplementedException();

    public ulong MaxUploadLimit => throw new NotImplementedException();
}