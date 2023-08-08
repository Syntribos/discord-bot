using Discord;

namespace DiscordBot.Services
{
    public interface IAudioService
    {
        bool IsBotConnectedToGuild(IGuild guild);

        Task<bool> TryJoinAudio(IGuild guild, IVoiceChannel target);

        Task<bool> LeaveAudio(IGuild guild);

        Task SendAudioAsync(IGuild guild, string path, CancellationToken token);
    }
}