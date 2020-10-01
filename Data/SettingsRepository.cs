using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace DiscordBot.Data
{
    public class SettingsRepository : DataRepository
    {
        public SettingsRepository(string databasePath)
            : base(databasePath)
        {
        }

        public bool ChangePrefix(string newPrefix)
        {
            try
            {
                return ExecuteBooleanNonQuery(
                    @"
                        INSERT OR REPLACE
                        INTO defaults (name, value)
                        VALUES (@name, @newPrefix)
                    ",
                    new List<Tuple<string, string>>
                    {
                        CreateParam("@name", "command_prefix"),
                        CreateParam("@newPrefix", newPrefix)
                    }
                );
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string InitializeAndGetPrefix()
        {
            try
            {
                try
                {
                    _connection.Open();
                }
                catch (SqliteException)
                {
                    throw new Exception($"FATAL: Could not connect to settings data repository. Ensure the database file exists at {_connectionString} and restart the bot.");
                }

                var command = _connection.CreateCommand();
                command.CommandText =
                    @"
                        SELECT value
                        FROM defaults
                        WHERE name = @name
                    ";
                command.Parameters.AddWithValue("@name", "command_prefix");

                try
                {
                    var reader = command.ExecuteReader();
                    reader.Read();
                    var value = reader.GetString(0);

                    if (string.IsNullOrEmpty(value))
                    {
                        throw new ArgumentNullException();
                    }
                    else
                    {
                        return value;
                    }
                }
                catch (Exception)
                {
                    throw new ArgumentNullException();
                }
            }
            finally
            {
                _connection.Close();
                _initialized = true;
            }
        }
    }
}
