using System;
using System.Windows.Forms;
using FacturacionSystem.Models;
using FacturacionSystem.BLL.Services;

namespace FacturacionSystem.Forms
{
    public partial class FrmEditarUsuario : Form
    {
        private readonly UsuarioService _usuarioService = new UsuarioService();
        private Usuario _usuario;
        private bool _esNuevo;

        public FrmEditarUsuario(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            _esNuevo = usuario.Id == 0;
            ConfigurarControles();
            CargarDatosUsuario();
        }

        private void ConfigurarControles()
        {
            // Configurar combo de roles
            cboRol.Items.AddRange(new string[] { "Administrador", "Vendedor", "Almacen" });
            
            // Configurar validaciones
            txtNombre.Validating += ValidarRequerido;
            cboRol.Validating += ValidarRequerido;

            // Configurar botones
            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += BtnCancelar_Click;
        }

        private void CargarDatosUsuario()
        {
            if (!_esNuevo) // Modo edición
            {
                txtNombre.Text = _usuario.Nombre;
                cboRol.SelectedItem = _usuario.Rol;
                Text = "Editar Usuario";
            }
            else // Modo nuevo
            {
                Text = "Nuevo Usuario";
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

            try
            {
                // Mapear datos del formulario al objeto
                _usuario.Nombre = txtNombre.Text;
                _usuario.Rol = cboRol.SelectedItem.ToString();

                // Guardar usuario
                if (_esNuevo)
                {
                    _usuario.Id = _usuarioService.CrearUsuario(_usuario, "Temp1234");
                    MessageBox.Show($"Usuario creado. Contraseña temporal: Temp1234", 
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _usuarioService.ActualizarUsuario(_usuario);
                }

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
            if (sender is ComboBox combo)
            {
                if (combo.SelectedIndex == -1)
                {
                    errorProvider.SetError(combo, "Selección requerida");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider.SetError(combo, "");
                }
            }
            else if (sender is TextBox textBox)
            {
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
        }
        #endregion
    }
}