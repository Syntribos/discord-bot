using DiscordBot.Services;
using DiscordBot.Services.Youtube;

using Discord;
using Discord.Interactions;

namespace DiscordBot.Commands;

public class YoutubeCommandModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IAudioService _audioService;
    private readonly IYoutubeService _youtubeService;
    private readonly IVideoToAudioConverter _videoToAudioConverter;
    private readonly CancellationTokenSource _tokenSource;

    public YoutubeCommandModule(IAudioService audioService, IYoutubeService youtubeService, IVideoToAudioConverter videoToAudioConverter)
    {
        _audioService = audioService ?? throw new ArgumentNullException(nameof(audioService));
        _youtubeService = youtubeService ?? throw new ArgumentNullException(nameof(youtubeService));
        _videoToAudioConverter = videoToAudioConverter ?? throw new ArgumentNullException(nameof(videoToAudioConverter));
        _tokenSource = new CancellationTokenSource();
    }

    [SlashCommand("play", "asd")]
    public async Task PlaySongInChannel(string args)
    {
        if (!_audioService.IsBotConnectedToGuild(Context.Guild))
        {
            await Context.Channel.SendMessageAsync("Bot must be connected to a voice channel first before playing.");
            return;
        }

        var results = await _youtubeService.Search(args, 0);
    }

    [SlashCommand("join", "Joins the executing user's current voice channel in preparation for audio playback", false, RunMode.Async)]
    public async Task Join()
    {
        await _audioService.JoinAudio(Context.Guild, (Context.User as IVoiceState).VoiceChannel);
    }

    [SlashCommand("leave", "Leaves the current audio channel")]
    public async Task Leave()
    {
        await _audioService.LeaveAudio(Context.Guild);
    }

    [SlashCommand("skip", "Skips the currently playing song")]
    public async Task Skip()
    {
        await Context.Channel.SendMessageAsync("Skipping current song...");
        _tokenSource.Cancel();
    }

    private string FormatDuration(TimeSpan duration)
    {
        if (duration < TimeSpan.FromSeconds(60))
        {
            return duration.ToString("ss");
        }
        else if (duration > TimeSpan.FromMinutes(60))
        {
            return duration.ToString(@"hh\:mm\:ss");
        }

        return duration.ToString(@"mm\:ss");
    }
}
