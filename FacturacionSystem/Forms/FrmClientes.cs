using System;
using System.Windows.Forms;
using FacturacionSystem.Models;
using FacturacionSystem.BLL.Services;

namespace FacturacionSystem.Forms
{
    public partial class FrmClientes : Form
    {
        private readonly ClienteService _clienteService = new ClienteService();
        private BindingSource _clientesBinding = new BindingSource();

        public FrmClientes()
        {
            InitializeComponent();
            ConfigurarControles();
            CargarClientes();
        }

        private void ConfigurarControles()
        {
            // Configurar DataGridView
            dgvClientes.AutoGenerateColumns = false;
            _clientesBinding.DataSource = new List<Cliente>();
            dgvClientes.DataSource = _clientesBinding;

            // Configurar columnas
            dgvClientes.Columns.Add("Nombre", "Nombre");
            dgvClientes.Columns["Nombre"].DataPropertyName = "Nombre";
            
            dgvClientes.Columns.Add("Telefono", "Teléfono");
            dgvClientes.Columns["Telefono"].DataPropertyName = "Telefono";
            
            dgvClientes.Columns.Add("Direccion", "Dirección");
            dgvClientes.Columns["Direccion"].DataPropertyName = "Direccion";
            
            dgvClientes.Columns.Add("RUC", "RUC");
            dgvClientes.Columns["RUC"].DataPropertyName = "RUC";

            // Configurar botones
            btnNuevo.Click += BtnNuevo_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += BtnBuscar_Click;
            btnRefresh.Click += BtnRefresh_Click;
        }

        private void CargarClientes()
        {
            try
            {
                _clientesBinding.DataSource = _clienteService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            var frm = new FrmEditarCliente(new Cliente());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarClientes();
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null) return;

            var cliente = (Cliente)dgvClientes.CurrentRow.DataBoundItem;
            var frm = new FrmEditarCliente(cliente);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarClientes();
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null) return;

            var cliente = (Cliente)dgvClientes.CurrentRow.DataBoundItem;
            if (MessageBox.Show($"¿Está seguro de eliminar al cliente {cliente.Nombre}?", 
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _clienteService.Delete(cliente.Id);
                    CargarClientes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                _clientesBinding.DataSource = _clienteService.Search(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            CargarClientes();
        }
    }
}