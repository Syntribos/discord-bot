using System.Threading;
using System.Threading.Tasks;

using Discord;

namespace DiscordBot.Utilities
{
    public interface IAudioService
    {
        bool IsBotConnectedToGuild(IGuild guild);

        Task JoinAudio(IGuild guild, IVoiceChannel target);

        Task LeaveAudio(IGuild guild);

        Task SendAudioAsync(IGuild guild, IMessageChannel channel, string path, CancellationToken token);
    }
}
