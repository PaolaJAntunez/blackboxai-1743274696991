using System;
using System.Data;
using System.Data.OleDb;

namespace FacturacionSystem.DAL
{
    public static class DatabaseHelper
    {
        private static readonly string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database/FacturaDB.accdb;Persist Security Info=False;";

        public static OleDbConnection GetConnection()
        {
            var connection = new OleDbConnection(connectionString);
            
            try
            {
                connection.Open();
                return connection;
            }
            catch (OleDbException ex)
            {
                throw new Exception("Error al conectar con la base de datos: " + ex.Message);
            }
        }

        public static DataTable ExecuteQuery(string query, OleDbParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            using (var command = new OleDbCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                DataTable dataTable = new DataTable();
                new OleDbDataAdapter(command).Fill(dataTable);
                return dataTable;
            }
        }

        public static int ExecuteNonQuery(string query, OleDbParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            using (var command = new OleDbCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string query, OleDbParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            using (var command = new OleDbCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command.ExecuteScalar();
            }
        }
    }
}