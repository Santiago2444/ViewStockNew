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
    public partial class CuentasViewReport : Form
    {
        ReportViewer reporte = new ReportViewer();
        BindingSource cuentas = new BindingSource();

        public CuentasViewReport()
        {
            InitializeComponent();
        }

        public CuentasViewReport(BindingSource cuentas)
        {
            InitializeComponent();
            this.cuentas.DataSource = cuentas;
            //
            reporte.Dock = DockStyle.Fill;
            reporte.SetDisplayMode(DisplayMode.PrintLayout);
            reporte.ZoomMode = ZoomMode.Percent;
            reporte.ZoomPercent = 100;
            Controls.Add(reporte);
        }

        private void CuentasViewReport_Load(object sender, EventArgs e)
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
            reporte.LocalReport.ReportEmbeddedResource = "ViewStockNew.Reports.RptCuentas.rdlc";
            var cuentasImprimir = from Cuenta cuenta in this.cuentas
                                  select new
                                  {
                                      Id = cuenta.Id,
                                      Nombre = cuenta.Nombre,
                                      Telefono = cuenta.Telefono,
                                      Domicilio = cuenta.Domicilio,
                                      Provincia = cuenta.Provincia.Nombre,
                                      Localidad = cuenta.Localidad.Nombre,
                                  };
            reporte.LocalReport.DataSources.Add(new ReportDataSource("DSCuentas", cuentasImprimir.ToList()));
            reporte.RefreshReport();
        }
    }
}
