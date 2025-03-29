using System;
using System.Windows.Forms;
using FacturacionSystem.Models;
using FacturacionSystem.BLL.Services;

namespace FacturacionSystem.Forms
{
    public partial class FrmEditarProducto : Form
    {
        private readonly ProductoService _productoService = new ProductoService();
        private Producto _producto;

        public FrmEditarProducto(Producto producto)
        {
            InitializeComponent();
            _producto = producto;
            ConfigurarControles();
            CargarDatosProducto();
        }

        private void ConfigurarControles()
        {
            // Configurar validación de campos
            txtCodigo.Validating += ValidarRequerido;
            txtNombre.Validating += ValidarRequerido;
            txtPrecio.Validating += ValidarPrecio;
            txtStock.Validating += ValidarStock;

            // Configurar botones
            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += BtnCancelar_Click;
        }

        private void CargarDatosProducto()
        {
            if (_producto.Id > 0) // Modo edición
            {
                txtCodigo.Text = _producto.Codigo;
                txtNombre.Text = _producto.Nombre;
                txtDescripcion.Text = _producto.Descripcion;
                txtPrecio.Text = _producto.Precio.ToString("N2");
                txtStock.Text = _producto.Stock.ToString();
                txtCategoria.Text = _producto.Categoria;
                chkActivo.Checked = _producto.Activo;
                Text = "Editar Producto";
            }
            else // Modo nuevo
            {
                Text = "Nuevo Producto";
                chkActivo.Checked = true;
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

            try
            {
                // Mapear datos del formulario al objeto
                _producto.Codigo = txtCodigo.Text;
                _producto.Nombre = txtNombre.Text;
                _producto.Descripcion = txtDescripcion.Text;
                _producto.Precio = decimal.Parse(txtPrecio.Text);
                _producto.Stock = int.Parse(txtStock.Text);
                _producto.Categoria = txtCategoria.Text;
                _producto.Activo = chkActivo.Checked;

                // Guardar producto
                if (_producto.Id > 0)
                    _productoService.Update(_producto);
                else
                    _producto.Id = _productoService.Create(_producto);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #region Validaciones
        private void ValidarRequerido(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                errorProvider.SetError(textBox, "Campo requerido");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(textBox, "");
            }
        }

        private void ValidarPrecio(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
            {
                errorProvider.SetError(txtPrecio, "Precio inválido");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtPrecio, "");
            }
        }

        private void ValidarStock(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                errorProvider.SetError(txtStock, "Stock inválido");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtStock, "");
            }
        }
        #endregion
    }
}