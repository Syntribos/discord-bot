﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Data
{
    public class CommandsRepository : DataRepository
    {
        public CommandsRepository(string databasePath)
            : base(databasePath)
        {
            _initialized = true;
        }

        public Dictionary<string, string> GetAllCustomCommands()
        {
            var customCommands = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            try
            {
                _connection.Open();
                var command = _connection.CreateCommand();
                command.CommandText =
                    @"
                        SELECT command, response
                        FROM customCommands
                    ";

                var reader = command.ExecuteReader();

                while(reader.Read())
                {
                    customCommands.Add(reader.GetString(0), reader.GetString(1));
                }
            }
            catch (Exception e)
            {
            }

            return customCommands;
        }

        public bool DeleteCommand(string command)
        {
            try
            {
                return ExecuteBooleanNonQuery(
                    @"
                        DELETE FROM customCommands
                        WHERE command = @command
                    ",
                    new List<Tuple<string, string>>
                    {
                        CreateParam("@command", command)
                    }
                );
            }
            catch
            {
                return false;
            }
        }

        public bool AddNewCommand(string command, string response)
        {
            try
            {
                return ExecuteBooleanNonQuery(
                    @"
                        INSERT OR REPLACE
                        INTO customCommands (command, response)
                        VALUES (@command, @response)
                    ",
                    new List<Tuple<string, string>>
                    {
                        CreateParam("@command", command),
                        CreateParam("@response", response)
                    });
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
