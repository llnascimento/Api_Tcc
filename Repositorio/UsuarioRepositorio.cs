using Microsoft.EntityFrameworkCore;
using SbRotina.Data;
using SbRotina.Models;
using SbRotina.Repositorio.Interfaces;

namespace SbRotina.Repositorio
{
    public class UsuarioRepositorio : IUsuarioReposistorio
    {
        private readonly SbRotinaDbContext _dbContext;
        public UsuarioRepositorio(SbRotinaDbContext sbRotinaDbContext)
        {
            _dbContext = sbRotinaDbContext;
        }

        public async Task<UsuarioModel> BuscarPorId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
            
            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
           UsuarioModel usuarioPorId = await BuscarPorId(id);
           
            if(usuarioPorId == null)
            {
                throw new Exception($"Usuario para o Id: {id} não foi encontrado. ");
            }

            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;
            usuarioPorId.Senha = usuario.Senha;

            _dbContext.Usuarios.Update(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario para o Id: {id} não foi encontrado. ");
            }

            _dbContext.Usuarios.Remove(usuarioPorId);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        

    }
}
