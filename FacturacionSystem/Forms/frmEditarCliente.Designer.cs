namespace FacturacionSystem.Forms
{
    partial class frmEditarCliente
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // txtNombre
            this.txtNombre.Location = new System.Drawing.Point(12, 12);
            this.txtNombre.Size = new System.Drawing.Size(300, 20);
            this.txtNombre.TabIndex = 0;
            
            // txtIdentificacion
            this.txtIdentificacion.Location = new System.Drawing.Point(12, 38);
            this.txtIdentificacion.ReadOnly = true;
            this.txtIdentificacion.Size = new System.Drawing.Size(200, 20);
            this.txtIdentificacion.TabIndex = 1;
            
            // txtTelefono
            this.txtTelefono.Location = new System.Drawing.Point(12, 64);
            this.txtTelefono.Size = new System.Drawing.Size(200, 20);
            this.txtTelefono.TabIndex = 2;
            
            // txtEmail
            this.txtEmail.Location = new System.Drawing.Point(12, 90);
            this.txtEmail.Size = new System.Drawing.Size(300, 20);
            this.txtEmail.TabIndex = 3;
            
            // btnGuardar
            this.btnGuardar.Location = new System.Drawing.Point(156, 130);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            
            // btnCancelar
            this.btnCancelar.Location = new System.Drawing.Point(237, 130);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            
            // frmEditarCliente
            this.ClientSize = new System.Drawing.Size(324, 161);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.txtIdentificacion);
            this.Controls.Add(this.txtNombre);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Editar Cliente";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}