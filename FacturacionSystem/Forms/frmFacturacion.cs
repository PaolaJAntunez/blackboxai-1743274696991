using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using FacturacionSystem.Models;
using FacturacionSystem.Repositories;

namespace FacturacionSystem.Forms
{
    public partial class frmFacturacion : Form
    {
        private readonly ClienteRepository _clienteRepo = new ClienteRepository();
        private readonly ProductoRepository _productoRepo = new ProductoRepository();
        private readonly FacturaRepository _facturaRepo = new FacturaRepository();
        
        private List<DetalleFactura> _detalles = new List<DetalleFactura>();
        private decimal _total = 0;

        public frmFacturacion()
        {
            InitializeComponent();
            CargarClientes();
            CargarProductos();
        }

        private void CargarClientes()
        {
            try
            {
                cboClientes.DataSource = _clienteRepo.GetAllClientes();
                cboClientes.DisplayMember = "NombreCompleto";
                cboClientes.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProductos()
        {
            try
            {
                cboProductos.DataSource = _productoRepo.GetAllProductos();
                cboProductos.DisplayMember = "Nombre";
                cboProductos.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var producto = (Producto)cboProductos.SelectedItem;
                var cantidad = (int)txtCantidad.Value;
                
                if(producto.Stock < cantidad)
                {
                    MessageBox.Show("No hay suficiente stock disponible", "Error", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var detalle = new DetalleFactura
                {
                    ProductoId = producto.Id,
                    NombreProducto = producto.Nombre,
                    PrecioUnitario = producto.Precio,
                    Cantidad = cantidad,
                    Subtotal = producto.Precio * cantidad
                };

                _detalles.Add(detalle);
                _total += detalle.Subtotal;
                ActualizarListaDetalles();
                lblTotal.Text = _total.ToString("C2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar producto: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarListaDetalles()
        {
            dgvDetalles.DataSource = null;
            dgvDetalles.DataSource = _detalles;
            dgvDetalles.AutoResizeColumns();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if(_detalles.Count == 0)
                {
                    MessageBox.Show("Debe agregar al menos un producto", "Error", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var factura = new Factura
                {
                    ClienteId = (int)cboClientes.SelectedValue,
                    Fecha = DateTime.Now,
                    Total = _total,
                    Detalles = _detalles
                };

                _facturaRepo.AddFactura(factura);
                MessageBox.Show("Factura guardada correctamente", "Éxito", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar factura: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}