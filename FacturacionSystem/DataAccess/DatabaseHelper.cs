using Microsoft.Data.Sqlite;
using System.IO;

namespace FacturacionSystem.DataAccess
{
    public static class DatabaseHelper
    {
        private const string DatabaseFile = "FacturacionDB.db";
        private static string ConnectionString => $"Data Source={DatabaseFile}";

        public static void InitializeDatabase()
        {
            if (!File.Exists(DatabaseFile))
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                
                var command = connection.CreateCommand();
                command.CommandText = File.ReadAllText("DataAccess/DatabaseSchema.sql");
                command.ExecuteNonQuery();
            }
        }

        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(ConnectionString);
        }
    }
}