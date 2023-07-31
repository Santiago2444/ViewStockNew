using System.ComponentModel.DataAnnotations;

namespace WebStock.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
        public string Genero { get; set; }
        public int TipoDeUsuarioId { get; set; }
        public TipoDeUsuario TipoDeUsuario { get; set; }
        public DateTime? Modificacion { get; set; }
        public byte[]? Imagen { get; set; }
        public bool Visible { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
