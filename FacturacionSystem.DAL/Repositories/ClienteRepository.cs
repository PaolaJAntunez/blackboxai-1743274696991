using System;
using System.Data;
using System.Data.OleDb;
using FacturacionSystem.Models;
using System.Collections.Generic;

namespace FacturacionSystem.DAL.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>
    {
        protected override string TableName => "Clientes";
        protected override string PrimaryKey => "Id";

        protected override (string, OleDbParameter[]) GetInsertQuery(Cliente cliente)
        {
            var query = @"INSERT INTO Clientes 
                        (Nombre, Telefono, Direccion, Email, RUC) 
                        VALUES 
                        (@Nombre, @Telefono, @Direccion, @Email, @RUC)";

            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Nombre", cliente.Nombre),
                new OleDbParameter("@Telefono", cliente.Telefono),
                new OleDbParameter("@Direccion", cliente.Direccion),
                new OleDbParameter("@Email", cliente.Email ?? (object)DBNull.Value),
                new OleDbParameter("@RUC", cliente.RUC ?? (object)DBNull.Value)
            };

            return (query, parameters);
        }

        protected override (string, OleDbParameter[]) GetUpdateQuery(Cliente cliente)
        {
            var query = @"UPDATE Clientes SET 
                        Nombre = @Nombre,
                        Telefono = @Telefono,
                        Direccion = @Direccion,
                        Email = @Email,
                        RUC = @RUC
                        WHERE Id = @Id";

            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Nombre", cliente.Nombre),
                new OleDbParameter("@Telefono", cliente.Telefono),
                new OleDbParameter("@Direccion", cliente.Direccion),
                new OleDbParameter("@Email", cliente.Email ?? (object)DBNull.Value),
                new OleDbParameter("@RUC", cliente.RUC ?? (object)DBNull.Value),
                new OleDbParameter("@Id", cliente.Id)
            };

            return (query, parameters);
        }

        protected override Cliente DataTableToObject(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0) return null;

            var row = dataTable.Rows[0];
            return new Cliente(
                id: Convert.ToInt32(row["Id"]),
                nombre: row["Nombre"].ToString(),
                telefono: row["Telefono"].ToString(),
                direccion: row["Direccion"].ToString(),
                email: row["Email"]?.ToString(),
                ruc: row["RUC"]?.ToString()
            );
        }

        protected override List<Cliente> DataTableToList(DataTable dataTable)
        {
            var clientes = new List<Cliente>();
            foreach (DataRow row in dataTable.Rows)
            {
                clientes.Add(new Cliente(
                    id: Convert.ToInt32(row["Id"]),
                    nombre: row["Nombre"].ToString(),
                    telefono: row["Telefono"].ToString(),
                    direccion: row["Direccion"].ToString(),
                    email: row["Email"]?.ToString(),
                    ruc: row["RUC"]?.ToString()
                ));
            }
            return clientes;
        }

        public List<Cliente> Search(string term)
        {
            var query = $"SELECT * FROM {TableName} WHERE Nombre LIKE @Term OR RUC LIKE @Term";
            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Term", $"%{term}%")
            };
            var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);
            return DataTableToList(dataTable);
        }
    }
}