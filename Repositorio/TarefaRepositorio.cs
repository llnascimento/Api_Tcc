using Microsoft.EntityFrameworkCore;
using SbRotina.Data;
using SbRotina.Models;
using SbRotina.Repositorio.Interfaces;

namespace SbRotina.Repositorio
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly SbRotinaDbContext _dbContext;
        public TarefaRepositorio(SbRotinaDbContext sbRotinaDbContext)
        {
            _dbContext = sbRotinaDbContext;
        }

        public async Task<TarefaModel> BuscarPorId(int id)
        {
            return await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TarefaModel>> BuscarTodasTarefas()
        {
            return await _dbContext.Tarefas.ToListAsync();
        }

        public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();

            return tarefa;
        }

        public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int id)
        {
            TarefaModel tarefaPorId = await BuscarPorId(id);

            if (tarefaPorId == null)
            {
                throw new Exception($"Tarefa para o Id: {id} não foi encontrado. ");
            }

            tarefaPorId.Nome = tarefa.Nome;
            tarefaPorId.Descricao = tarefa.Descricao;
            tarefaPorId.Status = tarefa.Status;
            tarefaPorId.DataTermino = tarefa.DataTermino;
            _dbContext.Tarefas.Update(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return tarefaPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            TarefaModel tarefaPorId = await BuscarPorId(id);

            if (tarefaPorId == null)
            {
                throw new Exception($"Tarefa  para o Id: {id} não foi encontrado. ");
            }

            else
            {
                _dbContext.Tarefas.Remove(tarefaPorId);
                await _dbContext.SaveChangesAsync();
                return true;
            }


        }

        Task<List<TarefaModel>> ITarefaRepositorio.BuscarPorId(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeletarAsync(int id)
        {

            TarefaModel tarefa = await _dbContext.Tarefas
                .FirstOrDefaultAsync(x => x.Id == id);

            if (tarefa == null)
                throw new Exception("Tarefa não encontrada.");

            _dbContext.Tarefas.Remove(tarefa);
            int linhasAfetadas = await _dbContext.SaveChangesAsync();
            return linhasAfetadas;

        }

        public async Task<IEnumerable<TarefaModel>> GetTarefasByUserIdAsync(int usuarioId)
        {
            var lista = await _dbContext.Tarefas.Where(x => x.UsuarioId == usuarioId).ToListAsync();

            if (lista.Count == 0)
                throw new Exception("Usuario não possui tarefas");

            return lista;
        }
    }
}
