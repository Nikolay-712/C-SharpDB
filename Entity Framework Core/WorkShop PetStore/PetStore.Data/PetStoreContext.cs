using Microsoft.EntityFrameworkCore;
using PetStore.Models.Models;
using System.Reflection;

namespace PetStore.Data
{
    public class PetStoreContext : DbContext
    {
        public PetStoreContext()
        {
        }

        public PetStoreContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Toy> Toys { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Food> Foods { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=PetStore;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

