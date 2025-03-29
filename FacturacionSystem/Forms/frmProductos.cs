using System;
using System.Windows.Forms;
using FacturacionSystem.Repositories;
using FacturacionSystem.Models;

namespace FacturacionSystem.Forms
{
    public partial class frmProductos : Form
    {
        private readonly ProductoRepository _productoRepo = new ProductoRepository();
        
        public frmProductos()
        {
            InitializeComponent();
            CargarProductos();
        }

        private void CargarProductos()
        {
            try
            {
                dgvProductos.DataSource = _productoRepo.GetAllProductos();
                dgvProductos.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frm = new frmNuevoProducto();
            if (frm.ShowDialog() == DialogResult.OK)
                CargarProductos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0) return;
            
            var producto = (Producto)dgvProductos.SelectedRows[0].DataBoundItem;
            var frm = new frmEditarProducto(producto);
            
            if (frm.ShowDialog() == DialogResult.OK)
                CargarProductos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0) return;
            
            var producto = (Producto)dgvProductos.SelectedRows[0].DataBoundItem;
            
            if (MessageBox.Show($"¿Eliminar el producto {producto.Nombre}?", "Confirmar", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _productoRepo.DeleteProducto(producto.Id);
                    CargarProductos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar producto: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}