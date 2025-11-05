using Euroskills2018.Models;
using Microsoft.EntityFrameworkCore;

namespace Euroskills2018.Data
{
    public class EuroskillsDbContext : DbContext
    {
        public EuroskillsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<OrszagModel> Orszagok { get; set; }
        public DbSet<SzakmaModel> Szakmak { get; set; }
        public DbSet<VersenyzoModel> Versenyzok { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<VersenyzoModel>().HasOne(x => x.Orszag).WithMany(x => x.Versenyzok).HasForeignKey(x => x.orszagId);
            modelBuilder.Entity<VersenyzoModel>().HasOne(y => y.Szakma).WithMany(y => y.Versenyzok).HasForeignKey(y => y.szakmaId);
        }
    }
}
