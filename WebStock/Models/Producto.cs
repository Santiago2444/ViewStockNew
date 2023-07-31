using System.ComponentModel.DataAnnotations;

namespace WebStock.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public int TipoProductoId { get; set; }
        public TipoProducto TipoProducto { get; set; }
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }
        public string? Detalles { get; set; }
        public int? SPECId { get; set; }
        public SPEC SPEC { get; set; }
        public decimal Descuento { get; set; }
        public decimal PrecioBulto { get; set; }
        public int CantidadBulto { get; set; }
        public decimal PrecioUnidad { get; set; }
        public decimal Ganancia { get; set; }
        [Required]
        public decimal PVP { get; set; }
        public int Stock { get; set; }
        public int? ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public byte[]? Imagen { get; set; }
        public bool Visible { get; set; }
        public DateTime? Modificacion { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int Carrito { get; set; }
    }
}
