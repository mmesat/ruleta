using Microsoft.EntityFrameworkCore;
using RuletaAPi.Models;

namespace RuletaAPi.Data
{
    public class ApplicationContext : DbContext
    {


        //public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        //{
        //}
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Ruleta> Ruletas { get;set; }
        public DbSet<Apuesta> Apuesta { get;set; } 
    }
}
