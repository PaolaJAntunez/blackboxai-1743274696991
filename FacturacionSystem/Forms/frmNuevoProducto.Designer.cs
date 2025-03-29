namespace FacturacionSystem.Forms
{
    partial class frmNuevoProducto
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
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtPrecio = new System.Windows.Forms.NumericUpDown();
            this.txtStock = new System.Windows.Forms.NumericUpDown();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStock)).BeginInit();
            this.SuspendLayout();
            
            // txtCodigo
            this.txtCodigo.Location = new System.Drawing.Point(100, 20);
            this.txtCodigo.Size = new System.Drawing.Size(200, 20);
            this.txtCodigo.TabIndex = 0;
            
            // txtNombre
            this.txtNombre.Location = new System.Drawing.Point(100, 50);
            this.txtNombre.Size = new System.Drawing.Size(200, 20);
            this.txtNombre.TabIndex = 1;
            
            // txtPrecio
            this.txtPrecio.DecimalPlaces = 2;
            this.txtPrecio.Location = new System.Drawing.Point(100, 80);
            this.txtPrecio.Maximum = 1000000;
            this.txtPrecio.Size = new System.Drawing.Size(100, 20);
            this.txtPrecio.TabIndex = 2;
            
            // txtStock
            this.txtStock.Location = new System.Drawing.Point(100, 110);
            this.txtStock.Maximum = 10000;
            this.txtStock.Size = new System.Drawing.Size(100, 20);
            this.txtStock.TabIndex = 3;
            
            // btnGuardar
            this.btnGuardar.Location = new System.Drawing.Point(144, 150);
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            
            // btnCancelar
            this.btnCancelar.Location = new System.Drawing.Point(225, 150);
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            
            // Etiquetas
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Text = "Código:";
            this.label2.Location = new System.Drawing.Point(20, 53);
            this.label2.Text = "Nombre:";
            this.label3.Location = new System.Drawing.Point(20, 83);
            this.label3.Text = "Precio:";
            this.label4.Location = new System.Drawing.Point(20, 113);
            this.label4.Text = "Stock:";
            
            // frmNuevoProducto
            this.ClientSize = new System.Drawing.Size(320, 190);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.txtCodigo, this.txtNombre, this.txtPrecio, this.txtStock,
                this.btnGuardar, this.btnCancelar,
                this.label1, this.label2, this.label3, this.label4});
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Nuevo Producto";
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}