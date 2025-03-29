using System;
using System.Windows.Forms;
using FacturacionSystem.Models;
using FacturacionSystem.BLL.Services;

namespace FacturacionSystem.Forms
{
    public partial class FrmLogin : Form
    {
        private readonly UsuarioService _usuarioService = new UsuarioService();

        public FrmLogin()
        {
            InitializeComponent();
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            // Configurar validaciones
            txtUsuario.Validating += ValidarRequerido;
            txtPassword.Validating += ValidarRequerido;

            // Configurar botones
            btnLogin.Click += BtnLogin_Click;
            btnCancelar.Click += BtnCancelar_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

            try
            {
                var usuario = _usuarioService.Autenticar(txtUsuario.Text, txtPassword.Text);
                
                if (usuario != null)
                {
                    // Abrir formulario principal
                    var frmMain = new FrmMain(usuario);
                    this.Hide();
                    frmMain.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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
    }
}