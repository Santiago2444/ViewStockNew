using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ViewStockNew.Models
{
    public class TipoDeUsuario
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public bool Visible { get; internal set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
