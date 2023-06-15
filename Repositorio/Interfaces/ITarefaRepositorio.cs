using SbRotina.Models;

namespace SbRotina.Repositorio.Interfaces
{
    public interface ITarefaRepositorio
    {
        Task<List<TarefaModel>> BuscarTodasTarefas();
        Task<List<TarefaModel>> BuscarPorId(int usuarioId);
        Task<TarefaModel> Adicionar(TarefaModel tarefa);
        Task<TarefaModel> Atualizar(TarefaModel  tarefa, int id);
        Task<bool> Apagar(int id);
        Task<int> DeletarAsync(int id);
        Task<IEnumerable<TarefaModel>> GetTarefasByUserIdAsync(int usuarioId);

    }
}
