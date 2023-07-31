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
    public partial class ComprobanteViewReport : Form
    {
        BindingSource remitosDetalles = new BindingSource();
        ReportViewer reporte = new ReportViewer();

        public ComprobanteViewReport()
        {
            InitializeComponent();
        }

        public ComprobanteViewReport(BindingSource remitosDetalles)
        {
            InitializeComponent();
            this.remitosDetalles.DataSource = remitosDetalles;
            //
            reporte.Dock = DockStyle.Fill;
            reporte.SetDisplayMode(DisplayMode.PrintLayout);
            reporte.ZoomMode = ZoomMode.Percent;
            reporte.ZoomPercent = 100;
            Controls.Add(reporte);

        }

        private void ComprobanteViewReport_Load(object sender, EventArgs e)
        {
            CargarReporteAsync();
        }

        private async Task CargarReporteAsync()
        {
            reporte.LocalReport.ReportEmbeddedResource = "ViewStockNew.Reports.RptComprobante.rdlc";
            var comprobanteImprimir = from RemitoDetalle remitoDetalle in this.remitosDetalles
                                      select  new
                                      {
                                          TipoProducto = remitoDetalle.TipoProducto.Nombre,
                                          Marca = remitoDetalle.Marca.Nombre,
                                          Detalles = remitoDetalle.Detalles,
                                          SPEC = remitoDetalle.SPEC.Nombre,
                                          CantidadBultos = remitoDetalle.CantidadBultos,
                                          CantidadXBultos = remitoDetalle.CantidadXBultos,
                                          PrecioBulto = "$" + remitoDetalle.PrecioBulto.ToString("0.00"),
                                          PrecioUnitario = "$" + remitoDetalle.PrecioUnitario.ToString("0.00"),
                                          CantidaTotal = remitoDetalle.CantidadTotal,
                                          PrecioTotal = "$" + remitoDetalle.PrecioTotal.ToString("0.00"),
                                          Fecha = remitoDetalle.Remito.Fecha,
                                          Proveedor = remitoDetalle.Remito.Proveedor.Nombre,
                                          Importe = "$" + remitoDetalle.Remito.Importe.ToString("0.00"),
                                          TipoComprobante = remitoDetalle.Remito.TipoComprobante,
                                          CantidadTotal = remitoDetalle.Remito.CantidadProductos,
                                          Bulto = remitoDetalle.bulto,
                                          Usuario = remitoDetalle.Remito.Usuario
                                      };
            reporte.LocalReport.DataSources.Add(new ReportDataSource("DSComprobante", comprobanteImprimir.ToList()));
            reporte.RefreshReport();
        }
    }
}
