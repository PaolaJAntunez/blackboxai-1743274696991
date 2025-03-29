using System;
using System.Windows.Forms;
using FacturacionSystem.Models;

namespace FacturacionSystem.Forms
{
    public partial class FrmMain : Form
    {
        private Usuario _usuarioActual;

        public FrmMain(Usuario usuario)
        {
            InitializeComponent();
            _usuarioActual = usuario;
            ConfigurarControles();
            ConfigurarMenuSegunRol();
        }

        private void ConfigurarControles()
        {
            // Configurar ventana MDI
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
            
            // Configurar barra de estado
            tsslUsuario.Text = $"Usuario: {_usuarioActual.Nombre}";
            tsslRol.Text = $"Rol: {_usuarioActual.Rol}";
            tsslFecha.Text = $"Fecha: {DateTime.Now.ToShortDateString()}";

            // Configurar eventos
            mnuSalir.Click += MnuSalir_Click;
            mnuClientes.Click += MnuClientes_Click;
            mnuProductos.Click += MnuProductos_Click;
            mnuFacturacion.Click += MnuFacturacion_Click;
            mnuCambiarPassword.Click += MnuCambiarPassword_Click;
        }

        private void ConfigurarMenuSegunRol()
        {
            // Configurar visibilidad según el rol
            switch (_usuarioActual.Rol)
            {
                case "Administrador":
                    // Mostrar todo
                    break;
                    
                case "Vendedor":
                    mnuUsuarios.Visible = false;
                    mnuConfiguracion.Visible = false;
                    mnuReportes.Visible = false;
                    break;
                    
                case "Almacen":
                    mnuUsuarios.Visible = false;
                    mnuFacturacion.Visible = false;
                    mnuReportes.Visible = false;
                    mnuConfiguracion.Visible = false;
                    break;
                    
                default:
                    mnuUsuarios.Visible = false;
                    mnuFacturacion.Visible = false;
                    mnuProductos.Visible = false;
                    mnuClientes.Visible = false;
                    mnuReportes.Visible = false;
                    mnuConfiguracion.Visible = false;
                    break;
            }

            // Configurar permisos específicos
            if (_usuarioActual.Rol != "Administrador")
            {
                mnuCambiarPassword.Visible = true;
            }
        }

        private void AbrirFormulario(Form formulario)
        {
            formulario.MdiParent = this;
            formulario.StartPosition = FormStartPosition.CenterScreen;
            formulario.Show();
        }

        #region Eventos del Menú
        private void MnuClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmClientes());
        }

        private void MnuProductos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmProductos());
        }

        private void MnuFacturacion_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmFacturacion());
        }

        private void MnuCambiarPassword_Click(object sender, EventArgs e)
        {
            var frm = new FrmCambiarPassword(_usuarioActual);
            frm.ShowDialog();
        }

        private void MnuSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}