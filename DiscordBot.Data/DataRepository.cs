using Microsoft.Data.Sqlite;

using DiscordBot.DataModels;

namespace DiscordBot.Data
{
    public class DataRepository : IDisposable
    {
        protected readonly IDatabaseInfo _databaseInfo;
        protected readonly SqliteConnection _connection;
        protected bool _initialized;

        public DataRepository(IDatabaseInfo databaseInfo)
        {
            _databaseInfo = databaseInfo;
            _connection = new SqliteConnection(_databaseInfo.ConnectionString);
            _initialized = false;

            if (!File.Exists(_databaseInfo.DatabasePath))
            {
                CreateFile(_databaseInfo.DatabasePath);
                InitializeDatabase();
            }
        }

        public bool ExecuteBooleanNonQuery(string query, List<Tuple<string, string>> parameters)
        {
            return ExecuteBooleanNonQuery(query, parameters, (x) => { return x > 0; });
        }

        public bool ExecuteBooleanNonQuery(string query, List<Tuple<string,string>> parameters, Func<int, bool> predicate)
        {
            try
            {
                _connection.Open();
                var command = _connection.CreateCommand();
                command.CommandText = query;

                foreach (var item in parameters)
                {
                    command.Parameters.AddWithValue(item.Item1, item.Item2);
                }

                return predicate(command.ExecuteNonQuery());
            }
            finally
            {
                _connection.Close();
            }
        }

        public Tuple<string, string> CreateParam(string str1, string str2)
        {
            return Tuple.Create(str1, str2);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        protected void EnsureInitialization()
        {
            if (!_initialized)
            {
                throw new NotSupportedException("Data repository must be initialized before use.");
            }
        }

        private void CreateFile(string path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            var fi = new FileInfo(path) ?? throw new ArgumentException($"Couldn't create file in directory {path}");

            Directory.CreateDirectory(fi.Directory?.FullName ?? throw new ArgumentException($"Couldn't create file in directory {path}"));

            File.Create(Path.GetFileName(path));
        }

        private void InitializeDatabase()
        {
            try
            {
                ExecuteBooleanNonQuery(
                    @"
                        CREATE TABLE customCommands
                        (command TEXT PRIMARY KEY, response TEXT NOT NULL,
                        UNIQUE (command COLLATE NOCASE));

                        CREATE TABLE defaults
                        (name TEXT PRIMARY KEY, value TEXT NOT NULL);

                        INSERT OR REPLACE
                        INTO defaults (name, value)
                        VALUES (@stream_announce_name, @stream_announce_default),
                        (@command_prefix_name, @command_prefix_default);
                        

                        CREATE TABLE streamers
                        (name PRIMARY KEY, liveMessage,
                        UNIQUE (name COLLATE NOCASE));
                    ",
                    new List<Tuple<string, string>>
                    {
                        CreateParam("@stream_announce_name", "stream_announce"),
                        CreateParam("@stream_announce_default", "{0} just went live!"),
                        CreateParam("@command_prefix_name", "command_prefix"),
                        CreateParam("@command_prefix_default", "?"),
                    }
                );

            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
