using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using FacturacionSystem.Models;
using FacturacionSystem.DataAccess;

namespace FacturacionSystem.Repositories
{
    public class ProductoRepository
    {
        public void Create(Producto producto)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            
            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Productos (Nombre, Descripcion, Precio, IVA, Activo)
                VALUES (@nombre, @descripcion, @precio, @iva, @activo)";
            
            command.Parameters.AddWithValue("@nombre", producto.Nombre);
            command.Parameters.AddWithValue("@descripcion", producto.Descripcion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@precio", producto.Precio);
            command.Parameters.AddWithValue("@iva", producto.IVA);
            command.Parameters.AddWithValue("@activo", producto.Activo ? 1 : 0);
            
            command.ExecuteNonQuery();
        }

        public List<Producto> GetAll()
        {
            var productos = new List<Producto>();
            
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Productos WHERE Activo = 1 ORDER BY Nombre";
            
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(Producto.FromReader(reader));
            }
            
            return productos;
        }

        public Producto GetById(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Productos WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            
            using var reader = command.ExecuteReader();
            return reader.Read() ? Producto.FromReader(reader) : null;
        }

        public void Update(Producto producto)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            
            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Productos 
                SET Nombre = @nombre,
                    Descripcion = @descripcion,
                    Precio = @precio,
                    IVA = @iva,
                    Activo = @activo
                WHERE Id = @id";
            
            command.Parameters.AddWithValue("@id", producto.Id);
            command.Parameters.AddWithValue("@nombre", producto.Nombre);
            command.Parameters.AddWithValue("@descripcion", producto.Descripcion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@precio", producto.Precio);
            command.Parameters.AddWithValue("@iva", producto.IVA);
            command.Parameters.AddWithValue("@activo", producto.Activo ? 1 : 0);
            
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Productos SET Activo = 0 WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            
            command.ExecuteNonQuery();
        }
    }
}