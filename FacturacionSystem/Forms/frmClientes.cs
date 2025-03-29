using System;
using System.Windows.Forms;
using FacturacionSystem.Repositories;
using FacturacionSystem.Models;

namespace FacturacionSystem.Forms
{
    public partial class frmClientes : Form
    {
        private readonly ClienteRepository _clienteRepo = new ClienteRepository();
        
        public frmClientes()
        {
            InitializeComponent();
            CargarClientes();
        }

        private void CargarClientes()
        {
            try
            {
                dgvClientes.DataSource = _clienteRepo.GetAllClientes();
                dgvClientes.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frmNuevoCliente = new frmNuevoCliente();
            if (frmNuevoCliente.ShowDialog() == DialogResult.OK)
            {
                CargarClientes(); // Refrescar lista
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0) return;
            
            var cliente = (Cliente)dgvClientes.SelectedRows[0].DataBoundItem;
            var frmEditarCliente = new frmEditarCliente(cliente);
            
            if (frmEditarCliente.ShowDialog() == DialogResult.OK)
            {
                CargarClientes(); // Refrescar lista
            }
        }
    }
}