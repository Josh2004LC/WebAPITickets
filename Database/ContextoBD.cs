using Microsoft.EntityFrameworkCore;
using WebAPITickets.Models;

namespace WebAPITickets.Database
{
    public class ContextoBD : DbContext
    {
        public ContextoBD(DbContextOptions<ContextoBD> options) : base(options)
        {
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Tiquetes> Tiquetes { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Importancias> Importancias { get; set; }
        public DbSet<Urgencias> Urgencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().HasKey(x => x.ro_identificador);
            modelBuilder.Entity<Tiquetes>().HasKey(x => x.ti_identificador);
            modelBuilder.Entity<Usuarios>().HasKey(x => x.us_identificador);
            modelBuilder.Entity<Categorias>().HasKey(x => x.ca_identificador);
            modelBuilder.Entity<Importancias>().HasKey(x => x.im_identificador);
            modelBuilder.Entity<Urgencias>().HasKey(x => x.ur_identificador);
        }
    }
}
