using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportskiKlub.Models;

namespace SportskiKlub.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Slika> Slika { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<Prisustvo> Prisustvo { get; set; }
        public DbSet<Inventar> Inventar { get; set; }
        public DbSet<Galerija> Galerija { get; set; }
        public DbSet<Clanarina> Clanarina { get; set; }
        public DbSet<Trening> Trening { get; set; }
        public DbSet<Ocjena> Ocjena { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Korisnik>().ToTable("Korisnik");
            modelBuilder.Entity<Slika>().ToTable("Slika");
            modelBuilder.Entity<Proizvod>().ToTable("Proizvod");
            modelBuilder.Entity<Prisustvo>().ToTable("Prisustvo");
            modelBuilder.Entity<Inventar>().ToTable("Inventar");
            modelBuilder.Entity<Galerija>().Ignore(p => p.DatotekaSlike);
            modelBuilder.Entity<Clanarina>().ToTable("Clanarina");
            modelBuilder.Entity<Trening>().ToTable("Trening");
            modelBuilder.Entity<Ocjena>().ToTable("Ocjena");
            base.OnModelCreating(modelBuilder);
        }
    }
}
