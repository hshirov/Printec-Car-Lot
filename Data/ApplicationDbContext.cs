using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public ApplicationDbContext() { }
        public virtual DbSet<Car> Cars { get; set; } 
        public virtual DbSet<Owner> Owners { get; set; } 
        public virtual DbSet<Make> Makes { get; set; } 
        public virtual DbSet<Model> Models { get; set; } 
    }
}
