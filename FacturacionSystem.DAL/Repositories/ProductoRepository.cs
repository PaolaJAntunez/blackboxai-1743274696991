using System;
using System.Data;
using System.Data.OleDb;
using FacturacionSystem.Models;
using System.Collections.Generic;

namespace FacturacionSystem.DAL.Repositories
{
    public class ProductoRepository : BaseRepository<Producto>
    {
        protected override string TableName => "Productos";
        protected override string PrimaryKey => "Id";

        protected override (string, OleDbParameter[]) GetInsertQuery(Producto producto)
        {
            var query = @"INSERT INTO Productos 
                        (Codigo, Nombre, Descripcion, Precio, Stock, Categoria, Activo) 
                        VALUES 
                        (@Codigo, @Nombre, @Descripcion, @Precio, @Stock, @Categoria, @Activo)";

            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Codigo", producto.Codigo),
                new OleDbParameter("@Nombre", producto.Nombre),
                new OleDbParameter("@Descripcion", producto.Descripcion ?? (object)DBNull.Value),
                new OleDbParameter("@Precio", producto.Precio),
                new OleDbParameter("@Stock", producto.Stock),
                new OleDbParameter("@Categoria", producto.Categoria ?? (object)DBNull.Value),
                new OleDbParameter("@Activo", producto.Activo)
            };

            return (query, parameters);
        }

        protected override (string, OleDbParameter[]) GetUpdateQuery(Producto producto)
        {
            var query = @"UPDATE Productos SET 
                        Codigo = @Codigo,
                        Nombre = @Nombre,
                        Descripcion = @Descripcion,
                        Precio = @Precio,
                        Stock = @Stock,
                        Categoria = @Categoria,
                        Activo = @Activo
                        WHERE Id = @Id";

            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Codigo", producto.Codigo),
                new OleDbParameter("@Nombre", producto.Nombre),
                new OleDbParameter("@Descripcion", producto.Descripcion ?? (object)DBNull.Value),
                new OleDbParameter("@Precio", producto.Precio),
                new OleDbParameter("@Stock", producto.Stock),
                new OleDbParameter("@Categoria", producto.Categoria ?? (object)DBNull.Value),
                new OleDbParameter("@Activo", producto.Activo),
                new OleDbParameter("@Id", producto.Id)
            };

            return (query, parameters);
        }

        protected override Producto DataTableToObject(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0) return null;

            var row = dataTable.Rows[0];
            return new Producto(
                id: Convert.ToInt32(row["Id"]),
                codigo: row["Codigo"].ToString(),
                nombre: row["Nombre"].ToString(),
                descripcion: row["Descripcion"]?.ToString(),
                precio: Convert.ToDecimal(row["Precio"]),
                stock: Convert.ToInt32(row["Stock"]),
                categoria: row["Categoria"]?.ToString()
            );
        }

        protected override List<Producto> DataTableToList(DataTable dataTable)
        {
            var productos = new List<Producto>();
            foreach (DataRow row in dataTable.Rows)
            {
                productos.Add(new Producto(
                    id: Convert.ToInt32(row["Id"]),
                    codigo: row["Codigo"].ToString(),
                    nombre: row["Nombre"].ToString(),
                    descripcion: row["Descripcion"]?.ToString(),
                    precio: Convert.ToDecimal(row["Precio"]),
                    stock: Convert.ToInt32(row["Stock"]),
                    categoria: row["Categoria"]?.ToString()
                ));
            }
            return productos;
        }

        public List<Producto> GetByCategory(string categoria)
        {
            var query = $"SELECT * FROM {TableName} WHERE Categoria = @Categoria AND Activo = true";
            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Categoria", categoria)
            };
            var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);
            return DataTableToList(dataTable);
        }

        public bool UpdateStock(int productoId, int cantidad)
        {
            var query = $"UPDATE {TableName} SET Stock = Stock + @Cantidad WHERE Id = @Id";
            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Cantidad", cantidad),
                new OleDbParameter("@Id", productoId)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}