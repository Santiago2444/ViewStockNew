using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewStockNew.Models;

namespace ViewStockNew.ViewReport
{
    public partial class TicketViewReport : Form
    {
        private IEnumerable<VentaDetalle> ventas;
        ReportViewer reporte = new ReportViewer();
        private decimal pvp;
        public TicketViewReport()
        {
            InitializeComponent();
        }

        public TicketViewReport(IEnumerable<VentaDetalle> ventas)
        {
            InitializeComponent();
            this.ventas = ventas;
            //
            reporte.Dock = DockStyle.Fill;
            reporte.SetDisplayMode(DisplayMode.PrintLayout);
            reporte.ZoomMode = ZoomMode.Percent;
            reporte.ZoomPercent = 100;
            Controls.Add(reporte);
        }

        private void TicketViewReport_Load(object sender, EventArgs e)
        {
            CargarReporteAsync();
        }

        private async Task CargarReporteAsync()
        {
            reporte.LocalReport.ReportEmbeddedResource = "ViewStockNew.Reports.RptTicket.rdlc";
            var ticketImprimir = from VentaDetalle ventaDetalle in this.ventas
                                    select new
                                    {
                                        TipoProducto = ventaDetalle.TipoProducto.Nombre,
                                        Marca = ventaDetalle.Marca.Nombre,
                                        Detalles = ventaDetalle.Detalles,
                                        SPEC = ventaDetalle.SPEC.Nombre,
                                        Bulto = ventaDetalle.Bulto,
                                        CantidadBultos = ventaDetalle.CantidadBultos,
                                        CantidadXBultos = ventaDetalle.CantidadXBultos,
                                        PrecioBulto = ventaDetalle.PrecioBulto.ToString("0.00"),
                                        PVP = ventaDetalle.PVP.ToString("0.00"),
                                        Cantidad = ventaDetalle.Cantidad,
                                        FechaDePago = ventaDetalle.FechaDePago,
                                        CodigoDeVenta = ventaDetalle.CodigoDeVenta,
                                        Importe = ventaDetalle.Venta.Importe,
                                        CantidadTotal = ventaDetalle.Venta.Articulos
                                    };
            reporte.LocalReport.DataSources.Add(new ReportDataSource("DSTicket", ticketImprimir.ToList()));
            reporte.RefreshReport();
        }
    }
}
