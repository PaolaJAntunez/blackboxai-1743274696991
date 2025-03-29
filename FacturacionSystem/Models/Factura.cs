using System;
using System.Collections.Generic;

namespace FacturacionSystem.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public string MetodoPago { get; set; }
        public List<DetalleFactura> Detalles { get; set; }

        public Factura()
        {
            Fecha = DateTime.Now;
            Detalles = new List<DetalleFactura>();
            Estado = "Pendiente";
        }
    }
}