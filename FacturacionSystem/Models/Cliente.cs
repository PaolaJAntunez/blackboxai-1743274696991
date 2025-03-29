namespace FacturacionSystem.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string RUC { get; set; }

        public Cliente()
        {
        }

        public Cliente(int id, string nombre, string telefono, string direccion, string email, string ruc)
        {
            Id = id;
            Nombre = nombre;
            Telefono = telefono;
            Direccion = direccion;
            Email = email;
            RUC = ruc;
        }
    }
}