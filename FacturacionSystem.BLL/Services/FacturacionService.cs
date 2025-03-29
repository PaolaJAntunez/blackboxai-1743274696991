using System;
using System.Collections.Generic;
using FacturacionSystem.Models;
using FacturacionSystem.DAL.Repositories;

namespace FacturacionSystem.BLL.Services
{
    public class FacturacionService
    {
        private readonly FacturaRepository _facturaRepo = new FacturaRepository();
        private readonly DetalleFacturaRepository _detalleRepo = new DetalleFacturaRepository();
        private readonly ProductoRepository _productoRepo = new ProductoRepository();
        private readonly ClienteRepository _clienteRepo = new ClienteRepository();

        public Factura CrearFactura(Factura factura)
        {
            // Validaciones
            if (factura.Detalles == null || factura.Detalles.Count == 0)
                throw new Exception("La factura debe tener al menos un detalle");

            if (factura.ClienteId <= 0)
                throw new Exception("Debe seleccionar un cliente válido");

            // Calcular totales
            CalcularTotalesFactura(factura);

            // Generar número de factura
            factura.NumeroFactura = _facturaRepo.GetNextInvoiceNumber();
            factura.Fecha = DateTime.Now;
            factura.Estado = "Pendiente";

            try
            {
                // Insertar factura
                var facturaId = _facturaRepo.Insert(factura);

                // Insertar detalles
                foreach (var detalle in factura.Detalles)
                {
                    detalle.FacturaId = facturaId;
                    _detalleRepo.Insert(detalle, facturaId);
                    
                    // Actualizar stock
                    _productoRepo.UpdateStock(detalle.ProductoId, -detalle.Cantidad);
                }

                // Obtener factura completa
                return _facturaRepo.GetById(facturaId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la factura: " + ex.Message);
            }
        }

        public void CalcularTotalesFactura(Factura factura)
        {
            decimal subtotal = 0;
            decimal impuesto = 0;

            foreach (var detalle in factura.Detalles)
            {
                detalle.CalcularTotales();
                subtotal += detalle.Subtotal;
                impuesto += detalle.Impuesto;
            }

            factura.Subtotal = subtotal;
            factura.Impuesto = impuesto;
            factura.Total = subtotal + impuesto;
        }

        public Factura GetFactura(int id)
        {
            return _facturaRepo.GetById(id);
        }

        public List<Factura> GetFacturasPorCliente(int clienteId)
        {
            return _facturaRepo.GetByCliente(clienteId);
        }

        public List<Factura> GetFacturasPorRangoFechas(DateTime desde, DateTime hasta)
        {
            return _facturaRepo.GetByDateRange(desde, hasta);
        }

        public bool AnularFactura(int facturaId)
        {
            var factura = _facturaRepo.GetById(facturaId);
            if (factura == null)
                throw new Exception("Factura no encontrada");

            if (factura.Estado == "Anulada")
                throw new Exception("La factura ya está anulada");

            // Restaurar stock
            foreach (var detalle in factura.Detalles)
            {
                _productoRepo.UpdateStock(detalle.ProductoId, detalle.Cantidad);
            }

            // Actualizar estado
            factura.Estado = "Anulada";
            return _facturaRepo.Update(factura) > 0;
        }
    }
}