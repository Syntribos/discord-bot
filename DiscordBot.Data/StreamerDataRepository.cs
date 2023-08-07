using Microsoft.Data.Sqlite;

using DiscordBot.DataModels.Twitch;
using DiscordBot.DataModels;

namespace DiscordBot.Data
{
    public class StreamerDataRepository : DataRepository
    {
        private StreamerFactory _streamerFactory;

        public StreamerDataRepository(IDatabaseInfo databaseInfo, StreamerFactory streamerFactory)
            : base(databaseInfo)
        {
            _streamerFactory = streamerFactory ?? throw new ArgumentNullException(nameof(streamerFactory));
        }

        public string DefaultAnnounceMessage
        {
            get
            {
                return _streamerFactory.DefaultAnnounce;
            }
        }

        public bool AddStreamer(string streamer, string liveMessage)
        {
            EnsureInitialization();

            try
            {
                return ExecuteBooleanNonQuery(
                    @"
                        INSERT OR REPLACE
                        INTO streamers (name, liveMessage)
                        VALUES (@name, @liveMessage)
                    ",
                    new List<Tuple<string, string>>
                    {
                        CreateParam("@name",streamer),
                        CreateParam("@liveMessage", liveMessage)
                    }
                );
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveStreamer(string streamer)
        {
            EnsureInitialization();

            try
            {
                return ExecuteBooleanNonQuery(
                    @"
                        DELETE FROM streamers
                        WHERE name = @streamer
                    ",
                    new List<Tuple<string, string>>
                    {
                        CreateParam("@streamer", streamer)
                    }
                );
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Streamer> GetStreamers()
        {
            EnsureInitialization();

            try
            {
                _connection.Open();
                var command = _connection.CreateCommand();
                command.CommandText =
                    @"
                    SELECT name, liveMessage
                    FROM streamers
                ";

                var streamers = new List<Streamer>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        streamers.Add(_streamerFactory.Create(
                            reader.GetString(0),
                            reader.GetString(1)));
                    }
                }

                return streamers;
            }
            finally
            {
                _connection.Close();
            }
            
        }

        public bool UpdateDefaultLiveMessage(string message)
        {
            EnsureInitialization();
            
            try
            {
                return ExecuteBooleanNonQuery(
                    @"
                        INSERT OR REPLACE INTO defaults
                        (name, value)
                        VALUES (@name, @message)
                    ",
                    new List<Tuple<string, string>>
                    {
                        CreateParam("@name", "stream_announce"),
                        CreateParam("@message", message),
                    }
                );
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateStreamerLiveMessage(string streamer, string message)
        {
            EnsureInitialization();

            try
            {
                _connection.Open();
                var command = _connection.CreateCommand();
            }
            finally
            {
                _connection.Close();
            }

            return false;
        }

        public void Initialize()
        {
            try
            {
                try
                {
                    _connection.Open();
                }
                catch (SqliteException)
                {
                    throw new Exception($"FATAL: Could not connect to streamer data repository. Ensure the database file exists at {_databaseInfo.DatabasePath} and restart the bot.");
                }

                var command = _connection.CreateCommand();
                command.CommandText =
                    @"
                        SELECT value
                        FROM defaults
                        WHERE name = @name
                    ";
                command.Parameters.AddWithValue("@name", "stream_announce");

                try
                {
                    var reader = command.ExecuteReader();
                    reader.Read();
                    var value = reader.GetString(0);

                    if (string.IsNullOrEmpty(value))
                    {
                        _streamerFactory = new StreamerFactory();
                    }
                    else
                    {
                        _streamerFactory = new StreamerFactory(value);
                    }
                }
                catch (Exception)
                {
                    _streamerFactory = new StreamerFactory();
                }
            }
            finally
            {
                _connection.Close();
            }

            _initialized = true;
        }
    }
}
