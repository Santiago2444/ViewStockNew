using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewStockNew.Models
{
    public class Venta
    {
        public int Id { get; set; }
        [Required]
        public decimal Importe { get; set; }
        public decimal Dinero { get; set; }
        public decimal Vuelto { get; set; }
        public int Articulos { get; set; }
        public int CuentaId { get; set; }
        public Cuenta Cuenta { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
        public int Codigo { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public string PagoId { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
