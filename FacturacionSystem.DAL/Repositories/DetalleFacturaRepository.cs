using System;
using System.Data;
using System.Data.OleDb;
using FacturacionSystem.Models;
using System.Collections.Generic;

namespace FacturacionSystem.DAL.Repositories
{
    public class DetalleFacturaRepository
    {
        public int Insert(DetalleFactura detalle, int facturaId)
        {
            var query = @"INSERT INTO DetalleFactura 
                        (FacturaId, ProductoId, Cantidad, PrecioUnitario, Subtotal, Impuesto, Total) 
                        VALUES 
                        (@FacturaId, @ProductoId, @Cantidad, @PrecioUnitario, @Subtotal, @Impuesto, @Total)";

            var parameters = new OleDbParameter[] {
                new OleDbParameter("@FacturaId", facturaId),
                new OleDbParameter("@ProductoId", detalle.ProductoId),
                new OleDbParameter("@Cantidad", detalle.Cantidad),
                new OleDbParameter("@PrecioUnitario", detalle.PrecioUnitario),
                new OleDbParameter("@Subtotal", detalle.Subtotal),
                new OleDbParameter("@Impuesto", detalle.Impuesto),
                new OleDbParameter("@Total", detalle.Total)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public List<DetalleFactura> GetByFacturaId(int facturaId)
        {
            var query = @"SELECT df.*, p.Nombre as ProductoNombre, p.Codigo as ProductoCodigo 
                        FROM DetalleFactura df
                        INNER JOIN Productos p ON df.ProductoId = p.Id
                        WHERE df.FacturaId = @FacturaId";

            var parameters = new OleDbParameter[] {
                new OleDbParameter("@FacturaId", facturaId)
            };

            var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);
            return DataTableToList(dataTable);
        }

        public int DeleteByFacturaId(int facturaId)
        {
            var query = "DELETE FROM DetalleFactura WHERE FacturaId = @FacturaId";
            var parameters = new OleDbParameter[] {
                new OleDbParameter("@FacturaId", facturaId)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        private List<DetalleFactura> DataTableToList(DataTable dataTable)
        {
            var detalles = new List<DetalleFactura>();
            foreach (DataRow row in dataTable.Rows)
            {
                detalles.Add(new DetalleFactura
                {
                    Id = Convert.ToInt32(row["Id"]),
                    FacturaId = Convert.ToInt32(row["FacturaId"]),
                    ProductoId = Convert.ToInt32(row["ProductoId"]),
                    ProductoNombre = row["ProductoNombre"].ToString(),
                    ProductoCodigo = row["ProductoCodigo"].ToString(),
                    Cantidad = Convert.ToInt32(row["Cantidad"]),
                    PrecioUnitario = Convert.ToDecimal(row["PrecioUnitario"]),
                    Subtotal = Convert.ToDecimal(row["Subtotal"]),
                    Impuesto = Convert.ToDecimal(row["Impuesto"]),
                    Total = Convert.ToDecimal(row["Total"])
                });
            }
            return detalles;
        }
    }
}