using System;
using System.Windows.Forms;
using FacturacionSystem.Models;
using FacturacionSystem.Repositories;

namespace FacturacionSystem.Forms
{
    public partial class frmEditarProducto : Form
    {
        private readonly Producto _producto;
        private readonly ProductoRepository _productoRepo = new ProductoRepository();

        public frmEditarProducto(Producto producto)
        {
            InitializeComponent();
            _producto = producto;
            CargarDatosProducto();
        }

        private void CargarDatosProducto()
        {
            txtCodigo.Text = _producto.Codigo;
            txtNombre.Text = _producto.Nombre;
            txtPrecio.Value = _producto.Precio;
            txtStock.Value = _producto.Stock;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                _producto.Nombre = txtNombre.Text;
                _producto.Precio = txtPrecio.Value;
                _producto.Stock = (int)txtStock.Value;

                _productoRepo.UpdateProducto(_producto);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar producto: {ex.Message}", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}