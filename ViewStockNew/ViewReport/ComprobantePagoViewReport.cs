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
    public partial class ComprobantePagoViewReport : Form
    {
        private BindingSource comprobantePago = new BindingSource();
        ReportViewer reporte = new ReportViewer();

        public ComprobantePagoViewReport()
        {
            InitializeComponent();
        }

        public ComprobantePagoViewReport(BindingSource comprobantePago)
        {
            InitializeComponent();
            this.comprobantePago.DataSource = comprobantePago;
            //
            reporte.Dock = DockStyle.Fill;
            reporte.SetDisplayMode(DisplayMode.PrintLayout);
            reporte.ZoomMode = ZoomMode.Percent;
            reporte.ZoomPercent = 100;
            Controls.Add(reporte);
        }

        private void ComprobantePagoViewReport_Load(object sender, EventArgs e)
        {
            CargarReporteAsync();
        }

        private async Task CargarReporteAsync()
        {
            reporte.LocalReport.ReportEmbeddedResource = "ViewStockNew.Reports.RptComprobantePago.rdlc";
            var comprobantePagoImprimir = from VentaDetalle ventaDetalle in this.comprobantePago
                                          select new 
                                          {
                                              TipoProducto = ventaDetalle.TipoProducto.Nombre,
                                              Marca = ventaDetalle.Marca.Nombre,
                                              Detalles = ventaDetalle.Detalles,
                                              SPEC = ventaDetalle.SPEC.Nombre,
                                              Bulto = ventaDetalle.Bulto,
                                              CantidadBultos = ventaDetalle.CantidadBultos,
                                              CantidadXBultos = ventaDetalle.CantidadXBultos,
                                              PrecioBulto = "$" + ventaDetalle.PrecioBulto.ToString("0.00"),
                                              PVP = "$" + ventaDetalle.PVP.ToString("0.00"),
                                              Cantidad = ventaDetalle.Cantidad,
                                              FechaDePago = ventaDetalle.FechaDePago,
                                              TipoPago = ventaDetalle.Venta.Pago.Tipo,
                                              Importe = "$" + ventaDetalle.Venta.Pago.Importe.ToString("0.00"),
                                              Fecha = ventaDetalle.Venta.Pago.Fecha,
                                              Usuario = ventaDetalle.Venta.Pago.Usuario.Nombre,
                                              Cuenta = ventaDetalle.Venta.Cuenta.Nombre,
                                              Dinero = "$" + ventaDetalle.Venta.Pago.Dinero.ToString("0.00"),
                                              Vuelto = "$" + ventaDetalle.Venta.Pago.Vuelto.ToString("0.00")
                                          };
            reporte.LocalReport.DataSources.Add(new ReportDataSource("DSPago", comprobantePagoImprimir.ToList()));
            reporte.RefreshReport();
        }
    }
}
