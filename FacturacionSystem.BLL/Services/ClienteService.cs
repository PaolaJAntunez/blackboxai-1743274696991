using System;
using System.Collections.Generic;
using FacturacionSystem.Models;
using FacturacionSystem.DAL.Repositories;

namespace FacturacionSystem.BLL.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository = new ClienteRepository();

        public List<Cliente> GetAll()
        {
            try
            {
                return _clienteRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener clientes: " + ex.Message);
            }
        }

        public Cliente GetById(int id)
        {
            try
            {
                return _clienteRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cliente: " + ex.Message);
            }
        }

        public int Create(Cliente cliente)
        {
            ValidateCliente(cliente);

            try
            {
                return _clienteRepository.Insert(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear cliente: " + ex.Message);
            }
        }

        public bool Update(Cliente cliente)
        {
            ValidateCliente(cliente);

            try
            {
                return _clienteRepository.Update(cliente) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar cliente: " + ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return _clienteRepository.Delete(id) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar cliente: " + ex.Message);
            }
        }

        public List<Cliente> Search(string term)
        {
            try
            {
                return _clienteRepository.Search(term);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en búsqueda de clientes: " + ex.Message);
            }
        }

        private void ValidateCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new Exception("El nombre del cliente es requerido");

            if (cliente.Nombre.Length > 100)
                throw new Exception("El nombre no puede exceder 100 caracteres");

            if (!string.IsNullOrEmpty(cliente.RUC) && cliente.RUC.Length != 11)
                throw new Exception("El RUC debe tener 11 dígitos");
        }
    }
}