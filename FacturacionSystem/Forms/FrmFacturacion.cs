using System;
using System.Windows.Forms;
using FacturacionSystem.Models;
using FacturacionSystem.BLL.Services;

namespace FacturacionSystem.Forms
{
    public partial class FrmFacturacion : Form
    {
        private readonly FacturacionService _facturacionService = new FacturacionService();
        private readonly ClienteService _clienteService = new ClienteService();
        private readonly ProductoService _productoService = new ProductoService();
        
        private Factura _facturaActual = new Factura();
        private BindingSource _detallesBinding = new BindingSource();

        public FrmFacturacion()
        {
            InitializeComponent();
            ConfigurarControles();
            CargarClientes();
            CargarProductos();
        }

        private void ConfigurarControles()
        {
            // Configurar DataGridView
            dgvDetalles.AutoGenerateColumns = false;
            _detallesBinding.DataSource = _facturaActual.Detalles;
            dgvDetalles.DataSource = _detallesBinding;

            // Configurar columnas
            dgvDetalles.Columns.Add("ProductoCodigo", "Código");
            dgvDetalles.Columns["ProductoCodigo"].DataPropertyName = "ProductoCodigo";
            
            dgvDetalles.Columns.Add("ProductoNombre", "Producto");
            dgvDetalles.Columns["ProductoNombre"].DataPropertyName = "ProductoNombre";
            
            dgvDetalles.Columns.Add("Cantidad", "Cantidad");
            dgvDetalles.Columns["Cantidad"].DataPropertyName = "Cantidad";
            
            dgvDetalles.Columns.Add("PrecioUnitario", "P. Unitario");
            dgvDetalles.Columns["PrecioUnitario"].DataPropertyName = "PrecioUnitario";
            dgvDetalles.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
            
            dgvDetalles.Columns.Add("Subtotal", "Subtotal");
            dgvDetalles.Columns["Subtotal"].DataPropertyName = "Subtotal";
            dgvDetalles.Columns["Subtotal"].DefaultCellStyle.Format = "C2";

            // Configurar botones
            btnAgregarProducto.Click += BtnAgregarProducto_Click;
            btnEliminarProducto.Click += BtnEliminarProducto_Click;
            btnGuardarFactura.Click += BtnGuardarFactura_Click;
            btnNuevaFactura.Click += BtnNuevaFactura_Click;
        }

        private void CargarClientes()
        {
            cboClientes.DataSource = _clienteService.GetAll();
            cboCli

[Response interrupted by a tool use result. Only one tool may be used at a time and should be placed at the end of the message.]