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
    public partial class UsuariosViewReport : Form
    {
        ReportViewer reporte = new ReportViewer();
        BindingSource usuarios = new BindingSource();

        public UsuariosViewReport()
        {
            InitializeComponent();
        }

        public UsuariosViewReport(BindingSource usuarios)
        {
            InitializeComponent();
            this.usuarios.DataSource = usuarios;
            //
            reporte.Dock = DockStyle.Fill;
            reporte.SetDisplayMode(DisplayMode.PrintLayout);
            reporte.ZoomMode = ZoomMode.Percent;
            reporte.ZoomPercent = 100;
            Controls.Add(reporte);
        }

        private void UsuariosViewReport_Load(object sender, EventArgs e)
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

        private void CargarReporteAsync()
        {
            reporte.LocalReport.ReportEmbeddedResource = "ViewStockNew.Reports.RptUsuarios.rdlc";
            var usuariosImpirmir = from Usuario usuario in this.usuarios
                                   select new
                                   {
                                       Id = usuario.Id,
                                       Nombre = usuario.Nombre,
                                       User = usuario.User,
                                       Genero = usuario.Genero,
                                       TipoDeUsuario = usuario.TipoDeUsuario.Nombre
                                   };
            reporte.LocalReport.DataSources.Add(new ReportDataSource("DSUsuarios", usuariosImpirmir.ToList()));
            reporte.RefreshReport();
        }
    }
}
