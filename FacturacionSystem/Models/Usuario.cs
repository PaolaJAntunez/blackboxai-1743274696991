namespace FacturacionSystem.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; }

        public Usuario()
        {
        }

        public Usuario(int id, string nombre, string contraseña, string rol)
        {
            ID = id;
            Nombre = nombre;
            Contraseña = contraseña;
            Rol = rol;
        }
    }
}