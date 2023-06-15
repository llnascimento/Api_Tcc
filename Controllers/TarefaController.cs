using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SbRotina.Models;
using SbRotina.Repositorio.Interfaces;

namespace SbRotina.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepositorio _tarefaReposistorio;

        public TarefaController(ITarefaRepositorio tarefaReposistorio)
        {
            _tarefaReposistorio = tarefaReposistorio;
        }

        [HttpGet("buscarPorTodos")]
        public async Task<ActionResult<List<TarefaModel>>> ListarTodas()
        {

            List<TarefaModel> tarefas = await _tarefaReposistorio.BuscarTodasTarefas();
            return Ok(tarefas);
        }

        [HttpGet("buscarPorId")]
        public async Task<ActionResult<List<TarefaModel>>> BuscarPorId(int usuarioId)
        {
            List<TarefaModel> tarefa = await _tarefaReposistorio.BuscarPorId(usuarioId);
            return Ok(tarefa);
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<TarefaModel>> Cadastrar([FromBody] TarefaModel tarefaModel)
        {
            TarefaModel tarefa = await _tarefaReposistorio.Adicionar(tarefaModel);
            return Ok(tarefa);
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult<TarefaModel>> Atualizar([FromBody] TarefaModel tarefaModel, int id)
        {
            tarefaModel.Id = id;
            TarefaModel tarefa = await _tarefaReposistorio.Atualizar(tarefaModel, id);
            return Ok(tarefa);
        }

        [HttpDelete("Deletar")]
        public async Task<ActionResult<TarefaModel>> Apagar(int id)
        {
            bool deletado = await _tarefaReposistorio.Apagar(id);
            return Ok(deletado);
        }

        [HttpDelete("Deletar/{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                int linhasAfetadas = await _tarefaReposistorio.DeletarAsync(id);
                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("Buscar/{usuarioId}")]
        public async Task<IActionResult> GetByUserIdAsync(int usuarioId)
        {
            try
            {
                var lista = await _tarefaReposistorio.GetTarefasByUserIdAsync(usuarioId);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
                throw;
            }
           
        }


    }
}
