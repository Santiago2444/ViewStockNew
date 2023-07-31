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
    public partial class ProvinciasViewReport : Form
    {
        private IEnumerable<Provincia> provincias;
        ReportViewer reporte = new ReportViewer();

        public ProvinciasViewReport()
        {
            InitializeComponent();
            //

        }

        public ProvinciasViewReport(IEnumerable<Provincia> provincias)
        {
            InitializeComponent();
            this.provincias = provincias;
            //
            reporte.Dock = DockStyle.Fill;
            reporte.SetDisplayMode(DisplayMode.PrintLayout);
            reporte.ZoomMode = ZoomMode.Percent;
            reporte.ZoomPercent = 100;
            Controls.Add(reporte);
        }

        private void ProvinciasViewReport_Load(object sender, EventArgs e)
        {
            CargarReporteAsync();
        }

        private async Task CargarReporteAsync()
        {
            reporte.LocalReport.ReportEmbeddedResource = "ViewStockNew.Reports.RptProvincias.rdlc";
            var provinciasImprimir = from Provincia provincia in provincias
                                    select new
                                    {
                                        provincia.Id,
                                        provincia.Nombre                                     
                                    };
            reporte.LocalReport.DataSources.Add(new ReportDataSource("DSProvincias", provinciasImprimir));
            reporte.RefreshReport();
        }
    }
}
