namespace FacturacionSystem.Models
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; }
        public string ProductoCodigo { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }

        public DetalleFactura()
        {
        }

        public void CalcularTotales()
        {
            Subtotal = Cantidad * PrecioUnitario;
            Impuesto = Subtotal * 0.18m; // 18% de IGV
            Total = Subtotal + Impuesto;
        }
    }
}