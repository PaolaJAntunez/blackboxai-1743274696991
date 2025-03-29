using System;
using System.Windows.Forms;
using FacturacionSystem.Models;
using FacturacionSystem.Repositories;

namespace FacturacionSystem.Forms
{
    public partial class frmEditarCliente : Form
    {
        private readonly Cliente _cliente;
        private readonly ClienteRepository _clienteRepo = new ClienteRepository();

        public frmEditarCliente(Cliente cliente)
        {
            InitializeComponent();
            _cliente = cliente;
            CargarDatosCliente();
        }

        private void CargarDatosCliente()
        {
            txtNombre.Text = _cliente.Nombre;
            txtIdentificacion.Text = _cliente.Identificacion;
            txtTelefono.Text = _cliente.Telefono;
            txtEmail.Text = _cliente.Email;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                _cliente.Nombre = txtNombre.Text;
                _cliente.Telefono = txtTelefono.Text;
                _cliente.Email = txtEmail.Text;

                _clienteRepo.UpdateCliente(_cliente);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar cliente: {ex.Message}", 
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