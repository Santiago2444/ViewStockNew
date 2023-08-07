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
    public partial class TicketViewReport : Form
    {
        BindingSource ticket = new BindingSource();
        ReportViewer reporte = new ReportViewer();

        private decimal pvp;
        public TicketViewReport()
        {
            InitializeComponent();
        }

        public TicketViewReport(BindingSource ticket)
        {
            InitializeComponent();
            this.ticket.DataSource = ticket;
            //
            reporte.Dock = DockStyle.Fill;
            reporte.SetDisplayMode(DisplayMode.PrintLayout);
            reporte.ZoomMode = ZoomMode.Percent;
            reporte.ZoomPercent = 100;
            Controls.Add(reporte);
        }

        private void TicketViewReport_Load(object sender, EventArgs e)
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
            reporte.LocalReport.ReportEmbeddedResource = "ViewStockNew.Reports.RptTicket.rdlc";
            var ticketImprimir = from VentaDetalle ventaDetalle in this.ticket
                                 select new
                                    {
                                        TipoProducto = ventaDetalle.TipoProducto.Nombre,
                                        Marca = ventaDetalle.Marca.Nombre,
                                        Detalles = ventaDetalle.Detalles,
                                        SPEC = ventaDetalle.SPEC.Nombre,
                                        Bulto = ventaDetalle.Bulto,
                                        CantidadBultos = ventaDetalle.CantidadBultos,
                                        CantidadXBultos = ventaDetalle.CantidadXBultos,
                                        PrecioBulto = "$"+ventaDetalle.PrecioBulto.ToString("0.00"),
                                        PVP = "$"+ventaDetalle.PVP.ToString("0.00"),
                                        Cantidad = ventaDetalle.Cantidad,
                                        FechaDePago = ventaDetalle.FechaDePago,
                                        CodigoDeVenta = ventaDetalle.CodigoDeVenta,
                                        Importe = "$"+ventaDetalle.Venta.Importe.ToString("0.00"),
                                        CantidadTotal = ventaDetalle.Venta.Articulos,
                                        Usuario = ventaDetalle.Usuario.Nombre,
                                    };
            reporte.LocalReport.DataSources.Add(new ReportDataSource("DSTicket", ticketImprimir.ToList()));
            reporte.RefreshReport();
        }
    }
}
