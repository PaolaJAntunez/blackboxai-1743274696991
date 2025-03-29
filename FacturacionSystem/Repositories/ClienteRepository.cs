using System.Collections.Generic;
using System.Linq;
using FacturacionSystem.Models;

namespace FacturacionSystem.Repositories
{
    public class ClienteRepository
    {
        private List<Cliente> _clientes = new List<Cliente>();

        public void AddCliente(Cliente cliente)
        {
            cliente.Id = _clientes.Count + 1;
            _clientes.Add(cliente);
        }

        public void UpdateCliente(Cliente cliente)
        {
            var existing = _clientes.FirstOrDefault(c => c.Id == cliente.Id);
            if (existing != null)
            {
                existing.Nombre = cliente.Nombre;
                existing.Apellido = cliente.Apellido;
                existing.Telefono = cliente.Telefono;
                existing.Email = cliente.Email;
            }
        }

        public void DeleteCliente(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente != null)
            {
                _clientes.Remove(cliente);
            }
        }

        public List<Cliente> GetAllClientes()
        {
            return _clientes.ToList();
        }

        public Cliente GetClienteById(int id)
        {
            return _clientes.FirstOrDefault(c => c.Id == id);
        }
    }
}