using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SbRotina.Data;
using SbRotina.Models;
using SbRotina.Repositorio.Interfaces;

namespace SbRotina.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase


    {
        private readonly SbRotinaDbContext _context;

        private readonly IUsuarioReposistorio _usuarioReposistorio;

        public UsuarioController(IUsuarioReposistorio usuarioReposistorio, SbRotinaDbContext context)
        {
            _usuarioReposistorio = usuarioReposistorio;
            _context = context;
        }


        private async Task<bool> UsuarioExistente(string email)
        {
            if (await _context.Usuarios.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }


        [HttpGet("buscarTodos")]
        public async Task <ActionResult<List<UsuarioModel>>>  BuscarTodosUsuarios() 
            {
            
           List<UsuarioModel> usuarios =  await _usuarioReposistorio.BuscarTodosUsuarios();
            return Ok(usuarios);
 }

        [HttpGet ("buscarPorId")]
        public async Task<ActionResult<UsuarioModel>> BuscarPorId(int id)
        {
            UsuarioModel usuario = await _usuarioReposistorio.BuscarPorId(id);
            return Ok(usuario);
        }

        [HttpPost ("Adicionar")]
        public async Task<ActionResult<UsuarioModel>> Cadastrar([FromBody] UsuarioModel usuarioModel)
        {
            UsuarioModel usuario = await _usuarioReposistorio.Adicionar(usuarioModel);
            return Ok(usuario);
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuarioModel, int id)
        {
            usuarioModel.Id= id;
            UsuarioModel usuario = await _usuarioReposistorio.Atualizar(usuarioModel, id);
            return Ok(usuario);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<UsuarioModel>> Deletar(int id)
        {
           bool deletado = await _usuarioReposistorio.Apagar(id);
            return Ok(deletado);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Autenticar(UsuarioModel creds)
        {
            try
            {
                UsuarioModel usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(creds.Email.ToLower()));

                if (usuario == null)
                    throw new Exception("Usuário não encontrado!");
                if (!usuario.Senha.Equals(creds.Senha))
                    throw new Exception("Senha incorreta!");
               
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
 