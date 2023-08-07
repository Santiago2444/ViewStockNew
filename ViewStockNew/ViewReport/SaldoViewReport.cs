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
    public partial class SaldoViewReport : Form
    {
        ReportViewer reporte = new ReportViewer();
        private BindingSource comprobanteSaldo = new BindingSource();

        public SaldoViewReport()
        {
            InitializeComponent();
        }

        public SaldoViewReport(BindingSource comprobanteSaldo)
        {
            InitializeComponent();
            this.comprobanteSaldo.DataSource = comprobanteSaldo;
            //
            reporte.Dock = DockStyle.Fill;
            reporte.SetDisplayMode(DisplayMode.PrintLayout);
            reporte.ZoomMode = ZoomMode.Percent;
            reporte.ZoomPercent = 100;
            Controls.Add(reporte);

        }

        private void SaldoViewReport_Load(object sender, EventArgs e)
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
            reporte.LocalReport.ReportEmbeddedResource = "ViewStockNew.Reports.RptSaldo.rdlc";
            var comprobanteSaldoImprimir = from Pago pago in this.comprobanteSaldo
                                           select new
                                           {
                                               Usuario = pago.Usuario.Nombre,
                                               Cuenta = pago.Cuenta.Nombre,
                                               Dinero = "$"+pago.Dinero.ToString("0.00"),
                                               Fecha = pago.Fecha,
                                               Tipo = pago.Tipo
                                           };
            reporte.LocalReport.DataSources.Add(new ReportDataSource("DSSaldo", comprobanteSaldoImprimir.ToList()));
            reporte.RefreshReport();
        }
    }
}
