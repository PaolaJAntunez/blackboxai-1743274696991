using System;
using System.Collections.Generic;
using FacturacionSystem.Models;
using FacturacionSystem.DAL.Repositories;

namespace FacturacionSystem.BLL.Services
{
    public class ProductoService
    {
        private readonly ProductoRepository _productoRepository = new ProductoRepository();

        public List<Producto> GetAll()
        {
            try
            {
                return _productoRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos: " + ex.Message);
            }
        }

        public List<Producto> GetActivos()
        {
            try
            {
                return _productoRepository.GetAll().FindAll(p => p.Activo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos activos: " + ex.Message);
            }
        }

        public Producto GetById(int id)
        {
            try
            {
                return _productoRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener producto: " + ex.Message);
            }
        }

        public int Create(Producto producto)
        {
            ValidateProducto(producto);

            try
            {
                return _productoRepository.Insert(producto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear producto: " + ex.Message);
            }
        }

        public bool Update(Producto producto)
        {
            ValidateProducto(producto);

            try
            {
                return _productoRepository.Update(producto) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar producto: " + ex.Message);
            }
        }

        public bool UpdateStock(int productoId, int cantidad)
        {
            try
            {
                return _productoRepository.UpdateStock(productoId, cantidad);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar stock: " + ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var producto = GetById(id);
                producto.Activo = false;
                return Update(producto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desactivar producto: " + ex.Message);
            }
        }

        public List<Producto> GetByCategory(string categoria)
        {
            try
            {
                return _productoRepository.GetByCategory(categoria);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos por categoría: " + ex.Message);
            }
        }

        private void ValidateProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Codigo))
                throw new Exception("El código del producto es requerido");

            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new Exception("El nombre del producto es requerido");

            if (producto.Precio <= 0)
                throw new Exception("El precio debe ser mayor a cero");

            if (producto.Stock < 0)
                throw new Exception("El stock no puede ser negativo");
        }
    }
}