using Discord;

namespace DiscordBot.Tests.Dummies;

public class BanDummy : IBan
{
    public BanDummy()
    : this(new UserDummy())
    {
    }
    
    public BanDummy(string reason)
    : this(new UserDummy(), reason)
    {
    }
    
    public BanDummy(IUser user)
    : this(user, "Poopa alt")
    {
    }

    public BanDummy(IUser user, string reason)
    {
        User = user;
        Reason = reason;
    }

    public IUser User { get; }

    public string Reason { get; }
}