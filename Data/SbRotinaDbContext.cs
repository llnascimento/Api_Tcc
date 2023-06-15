using Microsoft.EntityFrameworkCore;
using SbRotina.Data.Map;
using SbRotina.Models;

namespace SbRotina.Data
{
    public class SbRotinaDbContext : DbContext
    {
        public SbRotinaDbContext(DbContextOptions<SbRotinaDbContext> options)
            : base(options)
        {
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }

        public DbSet <TarefaModel> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new TarefaMap());
            base.OnModelCreating(modelBuilder);

            UsuarioModel user = new UsuarioModel();
            user.Id = 1;
            user.Nome = "UsuarioAdmin";
            user.Email = "Usuario@gmail.com";
            user.Senha = "1233456";
            user.SexoUsuario = "M";

            modelBuilder.Entity<UsuarioModel>().HasData(user);

        }
    }
}
