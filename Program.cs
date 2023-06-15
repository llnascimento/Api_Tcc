using Microsoft.EntityFrameworkCore;
using SbRotina.Data;
using SbRotina.Repositorio;
using SbRotina.Repositorio.Interfaces;

namespace SbRotina
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<SbRotinaDbContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBaseSomee"))
                );

            builder.Services.AddScoped<IUsuarioReposistorio, UsuarioRepositorio >();

            builder.Services.AddScoped<ITarefaRepositorio, TarefaRepositorio>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}