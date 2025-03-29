using System;
using System.Windows.Forms;
using FacturacionSystem.Models;
using FacturacionSystem.BLL.Services;

namespace FacturacionSystem.Forms
{
    public partial class FrmUsuarios : Form
    {
        private readonly UsuarioService _usuarioService = new UsuarioService();
        private BindingSource _usuariosBinding = new BindingSource();

        public FrmUsuarios()
        {
            InitializeComponent();
            ConfigurarControles();
            CargarUsuarios();
        }

        private void ConfigurarControles()
        {
            // Configurar DataGridView
            dgvUsuarios.AutoGenerateColumns = false;
            _usuariosBinding.DataSource = new List<Usuario>();
            dgvUsuarios.DataSource = _usuariosBinding;

            // Configurar columnas
            dgvUsuarios.Columns.Add("Nombre", "Usuario");
            dgvUsuarios.Columns["Nombre"].DataPropertyName = "Nombre";
            
            dgvUsuarios.Columns.Add("Rol", "Rol");
            dgvUsuarios.Columns["Rol"].DataPropertyName = "Rol";

            // Configurar botones
            btnNuevo.Click += BtnNuevo_Click;
            btnEditar.Click += BtnEditar_Click;
            btnResetPassword.Click += BtnResetPassword_Click;
        }

        private void CargarUsuarios()
        {
            try
            {
                _usuariosBinding.DataSource = _usuarioService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            var frm = new FrmEditarUsuario(new Usuario());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarUsuarios();
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) return;

            var usuario = (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;
            var frm = new FrmEditarUsuario(usuario);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarUsuarios();
            }
        }

        private void BtnResetPassword_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) return;

            var usuario = (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;
            if (MessageBox.Show($"¿Resetear contraseña del usuario {usuario.Nombre}?", 
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _usuarioService.CambiarPassword(usuario.ID, "Temp1234");
                    MessageBox.Show($"Contraseña reseteada a: Temp1234", "Éxito", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}