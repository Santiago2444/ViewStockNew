using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewStockNew.Models
{
    public class Remito
    {
        [Key]
        public int Id { get; set; }
        public string TipoComprobante { get; set; }
        public decimal Importe { get; set; }
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public int CantidadProductos { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
