using System;

namespace FacturacionSystem.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public string MetodoPago { get; set; } // Efectivo, Tarjeta, Transferencia
        public decimal Monto { get; set; }
        public string Estado { get; set; } // Completado, Pendiente, Anulado
        public string Referencia { get; set; }
        public string Notas { get; set; }

        public Pago()
        {
            Fecha = DateTime.Now;
            Estado = "Pendiente";
        }
    }
}