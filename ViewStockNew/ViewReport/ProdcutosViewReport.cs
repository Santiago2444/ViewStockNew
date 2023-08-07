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
    public partial class ProdcutosViewReport : Form
    {
        ReportViewer reporte = new ReportViewer();
        //private IEnumerable<Producto> productos;
        BindingSource productos = new BindingSource();
        //

        public ProdcutosViewReport()
        {
            InitializeComponent();
            //
        }

        public ProdcutosViewReport(BindingSource productos)
        {
            InitializeComponent();
            //
            this.productos.DataSource = productos;
            //
            reporte.Dock = DockStyle.Fill;
            reporte.SetDisplayMode(DisplayMode.PrintLayout);
            reporte.ZoomMode = ZoomMode.Percent;
            reporte.ZoomPercent = 100;
            //
            Controls.Add(reporte);
        }



        private async Task CargarReporteAsync()
        {
            reporte.LocalReport.ReportEmbeddedResource = "ViewStockNew.Reports.RptProductos.rdlc";
            var productosImprimir = from Producto producto in this.productos
                                    select new
                                    {
                                        Id = producto.Id,
                                        TipoProducto = producto.TipoProducto.Nombre,
                                        Marca = producto.Marca.Nombre,
                                        Detalles = producto.Detalles,
                                        SPEC = producto.SPEC.Nombre,
                                        PrecioBulto = "$"+producto.PrecioBulto.ToString("0.00"),
                                        PVP = "$"+producto.PVP.ToString("0.00"),
                                        Stock = producto.Stock,
                                    };
            reporte.LocalReport.DataSources.Add(new ReportDataSource("DSProductos", productosImprimir.ToList()));
            reporte.RefreshReport();
        }

        private void ProdcutosViewReport_Load(object sender, EventArgs e)
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
    }
}

