using System;
using System.Data;
using System.Data.OleDb;
using FacturacionSystem.Models;
using System.Collections.Generic;

namespace FacturacionSystem.DAL.Repositories
{
    public class FacturaRepository : BaseRepository<Factura>
    {
        private readonly DetalleFacturaRepository _detalleRepo = new DetalleFacturaRepository();
        
        protected override string TableName => "Facturas";
        protected override string PrimaryKey => "Id";

        protected override (string, OleDbParameter[]) GetInsertQuery(Factura factura)
        {
            var query = @"INSERT INTO Facturas 
                        (NumeroFactura, Fecha, ClienteId, Subtotal, Impuesto, Total, Estado, MetodoPago) 
                        VALUES 
                        (@NumeroFactura, @Fecha, @ClienteId, @Subtotal, @Impuesto, @Total, @Estado, @MetodoPago)";

            var parameters = new OleDbParameter[] {
                new OleDbParameter("@NumeroFactura", factura.NumeroFactura),
                new OleDbParameter("@Fecha", factura.Fecha),
                new OleDbParameter("@ClienteId", factura.ClienteId),
                new OleDbParameter("@Subtotal", factura.Subtotal),
                new OleDbParameter("@Impuesto", factura.Impuesto),
                new OleDbParameter("@Total", factura.Total),
                new OleDbParameter("@Estado", factura.Estado),
                new OleDbParameter("@MetodoPago", factura.MetodoPago ?? (object)DBNull.Value)
            };

            return (query, parameters);
        }

        protected override (string, OleDbParameter[]) GetUpdateQuery(Factura factura)
        {
            var query = @"UPDATE Facturas SET 
                        Estado = @Estado,
                        MetodoPago = @MetodoPago
                        WHERE Id = @Id";

            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Estado", factura.Estado),
                new OleDbParameter("@MetodoPago", factura.MetodoPago ?? (object)DBNull.Value),
                new OleDbParameter("@Id", factura.Id)
            };

            return (query, parameters);
        }

        protected override Factura DataTableToObject(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0) return null;

            var row = dataTable.Rows[0];
            var factura = new Factura
            {
                Id = Convert.ToInt32(row["Id"]),
                NumeroFactura = row["NumeroFactura"].ToString(),
                Fecha = Convert.ToDateTime(row["Fecha"]),
                ClienteId = Convert.ToInt32(row["ClienteId"]),
                Subtotal = Convert.ToDecimal(row["Subtotal"]),
                Impuesto = Convert.ToDecimal(row["Impuesto"]),
                Total = Convert.ToDecimal(row["Total"]),
                Estado = row["Estado"].ToString(),
                MetodoPago = row["MetodoPago"]?.ToString()
            };

            // Cargar detalles
            factura.Detalles = _detalleRepo.GetByFacturaId(factura.Id);
            return factura;
        }

        protected override List<Factura> DataTableToList(DataTable dataTable)
        {
            var facturas = new List<Factura>();
            foreach (DataRow row in dataTable.Rows)
            {
                var factura = new Factura
                {
                    Id = Convert.ToInt32(row["Id"]),
                    NumeroFactura = row["NumeroFactura"].ToString(),
                    Fecha = Convert.ToDateTime(row["Fecha"]),
                    ClienteId = Convert.ToInt32(row["ClienteId"]),
                    Subtotal = Convert.ToDecimal(row["Subtotal"]),
                    Impuesto = Convert.ToDecimal(row["Impuesto"]),
                    Total = Convert.ToDecimal(row["Total"]),
                    Estado = row["Estado"].ToString(),
                    MetodoPago = row["MetodoPago"]?.ToString()
                };
                facturas.Add(factura);
            }
            return facturas;
        }

        public string GetNextInvoiceNumber()
        {
            var year = DateTime.Now.Year;
            var query = "SELECT MAX(NumeroFactura) FROM Facturas WHERE NumeroFactura LIKE @Pattern";
            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Pattern", $"FACT-{year}-%")
            };
            
            var lastNumber = DatabaseHelper.ExecuteScalar(query, parameters)?.ToString();
            if (string.IsNullOrEmpty(lastNumber))
            {
                return $"FACT-{year}-00001";
            }

            var parts = lastNumber.Split('-');
            var number = int.Parse(parts[2]) + 1;
            return $"FACT-{year}-{number.ToString("D5")}";
        }

        public List<Factura> GetByCliente(int clienteId)
        {
            var query = $"SELECT * FROM {TableName} WHERE ClienteId = @ClienteId ORDER BY Fecha DESC";
            var parameters = new OleDbParameter[] {
                new OleDbParameter("@ClienteId", clienteId)
            };
            var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);
            return DataTableToList(dataTable);
        }

        public List<Factura> GetByDateRange(DateTime desde, DateTime hasta)
        {
            var query = $"SELECT * FROM {TableName} WHERE Fecha BETWEEN @Desde AND @Hasta ORDER BY Fecha";
            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Desde", desde.Date),
                new OleDbParameter("@Hasta", hasta.Date.AddDays(1).AddSeconds(-1))
            };
            var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);
            return DataTableToList(dataTable);
        }
    }
}