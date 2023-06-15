
namespace SbRotina.Models
{
    public class TarefaModel
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public string? DataTermino { get; set; }

        public string? Status { get; set;}

        public int UsuarioId { get; set;} 

 

    }
}
