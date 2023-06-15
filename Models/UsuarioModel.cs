
using System.ComponentModel.DataAnnotations;

namespace SbRotina.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        [EmailAddress]
        public  string Email { get; set; }
        public string Senha { get; set; }
        public string?  SexoUsuario  { get; set; }
    }
}
