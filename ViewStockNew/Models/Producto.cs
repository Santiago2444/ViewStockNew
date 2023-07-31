using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewStockNew.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public int TipoProductoId { get; set; }
        [ForeignKey("TipoProductoId")]
        public TipoProducto TipoProducto { get; set; }
        public int MarcaId { get; set; }
        [ForeignKey("MarcaId")]
        public Marca Marca { get; set; }
        public string? Detalles { get; set; }
        public int? SPECId { get; set; }
        [ForeignKey("SPECId")]
        public SPEC SPEC { get; set; }
        public decimal Descuento { get; set; }
        public decimal PrecioBulto { get; set; }
        public int CantidadBulto { get; set; }
        public decimal PrecioUnidad { get; set; }
        public decimal Ganancia { get; set; }
        [Required]
        public decimal PVP { get; set; }
        public int Stock { get; set; }
        public int? ProveedorId { get; set;}
        [ForeignKey("ProveedorId")]
        public Proveedor Proveedor { get; set; }
        public byte[]? Imagen { get; set; }
        public bool Visible { get; set; }
        public DateTime? Modificacion  { get; set; }
        public int? UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
        public int Carrito { get; set; }
    }
}
