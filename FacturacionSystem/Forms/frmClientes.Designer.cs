namespace FacturacionSystem.Forms
{
    partial class frmClientes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.SuspendLayout();
            
            // btnNuevo
            this.btnNuevo.Location = new System.Drawing.Point(12, 12);
            this.btnNuevo.Text = "Nuevo Cliente";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            
            // btnEditar
            this.btnEditar.Location = new System.Drawing.Point(120, 12);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            
            // dgvClientes
            this.dgvClientes.Location = new System.Drawing.Point(12, 50);
            this.dgvClientes.Size = new System.Drawing.Size(600, 300);
            this.dgvClientes.ReadOnly = true;
            
            // frmClientes
            this.ClientSize = new System.Drawing.Size(624, 362);
            this.Controls.Add(this.dgvClientes);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnNuevo);
            this.Text = "Gestión de Clientes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.ResumeLayout(false);
        }
    }
}