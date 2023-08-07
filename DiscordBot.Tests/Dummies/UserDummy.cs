using Discord;

namespace DiscordBot.Tests.Dummies;

public class UserDummy : IUser
{
    private const string DEFAULT_NAME = "dummy user";

    public UserDummy()
    : this(1)
    {
    }

    public UserDummy(ulong id, string username = DEFAULT_NAME)
    {
        Id = id;
        Username = username;
    }

    public ulong Id { get; }

    public string Username { get; }

    public string AvatarId => $"avatar {Username}";

    public string Discriminator => $"descrim {Username}";

    public ushort DiscriminatorValue => (ushort)Id;

    public bool IsBot => false;

    public bool IsWebhook => false;

    public UserProperties? PublicFlags => throw new NotImplementedException();

    public string GlobalName => $"global {Username}";

    public DateTimeOffset CreatedAt => throw new NotImplementedException();

    public string Mention => $"@mention {Username}";

    public UserStatus Status => throw new NotImplementedException();

    public IReadOnlyCollection<ClientType> ActiveClients => throw new NotImplementedException();

    public IReadOnlyCollection<IActivity> Activities => throw new NotImplementedException();

    public string GetAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
    {
        return "https://google.com";
    }

    public string GetDefaultAvatarUrl()
    {
        throw new NotImplementedException();
    }

    public Task<IDMChannel> CreateDMChannelAsync(RequestOptions options = null)
    {
        throw new NotImplementedException();
    }
}