namespace FacturacionSystem.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; }
        public bool Activo { get; set; }

        public Producto()
        {
            Activo = true;
        }

        public Producto(int id, string codigo, string nombre, string descripcion, 
                      decimal precio, int stock, string categoria)
        {
            Id = id;
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            Categoria = categoria;
            Activo = true;
        }
    }
}