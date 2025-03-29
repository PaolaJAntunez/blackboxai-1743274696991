using System;
using System.Data;
using System.Data.OleDb;
using FacturacionSystem.Models;

namespace FacturacionSystem.DAL.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>
    {
        protected override string TableName => "Usuarios";
        protected override string PrimaryKey => "ID";

        public Usuario GetByNombre(string nombre)
        {
            var query = "SELECT * FROM Usuarios WHERE Nombre = @Nombre";
            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Nombre", nombre)
            };

            var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);
            return DataTableToObject(dataTable);
        }

        public int UpdatePassword(int usuarioId, string newPassword)
        {
            var query = "UPDATE Usuarios SET Contraseña = @Password WHERE ID = @ID";
            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Password", newPassword),
                new OleDbParameter("@ID", usuarioId)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        protected override (string, OleDbParameter[]) GetInsertQuery(Usuario usuario)
        {
            var query = @"INSERT INTO Usuarios 
                        (Nombre, Contraseña, Rol) 
                        VALUES 
                        (@Nombre, @Contraseña, @Rol)";

            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Nombre", usuario.Nombre),
                new OleDbParameter("@Contraseña", usuario.Contraseña),
                new OleDbParameter("@Rol", usuario.Rol)
            };

            return (query, parameters);
        }

        protected override (string, OleDbParameter[]) GetUpdateQuery(Usuario usuario)
        {
            var query = @"UPDATE Usuarios SET 
                        Nombre = @Nombre,
                        Rol = @Rol
                        WHERE ID = @ID";

            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Nombre", usuario.Nombre),
                new OleDbParameter("@Rol", usuario.Rol),
                new OleDbParameter("@ID", usuario.ID)
            };

            return (query, parameters);
        }

        protected override Usuario DataTableToObject(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0) return null;

            var row = dataTable.Rows[0];
            return new Usuario(
                id: Convert.ToInt32(row["ID"]),
                nombre: row["Nombre"].ToString(),
                contraseña: row["Contraseña"].ToString(),
                rol: row["Rol"].ToString()
            );
        }

        protected override List<Usuario> DataTableToList(DataTable dataTable)
        {
            var usuarios = new List<Usuario>();
            foreach (DataRow row in dataTable.Rows)
            {
                usuarios.Add(new Usuario(
                    id: Convert.ToInt32(row["ID"]),
                    nombre: row["Nombre"].ToString(),
                    contraseña: row["Contraseña"].ToString(),
                    rol: row["Rol"].ToString()
                ));
            }
            return usuarios;
        }
    }
}