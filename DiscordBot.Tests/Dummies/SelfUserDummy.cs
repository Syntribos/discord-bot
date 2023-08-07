using Discord;

namespace DiscordBot.Tests.Dummies;

public class SelfUserDummy : UserDummy, ISelfUser
#nullable disable
{
    public SelfUserDummy(ulong id, string name)
        : base(id, name)
    {
    }

    public Task ModifyAsync(Action<SelfUserProperties> func, RequestOptions options = null)
    {
        throw new NotImplementedException();
    }

    public string Email => throw new NotImplementedException();

    public bool IsVerified => throw new NotImplementedException();

    public bool IsMfaEnabled => throw new NotImplementedException();

    public UserProperties Flags => throw new NotImplementedException();

    public PremiumType PremiumType => throw new NotImplementedException();

    public string Locale => throw new NotImplementedException();
}