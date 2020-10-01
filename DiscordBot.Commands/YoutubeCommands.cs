using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

using DiscordBot.Utilities;

using Discord.Commands;
using Discord;

namespace DiscordBot.Commands
{
    public class YoutubeCommands : ModuleBase<ICommandContext>
    {
       
        private readonly IAudioService _audioService;
        private readonly IYoutubeService _youtubeService;
        private readonly IVideoToAudioConverter _videoToAudioConverter;
        private readonly CancellationTokenSource _tokenSource;

        public YoutubeCommands(IAudioService audioService, IYoutubeService youtubeService, IVideoToAudioConverter videoToAudioConverter)
        {
            _audioService = audioService;
            _youtubeService = youtubeService;
            _videoToAudioConverter = videoToAudioConverter;
            _tokenSource = new CancellationTokenSource();
        }

        [Command("Play")]
        public async Task PlaySongInChannel([Remainder]string args)
        {
            if (!_audioService.IsBotConnectedToGuild(Context.Guild))
            {
                await Context.Channel.SendMessageAsync("Bot must be connected to a voice channel first before playing.");
                return;
            }

            var tempDir = new TempDirectory();

            try
            {
                var videoPaths = await _youtubeService.DownloadVideos(tempDir.DirectoryPath, args);
                var audioPath = _videoToAudioConverter.ConvertVideoToMp3(videoPaths);

                await _audioService.SendAudioAsync(Context.Guild, Context.Channel, audioPath.First(), _tokenSource.Token);

                await Context.Channel.SendMessageAsync($"Playing song.");
            }
            catch(Exception e)
            {
            }
            finally
            {
                tempDir.Dispose();
            }
        }

        [Command("Join", RunMode = RunMode.Async)]
        public async Task Join()
        {
            await _audioService.JoinAudio(Context.Guild, (Context.User as IVoiceState).VoiceChannel);
        }

        [Command("Leave", RunMode = RunMode.Async)]
        public async Task Leave()
        {
            await _audioService.LeaveAudio(Context.Guild);
        }

        [Command("Skip", RunMode = RunMode.Async)]
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
}
