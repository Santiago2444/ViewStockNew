using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewStockNew.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public decimal Importe { get; set; }
        public decimal Dinero { get; set; }
        public decimal Vuelto { get; set; }
        public string? Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public int? CuentaId { get; set; }
        public Cuenta Cuenta { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

    }
}
