using System;
using System.Windows.Forms;
using FacturacionSystem.Repositories;
using System.Collections.Generic;
using FacturacionSystem.Models;

namespace FacturacionSystem.Forms
{
    public partial class frmReportes : Form
    {
        private readonly FacturaRepository _facturaRepo = new FacturaRepository();

        public frmReportes()
        {
            InitializeComponent();
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;
            ConfigurarGrid();
        }

        private void ConfigurarGrid()
        {
            dgvReportes.AutoGenerateColumns = false;
            dgvReportes.Columns.Clear();
            
            // Configurar columnas manualmente
            dgvReportes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "NumeroFactura",
                HeaderText = "N° Factura",
                Width = 100
            });
            
            dgvReportes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Fecha",
                HeaderText = "Fecha",
                Width = 100,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });
            
            dgvReportes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "NombreCliente",
                HeaderText = "Cliente",
                Width = 200
            });
            
            dgvReportes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Total",
                HeaderText = "Total",
                Width = 100,
                DefaultCellStyle = { Format = "C2" }
            });
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                var facturas = _facturaRepo.GetFacturasByDateRange(dtpDesde.Value, dtpHasta.Value);
                dgvReportes.DataSource = facturas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar reporte: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReportes.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para imprimir", "Advertencia",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // TODO: Implementar lógica de impresión
                MessageBox.Show("Funcionalidad de impresión en desarrollo", "Información",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al imprimir: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}