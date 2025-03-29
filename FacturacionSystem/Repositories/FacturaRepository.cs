using System;
using System.Collections.Generic;
using System.Linq;
using FacturacionSystem.Models;

namespace FacturacionSystem.Repositories
{
    public class FacturaRepository
    {
        private List<Factura> _facturas = new List<Factura>();

        public void AddFactura(Factura factura)
        {
            factura.Id = _facturas.Count + 1;
            _facturas.Add(factura);
        }

        public List<Factura> GetAllFacturas()
        {
            return _facturas.Select(f => new Factura {
                Id = f.Id,
                Fecha = f.Fecha,
                Cliente = f.Cliente,
                Detalles = f.Detalles
            }).ToList();
        }

        public string GetNombreCliente(int clienteId)
        {
            var factura = _facturas.FirstOrDefault(f => f.Cliente.Id == clienteId);
            return factura != null ? $"{factura.Cliente.Nombre} {factura.Cliente.Apellido}" : string.Empty;
        }
    }
}