using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewStockNew.Models
{
    public class ClasesCompartidas
    {
        // Variables staticas
        public static int? UserId = null;

        public static int? TipoUsuarioId = null;

        public static string? MarcaNueva = null;

        public static string? TipoNuevo = null;

        public static string? SpecNuevo = null;

        public static string? ProveedorNuevo = null;

        public static string? LocalidadNueva = null;

        public static string? ProvinciaNueva = null;

        public static string? CuentaNueva = null;

        public static int? ProductoId = null;

        public static int? CodigoDeVenta = null;

        public static int? RemitoId = null;

        public static int? PagoId = null;

        public static int? SaldoId = null;

        // Listas de datos habilitados y otras cosas importantes
        public static BindingSource marcasList = new BindingSource();

        public static BindingSource listData = new BindingSource();

        public static BindingSource tiposProductosList = new BindingSource();

        public static BindingSource proveedoresList = new BindingSource();

        public static BindingSource specsList = new BindingSource();

        public static BindingSource localidadList = new BindingSource();

        public static BindingSource provinciasList = new BindingSource();

        public static BindingSource usuariosList = new BindingSource();

        public static BindingSource tiposdeUsuarioList = new BindingSource();

        public static BindingSource productosList = new BindingSource();

        public static BindingSource cuentasList = new BindingSource();

        public static BindingSource ventasList = new BindingSource();

        public static BindingSource ventaDetallesList = new BindingSource();

        // Lista de datos deshabilitados
        public static BindingSource DesProductList = new BindingSource();

        public static BindingSource DesProveedorList = new BindingSource();

        public static BindingSource DesUsuariosList = new BindingSource();

        public static BindingSource DesCuentasList = new BindingSource();

        // Listas para la Cuenta
        public static BindingSource ComprasList = new BindingSource();
        public static BindingSource ProductosList = new BindingSource();
        public static BindingSource PagosList = new BindingSource();
        //
        public static BindingSource ComprasDeudaList = new BindingSource();
        public static BindingSource ProductosDeudaList = new BindingSource();
        public static BindingSource PagosDeudaList = new BindingSource();
        //
        public static BindingSource ComprasListFilter = new BindingSource();
        public static BindingSource ProductosListFilter = new BindingSource();
        //
        public static BindingSource CompraSelected = new BindingSource();
        //
        public static BindingSource PagosListFilter = new BindingSource();
        //
        public static BindingSource ComprasDeudasListFilter = new BindingSource();
        public static BindingSource ProductosDeudasListFilter = new BindingSource();
        //
        public static BindingSource DeudaSelected = new BindingSource();
        internal static bool Permiso;

        // Listas para el MakeSale
        public static BindingSource VentaDetalle = new BindingSource();

        // Listas para el SalesMade
        public static BindingSource VentaSelected = new BindingSource();

        // Listas para RemitoView
        public static BindingSource remitos = new BindingSource();

        public static BindingSource remitosDetalle = new BindingSource();

        public static BindingSource RemitosDetalle = new BindingSource();

        // Otras Listas
        public static BindingSource FilterSend = new BindingSource();

        public static BindingSource PorcentajeGanancia = new BindingSource();

        public static BindingSource FilterRemito = new BindingSource();
    }
}
