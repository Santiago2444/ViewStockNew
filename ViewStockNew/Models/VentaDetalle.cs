using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewStockNew.Models
{
    public class VentaDetalle
    {
        public int Id { get; set; }
        [Required]
        public int? TipoProductoId { get; set; }
        public TipoProducto TipoProducto { get; set; }
        public int? MarcaId { get; set; }
        public Marca Marca { get; set; }
        public string Detalles { get; set; }
        public int? SPECId { get; set; }
        public SPEC SPEC { get; set; }
        public int? ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public bool Bulto { get; set; }
        public int? CantidadBultos { get; set; }
        public int? CantidadXBultos { get; set; }
        public decimal PrecioBulto { get; set; }
        public decimal PVP { get; set; }
        public int? Cantidad { get; set; }
        public int? CuentaId { get; set; }
        public Cuenta Cuenta { get; set; }
        public DateTime? FechaDePago { get; set; }
        public string Pagado { get; set; }
        public int CodigoDeVenta { get; set; }
        public byte[]? Imagen { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int? VentaId { get; set; }
        public Venta Venta { get; set;}
    }
}
