using System;
using System.Windows.Forms;
using FacturacionSystem.Models;
using FacturacionSystem.BLL.Services;

namespace FacturacionSystem.Forms
{
    public partial class FrmCambiarPassword : Form
    {
        private readonly UsuarioService _usuarioService = new UsuarioService();
        private Usuario _usuario;

        public FrmCambiarPassword(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            // Configurar validaciones
            txtPasswordActual.Validating += ValidarRequerido;
            txtNuevoPassword.Validating += ValidarPassword;
            txtConfirmarPassword.Validating += ValidarConfirmacionPassword;

            // Configurar botones
            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += BtnCancelar_Click;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

            try
            {
                // Verificar contraseña actual
                if (!_usuarioService.Autenticar(_usuario.Nombre, txtPasswordActual.Text))
                {
                    MessageBox.Show("La contraseña actual es incorrecta", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Cambiar contraseña
                _usuarioService.CambiarPassword(_usuario.ID, txtNuevoPassword.Text);
                
                MessageBox.Show("Contraseña cambiada exitosamente", "Éxito", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
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

        private void ValidarPassword(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNuevoPassword.Text))
            {
                errorProvider.SetError(txtNuevoPassword, "Campo requerido");
                e.Cancel = true;
            }
            else if (txtNuevoPassword.Text.Length < 6)
            {
                errorProvider.SetError(txtNuevoPassword, "Mínimo 6 caracteres");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtNuevoPassword, "");
            }
        }

        private void ValidarConfirmacionPassword(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtNuevoPassword.Text != txtConfirmarPassword.Text)
            {
                errorProvider.SetError(txtConfirmarPassword, "Las contraseñas no coinciden");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtConfirmarPassword, "");
            }
        }
        #endregion
    }
}