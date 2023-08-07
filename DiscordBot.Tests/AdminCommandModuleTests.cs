using DiscordBot.Tests.Dummies;

using Discord;
using DiscordBot.Commands.Runners;
using Moq;
using Xunit;

namespace DiscordBot.Tests;

public class AdminCommandModuleTests
{
    private readonly MockRepository _repo;

    public AdminCommandModuleTests()
    {
        _repo = new MockRepository(MockBehavior.Strict);
    }

    [Fact]
    public async Task BanCommand_ExistingUser_BansUser()
    {
        const ulong targetId = 123;
        const ulong senderId = 666;
        const PurgeDays daysToPurge = PurgeDays.Five;

        var dummyUser = new UserDummy(targetId, "non guild user");
        var dummyGuild = new GuildDummy();
        var dummyGuildUser = new GuildUserDummy(targetId, "guild user", dummyGuild);
        var dummySelfUser = new SelfUserDummy(senderId, "banner");
        dummyGuild.AddUser(dummyGuildUser);
        
        var mockContext = _repo.Create<IInteractionContext>();
        var mockInteraction = _repo.Create<IDiscordInteraction>();
        var mockClient = _repo.Create<IDiscordClient>();
        
        mockContext.SetupGet(x => x.Guild).Returns(dummyGuild);
        mockContext.SetupGet(x => x.Interaction).Returns(mockInteraction.Object);
        mockContext.SetupGet(x => x.Client).Returns(mockClient.Object);
        mockClient.Setup(x => x.CurrentUser).Returns(dummySelfUser);

        Embed[]? embeds = null;
        mockInteraction.Setup(x => x.RespondAsync(null, It.IsAny<Embed[]>(), false, false, null, null, null, null))
            .Returns(Task.CompletedTask)
            .Callback<string, Embed[], bool, bool, AllowedMentions, MessageComponent, Embed, RequestOptions>(
                (_, x, _, _, _, _, _, _) => embeds = x);

        var module = new AdminCommandRunner();

        await module.BanUser(mockContext.Object, dummyUser, daysToPurge, "felt like it");

        Assert.NotNull(embeds);
        Assert.Single(embeds);

        var banEmbed = embeds[0];
        Assert.NotNull(banEmbed);
        Assert.Equal((new EmbedBuilder().WithAuthor(dummySelfUser).Author).Name, banEmbed.Author?.Name);
        Assert.Single(dummyGuild.BannedUsers);
        Assert.Equal(targetId, dummyGuild.BannedUsers.First().Id);
        Assert.Empty(dummyGuild.GuildUsers);
        
        _repo.VerifyAll();
    }
    
    [Fact]
    public async Task BanCommand_NonExistingUser_DoesntBanUser()
    {
        const ulong guildUserId = 123;
        const ulong nonexistentUserId = 459867;

        var dummyUser = new UserDummy(nonexistentUserId, "non guild user");
        var dummyGuild = new GuildDummy();
        var dummyGuildUser = new GuildUserDummy(guildUserId, "guild user", dummyGuild);
        dummyGuild.AddUser(dummyGuildUser);

        var mockContext = _repo.Create<IInteractionContext>();
        var mockInteraction = _repo.Create<IDiscordInteraction>();

        mockContext.SetupGet(x => x.Guild).Returns(dummyGuild);
        mockContext.SetupGet(x => x.Interaction).Returns(mockInteraction.Object);

        mockInteraction.Setup(x => x.RespondAsync(It.IsAny<string>(), null, false, false, null, null, null, null))
            .Returns(Task.CompletedTask);

        var module = new AdminCommandRunner();

        await module.BanUser(mockContext.Object, dummyUser, PurgeDays.None, "felt like it");

        Assert.Empty(dummyGuild.BannedUsers);
        Assert.Single(dummyGuild.GuildUsers.Where(x => x.Id.Equals(guildUserId)));

        _repo.VerifyAll();
    }
}