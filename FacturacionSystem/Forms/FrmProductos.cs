using System;
using System.Windows.Forms;
using FacturacionSystem.Models;
using FacturacionSystem.BLL.Services;

namespace FacturacionSystem.Forms
{
    public partial class FrmProductos : Form
    {
        private readonly ProductoService _productoService = new ProductoService();
        private BindingSource _productosBinding = new BindingSource();

        public FrmProductos()
        {
            InitializeComponent();
            ConfigurarControles();
            CargarProductos();
        }

        private void ConfigurarControles()
        {
            // Configurar DataGridView
            dgvProductos.AutoGenerateColumns = false;
            _productosBinding.DataSource = new List<Producto>();
            dgvProductos.DataSource = _productosBinding;

            // Configurar columnas
            dgvProductos.Columns.Add("Codigo", "Código");
            dgvProductos.Columns["Codigo"].DataPropertyName = "Codigo";
            
            dgvProductos.Columns.Add("Nombre", "Nombre");
            dgvProductos.Columns["Nombre"].DataPropertyName = "Nombre";
            
            dgvProductos.Columns.Add("Precio", "Precio");
            dgvProductos.Columns["Precio"].DataPropertyName = "Precio";
            dgvProductos.Columns["Precio"].DefaultCellStyle.Format = "C2";
            
            dgvProductos.Columns.Add("Stock", "Stock");
            dgvProductos.Columns["Stock"].DataPropertyName = "Stock";
            
            dgvProductos.Columns.Add("Categoria", "Categoría");
            dgvProductos.Columns["Categoria"].DataPropertyName = "Categoria";

            // Configurar botones
            btnNuevo.Click += BtnNuevo_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnBuscar.Click += BtnBuscar_Click;
            btnRefresh.Click += BtnRefresh_Click;
        }

        private void CargarProductos()
        {
            try
            {
                _productosBinding.DataSource = _productoService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            var frm = new FrmEditarProducto(new Producto());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarProductos();
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null) return;

            var producto = (Producto)dgvProductos.CurrentRow.DataBoundItem;
            var frm = new FrmEditarProducto(producto);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarProductos();
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null) return;

            var producto = (Producto)dgvProductos.CurrentRow.DataBoundItem;
            if (MessageBox.Show($"¿Está seguro de eliminar el producto {producto.Nombre}?", 
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _productoService.Delete(producto.Id);
                    CargarProductos();
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
                _productosBinding.DataSource = _productoService.Search(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            CargarProductos();
        }
    }
}