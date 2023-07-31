using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewStockNew.Models
{
    public class Proveedor
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public int? ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }
        public int? LocalidadId { get; set; }
        public Localidad Localidad { get; set; }
        public DateTime? Modificacion { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public bool Visible { get; set; }

        public byte[]? Imagen { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
