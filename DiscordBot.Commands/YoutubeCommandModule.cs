using DiscordBot.Services;
using DiscordBot.Services.Youtube;

using Discord;
using Discord.Audio;
using Discord.Interactions;
using DiscordBot.Utilities;

namespace DiscordBot.Commands;

public class YoutubeCommandModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IAudioService _audioService;
    private readonly IYoutubeService _youtubeService;
    // private readonly IVideoToAudioConverter _videoToAudioConverter;
    private readonly CancellationTokenSource _tokenSource;

    private IAudioClient? _audioClient;

    public YoutubeCommandModule(IAudioService audioService, IYoutubeService youtubeService)
    {
        _audioService = audioService ?? throw new ArgumentNullException(nameof(audioService));
        _youtubeService = youtubeService ?? throw new ArgumentNullException(nameof(youtubeService));
        _tokenSource = new CancellationTokenSource();
    }

    [SlashCommand("play", "asd", runMode: RunMode.Async)]
    public async Task PlaySongInChannel(string args)
    {
        if (!_audioService.IsBotConnectedToGuild(Context.Guild))
        {
            await RespondAsync("Bot must be connected to a voice channel first before playing.");
            return;
        }

        var results = (await _youtubeService.Search(args, 0)).ToList();
        using var tmpDir = new TempDirectory(@"C:\Users\Jess\Desktop\tempy");
        var vid = results?.First();

        await RespondAsync("Grabbing video...");
        var video = results?.FirstOrDefault() ?? throw new ArgumentNullException(nameof(results));
        var audioPath =
            await _youtubeService.DownloadVideo(VideoDownloaderFactory.CreateDownloader(tmpDir), video);

        var msg = await Context.Channel.SendMessageAsync($"Playing {vid}"); 
        await _audioService.SendAudioAsync(Context.Guild, audioPath, _tokenSource.Token);
    }

    [SlashCommand("join", "Joins the executing user's current voice channel in preparation for audio playback", runMode: RunMode.Async)]
    public async Task Join(IVoiceChannel? channel = null)
    {
        channel ??= (Context.User as IGuildUser)?.VoiceChannel;
        if (channel == null)
        {
            await RespondAsync("User must be in a voice channel, or a voice channel must be passed as an argument.");
            return;
        }

        // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
        try
        {
            if (!await _audioService.TryJoinAudio(Context.Guild, channel))
            {
                await RespondAsync($"Couldn't connect to channel {channel.Name}.");
                return;
            }
        }
        catch (Exception e)
        {
            await RespondAsync($"Couldn't connect to channel {channel.Name}.");
            Console.WriteLine(e.Message);

            return;
        }
        
        await RespondAsync("Joined voice channel.");
    }

    [SlashCommand("leave", "Leaves the current audio channel")]
    public async Task Leave()
    {
        await RespondAsync("Leaving voice channel.");
        await _audioService.LeaveAudio(Context.Guild);
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
