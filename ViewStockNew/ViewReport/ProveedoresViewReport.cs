using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewStockNew.Models;

namespace ViewStockNew.ViewReport
{
    public partial class ProveedoresViewReport : Form
    {
        BindingSource proveedores = new BindingSource();
        ReportViewer reporte = new ReportViewer();

        public ProveedoresViewReport()
        {
            InitializeComponent();
        }

        public ProveedoresViewReport(BindingSource proveedores)
        {
            InitializeComponent();
            this.proveedores.DataSource = proveedores;
            //
            reporte.Dock = DockStyle.Fill;
            reporte.SetDisplayMode(DisplayMode.PrintLayout);
            reporte.ZoomMode = ZoomMode.Percent;
            reporte.ZoomPercent = 100;
            Controls.Add(reporte);
        }

        private void ProveedoresViewReport_Load(object sender, EventArgs e)
        {
            PageSettings page = new PageSettings();
            //
            page.Margins.Left = 0;
            page.Margins.Right = 0;
            page.Margins.Top = 0;
            page.Margins.Bottom = 0;
            //
            reporte.SetPageSettings(page);
            //
            CargarReporteAsync();
        }

        private async Task CargarReporteAsync()
        {
            reporte.LocalReport.ReportEmbeddedResource = "ViewStockNew.Reports.RptProveedores.rdlc";
            var proveedorImprimir = from Proveedor proveedor in this.proveedores
                                    select new
                                    {
                                        Id = proveedor.Id,
                                        Nombre = proveedor.Nombre,
                                        Telefono = proveedor.Telefono,
                                        Direccion = proveedor.Direccion,
                                        Provincia = proveedor.Provincia.Nombre,
                                        Localidad = proveedor.Localidad.Nombre
                                    };
            reporte.LocalReport.DataSources.Add(new ReportDataSource("DSProveedores", proveedorImprimir.ToList()));
            reporte.RefreshReport();
        }
    }
}
