using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewStockNew.Models
{
    public class RemitoDetalle
    {
        public int Id { get; set; }
        public int TipoProductoId { get; set; }
        public TipoProducto TipoProducto { get; set; }
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }
        public string? Detalles { get; set; }
        public int? SPECId { get; set; }
        public SPEC SPEC { get; set; }

        public bool bulto { get; set; }
        public int? CantidadBultos { get; set; }
        public int? CantidadXBultos { get; set; }
        public decimal PrecioBulto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int? CantidadTotal { get; set; }
        public decimal PrecioTotal { get; set; }
        public int? RemitoId { get; set; }
        public Remito Remito { get; set; }
    }
}
