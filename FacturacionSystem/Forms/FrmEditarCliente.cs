using System;
using System.Windows.Forms;
using FacturacionSystem.Models;
using FacturacionSystem.BLL.Services;

namespace FacturacionSystem.Forms
{
    public partial class FrmEditarCliente : Form
    {
        private readonly ClienteService _clienteService = new ClienteService();
        private Cliente _cliente;

        public FrmEditarCliente(Cliente cliente)
        {
            InitializeComponent();
            _cliente = cliente;
            ConfigurarControles();
            CargarDatosCliente();
        }

        private void ConfigurarControles()
        {
            // Configurar validación de campos
            txtNombre.Validating += ValidarRequerido;
            txtTelefono.Validating += ValidarTelefono;
            txtRUC.Validating += ValidarRUC;

            // Configurar botones
            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += BtnCancelar_Click;
        }

        private void CargarDatosCliente()
        {
            if (_cliente.Id > 0) // Modo edición
            {
                txtNombre.Text = _cliente.Nombre;
                txtTelefono.Text = _cliente.Telefono;
                txtDireccion.Text = _cliente.Direccion;
                txtEmail.Text = _cliente.Email;
                txtRUC.Text = _cliente.RUC;
                Text = "Editar Cliente";
            }
            else // Modo nuevo
            {
                Text = "Nuevo Cliente";
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

            try
            {
                // Mapear datos del formulario al objeto
                _cliente.Nombre = txtNombre.Text;
                _cliente.Telefono = txtTelefono.Text;
                _cliente.Direccion = txtDireccion.Text;
                _cliente.Email = txtEmail.Text;
                _cliente.RUC = txtRUC.Text;

                // Guardar cliente
                if (_cliente.Id > 0)
                    _clienteService.Update(_cliente);
                else
                    _cliente.Id = _clienteService.Create(_cliente);

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

        private void ValidarTelefono(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                errorProvider.SetError(txtTelefono, "Teléfono requerido");
                e.Cancel = true;
            }
            else if (txtTelefono.Text.Length < 7)
            {
                errorProvider.SetError(txtTelefono, "Teléfono inválido");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtTelefono, "");
            }
        }

        private void ValidarRUC(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRUC.Text) && txtRUC.Text.Length != 11)
            {
                errorProvider.SetError(txtRUC, "RUC debe tener 11 dígitos");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtRUC, "");
            }
        }
        #endregion
    }
}