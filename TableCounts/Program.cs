using System;
using Microsoft.Data.Sqlite;

namespace TableCounts
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbPath = "C:\\Users\\Mahmoud\\source\\repos\\FurnitureStore\\App_Data\\furniture_store.db";
            var connString = $"Data Source={dbPath}";
            using var connection = new SqliteConnection(connString);
            connection.Open();

            // Get list of user tables (exclude sqlite_sequence, etc.)
            using var cmdTables = connection.CreateCommand();
            cmdTables.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";
            using var reader = cmdTables.ExecuteReader();
            while (reader.Read())
            {
                var tableName = reader.GetString(0);
                using var countCmd = connection.CreateCommand();
                countCmd.CommandText = $"SELECT COUNT(*) FROM [{tableName}]";
                var count = Convert.ToInt64(countCmd.ExecuteScalar());
                Console.WriteLine($"{tableName}: {count}");
            }
        }
    }
}
