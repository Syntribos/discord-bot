using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Discord;
using Discord.Audio;
using Discord.Audio.Streams;

namespace DiscordBot.Utilities
{
    public class AudioService : IAudioService
    {
        private readonly ConcurrentDictionary<ulong, IAudioClient> _connectedChannels = new ConcurrentDictionary<ulong, IAudioClient>();

        public bool IsBotConnectedToGuild(IGuild guild)
        {
            return _connectedChannels.ContainsKey(guild.Id);
        }

        public async Task JoinAudio(IGuild guild, IVoiceChannel target)
        {
            if (target.Guild.Id != guild.Id || _connectedChannels.ContainsKey(guild.Id))
            {
                return;
            }

            var audioClient = await target.ConnectAsync();

            if (_connectedChannels.TryAdd(guild.Id, audioClient))
            {
                //Maybe add logging later
            }
        }

        public async Task LeaveAudio(IGuild guild)
        {
            if (_connectedChannels.TryRemove(guild.Id, out IAudioClient client))
            {
                await client.StopAsync();
            }
        }

        public async Task SendAudioAsync(IGuild guild, IMessageChannel channel, string path, CancellationToken token)
        {
            if (!File.Exists(path) || !_connectedChannels.TryGetValue(guild.Id, out IAudioClient client))
            {
                return;
            }

            using (var ffmpeg = CreateProcess(path))
            using (var stream = client.CreatePCMStream(AudioApplication.Music))
            using (var bws = new BufferedWriteStream(stream, client, 500, token))
            {
                try
                {
                    var output = ffmpeg.StandardOutput.BaseStream;
                    await ffmpeg.StandardOutput.BaseStream.CopyToAsync(stream);
                }
                catch (Exception e)
                {
                    await stream.FlushAsync();
                }
            }
        }

        private Process CreateProcess(string path)
        {
            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $@"-i ""{path}"" -ac 2 -f s16le -ar 48000 pipe:1",
                RedirectStandardOutput = true,
                UseShellExecute = false
            });
        }
    }
}
