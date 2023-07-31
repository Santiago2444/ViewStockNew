using System.ComponentModel.DataAnnotations;

namespace WebStock.Models
{
    public class Marca
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public DateTime? Modificacion { get; set; }
        public bool Visible { get; internal set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
