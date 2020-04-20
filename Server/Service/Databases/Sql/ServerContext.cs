using Microsoft.EntityFrameworkCore;
using Service.Databases.Sql.Models;

namespace Service.Databases.Sql
{
    public class ServerContext : DbContext
    {
        public ServerContext(DbContextOptions<ServerContext> options)
            : base(options)
        {
        }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<CubeData> CubeDatas { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Cube> Cubes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cube>()
                        .HasKey(c => new { c.CubeId, c.UserId });
        }
    }
}
