using Microsoft.EntityFrameworkCore;
using Agenda.Api.Models;

namespace Agenda.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option)
            :base(option)
        {

        }

        public DbSet<Contato> Contatos { get; set; }

        public DbSet<Sexo> Sexos { get; set; } 

        public DbSet<Usuario> Usuarios { get; set; }


    }
}