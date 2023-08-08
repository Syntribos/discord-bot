using System.Collections.Concurrent;
using System.Diagnostics;

using Discord;
using Discord.Audio;
using Discord.Audio.Streams;

namespace DiscordBot.Services
{
    public class AudioService : IAudioService
    {
        private readonly ConcurrentDictionary<ulong, IAudioClient> _connectedChannels = new();

        public bool IsBotConnectedToGuild(IGuild guild)
        {
            return _connectedChannels.ContainsKey(guild.Id);
        }

        public async Task<bool> TryJoinAudio(IGuild guild, IVoiceChannel target)
        {
            try
            {
                if (target.Guild.Id != guild.Id || _connectedChannels.ContainsKey(guild.Id))
                {
                    return false;
                }

                var audioClient = await target.ConnectAsync();

                if (_connectedChannels.TryAdd(guild.Id, audioClient))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return false;
        }

        public async Task<bool> LeaveAudio(IGuild guild)
        {
            try
            {
                if (_connectedChannels.TryRemove(guild.Id, out var client))
                {
                    await client.StopAsync();
                    client.Dispose();

                    return true;
                }
            }
            catch (Exception e)
            {
                await Console.Error.WriteLineAsync($"Problem leaving voice channel in guild {guild.Id}: {e.Message}");
            }

            return false;
        }

        public async Task SendAudioAsync(IGuild guild, string path, CancellationToken token)
        {
            if (!File.Exists(path) || !_connectedChannels.TryGetValue(guild.Id, out var client))
            {
                return;
            }

            using var ffmpeg = CreateProcess(path);
            await using var output = ffmpeg.StandardOutput.BaseStream;
            await using var stream = client.CreatePCMStream(AudioApplication.Mixed);
            try
            {
                await output.CopyToAsync(stream, token);
            }
            finally
            {
                await stream.FlushAsync(CancellationToken.None);   
            }
        }

        private static Process CreateProcess(string path)
        {
            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $@"-i ""{path}"" -ac 2 -f s16le -ar 48000 pipe:1",
                RedirectStandardOutput = true,
                UseShellExecute = false
            }) ?? throw new Exception("Something went wrong creating FFMPEG process.");
        }
    }
}
